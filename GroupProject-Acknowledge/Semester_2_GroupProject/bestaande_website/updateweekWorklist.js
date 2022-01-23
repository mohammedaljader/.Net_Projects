$( document ).ready(function() {
    // get the planning for this update week if available
    "use strict";
    uwWorklist.getWorklist();
    uwWorklist.setFilterBinding();
    uwWorklist.setModalBinding();
});

(function($) {

    "use strict";

    window.uwWorklist = function(){};

    var listNamePlanning = "Planning",
        listNameUpdateweek = "UpdateWeken",
        listNameSubscriptions = "Inschrijvingen",
        listNameWorkinstructions = "Werkinstructies",    
        listNameTaxonomy = "TaxonomyHiddenList",
        objWorkitems = [],
        objWorkinstructions = [],
        objCurrentUser = {},
        objFields = [],
        objUpdateweeks = [],
        objUpdateweek = {},
        objUniqueDays = [],
        objUniqueCustomers = [],
        objUniqueWindowServers = [],
        objUniqueEnvironments = [],
        objFilters = [],
        objSubscriptions = [],
        objSubscription = {},
        objTaxonomy = [];

//PUBLIC METHODS
    uwWorklist.showComments = function(planningId) {
        var obj = getWorkitemObject(planningId);

        showComments(obj);
    };

    uwWorklist.updateWorklistItem = function(planningId) {
        var obj = getWorkitemObject(planningId),
            updateWorkItemRequest = getUpdateWorkItemRequest(obj);

        updateWorkItemRequest
            .done(function(){
                showComments(obj);
                setColorCode(planningId);

                //update the worklist-object
                updateWorklistObject(obj);
            })
            .fail(function(error){
                alert("Er is iets misgegaan tijdens het bijwerken van het werkitem. \n\n" + error.responseText);
                console.error(error);
            });
    };

    uwWorklist.getWorklist = function() {
        $("#uw-worklist-container #uw-worklist-engineer").html("<img src='" + _spPageContextInfo.siteAbsoluteUrl + "/style%20library/images/ajax-loader.gif'/> De gegevens worden opgehaald...<br />");

        var userId = _spPageContextInfo.userId,
            worklistRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNamePlanning, ",uwWerklijstUpdateweek/Id,uwWerklijstUpdateweek/Title,uwWerklijstUpdateweek/uwBegindatum,uwWerklijstEngineer/Title,uwWerklijstServer/uwServerNaam,uwWerklijstContactpersoonKlant/uwContactpersoonNaam,uwWerklijstContactpersoonKlant/uwContactpersoonEmail,uwWerklijstContactpersoonKlant/uwContactpersoonTelefoon,uwWerklijstContactpersoonAcknowl/uwContactpersoonNaam,uwWerklijstContactpersoonAcknowl/uwContactpersoonEmail,uwWerklijstContactpersoonAcknowl/uwContactpersoonTelefoon&$expand=uwWerklijstUpdateweek/Id,uwWerklijstEngineer/Id,uwWerklijstServer/Id,uwWerklijstContactpersoonKlant/Id,uwWerklijstContactpersoonAcknowl/Id&$filter=(uwWerklijstEngineer/Id eq " + userId + ") and (uwWerklijstUpdateweek/uwActiefCalc eq 'Ja')"),
            updateWeekRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNameUpdateweek, ",uwRestaurant/Id,uwRestaurant/Title,uwRestaurant/uwRestaurantUrl&$expand=uwRestaurant/Id&$filter=uwActief eq 'Ja'"),
            workinstructionsRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNameWorkinstructions, "&$expand=FieldValuesAsText"),
            currentUserRequest = getCurrentUserRequest(),
            fieldsRequest = getFieldsRequest(_spPageContextInfo.webAbsoluteUrl, listNamePlanning);

            fieldsRequest.done(function(){
                // console.log(data);
            }).fail(function(error){
                console.error(error);
            });

        $.when(worklistRequest, currentUserRequest,fieldsRequest,updateWeekRequest, workinstructionsRequest)
        .done(function(a1, a2, a3, a4, a5) {
            setWorklistObjects(a1[0]);
            setCurrentUserObject(a2[0]);
            setFieldObjects(a3[0]);
            setUpdateWeekObjects(a4[0]);
            setWorkinstructionsObjects(a5[0]);
        }).then(function(){
            sortWorklistObjects();
            setUniqueValues();
            objUpdateweek = objUpdateweeks[0];
        }).then(function(){
            renderWorklist();
            setWorklistTitle();
            setFilterOptions();
            setDinnerMailButton();
        }).then(function(){
            checkShowDinnerScreen();
        }).then(function(){
            var taxonomyRequest = getItemsRequest(_spPageContextInfo.siteAbsoluteUrl,listNameTaxonomy, "");  
                taxonomyRequest.done(setTaxonomyObjects).then(setCustomersLabel);
        }).fail(function (error){
            console.error(error);
            $("#update-week-planning").html("Oeps... Er is iets misgegaan bij het ophalen van de gegevens voor de update week.\n\n" + error.responseText);
        });
    };

    uwWorklist.setFilterBinding = function() {
        $(document).on('change', '.uw-worklist-filter-checkbox', function(){
            var filterType = $(this).closest("div").data('filter-type'),
                filter = $(this).val(),
                checked = $(this).prop('checked'),
                objFilter = {};

                objFilter.filterType = filterType;
                objFilter.filter = filter;

                if (checked === false) {
                    $("div[data-" + filterType + "='" + filter + "']").hide();
                    removeFilter(filter, filterType);
                } else {
                    objFilters.push(objFilter);
                    $("div[data-" + filterType + "='" + filter + "']").show();
                }

                renderWorklist();
                setFilterOptions();
        });
    };

    uwWorklist.removeAllFilters = function() {
        objFilters.length = 0;
        renderWorklist();
        setFilterOptions();
    };

    uwWorklist.setModalBinding = function() {
        $(document).on('click', '#modal-dialog-container .close', function(){

            $("#modal-dialog-container").hide();
        });

        $(window).on('click', function(){
            if ( $(event.target).hasClass("modal-dialog") ) {
                $("#modal-dialog-container").hide();
            }
        });

    };

    uwWorklist.showContactInfo = function(planningId) {
        var obj = getWorklistObject(planningId),
            resultHtml = "";

        resultHtml += "<span class='close'>&times</span>";   
        resultHtml += "<div class='contact-info'>";

        //contact info
        resultHtml += getContactInfoHtml(obj);

        //Workinstructions
        resultHtml += getWorkInstructionsHtml(obj.CustomerName);

        //Messages to customer
        resultHtml += getMessagesHtml();
                 
        resultHtml += "</div>";
        $("#modal-dialog-container .modal-content").html(resultHtml);
        $("#modal-dialog-container").show();
        
    };

    uwWorklist.showDinnerInfo = function() {

        var request = getSubscriptionsRequest(),
            _html = "",
            body = "";

            request
            .done(function(data){
                setSubscriptionsObject(data);
            })
            .then(function(){
                if (isEngineerPlanned("Donderdag")) {
                    
                    body += 'Beste collega, <br /><br />';
            
                    body += 'Deze keer is de keuze gevallen op <a href="' + objUpdateweek.RestaurantUrl + '" target="_blank">' + objUpdateweek.Restaurant + '</a>. ';
                    body += 'Geef hieronder voor donderdag 12:00 door wat jij wil eten zodat dit tijdig besteld kan worden.<br /><br />';
                    body += 'Zoals iedere maand is er een budget van 14 Euro per persoon. <br />';
                    body += '<b>Omstreeks 17:15 zal het eten geleverd worden.</b>.<br /><br />';
                    
                    body += 'Met vriendeijke groet,<br />';
                    body += 'Patch Management';    
        
                    _html += "<span class='close'>&times</span>"; 
                    _html += "<div class='dinner-info'>";
                    _html += "<span class='content-header'>Bestelling eten donderdag</span>";
                    _html += "<span class='content-line'>" + body + "</span>";
                    _html += "<span class='content-line'><span class='content-subheader'>Je bestelling:<span class='dinner-saved-message no-display'>(niet opgeslagen)</span></span>";
                    _html += "<span class='content-line'><input id='dinner-order' class='content-field' onkeyup='Javascript:uwWorklist.showDinnerSavedStatus()' /></span>";
                    _html += "<span class='content-line content-button'><a class='dinner-info-button' href='Javascript:uwWorklist.processDinner()'>Bevestigen</a></span>";
        
                    _html += "</div>";
            
                    $("#modal-dialog-container .modal-content").html(_html);
                    $("#dinner-order").val(objSubscription.Dinner);
                    $("#modal-dialog-container").show();
                }
            })
            .fail(function(error){
                alert("Er is een fout opgetreden:\n\n" + error.responseText);
                console.error(error);
            });            
    };

    uwWorklist.processDinner = function() {
        $(".dinner-info-button").hide();
        var dinner = $("#dinner-order").val();

        objSubscription.Dinner = dinner;

        //update field in subcription item.
        var request = getUpdateSubscriptionRequest();

        request.done(function(){
            //show the updated dinner-message
            
            // uwWorklist.showDinnerSavedStatus();
            $("#modal-dialog-container").hide();
        }).fail(function(error){
            alert("Er is een fout opgetreden tijdens het bijwerken van je bestelling.\n\n" + error.responseText);
        });
    };

    uwWorklist.showDinnerSavedStatus = function() {
        var input = $("#dinner-order").val(),
            saved = checkEmptyValue(objSubscription.Dinner);
        
        if (input.toLowerCase() === saved.toLowerCase() ) {
            $(".dinner-saved-message").removeClass("display");
            $(".dinner-saved-message").addClass("no-display");
        } else {
            $(".dinner-saved-message").removeClass("no-display");
            $(".dinner-saved-message").addClass("display");
        }
    };

//OBJECTS
    function setWorklistObjects(data) {
        var _items = data.d.results;
        $.each(_items, function (indexInArray, _item) { 
            setWorklistObject(_item);        
        });

    }

    function setWorklistObject(props) {

        var obj = {};

        obj.EngineerName = props.uwWerklijstEngineer.Title;
        obj.EngineerId = props.uwWerklijstEngineerId;
        obj.Customer = props.uwWerklijstKlant;
        obj.Environment = props.uwWerklijstOmgeving;
        obj.AddtionalInfo = props.uwWerklijstExtraInfo;
        obj.CustomerEnvironment = props.uwWerklijstKlantOmgeving;
        obj.CustomerAdditionalInfo = props.uwWerklijstKlantExtraInfo;
        obj.Level = props.uwWerklijstNiveau;
        obj.UpdateDay = props.uwWerklijstUpdateDag;
        obj.UpdateDayOrder = props.uwWerklijstUpdateDagVolgorde;
        obj.UpdateWindowCustomer = props.uwWerklijstKlantUpdateWindow;
        obj.UpdateWindowServer = checkEmptyValue(props.uwWerklijstServerUpdateWindow);
        obj.UpdateMethod = props.uwWerklijstUpdateMethode;
        obj.Server = props.uwWerklijstServer.uwServerNaam;
        obj.PlanningId = props.ID;
        obj.Updateweek = props.uwWerklijstUpdateweek.Title;
        obj.UpdateweekBegindatum = props.uwWerklijstUpdateweek.uwBegindatum;
        obj.Status = props.uwWerklijstStatus;
        obj.Result = props.uwWerklijstResultaat;
        obj.Comments = props.uwWerklijstOpmerking;
        obj.InfoServer= props.uwWerklijstServerOpmerking;
        obj.ContactKlantNaam = checkEmptyValue(props.uwWerklijstContactpersoonKlant.uwContactpersoonNaam);
        obj.ContactKlantEmail = checkEmptyValue(props.uwWerklijstContactpersoonKlant.uwContactpersoonEmail);
        obj.ContactKlantTelefoon = checkEmptyValue(props.uwWerklijstContactpersoonKlant.uwContactpersoonTelefoon);        
        obj.ContactAcknowledgeNaam = checkEmptyValue(props.uwWerklijstContactpersoonAcknowl.uwContactpersoonNaam);
        obj.ContactAcknowledgeEmail = checkEmptyValue(props.uwWerklijstContactpersoonAcknowl.uwContactpersoonEmail);
        obj.ContactAcknowledgeTelefoon = checkEmptyValue(props.uwWerklijstContactpersoonAcknowl.uwContactpersoonTelefoon); 
        obj.ContactAcknowledgeProjectnummer = checkEmptyValue(props.uwWerklijstProjectnummer);
        obj.Werkinstructies = checkEmptyValue(props.uwWerklijstWerkinstructies);

        objWorkitems.push(obj);
    }

    function setFieldObjects(data) {
        var _items = data.d.results;
        $.each(_items, function (indexInArray, _item) { 
            setFieldObject(_item);        
        });
    }

    function setFieldObject(props) {
        var obj = {};

        obj.StaticName = props.StaticName;
        obj.Choices = props.Choices.results;

        objFields.push(obj);
    }    

    function setUpdateWeekObjects(data) {
        var _items = data.d.results;
        $.each(_items, function (indexInArray, _item) { 
            setUpdateWeekObject(_item);        
        });
    }

    function setUpdateWeekObject(props) {
        var obj = {};

        obj.Id = props.Id;
        obj.Title = props.Title;
        obj.Actief = props.uwActief;
        obj.Startdate = props.uwBegindatum;
        obj.RestaurantId = props.uwRestaurantId;
        obj.Restaurant = checkEmptyValue(props.uwRestaurant.Title);
        obj.RestaurantUrl = checkEmptyValue(props.uwRestaurant.uwRestaurantUrl);

        objUpdateweeks.push(obj);
    }

    function setWorkinstructionsObjects(data) {
        var _items = data.d.results;

        $.each(_items, function (indexInArray, _item) { 
            setWorkinstructionsObject(_item);
        });

    }

    function setWorkinstructionsObject(props) {
        var obj = {};

        obj.FileRef = props.FieldValuesAsText.FileRef;
        obj.FileLeafRef = props.FieldValuesAsText.FileLeafRef;
        obj.Title = checkEmptyValue(props.Title);
        obj.Relaties = props.Relaties;

        objWorkinstructions.push(obj);
    }

    function setCurrentUserObject(data) {
        objCurrentUser.Title = data.d.Title;
    }

    function setSubscriptionsObject(data) {
        var _items = data.d.results;

        $.each(_items, function (indexInArray, _item) { 
            setSubscriptionObjectAllUsers(_item);
        });
    }

    function setSubscriptionObjectAllUsers(props) {
        var obj = {};

        obj.Id = props.Id;
        obj.Dinner = checkEmptyValue(props.uwInschrijvingBestelling);

        objSubscriptions.push(obj);

        if (props.uwInschrijvingEngineerId === _spPageContextInfo.userId) {
            objSubscription = obj;
        }
    }    

    function setTaxonomyObjects(data) {
        var _items = data.d.results;

        $.each(_items, function (indexInArray, _item) { 
            setTaxonomyObject(_item);
        });

    }

    function setTaxonomyObject(props) {
        var obj = {};

        obj.TermGuid = props.IdForTerm;
        obj.TermSetGuid = props.IdForTermSet;
        obj.TermStoreGuid = props.IdForTermStore;
        obj.Path = props.Path;
        obj.Title = props.Title;

        objTaxonomy.push(obj);
    }

//LINQ
    function sortWorklistObjects() {
        objWorkitems = Enumerable
                            .from(objWorkitems)
                            .orderBy(function(x) {return x.UpdateDayOrder;})
                            .thenBy(function(x) {return x.CustomerAdditionalInfo;})
                            .thenBy(function(x) {return x.Environment;})
                            .thenBy(function(x) {return x.UpdateWindowServer;})
                            .toArray();
    }

    function getFieldObject(strStaticName) {
        var obj = Enumerable
                    .from(objFields)
                    .where(function(x) {return x.StaticName === strStaticName;})
                    .toArray();

        return obj[0];
    }

    function setUniqueValues() {
        setUniqueWorklistDaysEngineer();
        setUniqueWorklistCustomersEngineer();
        setUniqueWorklistWindowServers();
        setUniqueWorklistEnvironments();
    }

    function setUniqueWorklistDaysEngineer() {
        objUniqueDays = Enumerable
                        .from(objWorkitems)
                        .distinct(function(x) {return x.UpdateDay;})
                        .select("{Title:$.UpdateDay, UpdateDayOrder:$.UpdateDayOrder}")
                        .orderBy(function(x) {return x.UpdateDayOrder;})
                        .toArray();
    }

    function setUniqueWorklistCustomersEngineer() {
        objUniqueCustomers = Enumerable
                            .from(objWorkitems)
                            .orderBy(function(x) {return x.CustomerAdditionalInfo;})
                            .distinct(function(x) {return x.CustomerAdditionalInfo;})
                            .select("{Title:$.CustomerAdditionalInfo}")                            
                            .toArray();                                          
    }

    function setUniqueWorklistWindowServers() {
         objUniqueWindowServers = Enumerable
                            .from(objWorkitems)
                            .orderBy(function(x) {return x.UpdateWindowServer;})
                            .distinct(function(x) {return x.UpdateWindowServer;})
                            .select("{Title:$.UpdateWindowServer}")                            
                            .toArray();          
    }

    function setUniqueWorklistEnvironments() {
         objUniqueEnvironments = Enumerable
                            .from(objWorkitems)
                            .orderBy(function(x) {return x.Environment;})
                            .distinct(function(x) {return x.Environment;})
                            .select("{Title:$.Environment}")                            
                            .toArray();          
    }

    function checkFilterType(filterType) {
        var nrObjects = Enumerable
                        .from(objFilters)
                        .where(function(x) {return x.filterType === filterType; } )
                        .count();
        if (nrObjects === 0) {
            return false;
        } else {
            return true; //elements of filterType are filtered
        }
    }

    function checkItemFiltered(filter, filterType) {
        var returnValue = "";

        if (checkFilterType(filterType) === true ) {
            var nrObjects = Enumerable
                            .from(objFilters)
                            .where(function(x) {return x.filter === filter; } )
                            .where(function(x) {return x.filterType === filterType; } )
                            .count();

            if (nrObjects > 0) {
                returnValue = "itemFiltered";
            } else {
                returnValue = "itemNotFiltered";
            }

        } else {
            returnValue = "categoryNotFiltered";
        }

        return returnValue;
    }

    function updateWorklistObject(objChangedWorkItem) {
        var obj = Enumerable
            .from(objWorkitems)
            .where(function(x) {return x.PlanningId === parseInt(objChangedWorkItem.planningid) ;})
            .toArray(),
            objWorkitem = {};
            
        if (obj.length > 0) {
            objWorkitem = obj[0];
            objWorkitem.Result = objChangedWorkItem.result;
            objWorkitem.Comments = objChangedWorkItem.comments;
        }
    }

    function getWorklistObject(planningId) {
        var obj = Enumerable
                    .from(objWorkitems)
                    .where(function(x) {return x.PlanningId === planningId;})
                    .toArray();

        return obj[0];
    }

    function isEngineerPlanned(updateDay) {
        var nrObjects =Enumerable
                    .from(objWorkitems)
                    .where(function(x) {return x.UpdateDay === updateDay;})
                    .count();

        if (nrObjects > 0) {
            return true;
        } else {
            return false;
        }
    }

    function setCustomersLabel() {
        var obj = [];

        $.each(objWorkitems, function (indexInArray, objWorkitem) { 
            obj = Enumerable
            .from(objTaxonomy)
            .where(function(x) {return x.TermGuid === objWorkitem.Customer.TermGuid;})
            .toArray();

            objWorkitem.CustomerName = obj[0].Title;
        });


        $.each(objWorkinstructions, function (indexInArray, objWorkinstruction) { 

            if (objWorkinstruction.Relaties) {
                obj = Enumerable
                .from(objTaxonomy)
                .where(function(x) {return x.TermGuid === objWorkinstruction.Relaties.TermGuid;})
                .toArray();

                objWorkinstruction.CustomerName = obj[0].Title;
            } else {
                objWorkinstruction.CustomerName = "";
            }
        });

    }

    function getWorkinstructionsCustomer(customerName) {
        var obj = Enumerable
                    .from(objWorkinstructions)
                    .where(function(x) {return x.CustomerName === customerName;})
                    .orderBy(function(x){return x.FileRef;})
                    .toArray(0);
        
        return obj;
    }

//METHODS
    function renderWorklist() {
        var resultHtml = "",
            choicefieldResultHtml = "",
            choicefieldStatusHtml = "",
            itemColorCodeClass ="",
            classComments = "no-display",
            checkedComments = "";

        if (objUpdateweeks.length === 0) {
            $("#uw-worklist-container #uw-worklist-items").html("<div id='no-work-items'><div id='no-work-items-header'>Geen update week actief</div><div id='no-work-items-description'></div></div></div>");
            $("#uw-worklist-container #uw-worklist-engineer").html("");
            return;
        } else if (objWorkitems.length === 0) {
            $("#uw-worklist-container #uw-worklist-items").html("<div id='no-work-items'><div id='no-work-items-header'>Gamen en bier drinken vanavond!!</div><div id='no-work-items-description'>Er zijn geen items aan jouw toegekend!</div></div></div>");
            $("#uw-worklist-container #uw-worklist-engineer").html("");  
            $("#uw-worklist-message").hide();          
            return;
        }

        //div for modal dialog container
        resultHtml += "<div id='modal-dialog-container' class='modal-dialog'><div class='modal-content'></div></div>";

        //reset filter count
         resetFilterOptions();

        //set worklist headers
        resultHtml += setWorklistHeaders();

        $.each(objWorkitems, function (indexInArray, objWorkitem) { 
            if (isItemVisible(objWorkitem) === false) {
                return true; //item not check, some other item in one of the categories is checked
            } else {
                // add count to relevant filter options
                updateFilterOptions(objWorkitem);
            }

            choicefieldResultHtml = getChoiceFieldCode("uwWerklijstResultaat", objWorkitem.Result, objWorkitem.PlanningId);
            choicefieldStatusHtml = getChoiceFieldCode("uwWerklijstStatus", objWorkitem.Status, objWorkitem.PlanningId);
            itemColorCodeClass = getItemClass(objWorkitem.Result);
            checkedComments = "";
            classComments = "no-display";

            if (getShowComments(objWorkitem.PlanningId, objWorkitem.Result, checkEmptyValue(objWorkitem.Comments), false) === true) {
                checkedComments = "checked";
                classComments = "display";
            }

            resultHtml += "<div data-planning-id='" + objWorkitem.PlanningId + "'  data-day='" + objWorkitem.UpdateDay + "' data-customer='" + objWorkitem.CustomerAdditionalInfo + "' data-window-server='" + checkEmptyValue(objWorkitem.UpdateWindowServer) + "' data-environment='" + objWorkitem.Environment + "' class='uw-work-item " + itemColorCodeClass + "'>";
            resultHtml += "<span class='customer-addinfo " + itemColorCodeClass + "' title='" + checkEmptyValue(objWorkitem.CustomerAdditionalInfo) + "'><a href='javascript:uwWorklist.showContactInfo(" + objWorkitem.PlanningId + ")'>" + getSubstring(checkEmptyValue(objWorkitem.CustomerAdditionalInfo), 34) + "</a></span>";
            resultHtml += "<span class='contact-info'></span>";
            resultHtml += "<span class='environment'>" + checkEmptyValue(objWorkitem.Environment) + "</span>";            
            resultHtml += "<span class='update-day " + itemColorCodeClass + "' title='" + checkEmptyValue(objWorkitem.UpdateDay) + "'>" + getSubstring(checkEmptyValue(objWorkitem.UpdateDay), 10) + "</span>";
            resultHtml += "<span class='server' title='" + checkEmptyValue(objWorkitem.Server) + "'>" + getSubstring(checkEmptyValue(objWorkitem.Server), 35) + "&nbsp;</span>";         
            resultHtml += "<span class='update-method'>" + checkEmptyValue(objWorkitem.UpdateMethod) + "&nbsp;</span>";
            resultHtml += "<span class='update-window'>" + checkEmptyValue(objWorkitem.UpdateWindowCustomer) + "&nbsp;</span>";
            resultHtml += "<span class='update-window'>" + checkEmptyValue(objWorkitem.UpdateWindowServer) + "&nbsp;</span>";  
            // resultHtml += "<span class='status'>" + choicefieldStatusHtml +"</span>";
            resultHtml += "<span class='result' >" + choicefieldResultHtml + "</span>";                            
            resultHtml += "<span class='has-comments'><input class='comments-checkbox' type='checkbox' value='comments' " + checkedComments + " onchange=uwWorklist.showComments('" + objWorkitem.PlanningId + "') /></span>";
            resultHtml += "<span class='info-server' title='" + checkEmptyValue(objWorkitem.InfoServer) + "'>" + getSubstring(checkEmptyValue(objWorkitem.InfoServer), 10) + "&nbsp;</span>";              
            resultHtml += "<span class='comments " + classComments + "'><input type='text' id='" + objWorkitem.PlanningId + "-comments' class='comments-input' value='" + checkEmptyValue(objWorkitem.Comments) + "' placeholder='Opmerkingen' onchange='uwWorklist.updateWorklistItem(\"" + objWorkitem.PlanningId + "\")'/></span>";
            resultHtml += "</div>";
        });

        $("#uw-worklist-container #uw-worklist-items").html(resultHtml);

    }

    function isItemVisible(obj) {
        var returnValue = true;

        if (checkItemFiltered(obj.UpdateDay, "day") === "itemNotFiltered") {
            returnValue = false;
        }

        if (checkItemFiltered(obj.CustomerAdditionalInfo, "customer") === "itemNotFiltered") {
            returnValue = false;
        }

        if (checkItemFiltered(obj.UpdateWindowServer, "window-server") === "itemNotFiltered") {
            returnValue = false;
        }

        if (checkItemFiltered(obj.Environment, "environment") === "itemNotFiltered") {
            returnValue = false;
        }        

        return returnValue;
    }

    function updateFilterOptions(objWorkitem) {
        updateFilterOption(objWorkitem.CustomerAdditionalInfo, objUniqueCustomers);
        updateFilterOption(objWorkitem.UpdateDay, objUniqueDays);        
        updateFilterOption(objWorkitem.UpdateWindowServer, objUniqueWindowServers);  
        updateFilterOption(objWorkitem.Environment, objUniqueEnvironments);                  
    }

    function updateFilterOption(filterField, objFilterArray) {
        $.each(objFilterArray, function (indexInArray, obj) { 
             if(obj.Title === filterField) {
                if (typeof obj.Count === "number") {
                    obj.Count = obj.Count + 1;
                } else {
                    obj.Count = 1;
                }
                
                return false;
             }
        });
    }

    function removeFilter(filter, filterType) {
        $.each(objFilters, function (indexInArray, obj) { 
            if (obj.filter === filter && obj.filterType === filterType) {
                objFilters.splice(indexInArray,1);
                return false;
            }
        });    
    }

    function resetFilterOptions() {
        resetFilterOption("Count", objUniqueCustomers);
        resetFilterOption("Count", objUniqueDays);        
        resetFilterOption("Count", objUniqueWindowServers);         
        resetFilterOption("Count", objUniqueEnvironments);               
    }

    function resetFilterOption(filterField, objFilterArray) {
        $.each(objFilterArray, function (indexInArray, obj) { 
             obj[filterField] = 0;
        });
    }

    function setWorklistHeaders() {
        var _html = "";

        _html += "<div class='uw-work-item uw-work-item-headers'>";
        _html += "<span class='customer-addinfo'>Klant</span>";
        _html += "<span class='environment'>Omgeving</span>";        
        _html += "<span class='update-day'>Dag</span>";
        _html += "<span class='server'>Server</span>";
        _html += "<span class='update-method'>Methode</span>";      
        _html += "<span class='update-window'>Window klant</span>";
        _html += "<span class='update-window'>Window server</span>";                          
        // _html += "<span class='status'>Status</span>";
        _html += "<span class='result'>Resultaat</span>";  
        _html += "<span class='has-comments'>&nbsp;</span>";    
        _html += "<span class='info-server'>Info server</span>";          
        _html += "</div>";

        return _html;
    }

    function setWorklistTitle() {
        var resultHtml = "",
            updateWeeks = getActiveUpdateweeks();

        if (objUpdateweeks.length > 0) {
            resultHtml = "Werklijst " + objCurrentUser.Title + " - " + updateWeeks;
            $("#uw-worklist-container #uw-worklist-engineer").html(resultHtml);
        }
    }

    function getActiveUpdateweeks() {

        var updateWeeks = "";

        $.each(objUpdateweeks, function (indexInArray, updateWeek) {
            if (indexInArray > 0) {
                updateWeeks += " + ";
            }

            updateWeeks += updateWeek.Title + " (begint op " + setDisplayDate(updateWeek.Startdate)  +")";
        });

        return updateWeeks;       
    }

    function setFilterOptions() {
        //Update dagen voor engineer
        var returnHtml = "";

        returnHtml += "<div id='uw-worklist-filter-container'>";
    
        returnHtml += getFilterHtml(objUniqueDays, "days", "day", "Dag");
        returnHtml += getFilterHtml(objUniqueCustomers, "customers", "customer", "Klant");
        returnHtml += getFilterHtml(objUniqueEnvironments, "environments", "environment", "Omgeving");
        returnHtml += getFilterHtml(objUniqueWindowServers, "window-servers", "window-server", "Window server");

        returnHtml += "<br/><div><a href='javascript:uwWorklist.removeAllFilters()'><i class='fa fa-eraser' aria-hidden='true'></i> Wis filters</a></div>";
        returnHtml += "</div>";

        $("#sideNavBox #uw-worklist-filter-container").remove();
        $("#sideNavBox").append(returnHtml);
    }

    function getFilterHtml(objArray, filterClass, filterType, filterLabel) {
        var returnHtml = "";
        var strChecked = "";
        var strText = "";
        var filterStatus = "";

        returnHtml += "<div class='uw-worklist-filter " + filterClass + "' data-filter-type='" + filterType + "'>" + filterLabel + "<ul>";
        $.each(objArray, function (indexInArray, obj) { 
            strChecked = "";
            strText = obj.Title;

            if (strText.length === 0 ) {
                strText = "(Leeg)";
            }

            filterStatus = checkItemFiltered(obj.Title, filterType);

            if (filterStatus === "categoryNotFiltered") {
                strChecked = "";
            } else if (filterStatus === "itemNotFiltered") {
                strChecked = "";
            } else if (filterStatus === "itemFiltered") {
                strChecked = "checked";
            }

            returnHtml += "<li><label><input class='uw-worklist-filter-checkbox' type='checkbox' value='" + obj.Title + "' " + strChecked + " />" + strText + " (" + obj.Count + ")</label></li>";
        });
        returnHtml += "</ul></div>";  

        return returnHtml;
    }

    function setColorCode(planningId) {
        var obj = getWorkitemObject(planningId),
            itemColorCodeClass = getItemClass(obj.result);

        setItemClass(obj.workitem, itemColorCodeClass);
        setItemClass($(obj.workitem).find(".customer-addinfo"), itemColorCodeClass);
        setItemClass($(obj.workitem).find(".update-day"), itemColorCodeClass); 
    }

    function getChoiceFieldCode(strStaticName, value, planningId) {
        var obj = getFieldObject(strStaticName),
            choicefieldHtml = "<select>",
            selected = "";

            choicefieldHtml = "<select onchange=uwWorklist.updateWorklistItem('" + planningId + "')>";

        $.each(obj.Choices, function (indexInArray, choice) { 
            selected = "";
            if (choice === value) {
                selected = "selected='selected'";
            }
             choicefieldHtml += "<option value='" + choice + "' " + selected + "'>" + choice + "</option>";
        });

        choicefieldHtml += "</select>";
  
        return choicefieldHtml;
    }

    function getItemClass(itemResult) {
        var returnValue = "grey";

        if (typeof itemResult === "string") {
            switch (itemResult.toLowerCase()) {
                case "niet afgerond":
                    returnValue = "grey";
                    break;
                case "niet gestart":
                    returnValue = "grey";
                    break;    
                case "in behandeling":
                    returnValue = "darkgrey";
                    break;                                    
                case "klaar":
                    returnValue = "green";
                    break;
                case "klaar (minor issues)":
                    returnValue = "yellow";
                    break;            
                case "klaar (major issues)":
                    returnValue = "yellow";
                    break;
                case "mislukt":
                    returnValue = "red";
                    break;  
                case "geannuleerd":
                    returnValue = "green"; //workitems worden niet getoond, dit is indien engineer zelf status op geannuleerd zet
                    break;                                                                      
            }                 
        }

        return returnValue;
    }

    function setItemClass(selector, color) {
        $(selector).removeClass("grey").removeClass("darkgrey").removeClass("green").removeClass("yellow").removeClass("red").addClass(color);
    }

    function getWorkitemObject(planningId) {
            var workitem = $("[data-planning-id=" + planningId + "]"),
            obj = {};

        obj.planningid = planningId;
        obj.result = $(workitem).find(".result select").val();
        obj.status = $(workitem).find(".status select").val();
        obj.comments = $(workitem).find(".comments .comments-input").val();
        obj.checked = $(workitem).find(".has-comments .comments-checkbox").prop("checked");
        obj.workitem = workitem;

        return obj;
    }

    function getShowComments(planningId, result, comments, checkbox) {
        var returnValue = false,
            arrResultNoComments = ["klaar", "niet afgerond", "niet gestart", "in behandeling", "geannuleerd"],
            pos = arrResultNoComments.indexOf(result.toLowerCase() );

        if ( checkbox === true) {
            returnValue = true; 
        } else if (pos === -1) {
            returnValue = true;
        } else if (comments !== "") {
            returnValue = true;
        }

        return returnValue;
    }

    function showComments(objWorkitem) {
        if (getShowComments(objWorkitem.planningid, objWorkitem.result, objWorkitem.comments, objWorkitem.checked) === true) {
            $(objWorkitem.workitem).find(".comments").removeClass("no-display").addClass("display");
            $(objWorkitem.workitem).find(".has-comments .comments-checkbox").prop('checked', true);
        } else {
            $(objWorkitem.workitem).find(".comments").removeClass("display").addClass("no-display");
            $(objWorkitem.workitem).find(".has-comments .comments-checkbox").prop('checked', false);                   
        }
    }

    function setDinnerMailButton() {
        var _html = "";

        if (objUpdateweek.Restaurant !== "" && isEngineerPlanned("Donderdag")) {
            _html = "<a href='Javascript:uwWorklist.showDinnerInfo()'>Eten bestellen</a>";
            $("#uw-worklist-dinner-show").html(_html);
            $("#uw-worklist-dinner-show").show();
        } 
    }

    function checkShowDinnerScreen() {
        var action = checkEmptyValue(getQueryStringValue("action"));
        
        if (action.toLowerCase() === "dinner") {
            uwWorklist.showDinnerInfo();
        }
    }

    function getContactInfoHtml(obj) {

        var _html = "" ;

        _html += "<span class='content-header primary'>Contactinformatie</span>";
        _html += "<span class='content-subheader'>" + obj.CustomerAdditionalInfo + " (" + obj.Environment + ")</span>";
        _html += "<span class='content-line'><span class='content-label'>Naam:</span>";
        _html += "<span class='content-field'>" + obj.ContactKlantNaam + "</span></span>";
        _html += "<span class='content-line'><span class='content-label'>E-mail:</span>"; 
        _html += "<span class='content-field'><a href='mailto:" + obj.ContactKlantEmail + "'>" + obj.ContactKlantEmail + "</a></span></span>";               
        _html += "<span class='content-line'><span class='content-label'>Telefoon:</span>";                
        _html += "<span class='content-field'>" + obj.ContactKlantTelefoon + "</span></span>";        

        _html += "<span class='content-subheader'>Acknowledge</span>";
        _html += "<span class='content-line'><span class='content-label'>Naam:</span>";
        _html += "<span class='content-field'>" + obj.ContactAcknowledgeNaam + "</span></span>";
        _html += "<span class='content-line'><span class='content-label'>E-mail:</span>"; 
        _html += "<span class='content-field'><a href='mailto:" + obj.ContactAcknowledgeEmail + "' >" + obj.ContactAcknowledgeEmail + "</a></span></span>";               
        _html += "<span class='content-line'><span class='content-label'>Telefoon:</span>";                
        _html += "<span class='content-field'>" + obj.ContactAcknowledgeTelefoon + "</span></span>";
        _html += "<span class='content-line'><span class='content-label'>Beheer nummer:</span>";                
        _html += "<span class='content-field'>" + obj.ContactAcknowledgeProjectnummer + "</span></span>";
        _html += "<span class='content-line'>&nbsp;</span>";

        return _html;
    }

    function getWorkInstructionsHtml(CustomerName) {
        var _html = "",
            objWorkInstructionsGeneral = getWorkinstructionsCustomer(""),
            objWorkInstructionsCustomer = getWorkinstructionsCustomer(CustomerName);
        
        _html += "<span class='content-header secondary'>Werkinstructies</span>";


        if (objWorkInstructionsCustomer.length > 0 ) {
            _html += "<span class='content-subheader'>Klantspecifiek</span>";
            $.each(objWorkInstructionsCustomer, function (indexInArray, objWorkInstructionCustomer) { 
                _html += "<span class='content-line'><a href='" + objWorkInstructionCustomer.FileRef+ "?web=1' target='_blank'>" + objWorkInstructionCustomer.FileLeafRef + "</a></span>";                 
            });
        } 

        _html += "<span class='content-subheader'>Algemeen</span>";

        $.each(objWorkInstructionsGeneral, function (indexInArray, objWorkinstruction) { 
            _html += "<span class='content-line'><a href='" + objWorkinstruction.FileRef + "?web=1' target='_blank'>" + objWorkinstruction.FileLeafRef + "</a></span>";                                 
        });        
        
        return _html;
    }

    function getMessagesHtml() {
        var _html = "" ;

        _html += "<span class='content-line'><span class='content-button no-display'><a href='#'>Informeer klant</a></span></span>";
        _html += "<span class='content-line padding padding-top-half no-display'><a>DEMO Bericht verstuurd op 1 januari om 0:00 door Admin</a></span>";
        
        return _html;
    }

//REQUESTS

    function getItemsRequest(urlBase, listDisplayName, suffix){

        updateFormDigest(); //make sure connection is available

        var url = urlBase + "/_api/web/lists/getbytitle('" + listDisplayName + "')/items?$top=5000&$select=*" + suffix,
            request = $.ajax({
                url: url,
                type: "GET",            
                dataType: "json",
                headers: {
                    Accept: "application/json;odata=verbose"
                }
            });

        return request;
    }

    function getUpdateWorkItemRequest(objPlanning) {
        var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNamePlanning + "')/items(" + objPlanning.planningid + ")",
            itemPayload = {},
            itemType = getItemTypeForListName(listNamePlanning);  

        itemPayload.__metadata = { "type": itemType };
        itemPayload.uwWerklijstResultaat = objPlanning.result;
        itemPayload.uwWerklijstStatus = objPlanning.status;
        itemPayload.uwWerklijstOpmerking = objPlanning.comments;

        updateFormDigest(); //make sure connection is available

        var request = $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json;odata=verbose",
            data: JSON.stringify(itemPayload),
            headers: {
                "Accept": "application/json;odata=verbose",
                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                "X-HTTP-Method": "MERGE",
                "If-Match": "*"
            }   
        });

        return request;
    }

    function getFieldsRequest(urlBase, listDisplayName){
        updateFormDigest(); //make sure connection is available
        var url = urlBase + "/_api/web/lists/getbytitle('" + listDisplayName + "')/fields?$filter=(EntityPropertyName eq 'uwWerklijstResultaat') or (EntityPropertyName eq 'uwWerklijstStatus')",

            request = $.ajax({
            url: url,
            type: "GET",            
            dataType: "json",
            headers: {
                Accept: "application/json;odata=verbose"
            }
        });

        return request;
    }

    function getCurrentUserRequest() {
        updateFormDigest(); //make sure connection is available        
        var userid = _spPageContextInfo.userId;
        var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/getuserbyid(" + userid + ")";
        var requestHeaders = {"accept": "application/json;odata=verbose"};
    
        var request = $.ajax({
            url: url,
            contentType: "application/json;odata=verbose",
            headers: requestHeaders
        });
    
        return request;
    }

    function getUpdateSubscriptionRequest() {
        var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNameSubscriptions + "')/items(" + objSubscription.Id + ")",
            itemPayload = {},
            itemType = getItemTypeForListName(listNameSubscriptions);  

        itemPayload.__metadata = { "type": itemType };
        itemPayload.uwInschrijvingBestelling = objSubscription.Dinner;

        updateFormDigest(); //make sure connection is available

        var request = $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json;odata=verbose",
            data: JSON.stringify(itemPayload),
            headers: {
                "Accept": "application/json;odata=verbose",
                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                "X-HTTP-Method": "MERGE",
                "If-Match": "*"
            }   
        });

        return request;        

    }

    function getSubscriptionsRequest() {
        updateFormDigest();

        //get the subscription item for the current user
        var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNameSubscriptions + "')/items?$select=*,uwInschrijvingBegindatum/Id&$expand=uwInschrijvingBegindatum/Id&$filter=(uwInschrijvingBegindatum/Id eq " + objUpdateweek.Id + ")",
            request = $.ajax({
                url: url,
                type: "GET",            
                dataType: "json",
                headers: {
                    Accept: "application/json;odata=verbose"
                }
            });

        return request;
    }

//HELPER FUNCTIONS
    function updateFormDigest() {
        //make sure connection is available
        UpdateFormDigest(_spPageContextInfo.webServerRelativeUrl, _spFormDigestRefreshInterval);
    }

    function checkEmptyValue(value) {
       if (typeof value !== "string") {
           value = "";
       }

        return value;
    }

    function getSubstring(str, maxlength) {
        var returnstring = str;

        if ((str.length) > maxlength+3) {
            returnstring = str.substring(0, maxlength) + "...";
        }
        return returnstring;
    }

    function getItemTypeForListName(name) {
	    return "SP.Data." + name.charAt(0).toUpperCase() + name.slice(1) + "ListItem";
	}    

    function setDisplayDate(dateUTC) {
        var dateLocale = moment(dateUTC).locale("nl").format('LL');
        return dateLocale;
    }

    function getQueryStringValue (key) {  
        return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));  
      }  

})(jQuery);
$( document ).ready(function() {
    "use strict";
    // get the planning for this update week if available
     uwPlanning.getPlanning();
});

(function($) {

    "use strict";

    var listNameUpdateweek = "UpdateWeken",
        listNamePlanning = "Planning",
        listNameTaxonomy = "TaxonomyHiddenList",
        listNameInschrijvingen = "Inschrijvingen",
        listNameServers = "Servers",
        listNameKlanten = "Klanten",
        listNameEngineers = "Engineers",
        listNameRestaurants = "Restaurants",
        urlWerklijst = _spPageContextInfo.webAbsoluteUrl + "/Paginas/werklijst.aspx",
        sendFromAddress = getSenderAddress(); // get sendfromaddress based on dev, acc or prd

    var objCustomers = [],
        objServers = [],
        objTaxonomy = [],
        objWorkItems = [],
        objPlanningItems = [],
        objPlanningCustomers = [], //unique set of customers used for planning
        objSubscribedEngineers = [],
        objEngineers = [],
        objUpdateweek = {}, // getUpdateWeekInfo();
        objRestaurants = [],
        waitDialog = null;

    window.uwPlanning = function(){};

//PUBLIC FUNCTIONS
    uwPlanning.getPlanning = function() {

        $("#update-week-planning-processing").html("<div><img src='" + _spPageContextInfo.siteAbsoluteUrl + "/style%20library/images/ajax-loader.gif'/> De gegevens worden opgehaald...<br /></div>");
        setButtonsDisabled(true);

        var updateWeekId = getUrlParameter("ID"),
            updateWeekRequest = getUpdateWeekItemRequest(updateWeekId),
            planningRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNamePlanning, ",uwWerklijstUpdateweek/Id,uwWerklijstEngineer/Title,uwWerklijstEngineer/EMail&$expand=uwWerklijstUpdateweek/Id,uwWerklijstEngineer/Id&$filter=uwWerklijstUpdateweek/Id eq " + updateWeekId),
            subscriptionsRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNameInschrijvingen, ",uwInschrijvingBegindatum/Id,uwInschrijvingEngineer/Title,uwInschrijvingEngineer/EMail&$expand=uwInschrijvingBegindatum/Id,uwInschrijvingEngineer/Id&$filter=((uwInschrijvingBegindatum/ID eq " + updateWeekId +") and (uwInschrijvingAfgemeld eq 0))"),
            engineersRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNameEngineers, ",uwEngineer/Id,uwEngineer/Title,uwEngineer/EMail&$expand=uwEngineer/Id&$filter=uwEngineerActief eq 'Ja'");
            
        $.when(planningRequest, subscriptionsRequest, engineersRequest, updateWeekRequest)
            .done(function(a1, a2, a3, a4) {
                setUpdateWeekPlanningObjects(a1[0]);
                setSubscriptionsObjects(a2[0]);
                setEngineersObjects(a3[0]);
                setUpdateWeekObject(a4[0]);
            }).then(function(){
                setUpdateCustomers();
                setEngineersLevel();
            }).then(function(){
                renderUpdateweekPlanning();
            }).fail(function(error) {
                console.error(error);
                $("#update-week-planning").html("Oeps... Er is iets misgegaan bij het ophalen van de gegevens voor de update week.\n\n" + error.responseText);
            }).always(function(){
                setButtonsDisabled(false);
                $("#update-week-planning-processing").html("").hide();
            });
    };

    uwPlanning.createPlanning = function () {
        if (typeof objUpdateweek.planning === "string" && objUpdateweek.planning.toLowerCase() === "ja") {
            alert("De planning voor '" + objUpdateweek.title  +"' is al eerder aangemaakt op: " + setDisplayDate(objUpdateweek.datePlanning, "LLL"));
            return;
        }

        showWaitDialog(true); 

        var customersRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNameKlanten, ",uwKlantUpdatedag/Title,uwKlantUpdatedag/uwVolgorde&$filter=uwKlantActief eq 1&$expand=uwKlantUpdatedag"),
            serversRequest = getItemsRequest(_spPageContextInfo.webAbsoluteUrl, listNameServers, "&$filter=uwServerActief eq 1"),
            taxonomyRequest = getItemsRequest(_spPageContextInfo.siteAbsoluteUrl,listNameTaxonomy, "");            

        $.when(customersRequest, serversRequest, taxonomyRequest)
            .done(function (a1, a2, a3){
                // process the data of the requests
                setCustomersObjects(a1[0]);
                setServersObjects(a2[0]);
                setTaxonomyObjects(a3[0]);
            }).then(function(){
                setCustomersLabel();
            }).then(function(){
                setWorkItemsObjects();
            }).then(function() {
                var planningItemRequests = createListItems();

                $.when.apply($, planningItemRequests).done(function() {
                    setUpdateWeekInfo();              
                }).then(function(){
                   var updateItemRequest = getUpdateUpdateWeekItemRequest();
                   updateItemRequest
                        .done(function(){
                            closeWaitDialog();
                            window.location.reload();
                        }).fail(function(ex) {
                            alert("Er is iets misgegaan tijdens het bijwerken van de Update Week-record. \n\n" + ex.responseText);
                            console.error(ex);
                            closeWaitDialog();
                        });
                }).fail(function(ex) {
                    alert("Er is een fout opgetreden bij het aanmaken van de planning:\n\n" + ex.responseText);
                    console.error(ex);
                    closeWaitDialog();
                });  

            }).fail(function(ex){
                alert("Oeps.... something went wrong! \n \nYour computer will explode in 5 seconds... \n \nRUN!!!! \n\n\n" + ex.responseText);
                console.error(ex);
            });
    };

    uwPlanning.sendEmailPlanning = function() {
        var emailProperties = {},
            emailRequest = {};
        
        if (checkSendEmail() === true) {
            showWaitDialog();  
            emailProperties = getWorklistMailProperties();
            emailRequest = getEmailRequest(emailProperties);

            emailRequest.done(function () {
                objUpdateweek.planningSent = "Ja";
                objUpdateweek.datePlanningSent = new Date();
                updateUpdateWeekItem("E-mail met onderwerp '" + emailProperties.Subject + "' is verstuurd.");					
            }).then(function(){
                window.location.reload();
            }).fail(function (err) {
                alert('Er is iets mis gegaan met het versturen van de e-mail met onderwerp "' + emailProperties.Subject + '" \n \n' + JSON.stringify(err) );
                console.error(JSON.stringify(err));
            }).always(function() {
                closeWaitDialog();
            });
        }
    };

    uwPlanning.sendEmailPlanningReminder = function() {

        //choose days to be sent
        var arrPlannedDays = getPlannedDaysUpdateWeek();
        var checkBoxHtml = "";
        var dialogTemplate = document.getElementById("planning-modal-dialog-container-template"),
        dialogClone = dialogTemplate.cloneNode(true);
        
        $(dialogClone).attr('id', 'planning-modal-dialog-container');

        var options = {
            title: "Versturen herinnerings e-mail",
            width: 500,
            height: 300,
            autoSize: true,
            showClose: true,
            html: dialogClone,
            dialogReturnValueCallback: onModaldialogClose
        };

        SP.UI.ModalDialog.showModalDialog(options);

        $.each(arrPlannedDays, function (indexInArray, day) { 
             checkBoxHtml += "<label><input type='checkbox' name='chk_days' value='" + day + "' />" + day + "</label><br />";
        });

        $("#planning-modal-dialog-container #planning-modal-dialog-planned-days").html(checkBoxHtml);

        if (objUpdateweek.actief.toLowerCase() !== "ja") {
            $("#planning-modal-dialog-container #messages").html("<mark>Let op: deze update week is niet actief!</mark>");
        }

    };

    uwPlanning.sendEmailDinner = function() {
        var emailProperties = {},
        emailRequest = {}; 

        if (objUpdateweek.restaurantName === "") {
            alert("Kies eerst een restaurant!");
        }

        showWaitDialog();

        emailProperties = getDinnerMailProperties();
        emailRequest = getEmailRequest(emailProperties);

        emailRequest.done(function(){
            closeWaitDialog();
        });

        
    };

    uwPlanning.processModalDialog = function() {

        //get the selected days
        var engineersDay = [],
            plannedEngineers = [],
            selectedDays = $("input[name='chk_days']:checked").map(function() {
                return this.value;
            }).get(),
            messageHtml = "",
            emailProperties = {},
            emailRequest = {};

        if (selectedDays.length === 0) {
            $("#planning-modal-dialog-container #messages").html("<mark>Minimaal één dag selecteren voor het versturen van de herinnerings e-mail!</mark>");
            return;
        } 

        $("#planning-modal-dialog-container .sendMail").hide();
        $("#planning-modal-dialog-container #planning-modal-dialog-planned-days input").prop('disabled', true);
        $("#planning-modal-dialog-container #messages").html("<img src='" + _spPageContextInfo.siteAbsoluteUrl + "/style%20library/images/ajax-loader.gif'/> Wordt verwerkt...");

        //get the planned engineers for the selected days
        $.each(selectedDays, function (indexInArray, day) { 
            engineersDay = getPlannedEngineersByDay(day);
            for (var i=0; i < engineersDay.length; i++) {
                if (plannedEngineers.indexOf(engineersDay[i]) === -1 ) {
                    plannedEngineers.push(engineersDay[i] );
                }
            }     
        });

        //send the e-mail to the planned engineers. (One mail stating the selected days monday and/or tuesday and/or wednesday)
        emailProperties =  getWorklistReminderMailProperties(selectedDays, plannedEngineers);
        emailRequest = getEmailRequest(emailProperties);

        emailRequest
            .done(function(){
                //write to screen the engineers
                messageHtml += "<span>Reminder e-mail is verstuurd aan:</span>";
                messageHtml += "<ul>";

                for (var x = 0; x < plannedEngineers.length; x++) {
                    messageHtml += "<li>" + plannedEngineers[x] + "</li>";
                }

                messageHtml += "</ul>";  

                $("#planning-modal-dialog-container #messages").html(messageHtml);
                
            })
            .fail(function(err){
                
               $("#planning-modal-dialog-container #messages").html('Er is iets mis gegaan met het versturen van de e-mail met onderwerp "' + emailProperties.Subject + '" <br /> <br />' + JSON.stringify(err) );
                console.error(JSON.stringify(err));
            });

      
        // $("#planning-modal-dialog-container #messages").append(messageHtml);



        // SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, "");
    };

    uwPlanning.closeModalDialog = function() {
        SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.cancel, null);
    };    

    uwPlanning.hideForm = function() {
        $("#update-week-form").hide();
        $("#update-week-form-nav #update-week-form-expand").show();
        $("#update-week-form-nav #update-week-form-collapse").hide();                    
    };

    uwPlanning.showForm = function() {
        $("#update-week-form").show();
        $("#update-week-form-nav #update-week-form-expand").hide();
        $("#update-week-form-nav #update-week-form-collapse").show();            
    };    

    // uwPlanning.setEngineerCustomers = function() {
    //     var customer = "",
    //         environment = "",
    //         engineerId = "";

    //     $( "#update-week-planning div[id^='planning-customer-']" ).each(function() {
    //         customer = $(this).children(".customer-addinfo").text();
    //         environment = $(this).children(".environment").text();
    //         engineerId = $(this).find(".subscribed-engineers").val();
    //         setEngineerPlanning(customer, environment, engineerId, this.id);
    //     });
    // };

    uwPlanning.setEngineerCustomer = function(planningId) {

        var selector = "planning-customer-" + planningId,
            customer = $("#" + selector + " .customer-addinfo").text(),
            environment = $("#" + selector + " .environment").text(),
            engineerId = $("#" + selector + " .subscribed-engineers").val();

        setEngineerPlanning(customer, environment, engineerId, selector);

        //update the object
        updateObjPlanningCustomer(planningId, engineerId);
    };

//OBJECTS
    function setCustomersObjects(data) {
        var _items = data.d.results;

        $.each(_items, function (indexInArray, _item) { 
            setCustomerObject(_item);
        });
    }

    function setCustomerObject(props) {
        var obj = {};

        obj.ID = props.ID;
        obj.Active = props.uwKlantActief;
        obj.Comment = props.uwKlantOpmerking;
        obj.Contact = props.uwKlantContactpersoonId;
        obj.ContactAcknowledge = props.uwKlantContactpersoon_ackId;
        obj.Customer = props.uwKlantnaam;    
        obj.Environment = props.uwKlantomgeving;
        obj.AdditionalInfo = props.uwKlantExtraInfo;
        obj.Level = props.uwKlantlevel;
        obj.UpdateDay = props.uwKlantUpdatedag.Title;
        obj.UpdateDayOrder = props.uwKlantUpdatedag.uwVolgorde;
        obj.UpdateMethod = props.uwUpdateMethode;
        obj.UpdateWindow = props.uwKlantUpdatewindow;
        obj.Projectnummer = props.uwKlantProjectnummer;
        obj.Werkinstructies = props.uwKlantWerkinstructies;

        objCustomers.push(obj);
    }

    function setServersObjects(data) {
        var _items = data.d.results;
        $.each(_items, function (indexInArray, _item) { 
            setServerObject(_item);        
        });
    }

    function setServerObject(props) {
        var obj = {};

        obj.ID = props.ID;
        obj.Active = props.uwServerActief;
        obj.IloDrac = props.uwServerIloDrac;
        obj.Customer = props.uwServerKlant;
        obj.Server = props.uwServerNaam;
        obj.Environment = props.uwServerOmgeving;
        obj.AdditionalInfo = props.uwServerExtraInfo;
        obj.Comment = props.uwServerOpmerking;
        obj.Role = props.uwServerRol;
        obj.UpdateScript = props.uwServerUpdateScript;
        obj.UpdateWindow = props.uwServerUpdateWindow;
        obj.UpdateMethod = props.uwUpdateMethode;
        obj.ServerInfo = props.uwServerOpmerking;

        objServers.push(obj);
    }

    function setUpdateWeekPlanningObjects(data) {
        var _items = data.d.results;
        $.each(_items, function (indexInArray, _item) { 
            setUpdateWeekPlanningObject(_item);        
        });       
    }

    function setUpdateWeekPlanningObject(props) {
        var obj = {};

        obj.EngineerName = props.uwWerklijstEngineer.Title;
        obj.EngineerEmail = props.uwWerklijstEngineer.EMail;
        obj.EngineerId = props.uwWerklijstEngineerId;
        obj.Customer = props.uwWerklijstKlant;
        obj.Environment = props.uwWerklijstOmgeving;
        obj.CustomerEnvironment = props.uwWerklijstKlantOmgeving;
        obj.CustomerAdditionalInfoEnvironment = props.uwWerklijstKlantExtraInfo + " ~ " + props.uwWerklijstOmgeving ;        
        obj.CustomerAdditionalInfo = props.uwWerklijstKlantExtraInfo;
        obj.Level = props.uwWerklijstNiveau;
        obj.UpdateDay = props.uwWerklijstUpdateDag;
        obj.UpdateDayOrder = props.uwWerklijstUpdateDagVolgorde;
        obj.UpdateWindow = props.uwWerklijstKlantUpdateWindow;
        obj.PlanningId = props.ID;

        objPlanningItems.push(obj);
    }

    function setSubscriptionsObjects(data) {
        var _items = data.d.results;

        $.each(_items, function (indexInArray, _item) { 
            setSubscriptionsObject(_item);        
        });
    }

    function setSubscriptionsObject(props) {
        var obj = {};

        obj.EngineerName = props.uwInschrijvingEngineer.Title;
        obj.EngineerId = props.uwInschrijvingEngineerId;
        obj.EngineerEmail = props.uwInschrijvingEngineer.EMail;
        obj.Monday = props.uwInschrijvingMaandag;
        obj.Tuesday = props.uwInschrijvingDinsdag;
        obj.Wednesday = props.uwInschrijvingWoensdag;
        obj.Thursday = props.uwInschrijvingDonderdag;
        obj.Saturday = props.uwInschrijvingZaterdag;
        obj.Mmc = props.uwInschrijvingMMC;
        obj.Comments = props.uwInschrijvingOpmerkingen;

        objSubscribedEngineers.push(obj);
    }

    function setEngineersObjects(data) {
        var _items = data.d.results;

        $.each(_items, function (indexInArray, _item) { 
            setEngineersObject(_item);        
        });
    }

    function setEngineersObject(props) {
        var obj = {};

        obj.EngineerID = props.uwEngineerId;
        obj.Level = props.uwEngineerNiveau;
        obj.Email = props.uwEngineer.EMail;
        obj.Name = props.uwEngineer.Title;

        objEngineers.push(obj);
    }

    function setUpdateWeekObject(data) {

        var _item = data.d;

        objUpdateweek.ID = _item.ID;
        objUpdateweek.startDate = _item.uwBegindatum;
        objUpdateweek.title = _item.Title;
        objUpdateweek.planning = _item.uwPlanningGemaakt;
        objUpdateweek.datePlanning = _item.uwDatumPlanningGemaakt;
        objUpdateweek.planningSent = _item.uwPlanningVerstuurd;
        objUpdateweek.datePlanningSent = _item.uwDatumPlanningVerstuurd;
        objUpdateweek.actief = _item.uwActief;
        objUpdateweek.restaurantName = checkEmptyValue(_item.uwRestaurant.Title);
        objUpdateweek.restaurantWebsite = checkEmptyValue(_item.uwRestaurant.uwRestaurantUrl);
        
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
    function setCustomersLabel() {
        $.each(objCustomers, function (indexInArray, objCustomer) { 
             var obj = Enumerable
                            .from(objTaxonomy)
                            .where(function(x) {return x.TermGuid === objCustomer.Customer.TermGuid;})
                            .toArray();

            objCustomer.CustomerName = obj[0].Title;
        });
    }

    function getServersByCustomerEnvironment(objCustomer) {
        var servers = Enumerable
                        .from(objServers)
                        .where(function(x){return x.Customer.TermGuid === objCustomer.Customer.TermGuid;})
                        .where(function(x){return x.Environment === objCustomer.Environment;})
                        .where(function(x){return x.AdditionalInfo === objCustomer.AdditionalInfo;})
                        .toArray();

        return servers;
    } 

    function getPlanningByCustomerEnvironment(customer, environment) {
        var planningItems = Enumerable
                            .from(objPlanningItems)
                            .where(function(x) {return x.CustomerAdditionalInfo  === customer;})
                            .where(function(x) {return x.Environment  === environment;})
                            .toArray();

        return planningItems;
    }

    function setUpdateCustomers() {
        objPlanningCustomers = Enumerable
                                    .from(objPlanningItems)
                                    .distinct(function(x){return x.CustomerAdditionalInfoEnvironment;})
                                    .orderBy(function(x) {return x.UpdateDayOrder;})
                                    .thenBy(function(x){return x.CustomerEnvironment;})
                                    .toArray();
    }    

    function getSubscribedEngineersDay(updateDay) {
        var subscribersday = getPropertyToCheck(updateDay),
            engineers = Enumerable
                            .from(objSubscribedEngineers)
                            .where("$." + subscribersday + " === true")
                            .orderBy(function(x) {return x.EngineerName;})
                            .toArray();

        return engineers;
    }

    function getEmailSubscribedEngineersDay(updateDay) {
        var _subscribersday = getSubscribedEngineersDay(updateDay),
            emailAddresses = Enumerable
                                .from(_subscribersday)
                                .where(function(x) {return x.EngineerEmail !== null ;})                
                                .select(function(x) {return x.EngineerEmail;})              
                                .toArray();

        return emailAddresses;

    }

    function getEngineerLevel(engineerID) {

        var engineer = Enumerable
                            .from(objEngineers)
                            .where(function(x) {return x.EngineerID === engineerID;})
                            .toArray();

        if (typeof engineer[0] === "undefined") {
            return "0";
        } else {
            return engineer[0].Level;
        }
    }

    function getSubscribedEngineersEmail() {
        var email = Enumerable
                        .from(objSubscribedEngineers)
                        .where(function(x) {return x.EngineerEmail !== null ;})
                        .select(function(x) {return x.EngineerEmail ;})
                        .toArray();

        return email;
    }

    function getEngineerById(engineerId) {
        var obj = Enumerable
                    .from(objEngineers)
                    .where(function(x) {return x.EngineerID === parseInt(engineerId);})
                    .toArray();

        return obj[0];
    }

    function updateObjPlanningCustomer(planningId,engineerId) {
        var objEngineer = getEngineerById(engineerId);
        
        var obj = Enumerable
                    .from(objPlanningCustomers)
                    .where(function(x) {return x.PlanningId === parseInt(planningId);})
                    .toArray(),
            objPlanningCustomerUpdate = {};

        if (obj.length > 0) {
            objPlanningCustomerUpdate = obj[0];
            objPlanningCustomerUpdate.EngineerId = parseInt(engineerId);
            if (typeof objEngineer === "object") {
                objPlanningCustomerUpdate.EngineerName = objEngineer.Name;
                objPlanningCustomerUpdate.EngineerEmail = objEngineer.Email;
            } else {
                objPlanningCustomerUpdate.EngineerName = null;
                objPlanningCustomerUpdate.EngineerEmail = null;
            }
        }
    } 

    function getPlannedDaysUpdateWeek() {
        var obj = Enumerable
                        .from(objPlanningCustomers)
                        .where(function(x) {return x.EngineerId > 0;})
                        .distinct(function(x) {return x.UpdateDay ;})
                        .select(function(x) {return x.UpdateDay;})
                        .orderBy(function(x) {return x.UpdateDayOrder;})
                        .toArray();

        return obj;
    }

    function getPlannedEngineersByDay(day) {
    
        var obj = Enumerable
                        .from(objPlanningCustomers)
                        .where(function(x) {return x.EngineerEmail !== null ;})                          
                        .where(function(x) {return x.UpdateDay === day ;})
                        .where(function(x) {return x.EngineerId > 0;})
                        .distinct(function(x) {return x.EngineerEmail ;})                                               
                        .select(function(x) {return x.EngineerEmail ;})
                        .toArray();

        return obj;

    }

//METHODS
    function updateUpdateWeekItem(message) {
        var updateUpdateWeekRequest = getUpdateUpdateWeekItemRequest();
        updateUpdateWeekRequest
            .done(function() {
                if(typeof message === "string") {
                    alert(message);
                }
            })
            .fail(function(error) {
                alert("Er is iets misgegaan tijdens het bijwerken van de Update Week-record. \n\n" + error.responseText);
            });
    }

    function renderUpdateweekPlanning() {

        var resultHtml = "";
   
        if (objPlanningCustomers.length === 0) {
            $("#update-week-form-nav").html("");
            $("#sendplanning").hide();
            $("#sendplanningreminder").hide();
            $("#senddinnermail").hide();
            return;
        } else {
            $("#update-week-planning").show();
        }

        resultHtml +=getPlanningHeaders();

        $.each(objPlanningCustomers, function (indexInArray, objPlanningCustomer) { 
            var subscribedEngineersHtml = getHtmlgetSubscribedEngineersDay(objPlanningCustomer);

             resultHtml += "<div id='planning-customer-" + objPlanningCustomer.PlanningId + "' class='update-week-planning-customer'>";
             resultHtml += "<span class='customer-addinfo'>" + objPlanningCustomer.CustomerAdditionalInfo + "</span>";
             resultHtml += "<span class='environment'>" + objPlanningCustomer.Environment + "</span>"; 
             resultHtml += "<span class='update-day'>" + objPlanningCustomer.UpdateDay + "</span>";
             resultHtml += "<span class='update-window'>" + objPlanningCustomer.UpdateWindow + "</span>";
             resultHtml += "<span class='level'>" + objPlanningCustomer.Level + "</span>";             
             resultHtml += "<span class='engineer'>" + subscribedEngineersHtml + "</span>";     

             resultHtml += "<span class='engineer-comments'></span>";
             resultHtml += "</div>";
        });

        resultHtml += getModalDialogHtml();

        $("#update-week-planning").html(resultHtml);
    }

    function getModalDialogHtml() {
        var returnHtml = "";

        returnHtml += "<div id='planning-modal-dialog-container-template'>";

        returnHtml += "<div id='planning-modal-dialog-planned-days'></div>";
        returnHtml += "<div id='planning-modal-dialog-buttons'>";    
        returnHtml += "<a href='javascript:uwPlanning.processModalDialog()' class='sendMail'>E-mails verzenden</a>";        
        returnHtml += "<a href='javascript:uwPlanning.closeModalDialog()' class='closeDialof'>Sluiten</a>";                        
        returnHtml += "</div>";  //buttons      

        returnHtml += "<div id='messages'>";
        returnHtml += "</div>"; //messages           

        returnHtml += "</div>"; //container

        return returnHtml;
    }

    function getHtmlgetSubscribedEngineersDay(objPlanningCustomer) {
        var subscribedEngineers = getSubscribedEngineersDay(objPlanningCustomer.UpdateDay),
            plannedEngineerId = objPlanningCustomer.EngineerId,
            commentsIndicator = "",
            commentsAttribute = "",
            selected = "",
            _html = ""; 

        _html += "<select class='subscribed-engineers' onChange=uwPlanning.setEngineerCustomer('" + objPlanningCustomer.PlanningId + "')>";
        _html += "<option value='0'>(Selecteren)</option>";

        $.each(subscribedEngineers, function (indexInArray, subscribedEngineer) { 
            commentsIndicator = "";
            commentsAttribute = "";
            selected = "";

            if (typeof subscribedEngineer.Comments === "string" && subscribedEngineer.Comments !== "")
            {
                commentsIndicator = "*";
                commentsAttribute = "title='" + subscribedEngineer.Comments + "'";
            }
           
            if (plannedEngineerId === subscribedEngineer.EngineerId) {
                selected = "selected='selected'";
            }

             _html += "<option class='subscribed-engineers-option' " + selected + " value=" + subscribedEngineer.EngineerId + " " + commentsAttribute + ">" + subscribedEngineer.EngineerName + " (" + subscribedEngineer.Level + ")" + commentsIndicator + "</option>";
        });

        _html += "</select>";

        return _html;
    }

    function setEngineerPlanning(customer, environment, engineerId, planningSelector) {
        var planningItems = getPlanningByCustomerEnvironment(customer, environment),
            updateRequest = null;
         
        $.each(planningItems, function (indexInArray, planningItem) { 
             //update the server with the engineer
             updateRequest = getUpdatePlanningRequest(planningItem.PlanningId, engineerId);
             updateRequest.done(function() {
                    // $("#" + planningSelector + " .engineer-comments").text("Planning is bijgewerkt");
             }).fail(function(error){
                //set message update was not succesful with error message
                    $("#" + planningSelector + " .engineer-comments").text("Er is iets misgegaan! Check de console");
                    console.error("Error updating item:");
                    console.error(error);
             });
        });
    }

    function setEngineersLevel() {
        var level = "";

        $.each(objSubscribedEngineers, function (indexInArray, objSubscribedEngineer) { 
             level = getEngineerLevel(objSubscribedEngineer.EngineerId);
             objSubscribedEngineer.Level = level;
        });
    }

    function getPlanningHeaders() {
        var _html = "";
        _html += "<div id='update-week-planning-customer-headers' class='update-week-planning-customer'>";
        _html += "<span class='customer-addinfo'>Klant</span>";  
        _html += "<span class='environment'>Omgeving</span>";          
        _html += "<span class='update-day'>Update dag</span>";
        _html += "<span class='update-window'>Update window</span>";         
        _html += "<span class='level'>Niveau</span>";        
        _html += "<span class='engineer'>Engineer</span>";
        _html += "</div>";

        return _html;
    }

    function createListItems() {
        var itemType = getItemTypeForListName(listNamePlanning);
        var itemID = objUpdateweek.ID;
        var requests = [];

        $.each(objWorkItems, function (indexInArray, objWorkItem) { 
            requests.push(createPlanningItemRequest(objWorkItem, itemType, itemID));
        });

        return requests;

    }

    function setWorkItemsObjects() {
        var objCustomerServers = [];

        $.each(objCustomers, function (indexInArray, objCustomer) { 
            //get all servers from this customer
            objCustomerServers = getServersByCustomerEnvironment(objCustomer);
            setWorkItemsCustomer(objCustomer, objCustomerServers);
        });
    }

    function setWorkItemsCustomer(objCustomer, objCustomerServers) {
        var objWorkItem = {};

        $.each(objCustomerServers, function (indexInArray, objCustomerServer) { 
            objWorkItem = {};
            objWorkItem.Customer = objCustomer.Customer;
            objWorkItem.CustomerName = objCustomer.CustomerName;
            objWorkItem.CustomerEnvironment = objCustomer.CustomerName + " (" + objCustomer.Environment + ")";
            objWorkItem.CustomerAdditionalInfo = setCustomerWithAdditionalInfo(objCustomer);
            objWorkItem.UpdateWindowCustomer = objCustomer.UpdateWindow;
            objWorkItem.UpdateWindowServer = objCustomerServer.UpdateWindow;
            objWorkItem.Level = objCustomer.Level;
            objWorkItem.Environment = objCustomer.Environment;
            objWorkItem.UpdateMethod = getUpdateMethod(objCustomer, objCustomerServer);
            objWorkItem.UpdateWeekTitle = objUpdateweek.title;
            objWorkItem.UpdateWeekStartDate = objUpdateweek.startDate;
            objWorkItem.Day = objCustomer.UpdateDay;
            objWorkItem.DayOrder = objCustomer.UpdateDayOrder;
            objWorkItem.Server = objCustomerServer.ID;
            objWorkItem.ServerInfo = objCustomerServer.ServerInfo;
            objWorkItem.ContactCustomer = objCustomer.Contact;
            objWorkItem.ContactAcknowledge = objCustomer.ContactAcknowledge;
            objWorkItem.Projectnummer = objCustomer.Projectnummer;
            objWorkItem.Werkinstructies = objCustomer.Werkinstructies;

            objWorkItems.push(objWorkItem);
        });
    }

    function getUpdateMethod(objCustomer, objCustomerServer) {
        var method = objCustomer.UpdateMethod;
              
        if (typeof objCustomerServer.UpdateMethod === "string" && objCustomerServer.UpdateMethod !== "" && objCustomerServer.UpdateMethod.charCodeAt() !== 129 ) {
            method = objCustomerServer.UpdateMethod;
        }

        return method;
    }

    function setCustomerWithAdditionalInfo(objCustomer) {
        var returnValue = objCustomer.CustomerName;

        if (typeof objCustomer.AdditionalInfo === "string") {
            returnValue += " (" + objCustomer.AdditionalInfo + ")";
        }

        return returnValue;
    }

    function setUpdateWeekInfo() {
        objUpdateweek.planning = "Ja";
        objUpdateweek.datePlanning = new Date();
    }

    function setButtonsDisabled(disabled) {
        $(".ms-toolbar input").attr("disabled", disabled);
    }

    function showWaitDialog() {
        if (waitDialog === null) {
         waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose(SP.Res.dialogLoading15);
      }
    }

    function closeWaitDialog() {
        if (waitDialog !== null) {
            waitDialog.close();
            waitDialog = null;
        }

    }

    function onModaldialogClose(result, returnValue) {
        if (result === SP.UI.DialogResult.OK) {
                
        }
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

    function getUpdateWeekItemRequest(itemID) {
        updateFormDigest(); //make sure connection is available        

        
        var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNameUpdateweek + "')/items(" + itemID + ")?$select=*,uwRestaurant/Id,uwRestaurant/Title,uwRestaurant/uwRestaurantUrl&$expand=uwRestaurant/Id",

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

    function createPlanningItemRequest(objWorkItem, itemType, itemID) {
        updateFormDigest(); //make sure connection is available
        var itemPayload = {};

        itemPayload.__metadata =  { 'type': itemType };
        itemPayload.Title = "Planning";
        itemPayload.uwWerklijstKlant = objWorkItem.Customer;
        itemPayload.uwWerklijstOmgeving = objWorkItem.Environment;
        itemPayload.uwWerklijstUpdateweekId = itemID;
        itemPayload.uwWerklijstKlantUpdateWindow = objWorkItem.UpdateWindowCustomer;
        itemPayload.uwWerklijstServerUpdateWindow = objWorkItem.UpdateWindowServer;
        itemPayload.uwWerklijstNiveau = objWorkItem.Level;
        itemPayload.uwWerklijstUpdateMethode = objWorkItem.UpdateMethod;
        itemPayload.uwWerklijstUpdateDag = objWorkItem.Day;
        itemPayload.uwWerklijstUpdateDagVolgorde = objWorkItem.DayOrder;
        itemPayload.uwWerklijstServerId = objWorkItem.Server;
        itemPayload.uwWerklijstKlantOmgeving = objWorkItem.CustomerEnvironment;
        itemPayload.uwWerklijstKlantExtraInfo = objWorkItem.CustomerAdditionalInfo;
        itemPayload.uwWerklijstServerOpmerking = objWorkItem.ServerInfo;
        itemPayload.uwWerklijstContactpersoonKlantId = objWorkItem.ContactCustomer;
        itemPayload.uwWerklijstContactpersoonAcknowlId = objWorkItem.ContactAcknowledge;
        itemPayload.uwWerklijstProjectnummer = objWorkItem.Projectnummer;
        itemPayload.uwWerklijstWerkinstructies = objWorkItem.Werkinstructies;

        var request = $.ajax({
            url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNamePlanning + "')/items",
            type: "POST",
            contentType: "application/json;odata=verbose",
            data: JSON.stringify(itemPayload),
            headers: {
                "Accept": "application/json;odata=verbose",
                "X-RequestDigest": $("#__REQUESTDIGEST").val()
            }
        });

        return request;
 
    }

    function getUpdatePlanningRequest(planningItemId, engineerId) {
        updateFormDigest(); //make sure connection is available
        //update the planning with the engineer
        
        var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNamePlanning + "')/items(" + planningItemId + ")",
            itemPayload = {},
            itemType = getItemTypeForListName(listNamePlanning);  

        itemPayload.__metadata = { "type": itemType };
        itemPayload.uwWerklijstEngineerId = engineerId;

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

    function getUpdateUpdateWeekItemRequest() {
        updateFormDigest(); //make sure connection is available        
        var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNameUpdateweek + "')/items(" + objUpdateweek.ID + ")",
            itemPayload = {},
            itemType = getItemTypeForListName(listNameUpdateweek);
        
        itemPayload.__metadata = { "type": itemType };

        itemPayload.uwPlanningGemaakt = objUpdateweek.planning;
        if (typeof objUpdateweek.planning === "string" && objUpdateweek.planning.toLowerCase() === "ja" ) {
            itemPayload.uwDatumPlanningGemaakt = objUpdateweek.datePlanning;
        }

        itemPayload.uwPlanningVerstuurd = objUpdateweek.planningSent;
        if (typeof objUpdateweek.planningSent === "string" && objUpdateweek.planningSent.toLowerCase() === "ja") {
            itemPayload.uwDatumPlanningVerstuurd = objUpdateweek.datePlanningSent;
        }

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

    function getEmailRequest(emailProperties) {
        updateFormDigest(); //make sure connection is available		
 
		var mail = {
	        properties: {
	            __metadata: { 'type': 'SP.Utilities.EmailProperties' },
	            'From': sendFromAddress,
	            'To': { 'results': emailProperties.To },
	            'Body': emailProperties.Body,
	            'Subject': emailProperties.Subject
	        }
	    },
    		
		    getAppWebUrl = _spPageContextInfo.webServerRelativeUrl,
		    urlTemplate = getAppWebUrl + "/_api/SP.Utilities.Utility.SendEmail",
		    request = $.ajax({
		            contentType: 'application/json',
		            url: urlTemplate,
		            type: "POST",
		            data: JSON.stringify(mail),
		            headers: {
		                "Accept": "application/json;odata=verbose",
		                "content-type": "application/json;odata=verbose",
		                "X-RequestDigest": $("#__REQUESTDIGEST").val()
		            }
                });	
                                        
		return request;
		        		        
    }

//HELPER FUNCTIONS
	function getWorklistMailProperties() {

		var title = objUpdateweek.title,
			begindatum = objUpdateweek.startDate,
			emailProperties = [],

			subject = 'Werklijst ' + title,	
			body = 'Beste collega,<br /> <br/>';
			body += 'De planning voor <b>' + title + ' (begindatum ' + setDisplayDate(begindatum, 'LL') + ') </b> is beschikbaar.<br /><br />';
			body += 'Op <a href="' + urlWerklijst  + '">jouw werklijst</a> kun je zien voor welke werkzaamheden je ingepland bent.<br /><br />';
            body += 'We willen je vragen toekomstige lijst mutaties in te schieten in Priox als SRQ. Eventuele problemen met de update avond die niet via de Troubleshooting Guide te verhelpen zijn graag inboeken in Priox als INC.<br /><br />';
            body += '<b>TIP:</b><br/>';
            body += 'Een pre-check overdag, voorkomt verrassingen in de avond. Check je klanten even of alles er goed bijstaat zodat je ’s avonds niet verrast wordt.<br /><br />';
			body += 'Met vriendeijke groet,<br /><br />';
			body += 'Patch Management';

		emailProperties.To = getSubscribedEngineersEmail();
		emailProperties.Subject = subject;
        emailProperties.Body = body;

	
		return emailProperties;
    }
    
    function getWorklistReminderMailProperties(arrPlannedDays, arrPlannedEngineers) {
		var title = objUpdateweek.title,
			begindatum = objUpdateweek.startDate,
            emailProperties = {},
            subject = 'Herinnering: werklijst ' + title + "",
            plannedDays = arrPlannedDays.join(" of "),
            body = '';

        body += 'Beste collega, <br /><br />';

        body += '<a href="' + urlWerklijst  + '">Jouw werklijst</a> voor <b>' + arrPlannedDays.join("</b> en/of <b>") + '</b> voor ' + title + ' is beschikbaar.<br /><br />';
        // body += 'Op <a href="' + urlWerklijst  + '">jouw werklijst</a> kun je zien voor welke werkzaamheden je ingepland bent.<br /><br />';
        body += 'Door middel van deze mail willen je eraan herinneren dat je bent ingepland.<br /><br />';
        body += 'We willen je vragen toekomstige lijst mutaties in te schieten in Priox als SRQ. Eventuele problemen met de update avond die niet via de Troubleshooting Guide te verhelpen zijn graag inboeken in Priox als INC.<br /><br />';
        body += '<b>TIP:</b><br/>';
        body += 'Een pre-check overdag, voorkomt verrassingen in de avond. Check je klanten even of alles er goed bijstaat zodat je ’s avonds niet verrast wordt.<br /><br />';
        body += 'Met vriendeijke groet,<br /><br />';
        body += 'Patch Management';        
        
        emailProperties.To = arrPlannedEngineers;
        emailProperties.Subject = subject;
        emailProperties.Body = body;

        return emailProperties;

    }

function getDinnerMailProperties() {
		var title = objUpdateweek.title,
			begindatum = objUpdateweek.startDate,
            emailProperties = {},
            subject = 'Eetmail ' + title + "",
            body = '';

        body += 'Beste collega, <br /><br />';

        body += 'Deze keer is de keuze gevallen op <a href="' + objUpdateweek.restaurantWebsite + '" target="_blank">'+ objUpdateweek.restaurantName + '</a>.<br />';
        body += 'Geef via het <a href="' + urlWerklijst + '?action=dinner">Bestelformulier</a> voor donderdag 12:00 door wat jij wil eten zodat dit tijdig besteld kan worden.<br /><br />';
        body += 'Zoals iedere maand is er een budget van 14 Euro per persoon.<br />';
        body += '<b>Omstreeks 17:15 zal het eten geleverd worden.</b><br /><br />';
        
        body += 'Met vriendelijke groet,<br />';
        body += 'Patch Management';        
        
        emailProperties.To = getPlannedEngineersByDay("Donderdag");
        emailProperties.Subject = subject;
        emailProperties.Body = body;

        return emailProperties;
}

    function checkSendEmail() {
		// check Invitations already sent. if so ask confirmation to re-send
		var returnValue = true;

		// get confirmation that e-mail has to (re)send
		if (typeof objUpdateweek.planningSent === "string" && objUpdateweek.planningSent.toLowerCase() === "ja") {
			var message = "De planning voor '" + objUpdateweek.title + "' is al eerder verstuurd op " +  setDisplayDate(objUpdateweek.datePlanningSent, "LLL") +". Wilt u deze nogmaals uitsturen?";
			returnValue = confirm(message);
		}

		return returnValue;
	}

    function getItemTypeForListName(name) {
	    return "SP.Data." + name.charAt(0).toUpperCase() + name.slice(1) + "ListItem";
	}

    function getUrlParameter(sParam) {
	    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
	        sURLVariables = sPageURL.split('&'),
	        sParameterName,
	        i;
	
	    for (i = 0; i < sURLVariables.length; i++) {
	        sParameterName = sURLVariables[i].split('=');
	
	        if (sParameterName[0] === sParam) {
	            return sParameterName[1] === undefined ? true : sParameterName[1];
	        }
	    }
	}

    function getPropertyToCheck(updateDay) {
        var returnValue = "";

        switch (updateDay.toLowerCase() ) {
            case "maandag":
                returnValue = "Monday";
                break;
            case "dinsdag":
                returnValue = "Tuesday";
                break;
            case "woensdag":
                returnValue = "Wednesday";
                break;
            case "donderdag":
                returnValue = "Thursday";
                break;
            case "zaterdag":
                returnValue = "Saturday";
                break;
            case "mmc (4e woe/maand)":
                returnValue = "Mmc";
                break;
        }

        return returnValue;
    }

    function setDisplayDate(dateUTC, formaat) {
        var dateLocale = moment(dateUTC).locale("nl").format(formaat);
        return dateLocale;
    }

    function getSenderAddress(){

        var returnValue = "";        

        switch (location.hostname.toLowerCase() ) {
            case "intranet-dev.acknowledge.local":
                returnValue = "updateavond@devzone.local";
                break;
            case "intranet-acc.acknowledge.local":
                returnvalue = "updateavond@acknowledge.nl";
                break;
            case "intranet.acknowledge.local":
                returnvalue = "updateavond@acknowledge.nl";
                break;
            default:
                returnValue = "patchmanagement@acknowledge.nl";
        }
        
        return returnValue;
    }

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

})(jQuery);
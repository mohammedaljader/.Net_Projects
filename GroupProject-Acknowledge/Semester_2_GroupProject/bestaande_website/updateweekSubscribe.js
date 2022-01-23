(function($) {

	"use strict";

	//configuration
	var listNameUpdateWeeks = "UpdateWeken",
		listNameSubscriptions = "Inschrijvingen",
		listNameEngineers = "Engineers",
		//updateWeekCoordinator = "stevencharmant@devzone.local", //DEV
		//sendFromAddress = "no-reply@sharepoint"; //DEV
		
		updateWeekCoordinator = "updateavond@acknowledge.nl", //ACC en PRD
		sendFromAddress = "updateavond@acknowledge.nl"; //ACC en PRD

		
	var	curUser = "",
		updateWeeksID = [], // ID of all update weeks
		updateweekSubscribed = false,
		processedRequestsCounter = 0,
		totalRequestsCounter = 0,
		processingSubscriptions = false;

	window.updateweek = function(){}; //namespace for public methods

	//PUBLIC FUNCTIONS

	updateweek.initializeSubscriptions = function () {
		$("#uw-subscribe-message").html("");
	
		//get the display name of current engineer
		var curUserRequest = getCurrentUserNameRequest();
		curUserRequest.done(setCurrentUserName)
					  .then(initializeUpdateWeeks)	;

	    curUserRequest.fail(requestError);
	    
	};
	
	updateweek.processSubscriptions = function() {
	
		if (processingSubscriptions === true) { // check to prevent re-clicking Opslaan-button
			return;
		}
		
		processingSubscriptions = true;
		processedRequestsCounter = 0;
		totalRequestsCounter = updateWeeksID.length;
		
		$.each(updateWeeksID, function( index, _updateWeekID) {
			// re-check no subscription is available for this engineer for this update week
			var subscribedUpdateweekRequest = getUpdateweekSubscriptionCurrentUser(_updateWeekID);
			subscribedUpdateweekRequest.done(function(data) {
				updateweekSubscribed = false;

				if (data.d.results.length === 0 ) {
					//create new item
					sendCreateSubscriptionItemRequest(_updateWeekID);
				} else {
					//update item
					var subscriptionID =data.d.results[0].ID; // updateWeekSubscriptions[pos];
					sendUpdateSubscriptionItemRequest(_updateWeekID, subscriptionID);
				}
			});			  
		});
		
		// check current user is registered engineer. If not send e-mail to coordinator
		checkRegisteredEngineer();

	};
	
	//PRIVATE FUNCTIONS
		function initializeUpdateWeeks() {
			var updateweeksRequest = getUpdateweeksRequest();
			updateweeksRequest.done(setUpdateWeeks)
							  .then(setSubscriptions);
			updateweeksRequest.fail(requestError);				
		}
	
		function getCheckedItem(updateWeekID, day ) {
			var selector = $("#updateweekid" + updateWeekID + " #"+ day + updateWeekID);
			var checked = $(selector).prop("checked");

			if (checked === true) {
				updateweekSubscribed = true;
			}
					
			return checked;
		}
		
		function getComments(updateWeekID) {
			var selector = $("#updateweekid" + updateWeekID + " .uw-subscribe-update-week-comments-input");
			var comments = $(selector).prop("value");
			
			return comments;
		}
	 
		function sendCreateSubscriptionItemRequest(ID) {
			var itemPayload = getItemPayload(ID);

			if (updateweekSubscribed === false) {
				processSubscriptionsDone();
				return;
			}
		    
	        var request = $.ajax({
		        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNameSubscriptions+ "')/items",
		        type: "POST",
		        contentType: "application/json;odata=verbose",
		        data: JSON.stringify(itemPayload),
		        headers: {
		            "Accept": "application/json;odata=verbose",
		            "X-RequestDigest": $("#__REQUESTDIGEST").val()
		        }
		    });
		    
		    request.done(processSubscriptionsDone);
		    request.fail(requestError);
		}
		
		function sendUpdateSubscriptionItemRequest(uwID, subscriptionID) {

			var itemPayload = getItemPayload(uwID);
						
			var request = $.ajax({
		        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNameSubscriptions+ "')/items(" + subscriptionID + ")",
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
		    
		    request.done(processSubscriptionsDone);
		    request.fail(requestError);

		}
				
		function getItemPayload(ID) {
			updateweekSubscribed = false;
			var itemType = getItemTypeForListName(listNameSubscriptions);
			var item = {
			        "__metadata": { "type": itemType },
			        "Title": "Inschrijving",
			        "uwInschrijvingMaandag": getCheckedItem(ID, "ma"),
   			        "uwInschrijvingDinsdag": getCheckedItem(ID, "di"),
   			        "uwInschrijvingWoensdag": getCheckedItem(ID, "wo"),   			        
			        "uwInschrijvingDonderdag": getCheckedItem(ID, "do"),
					"uwInschrijvingMMC": getCheckedItem(ID, "mmc"),
					"uwInschrijvingZaterdag": getCheckedItem(ID, "za"),
					"uwInschrijvingAfgemeld": getCheckedItem(ID, "afm"),
					"uwInschrijvingOpmerkingen": getComments(ID),					
					"uwInschrijvingEngineerId": curUser.Id,
					"uwInschrijvingBegindatumId": ID
					
			    };

			return item;

		}
	
		function setSubscriptions () {
			//get the subscriptions of current engineer
			var subscriptionsRequest = getSubscriptionsCurrentUserRequest();
			subscriptionsRequest.done(setSubscriptionsCurrentUser);
			subscriptionsRequest.fail(requestError);
		
		}

		function getUpdateweeksRequest () {
	
	    	var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('" + listNameUpdateWeeks + "')/items?$select=ID,Title,uwBegindatum&$filter=uwInschrijvingOpen eq 'Ja'&$orderby=uwBegindatum asc";
		   	var requestHeaders = { "accept": "application/json;odata=verbose" };
		    var contentType = "application/json;odata=verbose";
		    var request = $.ajax({
		        url: url,
		        contentType: contentType,
		        headers: requestHeaders
		    });
		    
			return request;
	    }
	    
	    function setUpdateWeeks(data) {

	    	var _updateweeks = data.d.results;
	    	var resultHtml = "";
	    	
   			$.each(_updateweeks , function (index, _updateweek) {

				var  ID = _updateweek.ID;
				
				//add ID to array with update week ID's
				updateWeeksID.push(ID);


				var title = _updateweek.Title;
				var startdate = _updateweek.uwBegindatum;
				var startdateLocale = setDisplayDate(startdate);
				
				resultHtml += "<div id='updateweekid" + ID + "' class='uw-subscribe-update-week'>";
				resultHtml += "<span class='uw-subscribe-update-week-title'>" + title + "</span>";
				resultHtml += "<span class='uw-subscribe-update-week-startdate'>" + startdateLocale + "</span>";
				resultHtml += '<div id="checkboxes">';
				resultHtml += '<label for="ma"><input type="checkbox" id="ma'+ ID +'"/>Maandag</label>';
				resultHtml += '<label for="di"><input type="checkbox" id="di'+ ID +'"/>Dinsdag</label>';
				resultHtml += '<label for="wo"><input type="checkbox" id="wo'+ ID +'"/>Woensdag</label>';
				resultHtml += '<label for="do"><input type="checkbox" id="do'+ ID +'"/>Donderdag</label>';
				resultHtml += '<label for="za"><input type="checkbox" id="za'+ ID +'"/>Zaterdag</label>';
				resultHtml += '<label for="vr"><input type="checkbox" id="mmc'+ ID +'"/>MMC (4e woe/mnd)</label>';					
				resultHtml += '<label for="afm"><input type="checkbox" id="afm'+ ID +'"/>Afmelden</label>';
				resultHtml += '</div>' ;
				resultHtml += '<div class="uw-subscribe-update-week-comments">';
				resultHtml += '<input class="uw-subscribe-update-week-comments-input" type="text" value="" placeholder="Opmerkingen"/>';
				resultHtml += '</div>' ;
				resultHtml += '</div>' ;
			});

	    	$("#uw-subscribe-updateweken").html(resultHtml);
	    	
	    }
	    
	    function getSubscriptionsCurrentUserRequest() {
	    	var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('" + listNameSubscriptions + "')/items?$select=ID,Title,uwInschrijvingEngineer/Title,uwInschrijvingEngineer/EMail,uwInschrijvingMaandag,uwInschrijvingDinsdag,uwInschrijvingWoensdag,uwInschrijvingDonderdag,uwInschrijvingZaterdag,uwInschrijvingMMC,uwInschrijvingAfgemeld,uwInschrijvingOpmerkingen,uwInschrijvingBegindatum/ID,uwInschrijvingBegindatum/uwBegindatum,uwInschrijvingBegindatum/Title&$expand=uwInschrijvingBegindatum,uwInschrijvingEngineer/Id&$filter=uwInschrijvingEngineer/Title eq '" + curUser.Title + "' and uwInschrijvingBegindatum/uwInschrijvingOpenCalc eq 'Ja'";
			var requestHeaders = { "accept": "application/json;odata=verbose" };
		    var contentType = "application/json;odata=verbose";
		    var request = $.ajax({
		        url: url,
		        contentType: contentType,
		        headers: requestHeaders
		    });
		    
			return request;
    
	    }
	    
	    function setSubscriptionsCurrentUser(data) {
	    	var _subscriptions = data.d.results;
	    	
	    	$.each(_subscriptions , function (index, _subscription) {
	    		
	    		setSubscriptionsChecked(_subscription);
	    	});
	    }
	    
	    function setSubscriptionsChecked(_subscription) {
   
    	    var uwID = _subscription.uwInschrijvingBegindatum.ID;
    	    var opmerking = _subscription.uwInschrijvingOpmerkingen;	   
    		var uwDiv = $("#updateweekid" + uwID);
    		    		   
	    	setSubscriptionChecked(_subscription.uwInschrijvingMaandag, uwDiv , uwID, "ma");
	    	setSubscriptionChecked(_subscription.uwInschrijvingDinsdag, uwDiv , uwID, "di");
	    	setSubscriptionChecked(_subscription.uwInschrijvingWoensdag, uwDiv , uwID, "wo");
	    	setSubscriptionChecked(_subscription.uwInschrijvingDonderdag, uwDiv , uwID, "do");
	    	setSubscriptionChecked(_subscription.uwInschrijvingMMC, uwDiv , uwID, "mmc");
	    	setSubscriptionChecked(_subscription.uwInschrijvingZaterdag, uwDiv , uwID, "za");
	    	setSubscriptionChecked(_subscription.uwInschrijvingAfgemeld, uwDiv , uwID, "afm");	    
	    	
			//set the comments
			$(uwDiv).find(".uw-subscribe-update-week-comments-input").prop("value", opmerking);		    		    		    		    		    	
	    }
	    
	    function setSubscriptionChecked(subscribed, uwDiv, uwID, day) {
	    	if (subscribed === true)
	    	{
	    		$(uwDiv).find("#" + day + uwID).prop("checked",true);
	    	}
	    }
	    
	    function getUpdateweekSubscriptionCurrentUser(updateweekId) {
			var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('" + listNameSubscriptions + "')/items?";
			url += "$select=ID,uwInschrijvingBegindatum/Id,uwInschrijvingEngineer/Id&";
			url += "$expand=uwInschrijvingBegindatum/Id,uwInschrijvingEngineer/Id&";
			url += "$filter=uwInschrijvingEngineer/Title  eq '" + curUser.Title + "' and uwInschrijvingBegindatum/Id eq " + updateweekId;

			var requestHeaders = {"accept": "application/json;odata=verbose"};
	
			var request = $.ajax({
				url: url,
				contentType: "application/json;odata=verbose",
				headers: requestHeaders
			});
		
			return request;

		}

		function getCurrentUserNameRequest() {
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
	
	function setCurrentUserName(data) {
	    // set the global variabele
	    curUser = data.d;

	    $("#uw-subscribe-engineer").text(curUser.Title);
	}
	
	function setDisplayDate (dateUTC ) {
	    
	    var dateLocale = "";
    				
		if (dateUTC)
		{
			dateLocale = moment(dateUTC).locale("nl").format('l');
		}
		
		return dateLocale;
	}


	function requestError(err) {
		alert("an error has occurred \n \n" + err.responseText);
		console.log("an error has occurred");
		console.log(err);
    }
    
    function getItemTypeForListName(name) {
	    return "SP.Data." + name.charAt(0).toUpperCase() + name.slice(1) + "ListItem";
	}
	
	function processSubscriptionsDone()
	{
		processedRequestsCounter += 1;
		
		if (processedRequestsCounter === totalRequestsCounter )
		{
			$(".uw-subscribe-message").html("Alle inschrijvingen zijn verwerkt!");
		}
		
//		$(".uw-subscribe-button").show();
	}
	
	
	function checkRegisteredEngineer() {
		var requestCurUserEngineerItem = getRequestCurUserEngineerItem();
		
		requestCurUserEngineerItem.done(processCurUserEngineerItem);
		requestCurUserEngineerItem.fail(requestError);
	}

	function getRequestCurUserEngineerItem() {
	
	    var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('" + listNameEngineers + "')/items?$select=ID,uwEngineer/Title&$expand=uwEngineer/Id&$filter=uwEngineer/Title eq '" + curUser.Title + "'";

	   	var requestHeaders = { "accept": "application/json;odata=verbose" };
	    var contentType = "application/json;odata=verbose";
	    var request = $.ajax({
	        url: url,
	        contentType: contentType,
	        headers: requestHeaders
	    });
	    
		return request;

	}
	
	function processCurUserEngineerItem(data) {
		if (data.d.results.length === 0) 
		{
			// current user is not a registered engineer, send an email.
			var subject = "Niet standaard engineer aanmelding update avond";
			var body = "Beste Update avond,<br /><br />" + curUser.Title + " heeft zich aangemeld voor een update week.<br /><br />Gelieve hier rekening mee te houden in de planning.<br /><br />Met vriendelijke groet,<br/>Update Avond";
			var to = [];
			to.push(updateWeekCoordinator);
			
			var mail = {
		        properties: {
		            __metadata: { 'type': 'SP.Utilities.EmailProperties' },
		            'From': sendFromAddress,
		            'To': { 'results': to },
		            'Body': body,
		            'Subject': subject
		        }
		    };
		    	    
		    var getAppWebUrl = _spPageContextInfo.webServerRelativeUrl;
			var urlTemplate = getAppWebUrl + "/_api/SP.Utilities.Utility.SendEmail";

    		var request = $.ajax({
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

		    request.fail(requestError);
		
		}
	}
	
	
	updateweek.blueprint = function () {
    };	

})(jQuery);

$( document ).ready(function() {
	"use strict";
	$(".uw-subscribe-button").click(function() {
	$(".uw-subscribe-button").hide();
	  updateweek.processSubscriptions();
	});

    updateweek.initializeSubscriptions();
});
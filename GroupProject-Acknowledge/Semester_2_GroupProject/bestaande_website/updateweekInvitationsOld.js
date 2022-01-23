(function($) {

	"use strict";
	
	//configuration
	var listNameEngineers = "Engineers", 
		listNameUpdateweek = "UpdateWeken",
		listNameSubscriptions = "Inschrijvingen",
//		sendFromAddress = "updateavond@devzone.local"; // DEV
		sendFromAddress = "patchmanagement@acknowledge.nl"; // ACC en PRD
		
	var	urlInschrijfFormulier = _spPageContextInfo.webAbsoluteUrl,
		activeEngineers = [],
		subscribersUpdateweek = [],
		sendInvitationsInProgress=false,
		updateweekProperties = [],
		indicators = [],
		mailType = "",
		waitDialog = null;
					
	window.updateweek = function(){}; //namespace for public methods
	
	// PUBLIC FUNCTIONS
	updateweek.sendEmailInit = function(_mailType) {
		showWaitDialog();

		mailType = _mailType;
		updateweekProperties = getUpdateweekProperties();
		indicators = getIndicatorFieldValues(mailType);
		// check email is already sent and ask confirmation to re-send. If OK, send invitation or reminder
		if (checkSendEmail() === true) {
			if (mailType === "Uitnodigingen") {
				sendInvitations();
			}
			else if (mailType === "Reminders") {
				sendReminders();
			}
		}
	};

    updateweek.setDisplayDate = function(divid) {
    	
		var dateUTC = $("#" + divid).text();
			
		if (dateUTC)
		{
			var dateLocale = moment(dateUTC).locale("nl").format('l');
			$("#" + divid).text(dateLocale);
		}
	};

	//PRIVATE FUNCTIONS  
	function sendInvitations() {
		//send invitation to active engineers

		if (sendInvitationsInProgress === false)
		{
			sendInvitationsInProgress = true;
			//get all active engineers
			var activeEngineersRequest = getActiveEngineersRequest();

			activeEngineersRequest.done(setActiveEngineers)
								  .then(function () {
								  	var emailProperties = getInvitationMailProperties();
									if (emailProperties.To.length > 0) {
										sendEmail(emailProperties);
									}
								  }).fail(requestError).always(closeWaitDialog);
		}
	}

	function sendReminders() {
		//send reminders to engineers who did not respond to the invitation

		// get active engineers
		var activeEngineersRequest = getActiveEngineersRequest();
		var subscriptionsUpdateweekRequest = getSubscriptionsCurrentUpdateweekRequest();

		$.when(activeEngineersRequest, subscriptionsUpdateweekRequest)
			.done(function (a1, a2) {
				setActiveEngineers(a1[0]);
				setUpdateWeekSubscribers(a2[0]);
			})
			.then( function () {
				var emailProperties = getReminderMailProperties();
				if (emailProperties.To.length > 0) {
					sendEmail(emailProperties);
				}
			})
			.always(closeWaitDialog);
	}

	function getInvitationMailProperties() {

		var title = updateweekProperties.Title,
			begindatum = updateweekProperties.Startdate,
			emailProperties = [],

			subject = 'Verzoek inschrijven ' + title,	
			body = 'Beste collega,<br /> <br/>';
			body += 'Hierbij het verzoek om je in te schrijven voor <b>' + title + ' (vanaf ' + begindatum + ') </b>.<br /><br />';
			body += 'Via <a href="' + urlInschrijfFormulier  + '">Inschrijven update weken</a> kun je inschrijven voor alle vrijgegeven update weken.<br /><br />';
			body += 'Zodra je ingeschreven bent ontvang je geen reminders meer om in te schrijven voor ' + title + '. <br />';
			body += 'Als je je al eerder hebt ingeschreven voor ' + title + ' kun je via deze e-mail je inschrijving checken en eventueel aanpassen.<br /><br />';
			body += '<b>LET OP: deze lijst wordt als leidend beschouwd, je bent dus zelf verantwoordelijk voor je beschikbaarheid.</b><br /><br />';
			body += 'Met vriendeijke groet,<br /><br />';
			body += 'Patch Management';

		emailProperties.To = activeEngineers;
		emailProperties.Subject = subject;
		emailProperties.Body = body;
	
		return emailProperties;
	}

	function getReminderMailProperties() {
		var title = updateweekProperties.Title,
			begindatum = updateweekProperties.Startdate,
			emailProperties = [],

		//filter the subsribed engineers out of the total list of active engineers
		activeEngineersReminders = activeEngineers.filter( function( el ) {
			return subscribersUpdateweek.indexOf( el ) < 0;
		} );

		var subject = 'REMINDER: Verzoek inschrijven ' + title,	
			body = 'Beste collega,<br /> <br/>';
			body += 'Uit controle is gebleken dat je nog niet gereageerd hebt voor <b>' + title + ' (vanaf ' + begindatum + ') </b>.<br /><br />';
			body += 'Via <a href="' + urlInschrijfFormulier  + '">Inschrijven update weken</a> kun je inschrijven voor alle vrijgegeven update weken.<br /><br />';
			body += 'Zodra je ingeschreven bent ontvang je geen reminders meer om in te schrijven voor ' + title + '. <br />';
			body += '<b>LET OP: deze lijst wordt als leidend beschouwd, je bent dus zelf verantwoordelijk voor je beschikbaarheid.</b><br /><br />';
			body += 'Met vriendeijke groet,<br /><br />';
			body += 'Patch Management';

		emailProperties.To = activeEngineersReminders;
		emailProperties.Subject = subject;
		emailProperties.Body = body;

		return emailProperties;
	}

	function sendEmail(emailProperties) {
		var request = getEmailRequest(emailProperties);

		request.done(function () {
			var message = "E-mail met onderwerp '" + emailProperties.Subject + "' is verstuurd.";
				// sendInvitationsInProgress=false;

			alert(message);
			setEmailSent();

		});  

		request.fail(function (err) {
			alert('Er is iets mis gegaan met het versturen van de e-mail met onderwerp "' + emailProperties.Subject + '" \n \n' + JSON.stringify(err) );
			console.log(JSON.stringify(err));
			sendInvitationsInProgress=false;
		});
	}

	// Requests
	function getEmailRequest(emailProperties) {
				
		var mail = {
	        properties: {
	            __metadata: { 'type': 'SP.Utilities.EmailProperties' },
	            'From': sendFromAddress,
	            'To': { 'results': emailProperties.To },
	            'Body': emailProperties.Body,
	            'Subject': emailProperties.Subject
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

		return request;
		        		        
    }
	    
    function getActiveEngineersRequest () {

    	var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('" + listNameEngineers + "')/items?$expand=uwEngineer&$select=Title,uwEngineerActief,uwEngineer/Title,uwEngineer/EMail&$filter=uwEngineerActief eq 'Ja'",
	   		requestHeaders = { "accept": "application/json;odata=verbose" },
	    	contentType = "application/json;odata=verbose",
	    	request = $.ajax({
				url: url,
				contentType: contentType,
				headers: requestHeaders
			});
	    
		return request;
    }

	function getSubscriptionsCurrentUpdateweekRequest() {

		var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('" + listNameSubscriptions + "')/items?$select=ID,Title,uwInschrijvingEngineer/Title,uwInschrijvingEngineer/EMail,uwInschrijvingMaandag,uwInschrijvingDinsdag,uwInschrijvingWoensdag,uwInschrijvingDonderdag,uwInschrijvingZaterdag,uwInschrijvingMMC,uwInschrijvingAfgemeld,uwInschrijvingOpmerkingen,uwInschrijvingBegindatum/ID&$expand=uwInschrijvingBegindatum,uwInschrijvingEngineer/Id&$filter=uwInschrijvingBegindatum/ID eq " + updateweekProperties.ListId,
			
			contentType = "application/json;odata=verbose",
			requestHeaders = { "accept": "application/json;odata=verbose" },
	    	request = $.ajax({
				url: url,
				contentType: contentType,
				headers: requestHeaders
			});

		return request;
	}

	// Helper functions
	function checkSendEmail() {
		// check Invitations already sent. if so ask confirmation to re-send
		var returnValue = true;

		// get confirmation that e-mail has to (re)send
		if (indicators.mailSent.toLowerCase() === "ja") {
			var message = mailType + " voor '" + updateweekProperties.Title + "' zijn al eerder verstuurd op " + indicators.mailSentDate +". Wilt u deze nogmaals uitsturen?";
			returnValue = confirm(message);
		}

		return returnValue;
	}

	function setActiveEngineers(data) {
		var _engineers = data.d.results,
			arrEngineersEmail = [],
			email = "";
		
		$.each(_engineers, function (index, _engineer) {
			email = "";
			email = _engineer.uwEngineer.EMail;
			
			if(typeof email === 'string') {
				arrEngineersEmail.push(email);
			}
		});
		
		activeEngineers = arrEngineersEmail;
    }

	function setUpdateWeekSubscribers(data) {

		//create array with engineers who subscribed for update week
		var _subscriptions = data.d.results,
			// validSubscription = false,
			arrSubscribersEmail = [];

		$.each(_subscriptions, function (index, _subscription) {

			if (checkValidSubscription(_subscription) === true )
			{
				arrSubscribersEmail.push(_subscription.uwInschrijvingEngineer.EMail);
			}
		});		

		subscribersUpdateweek = arrSubscribersEmail;
	}
	    	  
	function setEmailSent() {
		var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listNameUpdateweek + "')/items(" + updateweekProperties.ListId + ")",
			
			itemPayload = getItemPayload(),
 	
    		request = $.ajax({
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

		request.done(updateItemRequestDone);    
		request.fail(requestError);
		
	}
		
	function updateItemRequestDone() {

		window.location.reload();
	}

	function getItemPayload(){
		var curDate = new Date(),
			itemType = getItemTypeForListName(listNameUpdateweek),
			
			itemPayload = {
				"__metadata": { "type": itemType }	
			};

			itemPayload[indicators.columnCheckbox ] = "Ja";
			itemPayload[indicators.columnDate] = curDate;

			return itemPayload;
	}
	
	function getItemTypeForListName(name) {
	    return "SP.Data." + name.charAt(0).toUpperCase() + name.slice(1) + "ListItem";
	}

	function checkValidSubscription(_subscription) {
		// valid als minimaal 1 dag of aangemeld is aangevinkt. Leeg item of alleen opmerking gevuld is niet valid
		var returnValue = false,
		maa = _subscription.uwInschrijvingMaandag,
		din = _subscription.uwInschrijvingDinsdag,
		woe = _subscription.uwInschrijvingWoensdag,
		don = _subscription.uwInschrijvingDonderdag,
		zat = _subscription.uwInschrijvingZaterdag,
		mmc = _subscription.uwInschrijvingMMC,
		afm = _subscription.uwInschrijvingAfgemeld;

		if (maa === true || din === true || woe === true || don === true || zat === true || mmc === true || afm === true ) {
			returnValue = true;
		}

		return returnValue;
	}

	function getUpdateweekProperties() {
		var properties = [];

		properties.ListId = getUrlParameter("ID");
		properties.ColumnInvitationSent = "uwUitnodigingVerstuurd";
		properties.ColumnReminderSent = "uwReminderVerstuurd";
		properties.ColumnDateInvitationSent = "uwDatumUitnodigingVerstuurd";
		properties.ColumnDateReminderSent = "uwDatumReminderVerstuurd";
		properties.Title = $("#updateWeekTitle").text();
		properties.Startdate = $("#uwBegindatum").text();
		properties.InvitationSent = $("#" + properties.ColumnInvitationSent ).text();
		properties.ReminderSent = $("#" + properties.ColumnReminderSent).text();
		properties.DateInvitationSent = $("#" + properties.ColumnDateInvitationSent).text();
		properties.DateReminderSent = $("#" + properties.ColumnDateReminderSent).text();

		return properties;
	}

	function getIndicatorFieldValues() {
		var indicators = [];

		//get information based on mailtype
		if (mailType.toLowerCase() === "uitnodigingen") {
				indicators.mailSent = updateweekProperties.InvitationSent;
				indicators.mailSentDate = updateweekProperties.DateInvitationSent;
				indicators.columnCheckbox = updateweekProperties.ColumnInvitationSent;
				indicators.columnDate = updateweekProperties.ColumnDateInvitationSent;
		}
		else if (mailType.toLowerCase() === "reminders") {
				indicators.mailSent = updateweekProperties.ReminderSent;
				indicators.mailSentDate = updateweekProperties.DateReminderSent;
				indicators.columnCheckbox = updateweekProperties.ColumnReminderSent;
				indicators.columnDate = updateweekProperties.ColumnDateReminderSent;				
		}

		return indicators;
	}

	// function filterSubscribedEngineers() {
	// 	var activeEngineersNotSubscribed = activeEngineers.filter( function( el ) {
	// 		return subscribersUpdateweek.indexOf( el ) < 0;
	// 	} );
	// }

	function showWaitDialog() {
        if (waitDialog === null) {
         waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose(SP.Res.dialogLoading15);
      }
    }

    function closeWaitDialog() {
        waitDialog.close();
        waitDialog = null;
    }

	function requestError(err) {
		alert("an error has occurred \n\n " + err.responseText);
		console.log("an error has occurred");
		console.log(err);
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

})(jQuery);

$( document ).ready(function() {
	"use strict";

    updateweek.setDisplayDate("uwBegindatum");
    updateweek.setDisplayDate("uwDatumUitnodigingVerstuurd");
	updateweek.setDisplayDate("uwDatumReminderVerstuurd");
	updateweek.setDisplayDate("uwDatumPlanningGemaakt");	
	updateweek.setDisplayDate("uwDatumPlanningVerstuurd");	
});

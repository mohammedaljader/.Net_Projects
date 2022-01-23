
// $(document).ready(function() {
//   var groupby = document.getElementsByClassName(".ms-gb2");
//   groupby.click(function(){
//     reRenderView();
//   });
// });


document.addEventListener('DOMContentLoaded', function(){ 
    var groupby = document.getElementsByClassName(".ms-gb2");
    groupby.click(function(){
    reRenderView();
    });    
}, false);


function setComments(ctx) {
    
    var comments = ctx.CurrentItem.uwWerklijstOpmerking;
    var returnValue = "";
    if (typeof comments === "string" && comments.length > 0) {
        returnValue = "<span title='" + comments + "'><b>Opmerkingen</b></span>";
    }
    
    return returnValue; 
}

function postRenderHandler(ctx) {
    var resultaatColors =  {
        'Niet gestart' : '#fafafa',  
        'In behandeling': '#c7c7c7',
        'Klaar' :  '#cbf7d9',
        'Klaar (minor issues)': '#fff5b8',
        'Klaar (major issues)': '#fff5b8',
        'Mislukt': '#ffada0',
        'Geannuleerd': '#cbf7d9'
    };

    var rows = ctx.ListData.Row;

    for (var i=0;i<rows.length;i++)
    {

        var resultaat = rows[i]["uwWerklijstResultaat"];
        if (typeof resultaat === "string") {
            var opmerking = (rows[i]["uwWerklijstOpmerking"]);
            var rowId = GenerateIIDForListItem(ctx, rows[i]);
            var row = document.getElementById(rowId); 

            row.style.backgroundColor = resultaatColors [resultaat];
        }
    }    

}

function reRenderView() {
    var overrideCurrentContext = {};

    overrideCurrentContext.Templates = {};

    overrideCurrentContext.Templates.Fields = {
        'uwWerklijstOpmerking': { 'View': setComments }
    };

    overrideCurrentContext.OnPostRender = postRenderHandler;



    SPClientTemplates.TemplateManager.RegisterTemplateOverrides(overrideCurrentContext); 
}

SP.SOD.executeFunc("clienttemplates.js", "SPClientTemplates", function() {
   reRenderView();

});

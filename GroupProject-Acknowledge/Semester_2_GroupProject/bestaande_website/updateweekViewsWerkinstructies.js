function setCustomer(ctx) {
    
    var relaties = ctx.CurrentItem.Relaties,
        returnValue = "";

    if (typeof relaties === 'string') {
        returnValue = "Algemeen";
    } else {
        returnValue = relaties.Label;
    }
    
    return returnValue; 
}

function reRenderView() {
    var overrideCurrentContext = {};

    overrideCurrentContext.Templates = {};

    overrideCurrentContext.Templates.Fields = {
        'Relaties': { 'View': setCustomer }
    };

    SPClientTemplates.TemplateManager.RegisterTemplateOverrides(overrideCurrentContext); 
}

SP.SOD.executeFunc("clienttemplates.js", "SPClientTemplates", function() {
    reRenderView();
 
 });
#pragma checksum "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f05dda9ad6f513edae189e5eceab6939b2e92929"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Job_Details), @"mvc.1.0.view", @"/Views/Job/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\_ViewImports.cshtml"
using JobCentrum_Website;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\_ViewImports.cshtml"
using JobCentrum_Website.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f05dda9ad6f513edae189e5eceab6939b2e92929", @"/Views/Job/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2dab00a7de70fa8a9fb29c9565ec0ddc13815d73", @"/Views/_ViewImports.cshtml")]
    public class Views_Job_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<JobCentrum_Website.Models.JobViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:48px; height:48px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div style=\"margin-left: 200px;\">\n    <h1>Details Job</h1>\n    <hr />\n    <div>\n        <dl class=\"row\">\n            <dt class=\"col-sm-2\" hidden>\n                ");
#nullable restore
#line 13 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Job_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dt>\n            <dd class=\"col-sm-10\" hidden>\n                ");
#nullable restore
#line 16 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayFor(model => model.Job_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt class=\"col-sm-2\">\n                ");
#nullable restore
#line 19 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Job_name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dt>\n            <dd class=\"col-sm-10\">\n                ");
#nullable restore
#line 22 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayFor(model => model.Job_name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt class=\"col-sm-2\">\n                ");
#nullable restore
#line 25 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Job_description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dt>\n            <dd class=\"col-sm-10\">\n                ");
#nullable restore
#line 28 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayFor(model => model.Job_description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt class=\"col-sm-2\">\n                ");
#nullable restore
#line 31 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Job_image));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dt>\n            <dd class=\"col-sm-10\">\n");
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f05dda9ad6f513edae189e5eceab6939b2e929297898", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1131, "~/images/", 1131, 9, true);
#nullable restore
#line 35 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
AddHtmlAttributeValue("", 1140, Html.DisplayFor(model => model.Job_image), 1140, 42, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </dd>\n            <dt class=\"col-sm-2\">\n                ");
#nullable restore
#line 38 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Job_location));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dt>\n            <dd class=\"col-sm-10\">\n                ");
#nullable restore
#line 41 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayFor(model => model.Job_location));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt class=\"col-sm-2\">\n                ");
#nullable restore
#line 44 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Categorie_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dt>\n            <dd class=\"col-sm-10\">\n                ");
#nullable restore
#line 47 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
           Write(ViewBag.categorie.Categorie_name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n        </dl>\n    </div>\n    <div>\n        ");
#nullable restore
#line 52 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Job\Details.cshtml"
   Write(Html.ActionLink("Edit", "Edit", new { id = Model.Job_Id }, new { @class = "btn btn-info" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f05dda9ad6f513edae189e5eceab6939b2e9292911537", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    </div>\n\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<JobCentrum_Website.Models.JobViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "405bc556b95bf3af23ed84fdb0b56916464b8c6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Categorie_Index), @"mvc.1.0.view", @"/Views/Categorie/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"405bc556b95bf3af23ed84fdb0b56916464b8c6b", @"/Views/Categorie/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2dab00a7de70fa8a9fb29c9565ec0ddc13815d73", @"/Views/_ViewImports.cshtml")]
    public class Views_Categorie_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<JobCentrum_Website.Models.CategorieViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div style=\"margin-left: 200px; min-height: 500px;\">\n    <h1>Categories List</h1>\n\n    <p>\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "405bc556b95bf3af23ed84fdb0b56916464b8c6b3972", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    </p>\n    <table class=\"table\">\n        <thead>\n            <tr>\n                <th hidden>\n                    ");
#nullable restore
#line 17 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Categorie_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </th>\n                <th>\n                    ");
#nullable restore
#line 20 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Categorie_name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </th>\n                <th>\n                    ");
#nullable restore
#line 23 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Categorie_description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </th>\n                <th></th>\n            </tr>\n        </thead>\n        <tbody>\n");
#nullable restore
#line 29 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
             if (Model.Count() != 0)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td hidden>\n                            ");
#nullable restore
#line 35 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Categorie_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                        <td>\n                            ");
#nullable restore
#line 38 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Categorie_name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                        <td>\n                            ");
#nullable restore
#line 41 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Categorie_description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                        <td>\n                            ");
#nullable restore
#line 44 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                       Write(Html.ActionLink("Edit", "Edit", new { id = item.Categorie_Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                            ");
#nullable restore
#line 45 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                       Write(Html.ActionLink("Details", "Details", new { id = item.Categorie_Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                            ");
#nullable restore
#line 46 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                       Write(Html.ActionLink("Delete", "Delete", new { id = item.Categorie_Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                    </tr>\n");
#nullable restore
#line 49 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
                 
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\n                    <td>\n                        <p style=\"color: red;\">No Categories founded..</p>\n                    </td>\n                </tr>\n");
#nullable restore
#line 58 "C:\Users\malja\OneDrive\Documenten\semester-2-software-fontys\JobCentrum_Website\JobCentrum_Website\Views\Categorie\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\n    </table>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<JobCentrum_Website.Models.CategorieViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

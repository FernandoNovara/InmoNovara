#pragma checksum "D:\Xampp\htdocs\.Net\InmoNovara\Views\Usuario\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8eb21af2c62fa6e7c1d5835d4a99614fe2e073d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuario_Login), @"mvc.1.0.view", @"/Views/Usuario/Login.cshtml")]
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
#line 1 "D:\Xampp\htdocs\.Net\InmoNovara\Views\_ViewImports.cshtml"
using InmoNovara;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Xampp\htdocs\.Net\InmoNovara\Views\_ViewImports.cshtml"
using InmoNovara.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8eb21af2c62fa6e7c1d5835d4a99614fe2e073d1", @"/Views/Usuario/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6fa93697ab8aa460e977aa66e5e5149b3e149a32", @"/Views/_ViewImports.cshtml")]
    public class Views_Usuario_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Xampp\htdocs\.Net\InmoNovara\Views\Usuario\Login.cshtml"
  
	ViewData["Title"] = "Acceso de usuarios";

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Xampp\htdocs\.Net\InmoNovara\Views\Usuario\Login.cshtml"
Write(await Html.PartialAsync("_Login"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "[route-data=true]")]
    public class RouteDataTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext Context { get; set; } = new();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "bg-primary m-2 p-2");

            var list = new TagBuilder("ul");
            
            list.Attributes["class"] = "list-group-item";

            var rd = Context.RouteData.Values;



        }
    }
}
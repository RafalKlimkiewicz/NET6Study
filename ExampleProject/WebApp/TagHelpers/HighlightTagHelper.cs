using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("*", Attributes = "[highlight=true]")]
    public class HighlightTagHelper : TagHelper
    {
        public override void Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetHtmlContent("<b><i>");
            output.PostContent.SetHtmlContent("</b></i>");
        }
    }
}

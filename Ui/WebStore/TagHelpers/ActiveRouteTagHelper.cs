using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldBeActive())
                MakeActive(output);

            output.Attributes.RemoveAll("is-active-route");
        }

        private bool ShouldBeActive()
        {
            var currentController = ViewContext.RouteData.Values["Controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["Action"].ToString();

            if (!string.IsNullOrWhiteSpace(Controller) &&
                currentController != Controller)
                return false;

            if (!string.IsNullOrWhiteSpace(Action) &&
                currentAction != Action)
                return false;

            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            const string classAttribString = "class";

            var classAttribute = output.Attributes
                .SingleOrDefault(a => a.Name == classAttribString);
            if (classAttribute == null)
            {
                classAttribute = new TagHelperAttribute(classAttribString, "active");
                output.Attributes.Add(classAttribute);

                return;
            }

            var isNotContainsActive = classAttribute.Value?.ToString()
                                       .Contains("active", StringComparison.Ordinal) != true;
            if (isNotContainsActive)
            {
                output.Attributes.SetAttribute(classAttribString, classAttribute.Value is null 
                    ? "active" // Если атрибут class есть без стилей (без значения атрибута)
                    : classAttribute.Value + " active");    // Если атрибут class есть и есть какие-то еще стили в его значении
            }
        }
    }
}

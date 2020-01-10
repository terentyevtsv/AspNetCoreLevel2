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

        /// <summary>
        /// Отслеживание активного меню
        /// </summary>
        /// <param name="context">Атрибуты прописанные в razor</param>
        /// <param name="output">Атрибуты, видимые клиенту на выходе</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldBeActive())
                MakeActive(output);

            // Удаляем все атрибуты is-active-route из тега на выходе
            output.Attributes.RemoveAll("is-active-route");
        }

        private bool ShouldBeActive()
        {
            // Смотрим текущий контроллер и действие, открытые в браузере
            var currentController = ViewContext.RouteData.Values["Controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["Action"].ToString();

            // Если они не совпадают с рассматриваемыми контроллером и действием текущего меню-ссылки
            // с атрибутом is-active-route, то меню неактивное
            if (!string.IsNullOrWhiteSpace(Controller) &&
                currentController != Controller)
                return false;

            if (!string.IsNullOrWhiteSpace(Action) &&
                currentAction != Action)
                return false;

            // Иначе меню активное
            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            const string classAttribString = "class";

            // Среди всех атрибутов тега ищем атрибут class
            var classAttribute = output.Attributes
                .SingleOrDefault(a => a.Name == classAttribString);
            if (classAttribute == null)
            {
                // Если он не найден, то создаем новый атрибут class со значением active
                classAttribute = new TagHelperAttribute(classAttribString, "active");
                output.Attributes.Add(classAttribute);

                return;
            }

            // Если атрибут class уже есть но нет значения active
            var isNotContainsActive = classAttribute.Value?.ToString()
                                       .Contains("active", StringComparison.Ordinal) != true;
            if (isNotContainsActive)
            {
                // добавление active в значение атрибута class
                output.Attributes.SetAttribute(classAttribString, classAttribute.Value is null 
                    ? "active" // Если атрибут class есть без стилей (без значения атрибута)
                    : classAttribute.Value + " active");    // Если атрибут class есть и есть какие-то еще стили в его значении
            }
        }
    }
}

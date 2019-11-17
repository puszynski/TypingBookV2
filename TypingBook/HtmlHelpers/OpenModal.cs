using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypingBook.HtmlHelpers
{
    public static class OpenModal
    {
        public static IHtmlContent Modal(this IHtmlHelper htmlHelper, IHtmlContent url)
        {

            var builder = new TagBuilder("a");

            builder.MergeAttribute("data-toggle", "modal");
            builder.MergeAttribute("data-target", "#mainModal");
            builder.MergeAttribute("class", "modal-action");
            builder.MergeAttribute("data-rel", url.ToString());
            builder.MergeAttribute("href", "#");

            var result = builder.RenderBody();
            var result2 = builder.RenderStartTag();


            return result;//new HtmlString(result);
        }

        public static String HelloWorldString(this IHtmlHelper htmlHelper)
            => "<strong>Hello World</strong>";

        //public static MvcHtmlString OptionModalMenuItem(this HtmlHelper htmlHelper, string label, string url,
        //   bool modal = false, string cl = "", Dictionary<string, string> adds = null)
        //{
        //    var li = new TagBuilder("li");
        //    if (adds == null)
        //        adds = new Dictionary<string, string>();
        //    if (modal)
        //    {
        //        adds.Add("data-toggle", "modal");
        //        adds.Add("class", "modal-action");
        //        adds.Add("data-target", "#myModal");
        //        adds.Add("data-rel", url);
        //        adds.Add("href", "#");
        //    }
        //    else
        //    {
        //        adds.Add("href", url);
        //    }
        //    li.InnerHtml = htmlHelper.OptionLink(label, cl, adds).ToString();
        //    return MvcHtmlString.Create(li.ToString());
        //}
    }
}
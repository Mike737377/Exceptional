using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Exceptional.Web.HtmlExtensions
{
    public static class HtmlExtensions
    {
        private static readonly string ModalFormat = @"<div class=""modal fade"" id=""{0}"" tabindex=""-1"" role=""dialog"" aria-hidden=""true""><div class=""modal-dialog"">{1}<div class=""modal-content""></div></div></div>";

        public static MvcHtmlString Modal<TModel>(this HtmlHelper<TModel> htmlHelper, string modalId, string partialViewName, TModel model)
        {
            return new MvcHtmlString(string.Format(ModalFormat, modalId, htmlHelper.Partial(partialViewName, model)));
        }

        public static MvcHtmlString Modal(this HtmlHelper htmlHelper, string modalId, string partialViewName)
        {
            return new MvcHtmlString(string.Format(ModalFormat, modalId, htmlHelper.Partial(partialViewName)));
        }
    }
}
using System.Web;
using System.Web.Mvc;

namespace Exceptional.Client.SampleWebsite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
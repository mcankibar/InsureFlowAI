using System.Web;
using System.Web.Mvc;
using InsureFlowAI.Web.Filters;

namespace InsureFlowAI.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CultureActionFilter());
        }
    }
}

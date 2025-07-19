using InsureFlowAI.Web.Helpers;
using System.Web.Mvc;

namespace InsureFlowAI.Web.Filters
{
    public class CultureActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string culture = CultureHelper.GetCultureFromRequest(filterContext.HttpContext.Request);
            CultureHelper.SetCulture(culture);
            base.OnActionExecuting(filterContext);
        }
    }
}

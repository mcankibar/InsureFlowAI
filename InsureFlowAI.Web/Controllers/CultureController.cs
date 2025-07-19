using System;
using System.Web;
using System.Web.Mvc;
using InsureFlowAI.Web.Helpers;

namespace InsureFlowAI.Web.Controllers
{
    public class CultureController : Controller
    {
        [HttpPost]
        [HttpGet]
        public ActionResult ChangeCulture(string culture, string returnUrl)
        {
            if (CultureHelper.IsValidCulture(culture))
            {
                // Cookie'ye kültür bilgisini kaydet
                var cookie = new HttpCookie("_culture", culture)
                {
                    Expires = DateTime.Now.AddYears(1),
                    HttpOnly = true
                };
                Response.Cookies.Add(cookie);

                // Thread'e kültürü uygula
                CultureHelper.SetCulture(culture);
            }

            // Geri dönülecek URL'i kontrol et
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        public ActionResult GetCurrentCulture()
        {
            var culture = CultureHelper.GetCurrentCulture();
            return Json(new { culture = culture }, JsonRequestBehavior.AllowGet);
        }
    }
}

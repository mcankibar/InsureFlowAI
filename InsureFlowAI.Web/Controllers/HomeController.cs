using InsureFlowAI.BLL.Services;
using InsureFlowAI.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InsureFlowAI.BLL.Concrete;

namespace InsureFlowAI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarouselManager _carouselManager;
        private readonly ServicesManager _servicesManager;
        private readonly FAQManager _faqManager;
        private readonly SocialMediaController _socialMediaController;

        public HomeController(CarouselManager carouselManager, SocialMediaController socialMediaController, ServicesManager servicesManager, FAQManager faqManager)
        {
            _carouselManager = carouselManager;
            _socialMediaController = socialMediaController;
            _servicesManager = servicesManager;
            _faqManager = faqManager;
        }

        public async Task<ActionResult> Index()
        {
            var homePageData = new HomePageViewModel()
            {
                FAQList =  _faqManager.TGetAll().ToList(),
                ServicesList = _servicesManager.TGetAll().ToList(),
                CarouselItems = _carouselManager.TGetAll().Select(c => new CarouselVM
                {
                    ID = c.ID,
                    WelcomeText = c.WelcomeText,
                    Headline = c.Headline,
                    SubHeadline = c.SubHeadline,
                    ImageUrl = c.ImageUrl

                }).ToList()
            };
            try
            {
                homePageData.LinkedInProfileVM = await _socialMediaController.GetLinkedInProfileAsync();
            }
            catch (Exception ex)
            {
                homePageData.LinkedInProfileVM = new LinkedInProfileVM
                {
                    CompanyName = "Allianz",
                    ProfileUrl = "https://www.linkedin.com/company/allianz",
                    FollowersCount = 0,
                    EmployeeCount = 0
                };
            }
                 
                

            return View(homePageData);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
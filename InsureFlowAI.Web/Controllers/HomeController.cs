using InsureFlowAI.BLL.Services;
using InsureFlowAI.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureFlowAI.BLL.Concrete;

namespace InsureFlowAI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarouselManager _carouselManager;

        public HomeController(CarouselManager carouselManager)
        {
            _carouselManager = carouselManager;
        }

        public ActionResult Index()
        {
            var homePageData = new HomePageViewModel()
            {
                CarouselItems = _carouselManager.TGetAll().Select(c => new CarouselVM
                {
                    ID = c.ID,
                    WelcomeText = c.WelcomeText,
                    Headline = c.Headline,
                    SubHeadline = c.SubHeadline,
                    ImageUrl = c.ImageUrl

                }).ToList()
            };
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
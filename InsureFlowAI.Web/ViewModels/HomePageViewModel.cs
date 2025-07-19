using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureFlowAI.DAL.Models.DataModel;

namespace InsureFlowAI.Web.ViewModels
{
    public class HomePageViewModel
    {
        public List<CarouselVM> CarouselItems { get; set; }
        public List<Tbl_Services> ServicesList { get; set; }
        public List<Tbl_FAQ> FAQList { get; set; }

        public LinkedInProfileVM LinkedInProfileVM { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureFlowAI.Web.ViewModels
{
    public class CarouselVM
    {
        public int ID { get; set; } 
        public string WelcomeText { get; set; }
        public string Headline { get; set; }
        public string SubHeadline { get; set; }
        public string ImageUrl { get; set; }
    }
}
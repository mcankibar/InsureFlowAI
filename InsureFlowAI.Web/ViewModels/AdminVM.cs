using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureFlowAI.DAL.Models.DataModel;

namespace InsureFlowAI.Web.ViewModels
{
    public class AdminVM
    { 
        public List<Tbl_FAQ> faqList;

       public List<Tbl_Services> servicesList;

       public AdminVM(List<Tbl_FAQ> faqList, List<Tbl_Services> servicesList)
       {
           this.faqList = faqList;
           this.servicesList = servicesList;
       }
    }
}
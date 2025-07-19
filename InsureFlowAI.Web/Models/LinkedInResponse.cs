using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureFlowAI.Web.Models
{
    public class LinkedInResponse
    {


        public LinkedInCompanyData Data { get; set; }
        public string Message { get; set; } // Hata veya bilgi mesajları için


        public class LinkedInCompanyData
        {
            [JsonProperty("company_name")] // JSON'daki isimle eşleştir
            public string CompanyName { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("follower_count")]
            public int FollowerCount { get; set; }

            [JsonProperty("linkedin_url")]
            public string LinkedInUrl { get; set; }

            [JsonProperty("logo_url")]
            public string LogoUrl { get; set; }


            [JsonProperty("employee_count")]
            public int EmployeeCount { get; set; }

            [JsonProperty("industries")]
            public string[] Industries { get; set; }

            [JsonProperty("website")]
            public string Website { get; set; }


        }


    }
}
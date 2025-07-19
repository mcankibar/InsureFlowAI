using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureFlowAI.Web.ViewModels
{
    public abstract class SocialMediaProfileVM
    {
        public abstract string Platform { get; }
        public string Username { get; set; } 
        public string ProfileUrl { get; set; } 
        public string ProfilePictureUrl { get; set; } 
        public string FullName { get; set; } 
        public string Bio { get; set; } 
        public int FollowersCount { get; set; } 
        public int FollowingCount { get; set; } 
        public int PostsCount { get; set; } 
        public bool IsPrivate { get; set; } 
        public DateTime? LastUpdated { get; set; } 
    }

  


    public class LinkedInProfileVM : SocialMediaProfileVM
    {
        public override string Platform => "LinkedIn";
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }

        public int EmployeeCount { get; set; }
        public string[] Industries { get; set; }
        public string WebsiteUrl { get; set; }

    }

    public class InstagramProfileVM : SocialMediaProfileVM
    {
        public override string Platform => "Instagram";

    }
}
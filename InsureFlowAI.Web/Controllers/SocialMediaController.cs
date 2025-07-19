using InsureFlowAI.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InsureFlowAI.Web.Models;

namespace InsureFlowAI.Web.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly HttpClient _httpClient;

        public SocialMediaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LinkedInProfileVM> GetLinkedInProfileAsync()
        {
            string companyName = "allianz";
            string linkedInCompanyBaseUrl = "https://www.linkedin.com/company/";
            string fullLinkedInUrl = $"{linkedInCompanyBaseUrl}{companyName}/";

            string encodedLinkedInUrl = HttpUtility.UrlEncode(fullLinkedInUrl);

            string rapidApiKey = ConfigurationManager.AppSettings["RapidApiKey"];
            string rapidApiHost = ConfigurationManager.AppSettings["RapidApiHost_LinkedIn"];

            if (string.IsNullOrEmpty(rapidApiKey) || string.IsNullOrEmpty(rapidApiHost))
            {
                throw new InvalidOperationException("RapidAPI key or host for LinkedIn is not configured in Web.config.");
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://fresh-linkedin-profile-data.p.rapidapi.com/get-company-by-linkedinurl?linkedin_url={encodedLinkedInUrl}"),
                Headers =
                {
                    { "x-rapidapi-key", rapidApiKey },
                    { "x-rapidapi-host", rapidApiHost },
                },
            };

            try
            {
                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode(); 
                    var json = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonConvert.DeserializeObject<LinkedInResponse>(json);

                    if (apiResponse?.Data != null)
                    {
                        return new LinkedInProfileVM
                        {

                            CompanyName = apiResponse.Data.CompanyName,
                            CompanyDescription = apiResponse.Data.Description,
                            FollowersCount = apiResponse.Data.FollowerCount,
                            ProfileUrl = apiResponse.Data.LinkedInUrl,
                            ProfilePictureUrl = apiResponse.Data.LogoUrl,
                            EmployeeCount = apiResponse.Data.EmployeeCount,
                            Industries = apiResponse.Data.Industries,
                            WebsiteUrl = apiResponse.Data.Website,
                            LastUpdated = DateTime.Now,

                            Username = apiResponse.Data.CompanyName, 
                            FullName = apiResponse.Data.CompanyName,
                            Bio = apiResponse.Data.Description,
                            FollowingCount = 0, 
                            PostsCount = 0, 
                            IsPrivate = false 
                        };
                    }
                }
                return null; 
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error calling LinkedIn API for {companyName}: {ex.Message}");
                throw new Exception($"Failed to fetch LinkedIn company profile for {companyName}. Details: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing LinkedIn JSON for {companyName}: {ex.Message}");
                throw new Exception($"Invalid JSON response from LinkedIn API for {companyName}. Details: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during LinkedIn API call for {companyName}: {ex.Message}");
                throw;
            }
        }
    }
}
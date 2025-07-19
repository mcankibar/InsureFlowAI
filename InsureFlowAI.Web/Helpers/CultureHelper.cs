using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace InsureFlowAI.Web.Helpers
{
    public static class CultureHelper
    {
        // Supported cultures
        private static readonly Dictionary<string, string> _supportedCultures = new Dictionary<string, string>
        {
            { "en-US", "English" },
            { "tr-TR", "Türkçe" },
            { "fr-FR", "Français" },
            { "es-ES", "Español" },
            { "ar-SA", "العربية" }
        };

        public static List<CultureInfo> GetSupportedCultures()
        {
            return _supportedCultures.Keys.Select(culture => new CultureInfo(culture)).ToList();
        }

        public static Dictionary<string, string> GetSupportedCulturesWithNames()
        {
            return _supportedCultures;
        }

        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        public static string GetCurrentCultureDisplayName()
        {
            string currentCulture = GetCurrentCulture();
            return _supportedCultures.ContainsKey(currentCulture) 
                ? _supportedCultures[currentCulture] 
                : "English";
        }

        public static void SetCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
                culture = "en-US";

            if (!_supportedCultures.ContainsKey(culture))
                culture = "en-US";

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }

        public static bool IsValidCulture(string culture)
        {
            return !string.IsNullOrEmpty(culture) && _supportedCultures.ContainsKey(culture);
        }

        public static string GetCultureFromRequest(HttpRequestBase request)
        {
            if (request.Cookies["_culture"] != null)
            {
                string cultureCookie = request.Cookies["_culture"].Value;
                if (IsValidCulture(cultureCookie))
                    return cultureCookie;
            }

            if (request.UserLanguages != null && request.UserLanguages.Length > 0)
            {
                string browserCulture = request.UserLanguages[0];
                if (IsValidCulture(browserCulture))
                    return browserCulture;

                var partialMatch = _supportedCultures.Keys.FirstOrDefault(c => 
                    c.StartsWith(browserCulture.Split('-')[0], StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(partialMatch))
                    return partialMatch;
            }

            return "en-US";
        }
    }
}

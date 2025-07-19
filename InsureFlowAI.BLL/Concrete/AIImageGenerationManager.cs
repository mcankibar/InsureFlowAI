using InsureFlowAI.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace InsureFlowAI.BLL.Concrete
{
    public class AIImageGenerationManager : IAIImageGenerationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _rapidApiKey;
        private readonly string _baseUrl;

        public AIImageGenerationManager()
        {
            _httpClient = new HttpClient();
            
            _baseUrl = "https://stable-diffusion-1-5-3-xl-20-models-image-generator.p.rapidapi.com/use_model_stable_diffusion_xl/sq";
        }

        public async Task<string> GenerateImageAsync(string prompt)
        {
            return await GenerateImageAsync(prompt, "realistic");
        }

        public async Task<string> GenerateImageAsync(string prompt, string style = "realistic")
        {
            try
            {
                // Query parametresi olarak prompt ekle
                var promptText = $"{prompt}, {style} style, high quality, professional";
                var encodedPrompt = Uri.EscapeDataString(promptText);
                var requestUri = $"{_baseUrl}?prompt={encodedPrompt}&strong=11";

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(requestUri),
                    Headers =
                    {
                        { "x-rapidapi-key", _rapidApiKey },
                        { "x-rapidapi-host", "stable-diffusion-1-5-3-xl-20-models-image-generator.p.rapidapi.com" },
                    }
                };

                // Debug: Request URL'yi logla
                System.Diagnostics.Debug.WriteLine($"Request URL: {requestUri}");

                using (var response = await _httpClient.SendAsync(request))
                {
                    // Debug: Response'u logla
                    System.Diagnostics.Debug.WriteLine($"API Response Status: {response.StatusCode}");
                    System.Diagnostics.Debug.WriteLine($"Content Type: {response.Content.Headers.ContentType}");
                    System.Diagnostics.Debug.WriteLine($"Content Length: {response.Content.Headers.ContentLength}");

                    if (response.IsSuccessStatusCode)
                    {
                        // Content type kontrol et
                        var contentType = response.Content.Headers.ContentType?.MediaType;
                        
                        if (contentType?.StartsWith("image/") == true)
                        {
                            // Binary image data geldi, base64'e çevir
                            var imageBytes = await response.Content.ReadAsByteArrayAsync();
                            var base64String = Convert.ToBase64String(imageBytes);
                            var dataUrl = $"data:{contentType};base64,{base64String}";
                            
                            System.Diagnostics.Debug.WriteLine($"Image received, size: {imageBytes.Length} bytes");
                            System.Diagnostics.Debug.WriteLine($"Base64 length: {base64String.Length}");
                            
                            return dataUrl;
                        }
                        else
                        {
                            // Text response (JSON veya URL)
                            var responseContent = await response.Content.ReadAsStringAsync();
                            System.Diagnostics.Debug.WriteLine($"API Response Content: {responseContent}");
                            
                            // Response boş mu kontrol et
                            if (string.IsNullOrWhiteSpace(responseContent))
                            {
                                throw new Exception("API returned empty response");
                            }

                            // Response JSON mi kontrol et
                            if (!responseContent.Trim().StartsWith("{") && !responseContent.Trim().StartsWith("["))
                            {
                                // JSON değil, muhtemelen direkt image URL
                                System.Diagnostics.Debug.WriteLine("Response is not JSON, returning as-is");
                                return responseContent.Trim();
                            }

                            try
                            {
                                // JSON parsing'i güvenli şekilde yap
                                dynamic result = JsonConvert.DeserializeObject(responseContent);
                                
                                // Bu API'nin response formatına göre ayarla
                                // Olası formatlar: result.image_url, result.output, result[0].url
                                return result.ToString();
                            }
                            catch (JsonException jsonEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"JSON Parsing Error: {jsonEx.Message}");
                                // JSON parsing hatası durumunda direkt response'u döndür
                                return responseContent.Trim();
                            }
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"API Error ({response.StatusCode}): {errorContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Image generation failed: {ex.Message}");
            }
        }

        public async Task<bool> IsImageUrlValid(string imageUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(imageUrl);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}

using InsureFlowAI.BLL.Abstract;
using InsureFlowAI.BLL.Concrete;
using InsureFlowAI.DAL.Models.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InsureFlowAI.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly FAQManager _faqManager;
        private readonly ServicesManager _servicesManager;
        private readonly AIImageGenerationManager _aiImageGenerationManager;
        private readonly AIFAQGenerationManager _aiFAQGenerationManager;

        public AdminController(FAQManager faqManager, ServicesManager servicesManager, AIImageGenerationManager aiImageGenerationManager, AIFAQGenerationManager aiFAQGenerationManager)
        {
            _faqManager = faqManager;
            _servicesManager = servicesManager;
            _aiImageGenerationManager = aiImageGenerationManager;
            _aiFAQGenerationManager = aiFAQGenerationManager;
        }

        public ActionResult Index()
        {
            var faqList = _faqManager.TGetAll();
            var servicesList = _servicesManager.TGetAll();
            var adminViewModel = new ViewModels.AdminVM(faqList, servicesList);
            return View(adminViewModel);
        }

        // Generate Image endpoint ekle
        [HttpPost]
        public async Task<JsonResult> GenerateImage(string prompt)
        {
            try
            {
                
                var imageDataUrl = await _aiImageGenerationManager.GenerateImageAsync(prompt);
                
                 string base64Data;
                if (imageDataUrl.StartsWith("data:image/"))
                {
                    var commaIndex = imageDataUrl.IndexOf(",");
                    base64Data = imageDataUrl.Substring(commaIndex + 1);
                }
                else
                {
                    base64Data = imageDataUrl; // Direkt base64 ise
                }

                
                byte[] imageBytes = Convert.FromBase64String(base64Data);

                
                var fileName = $"service_image_{Guid.NewGuid().ToString("N").Substring(0, 8)}.jpg";
                var uploadsPath = Server.MapPath("~/Images/Services/");
                
                
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var filePath = Path.Combine(uploadsPath, fileName);
                
                
                System.IO.File.WriteAllBytes(filePath, imageBytes);

                 var imageUrl = $"/Images/Services/{fileName}";

                return Json(new { success = true, imageUrl = imageUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult CreateNewService(string Name, string Description, string ImageUrl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    TempData["Error"] = "Service name is required.";
                    return RedirectToAction(nameof(Index));
                }

                if (string.IsNullOrWhiteSpace(Description))
                {
                    TempData["Error"] = "Service description is required.";
                    return RedirectToAction(nameof(Index));
                }

                
                var newService = new Tbl_Services
                {
                    Name = Name.Trim(),
                    Description = Description.Trim(),
                    ImageUrl = !string.IsNullOrWhiteSpace(ImageUrl) ? ImageUrl.Trim() : null
                };

                
                _servicesManager.TInsert(newService);

                // Başarı mesajı
                TempData["Success"] = $"Service '{Name}' has been created successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Hata durumunda
                TempData["Error"] = $"An error occurred while creating the service: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

      

        // AI FAQ Generation endpoints
        [HttpPost]
        public async Task<JsonResult> GenerateRandomFAQ()
        {
            try
            {
                var result = await _aiFAQGenerationManager.GenerateRandomInsuranceFAQAsync();
                string topic = result.Item1;
                string question = result.Item2;
                string answer = result.Item3;
                
                return Json(new 
                { 
                    success = true, 
                    topic = topic,
                    question = question,
                    answer = answer
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GenerateCompleteQA(string topic)
        {
            try
            {
                var result = await _aiFAQGenerationManager.GenerateCompleteFAQAsync(topic);
                string question = result.Item1;
                string answer = result.Item2;
                
                return Json(new 
                { 
                    success = true, 
                    question = question,
                    answer = answer
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        
       

        [HttpPost]
        public ActionResult CreateNewFAQ(string QuestionTitle, string AnswerContent)
        {
            try
            {
                // Validasyon kontrolleri
                if (string.IsNullOrWhiteSpace(QuestionTitle))
                {
                    TempData["Error"] = "FAQ question is required.";
                    return RedirectToAction(nameof(Index));
                }

                if (string.IsNullOrWhiteSpace(AnswerContent))
                {
                    TempData["Error"] = "FAQ answer is required.";
                    return RedirectToAction(nameof(Index));
                }

                
                var newFAQ = new Tbl_FAQ
                {
                    QuestionTitle = QuestionTitle.Trim(),
                    AnswerContent = AnswerContent.Trim()
                };

               
                _faqManager.TInsert(newFAQ);

                
                TempData["Success"] = $"FAQ has been created successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                TempData["Error"] = $"An error occurred while creating the FAQ: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
        
      

       

    }
}

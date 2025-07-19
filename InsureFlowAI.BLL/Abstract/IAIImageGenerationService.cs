using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsureFlowAI.BLL.Abstract
{
    public interface IAIImageGenerationService
    {
        Task<string> GenerateImageAsync(string prompt);
        Task<string> GenerateImageAsync(string prompt, string style = "realistic");
        Task<bool> IsImageUrlValid(string imageUrl);
    }
}

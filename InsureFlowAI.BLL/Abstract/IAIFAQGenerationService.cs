using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsureFlowAI.BLL.Abstract
{
    public interface IAIFAQGenerationService
    {
        Task<string> GenerateFAQQuestionAsync(string topic);
        Task<string> GenerateFAQAnswerAsync(string question, string context = null);
        Task<(string Question, string Answer)> GenerateCompleteFAQAsync(string topic);
        Task<(string Question, string Answer)> GenerateCompleteQAAsync(string topic);
        Task<(string Topic, string Question, string Answer)> GenerateRandomInsuranceFAQAsync();
        Task<string> ImproveQuestionAsync(string question);
        Task<string> ImproveAnswerAsync(string answer);
    }
}

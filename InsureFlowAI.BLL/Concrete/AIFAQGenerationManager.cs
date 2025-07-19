using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using InsureFlowAI.BLL.Abstract;

namespace InsureFlowAI.BLL.Concrete
{
    public class AIFAQGenerationManager : IAIFAQGenerationService
    {
        private readonly HttpClient _httpClient;
        
        private readonly string _baseUrl = "https://chatgpt-42.p.rapidapi.com/conversationgpt4";

        public AIFAQGenerationManager()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateFAQQuestionAsync(string topic)
        {
            var prompt = $"Generate a professional FAQ question about {topic} for an insurance company. Return only the question.";
            return await CallChatGPTAPIAsync(prompt);
        }

        public async Task<string> GenerateFAQAnswerAsync(string question, string context = null)
        {
            var prompt = $"Generate a professional answer for this insurance FAQ: '{question}'. Return only the answer.";
            return await CallChatGPTAPIAsync(prompt);
        }

        public async Task<(string Question, string Answer)> GenerateCompleteFAQAsync(string topic)
        {
            var prompt = $@"Generate a complete FAQ about {topic} for an insurance company.
Format: 
Question: [question here]
Answer: [answer here]";
            
            var response = await CallChatGPTAPIAsync(prompt);
            return ParseQA(response);
        }

        public async Task<(string Question, string Answer)> GenerateCompleteQAAsync(string topic)
        {
            return await GenerateCompleteFAQAsync(topic);
        }

        public async Task<(string Topic, string Question, string Answer)> GenerateRandomInsuranceFAQAsync()
        {
            var prompt = @"Generate a random insurance FAQ. Pick any insurance topic.
Format:
Topic: [topic]
Question: [question]
Answer: [answer]";
            
            var response = await CallChatGPTAPIAsync(prompt);
            return ParseTQA(response);
        }

        public async Task<string> ImproveQuestionAsync(string question)
        {
            var prompt = $"Improve this insurance FAQ question: '{question}'. Return only the improved question.";
            return await CallChatGPTAPIAsync(prompt);
        }

        public async Task<string> ImproveAnswerAsync(string answer)
        {
            var prompt = $"Improve this insurance FAQ answer: '{answer}'. Return only the improved answer.";
            return await CallChatGPTAPIAsync(prompt);
        }

        private async Task<string> CallChatGPTAPIAsync(string prompt)
        {
            try
            {
                var requestBody = new
                {
                    messages = new[]
                    {
                        new { role = "user", content = prompt }
                    },
                    system_prompt = "You are an insurance expert. Always respond clearly and professionally.",
                    temperature = 0.9,
                    top_k = 5,
                    top_p = 0.9,
                    max_tokens = 256,
                    web_access = false
                };

                var json = JsonConvert.SerializeObject(requestBody);
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_baseUrl),
                    Headers =
                    {
                        { "x-rapidapi-key", _rapidApiKey },
                        { "x-rapidapi-host", "chatgpt-42.p.rapidapi.com" },
                    },
                    Content = new StringContent(json)
                    {
                        Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                    }
                };

                using (var response = await _httpClient.SendAsync(request))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        return apiResponse?.result?.ToString()?.Trim();
                    }
                    return "Error generating content.";
                }
            }
            catch
            {
                return "Error generating content.";
            }
        }

        private (string Question, string Answer) ParseQA(string response)
        {
            if (string.IsNullOrEmpty(response)) return ("Sample Question?", "Sample Answer.");
            
            var lines = response.Split('\n');
            string question = "", answer = "";
            
            foreach (var line in lines)
            {
                if (line.StartsWith("Question:", StringComparison.OrdinalIgnoreCase))
                    question = line.Substring(9).Trim();
                else if (line.StartsWith("Answer:", StringComparison.OrdinalIgnoreCase))
                    answer = line.Substring(7).Trim();
            }
            
            return (string.IsNullOrEmpty(question) ? "Sample Question?" : question, 
                    string.IsNullOrEmpty(answer) ? "Sample Answer." : answer);
        }

        private (string Topic, string Question, string Answer) ParseTQA(string response)
        {
            if (string.IsNullOrEmpty(response)) return ("Car Insurance", "What is car insurance?", "Car insurance protects your vehicle.");
            
            var lines = response.Split('\n');
            string topic = "", question = "", answer = "";
            
            foreach (var line in lines)
            {
                if (line.StartsWith("Topic:", StringComparison.OrdinalIgnoreCase))
                    topic = line.Substring(6).Trim();
                else if (line.StartsWith("Question:", StringComparison.OrdinalIgnoreCase))
                    question = line.Substring(9).Trim();
                else if (line.StartsWith("Answer:", StringComparison.OrdinalIgnoreCase))
                    answer = line.Substring(7).Trim();
            }
            
            return (string.IsNullOrEmpty(topic) ? "Car Insurance" : topic,
                    string.IsNullOrEmpty(question) ? "What is car insurance?" : question,
                    string.IsNullOrEmpty(answer) ? "Car insurance protects your vehicle." : answer);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Malikinden.Models;

namespace Malikinden.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ChatBotController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        [HttpPost]
        public async Task<IActionResult> GetResponse([FromBody] string message)
        {
            try
            {
                var apiKey = _configuration["OpenAI:ApiKey"];
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                var messages = new[]
                {
                    new { role = "system", content = "Sen bir araç uzmanısın. Kullanıcılara araçlar hakkında bilgi veriyorsun. Türkçe yanıt ver." },
                    new { role = "user", content = message }
                };

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = messages,
                    temperature = 0.7
                };

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<JsonElement>(responseBody);
                    var assistantMessage = responseObject.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                    return Content(assistantMessage);
                }
                else
                {
                    return Content("Üzgünüm, bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }
            catch (Exception)
            {
                return Content("Üzgünüm, bir hata oluştu. Lütfen tekrar deneyin.");
            }
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
        public List<Message> History { get; set; }
    }

    public class OpenAIResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string content { get; set; }
    }
} 
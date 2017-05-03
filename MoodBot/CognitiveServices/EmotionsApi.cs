using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MoodBot.CognitiveServices
{
    public static class EmotionsApi
    {
        public static async Task<HttpResponseMessage> GetMoodForImage(byte[] imageData)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "XXXXXXXXXXXXXXXXXXX");
                using (var content = new ByteArrayContent(imageData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    return await client.PostAsync("https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize", content);
                }
            }
        }
        public static async Task<HttpResponseMessage> GetMoodForText(string text)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "XXXXXXXXXXXXXXXXXXX");
                dynamic data = new {documents = new[] {new { language = "en", id = 1, text = text }}};
                using (var sw = new StringWriter())
                {
                    Newtonsoft.Json.JsonSerializer.Create().Serialize(sw, data);
                    using (var content = new ByteArrayContent(Encoding.UTF8.GetBytes(sw.ToString())))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        return await client.PostAsync("https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment", content);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "XXXXXXXXXXXXXXXXXXXXXXX");
                using (var content = new ByteArrayContent(imageData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    return await client.PostAsync("https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize", content);
                }
            }
        }
    }
}
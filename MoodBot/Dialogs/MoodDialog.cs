using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using MoodBot.CognitiveServices;

namespace MoodBot.Dialogs
{
    [Serializable]
    public class MoodDialog : IDialog<Object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if ( message.Attachments!=null && message.Attachments.Any())
            {
                var imageData = new WebClient().DownloadData(message.Attachments[0].ContentUrl);
                var emotions = await EmotionsApi.GetMoodForImage(imageData);
                var json = await emotions.Content.ReadAsStringAsync();

                await context.PostAsync(json);
            }
            if (!string.IsNullOrWhiteSpace(message.Text))
            {
                var sentiment = await EmotionsApi.GetMoodForText(message.Text);
                var json = await sentiment.Content.ReadAsStringAsync();

                await context.PostAsync(json);
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}
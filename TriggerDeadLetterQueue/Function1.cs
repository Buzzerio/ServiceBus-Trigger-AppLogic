using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TriggerDeadLetterQueue
{
    public static class Function1
    {
        [FunctionName("FunctionDeadLetterQueue")]
        public static async Task Run([TimerTrigger("*/3 * * * * *")]TimerInfo myTimer, ILogger log)
        {

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                //Create a MessageFactory object to access the service bus where the messages are transferred
                
                MessageReceiver receiver = new MessageReceiver("Endpoint=sb://bustestdomenico.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=v8+8wIIxOYE9QA07Xd0U494K8hdC9D3wLS5GSaKK/gM=;TransportType=AmqpWebSockets", "testqueue/$deadletterqueue", ReceiveMode.PeekLock);
                var msg = receiver.ReceiveAsync().GetAwaiter().GetResult();
                log.LogInformation($"C# Message ID: {msg.MessageId}");
                receiver.CompleteAsync(msg.SystemProperties.LockToken).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                log.LogInformation($"Exception: {ex}");
            }
        }
    
    }
}

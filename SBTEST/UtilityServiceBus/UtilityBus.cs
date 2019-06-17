using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBTEST.Model;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace SBTEST.UtilityServiceBus
{
    public static class UtilityBus<T>
    {
        //public const string ServiceBusConnectionString = "Endpoint=sb://bustestdomenico.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=v8+8wIIxOYE9QA07Xd0U494K8hdC9D3wLS5GSaKK/gM=";
        //const string QueueName = "testqueue";

        public static NamespaceManager spaceManager { get; set; }

        static UtilityBus()
        {
   
            spaceManager = NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"]);
            if (!spaceManager.QueueExists(ConfigurationManager.AppSettings["QueueName"]))
            {
                spaceManager.CreateQueue(ConfigurationManager.AppSettings["QueueName"]);
            }
        }

        public static async Task SendMessagesAsync(List<T> daInviare)
        {
            

            QueueClient client = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"], ConfigurationManager.AppSettings["QueueName"]);
            try
            {
                foreach (var x in daInviare)
                {
                    var message = new BrokeredMessage(x);
                    Console.WriteLine($"Sending message: {x.GetType()} , objInviare.campo: {x.ToString()}");
                    await client.SendAsync(message);
                    
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
                await client.CloseAsync();
            }
            await client.CloseAsync();

        }

        public static async Task SendMessagesAsyncJ(List<T> daInviare)
        {


            QueueClient client = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"], ConfigurationManager.AppSettings["QueueName"]);
            try
            {
                foreach (var x in daInviare)
                {
                    var json = JsonConvert.SerializeObject(x);
                    var message = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(json)));
                    message.ContentType = "application/json";
                    Console.WriteLine($"Sending message: {x.GetType()} , objInviare.campo: {x.ToString()}");
                    await client.SendAsync(message);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
                await client.CloseAsync();
            }
            await client.CloseAsync();

        }
    }
}

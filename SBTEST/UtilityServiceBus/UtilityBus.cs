using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace SBTEST.UtilityServiceBus
{
    public static class UtilityBus<T>
    {
        
        public static NamespaceManager spaceManager { get; set; }

        static UtilityBus()
        {
   
            spaceManager = NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"]);
            if (!spaceManager.QueueExists(ConfigurationManager.AppSettings["QueueName"]))
            {
                spaceManager.CreateQueue(ConfigurationManager.AppSettings["QueueName"]);
            }
        }

        /**
         *  Metodo per inviare degli oggetti Al service Bus: gli elementi della lista non vengono parsizzati con Json ma vengono 
             direttamente trasformati nel Brokered Message
         * */
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

        /**
         *  Metodo per inviare degli oggetti Al service Bus: gli elementi della lista vengono parsizzati con Json in modo che la Logic App
         *  riesce a leggerli
         * */
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

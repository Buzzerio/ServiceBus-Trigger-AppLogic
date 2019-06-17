namespace SBTEST
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using SBTEST.Model;
    using SBTEST.UtilityServiceBus;

    class Program
    {
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
    
    

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {


            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");
            List<Example> inviare = new List<Example>();
            Console.ReadKey();
            for (var i = 0; i < 10; i++)
            {
                Example aux = new Example
                {
                    campo = "intero",
                    valore = i

                };
                inviare.Add(aux);
            }
            //await UtilityBus<Example>.SendMessagesAsync(inviare);
            await UtilityBus<Example>.SendMessagesAsyncJ(inviare);
            Console.WriteLine("======================================================");
            Console.WriteLine("Test DELETE , PRESS ANY KEY");
            Console.WriteLine("======================================================");
            Console.ReadKey();
            List<Example> invioDelete = new List<Example>()
            {
                new Example
                {
                    campo = "Delete",
                    valore = 2
                }
            };
            await UtilityBus<Example>.SendMessagesAsyncJ(invioDelete);
            //Send Messages

            Console.WriteLine("======================================================");
            Console.WriteLine("Esecuzione Delete terminata");
            Console.WriteLine("======================================================");
            Console.ReadKey();
            List<Example> invioUpdate = new List<Example>()
            {
                new Example
                {
                    campo = "Update",
                    valore = 6
                }
            };
            await UtilityBus<Example>.SendMessagesAsyncJ(invioUpdate);
            //Send Messages

            Console.WriteLine("======================================================");
            Console.WriteLine("Esecuzione Update terminata");
            Console.WriteLine("======================================================");
            Console.ReadKey();


        }



        //    private static async Task RetrieveMessageAsync()
        //    {
        //        client = QueueClient.CreateFromConnectionString(ServiceBusConnectionString, QueueName);
        //        if (!client.IsClosed)
        //        {
        //            Console.WriteLine("è aperto");
        //        }
        //        else
        //        {
        //            Console.WriteLine("è chiuso");
        //        }

        //        client.OnMessage(message =>
        //        {
        //            Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
        //            var a = message.GetBody<Example>();
        //            Console.WriteLine(String.Format("Message valore: {0}", a.valore));
        //            Console.WriteLine(String.Format("Message campo: {0}", a.campo));

        //        });

        //        //BrokeredMessage lista = await client.ReceiveAsync();
        //        //Console.WriteLine(lista.GetBody<Example>().valore);
        //    }

        //    static async Task SendMessagesAsync(int numberOfMessagesToSend)
        //    {
        //        try
        //        {
        //            for (var i = 0; i < numberOfMessagesToSend; i++)
        //            {
        //                // Create a new message to send to the queue
        //                Example objInviare = new Example { campo = "intero", valore = i };
        //                //var message = new Message(Encoding.UTF8.GetBytes(messageBody));
        //                //var message = new BrokeredMessage(Encoding.UTF8.GetBytes(messageBody));
        //                var message = new BrokeredMessage(objInviare);

        //                // Write the body of the message to the console
        //                Console.WriteLine($"Sending message: {objInviare.valore} , objInviare.campo: {objInviare.campo}");

        //                // Send the message to the queue
        //                await client.SendAsync(message);
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
        //        }
        //    }
    }
}

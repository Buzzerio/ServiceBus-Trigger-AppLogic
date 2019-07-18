
using FrameworkTest;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceBus.Messaging;
using SBTEST.Model;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;


namespace TriggerServiceBus
{
    public static class Function1
    {

        /**
         * Trigger che viene pubblicato su Azure com FunctionApp
         * Si crea un ServiceBusTrigger che legge sulla coda ServiceBus di Azure, con diritti di accesso Manage
         * il trigger legge già il BrokeredMessage e lo "parsa direttamente"
         * ILogger viene collegato a un Application Insights collegato
         * Con TelemetryCLient si crea manualmente "uno strumento di misurazione" per Application Insights
         * */

        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("testqueue", AccessRights.Manage, Connection = "AzureWebJobsServiceBus")]Example myQueueItem, ILogger log)
        {
            //TelemetryClient telemetry = new TelemetryClient();
            //telemetry.InstrumentationKey = System.Environment.GetEnvironmentVariable("APP_INSIGHTS_KEY");
            //telemetry.InstrumentationKey = "9dd68603-24f1-4b90-99ae-59b142d22794";
            var sw = Stopwatch.StartNew();
            var time = DateTime.Now;
            if (myQueueItem.campo.Equals("intero"))
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem.valore}");
                using (var db = new TestTriggerContext())
                {

                    //Test_Trigger ins = new Test_Trigger()
                    //{
                    //    campo = myQueueItem.campo,
                    //    valore = myQueueItem.valore
                    //};
                    //db.Test_Trigger.Add(ins);
                    //int result = db.SaveChanges();
                    //log.LogInformation($"C# Operazione Insert tramite EFdel valore : {myQueueItem.valore} , completata con esito : {1}");
                    //telemetry.TrackRequest("Insert EF", time, sw.Elapsed, "200", true);
                    var campo = new SqlParameter("@campo", myQueueItem.campo);
                    var valore = new SqlParameter("@valore", myQueueItem.valore);
                    string query = "EXECUTE [dbo].[Test_Insert_Test_Trigger] @campo, @valore";

                    try
                    {
                        db.Database.SqlQuery<Test_Trigger>(query, campo, valore).SingleOrDefault();
                    }
                    catch { }
                    log.LogInformation($"C# Operazione Insert tramite Stored Procedure del valore : {myQueueItem.valore} , completata con esito : {1}");

                    //telemetry.TrackRequest("Insert SP", time, sw.Elapsed, "200", true);
                }
            }
            else if (myQueueItem.campo.Equals("Update")) {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem.campo}");
                using (var db = new TestTriggerContext())
                {
                   
                    db.Test_Trigger.Where(x => x.valore.Value.Equals(myQueueItem.valore)).ToList().ForEach(c => c.campo = myQueueItem.campo);
                    var result = db.SaveChanges();
                    log.LogInformation($"C# Operazione Update tramite LINQ & EntityFramework : {myQueueItem.valore} , completata con esito : {result}");
                    //telemetry.TrackRequest("Update SP", time, sw.Elapsed, "200", true);
                }
            }
            else
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem.campo}");
                using (var db = new TestTriggerContext())
                {
                    var a = from aux in db.Test_Trigger
                            where aux.campo.StartsWith("Test")
                            select aux;

                    db.Test_Trigger.RemoveRange(a);
                    var result = db.SaveChanges();
                    log.LogInformation($"C# Operazione Delete tramite LINQ & EntityFramework : {myQueueItem.valore} , completata con esito : {result}");
                    //telemetry.TrackRequest("Delete SP", time, sw.Elapsed, "200", true);
                }



            }

        }
    }
}

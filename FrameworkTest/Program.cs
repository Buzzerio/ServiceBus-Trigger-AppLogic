using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            using (TestTriggerContext db = new TestTriggerContext())
            {
                var campo = new SqlParameter("@campo", "TestDaVS8");
                var valore = new SqlParameter("@valore", 34);
                var RC = new SqlParameter("@RC", 0);
                string query = "EXECUTE [dbo].[Test_Insert_Test_Trigger] @campo, @valore";

                try
                {
                    db.Database.SqlQuery<Test_Trigger>(query, campo, valore).SingleOrDefault();
                }
                catch { }
               
              

                
                //Test_Trigger ins = new Test_Trigger()
                //{
                //    campo = "intero",
                //    valore = 11
                //};

                //db.Test_Trigger.Add(ins);
                //db.SaveChanges();
            }
            

            Console.ReadKey();
        }
    }
}

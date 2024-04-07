using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace FlashCardProject
{
    public class FlashCardController
    {
        public static string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        //Console.WriteLine(connectionString);
        internal List<Stack> GetStacks()
        {
            List<Stack> stackTable = new List<Stack>();
            string query = "select * from Stacks";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query,connection))
            {
                connection.Open();


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) { }
                }



                var table = new Table();
                table.AddColumns("Id", "Name");

                //foreach (Stack stack in stacks)
                //{

                //}



                //foreach (CodingSession rows in query)
                //{
                //    //fields = rows as IDictionary<string, object>;

                //    table.AddRow(rows.Id.ToString(), rows.Duration ?? "", rows.StartTime.ToString(), rows.EndTime.ToString());

                //}

                //table.Border = TableBorder.MinimalDoubleHead;

                //table.Centered();

                //// Render the table to the console
                //AnsiConsole.Write(table);

            }


            return stackTable;
        }

    }
}

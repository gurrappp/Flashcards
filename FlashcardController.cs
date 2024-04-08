using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
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

                List<Stack> stacks = new List<Stack>();

                //var objects = command.ExecuteQuery<Stack>(query).ToList();

                



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

        public List<T> getObjects<T>(IDbConnection connection, string tableName, params string[] columnNames)
        {
            string query = $@"SELECT {String.Join(",", columnNames)} FROM {tableName}";
            using (var dc = new DataContext(connection))
            {
                return dc.ExecuteQuery<T>(query).ToList();
            }

        }

    }
}

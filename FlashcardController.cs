using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
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
            //string query = "select * from Stacks";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                List<Stack> stacks = new List<Stack>();

                string sql = "select * from Stacks";
                IEnumerable<dynamic> query = connection.Query<Stack>(sql);



                var table = new Table();
                table.AddColumns("Id", "Name");

                foreach (Stack rows in query)
                {
                    table.AddRow(rows.Id.ToString(), rows.Name ?? "");
                }

                table.Border = TableBorder.MinimalDoubleHead;

                table.Centered();

                // Render the table to the console
                AnsiConsole.Write(table);
            }


            return stackTable;
        }

        //public List<T> getObjects<T>(IDbConnection connection, string tableName, params string[] columnNames)
        //{
        //    string query = $@"SELECT {String.Join(",", columnNames)} FROM {tableName}";
        //    using (var dc = new DataContext(connection))
        //    {
        //        return dc.ExecuteQuery<T>(query).ToList();
        //    }

        //}

    }
}

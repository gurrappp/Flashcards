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
        internal void GetStacks()
        {
            
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
        }

        internal bool ValidateStackName(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                List<Stack> stacks = new List<Stack>();
                //var sql = "SELECT * FROM users WHERE username = '" + username + "' AND password = '" + password + "'";
                string sql = "select * from Stacks WHERE name = '" + name + "'";
                IEnumerable<dynamic> query = connection.Query<Stack>(sql);

                return query.Any();

            }
        }

        internal void GetFlashCards()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                List<FlashCard> flashCards = new List<FlashCard>();

                string sql = "select * from FlashCards";
                IEnumerable<dynamic> query = connection.Query<FlashCard>(sql);



                var table = new Table();
                table.AddColumns("Id", "StackId","Question", "Answer");

                foreach (FlashCard rows in query)
                {
                    table.AddRow(rows.Id.ToString(), rows.StackId.ToString(),rows.Question, rows.Answer);
                }

                table.Border = TableBorder.MinimalDoubleHead;

                table.Centered();

                // Render the table to the console
                AnsiConsole.Write(table);
            }
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

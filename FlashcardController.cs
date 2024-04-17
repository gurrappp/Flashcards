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
                
                string sql = "select * from Stacks WHERE name = '" + name + "'";
                IEnumerable<dynamic> query = connection.Query<Stack>(sql);

                return query.Any();

            }
        }

        internal void GetFlashCards(string? stackName = null, int? nrOfRows = null )
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql;
                List<FlashCard> flashCards = new List<FlashCard>();

                if(string.IsNullOrWhiteSpace(stackName) && !nrOfRows.HasValue) 
                    sql = "select * from FlashCards";
                else if (!string.IsNullOrWhiteSpace(stackName) && !nrOfRows.HasValue)
                    sql = "select * from FlashCards WHERE StackName = '" + stackName + "'";
                else if (!string.IsNullOrWhiteSpace(stackName) && nrOfRows.HasValue)
                    sql = $"select TOP({nrOfRows}) * from FlashCards";
                else
                    sql = $"select TOP({nrOfRows}) * from FlashCards WHERE StackName = '" + stackName + "'";



                IEnumerable<dynamic> query = connection.Query<FlashCard>(sql);



                var table = new Table();
                table.AddColumns("Id", "StackId","StackName","Question", "Answer");

                foreach (FlashCard rows in query)
                {
                    table.AddRow(rows.Id.ToString(), rows.StackId.ToString(), rows.StackName, rows.Question, rows.Answer);
                }

                table.Border = TableBorder.MinimalDoubleHead;

                table.Centered();

                // Render the table to the console
                AnsiConsole.Write(table);
            }
        }

        internal void CreateFlashCard(string stackName)
        {
            if (string.IsNullOrWhiteSpace(stackName))
                return;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var stackSql = "select * from stacks where name = '" + stackName + "'";
                
                var stack = connection.QuerySingle(stackSql);

                var sql = "INSERT INTO FlashCards (Id, StackId, StackName, Question, Answer) VALUES (@Id, @StackId, @StackName, @Question, @Answer)";
                var newFlashCard = new
                {
                    Id = GetNextFlashCardId(stackName), 
                    StackId = stack.Id,
                    StackName = stackName,
                    Question = string.Empty,
                    Answer = string.Empty
                };
                
                connection.Execute(sql, newFlashCard);
            }
        }

        public int GetNextFlashCardId(string stackName)
        {
            int maxId = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var sql = "select * from FlashCards WHERE StackName = '" + stackName + "'";

                IEnumerable<dynamic> query = connection.Query<FlashCard>(sql);

                var test = query.ToList().MaxBy(x => x.Id);
                if (test is null)
                    return 0;

                maxId = test.Id;
            }

            return maxId + 1;
        }

        public void EditFlashCard(string stackName, int id, string question, string answer)
        {
            //if (string.IsNullOrWhiteSpace(stackName))
            //    return;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //sql = $"insert into FlashCards values ({GetNextFlashCardId(stackName)}, {stack.Id}, '" + stackName + $"', {string.Empty}, {string.Empty} )" ;
                var sql = "update flashcards set question = '" + question +"' , answer = '" + answer + $"' WHERE id = {id}";

                connection.Execute(sql);

            }
        }

        public void DeleteFlashCard(string stackName, int id)
        {
            //if (string.IsNullOrWhiteSpace(stackName))
            //    return;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = $"delete from flashcards WHERE id = {id}";

                connection.Execute(sql);

            }
        }

        public List<FlashCard> GetFlashCardsForStudySession(string stackName)
        {
            var flashCards = new List<FlashCard>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "select * from FlashCards WHERE StackName = '" + stackName + "'";

                flashCards = connection.Query<FlashCard>(sql).ToList();
            }

            return flashCards;
        }
    }
}

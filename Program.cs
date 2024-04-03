
using System.Configuration;

namespace FlashCardProject
{
    public class Program
    {
        public static string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        public static void Main(string[] args)
        {
            
            Console.WriteLine(connectionString);
            GetUserInput getUserInput = new();
            
            getUserInput.Menu();
        }
    }
}
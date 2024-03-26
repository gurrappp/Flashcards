
using System.Configuration;

namespace FlashcardProject
{
    public class Program
    {
        public string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        public static void Main(string[] args)
        {

        }
    }
}
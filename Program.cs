
using System.Configuration;

namespace FlashCardProject
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            
            
            GetUserInput getUserInput = new();
            
            getUserInput.Menu();
        }
    }
}
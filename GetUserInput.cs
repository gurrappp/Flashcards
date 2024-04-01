using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardProject
{
    internal class GetUserInput
    {

        public void Menu()
        {
            bool closeApp = false;

            while (!closeApp)
            {
                Console.WriteLine("Welcome to Flashcards!\n");
                Console.WriteLine("------------MENU------------");


                var option = Console.ReadLine();

                if (!int.TryParse(option, out _))
                    return;

                switch (int.Parse(option))
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    default:
                        break;

                }
            }
        }
    }
}

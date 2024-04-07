using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardProject
{
    internal class GetUserInput
    {

        FlashCardController controller = new();

        public void Menu()
        {
            bool closeApp = false;

            while (!closeApp)
            {
                Console.WriteLine("Welcome to Flashcards!\n");
                Console.WriteLine("------------MENU------------");
                Console.WriteLine("0 - exit");
                Console.WriteLine("1 - Manage Stacks");
                Console.WriteLine("1 - Manage FlashCards");



                var option = Console.ReadLine();

                if (!int.TryParse(option, out _))
                    return;

                switch (int.Parse(option))
                {
                    case 0:
                        break;
                    case 1:
                        ManageStacks();
                        break;
                    default:
                        break;

                }
            }
        }

        public void ManageStacks()
        {
            controller.GetStacks();
        }
    }
}

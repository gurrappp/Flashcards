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
                Console.WriteLine("2 - Manage FlashCards");



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
                    case 2:
                        ManageFlashCards();
                        break;
                    default:
                        break;

                }
            }
        }
        
        public string GetStackName()
        {
            Console.WriteLine("Choose a stack to work with. Write Name!\n");
            Console.WriteLine("Any number - exit");

            var option = Console.ReadLine();

            if (int.TryParse(option, out _))
                Menu();

            return option ?? "";

        }

        public void ManageStacks()
        {
            Console.Clear();
            controller.GetStacks();
            string currentStack = GetStackName();
            if (!controller.ValidateStackName(currentStack))
                Menu();

            Console.ReadLine();

            
        }

        public void ManageFlashCards()
        {
            controller.GetFlashCards();
        }
    }
}

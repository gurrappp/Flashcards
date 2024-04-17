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
                Console.Clear();
                Console.WriteLine("Welcome to Flashcards!\n");
                Console.WriteLine("------------MENU------------");
                Console.WriteLine("0 - exit");
                Console.WriteLine("1 - Manage Stacks");
                Console.WriteLine("2 - Manage FlashCards");
                Console.WriteLine("3 - Study");
                Console.WriteLine("4 - View Study sessions data");

                var option = Console.ReadLine();

                if (!int.TryParse(option, out var parsedOption))
                    return;

                switch (parsedOption)
                {
                    case 0:
                        closeApp = true;
                        break;
                    case 1:
                        ManageStacks();
                        break;
                    case 2:
                        Console.Clear();
                        ManageFlashCards();
                        break;
                    case 3:
                        Console.Clear();
                        StartStudySession();
                        break;
                    default:
                        break;

                }
            }
        }
        
        public void ManageStacks()
        {
            Console.Clear();
            controller.GetStacks();
            string currentStack = GetStackName();
            if (!controller.ValidateStackName(currentStack))
                Menu();
            Console.Clear();
            StackMenu(currentStack);

            Console.ReadLine();

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

        public void StackMenu(string stackName)
        {
            while (true)
            {
                Console.WriteLine($"Current working stack:{stackName} \n");
                Console.WriteLine("0 - return to main menu");
                Console.WriteLine("1 - change working stack");
                Console.WriteLine("2 - view all FlashCards in stack");
                Console.WriteLine("3 - view X FlashCards in stack");
                Console.WriteLine("4 - Create FlashCard in stack");
                Console.WriteLine("5 - Edit a FlashCard in stack");
                Console.WriteLine("6 - Delete a FlashCard in stack");

                var option = Console.ReadLine();

                if (!int.TryParse(option, out _)) return;

                switch (int.Parse(option))
                {
                    case 0:
                        Menu();
                        break;
                    case 1:
                        ManageStacks();
                        break;
                    case 2:
                        controller.GetFlashCards(stackName);
                        continue;
                    case 3:
                        Console.WriteLine("How many rows?");
                        var rows = Console.ReadLine();
                        if (!int.TryParse(rows, out var intRows)) break;

                        controller.GetFlashCards(stackName, intRows);
                        break;
                    case 4:
                        controller.CreateFlashCard(stackName);
                        break;
                    case 5:
                        EditFlashCard(stackName);
                        break;
                    case 6:
                        DeleteFlashCard(stackName);
                        break;
                    default:
                        break;
                }
            }
        }

        public void EditFlashCard(string stackName)
        {
            controller.GetFlashCards(stackName);
            Console.WriteLine("enter id of flashcard to edit");
            var id = Console.ReadLine();

            if (!int.TryParse(id, out var flashCardId)) 
                return;

            Console.WriteLine("Enter new Question for flashcard");
            var question = Console.ReadLine();

            Console.WriteLine("Enter new Answer for flashcard");
            var answer = Console.ReadLine();

            controller.EditFlashCard(stackName,flashCardId, question ?? "", answer ?? "");
        }

        public void DeleteFlashCard(string stackName)
        {
            controller.GetFlashCards(stackName);
            Console.WriteLine("enter id of flashcard to delete");
            var id = Console.ReadLine();

            if (!int.TryParse(id, out var flashCardId))
                return;

            controller.DeleteFlashCard(stackName, flashCardId);
        }

        public void ManageFlashCards()
        {
            controller.GetFlashCards();
            Console.WriteLine("Press any input to go back to Menu");
            Console.ReadLine();
        }

        public void StartStudySession()
        {
            
            controller.GetStacks();

            Console.WriteLine("Choose a stack to Study. Write Name!\n");
            Console.WriteLine("Any number - exit");

            var option = Console.ReadLine();

            if (int.TryParse(option, out _))
                return;

            var flashCards = controller.GetFlashCardsForStudySession(option);

            int points = 0;
            foreach (var flashCard in flashCards)
            {
                Console.WriteLine($"{flashCard.Question}");
                var answer = Console.ReadLine();

                if (answer == flashCard.Answer)
                {
                    Console.WriteLine("Correct!");
                    points++;
                }
                else
                {
                    Console.WriteLine("Wrong!");
                    Console.WriteLine($"Correct answer is {flashCard.Answer}");
                }
            }

            Console.WriteLine($"You got {points} points.");

            StartStudySession();

        }
    }
}

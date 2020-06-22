using System;

namespace Reliquary.GameMenus
{
    class Game
    {
        public static int Choice(string[] choices)
        {
            Console.WriteLine("\nMake your choice:");
            for (int i = 0; i < choices.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("  " + (i + 1));
                Console.ResetColor();
                Console.WriteLine(") " + choices[i]);
            }
            bool AcceptableResponse = false;
            int ResponseInt = -1;
            while (!AcceptableResponse)
            {
                Console.Write(">");
                string Response = Console.ReadLine();
                if (Response.ToLower() == "q" || Response.ToLower() == "quit")
                {
                    Tx.Emphasis("If you want to quit, click the X for now.\n", "red");
                    // TODO: Make it possible to quit the game
                }
                for (int i = 0; i < choices.Length; i++)
                {
                    ResponseInt = i + 1;
                    if (ResponseInt.ToString() == Response)
                    {
                        AcceptableResponse = true;
                        break;
                    }
                }
            }
            return ResponseInt;
        }

        internal static void ShowLogo()
        {
            Console.Clear();
            Tx.Emphasis(@" __   ___         __             __      ", "gold");
            Console.WriteLine("");
            Tx.Emphasis(@"|__) |__  |    | /  \ |  |  /\  |__) \ / ", "gold");
            Console.WriteLine("");
            Tx.Emphasis(@"|  \ |___ |___ | \__X \__/ /——\ |  \  |  ", "gold");
            Console.WriteLine("\n");
            Tx.Emphasis("== Your daily dose of adventure! ==\n\n", "gold");
        }

        public static void LookAtSelf()
        {
            Console.Clear();
            Tx.Emphasis(Character.Gender + " " + Character.Name + "\n", "green");
            Console.WriteLine("Level " + Character.Level + " Adventurer\n");
            Console.Write("  Experience Points till level up: " + ((Character.Level * Character.Level) * 10 - Character.ExperiencePoints + "\n"));
            Console.Write("  Gold: ");
            Tx.Emphasis(Character.Gold, "gold");
            Console.Write("\n  Might: ");
            if (Character.Might > Character.MaxMight)
            {
                Tx.Emphasis("+" + Character.Might + "\n", "green");
            }
            else
            {
                Tx.Emphasis(Character.Might + "\n", "red");
                // Might say a status effect/condition next to it like SICK
            }
            Console.Write("  Will: ");
            if (Character.Wits >= Character.MaxWits)
            {
                Tx.Emphasis("+" + Character.Wits + "\n", "green");
            }
            else
            {
                Tx.Emphasis(Character.Wits + "\n", "red");
                // Might say a status effect/condition next to it like DRUNK or HUNGOVER
            }
            Console.Write("  Fitness: ");
            string Fitness = Character.ShowFitness();
            if (Fitness == "Healthy")
            {
                Tx.Emphasis(Fitness, "green");
            }
            else if (Fitness == "Bruised")
            {
                Tx.Emphasis(Fitness, "gold");
            }
            else if (Fitness == "Injured")
            {
                Tx.Emphasis(Fitness, "lightred");
            }
            else if (Fitness == "Heavily Wounded")
            {
                Tx.Emphasis(Fitness, "red");
            }
            Console.WriteLine("\n");
            Character.DisplayVentures();
        }

        public static void CheckInventory()
        {
            Console.Clear();
            Tx.Emphasis("Your Inventory\n", "green");
            if (Character.Inventory.Count == 0)
            {
                Tx.Emphasis("Empty!\n", "gray");
            }
            for (int item = 0; item < Character.Inventory.Count; item++)
            {
                Item CurrentItem = Character.Inventory[item];
                Console.Write("* ");
                if (CurrentItem.Stock > 1)
                {
                    Console.Write(CurrentItem.Stock + "x ");
                }
                Console.Write(Character.Inventory[item].Name);
                Tx.Emphasis(" - " + Character.Inventory[item].Description + "\n", "gray");
            }
            Console.WriteLine("");
            //Should be put into a loop with options like "Use," "Drop," and "Back"
        }
    }
}

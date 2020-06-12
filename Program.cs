/*
 App by Austin Ballard 
 */
using System;
using System.IO;

namespace Reliquary
{
    static class Game
    {
        public static void StartGame()
        {
            Tx.Emphasis("RELIQUARY - ©opyright 2020 Austin Ballard\n\n", "gray");
            Console.Write("Press any key to start.");
            Console.ReadKey();
            Console.Clear();
            ShowLogo();            
            if (GamesAreSaved("all"))
            {
                bool Success = false;
                string[] Options = { "New Game", "Continue", "Quit" };
                while (Success == false)
                {
                    int Answer = Choice(Options);
                    switch (Answer)
                    {
                        case 1:
                            CreateNewCharacter();
                            StartAdventure();
                            Success = true;
                            break;
                        case 2:
                            Console.Clear();
                            ShowLogo();
                            Console.Write("What is your name?\n>");
                            string SaveFileName = Console.ReadLine();
                            if (GamesAreSaved(SaveFileName))
                            {
                                LoadGame(SaveFileName);
                                StartAdventure();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Never heard of you. Move along.");
                            }
                            break;
                        case 3:
                            Console.Write("Fine! Press any key to leave.\n>");
                            Success = true;
                            break;
                    }
                }
            }
        }

        public static void ShowLogo()
        {
            Tx.Emphasis(@" __   ___         __             __      ", "yellow");
            Console.WriteLine("");
            Tx.Emphasis(@"|__) |__  |    | /  \ |  |  /\  |__) \ / ", "yellow");
            Console.WriteLine("");
            Tx.Emphasis(@"|  \ |___ |___ | \__X \__/ /--\ |  \  |  ", "yellow");
            Console.WriteLine("\n");
            Tx.Emphasis("== A daily dose of adventure! ==\n\n", "yellow");
        }

        public static void LoadGame(string game)
        {
            Tx.Emphasis("Loading game...\n", "gray");
            string file = "savedata/" + game + ".txt";
            StreamReader sr = new StreamReader(file);
            string line = null;
            int Ventures = 0;
            int VentureBonus = 0;
            string WelcomeMessage = "You wake up rested and ready for a new day of adventuring!";
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("Name:"))
                {
                    Character.Name = line.Substring(5);
                    Tx.Emphasis("Loaded name.\n", "gray");
                }
                else if (line.StartsWith("Gender:"))
                {
                    Character.Gender = line.Substring(7);
                    Tx.Emphasis("Loaded gender.\n", "gray");
                }
                else if (line.StartsWith("PlaceID:"))
                {
                    Character.CurrentLocation = Convert.ToInt32(line.Substring(8));
                    Tx.Emphasis("Loaded current location.\n", "gray");
                }
                else if (line.StartsWith("Item:"))
                {
                    Inventory.LoadItem(Convert.ToInt32(line.Substring(5)));
                }
                else if (line.StartsWith("Might:"))
                {
                    //There should be a handling function to check for a valid maximum number
                    Character.Might = Convert.ToInt32(line.Substring(6));
                    Tx.Emphasis("Loaded might stat.\n", "gray");
                }
                else if (line.StartsWith("Fitness:"))
                {
                    Character.Fitness = Convert.ToInt32(line.Substring(8));
                    Tx.Emphasis("Loaded fitness status.\n", "gray");
                }
                else if (line.StartsWith("Wits:"))
                {
                    //There should be a handling function to check for a valid maximum number
                    Character.Wits = Convert.ToInt32(line.Substring(5));
                    Tx.Emphasis("Loaded wits stat.\n", "gray");
                }
                else if (line.StartsWith("Experience:"))
                {
                    //There should be a handling function to check for a valid maximum number for the current level
                    Character.ExperiencePoints = Convert.ToInt32(line.Substring(11));
                    Tx.Emphasis("Loaded experience points.\n", "gray");
                }
                else if (line.StartsWith("Level:"))
                {
                    //There should be a handling function to check for a valid maximum number
                    Character.Level = Convert.ToInt32(line.Substring(6));
                    Tx.Emphasis("Loaded level.\n", "gray");
                }
                else if (line.StartsWith("SleepQuality:"))
                {
                    /*
                       This refers to the quality of inn or place you slept.
                       Maybe roll for a chance to get sick if you slept outside or in a barn
                       Get bonus ventures if you slept in a really nice inn suite
                       SleepQuality should be an integer from 1-6, with 1 being camping
                     */
                    Tx.Emphasis("Loaded sleep quality.\n", "gray");
                }
                else if (line.StartsWith("Ventures:"))
                {
                    Ventures = Convert.ToInt32(line.Substring(9));
                    Tx.Emphasis("Loaded ventures.\n", "gray");
                }
                else if (line.StartsWith("LastPlayed:"))
                {
                    DateTime LastLoggedIn = Convert.ToDateTime(line.Substring(11));
                    Tx.Emphasis("Loaded date of day last played.\n", "gray");
                    if (LastLoggedIn == DateTime.Today)
                    {
                        Character.Ventures = Ventures;
                        if (Ventures == 0)
                        {
                            WelcomeMessage = "You wake up exhausted. You still need a good night's sleep!";
                        }
                        else
                        {
                            WelcomeMessage = "You wake up. There's still time to do more adventuring today.";
                        }
                    }
                    else
                    {
                        /*
                          It's a new day! Ventures = 5 + Fitness*2 + Might + Wits + Level
                          And you get a "rest" bonus if you have ventures remaining from previous days
                          Or if you haven't played for a few days.
                          But if you wait too many days to play you get groggy. :D
                          You may also get a bonus to Ventures if you slept somewhere nice
                        */
                        int DaysSinceLastLogin = (LastLoggedIn - DateTime.Today).Days;
                        Tx.Emphasis("It has been " + DaysSinceLastLogin + " since you last played.", "red"); //DEBUG
                        switch (DaysSinceLastLogin)
                        {
                            case 2:
                            case 3:
                                VentureBonus = 2;
                                WelcomeMessage = "You wake up very refreshed! What a good rest!";
                                break;
                            case var _ when (DaysSinceLastLogin > 5):
                                VentureBonus = -1;
                                WelcomeMessage = "Wow, you slept for a long time. You feel a bit groggy.";
                                break;
                        }
                        if (Ventures > 0)
                        {
                            //If there are ventures left over from a previous day, you can keep up to 5 of them.
                            VentureBonus += Math.Max(5, Ventures);
                        }
                        Character.Ventures = 5 + (Character.Fitness * 2) + Character.Might + Character.Wits + Character.Level + VentureBonus;
                    }

                }
            }
            sr.Close();
            Console.Clear();
            Console.WriteLine("Welcome back, " + Character.Gender + " " + Character.Name + ".\n");
            Console.WriteLine(WelcomeMessage);
        }

        public static bool GamesAreSaved(string filename)
        {
            if (filename == "all")
            {
                string[] files = Directory.GetFiles("savedata/", "*.txt", SearchOption.TopDirectoryOnly);
                return (files.Length > 0);
            }
            else
            {
                string[] files = Directory.GetFiles("savedata/", filename + ".txt", SearchOption.TopDirectoryOnly);
                return (files.Length > 0);
            }
        }

        public static void SaveGame() //DO THIS NEXT!!
        {
            /*
             Should this save after every interaction, or only when you go to sleep, I wonder?
             Also, don't forget to save SleepQuality as an integer. I'm thinking on a scale of 1-6?
             */
                 
            //If for some stupid reason the player names themself "All," catch it and name it something else.
            string SavedData = "Name:" + Character.Name + "\n";
            SavedData += "Gender:" + Character.Gender + "\n";
            SavedData += "PlaceID:" + Character.CurrentLocation + "\n";
            SavedData += "Might:" + Character.Might + "\n";
            SavedData += "Fitness:" + Character.Fitness + "\n";
            SavedData += "Wits:" + Character.Wits + "\n";
            SavedData += "Experience:" + Character.ExperiencePoints + "\n";
            SavedData += "Level:" + Character.Level + "\n";
            //SavedData += "SleepQuality:" + SleepPlace + "\n"; //Should indicate what type of inn or sleep location        
            SavedData += "Ventures:" + Character.Ventures + "\n";
            SavedData += "LastPlayed:" + DateTime.Today + "\n";
            for (int i = 0; i < Character.Inventory.Count; i++)
            {
                SavedData += "Item:" + Character.Inventory[i].ID + "\n";
            }
            Console.WriteLine("SavedData");
            //File.WriteAllText("savedata.txt", SavedData);
        }

        static void CreateNewCharacter()
        {
            Console.Write("First things first: what's your name, friend?\n>");
            bool ValidName = false;
            bool LoadingGame = false;
            while (!ValidName)
            {
                string Name = Console.ReadLine();
                if (Name.Length == 1)
                {
                    Console.Write("That sounds like a nickname. What is it short for?\n>");
                    break;
                }
                if (GamesAreSaved(Name))
                {
                    Console.Write("Ah, you've been here before, haven't you?\n");
                    string[] Options2 = { "I misspoke.", "Yeah, that's me!" };
                    int LoadOrNew = Choice(Options2);
                    if (LoadOrNew == 1)
                    {
                        break;
                    }
                    if (LoadOrNew == 2)
                    {
                        ValidName = true;
                        LoadingGame = true;
                        LoadGame(Name);
                        break;
                    }
                }
            }
            if (!LoadingGame)
            {
                Character.Name = Console.ReadLine();
                for (int i = 1; i < Character.Name.Length; i++)
                    Character.Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Character.Name.ToLower());
                Console.WriteLine("Nice to meet you then, " + Character.Name + ".");
                Console.WriteLine("\nEh... does that mean you're a man or a woman?");
                string[] Options = { "Man", "Woman", "Nonbinary" };
                int Answer = Choice(Options);
                switch (Answer)
                {
                    case 3:
                        Console.WriteLine("\n-_- ... Okay fine, what was your BIOLOGICAL SEX at birth?");
                        Options = new string[] { "Male", "Female" };
                        Answer = Choice(Options);
                        break;
                    case 1:
                        Character.Gender = "Sir";
                        Console.WriteLine("\nThank you, Sir " + Character.Name + ". Your adventure awaits you.");
                        break;
                    case 2:
                        Character.Gender = "Lady";
                        Console.WriteLine("\nThank you, Lady " + Character.Name + ". Your adventure awaits you.");
                        break;
                }
                Initialize();
                Tx.Emphasis("Press any key to start your adventure!\n", "cyan");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void StartAdventure()
        {
            Character.LastPlayed = DateTime.Today.ToString();
            Tx.Emphasis(Tx.GetGameDate(DateTime.Today.ToString(), "full") + "\n", "gray");
            //This should analyze the place, not just Merrydale right off the bat.
            Tx.Emphasis("Merrydale Township\n", "cyan");
            Console.WriteLine("The cobblestones under your feet vibrate with the sounds of peasant life around you.");
            Console.WriteLine("Villagers go to and fro, tending to their daily duties and paying you little heed.");
            string[] Options = { "Self", "Inventory", "Travel", "Save Game" };
            int Answer = Choice(Options);
            switch (Answer)
            {
                case 1:
                    LookAtSelf();
                    StartAdventure();
                    break;
                case 2:
                    CheckInventory();
                    StartAdventure();
                    break;
                case 3:
                    Console.WriteLine("Naw, let's do something else.");
                    break;
                case 4:
                    SaveGame();
                    break;
            }
        }

        static void Initialize()
        {
            Inventory.AddSignetRing();
        }

        public static int Choice(string[] choices)
        /*
         Format a choice like this:
        string[] Options = { "Pet a goat", "Make a stew", "'I'm selling these fine leather jackets...'" };
        int Answer = Choice(Options);
        switch (Answer) {
            case 1: ...
        }
     */
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

        static void LookAtSelf()
        {
            Console.Clear();
            Tx.Emphasis(Character.Gender + " " + Character.Name + "\n", "cyan");
            Console.Write("Level " + Character.Level + " Adventurer");
            Console.WriteLine("Level " + Character.Level + " Adventurer");
            Console.Write("  Might: ");
            if (Character.Might > 0)
            {
                Tx.Emphasis("+" + Character.Might + "\n", "green");
            }
            else
            {
                Tx.Emphasis(Character.Might + "\n", "red");
            }
            Console.Write("  Will: ");
            if (Character.Wits > 0)
            {
                Tx.Emphasis("+" + Character.Wits + "\n", "green");
            }
            else
            {
                Tx.Emphasis(Character.Wits + "\n", "red");
            }
            Console.Write("  Fitness: ");
            if (Character.ShowFitness() == "Healthy")
            {
                Tx.Emphasis(Character.ShowFitness(), "green");
            }
            else if (Character.ShowFitness() == "Unfit for Adventuring")
            {
                Tx.Emphasis(Character.ShowFitness(), "red");
            }
            else
            {
                Tx.Emphasis(Character.ShowFitness(), "yellow");
            }
            Console.WriteLine("\n");
        }

        static void CheckInventory()
        {
            Console.Clear();
            Tx.Emphasis("Your Inventory\n", "cyan");
            for (int item = 0; item < Character.Inventory.Count; item++)
            {
                Console.Write("* " + Character.Inventory[item].Name);
                Tx.Emphasis(" - " + Character.Inventory[item].Description + "\n", "gray");
            }
            Console.WriteLine("");
        }
    }

    class Program
    {
        static void Main()
        {
            Game.StartGame();

            Console.ReadKey();
        }
    }
}

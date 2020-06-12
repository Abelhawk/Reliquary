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
            //Tx.Emphasis("RELIQUARY - ©opyright 2020 Austin Ballard\n\n", "gray");
            //Console.Write("Press any key to start.");
            //Console.ReadKey();
            //Console.Clear();
            ShowLogo();
            Console.WriteLine("Welcome to Reliquary v.0.5!");
            bool Success = false;
            string[] Options = { "New Game", "Continue", "Quit" };
            while (Success == false)
            {
                int Answer = Choice(Options);
                switch (Answer)
                {
                    case 1:
                        CreateNewCharacter("");
                        StartAdventure();
                        Success = true;
                        break;
                    case 2:
                        bool ValidResponse = false;
                        while (!ValidResponse)
                        {
                            ShowLogo();
                            Console.Write("What is your name?\n>");
                            string SaveFileName = Console.ReadLine();
                            if (GameIsSaved(SaveFileName))
                            {
                                ValidResponse = true;
                                LoadGame(SaveFileName);
                                StartAdventure();
                            }
                            else
                            {
                                ShowLogo();
                                Console.WriteLine("Never heard of you. Are you new here?");
                                string[] Options2 = { "I am, actually.", "No, I misspoke my name." };
                                int Response = Choice(Options2);
                                if (Response == 1)
                                {
                                    ValidResponse = true;
                                    CreateNewCharacter(SaveFileName);
                                    break;
                                }
                            }
                        }
                        break;
                    case 3:
                        Success = true;
                        break;
                }

            }
        }

        public static void ShowLogo()
        {
            Console.Clear();
            Tx.Emphasis(@" __   ___         __             __      ", "gold");
            Console.WriteLine("");
            Tx.Emphasis(@"|__) |__  |    | /  \ |  |  /\  |__) \ / ", "gold");
            Console.WriteLine("");
            Tx.Emphasis(@"|  \ |___ |___ | \__X \__/ /——\ |  \  |  ", "gold");
            Console.WriteLine("\n");
            Tx.Emphasis("== A daily dose of adventure! ==\n\n", "gold");
        }

        public static void LoadGame(string game)
        {
            string file = "savedata/" + game + ".txt";
            StreamReader sr = new StreamReader(file);
            string line = null;
            int Ventures = 0;
            int VentureBonus = 0;
            string WelcomeMessage = "";
            string RestMessage = "You wake up rested and ready for a new day of adventuring!";
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
                else if (line.StartsWith("Gold:"))
                {
                    //There should be a handling function to check for a valid maximum number...maybe
                    Character.Gold = Convert.ToInt32(line.Substring(5));
                    Tx.Emphasis("Loaded gold.\n", "gray");
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
                            //SleepQuality should definitely affect these.
                            string[] WakeUpStrings = {
                                "You wake up and yawn sleepily. You were having such a good dream...",
                                "You wake up and squint in the darkness. What woke you up at this ungodsly hour?",
                                "You wake up to answer nature's call, then climb back into bed."
                            };
                            RestMessage = Tx.RandomString(WakeUpStrings);
                        }
                        else
                        {
                            RestMessage = "You wake up and rub your eyes. That was a nice nap, but there's more to do today.";
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
                        int DaysSinceLastLogin = (DateTime.Today.Subtract(LastLoggedIn)).Days;
                        if (DaysSinceLastLogin > 1)
                        //Test this tomorrow... It should be 3, I guess.
                        {
                            WelcomeMessage = "I haven't seen you for " + DaysSinceLastLogin + " days.";
                            if (DaysSinceLastLogin >= 29)
                            {
                                WelcomeMessage = "I haven't seen you for a long time!";
                            }
                        }
                        switch (DaysSinceLastLogin)
                        {
                            case 1:
                                RestMessage = "You wake up and stretch. Time for a new day!";
                                break;
                            case 2:
                            case 3:
                                VentureBonus = 2;
                                RestMessage = "You wake up very refreshed. What a good rest!";
                                break;
                            case var _ when (DaysSinceLastLogin > 5):
                                VentureBonus = -1;
                                RestMessage = "Wow, you slept for a long time. You feel a bit groggy.";
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
            ShowLogo();
            Console.WriteLine("Welcome back, " + Character.Gender + " " + Character.Name + ".");
            if (WelcomeMessage.Length > 0) Console.WriteLine(WelcomeMessage);
            Tx.Emphasis("Press any key to continue your adventure!\n", "cyan");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(RestMessage + "\n"); //May also remark about the sleeping conditions in this message or another one.
        }

        public static bool GameIsSaved(string filename)
        {
                string[] files = Directory.GetFiles("savedata/", filename + ".txt", SearchOption.TopDirectoryOnly);
                return (files.Length > 0);            
        }

        public static void SaveGame()
        {
            /*
             Should this save after every interaction, or only when you go to sleep, I wonder?
             */
            string SavedData = "Name:" + Character.Name + "\n";
            SavedData += "Gender:" + Character.Gender + "\n";
            SavedData += "PlaceID:" + Character.CurrentLocation + "\n";
            SavedData += "Might:" + Character.Might + "\n";
            SavedData += "Fitness:" + Character.Fitness + "\n";
            SavedData += "Wits:" + Character.Wits + "\n";
            SavedData += "Experience:" + Character.ExperiencePoints + "\n";
            SavedData += "Level:" + Character.Level + "\n";
            //SavedData += "SleepQuality:" + SleepPlace + "\n"; //Should indicate what type of inn or sleep location, number from 1-6  
            SavedData += "Ventures:" + Character.Ventures + "\n";
            SavedData += "LastPlayed:" + DateTime.Today + "\n";
            SavedData += "Gold:" + Character.Gold + "\n";
            for (int i = 0; i < Character.Inventory.Count; i++)
            {
                SavedData += "Item:" + Character.Inventory[i].ID + "\n";
            }
            File.WriteAllText("savedata/" + Character.Name.ToUpper() + ".txt", SavedData);
            Console.Clear();
            Console.WriteLine("Your game is saved!");
            Console.WriteLine("Thanks for playing Reliquary! See you again soon."); //Oh wait, this should just say "You fall asleep..." 
            Tx.Emphasis("Press any key to quit.", "cyan");
            Console.ReadKey();
        }

        static void CreateNewCharacter(string Name)
        {
            bool ValidName = (Name.Length > 0);
            bool LoadingGame = false;
            string NameQuestion = "First things first: what's your name, friend?";
            while (!ValidName)
            {
                ShowLogo();
                Console.Write(NameQuestion + "\n>");
                Name = Console.ReadLine();
                NameQuestion = "What's your real name, then?";
                if (GameIsSaved(Name.ToUpper()))
                {
                    Console.Write("Wait... You've been here before, haven't you?\n");
                    string[] Options2 = { "I misspoke.", "Yeah, that's me!" };
                    int LoadOrNew = Choice(Options2);
                    if (LoadOrNew == 2)
                    {
                        ValidName = true;
                        LoadingGame = true;
                        LoadGame(Name);
                    }
                }
                else
                {
                    if (Name.Length == 1)
                    {
                        NameQuestion = "\"" + Name.ToUpper() + "?\" Nice nickname, but what's it short for?";
                    }
                    else if (Name.Length > 1)
                    {
                        if (Name.ToLower() == "abelhawk")
                        {
                            NameQuestion = "You're THE Abelhawk? Ha! Pull the other one, mate. What's your actual name?";
                        }
                        else
                        {
                            ValidName = true;
                        }
                    }
                }
            }

            if (!LoadingGame)
            {
                ShowLogo();
                Character.Name = Name;
                for (int i = 1; i < Character.Name.Length; i++)
                {
                    Character.Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Character.Name.ToLower());
                }
                Console.WriteLine("Welcome to Reliquary, " + Character.Name + ".");
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
        {
            /*
                Format a choice like this:
                string[] Options = { "Pet a goat", "Make a stew", "'I'm selling these fine leather jackets...'" };
                int Answer = Choice(Options);
                    switch (Answer) {
                        case 1: ...
                    }
            */
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
            Console.WriteLine("Level " + Character.Level + " Adventurer\n");
            Console.Write("  Experience Points till level up: " + ((Character.Level * Character.Level) * 10 - Character.ExperiencePoints + "\n"));
            Console.Write("  Money: ");
            Tx.Emphasis(Character.Gold, "gold");
            Console.Write(" gold\n");
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
                Tx.Emphasis(Character.ShowFitness(), "gold");
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

            //Console.ReadKey();
        }
    }
}

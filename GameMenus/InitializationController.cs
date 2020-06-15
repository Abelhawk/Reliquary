using System;
using System.Collections.Generic;
using System.Text;

namespace Reliquary.GameMenus
{
    class InitializationController
    {
        internal static void StartGame()
        {
            //Tx.Emphasis("RELIQUARY - ©opyright 2020 Austin Ballard\n\n", "gray");
            //Console.Write("Press any key to start.");
            //Console.ReadKey();
            //Console.Clear();
            Game.ShowLogo();
            Console.WriteLine("Welcome to Reliquary v.0.5!");
            bool Success = false;
            string[] Options = { "New Game", "Continue", "Quit" };
            while (Success == false)
            {
                int Answer = Game.Choice(Options);
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
                            Game.ShowLogo();
                            Console.Write("What is your name?\n>");
                            string SaveFileName = Console.ReadLine();
                            if (SaveLoadController.GameIsSaved(SaveFileName))
                            {
                                ValidResponse = true;
                                SaveLoadController.LoadGame(SaveFileName);
                                StartAdventure();
                            }
                            else
                            {
                                Game.ShowLogo();
                                Console.WriteLine("Never heard of you. Are you new here?");
                                string[] Options2 = { "I am, actually.", "No, I misspoke my name." };
                                int Response = Game.Choice(Options2);
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

        

        static void CreateNewCharacter(string Name)
        {
            bool ValidName = (Name.Length > 0);
            bool LoadingGame = false;
            string NameQuestion = "First things first: what's your name, friend?";
            while (!ValidName)
            {
                Game.ShowLogo();
                Console.Write(NameQuestion + "\n>");
                Name = Console.ReadLine();
                NameQuestion = "What's your real name, then?";
                if (SaveLoadController.GameIsSaved(Name.ToUpper()))
                {
                    Console.Write("Wait... You've been here before, haven't you?\n");
                    string[] Options2 = { "I misspoke.", "Yeah, that's me!" };
                    int LoadOrNew = Game.Choice(Options2);
                    if (LoadOrNew == 2)
                    {
                        ValidName = true;
                        LoadingGame = true;
                        SaveLoadController.LoadGame(Name);
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
                Game.ShowLogo();
                Character.Name = Name;
                for (int i = 1; i < Character.Name.Length; i++)
                {
                    Character.Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Character.Name.ToLower());
                }
                Console.WriteLine("Welcome to Reliquary, " + Character.Name + ".");
                Console.WriteLine("\nEh... does that mean you're a man or a woman?");
                string[] Options = { "Man", "Woman", "Nonbinary" };
                int Answer = Game.Choice(Options);
                switch (Answer)
                {
                    case 3:
                        Console.WriteLine("\n-_- ... Okay fine, what was your BIOLOGICAL SEX at birth?");
                        Options = new string[] { "Male", "Female" };
                        Answer = Game.Choice(Options);
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
            int Answer = Game.Choice(Options);
            switch (Answer)
            {
                case 1:
                    Game.LookAtSelf();
                    StartAdventure();
                    break;
                case 2:
                    Game.CheckInventory();
                    StartAdventure();
                    break;
                case 3:
                    Console.WriteLine("Naw, let's do something else.");
                    break;
                case 4:
                    SaveLoadController.SaveGame();
                    break;
            }
        }

        static void Initialize()
        {
            Inventory.AddSignetRing();
        }
    }
}

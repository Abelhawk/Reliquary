using System;
using System.IO;

namespace Reliquary.GameMenus
{
    class SaveLoadController
    {    
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
            Game.ShowLogo();
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
    }
}

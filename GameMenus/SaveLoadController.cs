using System;
using System.IO;
using Reliquary.Places;

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
            int SleepQuality = 0;
            string WelcomeMessage = "";
            string RestMessage = "";
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("Name:"))
                {
                    Character.Name = line.Substring(5);
                }
                else if (line.StartsWith("Gender:"))
                {
                    Character.Gender = line.Substring(7);
                }
                else if (line.StartsWith("PlaceID:"))
                {
                    Character.CurrentLocation = Convert.ToInt32(line.Substring(8));
                }
                else if (line.StartsWith("SleepLocation:"))
                {
                    Character.SleepLocation = Convert.ToInt32(line.Substring(14));
                }
                else if (line.StartsWith("Item:"))
                {
                    Inventory.AddItem(Convert.ToInt32(line.Substring(5)));
                }
                else if (line.StartsWith("Might:"))
                {
                    //There should be a handling function to check for a valid maximum number
                    Character.Might = Convert.ToInt32(line.Substring(6));
                }
                else if (line.StartsWith("Fitness:"))
                {
                    Character.Fitness = Convert.ToInt32(line.Substring(8));
                }
                else if (line.StartsWith("Wits:"))
                {
                    Character.Wits = Convert.ToInt32(line.Substring(5));
                }
                else if (line.StartsWith("Experience:"))
                {
                    //There should be a handling function to check for a valid maximum number for the current level
                    Character.ExperiencePoints = Convert.ToInt32(line.Substring(11));
                }
                else if (line.StartsWith("Level:"))
                {
                    //There should be a handling function to check for a valid maximum number
                    Character.Level = Convert.ToInt32(line.Substring(6));
                }
                else if (line.StartsWith("Gold:"))
                {
                    //There should be a handling function to check for a valid maximum number...maybe
                    Character.Gold = Convert.ToInt32(line.Substring(5));
                }
                else if (line.StartsWith("SleepQuality:"))
                {
                    /*
                       1: Terrible - Might wake up sick or missing some coins from scavengers
                       2: Okay - Sleeping in a poor house. Safe, but not comfortable.
                       3: Decent - A straw-filled mattress in an inn (+2 bonus Ventures)
                       4: Good - ? How many of these should I actually even bother with? (+4 bonus Ventures)
                       5: Amazing - A feather bed in a castle (+6 bonus Ventures)
                     */
                    SleepQuality = Convert.ToInt32(line.Substring(13));
                }
                else if (line.StartsWith("Ventures:"))
                {
                    Ventures = Convert.ToInt32(line.Substring(9));
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
                                "You wake up to answer nature's call, then lie back down groggily."
                            };
                            RestMessage = Tx.RandomString(WakeUpStrings);
                        }
                        else
                        {
                            RestMessage = "You rub your eyes. That was a nice nap, but there's more to do today.";
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
                        {
                            WelcomeMessage = "You last played " + DaysSinceLastLogin + " days ago.";
                            if (DaysSinceLastLogin >= 29)
                            {
                                WelcomeMessage += "It's been quite a while!";
                            }
                        }
                        switch (DaysSinceLastLogin) //I don't like this anymore
                        {
                            case 1:
                                RestMessage = "You feel rested.";
                                break;
                            case 2:
                            case 3:
                                VentureBonus = 2;
                                RestMessage = "You feel refreshed!";
                                break;
                            case var _ when (DaysSinceLastLogin > 5):
                                VentureBonus = -1;
                                RestMessage = "You feel a bit groggy.";
                                break;
                        }
                        if (Ventures > 0)
                        {
                            //If there are ventures left over from a previous day, you can keep up to 5 of them.
                            VentureBonus += Math.Max(5, Ventures);
                            
                        }
                        VentureBonus += ((SleepQuality - 2) * 2);
                        Character.Ventures = 5 + (Character.Fitness * 2) + Character.Might + Character.Wits + Character.Level + VentureBonus;
                    }

                }
            }
            sr.Close();
            Game.ShowLogo();
            Console.WriteLine("Welcome back, " + Character.Gender + " " + Character.Name + ".");
            if (WelcomeMessage.Length > 0) Console.WriteLine(WelcomeMessage);
            Tx.Emphasis("\nPress any key to continue your adventure!\n", "cyan");
            Console.ReadKey();
            Console.Clear();            
            Console.WriteLine(RestMessage);
        }

        public static bool GameIsSaved(string filename)
        {
            string[] files = Directory.GetFiles("savedata/", filename + ".txt", SearchOption.TopDirectoryOnly);
            return (files.Length > 0);
        }

        public static void SaveGame(int SleepQuality)
        {
            /*
             Should this save after every interaction, or only when you go to sleep, I wonder?
             */
            string SavedData = "Name:" + Character.Name + "\n";
            SavedData += "Gender:" + Character.Gender + "\n";
            SavedData += "PlaceID:" + Character.CurrentLocation + "\n";
            SavedData += "SleepLocation:" + Character.SleepLocation + "\n";
            SavedData += "Might:" + Character.Might + "\n";
            SavedData += "Fitness:" + Character.Fitness + "\n";
            SavedData += "Wits:" + Character.Wits + "\n";
            SavedData += "Experience:" + Character.ExperiencePoints + "\n";
            SavedData += "Level:" + Character.Level + "\n";
            SavedData += "SleepQuality:" + SleepQuality + "\n"; //Should indicate what type of inn or sleep location, number from 1-6  
            SavedData += "Ventures:" + Character.Ventures + "\n";
            SavedData += "LastPlayed:" + DateTime.Today + "\n";
            SavedData += "Gold:" + Character.Gold + "\n";
            for (int i = 0; i < Character.Inventory.Count; i++)
            {
                SavedData += "Item:" + Character.Inventory[i].ID + "\n";
            }
            File.WriteAllText("savedata/" + Character.Name.ToUpper() + ".txt", SavedData);
            if (Character.Ventures == 0)
            {
                Console.WriteLine("You fall asleep, utterly exhausted.");
            }
            else if (Character.Ventures < 5)
            {
                Console.WriteLine("You fall asleep.");
            } else
            {
                Console.WriteLine("You're not very tired, but eventually you fall asleep.");
            }
            Console.WriteLine("\nYour game is saved!");
            Tx.Emphasis("Press any key to quit Reliquary.", "cyan");
            Console.ReadKey();
        }        
    }
}

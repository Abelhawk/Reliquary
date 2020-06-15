using System;
using System.Collections.Generic;
using Reliquary.GameMenus;
using Reliquary.WorldData;

// This class is meant to be a template for all other places, but if you can think of a way to simplify it
// By using a Place "class" somehow, please submit a GitHub issue for me!

namespace Reliquary.Places
{
    class Merrydale
    {
        //New locations might be unlocked later.   
        public static List<string> LocationOptionsList = new List<string> {
                "Tavern",
                "General Store",
                "Smithy",
                "Wildmarch Field(1)"
            };

        public static void Load(string WakeOrTravel)
        {
            Character.CurrentLocation = 0;
            bool Done = false;

            string Name = "Merrydale Township";
            string WakeUp = "You wake up in a pile of refuse in an alleyway, dust yourself off, and enter the main thoroughfare...";
            string Travel = "You cross a babbling brook and approach Merrydale. The gatekeeper waves you through...";
            string Description = "The cobblestones under your feet vibrate with the sounds of peasant life around you.\n";
            Description += "Villagers go to and fro, tending to their daily duties and paying you little heed.";
            string[] Options = { "Self", "Inventory", "Locations", "Sleep" };
            string[] LocationOptions = LocationOptionsList.ToArray();

            if (WakeOrTravel == "Wake")
            {
                Console.WriteLine(WakeUp);
                Tx.Emphasis("\nPress any key.\n", "cyan");
                Console.ReadKey();
                Console.Clear();
                Character.DisplayVentures();
            }
            else if (WakeOrTravel == "Travel")
            {
                Console.WriteLine(Travel);
                Tx.Emphasis("\nPress any key.\n", "cyan");
                Console.ReadKey();
                Console.Clear();
                Character.DisplayVentures();
            }
            else if (WakeOrTravel == "New")
            {
                Console.WriteLine("You stroll into the town of Merrydale. This looks like a great place to prepare for an adventure!\n");
                Tx.Emphasis("\nPress any key.\n", "cyan");
                Console.ReadKey();
                Console.Clear();
                Character.DisplayVentures();
            }            
            Tx.Emphasis(Name + "\n", "cyan");
            Console.WriteLine(Description);
            int Answer = Game.Choice(Options);
            switch (Answer)
            {
                //These first two cases are particularly in need of their own scalable class.
                case 1:
                    Game.LookAtSelf();
                    Load("");
                    break;
                case 2:
                    Game.CheckInventory();
                    Load("");
                    break;
                case 3:
                    Console.Clear();
                    Character.DisplayVentures();
                    Tx.Emphasis(Name + "\n", "cyan");
                    Console.WriteLine("Where would you like to go?");
                    int Choice = Game.Choice(LocationOptions);
                    Choice--;
                    if (LocationOptions[Choice] == "Tavern")
                    {
                        Console.Clear();
                        Console.WriteLine("You want to go to the tavern? What are you, a drunk?\n");
                    }
                    else if (LocationOptions[Choice] == "General Store")
                    {
                        Console.Clear();
                        Console.WriteLine("You want to go to the general store? What are you, a shopaholic?\n");
                    }
                    else if (LocationOptions[Choice] == "Smithy")
                    {
                        Smithy();
                    }
                    else if (LocationOptions[Choice] == "Wildmarch Field(1)")
                    {
                        Console.Clear();
                        if (Character.Ventures > 1)
                        {
                            Done = true;                            
                        } else
                        {
                            Done = false;
                        }
                        PlacesData.LoadPlace(1, "Travel"); //I'd like this to be in the PlacesData class, but what to do with the Done variable?
                    }
                    if (Done == false)
                    {
                        Load("");
                    }
                    break;
                case 4:
                    Console.Clear();
                    Tx.Emphasis(Name + "\n", "cyan");
                    Character.DisplayVentures();
                    Console.WriteLine("Are you sure you want to sleep in the street?");
                    string[] YesNo = { "It's not like there's any other choice at the moment.", "No!" };
                    int SleepChoice = Game.Choice(YesNo);
                    if (SleepChoice == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("You find the cleanest alley you can, shoo away the rats, and hunker down in a pile of garbage.");
                        SaveLoadController.SaveGame(1);
                        Done = true;
                    }
                    Console.Clear();
                    if (Done == false)
                    {
                        Load("");
                    }
                    break;                    
            }
        }

        public static void Smithy()
        {            
            string[] Descriptions = {
                    "A man with an auburn beard looks up from helping his apprentice.",
                    "A strong blacksmith hammers away at a horseshoe on an anvil.",
                    "You meet the owner of this establishment, Calahan Smith."
                };
            string[] Dialogs = {
                    "\"What can I help you with?\"",
                    "\"I'll be right with you. I'm almost finished.\"",
                    "\"You say 'adventurer,' I say 'rat-catcher.'\""
                };
            bool JustEntering = true;
            string Description = Tx.RandomString(Descriptions);
            string Dialog = Tx.RandomString(Dialogs);
            string[] Options = { "Buy", "Sell", "Upgrade", "Exit" };
            bool Stay = true;
            Console.Clear();
            while (Stay)
            {
                Tx.Emphasis("Calahan's Smithy\n", "cyan");
                if (JustEntering)
                {
                    Console.WriteLine(Description);
                    JustEntering = false;
                }                
                Tx.Emphasis(Dialog, "gold");
                Console.WriteLine("");
                int Answer = Game.Choice(Options);
                switch (Answer)
                {
                    case 1:
                        Console.Clear();
                        Dialog = "\"Hoho! It'll be a while till Austin programs shops in this game. Come back in a few Git pushes.";
                        break;
                    case 2:
                        Console.Clear();
                        Dialog = "\"My 'shopGold' variable isn't even defined yet. Come back in a few code changes.\"";
                        break;
                    case 3:
                        Console.Clear();
                        Dialog = "\"Sorry, I don't have any materials to upgrade with. Come back in a few patches.\"";
                        break;
                    case 4:
                        Console.Clear();
                        Stay = false;
                        break;
                }
            }            
        }
    }
}

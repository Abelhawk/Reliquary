using System;
using System.Collections.Generic;
using Reliquary.GameMenus;
using Reliquary.WorldData;

namespace Reliquary.Places
{
    class Wildmarch
    {
        //public static List<string> LocationOptionsList = new List<string> {
        //        "Tavern",
        //        "General Store",
        //        "Smithy",
        //        "(1) Wildmarch Field"
        //    };

        public static void Load(string WakeOrTravel)
        {
            Character.CurrentLocation = 0;
            bool Done = false;

            string Name = "Wildmarch Field";
            string WakeUp = "You wake up shivering and covered with dew. You wring out your tunic and set off into the field...";
            string Travel = "You walk off the beaten path and into the tall grasses of Wildmarch Field...";
            string Description = "The smells of green vegetation and wildflowers fill your nose.\n";
            Description += "Birds sing, and the clicking of insects hiding in the tall grass can be heard nearby.";
            string[] Options = { "Self", "Inventory", "Locations", "Sleep" };
            //string[] LocationOptions = LocationOptionsList.ToArray();

            if (WakeOrTravel == "Wake")
            {
                Console.WriteLine(WakeUp);
                Tx.Emphasis("\nPress any key.\n", "cyan");
                Console.ReadKey();
            }
            else if (WakeOrTravel == "Travel")
            {
                Console.WriteLine(Travel);
                //Tx.Emphasis("\nPress any key.\n", "cyan");
                //Console.ReadKey();
            }

            Console.WriteLine("...And turn right back around because Wildmarch isn't programmed yet.");
            Tx.Emphasis("\nPress any key.\n", "cyan");
            Console.ReadKey();
            Console.Clear();
            PlacesData.LoadPlace(0, "Travel");
        }
    }
}
/*
            Character.DisplayVentures();
            Tx.Emphasis(Name + "\n", "cyan");
            Console.WriteLine(Description);
            int Answer = Game.Choice(Options);
            switch (Answer)
            {
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
                    else if (LocationOptions[Choice] == "(1) Wildmarch Field")
                    {
                        Console.Clear();
                        Console.WriteLine("You want to go to Wildmarch Field? Well, too bad, because it's not programmed yet!\n");
                    }
                    break;
                case 4:
                    Console.Clear();
                    Tx.Emphasis(Name + "\n", "cyan");
                    Character.DisplayVentures();
                    Console.WriteLine("Are you sure you want to sleep in the street?");
                    string[] YesNo = { "Yes", "No!" };
                    int SleepChoice = Game.Choice(YesNo);
                    if (SleepChoice == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("You find the cleanest alley you can, shoo away the rats, and hunker down in a pile of garbage.");
                        SaveLoadController.SaveGame(1);
                        Done = true;
                    }
                    Console.Clear();
                    break;                    
            }
            if (!Done)
            {
                Load("");
            }
        }

        public static void Smithy()
        {            
            string[] Descriptions = {
                    "A man with an auburn beard looks up from helping his apprentice.",
                    "A strong blacksmith hammers away at a horseshoe on an anvil.",
                    "You meet Calahan, the owner of this smithy."
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
*/
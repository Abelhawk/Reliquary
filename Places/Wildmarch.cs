using System;
using System.Collections.Generic;
using Reliquary.GameMenus;
using Reliquary.WorldData;
using Reliquary.Classes;

namespace Reliquary.Places
{
    class Wildmarch
    {
        public static List<string> LocationOptions = new List<string> {
                "Tall Grasses (1)",
                "Monastery",
                "Merrydale Township"
            };
        
        public static bool Travel()
        {
            if (Discoveries.IroncliffMountains == true && !LocationOptions.Contains("Ironcliff Mountains (1)"))
            {
                LocationOptions.Add("Ironcliff Mountains (1)");
                LocationOptions.Remove("Back");
            }
            if (!LocationOptions.Contains("Back")) {
                LocationOptions.Add("Back");
            };
            int Choice = Game.Choice(LocationOptions.ToArray());
            Choice--;

            if (LocationOptions[Choice] == "Tall Grasses (1)")
            {
                WildmarchEncounters.Encounter(1);
                Character.DisplayVentures();
                Tx.Emphasis(PlaceData.GetPlace(Character.CurrentLocation).Name + "\n", "cyan");
                Console.WriteLine("Where would you like to go?");
                Travel();
            }           
            else if (LocationOptions[Choice] == "Monastery")
            {
                Console.Clear();
                Tx.Emphasis("\"I'm afraid we're not accepted visitors at the moment. Our monastery isn't programmed yet.\"\n\n", "gold");
            }
            else if (LocationOptions[Choice] == "Merrydale Township")
            {
                Console.Clear();
                Character.CurrentLocation = 0;
                NavigationController.LoadPlace("Travel", 0);
                return true;
            }
            else if (LocationOptions[Choice] == "Ironcliff Mountains (1)")
            {
                Console.Clear();
                Character.Ventures --;
                Console.WriteLine("You go off to Ironcliff Mountains and have a cool adventure and then come back.\n"); // DEBUG
                Tx.Emphasis("Press any key to continue.", "cyan"); //DEBUG
                Console.ReadKey(); //DEBUG
                //Character.CurrentLocation = 2;
                //NavigationController.LoadPlace("Travel", 1);
                return true;
            }
            Console.Clear();
            return false;
        }    
}
}

using System;
using System.Collections.Generic;
using Reliquary.GameMenus;
using Reliquary.WorldData;

namespace Reliquary.Places
{
    class Wildmarch
    {
        public static List<string> LocationOptions = new List<string> {
                "Tall Grasses (1)",
                "Monastery",
                "Merrydale Township",
                "Back"
            };

        public static bool Travel()
        {
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
            Console.Clear();
            return false;
        }    
}
}

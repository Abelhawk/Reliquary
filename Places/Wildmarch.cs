using System;
using System.Collections.Generic;
using Reliquary.GameMenus;
using Reliquary.WorldData;

namespace Reliquary.Places
{
    class Wildmarch
    {
        public static List<string> LocationOptionsList = new List<string> {
                "Tall Grasses(1)",
                "Monastery",
                "Merrydale Township"
            };

        public static bool Travel()
        {
            string[] LocationOptions = LocationOptionsList.ToArray();
            int Choice = Game.Choice(LocationOptions);
            Choice--;

            if (LocationOptions[Choice] == "Tall Grasses(1)")
            {
                WildmarchEncounters.Encounter(1);
            }           
            else if (LocationOptions[Choice] == "Monastery")
            {
                Console.Clear();
                Tx.Emphasis("\"I'm afraid we're not accepted visitors at the moment. Our monastery isn't rendered yet.\"\n\n", "gold");
            }
            else if (LocationOptions[Choice] == "Merrydale Township")
            {
                Console.Clear();
                Character.CurrentLocation = 0;
                NavigationController.LoadPlace("Travel", 0);
                return true;
            }
            return false;
        }    
}
}

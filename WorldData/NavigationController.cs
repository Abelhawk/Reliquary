using System;
using System.Collections.Generic;
using System.Text;
using Reliquary.Places;
using Reliquary.GameMenus;

/*

    The player starts off at Merrydale Township

*/

namespace Reliquary.WorldData
{
    class NavigationController
    {
        static public void LoadPlace(string wakeOrTravel, int venturesCost)
        {
            if (wakeOrTravel == "Travel" && Character.Ventures == 0)
            {
                Console.WriteLine("You're too tired to venture anymore today.\n"); //BUG: This shows up directly after you spend your last venture
            }
            else
            {
                if (wakeOrTravel == "Wake")
                {
                    Character.LastPlayed = DateTime.Today.ToString();
                    Tx.Emphasis(Tx.GetGameDate(DateTime.Today.ToString(), "full") + "\n", "gray");
                }
                if (PlaceData.PlacesList[Character.CurrentLocation] == null)
                {
                    //If the save file is corrupted and it can't find the place ID, you just wake up in Merrydale.
                    Console.WriteLine("You look around confused. You don't remember going to sleep here. Huh. Oh well.\n");
                    Character.CurrentLocation = 0;
                    Gameplay.Play("Wake");
                }
                else
                {
                    Character.Ventures -= venturesCost;
                    Gameplay.Play(wakeOrTravel);
                }
            }            
        }
    }
}

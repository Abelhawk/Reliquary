using System;
using System.Collections.Generic;
using System.Text;

/*

    The player starts off at Merrydale Township

*/

namespace Reliquary.WorldData
{
    class PlacesData
    {
        static public void LoadPlace(int id, string WakeOrTravel)
        {
            if (WakeOrTravel == "Travel" && Character.Ventures == 0)
            {
                Console.WriteLine("You're too tired to venture anymore today.\n"); //BUG: This shows up directly after you spend your last venture
            }
            else
            {
                switch (id)
                {
                    case 0:
                        {
                            //Merrydale Township
                            Places.Merrydale.Load(WakeOrTravel);
                            break;
                        }
                    case 1:
                        {
                            if (WakeOrTravel == "Travel")
                            {
                                Character.Ventures --;
                            }
                            //Wildmarch Field
                            Places.Wildmarch.Load(WakeOrTravel);
                            break;
                        }
                    default:
                        Console.WriteLine("You look around confused. You don't remember going to sleep here. Huh. Oh well.\n");
                        Character.LastPlayed = DateTime.Today.ToString();
                        Tx.Emphasis(Tx.GetGameDate(DateTime.Today.ToString(), "full") + "\n", "gray");
                        Places.Merrydale.Load("");
                        break;
                }
            }            
        }
    }
}

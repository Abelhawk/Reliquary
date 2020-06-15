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
        static public void LoadPlaces()
        {
            Place Place0 = new Place();
            Place0.PlaceID = 0;
            Place0.Name = "Merrydale Township";
            Place0.Description = "The cobblestones under your feet vibrate with the sounds of peasant life around you.\n";
            Place0.Description += "Villagers go to and fro, tending to their daily duties and paying you little heed.";
            //List<Destination> 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Reliquary.Places
{
    class PlaceData
    {
        public static string[] NoInns = {};
        public static string[] MerrydaleInns =
        {
            "You wake up on the hard wooden floor, get the crick out of your back, and stand up and gather your things...",
            "You wake up in your room in the Horn and Lantern Inn, wash your face, use the chamberpot, and head downstairs for breakfast...",
            "You part the silk curtains and partake of the hot breakfast platter waiting for you on your bedside table, then you saunter back downstairs..."
        };
        public static Place Merrydale = new Place(
            0,
            "Merrydale Township",
            "The cobblestones under your feet vibrate with the sounds of peasant life around you.",
            "Villagers go to and fro, tending to their daily duties and paying you little heed",
            "You wake up in a pile of refuse in an alleyway, dust yourself off, and enter the main thoroughfare...",
            "You cross a babbling brook and approach Merrydale. The gatekeeper waves you through...",
            "Are you sure you want to sleep in the street?",
            "You find the cleanest alley you can, shoo away the rats, and hunker down in a pile of garbage.",
            MerrydaleInns
            );

        public static Place Wildmarch = new Place(
            0,
            "Wildmarch Field",
            "The smells of green vegetation and wildflowers fill your nose.",
            "Birds sing, and the clicking of insects hiding in the tall grass can be heard nearby.",
            "You wake up shivering and covered with dew. You wring out your tunic and set off into the field...",
            "You walk off the beaten path and into the tall grasses of Wildmarch Field...",
            "Are you sure you want to sleep out here?",
            "You pat down a patch of grass as best as you can and lie down for a rest under the open sky.",
            NoInns
            );

        public static List<Place> PlacesList = new List<Place>{
            Merrydale,
            Wildmarch
        };

        public static bool PlaceTravel(int id)
        {
            switch (id) {
                case 0:
                    return Places.Merrydale.Travel();
                case 1:
                    return Places.Wildmarch.Travel();
                default:
                    return false;
            }
        }
    }
}

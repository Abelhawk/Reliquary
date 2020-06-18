using System;
using Reliquary.Places;

namespace Reliquary.GameMenus
{
    class Gameplay
    {
        public static void Play(string wakeOrTravel)
        {
            Place Place = PlaceData.GetPlace(Character.CurrentLocation);
            bool Done = false;
            if (wakeOrTravel == "Wake")
            {
                Console.WriteLine(PlaceData.PlacesList[Character.CurrentLocation].WakeUpMessages[Character.SleepLocation]);
                Tx.Emphasis("\nPress any key.\n", "cyan");
                Console.ReadKey();
                Console.Clear();
                Character.DisplayVentures();
            }
            else if (wakeOrTravel == "Travel")
            {
                Console.WriteLine(Place.Travel);
                Tx.Emphasis("\nPress any key.\n", "cyan");
                Console.ReadKey();
                Console.Clear();
                Character.DisplayVentures();
            }
            else if (wakeOrTravel == "New")
            {
                Console.WriteLine("You stroll into the town of Merrydale. This looks like a great place to prepare for an adventure!\n");
                Tx.Emphasis("\nPress any key.\n", "cyan");
                Console.ReadKey();
                Console.Clear();
                Character.DisplayVentures();
            }
            Tx.Emphasis(Place.Name + "\n", "cyan");
            Console.WriteLine(Place.Description1);
            Console.WriteLine(Place.Description2);
            int Answer = Game.Choice(Place.Options);
            switch (Answer)
            {
                case 1:
                    Game.LookAtSelf();
                    break;
                case 2:
                    Game.CheckInventory();
                    break;
                case 3: // Travel
                    Console.Clear();
                    Character.DisplayVentures();
                    Tx.Emphasis(Place.Name + "\n", "cyan");
                    Console.WriteLine("Where would you like to go?");
                    Done = PlaceData.PlaceTravel(Character.CurrentLocation);
                    break;
                case 4:
                    Console.Clear();
                    Tx.Emphasis(Place.Name + "\n", "cyan");
                    Character.DisplayVentures();
                    Console.WriteLine(Place.SleepConfirm);
                    string[] YesNo = { "Yes", "No!" };
                    int SleepChoice = Game.Choice(YesNo);
                    Console.Clear();
                    if (SleepChoice == 1)
                    {
                        Done = true;
                        Console.WriteLine(Place.GoToSleep);
                        SaveLoadController.SaveGame(1);
                    }
                    break;
            }
            if (Done == false)
            {
                Play("");
            }
        }        
    }
}

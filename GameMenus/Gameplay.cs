using System;
using Reliquary.Places;

namespace Reliquary.GameMenus
{
    class Gameplay
    {
        public static void Play(int placeId, string wakeOrTravel)
        {
            Character.CurrentLocation = placeId;
            Place Place = GetPlace(placeId);            
            bool Done = false;

            if (wakeOrTravel == "Wake")
            {
                Console.WriteLine(Place.WakeUp);
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
                    Play(Place.ID, "");
                    break;
                case 2:
                    Game.CheckInventory();
                    Play(Place.ID, "");
                    break;
                case 3:
                    Console.Clear();
                    Character.DisplayVentures();
                    Tx.Emphasis(Place.Name + "\n", "cyan");
                    Console.WriteLine("Where would you like to go?");
                    Tx.Emphasis("Place ID is " + Place.ID, "red");
                    Done = PlaceData.PlaceTravel(Place.ID);        
                    if (Done == false)
                    {
                        Play(Place.ID, "");
                    }
                    break;
                case 4:
                    Console.Clear();
                    Tx.Emphasis(Place.Name + "\n", "cyan");
                    Character.DisplayVentures();
                    Console.WriteLine(Place.SleepConfirm);
                    string[] YesNo = { "Yes", "No!" };
                    int SleepChoice = Game.Choice(YesNo);
                    if (SleepChoice == 1)
                    {
                        Console.Clear();
                        Console.WriteLine(Place.GoToSleep);
                        SaveLoadController.SaveGame(1);
                        Done = true;
                    }
                    Console.Clear();
                    if (Done == false)
                    {
                        Play(Place.ID, "");
                    }
                    break;
            }
        }

        public static Place GetPlace(int id)
        {
            return PlaceData.PlacesList[id];
        }
    }    
}

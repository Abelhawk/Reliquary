using System.Collections.Generic;

namespace Reliquary
{
    public static class Character
    {
        public static string Name = "Smith";
        public static string Gender = "Sir";
        public static List<Item> Inventory = new List<Item>();
        public static int CurrentLocation = 1;
        public static int Gold = 5;

        public static int Ventures = 5;
        public static string LastPlayed = "6/9/2020 12:00:00 AM";
        public static int ExperiencePoints = 0;
        public static int Level = 1;

        //Character Stats
        public static int Might = 1;
        public static int Fitness = 3;
        public static int Wits = 1;        

        public static string ShowFitness()
        {
            switch (Fitness)
            {
                case 2:
                    return "Injured";
                case 1:
                    return "Wounded";
                case 0:
                    return "Unfit for Adventuring";
            }
            return "Healthy";
        }
    }
}

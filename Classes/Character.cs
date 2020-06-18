using System;
using System.Collections.Generic;

namespace Reliquary
{
    public static class Character
    {
        public static string Name = "Smith";
        public static string Gender = "Sir";
        public static List<Item> Inventory = new List<Item>();
        public static int CurrentLocation = 0;
        public static int SleepLocation = 0;
        public static int Gold = 5;

        public static int Ventures = 5;
        public static string LastPlayed = "6/9/2020 12:00:00 AM";
        public static int ExperiencePoints = 0;
        public static int Level = 1;
        public static int NextLevel = 10;

        //These might need "maximum" values so that the current value can be modified by factors
        public static int Might = 1;
        public static int Fitness = 3;
        public static int Wits = 1;        

        public static string ShowFitness()
        {
            /*
             Oo, you know what'd be cool? If the character actually had hit points (int),
             but it was just measured in percentages. So they knew if they were "wounded"
             or "injured," but they didn't know how badly. I like that.
             */
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

        public static void DisplayVentures()
        {
            Console.Write("Ventures Remaining: ");
            if (Ventures > 0)
            {
                Tx.Emphasis(Ventures + "\n\n", "green");
            } else
            {
                Tx.Emphasis(Ventures + "\n\n", "red");
            }            
        }

        public static bool MightCheck(int DC)
        {
            int DieRoll = Tx.RandomInt(1, 10); // Hmm... possible to crit or crit-fail?
            return (DieRoll + Might >= DC);
        }

        public static bool WitsCheck(int DC)
        {
            int DieRoll = Tx.RandomInt(1, 10);
            return (DieRoll + Wits >= DC);
        }

        public static bool WinsMightContest(int EnemyMight)
        {
            int DieRoll = Tx.RandomInt(1, 10);
            int PlayerRoll = Might + DieRoll;
            DieRoll = Tx.RandomInt(1, 10);
            return (PlayerRoll >= EnemyMight + DieRoll);
        }

        public static bool WinsWitsContest(int EnemyWits)
        {
            int DieRoll = Tx.RandomInt(1, 10);
            int PlayerRoll = Wits + DieRoll;
            DieRoll = Tx.RandomInt(1, 10);
            return (PlayerRoll >= EnemyWits + DieRoll);
        }

        public static void AddExp(int amount)
        {
            ExperiencePoints += amount;
            if (ExperiencePoints >= NextLevel)
            {
                Tx.Emphasis("LEVEL UP!", "red");
                Level ++;
                NextLevel = Level*Level*10;
                //Prompt player to choose where to add points in Might and Wits
            }
        }
    }

    
}

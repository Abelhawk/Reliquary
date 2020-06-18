using System;
using System.Collections.Generic;
using System.Text;
using Reliquary.Classes;

namespace Reliquary.GameMenus
{
    public static class CombatController
    {
        public static void Combat(Monster Enemy)
        {
            Tx.Emphasis(Enemy.Name + "\n", "gold");
            Console.WriteLine(Enemy.EncounterText);
            if (Character.WinsWitsContest(Enemy.Wits))
            {
                Console.WriteLine("You get the jump on it!");
            }
            else
            {
                Console.WriteLine("The " + Enemy.Name + " is quicker than you and attacks!");
            }
            Console.WriteLine("But then the " + Enemy.Name + " runs away. Whew.\n");
        }
    }
}

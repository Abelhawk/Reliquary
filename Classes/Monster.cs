using System;
using System.Collections.Generic;
using System.Text;

namespace Reliquary.Classes
{
    public class Monster
    {
        //Does everything need an ID or is there an easier way to just look up the name itself?
        public string Name = "error";
        public string EncounterText = "no encounter text";
        public int Level = 1;

        //Stats
        public int Fitness = 1;
        public int Might = 1;
        public int Wits = 1;

        //Item Drops
        public int GoldSpoils = 0; //Will this be the same every game?
        public int[] ItemDrops = new int[]
        {
            //Uh oh... rarity? D:
        };
    }
}

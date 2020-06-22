using System;
using System.Collections.Generic;
using System.Text;
using Reliquary.WorldData;

namespace Reliquary.Classes
{
    public class Monster
    {
        public string Name = "error";
        public string EncounterText = "no encounter text";
        public int Level = 1;

        //Stats
        public int Fitness = 5;
        public int CurrentFitness = 5;
        public int Might = 1;
        public int Wits = 1;

        //Item Drops
        public int GoldSpoils = 0; //Will this be the same every game?
        public Item[] ItemDrops = new Item[]
        {
            //Uh oh... rarity? D:
            ItemData.SignetRing
        };

        public string ShowFitness()
        {
            if (CurrentFitness <= Fitness / 2)
            {

                if (CurrentFitness <= Fitness / 3)
                {
                    return "Injured";
                }
                if (CurrentFitness == 1)
                {
                    return "Heavily Wounded";
                }
                else
                {
                    return "Bruised";
                }

            }
            return "Healthy";
        }

        public void Loot()
        {
            if (ItemDrops.Length > 0)
            {
                int Chance = Tx.RandomInt(1, 100);
                if (Chance >= 60) // This is dumb right now, because it means you have a 60% chance to get everything on its list
                {
                    if (ItemDrops.Length > 1)
                    {
                        Console.WriteLine("You found:");
                        for (int i = 0; i < ItemDrops.Length; i++)
                        {
                            Console.WriteLine("* " + ItemDrops[i].Name);
                            Inventory.AddItem(ItemDrops[0].ID); //You know what? Screw item IDs. todo: Get rid of them
                        }
                    }
                    else
                    {
                        Console.WriteLine("You found a " + ItemDrops[0].Name + ".");
                        Inventory.AddItem(ItemDrops[0].ID);
                    }
                }
            }
        }
    }
}


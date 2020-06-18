using System.Collections.Generic;
using Reliquary.Classes;

// I may need to eventually sort these by place. As well as encounters.

namespace Reliquary.WorldData
{
    public class MonsterData
    {
        // ========= MASTER MONSTER LIST ========= //

        public static Monster BigRat = new Monster()
        {
            Name = "Big Rat",
            EncounterText = "A rat the size of a small dog jumps out at you, its fangs bared!",
            Level = 1,
            Fitness = 1,
            Might = 1,
            Wits = 1,
        };

        public static Monster GoldRat = new Monster()
        {
            Name = "Gold Rat",
            EncounterText = "A large rat with glistening golden fur flashes a set of golden teeth at you and attacks!",
            Level = 1,
            Fitness = 1,
            Might = 1,
            Wits = 1,
            GoldSpoils = Tx.RandomInt(2, 9),
            ItemDrops = new int[]
            {
                ItemData.GetItem("garnet").ID
            }
        };

        public static Monster Bandit = new Monster()
        {
            Name = "Bandit",
            EncounterText = "You hear a snigger and see the glint of a knife in the vegetation nearby. A bandit attacks!",
            Level = 2,
            Fitness = 5,
            Might = 2,
            Wits = 3,
            GoldSpoils = Tx.RandomInt(5, 14),
            ItemDrops = new int[]
            {
                ItemData.GetItem("signet ring").ID
            }
        };

        public static Monster HorseflySwarm = new Monster()
        {
            Name = "Swarm of Horseflies",
            EncounterText = "A loud buzzing fills the air, and a cloud of biting horseflies swarms around you!",
            Level = 2,
            Fitness = 5,
            Might = 1,
            Wits = -1
        };

        // ========= END MASTER LIST ========= //

        public static List<Monster> Monsters = new List<Monster>{
            BigRat,
            GoldRat,
            Bandit
        };

        public static Monster GetMonster(string MonsterName)
        {
            for (int i = 0; i < Monsters.Count; i++)
            {
                if (MonsterName.ToLower() == Monsters[i].Name.ToLower())
                {
                    return Monsters[i];
                }
            }
            return Monsters[0];
        }
    }
}

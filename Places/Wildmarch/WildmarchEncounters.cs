using System;
using Reliquary.GameMenus;
using Reliquary.WorldData;
using Reliquary.Classes;

namespace Reliquary.Places
{
    class WildmarchEncounters
    {
        /* ENCOUNTER LIST:
         - Abandoned Campsite (random item or amount of gold)
         - Path to Ironcliff Mountains
         - Wandering traveler (50% bandit/swindler, 50% kind traveler)
         - Magic Pond (+ ventures)
         - Tree (rest; if Wits high enough, notice something buried nearby)

        MONSTER LIST:
         - Big Rat
         - Gold Rat (limited time only)
         - Bandit
         - Swarm of Horseflies
         
         */
        public static void Encounter(int VentureCost)
        {
            int Experience = 0;
            Console.Clear();
            if (Character.Ventures - VentureCost < 0)
            {
                Console.WriteLine("You're too tired to venture anymore today.\n");
                return;
            }            
            else
            {
                Character.Ventures -= VentureCost;
            }
            int EncounterIndex = Tx.RandomInt(1, 100);
            Console.Clear();
            // 70% chance of combat
            if (EncounterIndex <= 70)
            {
                int MonsterIndex = Tx.RandomInt(1, 10);
                string Monster = "";
                if (MonsterIndex <= 4)
                {
                    Monster = "Big Rat";
                }
                else if (MonsterIndex <= 7)
                {
                    Monster = "Swarm of Horseflies";
                }
                else if (MonsterIndex <= 9)
                {
                    Monster = "Bandit";
                }
                else
                {
                    Monster = "Gold Rat"; //Limited time only
                }
                CombatController.Combat(MonsterData.GetMonster(Monster));
            }
            else
            {
                int RandomEncounter = Tx.RandomInt(1, 5);
                switch (RandomEncounter)
                {
                    case 1:
                        Tx.Emphasis("Abandoned Campsite\n", "blue");
                        Console.WriteLine("The remains of a campfire and some ripped tents lie scattered on the ground.");
                        if (Character.WitsCheck(5))
                        {
                            int GoldFind = Tx.RandomInt(2, 6);
                            Console.WriteLine("You manage to find " + GoldFind + " gold coins discarded nearby.\n");
                            Character.Gold += GoldFind;
                        }
                        else
                        {
                            Console.WriteLine("You find nothing of use here.\n");
                        }
                        break;
                    case 2:
                        if (Character.WitsCheck(10))
                        {
                            Console.Write("You discover the path to ");
                            Tx.Emphasis("Ironcliff Mountains", "cyan");
                            Console.Write("!\n\n");
                            Discoveries.IroncliffMountains = true;
                        }
                        else
                        {
                            goto case 1;
                        }
                        break;
                    case 3:
                        Tx.Emphasis("Wandering Traveler\n", "blue");
                        Console.WriteLine("A weary-looking man with a walking stick stumbles out of the tall grass.");
                        bool IsABandit = false;
                        if (Tx.RandomInt(1, 2) == 2)
                        {
                            IsABandit = true;
                        }
                        Tx.Emphasis("\"Please, good " + Character.Gender.ToLower() + ", could you help me find the way to the nearest town?\"\n", "gold");
                        int Response = Game.Choice(new string[] { "Sure thing.", "Sorry, but no." });
                        Console.Clear();
                        Tx.Emphasis("Wandering Traveler\n", "blue");
                        switch (Response)
                        {
                            case 1:
                                if (IsABandit)
                                {
                                    Console.WriteLine("As you draw near to the man, he pulls out a knife and puts it to your throat!");
                                    if (Character.Gold > 0)
                                    {
                                        if (Character.WinsMightContest(2))
                                        {
                                            Console.WriteLine("He tries to grab your coin purse, but you shove him to the ground. He " +
                                                "stumbles to his feet and runs away into the grass.\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("He grabs your coinpurse, and before you can stop him, he shoves you aside " +
                                            "and disappears into the grass.");
                                            int Stolen = Character.Level + Tx.RandomInt(2, 12);
                                            if (Character.Gold - Stolen <= 0)
                                            {
                                                Console.WriteLine("The thief made off with the rest of your gold!\n");
                                                Character.Gold = 0;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The thief made off with " + Stolen + " gold!\n");
                                                Character.Gold -= Stolen;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("He tries to grab your coin purse, then realizes you don't have any gold.");
                                        Tx.Emphasis("\"You gotta be kidding me!\"\n", "gold");
                                        Console.WriteLine("The thief shoves you aside and runs away into the tall grass.\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You spend some time directing the traveler toward Merrydale. Grateful, he hands you a" +
                                        "few coins for your trouble.");
                                    int Reward2 = Tx.RandomInt(4, 8);
                                    Console.WriteLine("You received " + Reward2 + " gold!\n");
                                    Character.Gold += Reward2;
                                }
                                break;
                            case 2:
                                if (IsABandit)
                                {
                                    Console.WriteLine("The traveler glares at you angrily as you go on your way.\n");
                                }
                                else
                                {
                                    Console.WriteLine("The traveler sighs sadly and stumbles off into the field alone.\n");
                                }
                                break;
                        }
                        break;
                    case 4:
                        Tx.Emphasis("Magic Pond\n", "blue");
                        Console.WriteLine("You decide to take a rest by a crystal pond. Drinking its clear water, you feel supernaturally refreshed!");
                        int Reward = Tx.RandomInt(2, 4);
                        Console.WriteLine("You gained " + Reward + " ventures!\n");
                        Character.Ventures += Reward;
                        break;
                    case 5:
                        Tx.Emphasis("Discovery\n", "blue");                        
                        string[] Discovery = new string[] {
                            "the ruins of a magnificent mage tower.",
                            "a moss-covered statue of an armored knight.",
                            "a blessing of unicorns in a clearing ahead.",
                            "a circle of stones, vibrating with a magical hum.",
                            "the remains of an ancient cobblestone road, covered with grass.",
                            "a flock of griffins soaring across the sky."
                        };
                        Console.Write("You are inspired at the sight of " + Discovery);
                        Experience = Tx.RandomInt(2,18);
                        Console.Write("You gained " + Experience + " experience points!");
                        Character.AddExp(Experience);
                        break;
                }
            }
            Tx.Emphasis("Press any key to continue.", "cyan");
            Console.ReadKey();
            Console.Clear();
            Character.AddExp(Experience);
        }        
    }
}

using System;
using System.Collections.Generic;
using Reliquary.Classes;

namespace Reliquary.GameMenus
{
    public class CombatController
    {
        public static Monster Enemy;
        public static bool CombatOver = false;
        public static bool FledFromCombat = false;
        public static string[] MissText = new string[] {
                "                    Miss!\n",
                "                    Blocked!\n",
                "                    Dodged!\n",
                "                    Parried!\n"
            };

        public static void Combat(Monster enemy)
        {
            Enemy = enemy;
            Enemy.CurrentFitness = Enemy.Fitness;
            bool Surprised = false;
            int Round = 0;

            string[] VictoryMessage = {
                "You have successfully defeated the " + Enemy.Name.ToLower() + "!",
                "The " + Enemy.Name.ToLower() + " was defeated!",
                "You defeated the " + Enemy.Name.ToLower() + ". Victory is yours!"
            };

            Console.WriteLine(Enemy.EncounterText);
            CombatOver = false;
            FledFromCombat = false;
            if (Character.WinsWitsContest(Enemy.Wits))
            {
                Console.WriteLine("You get the jump on it!\n"); // Should be one of a few possible phrases
            }
            else
            {
                Console.WriteLine("The " + Enemy.Name + " is quicker than you and attacks!\n");
                Surprised = true;
            }
            Tx.Emphasis("Press any key to enter combat!", "gold");
            Console.ReadKey();

            while (!CombatOver)
            {
                Console.Clear();
                if (Round == 0 && Surprised)
                {
                    DisplayRound(Round);
                    Console.WriteLine("");
                    EnemyAction();
                    Tx.Emphasis("\nPress any key", "gold");
                    Console.ReadKey();
                    Round++;
                }
                else
                {
                    DisplayRound(Round);
                    string Action = CombatMenu();
                    if (!Surprised)
                    {
                        DisplayRound(Round);
                        Console.WriteLine("");
                        PlayerAction(Action);
                        Tx.Emphasis("\nPress any key", "gold");
                        Console.ReadKey();
                        Surprised = true;
                    }
                    else
                    {
                        AttackRound(Round, Action);
                    }
                    Round++;
                }
            }
            if (FledFromCombat == true)
            {
                // You ran away
                Console.Clear();
                Console.WriteLine("You flee, the sounds of the " + Enemy.Name.ToLower() + " fading in the distance behind you.");
            }
            if (Enemy.CurrentFitness < 1)
            {
                // The monster died
                Console.Clear();
                Console.WriteLine(VictoryMessage[Tx.RandomInt(0, VictoryMessage.Length - 1)]);
                Enemy.Loot();
                Character.AddExp(Character.Level*2);
            }
            if (Character.Fitness < 1)
            {
                // You died
                Console.Clear();
                Console.WriteLine("Mortally injured, your body falls to the ground as the world fills with darkness.");
                Console.WriteLine("You wake up at Wildmarch Monastery, being tended to by monks."); //Might be different depending on when you die
                Character.Fitness = 1;
                if (Character.Gold > 1)
                {
                    int HalfGold = Character.Gold / 2;
                    Console.WriteLine(HalfGold + " gold is missing from your inventory.");
                    Character.Gold -= HalfGold;
                    //Shoot, now I need to redo how I do navigation. -_-
                }
                if (Character.Ventures / 2 > 0)
                {
                    Console.WriteLine("You missed out on " + Character.Ventures / 2 + " ventures today.");
                    if (Character.Ventures == 1)
                    {
                        Console.WriteLine("You missed out on 1 venture today.");
                    }
                    Character.Ventures /= 2;
                }
            }
            // You lived
            Console.WriteLine("");
        }

        private static void EnemyAction()
        {
            if (Enemy.CurrentFitness > 0 && !FledFromCombat)
            {
                Console.WriteLine("The " + Enemy.Name.ToLower() + " strikes you..."); //Random assortment
                if (!Character.WinsMightContest(Enemy.Might))
                {
                    int CritConfirm = Tx.RandomInt(1, 20);
                    int Damage = Enemy.Might; // leveld4 + Might modifier
                    for (int i = 0; i < Enemy.Level; i++)
                    {
                        Damage += Tx.RandomInt(1, 4); //Might be too much. Definitely test.
                    }
                    if (CritConfirm != 20)
                    {
                        Tx.Emphasis("                    Hit!\n", "lightred");
                    }
                    else
                    {
                        Tx.Emphasis("                    CRTICAL STRIKE!\n", "red");
                        Damage *= 2;
                    }
                    Tx.Emphasis("                        (" + Damage + " damage)\n", "red"); // DEBUG
                    Character.Fitness -= Damage;
                    if (Character.Fitness < 1)
                    {
                        Console.WriteLine("You have fallen!");
                        CombatOver = true;
                    }
                }
                else
                {
                    Console.Write(MissText[Tx.RandomInt(0, MissText.Length - 1)]);
                }
            }
        }

        private static void PlayerAction(string Action)
        {
            if (Character.Fitness > 0)
            {
                if (Action == "Attack!")
                {
                    Console.WriteLine("You strike the " + Enemy.Name.ToLower() + "...");
                    if (Character.WinsMightContest(Enemy.Might))
                    {
                        int CritConfirm = Tx.RandomInt(1, 20);
                        int Damage = Character.Might; // leveld4 + Might modifier
                        for (int i = 0; i < Character.Level; i++)
                        {
                            Damage += Tx.RandomInt(1, 4); //Might be too much. Definitely test.
                        }
                        if (CritConfirm != 20)
                        {
                            Tx.Emphasis("                    Hit!\n", "lightred");
                        }
                        else
                        {
                            Tx.Emphasis("                    CRTICAL STRIKE!\n", "gold");
                            Damage *= 2;
                        }
                        Tx.Emphasis("                        (" + Damage + " damage)\n", "green"); // DEBUG
                        Enemy.CurrentFitness -= Damage;
                        if (Enemy.CurrentFitness < 1)
                        {
                            Console.WriteLine(Enemy.Name + " DEFEATED!");
                            CombatOver = true;
                        }
                    }
                    else
                    {
                        Console.Write(MissText[Tx.RandomInt(0, MissText.Length - 1)]);
                    }
                }
                else if (Action == "Run")
                {
                    Console.WriteLine("You turn to run...");
                    if (Character.WinsWitsContest(Enemy.Wits - 2))
                    {
                        Tx.Emphasis("                    Got away!", "green");
                        FledFromCombat = true;
                        CombatOver = true;
                    }
                    else
                    {
                        Tx.Emphasis("                    Failed to escape!", "red");
                        FledFromCombat = true;
                        CombatOver = true;
                    }
                }
            }
        }

        private static void AttackRound(int round, string action)
        {
            if (Character.WinsWitsContest(Enemy.Wits))
            {
                DisplayRound(round);
                PlayerAction(action);
                EnemyAction();
            }
            else
            {
                DisplayRound(round);
                EnemyAction();
                PlayerAction(action);
            }
            if (!FledFromCombat)
            {
                Tx.Emphasis("\nPress any key", "gold");
            }
            Console.ReadKey();
        }

        private static string CombatMenu()
        {
            string PlayerFitness = "green";
            string EnemyFitness = "green";
            string Fitness = Character.ShowFitness();
            if (Fitness == "Bruised")
            {
                PlayerFitness = "gold";
            }
            else if (Fitness == "Injured")
            {
                PlayerFitness = "lightred";
            }
            else if (Fitness == "Heavily Wounded")
            {
                PlayerFitness = "red";
            }
            Fitness = Enemy.ShowFitness();
            if (Fitness == "Bruised")
            {
                EnemyFitness = "gold";
            }
            else if (Fitness == "Injured")
            {
                EnemyFitness = "lightred";
            }
            else if (Fitness == "Heavily Wounded")
            {
                EnemyFitness = "red";
            }
            Console.WriteLine("\n " + Character.Gender + " " + Character.Name + "\t VS. \t" + Enemy.Name);
            Console.Write("\n ");
            Tx.Emphasis(Character.ShowFitness(), PlayerFitness);
            Console.Write("\t     \t");
            Tx.Emphasis(Enemy.ShowFitness(), EnemyFitness);
            //Maybe you could even put debuffs here, like POISONED or STUNNED or something!
            Console.WriteLine("\n\nMake your choice:");
            List<string> choices = new List<string>
            {
                "Attack!",
                //"Defend",
                "Run",
                //"Use Item",
            };
            for (int i = 0; i < choices.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("  " + (i + 1));
                Console.ResetColor();
                Console.WriteLine(") " + choices[i]);
            }
            bool AcceptableResponse = false;
            int ResponseInt = -1;
            while (!AcceptableResponse)
            {
                Console.Write(">");
                string Response = Console.ReadLine();
                for (int i = 0; i < choices.Count; i++)
                {
                    ResponseInt = i + 1;
                    if (ResponseInt.ToString() == Response)
                    {
                        AcceptableResponse = true;
                        break;
                    }
                }
            }
            return choices[ResponseInt - 1];
        }

        private static void DisplayRound(int round)
        {
            Console.Clear();
            Tx.Emphasis("===███=███=█===█=██===█==███==\n", "lightred");
            Tx.Emphasis("===█===█=█=██=██=█=█=███==█===\n", "lightred");
            Tx.Emphasis("===███=███=█=█=█=███=█=█==█===\n", "lightred");
            if (round == 0)
            {
                Tx.Emphasis("======= SURPRISE ROUND =======\n", "lightred");
            }
            else
            {
                if (round < 10)
                {
                    Tx.Emphasis("========== ROUND " + round + " ===========\n", "lightred");
                }
                else
                {
                    Tx.Emphasis("========= ROUND " + round + " ===========\n", "lightred");
                }
            }
        }
    }
}

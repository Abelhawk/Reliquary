using System;
using System.Collections.Generic;
using Reliquary.GameMenus;
using Reliquary.WorldData;

namespace Reliquary.Places
{
    class Merrydale
    {
        //New locations might be unlocked later.   
        public static List<string> LocationOptionsList = new List<string> {
                "Tavern",
                "General Store",
                "Smithy",
                "Wildmarch Field (1)"
            };

        public static bool Travel()
        {
            string[] LocationOptions = LocationOptionsList.ToArray();
            int Choice = Game.Choice(LocationOptions);
            Choice--;

            if (LocationOptions[Choice] == "Tavern")
            {
                return Tavern();
            }
            else if (LocationOptions[Choice] == "General Store")
            {
                Console.Clear();
                Console.WriteLine("You want to go to the general store? What are you, a shopaholic?\n");
            }
            else if (LocationOptions[Choice] == "Smithy")
            {
                Smithy();
            }
            else if (LocationOptions[Choice] == "Wildmarch Field (1)")
            {
                Console.Clear();
                if (Character.Ventures > 0)
                {
                    Character.CurrentLocation = 1;
                    NavigationController.LoadPlace("Travel", 1);
                }
                return (Character.Ventures > 0);
            }
            return false;
        }

        public static bool Tavern()
        {
            string[] Descriptions = {
                    "You approach the bar and are greeted by a smiling woman.",
                    "You sit down at a table and a barmaid comes by with a tray of mugs.",
                    "A bard plays a rousing song amid the noisy clientele of this inn."
                };
            string[] Dialogs = {
                    "\"Sit down, sit down! Ya look parched. What can I get ya?\"",
                    "\"Welcome! What's your pleasure?\"",
                    "\"Need a bed for the night? Or something to wet your whistle?'\""
                };
            string[] Goodbyes =
            {
                "\"Come back with stories of your adventures!\"",
                "\"See ya soon!\"",
                "\"Take care!\""
            };
            string[] Rumors = //More might be added as things get unlocked. Another save file field?
            {
                "\"Did you hear the giant threw up? It's all over town.\"",
                "\"Did you hear about the circus fire? It was intense.\"",
                "\"Did you hear the rumor about butter? Well, I'm not going to spread it.\""
            };
            string[] SleepTight =
            {
                "\"Hope ya sleep well!\"",
                "\"I'll see ya in the morning!\"",
                "\"Dream with the gods, dear!\""
            };
            string[] SleepOptions =
            {
                "The floor (3)",
                "Upstairs room (15)",
                "Private suite (50)",
                "Never mind."
            };
            bool JustEntering = true;
            string Description = Tx.RandomString(Descriptions);
            string Dialog = Tx.RandomString(Dialogs);
            string ByeSleep = Tx.RandomString(SleepTight) + "\n\n";
            string SleepMessage = "";
            string Goodbye = Tx.RandomString(Goodbyes) + "\n\n";
            string[] Options = { "Buy a drink (2 gold)", "Rumors", "Sleep", "Exit" };
            bool Stay = true;
            Console.Clear();
            while (Stay)
            {
                Console.Clear();
                Tx.Emphasis("The Horn and Lantern Tavern\n", "cyan");
                if (JustEntering)
                {
                    Console.WriteLine(Description);
                    JustEntering = false;
                }
                Tx.Emphasis(Dialog + "\n", "gold");
                int Answer = Game.Choice(Options);
                switch (Answer)
                {
                    case 1:
                        Console.Clear();
                        if (Character.Gold < 2)
                        {
                            Dialog = "\"Looks like you're out of coin, dear.\"";
                        }
                        else
                        {
                            Dialog = "\"Here ya go! Doesn't do much yet, but maybe someday it'll give you Might and cost Wits for the rest of the day? I dunno.\""; //todo
                            //Do something
                            Character.Gold -= 2;
                        }
                        break;
                    case 2:
                        string Rumor = Tx.RandomString(Rumors);
                        Dialog = Rumor;
                        break;
                    case 3:
                        Console.Clear();
                        Tx.Emphasis("The Horn and Lantern Tavern\n", "cyan");
                        Tx.Emphasis("\"Where would ya like to sleep tonight?\"\n", "gold");
                        int SleepChoice = Game.Choice(SleepOptions);
                        if (SleepChoice != 4)
                        {
                            int Cost = 0;
                            int Quality = 1;
                            switch (SleepChoice)
                            {
                                case 1:
                                    Cost = 3;
                                    Quality = 1;
                                    SleepMessage = "You eat some leftover watered-down soup and curl up on a thin mat in front of the fire.";
                                    Character.CurrentLocation = 1;
                                    break;
                                case 2:
                                    Cost = 15;
                                    Quality = 3;
                                    SleepMessage = "You eat some brown bread and pottage with roast carrots, then go upstairs into your locked room, wash " +
                                        "your hands and face, and lie down on a straw-filled mattress.";
                                    Character.CurrentLocation = 2;
                                    break;
                                case 3:
                                    Cost = 50;
                                    Quality = 5;
                                    SleepMessage = "You enjoy a sumptuous meal of spiced mutton, cheese, and wine, then go upstairs for a bath and massage " +
                                        "from an attractive servant, then draw the curtains of a comfortable, down-filled bed.";
                                    Character.CurrentLocation = 3;
                                    break;
                            }
                            if (Character.Gold < Cost)
                            {
                                Dialog = "\"Ooh, I think that's a bit too costly for ya, dear.\"";
                            }
                            else
                            {
                                Console.Clear();
                                Tx.Emphasis("The Horn and Lantern Tavern\n", "cyan");
                                Tx.Emphasis("\"" + Cost + " gold alright with ya?\"\n", "gold");
                                string[] YesNo = { "Yes", "No" };
                                int Confirm = Game.Choice(YesNo);
                                if (Confirm == 1)
                                {
                                    Console.Clear();
                                    Tx.Emphasis(ByeSleep, "gold");
                                    Console.WriteLine(SleepMessage);
                                    SaveLoadController.SaveGame(Quality);
                                    return true;
                                }
                                else
                                {
                                    Dialog = "\"Oh. Well, can I do anything else for ya?\"";
                                }
                            }
                        }
                        else
                        {
                            Dialog = "\"What else do ya need?\"";
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Tx.Emphasis(Goodbye, "gold");
                        Stay = false;
                        break;
                }
            }
            return false;
        }

        public static void Smithy()
        {
            string[] Descriptions = {
                    "A man with an auburn beard looks up from helping his apprentice.",
                    "A strong blacksmith hammers away at a horseshoe on an anvil.",
                    "You meet the owner of this establishment, Kalahan Smith."
                };
            string[] Dialogs = {
                    "\"What can I help you with?\"",
                    "\"I'll be right with you. I'm almost finished.\"",
                    "\"You say 'adventurer,' I say 'rat-catcher.'\""
                };
            string[] Goodbyes =
            {
                "\"Stay safe out there.\"",
                "\"Close the door on your way out.\"",
                "\"Bye.\""
            };
            bool JustEntering = true;
            string Description = Tx.RandomString(Descriptions);
            string Dialog = Tx.RandomString(Dialogs);
            string Goodbye = Tx.RandomString(Goodbyes) + "\n\n";
            string[] Options = { "Buy", "Sell", "Upgrade", "Exit" };
            bool Stay = true;
            Console.Clear();
            while (Stay)
            {
                Tx.Emphasis("Kalahan's Smithy\n", "cyan");
                if (JustEntering)
                {
                    Console.WriteLine(Description);
                    JustEntering = false;
                }
                Tx.Emphasis(Dialog, "gold");
                Console.WriteLine("");
                int Answer = Game.Choice(Options);
                switch (Answer)
                {
                    case 1:
                        Console.Clear();
                        Dialog = "\"Hoho! It'll be a while till Austin programs shops in this game. Come back in a few Git pushes.\"";
                        break;
                    case 2:
                        Console.Clear();
                        Dialog = "\"My 'shopGold' variable isn't even defined yet. Come back in a few code changes.\"";
                        break;
                    case 3:
                        Console.Clear();
                        Dialog = "\"Sorry, I don't have any materials to upgrade with. Come back in a few patches.\"";
                        break;
                    case 4:
                        Console.Clear();
                        Tx.Emphasis(Goodbye, "gold");
                        Stay = false;
                        break;
                }
            }
        }
    }
}

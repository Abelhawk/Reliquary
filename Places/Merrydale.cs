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
                "Wildmarch Field(1)"
            };

        public static bool Travel()
        {
            string[] LocationOptions = LocationOptionsList.ToArray();
            int Choice = Game.Choice(LocationOptions);
            Choice--;

            if (LocationOptions[Choice] == "Tavern")
            {
                Console.Clear();
                Console.WriteLine("You want to go to the tavern? What are you, a drunk?\n");
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
            else if (LocationOptions[Choice] == "Wildmarch Field(1)")
            {
                Console.Clear();
                NavigationController.LoadPlace(1, "Travel", 1);
                return (Character.Ventures > 0);
            }
            return false;
        }
    

    public static void Smithy()
    {
        string[] Descriptions = {
                    "A man with an auburn beard looks up from helping his apprentice.",
                    "A strong blacksmith hammers away at a horseshoe on an anvil.",
                    "You meet the owner of this establishment, Calahan Smith."
                };
        string[] Dialogs = {
                    "\"What can I help you with?\"",
                    "\"I'll be right with you. I'm almost finished.\"",
                    "\"You say 'adventurer,' I say 'rat-catcher.'\""
                };
        bool JustEntering = true;
        string Description = Tx.RandomString(Descriptions);
        string Dialog = Tx.RandomString(Dialogs);
        string[] Options = { "Buy", "Sell", "Upgrade", "Exit" };
        bool Stay = true;
        Console.Clear();
        while (Stay)
        {
            Tx.Emphasis("Calahan's Smithy\n", "cyan");
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
                    Stay = false;
                    break;
            }
        }
    }
}
}

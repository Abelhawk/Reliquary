using System;

namespace Reliquary
{
    public class Inventory
    {
        static public void LoadItem(int id)
        {
            //Load data from external file or array or something
            Tx.Emphasis("The ID for the loaded item is " + id + ", but we're just gonna give you another signet ring.\n", "red");
            AddSignetRing();
        }

        static public void AddSignetRing()
        {
            //Who knew one dynasty would have so many signet rings?
            Item NewItem = new Item();
            NewItem.Name = "signet ring";
            NewItem.Description = "A ring bestowed upon you by your noble ancestors.";
            Character.Inventory.Add(NewItem);
        }
    }
}

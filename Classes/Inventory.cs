using System;
using Reliquary.WorldData;

namespace Reliquary
{
    public class Inventory
    {
        public static void AddItem(int itemID)
        {
            Character.Inventory.Add(ItemData.Items[itemID]);
        }
    }
}

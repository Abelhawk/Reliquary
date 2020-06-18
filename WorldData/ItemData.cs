using System.Collections.Generic;

namespace Reliquary.WorldData
{
    public class ItemData
    {
        // ========= MASTER ITEM LIST ========= //

        public static Item SignetRing = new Item()
        {
            ID = 0,
            Name = "signet ring",
            Description = "A ring bestowed upon you by your noble ancestors.",
            Value = 120
        };

        public static Item Garnet = new Item()
        {
            ID = 1,
            Name = "garnet",
            Description = "Looks like it's worth a pretty penny!",
            Value = 10
        };

        // ========= END MASTER LIST ========= //

        public static List<Item> Items = new List<Item>{
            SignetRing,         // 0
            Garnet              // 1
        };

        public static Item GetItem(string ItemName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (ItemName.ToLower() == Items[i].Name.ToLower())
                {
                    return Items[i];
                }
            }
            return Items[0];
        }
    }
}

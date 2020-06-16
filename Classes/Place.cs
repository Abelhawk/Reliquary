using System;
using System.Collections.Generic;
using System.Text;
using Reliquary.WorldData;

namespace Reliquary
{
    public class Place
    {
        public int ID = -1;
        public string Name = "error";
        public string Description1 = "error";
        public string Description2 = "error";
        public string WakeUp = "error";
        public string Travel = "error";
        public string[] Options = { "Self", "Inventory", "Travel", "Sleep" };
        public string SleepConfirm = "error";
        public string GoToSleep = "error";

        public Place(
            int id,
            string name,
            string desc1,
            string desc2,
            string wakeUp,
            string travel,
            string sleepConfirm,
            string sleep
            )
        {
            ID = id;
            Name = name;
            Description1 = desc1;
            Description2 = desc2;
            WakeUp = wakeUp;
            Travel = travel;
            SleepConfirm = sleepConfirm;
            GoToSleep = sleep;
        }
        
    }
}

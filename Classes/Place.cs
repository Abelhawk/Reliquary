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
        public string[] WakeUpMessages = { "error" };
        public string Travel = "error";
        public string[] Options = { "Self", "Inventory", "Travel", "Sleep" };
        public string SleepConfirm = "error";
        public string GoToSleep = "error";       
    }
}

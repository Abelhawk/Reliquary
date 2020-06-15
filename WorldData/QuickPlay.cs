using System;
using System.Collections.Generic;
using System.Text;

// This class is used only for debug purposes.

namespace Reliquary.WorldData
{
    class QuickPlay
    {
        public static void QuickLoad()
        {
            Character.Name = "D'bugr";       
            Character.Gender = "Sir";
            Character.CurrentLocation = 0;
            Character.Might = 1;
            Character.Fitness = 3;
            Character.Wits = 1;
            Character.ExperiencePoints = 0;
            Character.Level = 1;
            Character.Gold = 5;
            Character.Ventures = 3;
        }
    }
}

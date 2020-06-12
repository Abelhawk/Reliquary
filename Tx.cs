using System;
using System.Collections.Generic;
using System.Text;

namespace Reliquary
{
    public class Tx
    {
        internal static void Emphasis(object message, string color)
        {
            // Currently supports cyan, gold, red, green, gray
            if (color.ToLower() == "cyan")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (color.ToLower() == "gold")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (color.ToLower() == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (color.ToLower() == "green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (color.ToLower() == "gray")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Incorrect color input.");
                Console.ResetColor();
            }
            Console.Write(message);
            Console.ResetColor();
        }

        internal static string GetGameDate(string date, string output)
        {
            DateTime FullDate = Convert.ToDateTime(date);
            string Month = "";
            int Day = FullDate.Day;
            string DayOrdinal = Day.ToString();
            int Weekday = Convert.ToInt32(FullDate.DayOfWeek);
            int Year = FullDate.Year;
            string HearthYear = Year-1449 + " AR";

            //Convert to Hearth Month
            switch (FullDate.Month)
            {
                case 0:
                    Month = "Mudmoon";
                    break;
                case 1:
                    Month = "Swiftmoon";
                    break;
                case 2:
                    Month = "Greenmoon";
                    break;
                case 3:
                    Month = "Thricemilk";
                    break;
                case 4:
                    Month = "Earlymild";
                    break;
                case 5:
                    Month = "Lattermild";
                    break;
                case 6:
                    Month = "Weedmoon";
                    break;
                case 7:
                    Month = "Harvestmoon";
                    break;
                case 8:
                    Month = "Wintermoon";
                    break;
                case 9:
                    Month = "Bloodmoon";
                    break;
                case 10:
                    Month = "Earlyfest";
                    break;
                case 11:
                    Month = "Latterfest";
                    break;
            }
            switch (Day % 10)
            {
                case 1:
                    DayOrdinal += "st";
                    break;
                case 2:
                    DayOrdinal += "nd";
                    break;
                case 3:
                    DayOrdinal += "rd";
                    break;
                default:
                    DayOrdinal += Day + "th";
                    break;
            }
            switch (Day)
            {
                case 11:
                    DayOrdinal = "11th";
                    break;
                case 12:
                    DayOrdinal = "12th";
                    break;
                case 13:
                    DayOrdinal = "13th";
                    break;
            }

            switch (output)
            {
                case "full":
                    return DayOrdinal + " of " + Month + ", " + HearthYear;
                default:
                    return date;
            }
            /*
             There should be some kind of benefit for logging on on Christmas Day, Halloween, and
             maybe like my birthday just for kicks.
             */
        
        }

        internal static string RandomString(string[] PossibleStrings)
        {
            Random rando = new Random();
            int n = rando.Next(0, PossibleStrings.Length);
            return PossibleStrings[n];
        }
    }
}

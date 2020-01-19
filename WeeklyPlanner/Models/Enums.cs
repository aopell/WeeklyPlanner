using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyPlanner.Models
{
    [Flags]
    public enum Weekdays
    {
        None = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
        NextWeek = 128
    }

    [Flags]
    public enum TextFormatting
    {
        None = 0,
        Bold = 1,
        Italics = 2
    }
}

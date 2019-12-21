using System;

namespace WeeklyPlanner.Items
{
    public class PlannerItem
    {
        public string Title { get; set; }
        public TextFormatting TextFormatting { get; set; } = TextFormatting.None;
        public Weekdays Weekdays { get; set; } = Weekdays.None;
    }

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
        Italics = 2,
        Underline = 4
    }
}

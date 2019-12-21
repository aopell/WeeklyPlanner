using System.Collections.Generic;

namespace WeeklyPlanner.Items
{
    public class ChecklistItem
    {
        public string Title { get; set; }
        public bool MainList { get; set; }
        public List<string> SubParts { get; set; }
        public TextFormatting TextFormatting { get; set; }
    }
}

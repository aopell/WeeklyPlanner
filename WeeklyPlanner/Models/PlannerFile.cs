using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyPlanner.Models
{
    public class PlannerFile
    {
        public DateTime StartDate { get; set; }
        public List<LegendItem> LegendItems { get; }
        public List<EventItem> Items { get; } = new List<EventItem>();

        [JsonIgnore]
        public IEnumerable<ChecklistEntry> MainListItems => Items.Where(x => x?.ChecklistEntry.MainList ?? false).Select(x => x.ChecklistEntry);
        [JsonIgnore]
        public IEnumerable<ChecklistEntry> SecondaryListItems => Items.Where(x => !(x?.ChecklistEntry.MainList ?? false)).Select(x => x.ChecklistEntry);

        public PlannerFile()
        {
            StartDate = DateTime.Today;
            Items = new List<EventItem>();
            LegendItems = new List<LegendItem>();
        }
    }
}

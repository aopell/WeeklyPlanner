using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeeklyPlanner.Models
{
    public class PlannerFile
    {
        public DateTime StartDate { get; set; }
        public List<LegendItem> LegendItems { get; }
        public List<EventItem> Items { get; }
        public bool AddExtraCheckBoxes { get; set; }

        [JsonIgnore]
        public IEnumerable<ChecklistEntry> MainListItems => Items.Where(x => x?.ChecklistEntry.MainList ?? false).Select(x => x.ChecklistEntry);
        [JsonIgnore]
        public IEnumerable<ChecklistEntry> SecondaryListItems => Items.Where(x => !(x?.ChecklistEntry.MainList ?? false)).Select(x => x.ChecklistEntry);

        [JsonIgnore]
        private static readonly LegendItem DefaultLegendItem = new LegendItem { Color = Colors.Black, Name = "LEGEND ITEM NOT FOUND" };
        public LegendItem GetLegendItemById(Guid id) => LegendItems.FirstOrDefault(x => x.ID == id) ?? DefaultLegendItem;

        public PlannerFile()
        {
            StartDate = DateTime.Today;
            Items = new List<EventItem>();
            LegendItems = new List<LegendItem>();
            AddExtraCheckBoxes = false;
        }
    }
}

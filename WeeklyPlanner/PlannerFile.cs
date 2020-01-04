using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeklyPlanner.Items;

namespace WeeklyPlanner
{
    public class PlannerFile
    {
        public DateTime StartDate { get; set; }
        public List<LegendItem> LegendItems { get; }

        public List<ToDoItem> Items { get; } = new List<ToDoItem>();

        [JsonIgnore]
        public IEnumerable<ChecklistItem> MainListItems => Items.Where(x => x?.ChecklistItem.MainList ?? false).Select(x => x.ChecklistItem);
        [JsonIgnore]
        public IEnumerable<ChecklistItem> SecondaryListItems => Items.Where(x => !(x?.ChecklistItem.MainList ?? false)).Select(x => x.ChecklistItem);

        public IEnumerable<PlannerItem> GetDailyItems(Weekdays weekdays) => Items.Where(x => x.PlannerItem != null && (x.PlannerItem.Weekdays & weekdays) == weekdays).Select(x => x.PlannerItem);

        public PlannerFile()
        {
            StartDate = DateTime.Today;
            Items = new List<ToDoItem>();
            LegendItems = new List<LegendItem>();
        }
    }
}

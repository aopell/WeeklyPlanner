using System.Collections.Generic;
using System.Linq;

namespace WeeklyPlanner.Items
{
    public class PlannerItemCollection
    {
        public List<ToDoItem> Items { get; } = new List<ToDoItem>();

        public IEnumerable<ChecklistItem> MainListItems => Items.Where(x => x?.ChecklistItem.MainList ?? false).Select(x => x.ChecklistItem);
        public IEnumerable<ChecklistItem> SecondaryListItems => Items.Where(x => !(x?.ChecklistItem.MainList ?? false)).Select(x => x.ChecklistItem);

        public IEnumerable<PlannerItem> GetDailyItems(Weekdays weekdays) => Items.Where(x => x.PlannerItem != null && (x.PlannerItem.Weekdays & weekdays) == weekdays).Select(x => x.PlannerItem);
    }
}

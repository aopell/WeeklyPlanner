using System;

namespace WeeklyPlanner.Models
{
    public class EventItem
    {
        public Guid LegendItemId { get; set; }
        public string Title { get; set; }
        public ChecklistEntry ChecklistEntry { get; set; } = new ChecklistEntry();
        public PlannerEntry PlannerEntry { get; set; } = new PlannerEntry();
    }
}

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
        public PlannerItemCollection Items { get; }
        public List<LegendItem> LegendItems { get; }

        public PlannerFile()
        {
            StartDate = DateTime.Today;
            Items = new PlannerItemCollection();
            LegendItems = new List<LegendItem>();
        }
    }
}

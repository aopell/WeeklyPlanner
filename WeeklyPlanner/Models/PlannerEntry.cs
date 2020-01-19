using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyPlanner.Models
{
    public class PlannerEntry
    {
        public DayEntry[] DayEntries { get; set; } = new DayEntry[8];
    }
}

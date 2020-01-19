using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyPlanner.Models
{
    public class DayEntry
    {
        public TaskItem Task { get; set; } = new TaskItem();
        public uint Priority { get; set; } = uint.MinValue;
    }
}

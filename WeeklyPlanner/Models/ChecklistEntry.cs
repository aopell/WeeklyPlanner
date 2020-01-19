using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyPlanner.Models
{
    public class ChecklistEntry
    {
        public List<TaskItem> Subtasks { get; set; } = new List<TaskItem>();
        public uint Priority { get; set; } = uint.MinValue;
        public bool MainList { get; set; } = true;
    }
}

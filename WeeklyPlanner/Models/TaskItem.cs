using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyPlanner.Models
{
    public class TaskItem
    {
        public Guid LegendItemId { get; set; }
        public string Title { get; set; } = null;
        public TextFormatting TextFormatting { get; set; } = TextFormatting.None;
    }
}

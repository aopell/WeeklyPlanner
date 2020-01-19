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
    public class LegendItem
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Guid ID { get; } = Guid.NewGuid();

        [JsonIgnore]
        public ListBoxItem Display => new ListBoxItem
        {
            FontSize = 10,
            Content = $"■ {Name}",
            Foreground = new SolidColorBrush(Color)
        };
    }
}

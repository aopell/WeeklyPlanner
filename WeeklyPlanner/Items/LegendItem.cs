using System.Windows.Controls;
using System.Windows.Media;

namespace WeeklyPlanner.Items
{
    public class LegendItem
    {
        public string Name { get; }
        public Color Color { get; }

        public LegendItem(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public override string ToString() => $"■ {Name}";

        public ListBoxItem ToListBoxItem() => new ListBoxItem() { Content = this, Foreground = new SolidColorBrush(Color) };
    }
}

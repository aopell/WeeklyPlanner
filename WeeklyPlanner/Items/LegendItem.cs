using System.Windows.Controls;
using System.Windows.Media;

namespace WeeklyPlanner.Items
{
    public class LegendItem : ListBoxItem
    {
        private Color color;
        private string title;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                Content = $"■ {value}";
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Foreground = new SolidColorBrush(color);
            }
        }

        public LegendItem(string name, Color color)
        {
            Title = name;
            Color = color;
            FontSize = 10;
        }
    }
}

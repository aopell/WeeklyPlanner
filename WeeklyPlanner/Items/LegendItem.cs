using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeeklyPlanner.Items
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LegendItem2 : ListBoxItem
    {
        private Color color;
        private string title;

        [JsonProperty]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                // Content = $"■ {value}";
            }
        }

        [JsonProperty]
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Foreground = new SolidColorBrush(color);
            }
        }

        public static readonly new DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(LegendItem2));

        public new object Content
        {
            get => $"■ {Title}";
            set { }
        }

        public LegendItem2(string name, Color color) : this()
        {
            Title = name;
            Color = color;
        }

        public LegendItem2()
        {
            FontSize = 10;
        }

        //static LegendItem()
        //{
        //    ContentProperty.
        //    ContentProperty.OverrideMetadata(typeof(ListBoxItem), new PropertyMetadata(null, null, (l, v) => $"■ {(l as LegendItem).Title}"));
        //}
    }
}

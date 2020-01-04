using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeeklyPlanner.Items
{
    /// <summary>
    /// Interaction logic for LegendItem.xaml
    /// </summary>
    public partial class LegendItem : ListBoxItem, INotifyPropertyChanged
    {
        private string title;
        private Color color;

        public LegendItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public LegendItem(string name, Color color) : this()
        {
            Title = name;
            Color = color;
        }

        [JsonProperty]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(Display));
            }
        }

        [JsonProperty]
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                NotifyPropertyChanged(nameof(ForegroundColorBrush));
            }
        }

        public object Display => $"■ {Title}";
        public Brush ForegroundColorBrush => new SolidColorBrush(Color);

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

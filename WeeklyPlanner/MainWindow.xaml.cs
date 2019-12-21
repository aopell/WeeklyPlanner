using System;
using System.Collections.Generic;
using System.Linq;
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
using WeeklyPlanner.Items;

namespace WeeklyPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                UpdateDates(value);
            }
        }

        public PlannerItemCollection Items { get; } = new PlannerItemCollection();
        public List<LegendItem> LegendItems { get; } = new List<LegendItem>();

        private void PrintWindow(Visual v, string description, double widthInches = 8.5, double heightInches = 11)
        {
            Height = heightInches * 96 + 39;
            Width = widthInches * 96 + 16;

            // Print(MainGrid);

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(MainGrid, description);
            }
        }

        private void UpdateDates(DateTime date)
        {
            TitleLabel.Content = $"Week of {_startDate:dddd M/d} to {_startDate + TimeSpan.FromDays(6):dddd M/d}";
            var dayLabels = new[] { Day1Label, Day2Label, Day3Label, Day4Label, Day5Label, Day6Label, Day7Label };
            for (int i = 0; i < 7; i++)
            {
                dayLabels[i].Content = (date + TimeSpan.FromDays(i)).ToString("dddd M/d").ToUpper();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            StartDate = DateTime.Today;
        }

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartDate = new DatePicker(StartDate).PromptDate();
        }

        private void Legend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LegendItem item = new LegendItemDialog().GetItem();
            if (item != null)
            {
                LegendListBox.Children.Add(item.ToListBoxItem());
            }
        }
    }
}

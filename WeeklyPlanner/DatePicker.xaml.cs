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
using System.Windows.Shapes;

namespace WeeklyPlanner
{
    /// <summary>
    /// Interaction logic for DatePicker.xaml
    /// </summary>
    public partial class DatePicker : Window
    {
        public DateTime PreviousDate { get; }
        public DateTime SelectedDate { get; set; }

        public DatePicker(DateTime previousDate)
        {
            PreviousDate = previousDate;
            SelectedDate = previousDate;
            InitializeComponent();
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public DateTime PromptDate() => ShowDialog() == true ? SelectedDate : PreviousDate;
    }
}

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
using WeeklyPlanner.Controls;
using WeeklyPlanner.Models;

namespace WeeklyPlanner
{
    /// <summary>
    /// Interaction logic for EventDialog.xaml
    /// </summary>
    public partial class EventDialog : Window
    {
        public PlannerFile PlannerFile { get; }

        public EventDialog(PlannerFile file)
        {
            PlannerFile = file;
            InitializeComponent();
        }

        private void AddSubtask_Click(object sender, RoutedEventArgs e)
        {
            SubtaskListBox.Items.Add(new Subtask());
        }

        private void SubtaskListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Delete || e.Key == Key.Back) && SubtaskListBox.SelectedIndex != -1)
            {
                SubtaskListBox.Items.RemoveAt(SubtaskListBox.SelectedIndex);
            }
        }
    }
}

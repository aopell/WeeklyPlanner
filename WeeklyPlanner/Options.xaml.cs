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
using WeeklyPlanner.Models;

namespace WeeklyPlanner
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private PlannerFile File { get; }
        private Result ActionResult { get; set; } = Result.None;

        public float PageWidth => PageWidthSelector.Value ?? 8.5f;
        public float PageHeight => PageHeightSelector.Value ?? 11f;

        public Options(PlannerFile file)
        {
            File = file;
            InitializeComponent();
            AddExtraChecks.IsChecked = File.AddExtraCheckBoxes;
        }

        private void AddExtraChecks_CheckChanged(object sender, RoutedEventArgs e)
        {
            File.AddExtraCheckBoxes = AddExtraChecks.IsChecked.Value;
        }

        public Result ShowAndGetResult()
        {
            ShowDialog();
            return ActionResult;
        }

        public enum Result
        {
            None,
            Print,
            Open,
            Save
        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)
        {
            ActionResult = Result.Print;
            Close();
        }

        private void OpenButtonClick(object sender, RoutedEventArgs e)
        {
            ActionResult = Result.Open;
            Close();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            ActionResult = Result.Save;
            Close();
        }
    }
}

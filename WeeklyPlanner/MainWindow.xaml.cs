using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WeeklyPlanner.Items;

namespace WeeklyPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlannerFile CurrentFile { get; set; } = new PlannerFile();

        public MainWindow()
        {
            InitializeComponent();
            RenderCurrentFile();
        }

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

        public void RenderCurrentFile()
        {
            UpdateDates(CurrentFile.StartDate);
            UpdateLegendItems(CurrentFile.LegendItems);
            UpdatePlannerItems(CurrentFile.Items);
        }

        private void UpdatePlannerItems(PlannerItemCollection items)
        {

        }

        private void UpdateLegendItems(List<LegendItem> legendItems)
        {
            LegendListBox.Children.Clear();

            foreach (var item in legendItems)
            {
                LegendListBox.Children.Add(item);
            }
        }

        private void UpdateDates(DateTime date)
        {
            TitleLabel.Content = $"Week of {CurrentFile.StartDate:dddd M/d} to {CurrentFile.StartDate + TimeSpan.FromDays(6):dddd M/d}";
            var dayLabels = new[] { Day1Label, Day2Label, Day3Label, Day4Label, Day5Label, Day6Label, Day7Label };
            for (int i = 0; i < 7; i++)
            {
                dayLabels[i].Content = (date + TimeSpan.FromDays(i)).ToString("dddd M/d").ToUpper();
            }
        }

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrentFile.StartDate = new DatePicker(CurrentFile.StartDate).PromptDate();
            RenderCurrentFile();
        }

        private void Legend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LegendItem item = new LegendItemDialog().GetOrModifyItem();
            if (item != null)
            {
                item.MouseDoubleClick += (a, b) =>
                {
                    new LegendItemDialog(item).GetOrModifyItem();
                    RenderCurrentFile();
                };
                CurrentFile.LegendItems.Add(item);
            }

            RenderCurrentFile();
        }
    }
}

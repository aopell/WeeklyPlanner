﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WeeklyPlanner.Models;

namespace WeeklyPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlannerFile CurrentFile { get; set; } = new PlannerFile();

        public ObservableCollection<ListBoxItem> LegendItemDisplays { get; } = new ObservableCollection<ListBoxItem>();

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

            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(MainGrid, description);
            }
        }

        public void RenderCurrentFile()
        {
            UpdateDates(CurrentFile.StartDate);
            UpdateLegendItems(CurrentFile.LegendItems);
            UpdatePlannerItems(CurrentFile);
        }

        private void UpdatePlannerItems(PlannerFile file)
        {

        }

        private void UpdateLegendItems(List<LegendItem> legendItems)
        {
            LegendItemDisplays.CollectionChanged -= LegendItemDisplays_CollectionChanged;
            LegendItemDisplays.Clear();

            foreach (var item in legendItems)
            {
                var display = item.Display;
                display.MouseDoubleClick += (a, b) =>
                {
                    LegendItem result = new LegendItemDialog(item).GetOrModifyItem();
                    if (result != null)
                    {
                        int index = CurrentFile.LegendItems.IndexOf(item);
                        CurrentFile.LegendItems.Remove(item);
                        CurrentFile.LegendItems.Insert(index, result);
                    }
                    RenderCurrentFile();
                };
                LegendItemDisplays.Add(display);
            }
            LegendItemDisplays.CollectionChanged += LegendItemDisplays_CollectionChanged;
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
                CurrentFile.LegendItems.Add(item);
            }

            RenderCurrentFile();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new EventDialog(CurrentFile).ShowDialog();
        }

        private LegendItem moveTarget = null;
        private void LegendItemDisplays_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    CurrentFile.LegendItems.Insert(e.NewStartingIndex, moveTarget);
                    moveTarget = null;
                    RenderCurrentFile();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    moveTarget = CurrentFile.LegendItems[e.OldStartingIndex];
                    CurrentFile.LegendItems.RemoveAt(e.OldStartingIndex);
                    break;
                default:
                    moveTarget = null;
                    break;
            }
        }
    }
}

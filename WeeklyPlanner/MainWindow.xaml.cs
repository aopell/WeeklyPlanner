using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Newtonsoft.Json;
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

        private static readonly SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);

        public MainWindow()
        {
            InitializeComponent();
            RenderCurrentFile();
        }

        private void PrintWindow(Visual v, string description, double widthInches = 8.5, double heightInches = 11)
        {
            Height = heightInches * 96 + 39;
            Width = widthInches * 96 + 16;

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
            UpdateChecklistItems(CurrentFile);
            UpdatePlannerItems(CurrentFile);
        }

        private void UpdateChecklistItems(PlannerFile file)
        {
            ToDoList.Items.Clear();
            MoreToDoList.Items.Clear();

            foreach (var item in file.Items)
            {
                if (item.ChecklistEntry == null) continue;

                var textBlock = new TextBlock();
                var itemBrush = new SolidColorBrush(file.GetLegendItemById(item.LegendItemId).Color);

                textBlock.Inlines.Add(new Run
                {
                    Text = item.Title,
                    Foreground = itemBrush,
                    FontWeight = item.TextFormatting.HasFlag(TextFormatting.Bold)
                                     ? FontWeights.Bold
                                     : FontWeights.Normal,
                    FontStyle = item.TextFormatting.HasFlag(TextFormatting.Italics)
                                    ? FontStyles.Italic
                                    : FontStyles.Normal
                });

                foreach (var subtask in item.ChecklistEntry.Subtasks)
                {
                    textBlock.Inlines.Add(new Run
                    {
                        Text = $" ({subtask.Title})",
                        Foreground = subtask.LegendItemId == Guid.Empty ? itemBrush : new SolidColorBrush(file.GetLegendItemById(subtask.LegendItemId).Color),
                        FontWeight = subtask.TextFormatting.HasFlag(TextFormatting.Bold)
                                         ? FontWeights.Bold
                                         : FontWeights.Normal,
                        FontStyle = subtask.TextFormatting.HasFlag(TextFormatting.Italics)
                                        ? FontStyles.Italic
                                        : FontStyles.Normal
                    });
                }

                var checkbox = new CheckBox { Content = textBlock, VerticalContentAlignment = VerticalAlignment.Center };
                var list = item.ChecklistEntry.MainList ? ToDoList : MoreToDoList;

                checkbox.MouseDoubleClick += (a, b) =>
                {
                    var dialog = new EventDialog(CurrentFile, item);
                    var result = dialog.GetOrModifyEvent();

                    if (dialog.Deleted)
                    {
                        CurrentFile.Items.Remove(item);
                    }
                    if (result != null)
                    {
                        int index = CurrentFile.Items.IndexOf(item);
                        CurrentFile.Items[index] = result;
                    }

                    RenderCurrentFile();
                };

                list.Items.Add(checkbox);
            }

            if (file.AddExtraCheckBoxes)
            {
                int itemsCount = ToDoList.Items.Count;
                for (int i = 0; i < (ToDoList.ActualHeight / 27.5) - itemsCount; i++)
                {
                    ToDoList.Items.Add(new CheckBox { Content = " ", VerticalContentAlignment = VerticalAlignment.Center });
                }

                itemsCount = MoreToDoList.Items.Count;
                for (int i = 0; i < (MoreToDoList.ActualHeight / 27.5) - itemsCount; i++)
                {
                    MoreToDoList.Items.Add(new CheckBox { Content = " ", VerticalContentAlignment = VerticalAlignment.Center });
                }
            }
        }

        private void UpdatePlannerItems(PlannerFile file)
        {
            ListBox[] dayLists = { Day1List, Day2List, Day3List, Day4List, Day5List, Day6List, Day7List, NextWeekList };

            foreach (var list in dayLists)
            {
                list.Items.Clear();
            }

            foreach (var item in file.Items)
            {
                var itemBrush = new SolidColorBrush(file.GetLegendItemById(item.LegendItemId).Color);

                for (int i = 0; i < 8; i++)
                {
                    var list = dayLists[i];
                    var dayEntry = item.PlannerEntry.DayEntries[i];

                    if (dayEntry == null) continue;

                    var display = new ListBoxItem
                    {
                        Content = string.IsNullOrEmpty(dayEntry.Task.Title) ? item.Title : dayEntry.Task.Title,
                        Foreground = dayEntry.Task.LegendItemId == Guid.Empty ? itemBrush : new SolidColorBrush(file.GetLegendItemById(dayEntry.Task.LegendItemId).Color),
                        FontWeight = dayEntry.Task.TextFormatting.HasFlag(TextFormatting.Bold)
                                         ? FontWeights.Bold
                                         : FontWeights.Normal,
                        FontStyle = dayEntry.Task.TextFormatting.HasFlag(TextFormatting.Italics)
                                        ? FontStyles.Italic
                                        : FontStyles.Normal
                    };

                    display.MouseDoubleClick += (a, b) =>
                    {
                        var dialog = new EventDialog(CurrentFile, item);
                        var result = dialog.GetOrModifyEvent();

                        if (dialog.Deleted)
                        {
                            CurrentFile.Items.Remove(item);
                        }
                        if (result != null)
                        {
                            int index = CurrentFile.Items.IndexOf(item);
                            CurrentFile.Items[index] = result;
                        }

                        RenderCurrentFile();
                    };

                    list.Items.Add(display);
                }
            }
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
                    var dialog = new LegendItemDialog(item);
                    var result = dialog.GetOrModifyItem();

                    if (dialog.Deleted)
                    {
                        CurrentFile.LegendItems.Remove(item);
                    }
                    else if (result != null)
                    {
                        int index = CurrentFile.LegendItems.IndexOf(item);
                        CurrentFile.LegendItems[index] = result;
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
            Label[] dayLabels = { Day1Label, Day2Label, Day3Label, Day4Label, Day5Label, Day6Label, Day7Label };
            for (int i = 0; i < 7; i++)
            {
                dayLabels[i].Content = (date + TimeSpan.FromDays(i)).ToString("dddd M/d").ToUpper();
            }
        }

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CurrentFile.StartDate = new DatePicker(CurrentFile.StartDate).PromptDate();
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                var options = new Options(CurrentFile);
                switch (options.ShowAndGetResult())
                {
                    case Options.Result.Print:
                        PrintWindow(MainGrid, Title, options.PageWidth, options.PageHeight);
                        break;
                    case Options.Result.Open:
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Filter = "Weekly Planner JSON Files (*.wkpln)|*.wkpln";
                        if (ofd.ShowDialog() != true) break;

                        try
                        {
                            string text = File.ReadAllText(ofd.FileName);
                            CurrentFile = JsonConvert.DeserializeObject<PlannerFile>(text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error Opening File", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;
                    case Options.Result.Save:
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Weekly Planner JSON Files (*.wkpln)|*.wkpln";
                        sfd.DefaultExt = "wkpln";
                        sfd.FileName = $"Week of {CurrentFile.StartDate.Year:0000}-{CurrentFile.StartDate.Month:00}-{CurrentFile.StartDate.Day:00}.wkpln";
                        if (sfd.ShowDialog() != true) break;

                        try
                        {
                            string text = JsonConvert.SerializeObject(CurrentFile);
                            File.WriteAllText(sfd.FileName, text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error Saving File", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;
                }
            }

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

        private void ThingsToDoLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentFile.LegendItems.Count == 0)
            {
                MessageBox.Show("You must create at least one legend item before creating an event.");
                return;
            }

            EventItem eventItem = new EventDialog(CurrentFile).GetOrModifyEvent();
            if (eventItem != null)
            {
                CurrentFile.Items.Add(eventItem);
            }

            RenderCurrentFile();
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

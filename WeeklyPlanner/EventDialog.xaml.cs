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
        public EventItem EventItem = new EventItem();

        public PlannerFile PlannerFile { get; }

        public IEnumerable<ListBoxItem> LegendItemDisplays => PlannerFile.LegendItems.Select(x => x.Display);

        private static readonly ListBoxItem NoneLegendItem = new ListBoxItem { Content = "None", Foreground = new SolidColorBrush(Colors.Black), FontSize = 10, Tag = Guid.Empty };
        private static readonly ListBoxItem DefaultLegendItem = new ListBoxItem { Content = "Default (Inherit)", Foreground = new SolidColorBrush(Colors.Black), FontSize = 10, Tag = Guid.Empty };

        public IEnumerable<ListBoxItem> LegendItemDisplaysWithNone => new[] { NoneLegendItem }.Concat(LegendItemDisplays);
        public IEnumerable<ListBoxItem> LegendItemDisplaysWithDefault => new[] { DefaultLegendItem }.Concat(LegendItemDisplays);

        public EventDialog(PlannerFile file, EventItem toEdit = null)
        {
            PlannerFile = file;
            InitializeComponent();

            DateTime day = PlannerFile.StartDate;
            var dayButtons = new[] { PlannerDay1Button, PlannerDay2Button, PlannerDay3Button, PlannerDay4Button, PlannerDay5Button, PlannerDay6Button, PlannerDay7Button };
            foreach (var button in dayButtons)
            {
                button.Content = day.DayOfWeek.ToString();
                day += TimeSpan.FromDays(1);
            }
        }

        private void AddSubtask_Click(object sender, RoutedEventArgs e)
        {
            SubtaskListBox.Items.Add(new Subtask { ComboBoxItems = LegendItemDisplaysWithDefault, ComboBoxSelectedIndex = 0 });
        }

        private void SubtaskListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Delete || e.Key == Key.Back) && SubtaskListBox.SelectedIndex != -1)
            {
                SubtaskListBox.Items.RemoveAt(SubtaskListBox.SelectedIndex);
            }
        }

        public EventItem GetOrModifyEvent() => ShowDialog() == true ? EventItem : null;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EventItem = new EventItem
            {
                Title = EventTitleTextBox.Text,
                LegendItemId = (Guid)EventLegendItemComboBox.SelectedValue,
                TextFormatting = EventTitleTextBox.TextFormatting,
                ChecklistEntry = ChecklistEntryNoneButton.IsChecked.Value
                                     ? null
                                     : new ChecklistEntry
                                     {
                                         MainList = ChecklistEntryPrimaryButton.IsChecked.Value,
                                         Subtasks = SubtaskListBox.Items.Cast<Subtask>().Select(x => x.TaskItem).ToList()
                                     },
                PlannerEntry =
                {
                    DayEntries = new[]
                    {
                        PlannerDay1Button.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerDay1TitleTextBox.Text == "" ? null : PlannerDay1TitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerDay1LegendItem.SelectedValue,
                                    TextFormatting = PlannerDay1TitleTextBox.TextFormatting
                                }
                            }
                            : null,
                        PlannerDay2Button.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerDay2TitleTextBox.Text == "" ? null : PlannerDay2TitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerDay2LegendItem.SelectedValue,
                                    TextFormatting = PlannerDay2TitleTextBox.TextFormatting
                                }
                            }
                            : null,
                        PlannerDay3Button.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerDay3TitleTextBox.Text == "" ? null : PlannerDay3TitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerDay3LegendItem.SelectedValue,
                                    TextFormatting = PlannerDay3TitleTextBox.TextFormatting
                                }
                            }
                            : null,
                        PlannerDay4Button.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerDay4TitleTextBox.Text == "" ? null : PlannerDay4TitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerDay4LegendItem.SelectedValue,
                                    TextFormatting = PlannerDay4TitleTextBox.TextFormatting
                                }
                            }
                            : null,
                        PlannerDay5Button.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerDay5TitleTextBox.Text == "" ? null : PlannerDay5TitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerDay5LegendItem.SelectedValue,
                                    TextFormatting = PlannerDay5TitleTextBox.TextFormatting
                                }
                            }
                            : null,
                        PlannerDay6Button.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerDay6TitleTextBox.Text == "" ? null : PlannerDay6TitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerDay6LegendItem.SelectedValue,
                                    TextFormatting = PlannerDay6TitleTextBox.TextFormatting
                                }
                            }
                            : null,
                        PlannerDay7Button.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerDay7TitleTextBox.Text == "" ? null : PlannerDay7TitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerDay7LegendItem.SelectedValue,
                                    TextFormatting = PlannerDay7TitleTextBox.TextFormatting
                                }
                            }
                            : null,
                        PlannerNextWeekButton.IsChecked.Value
                            ? new DayEntry
                            {
                                Task =
                                {
                                    Title = PlannerNextWeekTitleTextBox.Text == "" ? null : PlannerNextWeekTitleTextBox.Text,
                                    LegendItemId = (Guid) PlannerNextWeekLegendItem.SelectedValue,
                                    TextFormatting = PlannerNextWeekTitleTextBox.TextFormatting
                                }
                            }
                            : null
                    }
                }
            };

            DialogResult = true;
            Close();
        }
    }
}

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
    /// Interaction logic for LegendItemDialog.xaml
    /// </summary>
    public partial class LegendItemDialog : Window
    {
        public LegendItem LegendItem = new LegendItem { Name = "", Color = Colors.Black };
        public bool Deleted { get; private set; } = false;

        public LegendItemDialog(LegendItem toEdit = null)
        {
            InitializeComponent();

            if (toEdit != null)
            {
                LegendItem.ID = toEdit.ID;
                DeleteButton.IsEnabled = true;
                DeleteButton.Visibility = Visibility.Visible;
                Title = "Edit Legend Item";
                HeaderLabel.Content = "Edit Legend Item";
                NameTextBox.Text = toEdit.Name;
                LegendItemColorPicker.SelectedColor = toEdit.Color;
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            LegendItem.Name = NameTextBox.Text;
            LegendItem.Color = LegendItemColorPicker.SelectedColor ?? Colors.Black;
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public LegendItem GetOrModifyItem() => ShowDialog() == true ? LegendItem : null;

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this legend item?",
                                "Confirm Delete",
                                MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                DialogResult = false;
                Deleted = true;
                Close();
            }
        }
    }
}

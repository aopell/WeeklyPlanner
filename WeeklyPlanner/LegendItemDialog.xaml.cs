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
using WeeklyPlanner.Items;

namespace WeeklyPlanner
{
    /// <summary>
    /// Interaction logic for LegendItemDialog.xaml
    /// </summary>
    public partial class LegendItemDialog : Window
    {
        public LegendItem LegendItem => new LegendItem(NameTextBox.Text, LegendItemColorPicker.SelectedColor ?? Colors.Black);
        public LegendItem PreviousLegendItem { get; }

        public LegendItemDialog(LegendItem toEdit = null)
        {
            PreviousLegendItem = toEdit;

            if (toEdit != null)
            {
                NameTextBox.Text = toEdit.Name;
                LegendItemColorPicker.SelectedColor = toEdit.Color;
            }

            InitializeComponent();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public LegendItem GetItem() => ShowDialog() == true ? LegendItem : PreviousLegendItem;
    }
}

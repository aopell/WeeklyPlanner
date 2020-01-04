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
        public LegendItem LegendItem => previousLegendItem ?? newLegendItem;

        private LegendItem newLegendItem = new LegendItem("", Colors.Black);
        private LegendItem previousLegendItem = null;

        public LegendItemDialog(LegendItem toEdit = null)
        {
            InitializeComponent();

            previousLegendItem = toEdit;

            if (toEdit != null)
            {
                Title = "Edit Legend Item";
                HeaderLabel.Content = "Edit Legend Item";
                NameTextBox.Text = toEdit.Title;
                LegendItemColorPicker.SelectedColor = toEdit.Color;
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            LegendItem.Title = NameTextBox.Text;
            LegendItem.Color = LegendItemColorPicker.SelectedColor ?? Colors.Black;
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public LegendItem GetOrModifyItem() => ShowDialog() == true ? LegendItem : previousLegendItem;
    }
}

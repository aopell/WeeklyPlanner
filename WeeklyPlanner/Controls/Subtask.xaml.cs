using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeeklyPlanner.Models;

namespace WeeklyPlanner.Controls
{
    /// <summary>
    /// Interaction logic for Subtask.xaml
    /// </summary>
    public partial class Subtask : UserControl
    {
        public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register(nameof(TextBoxText), typeof(string), typeof(Subtask));
        public static readonly DependencyProperty ComboBoxItemsProperty = DependencyProperty.Register(nameof(ComboBoxItems), typeof(IEnumerable), typeof(Subtask));
        public static readonly DependencyProperty TextBoxBoldProperty = DependencyProperty.Register(nameof(TextBoxBold), typeof(bool), typeof(Subtask));
        public static readonly DependencyProperty TextBoxItalicProperty = DependencyProperty.Register(nameof(TextBoxItalic), typeof(bool), typeof(Subtask));
        public static readonly DependencyProperty TextBoxWatermarkProperty = DependencyProperty.Register(nameof(TextBoxWatermark), typeof(string), typeof(Subtask));

        public string TextBoxText
        {
            get => (string)GetValue(TextBoxTextProperty);
            set => SetValue(TextBoxTextProperty, value);
        }

        public IEnumerable ComboBoxItems
        {
            get => (IEnumerable)GetValue(ComboBoxItemsProperty);
            set => SetValue(ComboBoxItemsProperty, value);
        }

        public bool TextBoxBold
        {
            get => (bool)GetValue(TextBoxBoldProperty);
            set => SetValue(TextBoxBoldProperty, value);
        }

        public bool TextBoxItalic
        {
            get => (bool)GetValue(TextBoxItalicProperty);
            set => SetValue(TextBoxItalicProperty, value);
        }

        public string TextBoxWatermark
        {
            get => (string)GetValue(TextBoxWatermarkProperty);
            set => SetValue(TextBoxWatermarkProperty, value);
        }

        public object ComboBoxSelectedItem
        {
            get => ComboBox.SelectedItem;
            set => ComboBox.SelectedItem = value;
        }

        public int ComboBoxSelectedIndex
        {
            get => ComboBox.SelectedIndex;
            set => ComboBox.SelectedIndex = value;
        }

        public TaskItem TaskItem => new TaskItem { LegendItemId = (Guid)ComboBox.SelectedValue, Title = TextBox.Text, TextFormatting = TextBox.TextFormatting };

        public Subtask()
        {
            InitializeComponent();
        }
    }
}

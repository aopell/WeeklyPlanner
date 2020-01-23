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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeeklyPlanner.Models;

namespace WeeklyPlanner.Controls
{
    /// <summary>
    /// Interaction logic for FormattedWatermarkTextBox.xaml
    /// </summary>
    public partial class FormattedWatermarkTextBox : UserControl
    {
        public static readonly DependencyProperty BoldProperty = DependencyProperty.Register(nameof(Bold), typeof(bool), typeof(FormattedWatermarkTextBox));
        public static readonly DependencyProperty ItalicProperty = DependencyProperty.Register(nameof(Italic), typeof(bool), typeof(FormattedWatermarkTextBox));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(FormattedWatermarkTextBox));
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(FormattedWatermarkTextBox));

        public bool Bold
        {
            get => (bool)GetValue(BoldProperty);
            set => SetValue(BoldProperty, value);
        }

        public bool Italic
        {
            get => (bool)GetValue(ItalicProperty);
            set => SetValue(ItalicProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        public TextFormatting TextFormatting => FormattingButtons.TextFormatting;

        public FormattedWatermarkTextBox()
        {
            InitializeComponent();
        }
    }
}

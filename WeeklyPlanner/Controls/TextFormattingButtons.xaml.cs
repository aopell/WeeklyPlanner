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
    /// Interaction logic for TextFormattingButtons.xaml
    /// </summary>
    public partial class TextFormattingButtons : UserControl
    {
        public static readonly DependencyProperty BoldProperty = DependencyProperty.Register(nameof(Bold), typeof(bool), typeof(TextFormattingButtons));
        public static readonly DependencyProperty ItalicProperty = DependencyProperty.Register(nameof(Italic), typeof(bool), typeof(TextFormattingButtons));

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

        public TextFormatting TextFormatting => (BoldButton.IsChecked.Value ? TextFormatting.Bold : TextFormatting.None) | (ItalicButton.IsChecked.Value ? TextFormatting.Italics : TextFormatting.None);

        public TextFormattingButtons()
        {
            InitializeComponent();
        }
    }
}

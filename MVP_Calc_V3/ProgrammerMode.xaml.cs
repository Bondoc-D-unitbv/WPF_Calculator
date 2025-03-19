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

namespace MVP_Calc_V3
{
    public partial class ProgrammerMode : Window
    {
        private Calculator _calculator;

        private int _selectedBase = 10;
        public int SelectedBase { get => _selectedBase; set => _selectedBase = value; }

        public ProgrammerMode(Calculator calc)
        {
            InitializeComponent();
            _calculator = calc;
            BaseDisplay.Text = _calculator.Display;
        }

        public void UpdateBaseDisp()
        {
            if (!string.IsNullOrWhiteSpace(_calculator.Display) && long.TryParse(_calculator.Display, out long value))
            {
                BaseDisplay.Text = Convert.ToString(value, _selectedBase).ToUpper();
            }
            else
            {
                BaseDisplay.Text = "0"; 
            }
        }

        private void BaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BaseList.SelectedItem is ListBoxItem item && int.TryParse(item.Tag.ToString(), out int baseValue))
            {
                _selectedBase = baseValue;

                if (!string.IsNullOrWhiteSpace(_calculator.Display) && long.TryParse(_calculator.Display, out long value))
                {
                    BaseDisplay.Text = Convert.ToString(value, _selectedBase).ToUpper();
                }
                else
                {
                    // If Display is empty or invalid, reset it to "0"
                    BaseDisplay.Text = "0";
                }
            }
        }

        private void BaseDisplay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string validChars = _selectedBase switch
            {
                2 => "01",
                8 => "01234567",
                10 => "0123456789",
                16 => "0123456789ABCDEFabcdef",
                _ => ""
            };

            if (!validChars.Contains(e.Text))
            {
                e.Handled = true; // Prevent invalid input
            }
        }
    }
}

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVP_Calc_V3
{
    public partial class MainWindow : Window
    {

        private Calculator m_calculator = new Calculator();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = m_calculator;
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                char digit = button.Content.ToString()[0];
                m_calculator.EnterDigit(digit);
                //UpdateDisplay(); //to implement
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string op = button.Content.ToString(); // Get the operator from the button text

                m_calculator.EnterOperator(op); // Pass operator as a string
                UpdateDisplay();
            }
        }


        private void UpdateDisplay()
        {
            Display.Text = m_calculator.Display;
        }

        private void percent_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Percentage();
            UpdateDisplay();
        }

        private void sqroot_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.SquareRoot();
            UpdateDisplay();
        }

        private void squared_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Square();
            UpdateDisplay();
        }

        private void oneDivided_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Reciprocal();
            UpdateDisplay();
        }

        private void Ce_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.ClearEntry();
            UpdateDisplay();
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Clear();
            UpdateDisplay();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Backspace();
            UpdateDisplay();
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterOperator("/");
            UpdateDisplay();
        }

        private void seven_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('7');
            UpdateDisplay();
        }

        private void eight_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('8');
            UpdateDisplay();
        }

        private void nine_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('9');
            UpdateDisplay();
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterOperator("*");
            UpdateDisplay();
        }

        private void four_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('4');
            UpdateDisplay();
        }

        private void five_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('5');
            UpdateDisplay();
        }

        private void six_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('6');
            UpdateDisplay();
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterOperator("-");
            UpdateDisplay();
        }

        private void one_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('1');
            UpdateDisplay();
        }

        private void two_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('2');
            UpdateDisplay();
        }

        private void three_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('3');
            UpdateDisplay();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterOperator("+");
            UpdateDisplay();
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.InvertSign();
            UpdateDisplay();
        }

        private void zero_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.EnterDigit('0');
            UpdateDisplay();
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            // Check if the current display already contains a decimal point
            if (!m_calculator.Display.Contains("."))
            {
                m_calculator.SetDisplay(Display.Text + ".");
                UpdateDisplay();
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Equals();
            UpdateDisplay();
        }

        private void MC_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.MemoryClear();
            UpdateDisplay();
        }

        private void MPlus_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.MemoryAdd();
            UpdateDisplay();
        }

        private void MMinus_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.MemorySubtract();
            UpdateDisplay();
        }

        private void MR_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.MemoryRecall();
            UpdateDisplay();
        }

        private void GroupDigits_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.ApplyDigitGrouping();
            UpdateDisplay(); 
        }


        private void Programmer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void File_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
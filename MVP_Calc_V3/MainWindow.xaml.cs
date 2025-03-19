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
        private SettingsManager m_settings_manager = new SettingsManager();
        private ProgrammerMode _programmerWindow;
        private bool programmerModeEnabled;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = m_calculator;
            LoadUserSettings();

            this.Closing += MainWindow_Closing;
        }

        private bool _isMainWindowLoaded = false; // Flag to track if MainWindow is loaded

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _isMainWindowLoaded = true;
        }

        private void LoadUserSettings()
        {
            var settings = SettingsManager.LoadSettings();

            if (settings.ContainsKey("DigitGrouping"))
            {
                bool.TryParse(settings["DigitGrouping"], out bool isDigitGrouping);
                m_calculator.IsDigitGroupingEnabled = isDigitGrouping;
            }

            if (settings.ContainsKey("Mode"))
            {
                string mode = settings["Mode"];
                if (mode == "Programmer")
                {
                    Programmer_Click(null, null); 
                }
            }

            if (settings.ContainsKey("Base"))
            {
                int.TryParse(settings["Base"], out int baseValue);
                m_calculator.c_Base = baseValue;
                if(_programmerWindow != null)
                {
                    _programmerWindow.SelectedBase = m_calculator.c_Base;
                }
            }
        }

        private void SaveUserSettings()
        {
            var settings = new Dictionary<string, string>
            {
                { "DigitGrouping", m_calculator.IsDigitGroupingEnabled.ToString() },
                { "Mode", m_calculator.CurrentMode },
                { "Base", _programmerWindow.SelectedBase.ToString() }
             };

            SettingsManager.SaveSettings(settings);
        }


        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                char digit = button.Content.ToString()[0];
                m_calculator.EnterDigit(digit);
                UpdateDisplay();
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
            if(m_calculator.ApplyDigitGrouping())
            {
                Display.Text = m_calculator.DisplayGrouped;
            }
            else
            {
                Display.Text = m_calculator.Display;
            }
            UpdateProgrammerMode();
        }

        private void UpdateProgrammerMode()
        {
            if (_programmerWindow != null && _programmerWindow.IsLoaded)
            {
                _programmerWindow.UpdateBaseDisp();
            }
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
            m_calculator.EnterDigit('.');
            UpdateDisplay();
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Equals();
            UpdateDisplay();
        }



        private void Mc_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.MemoryClear();
            UpdateDisplay();
        }

        private void MPlus_Click_1(object sender, RoutedEventArgs e)
        {
            m_calculator.MemoryAdd();
            m_calculator.MemoryStackPush();
            UpdateDisplay();
        }

        private void Mminus_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.MemorySubtract();
            UpdateDisplay();
        }

        private void Ms_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.MemoryRecall();
            UpdateDisplay();
        }

        private void MemoryShow_Click(object sender, RoutedEventArgs e)
        {
            var memoryWindow = new MemoryWindow(m_calculator.MemoryStackDisplay());
            memoryWindow.MemorySelected += value => Display.Text = value.ToString();
            memoryWindow.ShowDialog();
        }

        private void DigitGrouping_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.ChangeDigitGrouping();
            //MessageBox.Show("You are grouping digits!");
            UpdateDisplay();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Cut();
            UpdateDisplay();
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Copy();
            UpdateDisplay();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            m_calculator.Paste();
            UpdateDisplay();
        }



        private void Programmer_Click(object sender, RoutedEventArgs e)
        {
            if (m_calculator.CurrentMode == "Standard")
            {
                if (_programmerWindow == null || !_programmerWindow.IsLoaded)
                {
                    _programmerWindow = new ProgrammerMode(m_calculator);

                    if (_isMainWindowLoaded)
                    {
                        _programmerWindow.Owner = this;
                    }

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _programmerWindow.Owner = this;
                    }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);


                    _programmerWindow.SelectedBase = m_calculator.c_Base;

                    // Position the window to the left of the main calculator
                    _programmerWindow.Left = this.Left - _programmerWindow.Width;
                    _programmerWindow.Top = this.Top;

                    _programmerWindow.Show();
                }
                else
                {
                    _programmerWindow.Show();
                }
                m_calculator.CurrentMode = "Programmer";
            }
            else
            {
                m_calculator.CurrentMode = "Standard";
                _programmerWindow.Hide();
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9) // Numbers 0-9 from main keyboard
            {
                m_calculator.EnterDigit((char)('0' + (e.Key - Key.D0)));
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) // Numbers from NumPad
            {
                m_calculator.EnterDigit((char)('0' + (e.Key - Key.NumPad0)));
            }
            else if (e.Key == Key.OemPlus || e.Key == Key.Add)
            {
                m_calculator.EnterOperator("+");
            }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract) 
            {
                m_calculator.EnterOperator("-");
            }
            else if (e.Key == Key.Multiply)
            {
                m_calculator.EnterOperator("*");
            }
            else if (e.Key == Key.Divide || e.Key == Key.Oem2) 
            {
                m_calculator.EnterOperator("/");
            }
            else if (e.Key == Key.Enter || e.Key == Key.Return) 
            {
                m_calculator.Equals();
            }
            else if (e.Key == Key.Back)
            {
                m_calculator.Backspace();
            }
            else if (e.Key == Key.Escape)
            {
                m_calculator.Clear();
            }
            else if (e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Decimal) 
            {
                m_calculator.EnterDigit('.');
            }

            UpdateDisplay();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicație dezvoltată de: \n" +
                "Bondoc Daniel, 10LF331", "About");
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveUserSettings();
        }
    }
}
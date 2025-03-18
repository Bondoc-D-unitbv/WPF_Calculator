using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MVP_Calc_V3
{
    public class Calculator
    {
        private double _currentValue;
        private double _memory;
        private string _currentOperator;
        private bool _isNewEntry;
        private Stack<double> _memoryStack = new Stack<double>();


        private bool _isDigitGroupingEnabled = false;
        public bool IsDigitGroupingEnabled { get => _isDigitGroupingEnabled; set => _isDigitGroupingEnabled = value; }

        private string currentMode = "Standard";
        public string CurrentMode { get => currentMode; set => currentMode = value; }

        private int selectedBase = 10;
        public int SelectedBase { get => selectedBase; set => selectedBase = value; }



        public event PropertyChangedEventHandler PropertyChanged;

        private string _display = "0";
        public string Display
        {
            get => _display;
            private set
            {
                _display = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Display)));
            }
        }


        public void SetDisplay(string value)
        {
            Display = value;
        }

        public void EnterDigit(char digit)
        {
            if (_isNewEntry)
            {
                Display = digit.ToString();
                _isNewEntry = false;
            }
            else
            {
                Display += digit;
            }
            ApplyDigitGrouping();
        }

        public void EnterOperator(string op)
        {
            if (double.TryParse(Display, out double value))
            {
                if (_currentOperator != null)
                {
                    Calculate(value);
                }
                else
                {
                    _currentValue = value;
                }
                _currentOperator = op;
                _isNewEntry = true;
            }
        }

        public void Calculate(double value)
        {
            switch (_currentOperator)
            {
                case "+":
                    _currentValue += value;
                    break;
                case "-":
                    _currentValue -= value;
                    break;
                case "*":
                    _currentValue *= value;
                    break;
                case "/":
                    _currentValue = value == 0 ? double.NaN : _currentValue / value;
                    break;
                case "%":
                    _currentValue %= value;
                    break;
            }
            Display = _currentValue.ToString();
            _currentOperator = null;
        }

        public void Equals()
        {
            if (double.TryParse(Display, out double value))
            {
                Calculate(value);
                _isNewEntry = true;
            }
        }

        public void ClearEntry()
        {
            Display = "0";
        }

        public void Clear()
        {
            _currentValue = 0;
            _currentOperator = null;
            Display = "0";
        }

        public void Backspace()
        {
            if (Display.Length > 1)
            {
                Display = Display.Substring(0, Display.Length - 1);
            }
            else
            {
                Display = "0";
            }
        }

        public void MemoryStore()
        {
            if (double.TryParse(Display, out double value))
            {
                _memory = value;
            }
        }

        public void MemoryRecall()
        {
            Display = _memory.ToString();
        }

        public void MemoryClear()
        {
            _memory = 0;
        }

        public void MemoryAdd()
        {
            if (double.TryParse(Display, out double value))
            {
                _memory += value;
            }
        }

        public void MemorySubtract()
        {
            if (double.TryParse(Display, out double value))
            {
                _memory -= value;
            }
        }

        public void MemoryStackPush()
        {
            if (double.TryParse(Display, out double value))
            {
                _memoryStack.Push(value);
            }
        }

        public string MemoryStackDisplay()
        {
            return string.Join(", ", _memoryStack);
        }

        public void SquareRoot()
        {
            if (double.TryParse(Display, out double value) && value >= 0)
            {
                Display = Math.Sqrt(value).ToString();
            }
            else
            {
                Display = "Invalid";
            }
        }

        public void Square()
        {
            if (double.TryParse(Display, out double value))
            {
                Display = (value * value).ToString();
            }
        }

        public void InvertSign()
        {
            if (double.TryParse(Display, out double value))
            {
                Display = (-value).ToString();
            }
        }

        public void Reciprocal()
        {
            if (double.TryParse(Display, out double value) && value != 0)
            {
                Display = (1 / value).ToString();
            }
            else
            {
                Display = "Invalid";
            }
        }

        public void Percentage()
        {
            if (double.TryParse(Display, out double value))
            {
                if (_currentOperator != null)
                {
                    // Case: Calculating percentage with respect to previous value
                    double result = _currentValue + (_currentValue * value / 100);
                    Display = result.ToString();
                }
                else
                {
                    // Case: Calculating percentage of current value
                    Display = (value / 100).ToString();
                }
            }
        }


        public void ApplyDigitGrouping()
        {
            if (_isDigitGroupingEnabled)
            {
                if (double.TryParse(Display, out double value))
                {
                    string valueString = value.ToString(CultureInfo.CurrentCulture);

                    var parts = valueString.Split('.');

                    string integerPart = parts[0];

                    integerPart = GroupDigits(integerPart);

                    if (parts.Length > 1)
                    {
                        Display = integerPart + "." + parts[1];
                    }
                    else
                    {
                        Display = integerPart;
                    }
                }
            }
            //else
            //{
            //    Display = double.TryParse(Display, out double value)
            //        ? value.ToString(CultureInfo.CurrentCulture)
            //        : Display;
            //}
        }

        private string GroupDigits(string number)
        {
            int length = number.Length;
            if (length <= 3)
                return number;

            var grouped = new List<char>();
            int count = 0;

            for (int i = length - 1; i >= 0; i--)
            {
                grouped.Add(number[i]);
                count++;

                if (count == 3 && i != 0)
                {
                    grouped.Add(',');
                    count = 0;
                }
            }

            grouped.Reverse();

            return new string(grouped.ToArray());
        }



        public void ChangeDigitGrouping()
        {
            _isDigitGroupingEnabled = !_isDigitGroupingEnabled;
        }


    }
}

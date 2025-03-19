using System;
using System.Collections.Generic;
using System.Windows;

namespace MVP_Calc_V3
{
    public partial class MemoryWindow : Window
    {
        private Stack<double> _memoryStack;
        public event Action<double> MemorySelected;

        public MemoryWindow(Stack<double> memoryStack)
        {
            InitializeComponent();
            _memoryStack = memoryStack;
            UpdateMemoryList();
        }

        private void UpdateMemoryList()
        {
            lstMemoryStack.Items.Clear();
            foreach (var value in _memoryStack)
            {
                lstMemoryStack.Items.Add(value);
            }
        }

        private void lstMemoryStack_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstMemoryStack.SelectedItem is double selectedValue)
            {
                MemorySelected?.Invoke(selectedValue);
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstMemoryStack.SelectedItem is double selectedValue)
            {
                _memoryStack = new Stack<double>(_memoryStack.Where(x => x != selectedValue)); // Remove selected value
                UpdateMemoryList();
            }
        }
    }
}

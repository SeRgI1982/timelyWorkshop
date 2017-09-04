using System;
using System.Windows;
using timely.common.Infrastructure;

namespace timely.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private int _selectedIndex;

        public ShellViewModel()
        {
            MessageBox.Show("shell view model was created");
        }

        public override string Name => "Shell";

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                Refresh();
            }
        }

        private void Refresh()
        {
            switch (SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}

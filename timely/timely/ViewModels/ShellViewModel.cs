using System;
using System.ComponentModel;
using timely.Infrastructure;

namespace timely.ViewModels
{
    public class ShellViewModel : NotifiableObject
    {
        public ShellViewModel()
        {
            DashboardViewModel = new DashboardViewModel();
            TimesheetViewModel = new TimesheetViewModel();
            ReportsViewModel = new ReportsViewModel();
        }

        public DashboardViewModel DashboardViewModel { get; }

        public TimesheetViewModel TimesheetViewModel { get; }

        public ReportsViewModel ReportsViewModel { get; }

        private int _selectedIndex;

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

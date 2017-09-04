using timely.common.Infrastructure;
using timely.dashboard.Models;

namespace timely.dashboard.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private UserType? _selectedUserType;

        public UserType? SelectedUserType
        {
            get { return _selectedUserType; }
            set
            {
                _selectedUserType = value;
                RaisePropertyChanged();
            }
        }

        public override string Name => "Dashboard";
    }
}
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MvvmDialogs;
using timely.Infrastructure;
using timely.Models;

namespace timely.ViewModels
{
    public class TimesheetEntriesViewModel : NotifiableObject
    {
        private readonly TimelyService _service;
        private readonly IDialogService _dialogService;

        public TimesheetEntriesViewModel()
        {
            _service = new TimelyService();
            _dialogService = DialogServiceProvider.Instance.DialogService;

            Users = new ObservableCollection<User>(_service.GetUsers());
            Projects = new ObservableCollection<Project>(_service.GetProjects());
            TimesheetEntries = new ObservableCollection<TimesheetEntry>(_service.GetTimesheetEntries());

            AddTimesheetEntryCommand = new MyCommand(OnAddTimesheetEntry);
            EditTimesheetEntryCommand = new MyCommand(OnEditTimesheetEntry);
            DeleteTimesheetEntryCommand = new MyCommand(OnDeleteTimesheetEntry);
        }

        public ICommand AddTimesheetEntryCommand { get; }

        public ICommand EditTimesheetEntryCommand { get; }

        public ICommand DeleteTimesheetEntryCommand { get; }

        public ObservableCollection<User> Users { get; }

        public ObservableCollection<Project> Projects { get; }

        public ObservableCollection<TimesheetEntry> TimesheetEntries { get; }

        private void OnAddTimesheetEntry(object parameter)
        {
            var timesheetEntryViewModel = new TimesheetEntryDialogViewModel { Entry = new TimesheetEntry() };
            bool? success = _dialogService.ShowDialog(this, timesheetEntryViewModel);

            if (success.GetValueOrDefault())
            {
                TimesheetEntries.Insert(0, timesheetEntryViewModel.Entry);
            }
        }

        private void OnEditTimesheetEntry(object parameter)
        {
            var timesheetEntry = parameter as TimesheetEntry;

            if (timesheetEntry == null)
            {
                return;
            }

            var toEdit = _service.GetTimesheetEntryById(timesheetEntry.Id);
            var timesheetEntryViewModel = new TimesheetEntryDialogViewModel { Entry = toEdit };
            var success = _dialogService.ShowDialog(this, timesheetEntryViewModel);

            if (success.GetValueOrDefault())
            {
                var index = TimesheetEntries.IndexOf(timesheetEntry);
                TimesheetEntries.Remove(timesheetEntry);
                TimesheetEntries.Insert(index, timesheetEntryViewModel.Entry);
            }
        }

        private void OnDeleteTimesheetEntry(object parameter)
        {
            var timesheetEntry = parameter as TimesheetEntry;

            if (timesheetEntry == null)
            {
                return;
            }

            if (_dialogService.ShowMessageBox(this,
                $"Do you want to delete timesheet entry: {timesheetEntry.WorkTitle} - {timesheetEntry.DateStarted} ?",
                "Deletion of Timesheet Entry", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _service.DeleteTimesheetEntry(timesheetEntry.Id);
                TimesheetEntries.Remove(timesheetEntry);
            }
        }
    }
}
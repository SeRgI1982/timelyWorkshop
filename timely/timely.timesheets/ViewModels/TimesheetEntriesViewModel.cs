using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MvvmDialogs;
using timely.common.Infrastructure;
using timely.timesheets.Models;
using timely.timesheets.Services;

namespace timely.timesheets.ViewModels
{
    public class TimesheetEntriesViewModel : ViewModelBase
    {
        private readonly TimelyService _service;
        private readonly IDialogService _dialogService;

        public TimesheetEntriesViewModel(TimelyService service, IDialogService dialogService)
        {
            _service = service;
            _dialogService = dialogService;

            Users = new ObservableCollection<User>(_service.GetUsers());
            Projects = new ObservableCollection<Project>(_service.GetProjects());
            TimesheetEntries = new ObservableCollection<TimesheetEntry>(_service.GetTimesheetEntries());

            AddTimesheetEntryCommand = new MyCommand(OnAddTimesheetEntry);
            EditTimesheetEntryCommand = new MyCommand(OnEditTimesheetEntry);
            DeleteTimesheetEntryCommand = new MyCommand(OnDeleteTimesheetEntry);
        }


        public override string Name => "Timesheets";
        public ICommand AddTimesheetEntryCommand { get; }

        public ICommand EditTimesheetEntryCommand { get; }

        public ICommand DeleteTimesheetEntryCommand { get; }

        public ObservableCollection<User> Users { get; }

        public ObservableCollection<Project> Projects { get; }

        public ObservableCollection<TimesheetEntry> TimesheetEntries { get; }

        private void OnAddTimesheetEntry(object parameter)
        {
            var timesheetEntryViewModel = new TimesheetEntryDialogViewModel(_service) { Entry = new TimesheetEntry() };
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
            var timesheetEntryViewModel = new TimesheetEntryDialogViewModel(_service) { Entry = toEdit };
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
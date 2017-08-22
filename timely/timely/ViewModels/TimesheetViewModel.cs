using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using timely.Infrastructure;
using timely.Models;

namespace timely.ViewModels
{
    public class TimesheetViewModel : NotifiableObject
    {
        public readonly TimelyService _service;

        public TimesheetViewModel()
        {
            _service = new TimelyService();

            Users = new ObservableCollection<User>(_service.GetUsers());
            Projects = new ObservableCollection<Project>(_service.GetProjects());
            TimesheetEntries = new ObservableCollection<TimesheetEntry>(_service.GetTimesheetEntries());

            AddTimesheetEntryCommand = new MyCommand(OnAddTimesheetEntry);
            EditTimesheetEntryCommand = new MyCommand(OnEditTimesheetEntry);
            DeleteTimesheetEntryCommand = new MyCommand(OnDeleteTimesheetEntry);
        }

        private void OnDeleteTimesheetEntry(object parameter)
        {
            var timesheetEntry = parameter as TimesheetEntry;

            if (timesheetEntry == null)
            {
                return;
            }

            _service.DeleteTimesheetEntry(timesheetEntry.Id);
            TimesheetEntries.Remove(timesheetEntry);
        }

        private void OnEditTimesheetEntry(object parameter)
        {
            var timesheetEntry = parameter as TimesheetEntry;

            if (timesheetEntry == null)
            {
                return;
            }

            MessageBox.Show($"Timesheet entry to be edited: {timesheetEntry.DateStarted} - {timesheetEntry.WorkTitle}");
        }

        private void OnAddTimesheetEntry(object parameter)
        {
            throw new System.NotImplementedException();
        }

        public ICommand AddTimesheetEntryCommand { get; }

        public ICommand EditTimesheetEntryCommand { get; }

        public ICommand DeleteTimesheetEntryCommand { get; }

        public ObservableCollection<User> Users { get; }

        public ObservableCollection<Project> Projects { get; }

        public ObservableCollection<TimesheetEntry> TimesheetEntries { get; }
    }
}
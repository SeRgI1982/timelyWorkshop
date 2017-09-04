using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmDialogs;
using timely.common.Infrastructure;
using timely.timesheets.Models;
using timely.timesheets.Services;

namespace timely.timesheets.ViewModels
{
    public class TimesheetEntryDialogViewModel : ViewModelBase, IModalDialogViewModel
    {
        private bool? _dialogResult;
        private string _title;

        private TimesheetEntry _entry;
        private readonly TimelyService _service;

        public TimesheetEntryDialogViewModel(TimelyService service)
        {
            _title = "Timesheet entry";

            _service = service;

            Users = new ObservableCollection<User>(_service.GetUsers());
            Projects = new ObservableCollection<Project>(_service.GetProjects());

            OkCommand = new MyCommand(OnOk);
        }

        public override string Name => "Timesheet Entry";
        public ICommand OkCommand { get; }

        public ObservableCollection<User> Users { get; }

        public ObservableCollection<Project> Projects { get; }

        public TimesheetEntry Entry
        {
            get { return _entry; }
            set
            {
                _entry = value;
                RaisePropertyChanged();
                CalculateTitle();
            }
        }

        private void CalculateTitle()
        {
            Title = Entry?.Id == Guid.Empty ? "Add Timesheet Entry" : "Edit Timesheet Entry";
        }

        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public bool? DialogResult
        {
            get { return _dialogResult; }
            private set
            {
                _dialogResult = value;
                RaisePropertyChanged();
            }
        }

        private void OnOk(object parameter)
        {
            Entry.Validate();
            if (Entry.HasErrors)
            {
                return;
            }

            if (Entry.Id == Guid.Empty)
            {
                _service.AddTimesheetEntry(Entry);
            }
            else
            {
                _service.UpdateTimesheetEntry(Entry);
            }

            DialogResult = true;
        }
    }
}
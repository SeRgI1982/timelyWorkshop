using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmDialogs;
using timely.Infrastructure;
using timely.Models;

namespace timely.ViewModels
{
    public class TimesheetEntryDialogViewModel : NotifiableObject, IModalDialogViewModel
    {
        private bool? _dialogResult;
        private string _title;

        private TimesheetEntry _entry;
        private readonly TimelyService _service;

        public TimesheetEntryDialogViewModel()
        {
            _title = "Timesheet entry";

            _service = new TimelyService();

            Users = new ObservableCollection<User>(_service.GetUsers());
            Projects = new ObservableCollection<Project>(_service.GetProjects());

            OkCommand = new MyCommand(OnOk);
        }

        public ICommand OkCommand { get; }

        public ObservableCollection<User> Users { get; }

        public ObservableCollection<Project> Projects { get; }

        public TimesheetEntry Entry
        {
            get { return _entry; }
            set
            {
                _entry = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public bool? DialogResult
        {
            get { return _dialogResult; }
            private set
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }

        private void OnOk(object parameter)
        {
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
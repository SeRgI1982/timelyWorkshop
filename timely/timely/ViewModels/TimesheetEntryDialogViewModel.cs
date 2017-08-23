using System;
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

        public TimesheetEntryDialogViewModel()
        {
            _title = "Timesheet entry";
            OkCommand = new MyCommand(OnOk);
        }

        public ICommand OkCommand { get; }

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
            if (true)
            {
                DialogResult = true;
            }
        }
    }
}
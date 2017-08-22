using System;
using timely.Infrastructure;

namespace timely.Models
{
    public class TimesheetEntry : NotifiableObject
    {
        private Guid? _projectId;
        private Guid? _userId;
        private string _workTitle;
        private string _workDescription;
        private DateTime? _dateStarted;
        private TimeSpan? _timeSpent;

        public Guid Id { get; set; }

        public Guid? ProjectId
        {
            get { return _projectId; }
            set
            {
                _projectId = value;
                OnPropertyChanged();
            }
        }

        public Guid? UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        public string WorkTitle
        {
            get { return _workTitle; }
            set
            {
                _workTitle = value;
                OnPropertyChanged();
            }
        }

        public string WorkDescription
        {
            get { return _workDescription; }
            set
            {
                _workDescription = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DateStarted
        {
            get { return _dateStarted; }
            set
            {
                _dateStarted = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan? TimeSpent
        {
            get { return _timeSpent; }
            set
            {
                _timeSpent = value;
                OnPropertyChanged();
            }
        }
    }
}

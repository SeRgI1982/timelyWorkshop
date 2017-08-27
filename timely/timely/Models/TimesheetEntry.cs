using System;
using System.ComponentModel.DataAnnotations;
using timely.Infrastructure;
using timely.Properties;

namespace timely.Models
{
    public class TimesheetEntry : ValidatableObject
    {
        private Guid? _projectId;
        private Guid? _userId;
        private string _workTitle = String.Empty;
        private string _workDescription;
        private DateTime? _dateStarted;
        private TimeSpan? _timeSpent;

        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "ProjectIsRequired", ErrorMessageResourceType = typeof(Resources))]
        public Guid? ProjectId
        {
            get { return _projectId; }
            set { SetProperty(ref _projectId, value); }
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

        [StringLength(50, MinimumLength = 2, ErrorMessage = @"Title should contain at least 2 characters and no more than 50")]
        public string WorkTitle
        {
            get { return _workTitle; }
            set { SetProperty(ref _workTitle, value); }
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

        [CustomValidation(typeof(TimesheetEntryTimeSpentValidator), TimesheetEntryTimeSpentValidator.ValidateDateStartedName)]
        public DateTime? DateStarted
        {
            get { return _dateStarted; }
            set { SetProperty(ref _dateStarted, value); }
        }

        [CustomValidation(typeof(TimesheetEntryTimeSpentValidator), TimesheetEntryTimeSpentValidator.ValidateTimeSpentName)]
        public TimeSpan? TimeSpent
        {
            get { return _timeSpent; }
            set { SetProperty(ref _timeSpent, value); }
        }
    }
}

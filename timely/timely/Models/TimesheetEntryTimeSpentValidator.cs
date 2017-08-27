using System;
using System.ComponentModel.DataAnnotations;

namespace timely.Models
{
    public static class TimesheetEntryTimeSpentValidator
    {
        public const string ValidateTimeSpentName = "ValidateTimeSpent";
        public const string ValidateDateStartedName = "ValidateDateStarted";

        public static ValidationResult ValidateTimeSpent(TimeSpan? timeSpent, ValidationContext context)
        {
            var instance = (TimesheetEntry)context.ObjectInstance;
            var totalHours = timeSpent.GetValueOrDefault().TotalHours;

            if (instance.DateStarted.HasValue && instance.DateStarted.Value.DayOfWeek == DayOfWeek.Sunday &&
                totalHours > 0)
            {
                return new ValidationResult("Work on Sunday is prohibited",
                    new[] { nameof(TimesheetEntry.TimeSpent), nameof(TimesheetEntry.DateStarted) });
            }

            if (totalHours > 8)
            {
                return new ValidationResult("Task can not be longer than 8 hours",
                    new[] {nameof(TimesheetEntry.TimeSpent)});
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateDateStarted(DateTime? dateStarted, ValidationContext context)
        {
            var instance = (TimesheetEntry)context.ObjectInstance;
            var totalHours = instance.TimeSpent.GetValueOrDefault().TotalHours;

            if (dateStarted.HasValue && dateStarted.Value.DayOfWeek == DayOfWeek.Sunday &&
                totalHours > 0)
            {
                return new ValidationResult("Work on Sunday is prohibited",
                    new[] { nameof(TimesheetEntry.TimeSpent), nameof(TimesheetEntry.DateStarted) });
            }

            return ValidationResult.Success;
        }
    }
}
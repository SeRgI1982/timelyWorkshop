using System;
using System.Collections.Generic;
using timely.Models;

namespace timely
{
    public class TimelyService
    {
        public ICollection<User> GetUsers()
        {
            return Database.GetUsers();
        }

        public ICollection<Project> GetProjects()
        {
            return Database.GetProjects();
        }

        public ICollection<TimesheetEntry> GetTimesheetEntries()
        {
            return Database.GetTimesheetEntries();
        }

        public TimesheetEntry GetTimesheetEntryById(Guid id)
        {
            return Database.GetTimesheetEntryById(id);
        }

        public TimesheetEntry AddTimesheetEntry(TimesheetEntry newEntry)
        {
            return Database.AddTimesheetEntry(newEntry);
        }

        public void UpdateTimesheetEntry(TimesheetEntry entry)
        {
            Database.UpdateTimesheetEntry(entry);
        }

        public void DeleteTimesheetEntry(Guid id)
        {
            Database.DeleteTimesheetEntry(id);
        }
    }
}
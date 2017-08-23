using System;
using System.Collections.Generic;
using System.Linq;
using timely.Models;

namespace timely
{
    public static class Database
    {
        private static readonly ICollection<User> Users;
        private static readonly ICollection<Project> Projects;
        private static readonly ICollection<TimesheetEntry> TimesheetEntries;   

        static Database()
        {
            Users = new List<User>(GenerateUsers());
            Projects = new List<Project>(GenerateProjects());    
            TimesheetEntries = new List<TimesheetEntry>(GenerateTimesheetEntries());
        }

        public static ICollection<User> GetUsers()
        {
            return Users;
        }

        public static ICollection<Project> GetProjects()
        {
            return Projects;
        }

        public static ICollection<TimesheetEntry> GetTimesheetEntries()
        {
            return TimesheetEntries;
        }

        public static TimesheetEntry GetTimesheetEntryById(Guid id)
        {
            var entry = new TimesheetEntry { Id = id };
            var storedEntry = TimesheetEntries.SingleOrDefault(x => x.Id == id);

            if (storedEntry == null)
            {
                throw new NullReferenceException($"There is no entry with Id={id}");
            }

            MapTimesheetEntry(entry, storedEntry);
            return entry;
        }

        public static TimesheetEntry AddTimesheetEntry(TimesheetEntry newEntry)
        {
            newEntry.Id = Guid.NewGuid();
            TimesheetEntries.Add(newEntry);

            return newEntry;
        }

        public static void UpdateTimesheetEntry(TimesheetEntry entry)
        {
            var entryToUpdate = TimesheetEntries.SingleOrDefault(x => x.Id == entry.Id);

            if (entryToUpdate == null)
            {
                throw new NullReferenceException($"There is no entry with Id={entry.Id}");
            }

            MapTimesheetEntry(entryToUpdate, entry);
        }

        public static void DeleteTimesheetEntry(Guid id)
        {
            var entryToDelete = TimesheetEntries.SingleOrDefault(x => x.Id == id);

            if (entryToDelete == null)
            {
                throw new NullReferenceException($"There is no entry with Id={id}");
            }

            TimesheetEntries.Remove(entryToDelete);
        }

        private static void MapTimesheetEntry(TimesheetEntry to, TimesheetEntry @from)
        {
            to.DateStarted = @from.DateStarted;
            to.ProjectId = @from.ProjectId;
            to.TimeSpent = @from.TimeSpent;
            to.UserId = @from.UserId;
            to.WorkDescription = @from.WorkDescription;
            to.WorkTitle = @from.WorkTitle;
        }

        private static IEnumerable<TimesheetEntry> GenerateTimesheetEntries()
        {
            var result = new List<TimesheetEntry>();

            var startDate = new DateTime(2017, 7, 1);
            var random = new Random();

            foreach (var day in Enumerable.Range(0, 75).Select(x => startDate.AddDays(x)))
            {
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                foreach (var user in Users)
                {
                    foreach (var project in Projects)
                    {
                        var timeSpent = TimeSpan.FromMinutes(random.Next(1, 180));

                        if (random.Next(0, 4) % 2 == 0 || timeSpent.TotalMinutes < 1)
                        {
                            continue;
                        }

                        var entry = new TimesheetEntry();
                        entry.Id = Guid.NewGuid();
                        entry.DateStarted = day;
                        entry.TimeSpent = timeSpent;
                        entry.ProjectId = project.Id;
                        entry.UserId = user.Id;
                        entry.WorkTitle = $"[{project.Name}] - Task#{random.Next(1, 5)}";
                        entry.WorkDescription = $"Today is {day.DayOfWeek}";

                        result.Add(entry);
                    }   
                }
            }

            return result;
        }

        private static IEnumerable<Project> GenerateProjects()
        {
            var result = new List<Project>();

            var apollo = new Project { Id = Guid.NewGuid(), Name = "Apollo" };
            var galileo = new Project { Id = Guid.NewGuid(), Name = "Galileo" };
            var genesis = new Project { Id = Guid.NewGuid(), Name = "Genesis" };
            var magellan = new Project { Id = Guid.NewGuid(), Name = "Magellan" };

            result.AddRange(new[] { apollo, galileo, genesis, magellan });

            return result;
        }

        private static IEnumerable<User> GenerateUsers()
        {
            var result = new List<User>();

            var jarek = new User { Id = Guid.NewGuid(), Name = "Jarek Gancar" };
            var lukas = new User { Id = Guid.NewGuid(), Name = "Lukas Szu" };
            var pawel = new User { Id = Guid.NewGuid(), Name = "Pawel Linee" };
            var michal = new User { Id = Guid.NewGuid(), Name = "Michal Doo" };

            result.AddRange(new[] { jarek, lukas, pawel, michal });

            return result;
        }
    }
}

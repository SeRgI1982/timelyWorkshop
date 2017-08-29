using System.Windows;
using System.Windows.Controls;
using timely.Models;

namespace timely.Infrastructure
{
    public class TimesheetEntryRowStyleSelector : StyleSelector
    {
        public Style ApolloStyle { get; set; }
        
        public Style DefaultStyle { get; set; }

        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.Windows.Style"/> based on custom logic.
        /// </summary>
        /// <returns>
        /// Returns an application-specific style to apply; otherwise, null.
        /// </returns>
        /// <param name="item">The content.</param><param name="container">The element to which the style will be applied.</param>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var timesheetEntry = item as TimesheetEntry;

            if (timesheetEntry != null)
            {
                return timesheetEntry.WorkTitle.Contains("Apollo") ? ApolloStyle : DefaultStyle;
            }

            return base.SelectStyle(item, container);
        }
    }
}
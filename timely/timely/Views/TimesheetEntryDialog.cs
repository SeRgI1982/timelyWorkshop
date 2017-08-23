using System.Windows;
using System.Windows.Controls;
using MvvmDialogs;

namespace timely.Views
{
    public class TimesheetEntryDialog : IWindow
    {
        private readonly TimesheetEntryView _dialog;

        public TimesheetEntryDialog()
        {
            _dialog = new TimesheetEntryView();
        }

        object IWindow.DataContext
        {
            get { return _dialog.DataContext; }
            set { _dialog.DataContext = value; }
        }

        bool? IWindow.DialogResult
        {
            get { return _dialog.DialogResult; }
            set { _dialog.DialogResult = value; }
        }

        ContentControl IWindow.Owner
        {
            get { return _dialog.Owner; }
            set { _dialog.Owner = (Window)value; }
        }

        bool? IWindow.ShowDialog()
        {
            return _dialog.ShowDialog();
        }

        void IWindow.Show()
        {
            _dialog.Show();
        }
    }
}
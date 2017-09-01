using MvvmDialogs;

namespace timely.Infrastructure
{
    public class DialogServiceProvider
    {
        private DialogServiceProvider()
        {
            DialogService = new DialogService();    
        }

        public IDialogService DialogService { get; }

        public static DialogServiceProvider Instance { get; } = new DialogServiceProvider();
    }
}
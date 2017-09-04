using Prism.Mvvm;

namespace timely.common.Infrastructure
{
    public abstract class ViewModelBase : BindableBase
    {
        public abstract string Name { get; }
    }
}

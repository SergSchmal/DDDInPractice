using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDDInPractice.UI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public virtual string Caption => string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

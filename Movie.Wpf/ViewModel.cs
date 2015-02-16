using System;
using System.ComponentModel;

namespace Movies.Wpf
{
    [Serializable]
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

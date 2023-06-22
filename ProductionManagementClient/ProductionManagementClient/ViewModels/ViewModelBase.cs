using ProductionManagementClient.Services.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace ProductionManagementClient.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private RelayCommand _skipColumnsCommand;
        public RelayCommand SkipColumnsCommand
        {
            get
            {
                return _skipColumnsCommand ??
                    (_skipColumnsCommand = new RelayCommand(args =>
                    {
                        var column = (DataGridAutoGeneratingColumnEventArgs)args;
                        if (column.Column.Header == "Ид")
                        {
                            column.Cancel = true;
                        }
                    }
                    ));
            }
        }
    }
}

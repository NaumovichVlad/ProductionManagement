using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels
{
    public abstract class EditViewModel<T> : ViewModelBase
        where T : ModelBase, new()
    {
        private T _model;
        private protected IApiClient _client;
        private protected IDialogService _messageBoxService;

        public T Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public EditViewModel(string id, IApiClient client, IDialogService messageBoxService)
        {
            _client = client;
            Model = GetModel(id);
            _messageBoxService = messageBoxService;
        }

        private protected RelayCommand _confirmCommand;
        public abstract RelayCommand ConfirmCommand { get; }

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new RelayCommand(obj =>
                    {
                        ((Window)obj).Close();
                    }));
            }
        }

        public abstract T GetModel(string id);

    }
}

﻿using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Materials
{
    public class PurchaseCreateViewModel : CreateViewModel<PurchaseModel>
    {
        private List<CounteragentModel> _counteragents;
        private CounteragentModel _selectedCounteragent;

        public List<CounteragentModel> Counteragents
        {
            get
            {
                return _counteragents;
            }
            set
            {
                _counteragents = value;
                OnPropertyChanged();
            }
        }
        public CounteragentModel SelectedCounteragent
        {
            get => _selectedCounteragent;
            set
            {
                _selectedCounteragent = value;
                OnPropertyChanged();
            }
        }

        public PurchaseCreateViewModel(IApiClient client, IDialogService messageBoxService)
            : base(client, messageBoxService)
        {
            Counteragents = _client.Get<List<CounteragentModel>>("counteragent/all").Result;
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.CounteragentName = SelectedCounteragent.Name;
                        Model.CounteragentId = SelectedCounteragent.Id;

                        _client.Post(Model, "material/purchase/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

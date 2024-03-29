﻿using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Windows;

namespace ProductionManagementClient.ViewModels.StoragePlaces
{
    public class StoragePlacesEditViewModel : EditViewModel<StoragePlaceModel>
    {
        public StoragePlacesEditViewModel(string id, IApiClient client, IDialogService messageBoxService)
            : base(id, client, messageBoxService)
        { }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Put(Model, $"storagePlace/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override StoragePlaceModel GetModel(string id)
        {
            return _client.Get<StoragePlaceModel>($"storagePlace/get/{id}").Result;
        }
    }
}

﻿using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Products
{
    public class ProductsCreateViewModel : CreateViewModel<ProductModel>
    {
        public ProductsCreateViewModel(IApiClient client, IDialogService messageBoxService)
            : base(client, messageBoxService)
        { }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Post(Model, $"product/insert");
                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

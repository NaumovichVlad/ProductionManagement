using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Products
{
    public class ProductsEditViewModel : EditViewModel<ProductModel>
    {
        public ProductsEditViewModel(string id, IApiClient client, IDialogService messageBoxService)
            : base(id, client, messageBoxService)
        { }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Put(Model, $"product/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override ProductModel GetModel(string id)
        {
            return _client.Get<ProductModel>($"product/get/{id}").Result;
        }
    }
}

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

namespace ProductionManagementClient.ViewModels.Products
{
    public class FinishedProductsEditViewModel : EditViewModel<FinishedProductModel>
    {
        private List<ProductModel> _products;
        private ProductModel _selectedProduct;

        public List<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }
        public FinishedProductsEditViewModel(string id, IApiClient client, IDialogService messageBoxService) : base(id, client, messageBoxService)
        {
            Products = _client.Get<List<ProductModel>>("product/all").Result;
            SelectedProduct = Products.Where(p => p.Id == Model.ProductId).First();
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.ProductName = SelectedProduct.Name;
                        Model.ProductId = SelectedProduct.Id;

                        _client.Put(Model, $"product/finished/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override FinishedProductModel GetModel(string id)
        {
            return _client.Get<FinishedProductModel>($"product/finished/get/{id}").Result;
        }
    }
}

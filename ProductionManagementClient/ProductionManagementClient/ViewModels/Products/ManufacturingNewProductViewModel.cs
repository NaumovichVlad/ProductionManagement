using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProductionManagementClient.ViewModels.Products
{
    public class ManufacturingNewProductViewModel : CreateViewModel<FinishedProductModel>
    {
        private List<ProductModel> _products;
        private ProductModel _selectedProduct;
        private int _count;

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        public ProductModel SelectedPeoduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }
        public List<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ManufacturingNewProductViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Products = _client.Get<List<ProductModel>>("product/all").Result;
            SelectedPeoduct = Products.First();
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        var result = _client.Post($"product/finished/create/{SelectedPeoduct.Id}/{Count}").Result;

                        _messageBoxService.ShowMessage("Cоздание", result.Content.ReadAsStringAsync().Result.Replace("\"", ""));

                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

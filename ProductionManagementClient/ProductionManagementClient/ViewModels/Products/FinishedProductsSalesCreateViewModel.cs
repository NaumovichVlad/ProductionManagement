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
    public class FinishedProductsSalesCreateViewModel : CreateViewModel<FinishedProductSaleModel>
    {
        private List<ProductReserveModel> _products;
        private List<SaleModel> _sales;
        private ProductReserveModel _selectedProduct;
        private SaleModel _selectedSale;

        public List<ProductReserveModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
        public List<SaleModel> Sales
        {
            get => _sales;
            set
            {
                _sales = value;
                OnPropertyChanged();
            }
        }
        public ProductReserveModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }
        public SaleModel SelectedSale
        {
            get => _selectedSale;
            set
            {
                _selectedSale = value;
                OnPropertyChanged();
            }
        }

        public FinishedProductsSalesCreateViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Products = _client.Get<List<ProductReserveModel>>("product/reserve/all").Result;
            Sales = _client.Get<List<SaleModel>>("product/sale/all").Result;
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.SaleNumber = SelectedSale.OrderNumber.ToString();
                        Model.ProductName = SelectedProduct.FinishedProductName;
                        Model.SaleId = SelectedSale.Id;
                        Model.ProductsReserveId = SelectedProduct.Id;

                        _client.Post(Model, "product/finished/sale/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

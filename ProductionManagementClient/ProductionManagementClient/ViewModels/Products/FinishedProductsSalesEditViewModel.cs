using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Products
{
    public class FinishedProductsSalesEditViewModel : EditViewModel<FinishedProductSaleModel>
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

        public FinishedProductsSalesEditViewModel(string id, IApiClient client, IDialogService messageBoxService) : base(id, client, messageBoxService)
        {
            Products = _client.Get<List<ProductReserveModel>>("product/reserve/all").Result;
            Sales = _client.Get<List<SaleModel>>("product/sale/all").Result;
            SelectedProduct = Products.Where(m => m.Id == Model.ProductsReserveId).First();
            SelectedSale = Sales.Where(p => p.Id == Model.SaleId).First();
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

                        _client.Put(Model, $"product/finished/sale/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override FinishedProductSaleModel GetModel(string id)
        {
            return _client.Get<FinishedProductSaleModel>($"product/finished/sale/get/{id}").Result;
        }
    }
}

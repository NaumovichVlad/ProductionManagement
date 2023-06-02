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
    public class ProductsReservesCreateViewModel : CreateViewModel<ProductReserveModel>
    {
        private List<FinishedProductModel> _products;
        private List<StoragePlaceModel> _storagePlaces;
        private FinishedProductModel _selectedProduct;
        private StoragePlaceModel _selectedStoragePlace;
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
        public List<FinishedProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
        public List<StoragePlaceModel> StoragePlaces
        {
            get => _storagePlaces;
            set
            {
                _storagePlaces = value;
                OnPropertyChanged();
            }
        }
        public FinishedProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                Count = _selectedProduct.Count;
                OnPropertyChanged();
            }
        }
        public StoragePlaceModel SelectedStoragePlace
        {
            get => _selectedStoragePlace;
            set
            {
                _selectedStoragePlace = value;
                OnPropertyChanged();
            }
        }
        public ProductsReservesCreateViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Products = _client.Get<List<FinishedProductModel>>("product/finished/notAccepted").Result;
            StoragePlaces = _client.Get<List<StoragePlaceModel>>("storagePlace/all").Result;
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.FinishedProductName = SelectedProduct.ProductName;
                        Model.StoragePlaceName = SelectedStoragePlace.Name;
                        Model.FinishedProductId = SelectedProduct.ProductId;
                        Model.StoragePlaceId = SelectedStoragePlace.Id;
                        Model.Count = Count;
                        SelectedProduct.IsApproved = true;

                        _client.Put(SelectedProduct, $"product/finished/edit");
                        _client.Post(Model, "product/reserve/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

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

namespace ProductionManagementClient.ViewModels.Materials
{
    public class MaterialReserveEditViewModel : EditViewModel<MaterialReserveModel>
    {
        private List<MaterialOrderModel> _orders;
        private List<StoragePlaceModel> _storagePlaces;
        private MaterialOrderModel _selectedOrder;
        private StoragePlaceModel _selectedStoragePlace;

        public List<MaterialOrderModel> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
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
        public MaterialOrderModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
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
        public MaterialReserveEditViewModel(string id, IApiClient client, IMessageBoxService messageBoxService) : base(id, client, messageBoxService)
        {
            Orders = _client.Get<List<MaterialOrderModel>>("material/order/all").Result;
            StoragePlaces = _client.Get<List<StoragePlaceModel>>("storagePlace/all").Result;

            SelectedOrder = Orders.Where(o => o.Id == Model.MaterialOrderId).First();
            SelectedStoragePlace = StoragePlaces.Where(s => s.Id == Model.StoragePlaceId).First();
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                        (_confirmCommand = new RelayCommand(obj =>
                        {
                            Model.MaterialOrderNumber = SelectedOrder.OrderNumber;
                            Model.StoragePlaceName = SelectedStoragePlace.Name;
                            Model.MaterialOrderId = SelectedOrder.Id;
                            Model.StoragePlaceId = SelectedStoragePlace.Id;

                            _client.Put(Model, $"material/reserve/edit");
                            _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                            ((Window)obj).Close();

                        }));
            }
        }

        public override MaterialReserveModel GetModel(string id)
        {
            return _client.Get<MaterialReserveModel>($"material/reserve/get/{id}").Result;
        }
    }
}

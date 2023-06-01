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
    public class MaterialReserveCreateViewModel : CreateViewModel<MaterialReserveModel>
    {
        private List<MaterialPurchaseModel> _materials;
        private List<StoragePlaceModel> _storagePlaces;
        private MaterialPurchaseModel _selectedMaterial;
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
        public List<MaterialPurchaseModel> Materials 
        { 
            get => _materials;
            set
            {
                _materials = value;
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
        public MaterialPurchaseModel SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                Count = _selectedMaterial.Count;
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

        public MaterialReserveCreateViewModel(IApiClient client, IDialogService messageBoxService) 
            : base(client, messageBoxService)
        {
            Materials = _client.Get<List<MaterialPurchaseModel>>("material/purchaseMaterial/notAccepted").Result;
            StoragePlaces = _client.Get<List<StoragePlaceModel>>("storagePlace/all").Result;
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.MaterialOrderNumber = SelectedMaterial.PurchaseNumber;
                        Model.MaterialName = SelectedMaterial.MaterialName;
                        Model.StoragePlaceName = SelectedStoragePlace.Name;
                        Model.MaterialPurchaseId = SelectedMaterial.Id;
                        Model.StoragePlaceId = SelectedStoragePlace.Id;
                        Model.Count = Count;
                        SelectedMaterial.IsAccepted = true;
                        
                        _client.Put(SelectedMaterial, $"material/purchaseMaterial/edit");
                        _client.Post(Model, "material/reserve/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

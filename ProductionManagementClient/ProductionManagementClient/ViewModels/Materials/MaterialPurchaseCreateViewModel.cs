using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Materials
{
    public class MaterialPurchaseCreateViewModel : CreateViewModel<MaterialPurchaseModel>
    {
        private List<MaterialModel> _materials;
        private List<PurchaseModel> _purchases;
        private MaterialModel _selectedMaterial;
        private PurchaseModel _selectedPurchase;

        public List<MaterialModel> Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }
        public List<PurchaseModel> Purchases
        {
            get => _purchases;
            set
            {
                _purchases = value;
                OnPropertyChanged();
            }
        }
        public MaterialModel SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged();
            }
        }
        public PurchaseModel SelectedPurchase
        {
            get => _selectedPurchase;
            set
            {
                _selectedPurchase = value;
                OnPropertyChanged();
            }
        }

        public MaterialPurchaseCreateViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Materials = _client.Get<List<MaterialModel>>("material/all").Result;
            Purchases = _client.Get<List<PurchaseModel>>("material/purchase/all").Result;
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.PurchaseNumber = SelectedPurchase.OrderNumber;
                        Model.MaterialName = SelectedMaterial.Name;
                        Model.PurchaseId = SelectedPurchase.Id;
                        Model.MaterialId = SelectedMaterial.Id;

                        _client.Post(Model, "material/purchaseMaterial/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

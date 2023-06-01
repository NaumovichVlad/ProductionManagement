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
    public class MaterialPurchaseEditViewModel : EditViewModel<MaterialPurchaseModel>
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
        public MaterialPurchaseEditViewModel(string id, IApiClient client, IDialogService messageBoxService) : base(id, client, messageBoxService)
        {
            Materials = _client.Get<List<MaterialModel>>("material/all").Result;
            Purchases = _client.Get<List<PurchaseModel>>("material/purchase/all").Result;
            SelectedMaterial = Materials.Where(m => m.Id == Model.MaterialId).First();
            SelectedPurchase = Purchases.Where(p => p.Id == Model.PurchaseId).First();
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

                        _client.Put(Model, $"material/purchaseMaterial/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override MaterialPurchaseModel GetModel(string id)
        {
            return _client.Get<MaterialPurchaseModel>($"material/purchaseMaterial/get/{id}").Result;
        }
    }
}

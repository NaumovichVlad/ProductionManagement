using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Materials
{
    public class MaterialsForFinishedProductsEditViewModel : EditViewModel<MaterialsForFinishedProductsModel>
    {
        private List<MaterialReserveModel> _materials;
        private List<FinishedProductModel> _products;
        private MaterialReserveModel _selectedMaterial;
        private FinishedProductModel _selectedProduct;

        public List<MaterialReserveModel> Materials
        {
            get => _materials;
            set
            {
                _materials = value;
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
        public MaterialReserveModel SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged();
            }
        }
        public FinishedProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public MaterialsForFinishedProductsEditViewModel(string id, IApiClient client, IDialogService messageBoxService) : base(id, client, messageBoxService)
        {
            Materials = _client.Get<List<MaterialReserveModel>>("material/reserve/all").Result;
            Products = _client.Get<List<FinishedProductModel>>("product/finished/all").Result;

            SelectedMaterial = Materials.Where(m => m.Id == Model.MaterialReserveId).First();
            SelectedProduct = Products.Where(p => p.Id == Model.ProductId).First();
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.ProductName = SelectedProduct.ProductName;
                        Model.MaterialName = SelectedMaterial.MaterialName;
                        Model.ProductId = SelectedProduct.Id;
                        Model.MaterialReserveId = SelectedMaterial.Id;

                        _client.Put(Model, $"material/forFinishedProducts/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override MaterialsForFinishedProductsModel GetModel(string id)
        {
            return _client.Get<MaterialsForFinishedProductsModel>($"material/forFinishedProducts/get/{id}").Result;
        }
    }
}

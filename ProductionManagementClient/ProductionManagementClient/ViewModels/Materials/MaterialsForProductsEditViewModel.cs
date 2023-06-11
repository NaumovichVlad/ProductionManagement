using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Materials
{
    public class MaterialsForProductsEditViewModel : EditViewModel<MaterialsForProductsModel>
    {
        private List<MaterialModel> _materials;
        private List<ProductModel> _products;
        private MaterialModel _selectedMaterial;
        private ProductModel _selectedProduct;

        public List<MaterialModel> Materials
        {
            get => _materials;
            set
            {
                _materials = value;
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
        public MaterialModel SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
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

        public MaterialsForProductsEditViewModel(string id, IApiClient client, IDialogService messageBoxService) : base(id, client, messageBoxService)
        {
            Materials = _client.Get<List<MaterialModel>>("material/all").Result;
            Products = _client.Get<List<ProductModel>>("product/all").Result;

            SelectedMaterial = Materials.Where(m => m.Id == Model.MaterialId).First();
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
                        Model.MaterialName = SelectedMaterial.Name;
                        Model.ProductId = SelectedProduct.Id;
                        Model.MaterialId = SelectedMaterial.Id;

                        _client.Put(Model, $"material/forProducts/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override MaterialsForProductsModel GetModel(string id)
        {
            return _client.Get<MaterialsForProductsModel>($"material/forProducts/get/{id}").Result;
        }
    }
}

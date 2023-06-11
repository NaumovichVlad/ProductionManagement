using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Materials
{
    public class MaterialsForProductsCreateViewModel : CreateViewModel<MaterialsForProductsModel>
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

        public MaterialsForProductsCreateViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Materials = _client.Get<List<MaterialModel>>("material/all").Result;
            Products = _client.Get<List<ProductModel>>("product/all").Result;
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

                        _client.Post(Model, "material/forProducts/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

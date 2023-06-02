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
    public class MaterialsForFinishedProductsCreateViewModel : CreateViewModel<MaterialsForFinishedProductsModel>
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

        public MaterialsForFinishedProductsCreateViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Materials = _client.Get<List<MaterialReserveModel>>("material/reserve/all").Result;
            Products = _client.Get<List<FinishedProductModel>>("product/finished/all").Result;
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

                        _client.Post(Model, "material/forFinishedProducts/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

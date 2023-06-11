using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.Views.Counteragents;
using ProductionManagementClient.Views.Products;
using ProductionManagementClient.Views.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class ManufacturerMainViewModel : ViewModelBase
    {
        private readonly IApiClient _client;
        private readonly IWindowService _windowService;
        private readonly IDialogService _dialogService;
        private DataTable _materials;
        private DataTable _products;
        private List<ProductModel> _cbProducts;
        private ProductModel _selectedCbProduct;
        private DataTable _materialsForProducts;

        public DataTable MaterialsForProducts
        {
            get => _materialsForProducts;
            set
            {
                _materialsForProducts = value;
                OnPropertyChanged();
            }
        }

        public DataTable Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public DataTable Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }

        public List<ProductModel> CbProducts
        {
            get
            {
                return _cbProducts;
            }
            set
            {
                _cbProducts = value;
                OnPropertyChanged();
            }
        }

        public ProductModel SelectedCbProduct
        {
            get => _selectedCbProduct;
            set
            {
                _selectedCbProduct = value;

                FillMaterialsForProductsTable();

                OnPropertyChanged();
            }
        }

        public ManufacturerMainViewModel(IApiClient client, IWindowService windowService, IDialogService dialogService)
        {
            _client = client;
            _windowService = windowService;
            _dialogService = dialogService;

            CbProducts = _client.Get<List<ProductModel>>("product/all").Result;
            SelectedCbProduct = CbProducts.First();

            Update();
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(param =>
                    {
                        _windowService.ShowWindowDialog<ManufacturingNewProductWIn>();
                    }));
            }
        }

        private void Update()
        {
            FillMaterialsTable();
            FillProductsTable();
            FillMaterialsForProductsTable();
        }

        private void FillMaterialsTable()
        {
            var materials = _client.Get<List<AvailableMaterialModel>>("material/reserve/available/all").Result;
            Materials = CreateMaterialsTable(materials);
        }

        private void FillProductsTable()
        {
            var materials = _client.Get<List<AvailableProductModel>>("product/reserve/available/all").Result;
            Products = CreateProductsTable(materials);
        }

        private void FillMaterialsForProductsTable()
        {
            var materialsForProducts = _client.Get<List<MaterialsForProductsModel>>($"material/forProducts/byProduct/get/{SelectedCbProduct.Id}").Result;
            MaterialsForProducts = CreateMaterialsForProductsTable(materialsForProducts);
        }

        private DataTable CreateMaterialsTable(List<AvailableMaterialModel> models)
        {
            var table = new DataTable();

            var nameColumn = new DataColumn("Материал");
            var countColumn = new DataColumn("Количество");

            table.Columns.Add(nameColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[nameColumn] = model.Name;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialsForProductsTable(List<MaterialsForProductsModel> models)
        {
            var table = new DataTable();

            var nameColumn = new DataColumn("Материал");
            var countColumn = new DataColumn("Количество");

            table.Columns.Add(nameColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[nameColumn] = model.MaterialName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateProductsTable(List<AvailableProductModel> models)
        {
            var table = new DataTable();

            var nameColumn = new DataColumn("Продукт");
            var countColumn = new DataColumn("Количество");

            table.Columns.Add(nameColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[nameColumn] = model.Name;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }
    }
}

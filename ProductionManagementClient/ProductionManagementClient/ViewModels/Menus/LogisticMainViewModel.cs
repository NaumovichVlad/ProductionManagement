using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.Views.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class LogisticMainViewModel : ViewModelBase
    {
        private Container _materialsContainer;
        private Container _productsContainer;
        private List<StoragePlaceModel> _storagePlaces;
        private StoragePlaceModel _selectedStoragePlaceProd;
        private StoragePlaceModel _selectedStoragePlaceMat;
        private IApiClient _client;
        private IWindowService _windowService;

        public Container Productions
        {
            get 
            { 
                return _productsContainer; 
            }
            set 
            { 
                _productsContainer = value;
                OnPropertyChanged();
            }
        }
        public Container Materials 
        { 
            get => _materialsContainer;
            set
            {
                _materialsContainer = value;
                OnPropertyChanged();
            }
        }
        public StoragePlaceModel SelectedStoragePlaceProd
        {
            get
            {
                return _selectedStoragePlaceProd;
            }
            set
            {
                _selectedStoragePlaceProd = value;
                FillProductsReserves();
                OnPropertyChanged();
            }
        }
        public StoragePlaceModel SelectedStoragePlaceMat
        {
            get => _selectedStoragePlaceMat;
            set
            {
                _selectedStoragePlaceMat = value;
                FillMaterialReserves();
                OnPropertyChanged();
            }
        }

        public List<StoragePlaceModel> StoragePlaces
        {
            get
            {
                return _storagePlaces;
            }
            set
            {
                _storagePlaces = value;
                OnPropertyChanged();
            }
        }

        public LogisticMainViewModel(IApiClient apiClient, IWindowService windowService)
        {
            _client = apiClient;
            _windowService = windowService;
            StoragePlaces = _client.Get<List<StoragePlaceModel>>("storagePlace/all").Result;

            FillMaterialsContainer();
            FillProductsContainer();

            SelectedStoragePlaceProd = StoragePlaces.First();
            SelectedStoragePlaceMat = StoragePlaces.First();
            
            

            
        }

        private void FillMaterialsContainer()
        {
            Materials = new Container();
            Materials.PendingUnloading = CreateMaterialOrderTable(
                _client.Get<List<MaterialPurchaseModel>>("material/reserve/pending").Result);
        }

        private void FillMaterialReserves()
        {
            Materials.Reserves = CreateMaterialReserveTable(
                _client.Get<List<MaterialReserveModel>>($"material/reserve/place/{_selectedStoragePlaceMat.Id}").Result);
        }

        private void FillProductsContainer()
        {
            Productions = new Container();
            Productions.PendingUnloading = CreateFinishedProductTable(
                _client.Get<List<FinishedProductModel>>("product/reserve/pending").Result);
        }

        private void FillProductsReserves()
        {
            Productions.Reserves = CreateProductReserveTable(
                _client.Get<List<ProductReserveModel>>($"product/reserve/place/{_selectedStoragePlaceProd.Id}").Result);
        }

        private RelayCommand _addMaterialReserveCommand;
        public RelayCommand AddMaterialReserveCommand
        {
            get
            {
                return _addMaterialReserveCommand ??
                    (_addMaterialReserveCommand = new RelayCommand(param =>
                    {
                        var row = (DataRowView)param;
                        _windowService.ShowWindowDialog<MaterialInvoiceWin>(row["Ид"]);
                    }));
            }
        }

        private RelayCommand _updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand ??
                    (_updateCommand = new RelayCommand(param =>
                    {
                        Materials.PendingUnloading = CreateMaterialOrderTable(
                            _client.Get<List<MaterialPurchaseModel>>("material/reserve/pending").Result);
                    }));
            }
        }

        private RelayCommand _addProductReserveCommand;
        public RelayCommand AddProductReserveCommand
        {
            get
            {
                return _addProductReserveCommand ??
                    (_addProductReserveCommand = new RelayCommand(param =>
                    {
                        var row = (DataRowView)param;
                        var product = _client.Get<FinishedProductModel>($"product/finished/get/{row["Ид"]}").Result;

                        product.IsApproved = true;

                        _client.Put(product, "product/finished/edit");

                        var reserve = new ProductReserveModel()
                        {
                            FinishedProductId = product.Id,
                            StoragePlaceId = SelectedStoragePlaceProd.Id,
                            Count = product.Count,
                        };

                        _client.Post(reserve, "product/reserve/insert");
                    }));
            }
        }

        private RelayCommand _updateProductCommand;
        public RelayCommand UpdateProductCommand
        {
            get
            {
                return _updateProductCommand ??
                    (_updateProductCommand = new RelayCommand(param =>
                    {
                        Productions.PendingUnloading = CreateFinishedProductTable(
                            _client.Get<List<FinishedProductModel>>("product/reserve/pending").Result);
                    }));
            }
        }

        private RelayCommand _availableMaterialsReportCommand;
        public RelayCommand AvailableMaterialsReportCommand
        {
            get
            {
                return _availableMaterialsReportCommand ??
                    (_availableMaterialsReportCommand = new RelayCommand(param =>
                    {
                        _windowService.ShowWindowDialog<AvailableMaterialsReportWin>();
                    }));
            }
        }

        private RelayCommand _consumptionMaterialsReportCommand;
        public RelayCommand ConsumptionMaterialsReportCommand
        {
            get
            {
                return _consumptionMaterialsReportCommand ??
                    (_consumptionMaterialsReportCommand = new RelayCommand(param =>
                    {
                        _windowService.ShowWindowDialog<ConsumptionMaterialReportWin>();
                    }));
            }
        }

        public class Container : ModelBase
        {
            private DataTable _reserves;
            private DataTable _pendingUnloading;
            

            public DataTable Reserves 
            { 
                get => _reserves;
                set
                {
                    _reserves = value;
                    OnPropertyChanged();
                }
            }
            public DataTable PendingUnloading 
            { 
                get => _pendingUnloading;
                set
                {
                    _pendingUnloading = value;
                    OnPropertyChanged();
                }
            }
        }

        private DataTable CreateMaterialOrderTable(List<MaterialPurchaseModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Номер заказа");
            numberColumn.Caption = "OrderNumber";

            var materialColumn = new DataColumn("Материал");
            materialColumn.Caption = "MaterialName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            var priceColumn = new DataColumn("Стоимость");
            priceColumn.Caption = "Price";

            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);
            table.Columns.Add(priceColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.PurchaseNumber;
                newRow[materialColumn] = model.MaterialName;
                newRow[countColumn] = model.Count;
                newRow[priceColumn] = model.Price;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialReserveTable(List<MaterialReserveModel> models)
        {
            var table = new DataTable();

            var numberColumn = new DataColumn("Номер заказа");
            numberColumn.Caption = "MaterialOrderNumber";

            var materialColumn = new DataColumn("Название");
            materialColumn.Caption = "MaterialName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            table.Columns.Add(numberColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();
                var order = model.MaterialOrderNumber;

                newRow[numberColumn] = model.MaterialOrderNumber;

                newRow[materialColumn] = model.MaterialName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateFinishedProductTable(List<FinishedProductModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var nameColumn = new DataColumn("Изделие");
            nameColumn.Caption = "ProductName";

            var dateColumn = new DataColumn("Дата изготовления");
            dateColumn.Caption = "ManufactureDate";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            table.Columns.Add(idColumn);
            table.Columns.Add(nameColumn);
            table.Columns.Add(dateColumn);
            table.Columns.Add(countColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[nameColumn] = model.ProductName;
                newRow[dateColumn] = model.ManufactureDate.ToShortDateString();
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateProductReserveTable(List<ProductReserveModel> models)
        {
            var table = new DataTable();

            var materialColumn = new DataColumn("Название");
            materialColumn.Caption = "FinishedProductName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[materialColumn] = model.FinishedProductName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }
    }
}

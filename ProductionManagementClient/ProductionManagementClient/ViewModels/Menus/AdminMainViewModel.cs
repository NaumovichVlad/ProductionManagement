using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.ViewModels.Products;
using ProductionManagementClient.Views;
using ProductionManagementClient.Views.Counteragents;
using ProductionManagementClient.Views.Materials;
using ProductionManagementClient.Views.Products;
using ProductionManagementClient.Views.Roles;
using ProductionManagementClient.Views.Salaries;
using ProductionManagementClient.Views.StoragePlaces;
using ProductionManagementClient.Views.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class AdminMainViewModel : ViewModelBase
    {
        private const int pageSize = 10;
        private DataContainer _dataContainer;
        private int _pageIndex;
        private readonly IApiClient _client;
        private DataTypes _dataType;
        private readonly IWindowService _windowService;
        private readonly IDialogService _dialogService;

        public DataContainer DataContainer
        {
            get
            {
                return _dataContainer;
            }
            set
            {
                _dataContainer = value;
                OnPropertyChanged();
            }
        }

        public int PageIndex
        {
            get => _pageIndex + 1;
            set
            {
                _pageIndex = value - 1;
                OnPropertyChanged();
            }
        }

        public AdminMainViewModel(IApiClient client, IWindowService windowService, IDialogService dialogService)
        {
            _client = client;
            _windowService = windowService;
            _dataType = DataTypes.Users;
            _dialogService = dialogService;

            Clear();
        }

        private RelayCommand _selectItemCommand;
        public RelayCommand SelectItemCommand
        {
            get
            {
                return _selectItemCommand ??
                    (_selectItemCommand = new RelayCommand(param =>
                    {
                        Clear();
                        Enum.TryParse<DataTypes>(((TreeViewItem)param).Name, out var item);
                        _dataType = item;

                        GetServerEntities();
                    }
                    ));
            }
        }

        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(param =>
                    {
                        AddServerEntity();

                        Clear();
                    }));
            }
        }

        private RelayCommand _editCommand;

        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new RelayCommand(param =>
                    {
                        if (param != null)
                        {
                            var row = (DataRowView)param;
                            EditServerEntity(row["Ид"]);

                            Clear();
                        }
                        else
                        {
                            _dialogService.ShowErrorMessage("Внимание", "Выберите элемент таблицы");
                        }
                    }));
            }
        }

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(param =>
                    {
                        if (param != null)
                        {
                            var row = (DataRowView)param;
                            DeleteServerEntity(row["Ид"]);

                            Clear();
                        }
                        else
                        {
                            _dialogService.ShowErrorMessage("Внимание", "Выберите элемент таблицы");
                        }
                    }));
            }
        }


        private RelayCommand _sortCommand;

        public RelayCommand SortCommand
        {
            get
            {
                return _sortCommand ??
                    (_sortCommand = new RelayCommand(param =>
                    {
                        PageIndex = 1;
                        var column = ((DataGridSortingEventArgs)param).Column;
                        var name = _dataContainer.Table.Columns[column.Header.ToString()].Caption;
                        _dataContainer.SortParam = name;
                        _dataContainer.SortDirection = _dataContainer.SortDirection == "asc" ? "desc" : "asc";

                        GetServerEntities();
                    }));
            }
        }

        private RelayCommand _nextPageCommand;
        public RelayCommand NextPageCommand
        {
            get
            {
                return _nextPageCommand ??
                    (_nextPageCommand = new RelayCommand(param =>
                    {
                        PageIndex++;
                        Enum.TryParse(typeof(DataTypes), ((TreeViewItem)param).Name, out var item);

                        GetServerEntities();
                    }
                    ));
            }
        }
        private RelayCommand _previousPageCommand;
        public RelayCommand PreviousPageCommand
        {
            get
            {
                return _previousPageCommand ??
                    (_previousPageCommand = new RelayCommand(param =>
                    {
                        PageIndex--;
                        Enum.TryParse(typeof(DataTypes), ((TreeViewItem)param).Name, out var item);

                        GetServerEntities();
                    }
                    ));
            }
        }
        private RelayCommand _skipColumnsCommand;
        public RelayCommand SkipColumnsCommand
        {
            get
            {
                return _skipColumnsCommand ??
                    (_skipColumnsCommand = new RelayCommand(args =>
                    {
                        var column = (DataGridAutoGeneratingColumnEventArgs)args;
                        if (column.Column.Header == "Ид")
                        {
                            column.Cancel = true;
                        }
                    }
                    ));
            }
        }
        private void GetServerEntities()
        {
            switch (_dataType)
            {
                case DataTypes.Roles:
                    DataContainer.Table = CreateRoleTable(_client.Get<List<RoleModel>>(
                        $"role/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Users:
                    DataContainer.Table = CreateUserTable(_client.Get<List<UserModel>>(
                        $"user/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Employees:
                    DataContainer.Table = CreateEmployeeTable(_client.Get<List<EmployeeModel>>(
                        $"employee/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Counteragents:
                    DataContainer.Table = CreateCounteragentTable(_client.Get<List<CounteragentModel>>(
                        $"counteragent/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.StoragePlaces:
                    DataContainer.Table = CreateStoragePlaceTable(_client.Get<List<StoragePlaceModel>>(
                        $"storagePlace/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Materials:
                    DataContainer.Table = CreateMaterialTable(_client.Get<List<MaterialModel>>(
                        $"material/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Products:
                    DataContainer.Table = CreateProductTable(_client.Get<List<ProductModel>>(
                        $"product/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Purchases:
                    DataContainer.Table = CreatePurchaseTable(_client.Get<List<PurchaseModel>>(
                        $"material/purchase/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.MaterialsPurchases:
                    DataContainer.Table = CreateMaterialPurchaseTable(_client.Get<List<MaterialPurchaseModel>>(
                        $"material/purchaseMaterial/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.MaterialReserves:
                    DataContainer.Table = CreateMaterialReserveTable(_client.Get<List<MaterialReserveModel>>(
                        $"material/reserve/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Salaries:
                    DataContainer.Table = CreateSalaryTable(_client.Get<List<SalaryModel>>(
                        $"salary/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.FinishedProducts:
                    DataContainer.Table = CreateFinishedProductTable(_client.Get<List<FinishedProductModel>>(
                        $"product/finished/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.ProductsReserves:
                    DataContainer.Table = CreateProductReserveTable(_client.Get<List<ProductReserveModel>>(
                        $"product/reserve/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Sales:
                    DataContainer.Table = CreateSaleTable(_client.Get<List<SaleModel>>(
                        $"product/sale/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.FinishedProductsSales:
                    DataContainer.Table = CreateFinishedProductSaleTable(_client.Get<List<FinishedProductSaleModel>>(
                        $"product/finished/sale/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.MaterialsForProducts:
                    DataContainer.Table = CreateMaterialsForProductsTable(_client.Get<List<MaterialsForProductsModel>>(
                        $"material/forProducts/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.MaterialsForFinishedProducts:
                    DataContainer.Table = CreateMaterialsForFinishedProductsTable(_client.Get<List<MaterialsForFinishedProductsModel>>(
                        $"material/forFinishedProducts/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                default:
                    break;
            }
        }

        private void EditServerEntity(object id)
        {
            switch (_dataType)
            {
                case DataTypes.Employees:
                    _windowService.ShowWindowDialog<EmployeesEditWin>(id);
                    break;
                case DataTypes.Roles:
                    _windowService.ShowWindowDialog<RolesEditWin>(id);
                    break;
                case DataTypes.Users:
                    _windowService.ShowWindowDialog<UsersEditWin>(id);
                    break;
                case DataTypes.Counteragents:
                    _windowService.ShowWindowDialog<CounteragentsEditWin>(id);
                    break;
                case DataTypes.StoragePlaces:
                    _windowService.ShowWindowDialog<StoragePlacesEditWin>(id);
                    break;
                case DataTypes.Products:
                    _windowService.ShowWindowDialog<ProductsEditWin>(id);
                    break;
                case DataTypes.Materials:
                    _windowService.ShowWindowDialog<MaterialsEditWin>(id);
                    break;
                case DataTypes.Purchases:
                    _windowService.ShowWindowDialog<PurchaseEditWin>(id);
                    break;
                case DataTypes.MaterialsPurchases:
                    _windowService.ShowWindowDialog<MaterialPurchaseEditWin>(id);
                    break;
                case DataTypes.Salaries:
                    _windowService.ShowWindowDialog<SalariesEditWin>(id);
                    break;
                case DataTypes.FinishedProducts:
                    _windowService.ShowWindowDialog<FinishedProductsEditWin>(id);
                    break;
                case DataTypes.Sales:
                    _windowService.ShowWindowDialog<SalesEditWIn>(id);
                    break;
                case DataTypes.FinishedProductsSales:
                    _windowService.ShowWindowDialog<FinishedProductsSalesEditWin>(id);
                    break;
                case DataTypes.MaterialsForProducts:
                    _windowService.ShowWindowDialog<MaterialsForProductsEditWin>(id);
                    break;
                case DataTypes.MaterialsForFinishedProducts:
                    _windowService.ShowWindowDialog<MaterialsForFinishedProductsEditWin>(id);
                    break;
                default:
                    _dialogService.ShowErrorMessage("Предупреждение", "Редактирование невозможно");
                    break;
            }
        }

        private void AddServerEntity()
        {
            switch (_dataType)
            {
                case DataTypes.Employees:
                    _windowService.ShowWindowDialog<EmployeesCreateWin>();
                    break;
                case DataTypes.Roles:
                    _windowService.ShowWindowDialog<RolesCreateWin>();
                    break;
                case DataTypes.Users:
                    _windowService.ShowWindowDialog<UsersCreateWin>();
                    break;
                case DataTypes.Counteragents:
                    _windowService.ShowWindowDialog<CounteragentsCreateWin>();
                    break;
                case DataTypes.StoragePlaces:
                    _windowService.ShowWindowDialog<StoragePlacesCreateWin>();
                    break;
                case DataTypes.Products:
                    _windowService.ShowWindowDialog<ProductsCreateWin>();
                    break;
                case DataTypes.Materials:
                    _windowService.ShowWindowDialog<MaterialsCreateWin>();
                    break;
                case DataTypes.Purchases:
                    _windowService.ShowWindowDialog<PurchaseCreateWin>();
                    break;
                case DataTypes.MaterialsPurchases:
                    _windowService.ShowWindowDialog<MaterialPurchaseCreateWin>();
                    break;
                case DataTypes.MaterialReserves:
                    _windowService.ShowWindowDialog<MaterialReserveCreateWin>();
                    break;
                case DataTypes.Salaries:
                    _windowService.ShowWindowDialog<SalariesCreateWin>();
                    break;
                case DataTypes.FinishedProducts:
                    _windowService.ShowWindowDialog<FinishedProductsCreateWin>();
                    break;
                case DataTypes.ProductsReserves:
                    _windowService.ShowWindowDialog<ProductsReservesCreateWin>();
                    break;
                case DataTypes.Sales:
                    _windowService.ShowWindowDialog<SalesCreateWin>();
                    break;
                case DataTypes.FinishedProductsSales:
                    _windowService.ShowWindowDialog<FinishedProductsSalesCreateWin>();
                    break;
                case DataTypes.MaterialsForProducts:
                    _windowService.ShowWindowDialog<MaterialsForProductsCreateWin>();
                    break;
                case DataTypes.MaterialsForFinishedProducts:
                    _windowService.ShowWindowDialog<MaterialsForFinishedProductsCreateWin>();
                    break;
            }
        }

        private void DeleteServerEntity(object id)
        {
            switch (_dataType)
            {
                case DataTypes.Employees:
                    _client.Delete($"employee/remove/{id}");
                    break;
                case DataTypes.Roles:
                    _client.Delete($"role/remove/{id}");
                    break;
                case DataTypes.Users:
                    _client.Delete($"user/remove/{id}");
                    break;
                case DataTypes.Counteragents:
                    _client.Delete($"counteragent/remove/{id}");
                    break;
                case DataTypes.StoragePlaces:
                    _client.Delete($"storagePlace/remove/{id}");
                    break;
                case DataTypes.Products:
                    _client.Delete($"product/remove/{id}");
                    break;
                case DataTypes.Materials:
                    _client.Delete($"material/remove/{id}");
                    break;
                case DataTypes.Purchases:
                    _client.Delete($"material/purchase/remove/{id}");
                    break;
                case DataTypes.MaterialsPurchases:
                    _client.Delete($"material/purchaseMaterial/remove/{id}");
                    break;
                case DataTypes.MaterialReserves:
                    _client.Delete($"material/reserve/remove/{id}");
                    break;
                case DataTypes.Salaries:
                    _client.Delete($"salary/remove/{id}");
                    break;
                case DataTypes.FinishedProducts:
                    _client.Delete($"product/finished/remove/{id}");
                    break;
                case DataTypes.ProductsReserves:
                    _client.Delete($"product/reserve/remove/{id}");
                    break;
                case DataTypes.Sales:
                    _client.Delete($"product/sale/remove/{id}");
                    break;
                case DataTypes.MaterialsForProducts:
                    _client.Delete($"material/forProducts/remove/{id}");
                    break;
                case DataTypes.MaterialsForFinishedProducts:
                    _client.Delete($"material/forFinishedProducts/remove/{id}");
                    break;
            }
        }

        private void Clear()
        {
            PageIndex = 1;

            DataContainer = new DataContainer();

            DataContainer.DataType = _dataType;
            DataContainer.PageNumber = 1;
            DataContainer.SortDirection = "asc";
            DataContainer.SortParam = "Id";

            GetServerEntities();
        }

        private DataTable CreateRoleTable(List<RoleModel> models)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";
            

            var nameColumn = new DataColumn("Название");
            nameColumn.Caption = "Name";

            table.Columns.Add(idColumn);
            table.Columns.Add(nameColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[nameColumn] = model.Name;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateUserTable(List<UserModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var loginColumn = new DataColumn("Логин");
            loginColumn.Caption = "Login";

            var passwordColumn = new DataColumn("Пароль");
            passwordColumn.Caption = "Password";

            var surnameColumn = new DataColumn("Фамилия");
            surnameColumn.Caption = "Surname";

            var nameColumn = new DataColumn("Имя");
            nameColumn.Caption = "Name";

            var middleNameColumn = new DataColumn("Отчество");
            middleNameColumn.Caption = "MiddleName";

            var roleColumn = new DataColumn("Роль");
            roleColumn.Caption = "Role";

            table.Columns.Add(idColumn);
            table.Columns.Add(loginColumn);
            table.Columns.Add(passwordColumn);
            table.Columns.Add(roleColumn);
            table.Columns.Add(surnameColumn);
            table.Columns.Add(nameColumn);
            table.Columns.Add(middleNameColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[loginColumn] = model.Login;
                newRow[passwordColumn] = model.Password;
                newRow[roleColumn] = model.RoleName;
                newRow[surnameColumn] = model.EmployeeSurname;
                newRow[nameColumn] = model.EmployeeName;
                newRow[middleNameColumn] = model.EmployeeMiddleName;


                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateEmployeeTable(List<EmployeeModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var surnameColumn = new DataColumn("Фамилия");
            surnameColumn.Caption = "Surname";

            var nameColumn = new DataColumn("Имя");
            nameColumn.Caption = "Name";

            var middleNameColumn = new DataColumn("Отчество");
            middleNameColumn.Caption = "MiddleName";

            var passportColumn = new DataColumn("Номер паспорта");
            passportColumn.Caption = "PassportNumber";

            var addressNameColumn = new DataColumn("Адрес");
            addressNameColumn.Caption = "Address";

            var phoneColumn = new DataColumn("Телефон");
            phoneColumn.Caption = "PhoneNumber";

            table.Columns.Add(idColumn);
            table.Columns.Add(surnameColumn);
            table.Columns.Add(nameColumn);
            table.Columns.Add(middleNameColumn);
            table.Columns.Add(passportColumn);
            table.Columns.Add(phoneColumn);
            table.Columns.Add(addressNameColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[surnameColumn] = model.Surname;
                newRow[nameColumn] = model.Name;
                newRow[middleNameColumn] = model.MiddleName;
                newRow[passportColumn] = model.PassportNumber;
                newRow[addressNameColumn] = model.Address;
                newRow[phoneColumn] = model.PhoneNumber;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateCounteragentTable(List<CounteragentModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var nameColumn = new DataColumn("Название");
            nameColumn.Caption = "Name";

            var innColumn = new DataColumn("ИНН");
            innColumn.Caption = "INN";

            var accountNumberColumn = new DataColumn("Номер счёта");
            accountNumberColumn.Caption = "AccountNumber";

            var postalCodeColumn = new DataColumn("Почтовый индекс");
            postalCodeColumn.Caption = "PostalCode";

            var addressNameColumn = new DataColumn("Адрес");
            addressNameColumn.Caption = "Address";

            var registrationCountryColumn = new DataColumn("Страна регистрации");
            registrationCountryColumn.Caption = "RegistrationCountry";

            var phoneColumn = new DataColumn("Телефон");
            phoneColumn.Caption = "PhoneNumber";

            table.Columns.Add(idColumn);
            table.Columns.Add(nameColumn);
            table.Columns.Add(innColumn);
            table.Columns.Add(accountNumberColumn);
            table.Columns.Add(phoneColumn);
            table.Columns.Add(postalCodeColumn);
            table.Columns.Add(registrationCountryColumn);
            table.Columns.Add(addressNameColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[nameColumn] = model.Name;
                newRow[innColumn] = model.Inn;
                newRow[accountNumberColumn] = model.AccountNumber;
                newRow[postalCodeColumn] = model.PostalCode;
                newRow[addressNameColumn] = model.Address;
                newRow[phoneColumn] = model.PhoneNumber;
                newRow[registrationCountryColumn] = model.RegistrationCountry;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateStoragePlaceTable(List<StoragePlaceModel> models)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var nameColumn = new DataColumn("Название");
            nameColumn.Caption = "Name";

            table.Columns.Add(idColumn);
            table.Columns.Add(nameColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[nameColumn] = model.Name;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialTable(List<MaterialModel> models)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var nameColumn = new DataColumn("Название");
            nameColumn.Caption = "Name";

            table.Columns.Add(idColumn);
            table.Columns.Add(nameColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[nameColumn] = model.Name;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateProductTable(List<ProductModel> models)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var nameColumn = new DataColumn("Название");
            nameColumn.Caption = "Name";

            table.Columns.Add(idColumn);
            table.Columns.Add(nameColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[nameColumn] = model.Name;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreatePurchaseTable(List<PurchaseModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Номер заказа");
            numberColumn.Caption = "OrderNumber";

            var counteragentColumn = new DataColumn("Контерагент");
            counteragentColumn.Caption = "CounteragentName";

            var dateColumn = new DataColumn("Дата изготовления");
            dateColumn.Caption = "ManufactureDate";

            var countryColumn = new DataColumn("Страна производства");
            countryColumn.Caption = "ManufactureCountry";


            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(counteragentColumn);
            table.Columns.Add(dateColumn);
            table.Columns.Add(countryColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.OrderNumber;
                newRow[counteragentColumn] = model.CounteragentName;
                newRow[dateColumn] = model.ManufactureDate;
                newRow[countryColumn] = model.ManufactureCountry;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialPurchaseTable(List<MaterialPurchaseModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Номер заказа");
            numberColumn.Caption = "OrderNumber";

            var materialColumn = new DataColumn("Материал");
            materialColumn.Caption = "MaterialName";

            var priceColumn = new DataColumn("Стоимость");
            priceColumn.Caption = "Price";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            var isAcceptedColumn = new DataColumn("Принято на складе");
            isAcceptedColumn.Caption = "IsAccepted";


            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(priceColumn);
            table.Columns.Add(countColumn);
            table.Columns.Add(isAcceptedColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.PurchaseNumber;
                newRow[materialColumn] = model.MaterialName;
                newRow[priceColumn] = model.Price;
                newRow[countColumn] = model.Count;
                newRow[isAcceptedColumn] = model.IsAccepted ? "Принято" : "Не принято";

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialReserveTable(List<MaterialReserveModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Номер заказа");
            numberColumn.Caption = "MaterialOrderNumber";

            var materialColumn = new DataColumn("Материал");
            materialColumn.Caption = "MaterialName";

            var storagePlaceColumn = new DataColumn("Склад");
            storagePlaceColumn.Caption = "StoragePlaceName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            table.Columns.Add(idColumn);
            table.Columns.Add(storagePlaceColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.MaterialOrderNumber;
                newRow[materialColumn] = model.MaterialName;
                newRow[storagePlaceColumn] = model.StoragePlaceName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateSalaryTable(List<SalaryModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var accruedColumn = new DataColumn("Начислено");
            accruedColumn.Caption = "Accrued";

            var toBePaidColumn = new DataColumn("К выплате");
            toBePaidColumn.Caption = "ToBePaid";

            var paidColumn = new DataColumn("Выплачено");
            paidColumn.Caption = "Paid";

            var dateColumn = new DataColumn("Дата выплаты");
            dateColumn.Caption = "PaymentDate";

            var surnameColumn = new DataColumn("Фамилия");
            surnameColumn.Caption = "Surname";

            var nameColumn = new DataColumn("Имя");
            nameColumn.Caption = "Name";

            var middleNameColumn = new DataColumn("Отчество");
            middleNameColumn.Caption = "MiddleName";

            table.Columns.Add(idColumn);
            table.Columns.Add(accruedColumn);
            table.Columns.Add(toBePaidColumn);
            table.Columns.Add(paidColumn);
            table.Columns.Add(dateColumn);
            table.Columns.Add(surnameColumn);
            table.Columns.Add(nameColumn);
            table.Columns.Add(middleNameColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[accruedColumn] = model.Accrued;
                newRow[toBePaidColumn] = model.ToBePaid;
                newRow[paidColumn] = model.Paid;
                newRow[dateColumn] = model.PaymentDate;
                newRow[surnameColumn] = model.EmployeeSurname;
                newRow[nameColumn] = model.EmployeeName;
                newRow[middleNameColumn] = model.EmployeeMiddleName;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateFinishedProductTable(List<FinishedProductModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var materialColumn = new DataColumn("Продукция");
            materialColumn.Caption = "ProductName";

            var dateColumn = new DataColumn("Дата производства");
            dateColumn.Caption = "ManufactureDate";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            var isAcceptedColumn = new DataColumn("Принято на складе");
            isAcceptedColumn.Caption = "IsApproved";


            table.Columns.Add(idColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(dateColumn);
            table.Columns.Add(countColumn);
            table.Columns.Add(isAcceptedColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[materialColumn] = model.ProductName;
                newRow[dateColumn] = model.ManufactureDate.ToShortDateString();
                newRow[countColumn] = model.Count;
                newRow[isAcceptedColumn] = model.IsApproved ? "Принято" : "Не принято";

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateProductReserveTable(List<ProductReserveModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var materialColumn = new DataColumn("Продукция");
            materialColumn.Caption = "MaterialName";

            var storagePlaceColumn = new DataColumn("Склад");
            storagePlaceColumn.Caption = "StoragePlaceName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";

            table.Columns.Add(idColumn);
            table.Columns.Add(storagePlaceColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[materialColumn] = model.FinishedProductName;
                newRow[storagePlaceColumn] = model.StoragePlaceName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateSaleTable(List<SaleModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Номер заказа");
            numberColumn.Caption = "OrderNumber";

            var counteragentColumn = new DataColumn("Контерагент");
            counteragentColumn.Caption = "CounteragentName";

            var dateColumn = new DataColumn("Дата заказа");
            dateColumn.Caption = "OrderDate";

            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(counteragentColumn);
            table.Columns.Add(dateColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.OrderNumber;
                newRow[counteragentColumn] = model.CounteragentName;
                newRow[dateColumn] = model.OrderDate.ToShortDateString();

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateFinishedProductSaleTable(List<FinishedProductSaleModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Номер заказа");
            numberColumn.Caption = "OrderNumber";

            var materialColumn = new DataColumn("Продукция");
            materialColumn.Caption = "ProductNameName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";



            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.SaleNumber;
                newRow[materialColumn] = model.ProductName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialsForProductsTable(List<MaterialsForProductsModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Материал");
            numberColumn.Caption = "MaterialName";

            var materialColumn = new DataColumn("Продукция");
            materialColumn.Caption = "ProductName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";



            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.MaterialName;
                newRow[materialColumn] = model.ProductName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialsForFinishedProductsTable(List<MaterialsForFinishedProductsModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var numberColumn = new DataColumn("Материал");
            numberColumn.Caption = "MaterialName";

            var materialColumn = new DataColumn("Продукция");
            materialColumn.Caption = "ProductName";

            var countColumn = new DataColumn("Количество");
            countColumn.Caption = "Count";



            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(materialColumn);
            table.Columns.Add(countColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[numberColumn] = model.MaterialName;
                newRow[materialColumn] = model.ProductName;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }
    }
}

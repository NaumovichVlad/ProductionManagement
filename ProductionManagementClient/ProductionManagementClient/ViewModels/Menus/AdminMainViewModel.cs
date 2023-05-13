using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.Views;
using ProductionManagementClient.Views.Counteragents;
using ProductionManagementClient.Views.Roles;
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

        public AdminMainViewModel(IApiClient client, IWindowService windowService)
        {
            _client = client;
            _dataContainer = new DataContainer();
            _windowService = windowService;
            _dataType = DataTypes.Users;

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
                        var row = (DataRowView)param;
                        EditServerEntity(row["Ид"]);

                        Clear();
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
                        var row = (DataRowView)param;
                        DeleteServerEntity(row["Ид"]);

                        Clear();
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

        private void GetServerEntities()
        {
            switch (_dataType)
            {
                case DataTypes.Roles:
                    _dataContainer.Table = CreateRoleTable(_client.Get<List<RoleModel>>(
                        $"role/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Users:
                    _dataContainer.Table = CreateUserTable(_client.Get<List<UserModel>>(
                        $"user/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Employees:
                    _dataContainer.Table = CreateEmployeeTable(_client.Get<List<EmployeeModel>>(
                        $"employee/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
                case DataTypes.Counteragents:
                    _dataContainer.Table = CreateCounteragentTable(_client.Get<List<CounteragentModel>>(
                        $"counteragent/all/select/{_dataContainer.SortDirection}/{_dataContainer.SortParam}/{_pageIndex * pageSize}/{pageSize}").Result);
                    break;
            }

            
        }

        private void EditServerEntity(object id)
        {
            switch (_dataType)
            {
                case DataTypes.Employees:
                    _windowService.ShowWindow<EmployeesEditWin>(id);
                    break;
                case DataTypes.Roles:
                    _windowService.ShowWindow<RolesEditWin>(id);
                    break;
                case DataTypes.Users:
                    _windowService.ShowWindow<UsersEditWin>(id);
                    break;
                case DataTypes.Counteragents:
                    _windowService.ShowWindow<CounteragentsEditWin>(id);
                    break;
            }
        }

        private void AddServerEntity()
        {
            switch (_dataType)
            {
                case DataTypes.Employees:
                    _windowService.ShowWindow<EmployeesCreateWin>();
                    break;
                case DataTypes.Roles:
                    _windowService.ShowWindow<RolesCreateWin>();
                    break;
                case DataTypes.Users:
                    _windowService.ShowWindow<UsersCreateWin>();
                    break;
                case DataTypes.Counteragents:
                    _windowService.ShowWindow<CounteragentsCreateWin>();
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
            }
        }

        private void Clear()
        {
            PageIndex = 1;

            _dataContainer.PageNumber = 1;
            _dataContainer.SortDirection = "asc";
            _dataContainer.SortParam = "Id";

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
    }
}

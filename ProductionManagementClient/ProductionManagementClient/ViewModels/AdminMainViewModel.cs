using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductionManagementClient.ViewModels
{
    public class AdminMainViewModel : ViewModelBase
    {
        private const int pageSize = 10;
        private DataContainer _dataContainer;
        private int _pageIndex;
        private readonly IApiClient _client;
        private DataTypes _dataType;

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

        public AdminMainViewModel(IApiClient client)
        {
            _client = client;
            _dataContainer = new DataContainer();

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
            }
        }

        private void Clear()
        {
            PageIndex = 1;

            _dataContainer.PageNumber = 1;
            _dataContainer.SortDirection = "asc";
            _dataContainer.SortParam = "Id";
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

            var loginColumn = new DataColumn("Логин");
            loginColumn.Caption = "Login";

            var passwordColumn = new DataColumn("Пароль");
            passwordColumn.Caption = "Password";

            var roleColumn = new DataColumn("Роль");
            roleColumn.Caption = "Role";

            table.Columns.Add(loginColumn);
            table.Columns.Add(passwordColumn);
            table.Columns.Add(roleColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[loginColumn] = model.Login;
                newRow[passwordColumn] = model.Password;
                newRow[roleColumn] = model.Role;

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
    }
}

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
        private DataContainer _dataContainer;
        private int _pageIndex;
        private readonly IApiClient _client;

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
                _pageIndex = value;
                OnPropertyChanged();
            }
        }

        public AdminMainViewModel(IApiClient client)
        {
            _client = client;
            _dataContainer = new DataContainer();
            _dataContainer.PageSize = 1;
            PageIndex = 0;
        }

        private RelayCommand _selectItemCommand;
        public RelayCommand SelectItemCommand
        {
            get
            {
                return _selectItemCommand ??
                    (_selectItemCommand = new RelayCommand(param =>
                    {
                        Enum.TryParse(typeof(DataTypes),((TreeViewItem)param).Name, out var item);

                        switch (item)
                        {
                            case DataTypes.Roles:
                                _dataContainer.Table = CreateRoleTable(_client.Get<List<RoleModel>>("role/all").Result);
                                break;
                            case DataTypes.Users:
                                _dataContainer.Table = CreateUserTable(_client.Get<List<UserModel>>("user/all").Result);
                                break;
                            case DataTypes.Employees:
                                _dataContainer.Table = CreateEmployeeTable(_client.Get<List<EmployeeModel>>("employee/all").Result);
                                break;
                        }                     
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
                        var column = ((DataGridSortingEventArgs)param).Column;
                        var name = _dataContainer.Table.Columns[column.Header.ToString()].Caption;
                        _dataContainer.SortParam = name;
                        _dataContainer.SortDirection = column.SortDirection.Value.ToString();
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

                    }
                    ));
            }
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
            var passwordColumn = new DataColumn("Пароль");
            var roleColumn = new DataColumn("Роль");

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
            var surnameColumn = new DataColumn("Фамилия");
            var nameColumn = new DataColumn("Имя");
            var middleNameColumn = new DataColumn("Отчество");
            var passportColumn = new DataColumn("Номер паспорта");
            var addressNameColumn = new DataColumn("Адрес");
            var phoneColumn = new DataColumn("Телефон");

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

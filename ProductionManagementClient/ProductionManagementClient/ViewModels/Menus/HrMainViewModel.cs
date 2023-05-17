using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class HrMainViewModel : ViewModelBase
    {
        private readonly IApiClient _client;
        private readonly IWindowService _windowService;
        private int _pageIndex;
        private EmployeeModel _selectedEmployee;
        private DataContainer _employeesContainer;
        private DataContainer _salaries;
        private List<EmployeeModel> _employees;

        public DataContainer EmployeesContainer 
        { 
            get => _employeesContainer;
            set
            {
                _employeesContainer = value;
                OnPropertyChanged();
            }
        }
        public DataContainer Salaries 
        {
            get => _salaries;
            set
            {
                _salaries = value;
                OnPropertyChanged();
            }
        }

        public int PageIndex 
        { 
            get => _pageIndex;
            set
            {
                if (value > 0)
                {
                    _pageIndex = value;
                    EmployeesContainer.PageNumber = value - 1;
                }
                OnPropertyChanged();
            }
        }

        public EmployeeModel SelectedEmployee 
        { 
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                FillSalaries();
                OnPropertyChanged();
            }
        }

        public List<EmployeeModel> Employees 
        { 
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        public HrMainViewModel(IApiClient client, IWindowService windowService)
        {
            _client = client;
            _windowService = windowService;
            _employeesContainer = new DataContainer();
            PageIndex = 1;
            _salaries = new DataContainer();
            EmployeesContainer.PageSize = 10;
            EmployeesContainer.SortParam = "Id";
            EmployeesContainer.SortDirection = "asc";
            

            FillEmployeesContainer();
            FillEmployees();
        }

        private void FillEmployeesContainer()
        {
            EmployeesContainer.Table = CreateEmployeeTable(_client.Get<List<EmployeeModel>>(
                        $"employee/all/select/" +
                        $"{EmployeesContainer.SortDirection}/{EmployeesContainer.SortParam}/{EmployeesContainer.PageNumber * EmployeesContainer.PageSize}/{EmployeesContainer.PageSize}").Result);

        }

        private void FillEmployees()
        {
            Employees = _client.Get<List<EmployeeModel>>("employee/all").Result;
            SelectedEmployee = Employees.First();
        }

        private void FillSalaries()
        {
            Salaries.Table = CreateSalaryTable(_client.Get<List<SalaryModel>>(
                $"salary/employee/{SelectedEmployee.Id}").Result);
        }

        private RelayCommand _addEmployeeCommand;
        public RelayCommand AddEmployeeCommand
        {
            get
            {
                return _addEmployeeCommand ??
                (_addEmployeeCommand = new RelayCommand(param =>
                    {
                        _windowService.ShowWindowDialog<EmployeesCreateWin>();
                    }));
            }
        }

        private RelayCommand _editEmployeeCommand;

        public RelayCommand EditEmployeeCommand
        {
            get
            {
                return _editEmployeeCommand ??
                    (_editEmployeeCommand = new RelayCommand(param =>
                    {
                        var row = (DataRowView)param;
                        _windowService.ShowWindowDialog<EmployeesEditWin>(row["Ид"]);
                    }));
            }
        }

        private RelayCommand _deleteEmployeeCommand;

        public RelayCommand DeleteEmployeeCommand
        {
            get
            {
                return _deleteEmployeeCommand ??
                    (_deleteEmployeeCommand = new RelayCommand(param =>
                    {
                        var row = (DataRowView)param;
                        _client.Delete($"employee/remove/{row["Ид"]}");
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
                        var name = EmployeesContainer.Table.Columns[column.Header.ToString()].Caption;

                        EmployeesContainer.SortParam = name;
                        EmployeesContainer.SortDirection = EmployeesContainer.SortDirection == "asc" ? "desc" : "asc";

                        FillEmployeesContainer();
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

                        FillEmployeesContainer();
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

                        FillEmployeesContainer();
                    }
                    ));
            }
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
    }
}

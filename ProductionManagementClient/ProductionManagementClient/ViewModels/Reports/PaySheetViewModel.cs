using Aspose.Words;
using ProductionManagementClient.Connection;
using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Models.Reports;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Reports
{
    public class PaySheetViewModel : ViewModelBase
    {
        private PaySheetModel _paySheet;
        private string _savePath;
        private EmployeeModel _compilationEmployee;
        private DateTime _paymentDate;
        private readonly IDocumentService _documentService;
        private readonly IDialogService _dialogService;
        private readonly IApiClient _client;

        public EmployeeModel CompilationEmployee
        {
            get
            {
                return _compilationEmployee;
            }
            set
            {
                _compilationEmployee = value;
                OnPropertyChanged();
            }
        }
        public DateTime PaymentDate
        {
            get
            {
                return _paymentDate;
            }
            set
            {
                _paymentDate = value;
                OnPropertyChanged();
            }
        }
        public PaySheetModel PaySheet
        {
            get => _paySheet;
            set
            {
                _paySheet = value;
                OnPropertyChanged();
            }
        }
        public string SavePath
        {
            get => _savePath;
            set
            {
                _savePath = value;
                OnPropertyChanged();
            }
        }

        public PaySheetViewModel(IApiClient client, IDocumentService documentService, IDialogService dialogService)
        {
            PaySheet = new PaySheetModel();
            _documentService = documentService;
            _dialogService = dialogService;
            _client = client;

            PaymentDate = DateTime.Now;
            CompilationEmployee = GetCompilationEmployee();

            PaySheet.CompilationDate = DateTime.Now;
            PaySheet.Employees = GetEmployees();
            PaySheet.SelectedEmployee = PaySheet.Employees.First();
        }

        private List<EmployeeModel> GetEmployees()
        {
            return _client.Get<List<EmployeeModel>>("employee/all").Result;
        }

        private EmployeeModel GetCompilationEmployee()
        {
            return _client.Get<EmployeeModel>($"employee/get/{ApiConnection.User.EmployeeId}").Result;
        }

        private RelayCommand _createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand ??
                    (_createCommand = new RelayCommand(param =>
                    {
                        CreateOrderDocument();

                        ((Window)param).Close();
                    }));
            }
        }

        private RelayCommand _setPathCommand;
        public RelayCommand SetPathCommand
        {
            get
            {
                return _setPathCommand ??
                    (_setPathCommand = new RelayCommand(param =>
                    {
                        _dialogService.SaveFileDialog("Word (*.docx)|*.docx");
                        SavePath = _dialogService.FilePath;
                    }));
            }
        }

        private void CreateOrderDocument()
        {
            var salary = GetSalaryByDate(PaySheet.SelectedEmployee.Id, PaymentDate);

            _documentService.Font.Size = 12;
            _documentService.Font.Bold = true;
            _documentService.Font.Color = System.Drawing.Color.Black;
            _documentService.Font.Name = "Times New Roman";
            _documentService.Aligment = ParagraphAlignment.Right;

            _documentService.StartTable();
            _documentService.InsertCell("Номер документа");
            _documentService.InsertLastCellInRow("Дата составления");
            _documentService.InsertCell(PaySheet.OrderNumber.ToString());
            _documentService.InsertLastCellInRow(PaySheet.CompilationDate.ToShortDateString());
            _documentService.EndTable();

            _documentService.Font.Underline = Underline.Single;
            _documentService.Aligment = ParagraphAlignment.Center;
            _documentService.SkipLines(4);
            _documentService.WriteLine("УП \"Универсал Бобруйск\"");

            _documentService.SkipLines(4);
            _documentService.Font.Size = 14;
            _documentService.Font.Underline = Underline.None;
            _documentService.WriteLine("ПЛАТЁЖНАЯ ВЕДОМОСТЬ");
            _documentService.WriteLine($"за {PaymentDate.Month}.{PaymentDate.Year}");

            _documentService.Aligment = ParagraphAlignment.Left;

            _documentService.Font.Size = 12;
            _documentService.SkipLines(2);
            _documentService.WriteLine($"По настоящей платёжной ведомости выплачено" +
                $" {salary} рублей.");

            _documentService.Write("ФИО: ");
            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{PaySheet.SelectedEmployee.Surname} {PaySheet.SelectedEmployee.Name} {PaySheet.SelectedEmployee.MiddleName}");

            _documentService.Font.Bold = true;
            _documentService.Write("Выплативший сотрудник: ");
            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{CompilationEmployee.Surname} {CompilationEmployee.Name} {CompilationEmployee.MiddleName}");

            _documentService.Font.Bold = true;
            _documentService.Write("Комментарий: ");

            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{PaySheet.Note}");

            _documentService.SkipLines(4);
            _documentService.Font.Bold = true;
            _documentService.Font.Underline = Underline.None;
            _documentService.WriteLine("Руководитель организации");

            _documentService.Save(SavePath);
        }

        private int GetSalaryByDate(int employeeId, DateTime date)
        {
            var salaries = _client.Get<List<SalaryModel>>($"salary/date/{employeeId}/{date.ToShortDateString()}").Result;
            var salary = 0.0;

            foreach (var s in salaries)
            {
                salary += s.Paid;
            }

            return (int)salary;
        }
    }
}

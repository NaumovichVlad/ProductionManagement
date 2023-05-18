using Aspose.Words;
using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Reports
{
    public class DismissalOrderViewModel : ViewModelBase
    {
        private DismissalOrderModel _dismissalOrder;
        private string _savePath;
        private readonly IDocumentService _documentService;
        private readonly IDialogService _dialogService;
        private readonly IApiClient _client;

        public DismissalOrderModel DismissalOrder 
        { 
            get => _dismissalOrder;
            set
            {
                _dismissalOrder = value;
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

        public DismissalOrderViewModel(IApiClient client, IDocumentService documentService, IDialogService dialogService)
        {
            DismissalOrder = new DismissalOrderModel();
            _documentService = documentService;
            _dialogService = dialogService;
            _client = client;

            DismissalOrder.Employees = GetEmployees();
            DismissalOrder.SelectedEmployee = DismissalOrder.Employees.First();
        }

        private List<EmployeeModel> GetEmployees()
        {
            return _client.Get<List<EmployeeModel>>("employee/all").Result;
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
            _documentService.Font.Size = 12;
            _documentService.Font.Bold = true;
            _documentService.Font.Color = System.Drawing.Color.Black;
            _documentService.Font.Name = "Times New Roman";
            _documentService.Aligment = ParagraphAlignment.Right;

            _documentService.StartTable();
            _documentService.InsertCell("Номер документа");
            _documentService.InsertLastCellInRow("Дата составления");
            _documentService.InsertCell(DismissalOrder.OrderNumber.ToString());
            _documentService.InsertLastCellInRow(DismissalOrder.CompilationDate.ToShortDateString());
            _documentService.EndTable();

            _documentService.Font.Underline = Underline.Single;
            _documentService.Aligment = ParagraphAlignment.Center;
            _documentService.SkipLines(4);
            _documentService.WriteLine("УП \"Универсал Бобруйск\"");

            _documentService.SkipLines(4);
            _documentService.Font.Size = 14;
            _documentService.Font.Underline = Underline.None;
            _documentService.WriteLine("ПРИКАЗ");
            _documentService.WriteLine("(распоряжение)");
            _documentService.WriteLine("о прекращении действия трудового договора (контракта) с работником");

            _documentService.Aligment = ParagraphAlignment.Left;
            
            _documentService.Font.Size = 12;
            _documentService.SkipLines(2);
            _documentService.WriteLine($"Уволить c {DismissalOrder.DismissalDate.ToShortDateString()}.");

            _documentService.Write("ФИО: ");

            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{DismissalOrder.SelectedEmployee.Surname} {DismissalOrder.SelectedEmployee.Name} {DismissalOrder.SelectedEmployee.MiddleName}");

            _documentService.Font.Bold = true;
            _documentService.Write("Основание: ");

            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{DismissalOrder.Note}");

            _documentService.SkipLines(4);
            _documentService.Font.Bold = true;
            _documentService.Font.Underline = Underline.None;
            _documentService.WriteLine("Руководитель организации");
            _documentService.WriteLine("С приказом (распоряжением) ознакомлен");



            _documentService.Save(SavePath);
        }
    }
}

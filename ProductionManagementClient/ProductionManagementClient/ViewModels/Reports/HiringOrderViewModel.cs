using Aspose.Words;
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

namespace ProductionManagementClient.ViewModels.Reports
{
    public class HiringOrderViewModel : ViewModelBase
    {
        private HiringOrderModel _hiringOrder;
        private string _savePath;
        private readonly IDocumentService _documentService;
        private readonly IDialogService _dialogService;
        private readonly IApiClient _client;

        public HiringOrderModel HiringOrder
        {
            get => _hiringOrder;
            set
            {
                _hiringOrder = value;
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

        public HiringOrderViewModel(IApiClient client, IDocumentService documentService, IDialogService dialogService)
        {
            HiringOrder = new HiringOrderModel();
            _documentService = documentService;
            _dialogService = dialogService;
            _client = client;

            HiringOrder.Employees = GetEmployees();
            HiringOrder.SelectedEmployee = HiringOrder.Employees.First();
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
            _documentService.InsertCell(HiringOrder.OrderNumber.ToString());
            _documentService.InsertLastCellInRow(HiringOrder.CompilationDate.ToShortDateString());
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
            _documentService.WriteLine("о начале действия трудового договора (контракта) с работником");

            _documentService.Aligment = ParagraphAlignment.Left;

            _documentService.Font.Size = 12;
            _documentService.SkipLines(2);
            _documentService.WriteLine($"Принять на работу с {HiringOrder.HiringDate.ToShortDateString()}.");

            _documentService.Write("ФИО: ");

            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{HiringOrder.SelectedEmployee.Surname} {HiringOrder.SelectedEmployee.Name} {HiringOrder.SelectedEmployee.MiddleName}");

            _documentService.Font.Bold = true;
            _documentService.Write("Примечание: ");

            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{HiringOrder.Note}");

            _documentService.SkipLines(4);
            _documentService.Font.Bold = true;
            _documentService.Font.Underline = Underline.None;
            _documentService.WriteLine("Руководитель организации");
            _documentService.WriteLine("С приказом (распоряжением) ознакомлен");

            _documentService.Save(SavePath);
        }
    }
}

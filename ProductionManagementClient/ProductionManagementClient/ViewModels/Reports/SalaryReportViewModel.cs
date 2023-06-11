using Aspose.Words;
using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Reports
{
    public class SalaryReportViewModel : ViewModelBase
    {
        private readonly IDocumentService _documentService;
        private readonly IDialogService _dialogService;
        private readonly IApiClient _client;
        private string _savePath;
        private string _year;

        public string SavePath
        {
            get => _savePath;
            set
            {
                _savePath = value;
                OnPropertyChanged();
            }
        }

        public string Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged();
            }
        }

        public SalaryReportViewModel(IDocumentService documentService, IDialogService dialogService, IApiClient client)
        {
            _documentService = documentService;
            _dialogService = dialogService;
            _client = client;
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
            var salaries = _client.Get<List<SalaryModel>>($"salary/year/{int.Parse(Year)}").Result.GroupBy(s => s.EmployeeId)
                .Select(g => new
                {
                    Fio = g.Select(sl => $"{sl.EmployeeSurname} {sl.EmployeeName} {sl.EmployeeMiddleName}"),
                    Sum = g.Sum(sl => sl.Paid)
                });


            _documentService.Font.Size = 12;
            _documentService.Font.Bold = true;
            _documentService.Font.Color = System.Drawing.Color.Black;
            _documentService.Font.Name = "Times New Roman";
            _documentService.Aligment = ParagraphAlignment.Center;

            _documentService.SkipLines(2);
            _documentService.WriteLine("ОТЧЁТ");
            _documentService.WriteLine($"о зарплатах за {Year} г.");


            _documentService.Aligment = ParagraphAlignment.Left;
            _documentService.SkipLines(2);
            _documentService.StartTable();
            _documentService.InsertCell("ФИО сотрудника");
            _documentService.InsertLastCellInRow("Выплата");

            _documentService.Font.Bold = false;

            foreach (var salary in salaries)
            {
                _documentService.InsertCell(salary.Fio.First());
                _documentService.InsertLastCellInRow(salary.Sum.ToString());
            }

            _documentService.EndTable();


            _documentService.Save(SavePath);
        }
    }
}

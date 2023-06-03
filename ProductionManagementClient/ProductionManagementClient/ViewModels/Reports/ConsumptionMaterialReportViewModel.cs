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
    public class ConsumptionMaterialReportViewModel : ViewModelBase
    {
        private readonly IDocumentService _documentService;
        private readonly IDialogService _dialogService;
        private readonly IApiClient _client;
        private string _savePath;
        private MaterialModel _material;
        private List<MaterialModel> _materials;

        public string SavePath
        {
            get => _savePath;
            set
            {
                _savePath = value;
                OnPropertyChanged();
            }
        }
        public MaterialModel Material
        {
            get => _material;
            set
            {
                _material = value;
                OnPropertyChanged();
            }
        }
        public List<MaterialModel> Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }

        public ConsumptionMaterialReportViewModel(IApiClient apiClient, IDialogService dialogService, IDocumentService documentService)
        {
            _client = apiClient;
            _documentService = documentService;
            _dialogService = dialogService;

            Materials = _client.Get<List<MaterialModel>>("material/all").Result;
            Material = Materials.First();
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
            var materials = _client.Get<List<MaterialReserveModel>>($"material/reserve/get/consumption/{Material.Id}").Result;

            _documentService.Font.Size = 12;
            _documentService.Font.Bold = true;
            _documentService.Font.Color = System.Drawing.Color.Black;
            _documentService.Font.Name = "Times New Roman";
            _documentService.Aligment = ParagraphAlignment.Center;

            _documentService.SkipLines(2);
            _documentService.WriteLine("ОТЧЁТ");
            _documentService.WriteLine($"о расходе {Material.Name}");


            _documentService.Aligment = ParagraphAlignment.Left;
            _documentService.SkipLines(2);
            _documentService.StartTable();
            _documentService.InsertCell("Номер заказа");
            _documentService.InsertCell("Название склада");
            _documentService.InsertLastCellInRow("Количество");

            _documentService.Font.Bold = false;

            foreach (var material in materials)
            {
                _documentService.InsertCell(material.MaterialOrderNumber);
                _documentService.InsertCell(material.StoragePlaceName);
                _documentService.InsertLastCellInRow(material.Count.ToString());
            }

            _documentService.EndTable();


            _documentService.Save(SavePath);
        }
    }
}

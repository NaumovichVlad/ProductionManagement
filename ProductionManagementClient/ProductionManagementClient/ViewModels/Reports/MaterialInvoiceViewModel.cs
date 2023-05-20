using Aspose.Words;
using ProductionManagementClient.Connection;
using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Reports
{
    public class MaterialInvoiceViewModel : ViewModelBase
    {
        private readonly IDocumentService _documentService;
        private readonly IDialogService _dialogService;
        private readonly IApiClient _client;
        private DateTime _compilationDate;
        private int _orderNumber;
        private string _savePath;
        private int _orderId;
        private List<StoragePlaceModel> _storagePlaces;
        private StoragePlaceModel _selectedStoragePlace;
        private MaterialOrderModel _materialOrderModel;
        private EmployeeModel _compilationEmployee;

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
        public StoragePlaceModel SelectedStoragePlace
        {
            get
            {
                return _selectedStoragePlace;
            }
            set
            {
                _selectedStoragePlace = value;
                OnPropertyChanged();
            }
        }
        public List<StoragePlaceModel> StoragePlaces
        {
            get 
            { 
                return _storagePlaces; 
            }
            set
            {
                _storagePlaces = value;
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
        public DateTime CompilationDate
        {
            get
            {
                return _compilationDate;
            }
            set
            {
                _compilationDate = value;
                OnPropertyChanged();
            }
        }
        public int OrderNumber
        {
            get
            {
                return _orderNumber;
            }
            set
            {
                _orderNumber = value;
                OnPropertyChanged();
            }
        }
        public MaterialOrderModel MaterialOrder
        {
            get => _materialOrderModel;
            set
            {
                _materialOrderModel = value;
                OnPropertyChanged();
            }
        }

        public MaterialInvoiceViewModel(int orderId, IDocumentService documentService, IDialogService dialogService, IApiClient client)
        {
            _documentService = documentService;
            _dialogService = dialogService;
            _client = client;
            _orderId = orderId;

            CompilationDate = DateTime.Now;
            StoragePlaces = _client.Get<List<StoragePlaceModel>>("storagePlace/all").Result;
            SelectedStoragePlace = StoragePlaces.First();
            CompilationEmployee = _client.Get<EmployeeModel>($"employee/get/{ApiConnection.User.EmployeeId}").Result;

            FillMaterialOrder(orderId);
        }
        
        private void FillMaterialOrder(int orderId)
        {
            MaterialOrder = _client.Get<MaterialOrderModel>($"material/order/get/{orderId}").Result;
            MaterialOrder.MaterialName = _client.Get<MaterialModel>($"material/get/{MaterialOrder.MaterialId}").Result.Name;
            MaterialOrder.CounteragentName = _client.Get<CounteragentModel>($"counteragent/get/{MaterialOrder.CounteragentId}").Result.Name;
        }

        private RelayCommand _createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand ??
                    (_createCommand = new RelayCommand(param =>
                    {
                        AddReserve();
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

        private void AddReserve()
        {
            var reserve = new MaterialReserveModel()
            {
                StoragePlaceId = SelectedStoragePlace.Id,
                Count = MaterialOrder.Count,
                MaterialOrderNumber = MaterialOrder.OrderNumber,
                MaterialOrderId = MaterialOrder.Id,
                StoragePlaceName = SelectedStoragePlace.Name
            };

            _client.Post(reserve, "material/reserve/insert");
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
            _documentService.InsertCell(OrderNumber.ToString());
            _documentService.InsertLastCellInRow(CompilationDate.ToShortDateString());
            _documentService.EndTable();

            _documentService.Font.Underline = Underline.Single;
            _documentService.Aligment = ParagraphAlignment.Center;
            _documentService.SkipLines(4);
            _documentService.WriteLine("УП \"Универсал Бобруйск\"");

            _documentService.SkipLines(4);

            _documentService.Aligment = ParagraphAlignment.Left;

            _documentService.Font.Underline = Underline.None;
            _documentService.Write("Грузоотправитель: ");
            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{MaterialOrder.CounteragentName}");

            _documentService.Font.Underline = Underline.None;
            _documentService.Font.Bold = true;
            _documentService.Write("Грузополучатель: ");
            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"УП \"Универсал Бобруйск\", {SelectedStoragePlace.Name}");

            _documentService.Font.Underline = Underline.None;
            _documentService.SkipLines(2);
            _documentService.StartTable();
            _documentService.InsertCell("Номер заказа");
            _documentService.InsertCell("Наименование материала");
            _documentService.InsertCell("Количество");
            _documentService.InsertLastCellInRow("Стоимость");
            _documentService.InsertCell(MaterialOrder.OrderNumber.ToString());
            _documentService.InsertCell(MaterialOrder.MaterialName);
            _documentService.InsertCell(MaterialOrder.Count.ToString());
            _documentService.InsertLastCellInRow(MaterialOrder.Price.ToString());
            _documentService.EndTable();

            _documentService.SkipLines(2);
            _documentService.Font.Bold = true;
            _documentService.Write("Выплативший сотрудник: ");
            _documentService.Font.Bold = false;
            _documentService.Font.Underline = Underline.Single;
            _documentService.WriteLine($"{CompilationEmployee.Surname} {CompilationEmployee.Name} {CompilationEmployee.MiddleName}");

            _documentService.SkipLines(4);
            _documentService.Font.Bold = true;
            _documentService.Font.Underline = Underline.None;
            _documentService.WriteLine("Руководитель организации");

            _documentService.Save(SavePath);
        }
    }
}

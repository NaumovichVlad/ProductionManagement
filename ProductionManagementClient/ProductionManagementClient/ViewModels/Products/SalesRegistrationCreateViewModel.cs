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

namespace ProductionManagementClient.ViewModels.Products
{
    public class SalesRegistrationCreateViewModel : CreateViewModel<SaleModel>
    {
        private List<CounteragentModel> _counteragents;
        private DataTable _materials;
        private List<MaterialPurchaseModel> _selectedMaterials;
        private CounteragentModel _selectedCounteragent;
        private int _count;
        private int _price;

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        public List<MaterialPurchaseModel> SelectedMaterials
        {
            get
            {
                return _selectedMaterials;
            }
            set
            {
                _selectedMaterials = value;
                OnPropertyChanged();
            }
        }
        public List<CounteragentModel> Counteragents
        {
            get
            {
                return _counteragents;
            }
            set
            {
                _counteragents = value;
                OnPropertyChanged();
            }
        }
        public CounteragentModel SelectedCounteragent
        {
            get => _selectedCounteragent;
            set
            {
                _selectedCounteragent = value;
                OnPropertyChanged();
            }
        }

        public DataTable Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }

        public CheckoutCreateViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Counteragents = _client.Get<List<CounteragentModel>>("counteragent/all").Result;
            Materials = CreateMaterialTable(_client.Get<List<MaterialModel>>("material/all").Result);
            SelectedMaterials = new List<MaterialPurchaseModel>();
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.CounteragentName = SelectedCounteragent.Name;
                        Model.CounteragentId = SelectedCounteragent.Id;

                        _client.Post(Model, "material/purchase/insert");

                        var purchase = _client.Get<PurchaseModel>($"material/purchase/getByNumber/{Model.OrderNumber}").Result;

                        foreach (var material in SelectedMaterials)
                        {
                            material.PurchaseNumber = Model.OrderNumber;
                            material.PurchaseId = purchase.Id;

                            _client.Post(material, "material/purchaseMaterial/insert");
                        }

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }

        private RelayCommand _isSelected;
        public RelayCommand IsSelected
        {
            get
            {
                return _isSelected ??
                    (_isSelected = new RelayCommand(obj =>
                    {
                        var row = (DataRowView)obj;

                        SelectedMaterials.Add(new MaterialPurchaseModel()
                        {
                            MaterialId = int.Parse(row["Ид"].ToString()),
                            MaterialName = row["Название"].ToString(),
                            Count = Count,
                            Price = Price,
                            IsAccepted = false

                        });
                        _messageBoxService.ShowMessage("Успех", "Материал добавлен в заказ");

                    }));
            }
        }

        private DataTable CreateMaterialTable(List<MaterialModel> models)
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
    }
}

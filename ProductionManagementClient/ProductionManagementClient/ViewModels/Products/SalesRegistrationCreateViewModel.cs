using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Products
{
    public class SalesRegistrationCreateViewModel : CreateViewModel<SaleModel>
    {
        private List<CounteragentModel> _counteragents;
        private DataTable _products;
        private List<AvailableProductModel> _selectedProducts;
        private CounteragentModel _selectedCounteragent;
        private int _count;

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }
        public List<AvailableProductModel> SelectedProducts
        {
            get
            {
                return _selectedProducts;
            }
            set
            {
                _selectedProducts = value;
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

        public DataTable Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public SalesRegistrationCreateViewModel(IApiClient client, IDialogService messageBoxService) : base(client, messageBoxService)
        {
            Counteragents = _client.Get<List<CounteragentModel>>("counteragent/all").Result;
            Products = CreateProductTable(_client.Get<List<AvailableProductModel>>("product/reserve/available/all").Result);
            SelectedProducts = new List<AvailableProductModel>();
            Model.OrderDate = DateTime.Now;
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

                        _client.Post(Model, "product/sale/insert");

                        var sale = _client.Get<SaleModel>($"product/sale/getByNumber/{Model.OrderNumber}").Result;

                        foreach (var product in SelectedProducts)
                        {

                            _client.Post($"product/finished/sale/insert/byProductName/{product.Name}/{product.Count}/{sale.Id}");
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

                        var product = new AvailableProductModel()
                        {
                            Name = row["Название"].ToString(),
                            Count = Count,
                        };

                        if (int.Parse(row["На складе"].ToString()) >= Count)
                        {
                            SelectedProducts.Add(product);

                            _messageBoxService.ShowMessage("Успех", "Материал добавлен в заказ");
                        }
                        else
                        {
                            _messageBoxService.ShowErrorMessage("Ошибка", "Недостаточное количество продукции на складе");
                        }
                    }));
            }
        }

        private DataTable CreateProductTable(List<AvailableProductModel> models)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("На складе");
            idColumn.Caption = "Id";

            var nameColumn = new DataColumn("Название");
            nameColumn.Caption = "Name";

            table.Columns.Add(nameColumn);
            table.Columns.Add(idColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[nameColumn] = model.Name;
                newRow[idColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }
    }
}

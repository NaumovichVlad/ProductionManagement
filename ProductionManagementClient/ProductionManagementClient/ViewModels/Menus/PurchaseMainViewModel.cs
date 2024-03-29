﻿using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.Views.Counteragents;
using ProductionManagementClient.Views.Materials;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class PurchaseMainViewModel : ViewModelBase
    {
        private readonly IApiClient _client;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        private DataTable _purchases;
        private DataTable _counteragents;
        private DataTable _materials;

        public DataTable Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }
        public DataTable Counteragents
        {
            get => _counteragents;
            set
            {
                _counteragents = value;
                OnPropertyChanged();
            }
        }

        public DataTable Purchases
        {
            get => _purchases;
            set
            {
                _purchases = value;
                OnPropertyChanged();
            }
        }

        public PurchaseMainViewModel(IApiClient client, IDialogService dialogService, IWindowService windowService)
        {
            _client = client;
            _dialogService = dialogService;
            _windowService = windowService;

            Update();
        }

        private void Update()
        {
            FillPurchasesTable();
            FillCounteragentsTable();
            FillMaterialsTable();
        }

        private RelayCommand _skipColumnsCommand;
        public RelayCommand SkipColumnsCommand
        {
            get
            {
                return _skipColumnsCommand ??
                    (_skipColumnsCommand = new RelayCommand(args =>
                    {
                        var column = (DataGridAutoGeneratingColumnEventArgs)args;
                        if (column.Column.Header == "Ид")
                        {
                            column.Cancel = true;
                        }
                    }
                    ));
            }
        }

        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(param =>
                    {
                        _windowService.ShowWindowDialog<CounteragentsCreateWin>();

                        Update();
                    }));
            }
        }

        private RelayCommand _editCommand;

        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new RelayCommand(param =>
                    {
                        if (param != null)
                        {
                            var row = (DataRowView)param;

                            _windowService.ShowWindowDialog<CounteragentsEditWin>(row["Ид"]);

                            Update();
                        }
                        else
                        {
                            _dialogService.ShowErrorMessage("Внимание", "Выберите элемент таблицы");
                        }
                    }));
            }
        }

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(param =>
                    {
                        if (param != null)
                        {
                            var row = (DataRowView)param;

                            _client.Delete($"counteragent/remove/{row["Ид"]}");

                            Update();
                        }
                        else
                        {
                            _dialogService.ShowErrorMessage("Внимание", "Выберите элемент таблицы");
                        }
                    }));
            }
        }

        private RelayCommand _checkoutCommand;

        public RelayCommand CheckoutCommand
        {
            get
            {
                return _checkoutCommand ??
                    (_checkoutCommand = new RelayCommand(param =>
                    {
                        _windowService.ShowWindowDialog<CheckoutWin>();

                        Update();
                    }));
            }
        }

        private RelayCommand _addMaterialCommand;

        public RelayCommand AddMaterialCommand
        {
            get
            {
                return _addMaterialCommand ??
                    (_addMaterialCommand = new RelayCommand(param =>
                    {
                        _windowService.ShowWindowDialog<MaterialsCreateWin>();

                        Update();
                    }));
            }
        }

        private void FillCounteragentsTable()
        {
            var counteragents = _client.Get<List<CounteragentModel>>("counteragent/all").Result;

            Counteragents = CreateCounteragentTable(counteragents);
        }

        private void FillPurchasesTable()
        {
            var purchases = _client.Get<List<PurchaseContainerModel>>("material/purchase/full").Result;
            Purchases = CreatePurchaseTable(purchases);
        }

        private void FillMaterialsTable()
        {
            var materials = _client.Get<List<AvailableMaterialModel>>("material/reserve/available/all").Result;
            Materials = CreateMaterialsTable(materials);
        }

        private DataTable CreateCounteragentTable(List<CounteragentModel> models)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Ид");
            idColumn.Caption = "Id";

            var nameColumn = new DataColumn("Название");
            nameColumn.Caption = "Name";

            var innColumn = new DataColumn("ИНН");
            innColumn.Caption = "INN";

            var accountNumberColumn = new DataColumn("Номер счёта");
            accountNumberColumn.Caption = "AccountNumber";

            var postalCodeColumn = new DataColumn("Индекс");
            postalCodeColumn.Caption = "PostalCode";

            var addressNameColumn = new DataColumn("Адрес");
            addressNameColumn.Caption = "Address";

            var registrationCountryColumn = new DataColumn("Страна регистрации");
            registrationCountryColumn.Caption = "RegistrationCountry";

            var phoneColumn = new DataColumn("Телефон");
            phoneColumn.Caption = "PhoneNumber";

            table.Columns.Add(idColumn);
            table.Columns.Add(nameColumn);
            table.Columns.Add(innColumn);
            table.Columns.Add(accountNumberColumn);
            table.Columns.Add(phoneColumn);
            table.Columns.Add(postalCodeColumn);
            table.Columns.Add(registrationCountryColumn);
            table.Columns.Add(addressNameColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[idColumn] = model.Id;
                newRow[nameColumn] = model.Name;
                newRow[innColumn] = model.Inn;
                newRow[accountNumberColumn] = model.AccountNumber;
                newRow[postalCodeColumn] = model.PostalCode;
                newRow[addressNameColumn] = model.Address;
                newRow[phoneColumn] = model.PhoneNumber;
                newRow[registrationCountryColumn] = model.RegistrationCountry;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreatePurchaseTable(List<PurchaseContainerModel> models)
        {
            var table = new DataTable();

            var numberColumn = new DataColumn("Номер заказа");
            var counteragentColumn = new DataColumn("Контерагент");
            var dateColumn = new DataColumn("Дата изготовления");
            var countryColumn = new DataColumn("Страна производства");
            var materialsColumn = new DataColumn("Материалы");

            table.Columns.Add(numberColumn);
            table.Columns.Add(counteragentColumn);
            table.Columns.Add(dateColumn);
            table.Columns.Add(countryColumn);
            table.Columns.Add(materialsColumn);


            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[numberColumn] = model.OrderNumber;
                newRow[counteragentColumn] = model.CounteragentName;
                newRow[dateColumn] = model.ManufactureDate.ToShortDateString();
                newRow[countryColumn] = model.ManufactureCountry;
                newRow[materialsColumn] = model.Materials;

                table.Rows.Add(newRow);
            }

            return table;
        }

        private DataTable CreateMaterialsTable(List<AvailableMaterialModel> models)
        {
            var table = new DataTable();

            var nameColumn = new DataColumn("Материал");
            var countColumn = new DataColumn("Количество");

            table.Columns.Add(nameColumn);
            table.Columns.Add(countColumn);

            foreach (var model in models)
            {
                var newRow = table.NewRow();

                newRow[nameColumn] = model.Name;
                newRow[countColumn] = model.Count;

                table.Rows.Add(newRow);
            }

            return table;
        }
    }
}

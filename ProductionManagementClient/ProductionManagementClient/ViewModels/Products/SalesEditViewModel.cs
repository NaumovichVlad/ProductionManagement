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

namespace ProductionManagementClient.ViewModels.Products
{
    public class SalesEditViewModel : EditViewModel<SaleModel>
    {
        private List<CounteragentModel> _counteragents;
        private CounteragentModel _selectedCounteragent;

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
        public SalesEditViewModel(string id, IApiClient client, IDialogService messageBoxService)
            : base(id, client, messageBoxService)
        {
            Counteragents = _client.Get<List<CounteragentModel>>("counteragent/all").Result;
            SelectedCounteragent = Counteragents.Where(c => c.Id == Model.CounteragentId).First();
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

                            _client.Put(Model, $"product/sale/edit");
                            _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                            ((Window)obj).Close();

                        }));
            }
        }

        public override SaleModel GetModel(string id)
        {
            return _client.Get<SaleModel>($"product/sale/get/{id}").Result;
        }
    }
}

using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Materials
{
    public class PurchaseEditViewModel : EditViewModel<PurchaseModel>
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
        public PurchaseEditViewModel(string id, IApiClient client, IDialogService messageBoxService)
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

                            _client.Put(Model, $"material/purchase/edit");
                            _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                            ((Window)obj).Close();

                        }));
            }
        }

        public override PurchaseModel GetModel(string id)
        {
            return _client.Get<PurchaseModel>($"material/purchase/get/{id}").Result;
        }
    }
}

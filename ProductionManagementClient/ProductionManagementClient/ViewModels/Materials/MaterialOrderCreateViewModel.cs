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

namespace ProductionManagementClient.ViewModels.Materials
{
    public class MaterialOrderCreateViewModel : CreateViewModel<MaterialOrderModel>
    {
        private List<CounteragentModel> _counteragents;
        private List<MaterialModel> _materials;
        private MaterialModel _selectedMaterial;
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
        public List<MaterialModel> Materials
        {
            get
            {
                return _materials;
            }
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }
        public MaterialModel SelectedMaterial 
        { 
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
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

        public MaterialOrderCreateViewModel(IApiClient client, IMessageBoxService messageBoxService)
            : base(client, messageBoxService)
        {
            Counteragents = _client.Get<List<CounteragentModel>>("counteragent/all").Result;
            Materials = _client.Get<List<MaterialModel>>("material/all").Result;
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.MaterialName = SelectedMaterial.Name;
                        Model.CounteragentName = SelectedMaterial.Name;
                        Model.MaterialId = SelectedMaterial.Id;
                        Model.CounteragentId = SelectedMaterial.Id;

                        _client.Post(Model, "material/order/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}

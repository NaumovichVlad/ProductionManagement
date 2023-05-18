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
    public class MaterialOrderEditViewModel : EditViewModel<MaterialOrderModel>
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
        public MaterialOrderEditViewModel(string id, IApiClient client, IDialogService messageBoxService) 
            : base(id, client, messageBoxService)
        {
            Counteragents = _client.Get<List<CounteragentModel>>("counteragent/all").Result;
            Materials = _client.Get<List<MaterialModel>>("material/all").Result;

            SelectedMaterial = Materials.Where(m => m.Id == Model.MaterialId).First();
            SelectedCounteragent = Counteragents.Where(c => c.Id == Model.CounteragentId).First();
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

                            _client.Put(Model, $"material/order/edit");
                            _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                            ((Window)obj).Close();

                        }));
            }
        }

        public override MaterialOrderModel GetModel(string id)
        {
            return _client.Get<MaterialOrderModel>($"material/order/get/{id}").Result;
        }
    }
}

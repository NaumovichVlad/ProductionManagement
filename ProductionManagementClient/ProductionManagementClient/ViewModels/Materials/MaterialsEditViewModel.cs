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
    public class MaterialsEditViewModel : EditViewModel<MaterialModel>
    {
        public MaterialsEditViewModel(string id, IApiClient client, IDialogService messageBoxService)
            : base(id, client, messageBoxService)
        { }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Put(Model, $"material/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override MaterialModel GetModel(string id)
        {
            return _client.Get<MaterialModel>($"material/get/{id}").Result;
        }
    }
}

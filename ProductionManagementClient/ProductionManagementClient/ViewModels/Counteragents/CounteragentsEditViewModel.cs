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

namespace ProductionManagementClient.ViewModels.Counteragents
{
    public class CounteragentsEditViewModel : EditViewModel<CounteragentModel>
    {
        public CounteragentsEditViewModel(string id, IApiClient client, IMessageBoxService messageBoxService) 
            : base(id, client, messageBoxService)
        { }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Put(Model, $"counteragent/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override CounteragentModel GetModel(string id)
        {
            return _client.Get<CounteragentModel>($"counteragent/get/{id}").Result;
        }
    }
}

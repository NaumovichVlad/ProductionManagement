using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProductionManagementClient.Interfaces.Services;

namespace ProductionManagementClient.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string header, string message)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK);
        }
    }
}

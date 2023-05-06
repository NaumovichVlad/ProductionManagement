using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.Services
{
    public class WindowService : IWindowService
    {
        public void ShowWindow<T>()
            where T : Window, new()
        {
            var window = new T();

            window.Show();
        }
    }
}

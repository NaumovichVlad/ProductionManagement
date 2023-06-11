using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
using System.Windows;

namespace ProductionManagementClient.Views.Menus
{
    /// <summary>
    /// Логика взаимодействия для LogisticMainWin.xaml
    /// </summary>
    public partial class LogisticMainWin : Window
    {
        public LogisticMainWin()
        {
            InitializeComponent();

            DataContext = new LogisticMainViewModel(new HttpApiClient(), new WindowService());
        }
    }
}

using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Counteragents;
using System.Windows;

namespace ProductionManagementClient.Views.Counteragents
{
    /// <summary>
    /// Логика взаимодействия для CounteragentsCreateWin.xaml
    /// </summary>
    public partial class CounteragentsCreateWin : Window
    {
        public CounteragentsCreateWin()
        {
            InitializeComponent();

            DataContext = new CounteragentsCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}

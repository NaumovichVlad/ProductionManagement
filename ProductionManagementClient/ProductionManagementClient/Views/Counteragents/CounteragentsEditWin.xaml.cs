using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Counteragents;
using System.Windows;

namespace ProductionManagementClient.Views.Counteragents
{
    /// <summary>
    /// Логика взаимодействия для CounteragentsEditWin.xaml
    /// </summary>
    public partial class CounteragentsEditWin : Window
    {
        public CounteragentsEditWin(object id)
        {
            InitializeComponent();

            DataContext = new CounteragentsEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}

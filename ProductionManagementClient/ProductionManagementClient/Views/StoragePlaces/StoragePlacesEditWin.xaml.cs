using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.StoragePlaces;
using System.Windows;

namespace ProductionManagementClient.Views.StoragePlaces
{
    /// <summary>
    /// Логика взаимодействия для StoragePlacesEditWin.xaml
    /// </summary>
    public partial class StoragePlacesEditWin : Window
    {
        public StoragePlacesEditWin(object id)
        {
            InitializeComponent();

            DataContext = new StoragePlacesEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}

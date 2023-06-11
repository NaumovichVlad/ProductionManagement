using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.StoragePlaces;
using System.Windows;

namespace ProductionManagementClient.Views.StoragePlaces
{
    /// <summary>
    /// Логика взаимодействия для StoragePlacesCreateWin.xaml
    /// </summary>
    public partial class StoragePlacesCreateWin : Window
    {
        public StoragePlacesCreateWin()
        {
            InitializeComponent();

            DataContext = new StoragePlacesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}

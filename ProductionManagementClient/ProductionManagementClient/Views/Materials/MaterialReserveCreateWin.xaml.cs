using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialReserveCreateWin.xaml
    /// </summary>
    public partial class MaterialReserveCreateWin : Window
    {
        public MaterialReserveCreateWin()
        {
            InitializeComponent();

            DataContext = new MaterialReserveCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}

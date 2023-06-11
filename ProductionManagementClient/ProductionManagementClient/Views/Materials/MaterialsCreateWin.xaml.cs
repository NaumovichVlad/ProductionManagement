using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsCreateWin.xaml
    /// </summary>
    public partial class MaterialsCreateWin : Window
    {
        public MaterialsCreateWin()
        {
            InitializeComponent();

            DataContext = new MaterialsCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}

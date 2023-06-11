using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsEditWin.xaml
    /// </summary>
    public partial class MaterialsEditWin : Window
    {
        public MaterialsEditWin(object id)
        {
            InitializeComponent();

            DataContext = new MaterialsEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}

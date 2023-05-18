using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.StoragePlaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

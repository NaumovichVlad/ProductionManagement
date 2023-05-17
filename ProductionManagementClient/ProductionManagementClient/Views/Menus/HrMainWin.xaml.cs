using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
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

namespace ProductionManagementClient.Views.Menus
{
    /// <summary>
    /// Логика взаимодействия для HrMainWin.xaml
    /// </summary>
    public partial class HrMainWin : Window
    {
        public HrMainWin()
        {
            InitializeComponent();

            DataContext = new HrMainViewModel(new HttpApiClient(), new WindowService());
        }
    }
}

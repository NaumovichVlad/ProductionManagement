using ProductionManagementClient.Connection;
using ProductionManagementClient.ViewModels;
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
    /// Логика взаимодействия для AdminMainWin.xaml
    /// </summary>
    public partial class AdminMainWin : Window
    {
        public AdminMainWin()
        {
            InitializeComponent();
            DataContext = new AdminMainViewModel(new HttpApiClient());
        }
    }
}

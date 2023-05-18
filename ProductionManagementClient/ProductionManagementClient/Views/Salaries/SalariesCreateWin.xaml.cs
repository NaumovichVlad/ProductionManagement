using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Salaries;
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

namespace ProductionManagementClient.Views.Salaries
{
    /// <summary>
    /// Логика взаимодействия для SalariesCreateWin.xaml
    /// </summary>
    public partial class SalariesCreateWin : Window
    {
        public SalariesCreateWin()
        {
            InitializeComponent();

            DataContext = new SalariesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}

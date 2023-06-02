﻿using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
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

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для FinishedProductsSalesCreateWin.xaml
    /// </summary>
    public partial class FinishedProductsSalesCreateWin : Window
    {
        public FinishedProductsSalesCreateWin()
        {
            InitializeComponent();

            DataContext = new FinishedProductsSalesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
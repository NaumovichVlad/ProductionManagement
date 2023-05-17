﻿using ProductionManagementClient.Connection;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels;
using ProductionManagementClient.ViewModels.Employees;
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

namespace ProductionManagementClient.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesCreateWin.xaml
    /// </summary>
    public partial class EmployeesEditWin : Window
    {
        public EmployeesEditWin(object id)
        {
            InitializeComponent();

            DataContext = new EmployeesEditViewModel(id.ToString(), new HttpApiClient(), new MessageBoxService());
        }
    }
}
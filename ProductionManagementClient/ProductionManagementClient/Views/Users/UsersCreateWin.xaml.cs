﻿using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Users;
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

namespace ProductionManagementClient.Views.Users
{
    /// <summary>
    /// Логика взаимодействия для UsersCreateWin.xaml
    /// </summary>
    public partial class UsersCreateWin : Window
    {
        public UsersCreateWin()
        {
            InitializeComponent();

            DataContext = new UsersCreateViewModel(new HttpApiClient(), new MessageBoxService());
        }
    }
}

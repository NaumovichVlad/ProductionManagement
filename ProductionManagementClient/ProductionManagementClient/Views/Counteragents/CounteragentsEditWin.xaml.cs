﻿using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Counteragents;
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

namespace ProductionManagementClient.Views.Counteragents
{
    /// <summary>
    /// Логика взаимодействия для CounteragentsEditWin.xaml
    /// </summary>
    public partial class CounteragentsEditWin : Window
    {
        public CounteragentsEditWin(object id)
        {
            InitializeComponent();

            DataContext = new CounteragentsEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}

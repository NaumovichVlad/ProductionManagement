﻿using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
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

            DataContext = new MaterialsEditViewModel(id.ToString(), new HttpApiClient(), new MessageBoxService());
        }
    }
}

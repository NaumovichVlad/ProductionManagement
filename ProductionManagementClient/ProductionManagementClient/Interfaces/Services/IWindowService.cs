﻿using System.Windows;

namespace ProductionManagementClient.Interfaces.Services
{
    public interface IWindowService
    {
        void ShowWindow<T>() where T : Window, new();
    }
}
﻿using System.Windows;

namespace ProductionManagementClient.Interfaces.Services
{
    public interface IWindowService
    {
        void ShowWindow<T>() where T : Window, new();
        void ShowWindow<T>(object param) where T : Window;
        void ShowWindowDialog<T>() where T : Window, new();
        void ShowWindowDialog<T>(object param) where T : Window;
    }
}
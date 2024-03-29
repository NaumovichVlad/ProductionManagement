﻿using ProductionManagementClient.Interfaces.Services;
using System;
using System.Windows;

namespace ProductionManagementClient.Services
{
    public class WindowService : IWindowService
    {
        public void ShowWindow<T>()
            where T : Window, new()
        {
            var window = new T();

            window.Show();
        }

        public void ShowWindow<T>(object param)
            where T : Window
        {

            Type windowType = typeof(T);
            var windowConstructor = windowType.GetConstructor(new Type[] { param.GetType() });
            T window = (T)windowConstructor.Invoke(new object[] { param });

            window.Show();
        }

        public void ShowWindowDialog<T>()
            where T : Window, new()
        {
            var window = new T();

            window.ShowDialog();
        }

        public void ShowWindowDialog<T>(object param)
            where T : Window
        {

            Type windowType = typeof(T);
            var windowConstructor = windowType.GetConstructor(new Type[] { param.GetType() });
            T window = (T)windowConstructor.Invoke(new object[] { param });

            window.ShowDialog();
        }
    }
}

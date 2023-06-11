using Microsoft.Win32;
using ProductionManagementClient.Interfaces.Services;
using System.Windows;

namespace ProductionManagementClient.Services
{
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog(string format)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = format;

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }

            return false;
        }

        public bool SaveFileDialog(string format)
        {
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = format;

            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }

            return false;
        }
        public void ShowMessage(string header, string message)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void ShowErrorMessage(string header, string message)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

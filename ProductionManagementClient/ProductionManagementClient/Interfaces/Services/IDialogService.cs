using Microsoft.Win32;
using System.DirectoryServices.ActiveDirectory;

namespace ProductionManagementClient.Interfaces.Services
{
    public interface IDialogService
    {
        string FilePath { get; set; }

        bool OpenFileDialog(string format);

        public bool SaveFileDialog(string format);

        void ShowMessage(string header, string message);
        void ShowErrorMessage(string header, string message);
    }
}
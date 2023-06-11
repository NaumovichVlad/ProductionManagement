using ProductionManagementClient.Models;
using System.Configuration;

namespace ProductionManagementClient.Connection
{
    public static class ApiConnection
    {
        public static string Token { get; set; }
        public static UserModel User { get; set; }
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
        public static void SetConnectionString(string uri)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.ConnectionStrings.ConnectionStrings.Remove("Default");
            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("Default", uri));
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}

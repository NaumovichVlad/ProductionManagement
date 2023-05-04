using ProductionManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Connection
{
    public static class ApiConnection
    {
        public static string Token {  get; set; }
        public static UserModel User { get; set; }
        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
    }
}

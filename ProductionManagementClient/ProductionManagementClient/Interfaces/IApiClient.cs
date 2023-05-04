using ProductionManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Interfaces
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> Post<T>(T model, string uri);
        Task<T> Get<T>(string uri);
    }
}

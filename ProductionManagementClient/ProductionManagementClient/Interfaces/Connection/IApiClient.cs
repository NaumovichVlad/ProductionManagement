using ProductionManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Interfaces.Connection
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> Post<T>(T model, string uri);
        Task<HttpResponseMessage> Post(string uri);
        Task<T> Get<T>(string uri);
        Task<HttpResponseMessage> Put<T>(T model, string uri);
        Task<HttpResponseMessage> Delete(string uri);
    }
}

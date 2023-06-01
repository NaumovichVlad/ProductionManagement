using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Connection
{
    public class HttpApiClient : IApiClient
    {
        private readonly string _apiUri;

        public HttpApiClient()
        {
            _apiUri = ApiConnection.GetConnectionString();
        }
        
        public Task<HttpResponseMessage> Post<T>(T model, string uri)
        {
            uri = _apiUri + uri;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiConnection.Token);
                var request = client.PostAsJsonAsync(uri, model);

                try
                {
                    request.Wait();
                }
                catch (Exception ex)
                {
                    throw;
                }

                return request;
            }
        }

        public Task<T> Get<T>(string uri)
        {
            uri = _apiUri + uri;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiConnection.Token);

                var request = client.GetFromJsonAsync<T>(uri);

                request.Wait();

                return request;
            }
        }

        public Task<HttpResponseMessage> Put<T>(T model, string uri)
        {
            uri = _apiUri + uri;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiConnection.Token);
                var request = client.PutAsJsonAsync(uri, model);

                request.Wait();

                return request;
            }
        }

        public Task<HttpResponseMessage> Delete(string uri)
        {
            uri = _apiUri + uri;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiConnection.Token);
                var request = client.DeleteAsync(uri);

                request.Wait();

                return request;
            }
        }
    }
}

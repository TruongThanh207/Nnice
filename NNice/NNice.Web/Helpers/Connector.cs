using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NNice.Web.Models;

namespace NNice.Web.Helpers
{
    public class Connector
    {
       private static HttpClient _client;
       public Connector(HttpClient client)
        {
            _client = client;
        }
        public async Task<Uri> CreateAsync<T>(T model, string path) where T : class
        {
            var response = await _client.PostAsJsonAsync(
                path, model);

            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<ResponseObject<T>> GetAsync<T>(string path) where T : class
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            if (!response.IsSuccessStatusCode)
            {
                return new ResponseObject<T>();
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseObject<T>>(result);
        }

        public async Task<T> UpdateAsync<T>(T model, string path) where T : class
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(path, model);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            var result = await response.Content.ReadAsAsync<T>();
            return result;
        }

        public async Task<HttpStatusCode> DeleteAsync(string path)
        {
            var response = await _client.DeleteAsync(path);
            return response.StatusCode;
        }
        public async Task<ResponseObject<AccountViewModel>> Authencate(AccountViewModel account)
        {
            var response = await _client.PostAsJsonAsync(
                "api/Accounts/authenticate", account);

            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseObject<AccountViewModel>>(result);

        }
    }
}

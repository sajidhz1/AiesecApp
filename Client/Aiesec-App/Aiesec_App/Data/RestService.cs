using Aiesec_App.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aiesec_App.Data
{
    class RestService<T> : IRestService<T>
    {
        RestClient client;
        string authHeaderValue;

        public List<T> Items { get; private set; }

        public RestService()
        {
            //var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            //authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new RestClient(Constants.RestUrl);       
        }

        public async Task<List<T>> RefreshDataAsync(string apiUrl, bool authorized = true)
        {
            Items = new List<T>();

            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var request = new RestRequest(apiUrl, Method.GET);
            //request.AddParameter("Authorization", string.Format("Basic " + authHeaderValue), ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader);

            if (authorized)
            {
                request.AddParameter("Authorization", "JWT " + Application.Current.Properties["token"], ParameterType.HttpHeader);
            }           
            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Items = JsonConvert.DeserializeObject<List<T>>(response.Content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        } 

        public async Task<List<T>> GetLatestAsync(string url)
        {
            Items = new List<T>();

            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var request = new RestRequest(url, Method.GET);
            request.AddParameter("Table", nameof(T));

            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Items = JsonConvert.DeserializeObject<List<T>>(response.Content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        Task<bool> IRestService<T>.SaveItemAsync(string url, T item, bool isNewItem)
        {
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Authorization", "JWT " + Application.Current.Properties["token"]);
            try
            {
                var json = JsonConvert.SerializeObject(item);
       
                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return Task.FromResult(true);
                    // Items = JsonConvert.DeserializeObject<List<T>>(response.Content);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return Task.FromResult(false);
        }

        Task<bool> IRestService<T>.SaveItemsAsync(List<T> items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(string url, string id, T item)
        {
            var request = new RestRequest(url+"/"+id, Method.PUT);
            request.AddHeader("Authorization", "JWT " + Application.Current.Properties["token"]);
            try
            {
                var json = JsonConvert.SerializeObject(item);

                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return Task.FromResult(false);
        }

        Task<bool> IRestService<T>.DeleteItemAsync(string url, string id)
        {
            var request = new RestRequest(url + "/" + id, Method.DELETE);
            request.AddHeader("Authorization", "JWT " + Application.Current.Properties["token"]);
            try
            {
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return Task.FromResult(false);
        }
    }
}

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

namespace Aiesec_App.Data
{
    class RestService<T> : IRestService<T>
    {
        RestClient client;
        string authHeaderValue;

        public List<T> Items { get; private set; }

        public RestService()
        {
            var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new RestClient(Constants.RestUrl);
       
        }

        public async Task<List<T>> RefreshDataAsync(string apiUrl)
        {
            Items = new List<T>();

            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var request = new RestRequest(apiUrl, Method.GET);
            request.AddParameter("Authorization", string.Format("Basic " + authHeaderValue), ParameterType.HttpHeader);

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

        public async Task SaveItemAsync(T item, bool isNewItem)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
          //  var uri = new Uri(string.Format(Constants.RestUrl, item.ID));

            //try
            //{
            //    var json = JsonConvert.SerializeObject(item);
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");

            //    HttpResponseMessage response = null;
            //    if (isNewItem)
            //    {
            //        response = await client.PostAsync(uri, content);
            //    }
            //    else
            //    {
            //        response = await client.PutAsync(uri, content);
            //    }

            //    if (response.IsSuccessStatusCode)
            //    {
            //        Debug.WriteLine(@"				TodoItem successfully saved.");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(@"				ERROR {0}", ex.Message);
            //}
        }

        public async Task SaveItemsAsync(List<T> items)
        {


        }

        public async Task DeleteItemAsync(string id)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, id));

            //try
            //{
            //    var response = await client.DeleteAsync(uri);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        Debug.WriteLine(@"				TodoItem successfully deleted.");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(@"				ERROR {0}", ex.Message);
            //}
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

        Task<bool> IRestService<T>.SaveItemAsync(T item, bool isNewItem)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRestService<T>.SaveItemsAsync(List<T> items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRestService<T>.DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}

using Newtonsoft.Json;
using SaleManager.Wpf.Inflastructor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor
{
    public sealed class RestApiUtils
    {
        private static readonly Lazy<RestApiUtils> lazy = new Lazy<RestApiUtils>(() => new RestApiUtils());

        public static RestApiUtils Instance { get { return lazy.Value; } }

        private RestApiUtils()
        {
            client.BaseAddress = new Uri("https://localhost:44312/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpClient client = new HttpClient();
        private TokenModel token = new TokenModel();

        public bool IsLogged()
        {
            return !string.IsNullOrEmpty(token.Token);
        }

        public async Task<bool> Login(string username, string password)
        {
            var content = new Dictionary<string, string>{
              { "Username", username },
              { "Password", password },
            };
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/user/login", stringContent);
            if (!response.IsSuccessStatusCode)
                return false;
            response.EnsureSuccessStatusCode();
            var jsonStr = await response.Content.ReadAsStringAsync();
            token = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(jsonStr);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            return IsLogged();
        }

        public void Logout()
        {
            token = null;
        }

        public async Task<T> Post<T>(string url, Dictionary<string, object> content)
        {
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            //response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
            return data;
        }
        public async Task<bool> Post(string url, Dictionary<string, object> content)
        {
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        public async Task<T> Get<T>(string url)
        {
            //string json = JsonConvert.SerializeObject(content, Formatting.Indented);
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
            return data; 
        }
    }
}

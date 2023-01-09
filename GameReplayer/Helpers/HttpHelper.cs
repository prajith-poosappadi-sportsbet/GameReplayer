using System;
using System.Net.Http;
using Infrastructure.Http.Client;
using Newtonsoft.Json;
using Nfl.UI.Contracts;

namespace GameReplayer.Helpers
{
    public static class HttpHelper
    {
        private static string GetCookie()
        {
            return GameHelper.Cookie;
        }
        
        public static string GetJson(string url, Uri baseAddress)
        {
            var client = MakeClient(baseAddress, GetCookie());
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }

        public static CommandPublished PostJson<T>(string url, Uri baseAddress, T value)
        {
            var client = MakeClient(baseAddress, GetCookie());
            var response = client.PostAsJsonAsync(url, value).Result;
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CommandPublished>(json);
        }

        public static CommandPublished PutJson<T>(string url, Uri baseAddress, T value)
        {
            var client = MakeClient(baseAddress, GetCookie());
            var response = client.PutAsJsonAsync(url, value).Result;
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CommandPublished>(json);
        }

        public static Guid PostAndReturnCorrelationId(string url, Uri baseAddress)
        {
            var response = Post(url, baseAddress);
            var json = response.Content.ReadAsStringAsync().Result;
            var cmdPublished = JsonConvert.DeserializeObject<CommandPublished>(json);
            return cmdPublished.CorrelationId;
        }

        private static HttpResponseMessage Post(string url, Uri baseAddress)
        {
            var client = MakeClient(baseAddress, GetCookie());
            var response = client.PostAsync(url, null).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

        private static HttpClientWrapperJson MakeClient(Uri baseAddress, string cookie)
        {
            var httpClient = new HttpClient { BaseAddress = baseAddress };
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
            httpClient.DefaultRequestHeaders.Add("access-control-allow-origin", "*");
            httpClient.DefaultRequestHeaders.Add("cookie", cookie);
            var client = new HttpClientWrapperJson(httpClient);
            return client;
        }
    }
}
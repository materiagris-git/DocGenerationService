using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Util
{
    public class PrintGenericService<T, U> where T : class
    {
        public static T Get(string baseUrl, string url)
        {
            T result = null;
            try
            {
                UriBuilder builder = new UriBuilder(baseUrl + url);
                //ELog.save("Servicio Get (URL)", baseUrl + url);
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    //LogHelper.Exception(string.Format("Get - {0} : ", 0), LogHelper.Paso.CreateDirectory, "Get toy aki");

                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = httpClient.GetAsync(builder.Uri).Result;

                    result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                //ELog.save("Get", ex.ToString());
            }

            return result;
        }

        public static T GetWithParameters(string baseUrl, string url, Dictionary<string, string> parameters)
        {
            T result = null;

            try
            {
                UriBuilder builder = new UriBuilder(baseUrl + url);
                string query = "";

                foreach (var item in parameters)
                {
                    query += string.Format("{0}={1}", item.Key, item.Value);
                }
                builder.Query = query;

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();

                    var response = httpClient.GetAsync(builder.Uri).Result;

                    result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

                }
            }
            catch (Exception ex)
            {
                //ELog.saveError("GetWithParameters", ex.ToString());
            }

            return result;
        }

        public static T PostRequest(string baseUrl, string url, U postObject)
        {
            T result = null;
            try
            {
                UriBuilder builder = new UriBuilder(baseUrl + url);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(60);
                    var response = client.PostAsync(builder.Uri, postObject, new JsonMediaTypeFormatter()).Result;

                    result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                //ELog.saveError("PostRequest", ex.ToString());
            }

            return result;
        }
    }
}

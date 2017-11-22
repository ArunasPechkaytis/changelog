using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;

using changelog.dal;

namespace jsonParse
{
    /// <summary>
    /// Класс загрузки HTML разметки (json структура внутри страницы)
    /// </summary>
    static class cs_webrequest
    {
        /// <summary>
        /// Загрузка Web страницы с автоматической авторизацией
        /// </summary>
        /// <param name="ProjectLink">Ссылка на проект данных</param>
        /// <param name="PullMax">Последний Pull обновлений</param>
        static public string getJsonFromWebPage(string ProjectLink, int PullMax)
        {
            // Если нужно подключить прокси сервер
            //HttpClientHandler handler = new HttpClientHandler()
            //{
            //    Proxy = new WebProxy("http://127.0.0.1:8888"),
            //    UseProxy = true,
            //};

            // ... Use HttpClient.            
            //HttpClient client = new HttpClient(handler);

            HttpClient client = new HttpClient();
            string setLoginPassword = cs_authentication.getLoginWeb() + ":" + cs_authentication.getPasswordWeb();
            var byteArray = Encoding.ASCII.GetBytes(setLoginPassword);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = client.GetAsync(string.Format(ProjectLink, PullMax)).Result;

            // ... Check Status Code                                
            Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            string result = "";

            if ((int)response.StatusCode != 404)
            {
                result = ReadContentAsString(response.Content);
            }

            return result;
        }

        private static string ReadContentAsString(HttpContent content)
        {
            return content == null ? null : content.ReadAsStringAsync().Result;
        }

    }
}

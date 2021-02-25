using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Server.ApiClient
{
    public static class Api
    {
        public static HttpClient Client { get; set; }

        public static void Init()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:5001/QuestionAnswers/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

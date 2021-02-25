using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Server.ApiClient
{
    class Processor
    {
        public async Task<string> GetRandomQuestion()
        {
            string url = "/QuestionAnswers/Random";
            try
            {
                using (HttpResponseMessage response = await Api.Client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string qas = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(qas);
                        return qas;
                    }
                    else
                    {
                        Console.WriteLine(response.ReasonPhrase);
                    }

                }
            }
            catch
            {
                Console.WriteLine("Host API (Get) unreachable");
            }
            return null;
        }


        public async Task<bool> LoginToAPI()
        {

            HttpContent HttpC = new StringContent(ConfigReader.LocalConfiguration.ToString(), Encoding.UTF8, "application/json");

            //Console.WriteLine(HttpC.ReadAsStringAsync().Result);

            string url = "/Login";
            try
            {
                using (HttpResponseMessage response = await Api.Client.PostAsync(url, HttpC))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Host API unreachable");
            }
            return false;
        }
    }
}

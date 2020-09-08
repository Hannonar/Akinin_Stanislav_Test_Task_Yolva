using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Akinin_Stanislav_Test_Task_Yolva.Logic
{
    public static class APICaller
    {
        static APICaller()
        {
            InitializeClient();
        }

        public static HttpClient ApiClient { get; set; }

        private static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

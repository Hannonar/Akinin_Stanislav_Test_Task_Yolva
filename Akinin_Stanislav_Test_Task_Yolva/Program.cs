using Akinin_Stanislav_Test_Task_Yolva.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Akinin_Stanislav_Test_Task_Yolva.Models;
using System.IO;

namespace Akinin_Stanislav_Test_Task_Yolva
{
    class Program
    {
        static void Main(string[] args)
        {
            string address, fileName;
            int pointFrequency;

            // TODO: Добавить валидацию вводимых значений

            Console.WriteLine("Введите адрес для поиска полигона");
            address = Console.ReadLine();

            Console.WriteLine("Введите частоту точек");
            pointFrequency = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите имя файла");
            fileName = Console.ReadLine();

            ProcessResult(address, fileName, pointFrequency);
            Console.ReadKey();
        }

        static async void ProcessResult(string address,string fileName,int pointFrequency) 
        {
            try
            {
                var msg = GetMessageForOSM(address);
                var responseMessage = await APICaller.ApiClient.SendAsync(msg);

                Console.WriteLine("Ответ API получен");

                var listOfRegions = await responseMessage.Content.ReadAsAsync<IList<OSMRegionExternal>>();

                Console.WriteLine("Ответ десериализован");

                var Regions = listOfRegions.Select(r => RegionConverter.ConvertOSMToInternal(r)).ToList();

                foreach (var reg in Regions)
                    reg.SimplifyAllPolygons(pointFrequency);

                Console.WriteLine("Полигоны упрощены");

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    var serializedResult = JsonConvert.SerializeObject(Regions);
                    sw.Write(serializedResult);
                }

                Console.WriteLine("Результат записан");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static HttpRequestMessage GetMessageForOSM(string address) 
        {
            string baseUri = "https://nominatim.openstreetmap.org/";
            string parameters = $"search?q={address}&format=json&polygon_geojson=1";
            var result = new HttpRequestMessage(HttpMethod.Get, baseUri + parameters);
            result.Headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

            return result;
        }
    }
}

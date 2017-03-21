using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CYInfo.CMKUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }


        [TestMethod]
        
        public static void PostBasicDataList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4767/");//正式环境
                client.Timeout = TimeSpan.FromMinutes(30);
                client.DefaultRequestHeaders.Add("X-ApiKey", "MyRandomApiKeyValue");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var authByteArray = Encoding.ASCII.GetBytes("AuthnticatedApiUser:PasswordForApi");
                var authString = Convert.ToBase64String(authByteArray);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                var content = new StringContent("{'CMD':'TSA001','PARAMS':{'A':1}}", Encoding.UTF8, "application/json");

                var message = client.PostAsync("api/GetShoes", content).Result.Content.ReadAsStringAsync().Result;

                string ttt = message;
            }
        }

    }
}

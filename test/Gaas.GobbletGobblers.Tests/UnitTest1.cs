using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Newtonsoft.Json;
using NUnit.Framework;
using Wsa.Gaas.GobbletGobblers.WebApi;
using Wsa.Gaas.GobbletGobblers.WebApi.Controllers;

namespace TestProject4
{
    public class Tests
    {
        private HttpServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            //config.Services.Replace(typeof(IHttpControllerActivator), new MyControllerActivator());

            //建立 HttpServer 物件
            _server = new HttpServer(config);

            _client = new HttpClient(_server);
        }

        [Test]
        public async Task Test1Async()
        {
            var response = await _client.GetAsync("http://localhost/api/game/helloworld");

            var content = response.Content.ReadAsStringAsync().Result;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content, Is.EqualTo("Hello .Net Farework"));
        }

        [Test]
        public async Task Test2Async()
        {
            var response = await _client.GetAsync("https://localhost/api/Game/GetHelloWorldName?name=test");

            var content = response.Content.ReadAsStringAsync().Result;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content, Is.EqualTo("Hello test"));
        }

        [Test]
        public async Task Test3Async()
        {
            var request = new Test
            {
                Name = "Test",
            };

            var rquestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost/api/Game/PostHelloWorldName", rquestContent);

            var content = response.Content.ReadAsStringAsync().Result;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content, Is.EqualTo("Hello Test"));
        }

        public class MyControllerActivator : IHttpControllerActivator
        {
            public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
            {
                if (controllerType == typeof(GameController))
                {
                    return new GameController();
                }

                throw new ArgumentException("Invalid controller type.");
            }
        }
    }
}
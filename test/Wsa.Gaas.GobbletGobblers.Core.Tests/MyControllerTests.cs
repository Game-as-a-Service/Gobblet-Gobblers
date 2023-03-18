using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class MyControllerTests
        : WebApplicationFactory<Program>
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // 建立 HttpClient 實例
            _client = CreateClient();
        }

        [Test]
        public async Task TestGet()
        {
            // 建立 HTTP 請求
            var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast");

            // 執行 HTTP 請求
            var response = await _client.SendAsync(request);

            // 驗證 HTTP 回應
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}

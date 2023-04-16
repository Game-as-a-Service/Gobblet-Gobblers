using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Wsa.Gaas.GobbletGobblers.Application;
using Wsa.Gaas.GobbletGobblers.Application.UseCases;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class FinishGameTests
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
        public async Task Game()
        {
            var json = JsonConvert.SerializeObject(new CreateGameRequest
            {
                PlayerName = "Tom"
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "Game/Create");
            request.Content = content;

            var response = await _client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var result = await response.Content.ReadAsStringAsync();
            var gameId = JsonConvert.DeserializeObject<GameModel>(result).Id;

            var joinJson = JsonConvert.SerializeObject(new JoinGameRequest
            {
                Id = gameId,
                PlayerName = "John"
            });

            var jsonContent = new StringContent(joinJson, Encoding.UTF8, "application/json");

            var joinRequest = new HttpRequestMessage(HttpMethod.Post, "Game/Create");
            joinRequest.Content = jsonContent;


            var joinResponse = await _client.SendAsync(joinRequest);

            Assert.That(joinResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}

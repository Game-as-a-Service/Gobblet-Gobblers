using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wsa.Gaas.GobbletGobblers.Application;
using Wsa.Gaas.GobbletGobblers.Application.UseCases;
using Wsa.Gaas.GobbletGobblers.Domain;

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
            var createGameJson = JsonConvert.SerializeObject(new CreateGameRequest
            {
                PlayerName = "Tom"
            });

            var createGameContent = new StringContent(createGameJson, Encoding.UTF8, "application/json");

            var createGameRequest = new HttpRequestMessage(HttpMethod.Post, "Game/Create");
            createGameRequest.Content = createGameContent;

            var createGameResponse = await _client.SendAsync(createGameRequest);

            var result = await createGameResponse.Content.ReadAsStringAsync();
            var gameId = JsonConvert.DeserializeObject<GameModel>(result).Id;

            Assert.That(createGameResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var joinGameJson = JsonConvert.SerializeObject(new JoinGameRequest
            {
                Id = gameId,
                PlayerName = "John"
            });

            var joinGameContent = new StringContent(joinGameJson, Encoding.UTF8, "application/json");

            var joinGameRequest = new HttpRequestMessage(HttpMethod.Post, "Game/Join");
            joinGameRequest.Content = joinGameContent;


            var joinGameResponse = await _client.SendAsync(joinGameRequest);

            var joinGameResult = await joinGameResponse.Content.ReadAsStringAsync();
            var gameResult = JsonConvert.DeserializeObject<JObject>(joinGameResult);
            var player1Id = gameResult["players"][0]["id"].ToString();
            var player2Id = gameResult["players"][1]["id"].ToString();

            Assert.That(joinGameResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var putCockJson = JsonConvert.SerializeObject(new PutCockRequest
            {
                Id = gameId,
                PlayerId = Guid.Parse(player1Id),
                HandCockIndex = 0,
                Location = new Location(1, 1),
            });

            var putCockContent = new StringContent(putCockJson, Encoding.UTF8, "application/json");

            var putCockRequest = new HttpRequestMessage(HttpMethod.Post, "Game/PutCock");
            putCockRequest.Content = putCockContent;

            var putCockResponse = await _client.SendAsync(putCockRequest);

            Assert.That(putCockResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}

using System.Net;
using System.Text;
using Gaas.GobbletGobblers.Application;
using Gaas.GobbletGobblers.Application.UseCases;
using Gaas.GobbletGobblers.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gaas.Gobblet_Gobblers.Tests
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

            var putCockJsonStep1 = JsonConvert.SerializeObject(new PutCockRequest
            {
                Id = gameId,
                PlayerId = Guid.Parse(player1Id),
                HandCockIndex = 0,
                Location = new Location(1, 1),
            });

            var putCockContentStep1 = new StringContent(putCockJsonStep1, Encoding.UTF8, "application/json");

            var putCockRequestStep1 = new HttpRequestMessage(HttpMethod.Post, "Game/PutCock");
            putCockRequestStep1.Content = putCockContentStep1;

            var putCockResponseStep1 = await _client.SendAsync(putCockRequestStep1);

            Assert.That(putCockResponseStep1.StatusCode, Is.EqualTo(HttpStatusCode.OK));


            var putCockJsonStep2 = JsonConvert.SerializeObject(new PutCockRequest
            {
                Id = gameId,
                PlayerId = Guid.Parse(player2Id),
                HandCockIndex = 0,
                Location = new Location(2, 2),
            });

            var putCockContentStep2 = new StringContent(putCockJsonStep2, Encoding.UTF8, "application/json");

            var putCockRequestStep2 = new HttpRequestMessage(HttpMethod.Post, "Game/PutCock");
            putCockRequestStep2.Content = putCockContentStep2;

            var putCockResponseStep2 = await _client.SendAsync(putCockRequestStep2);

            Assert.That(putCockResponseStep2.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var moveCockJsonStep1 = JsonConvert.SerializeObject(new MoveCockRequest
            {
                Id = gameId,
                PlayerId = Guid.Parse(player1Id),
                From = new Location(1, 1),
                To = new Location(0, 0),
            });

            var moveCockContentStep1 = new StringContent(moveCockJsonStep1, Encoding.UTF8, "application/json");

            var moveCockRequestStep1 = new HttpRequestMessage(HttpMethod.Post, "Game/MoveCock");
            moveCockRequestStep1.Content = moveCockContentStep1;

            var moveCockResponseStep1 = await _client.SendAsync(moveCockRequestStep1);

            Assert.That(moveCockResponseStep1.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}

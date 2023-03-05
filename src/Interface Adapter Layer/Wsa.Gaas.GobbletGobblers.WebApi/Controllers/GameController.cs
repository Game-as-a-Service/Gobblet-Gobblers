using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Wsa.Gaas.GobbletGobblers.WebApi.Controllers
{
    public class GameController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage HelloWorld()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent("Hello .Net Farework");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetHelloWorldName(string name)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent($"Hello {name}");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            return response;
        }

        [HttpPost]
        public HttpResponseMessage PostHelloWorldName(Test test)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent($"Hello {test.Name}");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;
        }
    }

    public class Test
    {
        public string Name { get; set; }
    }
}

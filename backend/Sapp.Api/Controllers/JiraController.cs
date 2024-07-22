using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sapp.Common;

namespace Sapp.Api.Controllers
{
    [Route("jira")]
    [Produces("application/json")]
    public class JiraController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JiraController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> SearchIssues(
            [FromQuery] string jql)
        {
            using var client = _httpClientFactory.CreateClient("jiraClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                "<KEY>");
            
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://indeavor.atlassian.net/rest/api/latest/search?jql={jql}");
            
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<JiraSearchDto>(content);
            return Ok(jsonObject);
        }
    }
}

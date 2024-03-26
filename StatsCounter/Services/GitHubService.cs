using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;
        private readonly IConfiguration _configuration;

        public GitHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["GitHubSettings:BaseApiUrl"];
            _configuration = configuration;

            var token = _configuration["GitHubSettings:AccessToken"];
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "StatsCounter");
        }

        public async Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner)
        {
            var url = $"{_baseApiUrl}/users/{owner}/repos";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error retrieving repositories: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<IEnumerable<RepositoryInfo>>(content);

            return repositories ?? new List<RepositoryInfo>();
        }
    }
}

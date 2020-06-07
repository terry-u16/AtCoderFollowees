using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AtCoderFollowees.Core.Models;
using Microsoft.Extensions.Logging;

namespace AtCoderFollowees.Core.Services
{
    public class CodeforceService : ICodeforceService
    {
        private readonly HttpClient _client;
        private readonly ILogger<CodeforceService> _logger;

        public CodeforceService(HttpClient client, ILogger<CodeforceService> logger)
        {
            client.BaseAddress = new Uri("https://codeforces.com/");
            _client = client;
            _logger = logger;
        }

        public async Task<CodeforceUser?> GetCodeforcesUserAsync(string userName)
        {
            try
            {
                var response = await _client.GetAsync($"/api/user.info?handles={userName}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStreamAsync();

                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var userResponse = await JsonSerializer.DeserializeAsync<CodeforcesUserResponse>(json, jsonOptions);
                return userResponse?.Result?[0];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}

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
    public class TopCoderService : ITopCoderService
    {
        private readonly HttpClient _client;
        private readonly ILogger<TopCoderService> _logger;

        public TopCoderService(HttpClient client, ILogger<TopCoderService> logger)
        {
            client.BaseAddress = new Uri("https://api.topcoder.com/");
            _client = client;
            _logger = logger;
        }

        public async Task<TopCoderUserResponse?> GetTopCoderUserAsync(string userName)
        {
            try
            {
                var response = await _client.GetAsync($"/v2/users/{userName}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStreamAsync();

                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var userResponse = await JsonSerializer.DeserializeAsync<TopCoderUserResponse>(json, jsonOptions);
                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}

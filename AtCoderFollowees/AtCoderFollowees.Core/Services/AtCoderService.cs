using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using AtCoderFollowees.Core.Models;
using Microsoft.Extensions.Logging;

namespace AtCoderFollowees.Core.Services
{
    public class AtCoderService : IAtCoderService
    {
        // Disposeは不要 https://www.stevejgordon.co.uk/httpclient-creation-and-disposal-internals-should-i-dispose-of-httpclient
        private readonly HttpClient _client;
        private readonly ILogger<AtCoderService> _logger;
        private static readonly TimeSpan delayTime = TimeSpan.FromMilliseconds(1000);

        public AtCoderService(HttpClient client, ILogger<AtCoderService> logger)
        {
            client.BaseAddress = new Uri("https://atcoder.jp/");
            _client = client;
            _logger = logger;
        }

        public async IAsyncEnumerable<string> GetUserNamesAsync()
        {
            var parser = new HtmlParser();

            for (int page = 1; true; page++)
            {
                string[] userNames = null!;

                try
                {
                    using var response = await _client.GetAsync($"/ranking/all?page={page}");
                    response.EnsureSuccessStatusCode();
                    using var content = await response.Content.ReadAsStreamAsync();
                    using var html = await parser.ParseDocumentAsync(content);

                    userNames = html.GetElementsByClassName("username").Select(el => el.TextContent).ToArray();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    throw;
                }

                if (userNames.Length == 0)
                {
                    yield break;
                }

                foreach (var userName in userNames)
                {
                    yield return userName;
                }

                await Task.Delay(delayTime);
            }
        }

        public async Task<AtCoderUserResponse?> GetUserAsync(string userName)
        {
            var parser = new HtmlParser();

            try
            {
                using var response = await _client.GetAsync($"/users/{userName}");
                response.EnsureSuccessStatusCode();
                using var content = await response.Content.ReadAsStreamAsync();
                using var html = await parser.ParseDocumentAsync(content);

                // 無理矢理感あるので https://atcoder.jp/users/{user_name}/history/json からの取得も視野
                var rating = int.Parse(html.QuerySelector("#main-container > div.row > div.col-sm-9 > table > tbody > tr > td > span[class^=\"user-\"]").TextContent);
                var topCoderID = html.GetElementById("topcoder_id")?.TextContent;
                var codeforcesID = html.GetElementById("codeforces_id")?.TextContent;
                var twitterID = html.GetElementsByTagName("a").FirstOrDefault(el => el.HasAttribute("href") && el.Attributes["href"].Value.StartsWith("//twitter.com/"))?.TextContent;

                return new AtCoderUserResponse(userName, rating, topCoderID, codeforcesID, twitterID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}

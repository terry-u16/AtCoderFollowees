using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;
using AtCoderFollowees.Core.Services;
using Microsoft.Extensions.Logging;

namespace AtCoderFollowees.Test
{
    public class CodeforceServiceTest
    {
        [Fact]
        public async Task GetTouristSuccessTestAsync()
        {
            const string userName = "tourist";
            var mockHandler = new MockHttpMessageHandler();

            mockHandler
                .When($"https://codeforces.com/api/user.info?handles={userName}")
                .Respond(() => Task.Factory.StartNew(() =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Content = new StringContent("{\"status\":\"OK\",\"result\":[{\"lastName\":\"Korotkevich\",\"country\":\"Belarus\",\"lastOnlineTimeSeconds\":1591493250,\"city\":\"Gomel\",\"rating\":3520,\"friendOfCount\":26186,\"titlePhoto\":\"//userpic.codeforces.com/422/title/50a270ed4a722867.jpg\",\"handle\":\"tourist\",\"avatar\":\"//userpic.codeforces.com/422/avatar/2b5dbe87f0d859a2.jpg\",\"firstName\":\"Gennady\",\"contribution\":166,\"organization\":\"ITMO University\",\"rank\":\"legendary grandmaster\",\"maxRating\":3739,\"registrationTimeSeconds\":1265987288,\"maxRank\":\"legendary grandmaster\"}]}");
                    return response;
                }));
            var mockHttp = mockHandler.ToHttpClient();
            var mockLogger = new Mock<ILogger<CodeforceService>>();

            var codeforceService = new CodeforceService(mockHttp, mockLogger.Object);
            var user = await codeforceService.GetCodeforcesUserAsync(userName);
            Assert.Equal(userName, user?.Handle);
            Assert.Equal(3520, user?.Rating);
        }

        [Fact]
        public async Task GetInvalidUserFailTestAsync()
        {
            const string userName = "__invalid_user";
            var mockHandler = new MockHttpMessageHandler();

            mockHandler
                .When($"https://codeforces.com/api/user.info?handles={userName}")
                .Respond(() => Task.Factory.StartNew(() =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Content = new StringContent("{\"status\":\"FAILED\",\"comment\":\"handles: User with handle __invalid_user not found\"}");
                    return response;
                }));
            var mockHttp = mockHandler.ToHttpClient();
            var mockLogger = new Mock<ILogger<CodeforceService>>();

            var codeforceService = new CodeforceService(mockHttp, mockLogger.Object);
            var user = await codeforceService.GetCodeforcesUserAsync("invalid_user");
            Assert.Null(user);
        }
    }
}

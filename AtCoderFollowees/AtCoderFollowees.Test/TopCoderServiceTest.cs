using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AtCoderFollowees.Core.Services;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;

namespace AtCoderFollowees.Test
{
    public class TopCoderServiceTest
    {
        [Fact]
        public async Task GetTouristSuccessTestAsync()
        {
            const string userName = "tourist";
            var mockHandler = new MockHttpMessageHandler();

            mockHandler
                .When($"https://api.topcoder.com/v2/users/{userName}")
                .Respond(() => Task.Factory.StartNew(() =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Content = new StringContent(touristResponse);
                    return response;
                }));
            var mockHttp = mockHandler.ToHttpClient();
            var mockLogger = new Mock<ILogger<TopCoderService>>();

            var topCoderServices = new TopCoderService(mockHttp, mockLogger.Object);
            var user = await topCoderServices.GetTopCoderUserAsync(userName);
            Assert.Equal(userName, user?.Handle);
            Assert.Equal(3825, user?.RatingSummary?.FirstOrDefault(s => s.Name == "Algorithm")?.Rating);
        }

        [Fact]
        public async Task GetInvalidUserFailTestAsync()
        {
            const string userName = "__invalid_user";
            var mockHandler = new MockHttpMessageHandler();

            mockHandler
                .When($"https://api.topcoder.com/v2/users/{userName}")
                .Respond(() => Task.Factory.StartNew(() =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Content = new StringContent(invalidUserResponse);
                    return response;
                }));
            var mockHttp = mockHandler.ToHttpClient();
            var mockLogger = new Mock<ILogger<TopCoderService>>();

            var topCoderServices = new TopCoderService(mockHttp, mockLogger.Object);
            var user = await topCoderServices.GetTopCoderUserAsync(userName);
            Assert.Null(user);
        }

        const string touristResponse = @"{
    ""handle"": ""tourist"",
    ""country"": ""Belarus"",
    ""memberSince"": ""2006-05-31T07:28:00.000-04:00"",
    ""quote"": """",
    ""photoLink"": ""/i/m/tourist.jpeg"",
    ""copilot"": false,
    ""ratingSummary"": [
        {
            ""name"": ""Code"",
            ""rating"": 1189,
            ""colorStyle"": ""color: #00A900""
        },
        {
            ""name"": ""Algorithm"",
            ""rating"": 3825,
            ""colorStyle"": ""color: #EE0000""
        },
        {
            ""name"": ""Marathon Match"",
            ""rating"": 2201,
            ""colorStyle"": ""color: #EE0000""
        }
    ],
    ""Achievements"": [
        {
            ""date"": ""2020-05-12T00:00:00.000-04:00"",
            ""description"": ""Eight Marathon Top-5 Placements""
        },
        {
            ""date"": ""2020-05-08T00:00:00.000-04:00"",
            ""description"": ""Twenty Marathon Competitions""
        },
        {
            ""date"": ""2019-12-06T00:00:00.000-05:00"",
            ""description"": ""Studio First Passing Submission""
        },
        {
            ""date"": ""2019-12-06T00:00:00.000-05:00"",
            ""description"": ""Studio First Placement""
        },
        {
            ""date"": ""2019-12-06T00:00:00.000-05:00"",
            ""description"": ""Studio First Win""
        },
        {
            ""date"": ""2019-11-28T00:00:00.000-05:00"",
            ""description"": ""Four Marathon Top-5 Placements""
        },
        {
            ""date"": ""2019-11-19T00:00:00.000-05:00"",
            ""description"": ""First Win""
        },
        {
            ""date"": ""2019-08-12T00:00:00.000-04:00"",
            ""description"": ""First Placement""
        },
        {
            ""date"": ""2019-07-29T00:00:00.000-04:00"",
            ""description"": ""Fifty Placements""
        },
        {
            ""date"": ""2019-07-29T00:00:00.000-04:00"",
            ""description"": ""Twenty Five Placements""
        },
        {
            ""date"": ""2018-12-03T00:00:00.000-05:00"",
            ""description"": ""Marathon Match Winner""
        },
        {
            ""date"": ""2018-12-03T00:00:00.000-05:00"",
            ""description"": ""Two Marathon Top-5 Placements""
        },
        {
            ""date"": ""2018-11-29T00:00:00.000-05:00"",
            ""description"": ""One Hundred Passing Submissions""
        },
        {
            ""date"": ""2018-11-14T00:00:00.000-05:00"",
            ""description"": ""TCO18 Finalist""
        },
        {
            ""date"": ""2018-10-04T00:00:00.000-04:00"",
            ""description"": ""Five Hundred Solved Algorithm Problems""
        },
        {
            ""date"": ""2018-08-05T00:00:00.000-04:00"",
            ""description"": ""Ten Marathon Competitions""
        },
        {
            ""date"": ""2018-06-19T00:00:00.000-04:00"",
            ""description"": ""First Marathon Top-5 Placement""
        },
        {
            ""date"": ""2018-05-08T00:00:00.000-04:00"",
            ""description"": ""Marathon Match 100""
        },
        {
            ""date"": ""2018-01-17T00:00:00.000-05:00"",
            ""description"": ""Fifty Passing Submissions""
        },
        {
            ""date"": ""2018-01-17T00:00:00.000-05:00"",
            ""description"": ""First Passing Submission""
        },
        {
            ""date"": ""2017-11-21T00:00:00.000-05:00"",
            ""description"": ""SRM Engagement Honor""
        },
        {
            ""date"": ""2017-10-24T00:00:00.000-04:00"",
            ""description"": ""TCO17 Finalist""
        },
        {
            ""date"": ""2017-05-28T00:00:00.000-04:00"",
            ""description"": ""Three Marathon Competitions""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""Fifty Solved Algorithm Problems""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""First Solved Algorithm Problem""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""First Successful Challenge""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""Five Successful Challenges""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""One Hundred Successful Challenges""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""Solved Hard Div1 Problem in SRM""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""Ten Solved Algorithm Problems""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""Twenty Five Successful Challenges""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""Two Hundred Solved Algorithm Problems""
        },
        {
            ""date"": ""2016-07-24T00:00:00.000-04:00"",
            ""description"": ""Two Hundred Successful Challenges""
        },
        {
            ""date"": ""2015-01-25T00:00:00.000-05:00"",
            ""description"": ""One Hundred SRM Room Wins (Any Division)""
        },
        {
            ""date"": ""2014-11-25T00:00:00.000-05:00"",
            ""description"": ""2014 Algorithm Champion""
        },
        {
            ""date"": ""2013-11-13T00:00:00.000-05:00"",
            ""description"": ""2013 Topcoder Open Algorithm Finalist""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""Algorithm Target""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""Fifty SRM Room Wins (Any Division)""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""First Marathon Competition""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""First Rated Algorithm Competition""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""First SRM Room Win (Any Division)""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""Five Rated Algorithm Competitions""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""Five SRM Room Wins (Any Division)""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""One Hundred Rated Algorithm Competitions""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""SRM Winner Div 1""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""Twenty Five Rated Algorithm Competitions""
        },
        {
            ""date"": ""2012-09-28T00:00:00.000-04:00"",
            ""description"": ""Twenty SRM Room Wins (Any Division)""
        },
        {
            ""date"": ""2010-08-04T00:00:00.000-04:00"",
            ""description"": ""First Forum Post""
        },
        {
            ""date"": ""2010-03-20T00:00:00.000-04:00"",
            ""description"": ""2010 TopCoder High School Champion""
        },
        {
            ""date"": ""2009-07-01T00:00:00.000-04:00"",
            ""description"": ""TopCoder Algorithm Coder of the Month for July 2009""
        }
    ],
    ""serverInformation"": {
        ""serverName"": ""TopCoder API"",
        ""apiVersion"": ""0.0.1"",
        ""requestDuration"": 3,
        ""currentTime"": 1591628057942
    },
    ""requesterInformation"": {
        ""id"": ""7bb5fefb38191f50bdd808f02fda8ddc03e0a7e9-dvysFSLZS97OCpyw"",
        ""remoteIP"": ""119.242.14.93"",
        ""receivedParams"": {
            ""apiVersion"": ""v2"",
            ""handle"": ""tourist"",
            ""action"": ""getBasicUserProfile""
        }
    }
}";
        const string invalidUserResponse = @"{
    ""error"": {
      ""name"": ""Not Found"",
      ""value"": 404,
      ""description"": ""The URI requested is invalid or the requested resource does not exist."",
      ""details"": ""User does not exist.""
    },
    ""serverInformation"": {
      ""serverName"": ""TopCoder API"",
      ""apiVersion"": ""0.0.1"",
      ""requestDuration"": 10,
      ""currentTime"": 1591628577703
    },
    ""requesterInformation"": {
      ""id"": ""7bb5fefb38191f50bdd808f02fda8ddc03e0a7e9-94PQDg52wNU15XMY"",
      ""remoteIP"": ""119.242.14.93"",
      ""receivedParams"": {
        ""apiVersion"": ""v2"",
        ""handle"": ""__invalid_user"",
        ""action"": ""getBasicUserProfile""
      }
    }
  }";
    }
}

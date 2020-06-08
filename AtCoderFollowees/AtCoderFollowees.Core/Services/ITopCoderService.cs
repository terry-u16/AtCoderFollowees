using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtCoderFollowees.Core.Models;

namespace AtCoderFollowees.Core.Services
{
    public interface ITopCoderService
    {
        Task<TopCoderUserResponse?> GetTopCoderUserAsync(string userName);
    }
}

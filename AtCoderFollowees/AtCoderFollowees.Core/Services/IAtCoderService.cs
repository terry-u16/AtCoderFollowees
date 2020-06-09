using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtCoderFollowees.Core.Models;

namespace AtCoderFollowees.Core.Services
{
    public interface IAtCoderService
    {
        IAsyncEnumerable<string> GetUserNamesAsync();
        Task<AtCoderUserResponse?> GetUserAsync(string userName);
    }
}

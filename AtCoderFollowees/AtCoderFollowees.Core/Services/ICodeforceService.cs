using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtCoderFollowees.Core.Models;

namespace AtCoderFollowees.Core.Services
{
    public interface ICodeforceService
    {
        Task<CodeforceUser?> GetCodeforcesUserAsync(string userName);
    }
}

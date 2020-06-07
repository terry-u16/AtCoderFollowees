using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtCoderFollowees.Core.Models;
using AtCoderFollowees.Models.Data;

namespace AtCoderFollowees.Core.Services
{
    public interface ICodeforceService
    {
        Task<CodeforceUser?> GetCodeforcesUserAsync(string userName);
    }
}

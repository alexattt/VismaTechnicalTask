using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IAppRecService
    {
        Task<List<AppRec>> GetAllAppRecsAsync();
        Task InsertAppRecAsync(AppRec apprec);
    }
}
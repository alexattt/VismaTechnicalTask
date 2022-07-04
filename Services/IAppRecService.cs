using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.HelperModels;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IAppRecService
    {
        Task<List<AppRec>> GetAllAppRecsAsync();
        Task<AppRec> GetAppRecById(string id);
        Task<List<SenderStatusRatio>> GetSenderMonthlyRatio(string SenderIdentifier, string StartDate, string EndDate);
        Task<bool> InsertAppRecAsync(AppRec apprec);
    }
}
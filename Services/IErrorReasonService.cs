using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.HelperModels;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IErrorReasonService
    {
        Task<List<ErrorReason>> GetAllErrorReasonsAsync();
        Task<bool> InsertErrorReasonAsync(ErrorReason errorReason);
        Task<List<ErrorTop>> GetTopErrorsForPeriod(String StartDate, String EndDate);
        Task<List<ErrorTop>> GetTopErrorsForSender(String SenderIdentifier, String StartDate, String EndDate);
        Task<List<FailedPeriodOverview>> GetFailedApprecPeriodsGlobal();
        Task<List<FailedPeriodOverview>> GetFailedApprecPeriodsSender(String SenderIdentifier);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.HelperModels;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class ErrorReasonService : IErrorReasonService
    {
        private readonly DataContext _dataContext;

        public ErrorReasonService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<ErrorReason>> GetAllErrorReasonsAsync()
        {
            List<ErrorReason> errorReasons = await _dataContext.ErrorReasons.ToListAsync();
            return errorReasons;
        }

        public async Task<bool> InsertErrorReasonAsync(ErrorReason errorReason)
        {
            await _dataContext.ErrorReasons.AddAsync(errorReason);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ErrorTop>> GetTopErrorsForPeriod(String StartDate, String EndDate)
        {
            List<ErrorTop> errorTop =
                await _dataContext.ErrorTops
                    .FromSqlInterpolated($"EXEC dbo.AppRecErrorTopGlobal {StartDate}, {EndDate}")
                    .ToListAsync();

            return errorTop;
        }

        public async Task<List<ErrorTop>> GetTopErrorsForSender(String SenderIdentifier, String StartDate, String EndDate)
        {
            List<ErrorTop> errorTop =
                await _dataContext.ErrorTops
                    .FromSqlInterpolated($"EXEC dbo.AppRecErrorTopSender {SenderIdentifier}, {StartDate}, {EndDate}")
                    .ToListAsync();

            return errorTop;
        }

        public async Task<List<FailedPeriodOverview>> GetFailedApprecPeriodsGlobal()
        {
            List<FailedPeriodOverview> globalFailedPeriods =
                await _dataContext.FailedPeriodOverviews
                    .FromSqlInterpolated($"EXEC dbo.GetFailedApprecPeriodsGlobal")
                    .ToListAsync();

            return globalFailedPeriods;
        }

        public async Task<List<FailedPeriodOverview>> GetFailedApprecPeriodsSender(String SenderIdentifier)
        {
            List<FailedPeriodOverview> senderFailedPeriods =
                await _dataContext.FailedPeriodOverviews
                    .FromSqlInterpolated($"EXEC dbo.GetFailedApprecPeriodsSender {SenderIdentifier}")
                    .ToListAsync();

            return senderFailedPeriods;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.HelperModels;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class AppRecService : IAppRecService
    {
        private readonly DataContext _dataContext;

        public AppRecService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<AppRec>> GetAllAppRecsAsync()
        {
            List<AppRec> apprecs = await _dataContext.AppRecs.ToListAsync();
            return apprecs;
        }

        public async Task<AppRec> GetAppRecById(string id)
        {
            return await _dataContext.AppRecs.FindAsync(id);
        }

        public async Task<List<SenderStatusRatio>> GetSenderMonthlyRatio(string SenderIdentifier, string StartDate, string EndDate)
        {
            List<SenderStatusRatio> senderRatios =
                await _dataContext.SenderStatusRatios
                    .FromSqlInterpolated($"EXEC dbo.GetMonthlySenderRatio {SenderIdentifier}, {StartDate}, {EndDate}")
                    .ToListAsync();

            foreach (var ratios in senderRatios)
            {
                if (ratios.Status == "1")
                {
                    ratios.Status = "OK";
                }
                else if (ratios.Status == "2")
                {
                    ratios.Status = "Avvist";
                }
                else
                {
                    ratios.Status = "OK, feil i delmelding";
                }
            }

            return senderRatios;

        }

        public async Task<bool> InsertAppRecAsync(AppRec apprec)
        {
            await _dataContext.AppRecs.AddAsync(apprec);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}

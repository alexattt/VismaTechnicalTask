using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
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

        public async Task InsertAppRecAsync(AppRec apprec)
        {
            await _dataContext.AppRecs.AddAsync(apprec);
            await _dataContext.SaveChangesAsync();
        }
    }
}

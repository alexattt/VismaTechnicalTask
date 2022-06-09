using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class HelperInfoService : IHelperInfoService
    {
        private readonly DataContext _dataContext;

        public HelperInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<HelperInfo> GetLastAddedXmlDate()
        {
            List<HelperInfo> helperInfos = await _dataContext.HelperInfo.ToListAsync();
            return helperInfos.First();
        }

        public async Task InsertLastAddedXmlDate(HelperInfo helperInfo)
        {
            await _dataContext.HelperInfo.AddAsync(helperInfo);
            await _dataContext.SaveChangesAsync();
        }
    }
}

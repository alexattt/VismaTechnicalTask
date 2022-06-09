using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class HCPersonService : IHCPersonService
    {
        private readonly DataContext _dataContext;

        public HCPersonService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<HCPerson>> GetAllHCPersonsAsync()
        {
            List<HCPerson> hcpersons = await _dataContext.HCPersons.ToListAsync();
            return hcpersons;
        }

        public async Task InsertHCPersonAsync(HCPerson hcperson)
        {
            await _dataContext.HCPersons.AddAsync(hcperson);
            await _dataContext.SaveChangesAsync();
        }
    }
}

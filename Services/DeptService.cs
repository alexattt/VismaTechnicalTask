using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class DeptService : IDeptService
    {
        private readonly DataContext _dataContext;

        public DeptService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<Dept>> GetAllDeptsAsync()
        {
            List<Dept> depts = await _dataContext.Depts.ToListAsync();
            return depts;
        }

        public async Task InsertDeptAsync(Dept dept)
        {
            await _dataContext.Depts.AddAsync(dept);
            await _dataContext.SaveChangesAsync();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IDeptService
    {
        Task<List<Dept>> GetAllDeptsAsync();
        Task InsertDeptAsync(Dept dept);
    }
}
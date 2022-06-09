using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IHCPersonService
    {
        Task<List<HCPerson>> GetAllHCPersonsAsync();
        Task InsertHCPersonAsync(HCPerson hcperson);
    }
}
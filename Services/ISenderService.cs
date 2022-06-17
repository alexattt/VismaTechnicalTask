using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface ISenderService
    {
        Task<List<Sender>> GetAllSendersAsync();
        Task<Sender> GetSenderById(int id);
        Task<int> CheckSenderExists(Sender se);
        Task<int> InsertSenderAsync(Sender sender);
    }
}
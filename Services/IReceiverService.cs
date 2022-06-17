using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IReceiverService
    {
        Task<List<Receiver>> GetAllReceiversAsync();
        Task<Receiver> GetReceiverById(int id);
        Task<int> CheckReceiverExists(Receiver rec);
        Task<int> InsertReceiverAsync(Receiver receiver);
    }
}
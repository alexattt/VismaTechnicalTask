using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IReceiverService
    {
        Task<List<Receiver>> GetAllReceiversAsync();
        Task InsertReceiverAsync(Receiver receiver);
    }
}
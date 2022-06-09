using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface ISenderService
    {
        Task<List<Sender>> GetAllSendersAsync();
        Task InsertSenderAsync(Sender sender);
    }
}
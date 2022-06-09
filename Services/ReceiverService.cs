using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly DataContext _dataContext;

        public ReceiverService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<Receiver>> GetAllReceiversAsync()
        {
            List<Receiver> receivers = await _dataContext.Receivers.ToListAsync();
            return receivers;
        }

        public async Task InsertReceiverAsync(Receiver receiver)
        {
            await _dataContext.Receivers.AddAsync(receiver);
            await _dataContext.SaveChangesAsync();
        }
    }
}

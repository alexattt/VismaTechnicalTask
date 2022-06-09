using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class SenderService : ISenderService
    {
        private readonly DataContext _dataContext;

        public SenderService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<Sender>> GetAllSendersAsync()
        {
            List<Sender> senders = await _dataContext.Senders.ToListAsync();
            return senders;
        }

        public async Task InsertSenderAsync(Sender sender)
        {
            await _dataContext.Senders.AddAsync(sender);
            await _dataContext.SaveChangesAsync();
        }
    }
}

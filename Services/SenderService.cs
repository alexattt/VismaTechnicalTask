using System.Collections.Generic;
using System.Linq;
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

        public async Task<Sender> GetSenderById(int id)
        {
            return await _dataContext.Senders.FindAsync(id);
        }

        public async Task<int> CheckSenderExists(Sender se)
        {
            Sender sender = await _dataContext.Senders
                                                       .Where(s => s.SenderId == se.SenderId && s.MedSpeciality == se.MedSpeciality && s.Name == se.Name &&
                                                              s.Type == se.Type && s.TypeId == se.TypeId && s.DeptId == se.DeptId && s.HCPersonId == se.HCPersonId &&
                                                              s.DeptName == se.DeptName && s.HCPersonName == se.HCPersonName && s.TeleAddress == se.TeleAddress)
                                                       .FirstOrDefaultAsync();
            return sender == null ? 0 : sender.Id;
        }

        public async Task<int> InsertSenderAsync(Sender sender)
        {
            await _dataContext.Senders.AddAsync(sender);
            await _dataContext.SaveChangesAsync();
            return sender.Id;
        }
    }
}

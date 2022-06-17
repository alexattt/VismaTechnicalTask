using System.Collections.Generic;
using System.Linq;
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

        public async Task<Receiver> GetReceiverById(int id)
        {
            return await _dataContext.Receivers.FindAsync(id);
        }
        public async Task<int> CheckReceiverExists(Receiver rec)
        {
            Receiver receiver = await _dataContext.Receivers
                                                       .Where(r => r.ReceiverId == rec.ReceiverId && r.MedSpeciality == rec.MedSpeciality && r.Name == rec.Name &&
                                                              r.Type == rec.Type && r.TypeId == rec.TypeId && r.DeptId == rec.DeptId && r.HCPersonId == rec.HCPersonId &&
                                                              r.DeptName == rec.DeptName && r.HCPersonName == rec.HCPersonName && r.TeleAddress == rec.TeleAddress)
                                                       .FirstOrDefaultAsync();
            return receiver == null ? 0 : receiver.Id;
        }

        public async Task<int> InsertReceiverAsync(Receiver receiver)
        {
            await _dataContext.Receivers.AddAsync(receiver);
            await _dataContext.SaveChangesAsync();
            return receiver.Id;
        }
    }
}

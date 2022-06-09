using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.Data;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public class ErrorReasonService : IErrorReasonService
    {
        private readonly DataContext _dataContext;

        public ErrorReasonService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<ErrorReason>> GetAllErrorReasonsAsync()
        {
            List<ErrorReason> errorReasons = await _dataContext.ErrorReasons.ToListAsync();
            return errorReasons;
        }

        public async Task InsertErrorReasonAsync(ErrorReason errorReason)
        {
            await _dataContext.ErrorReasons.AddAsync(errorReason);
            await _dataContext.SaveChangesAsync();
        }
    }
}

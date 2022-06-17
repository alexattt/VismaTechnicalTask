using System.Collections.Generic;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IErrorReasonService
    {
        Task<List<ErrorReason>> GetAllErrorReasonsAsync();
        Task<bool> InsertErrorReasonAsync(ErrorReason errorReason);
    }
}
using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IHelperInfoService
    {
        Task<HelperInfo> GetLastAddedXmlDate();
        Task UpdateLastAddedXmlDate(HelperInfo helperInfo);
    }
}
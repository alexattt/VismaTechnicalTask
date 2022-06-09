using System.Threading.Tasks;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Services
{
    public interface IHelperInfoService
    {
        Task<HelperInfo> GetLastAddedXmlDate(int Id);
        Task InsertLastAddedXmlDate(HelperInfo helperInfo);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using VismaTechnicalTask.HelperModels;

namespace VismaTechnicalTask.Hubs
{
    public class FailedApprecsHub : Hub
    {
        public async Task SendNegativeApprecPeriodsSender(List<FailedPeriodOverview> negativeApprecsSender)
        {
            await Clients.All.SendAsync("ReceiveSenderData", negativeApprecsSender);
        }
    }
}

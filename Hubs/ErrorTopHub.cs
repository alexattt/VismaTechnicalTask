using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using VismaTechnicalTask.HelperModels;

namespace VismaTechnicalTask.Hubs
{
    public class ErrorTopHub : Hub
    {
        public async Task SendGlobalErrorData(List<ErrorTop> errorTopGlobal)
        {
           await Clients.All.SendAsync("ReceiveData", errorTopGlobal);
        }

        public async Task SendSenderErrorData(List<ErrorTop> errorTopSender)
        {
            await Clients.All.SendAsync("ReceiveSenderData", errorTopSender);
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace VismaTechnicalTask.Hubs
{
    public class ParsingResponseHub : Hub
    {
        public async Task SendXmlParsingResult(String parsingRes)
        {
            await Clients.All.SendAsync("ReceiveParsingRes", parsingRes);
        }

        public async Task SendXmlLastAddedDate(String lastXmlDate)
        {
            await Clients.All.SendAsync("ReceiveLastXMLDate", lastXmlDate);
        }
    }
}

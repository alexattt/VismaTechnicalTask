using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VismaTechnicalTask.Models;
using VismaTechnicalTask.Services;

namespace VismaTechnicalTask.HelperMethods
{
    public class EntitySaving
    {
        readonly IAppRecService IAppRecService;
        readonly IErrorReasonService IErrorReasonService;
        readonly IReceiverService IReceiverService;
        readonly ISenderService ISenderService;

        public EntitySaving()
        {

        }

        public EntitySaving(IAppRecService IAppRecService, IErrorReasonService IErrorReasonService, IReceiverService IReceiverService, ISenderService ISenderService)
        {
            this.IAppRecService = IAppRecService;
            this.IErrorReasonService = IErrorReasonService;
            this.IReceiverService = IReceiverService;
            this.ISenderService = ISenderService;
        }

        public async Task<String> Save(AppRec apprec, List<ErrorReason> errorReasons, Sender sender, Receiver receiver)
        {
            bool receiverIsAdded = false;
            bool senderIsAdded = false;

            int newReceiverId = 0;
            int newSenderId = 0;

            int receiverExists = 0;
            int senderExists = 0;

            bool receiverHasInfo = receiver.GetType()
                                                .GetProperties()
                                                .Select(r => r.GetValue(receiver))
                                                .Any(value => value != null);

            bool senderHasInfo = sender.GetType()
                                            .GetProperties()
                                            .Select(r => r.GetValue(sender))
                                            .Any(value => value != null);

            bool apprecHasInfo = apprec.GetType()
                                            .GetProperties()
                                            .Select(ar => ar.GetValue(apprec))
                                            .Any(value => value != null);

            if (receiverHasInfo)
            {
                receiverExists = await IReceiverService.CheckReceiverExists(receiver);
            }
            if (senderHasInfo)
            {
                senderExists = await ISenderService.CheckSenderExists(sender);
            }

            if (receiverHasInfo && receiverExists == 0)
            {
                try
                {
                    newReceiverId = await IReceiverService.InsertReceiverAsync(receiver);
                }
                catch
                {
                    return $"Problem occured while saving entities, reason - Apprec with ID {apprec.Id}, Receiver problem";
                }
                receiverIsAdded = true;
            }

            if (senderHasInfo && senderExists == 0)
            {
                try
                {
                    newSenderId = await ISenderService.InsertSenderAsync(sender);
                }
                catch
                {
                    return $"Problem occured while saving entities, reason - Apprec with ID {apprec.Id}, Sender problem";
                }
                senderIsAdded = true;
            }

            if (apprecHasInfo)
            {
                if (senderIsAdded && newSenderId != 0)
                {
                    Sender newSender = await ISenderService.GetSenderById(newSenderId);
                    apprec.SenderID = newSender.Id;
                }
                else
                {
                    apprec.SenderID = senderExists;
                }

                if (receiverIsAdded && newReceiverId != 0)
                {
                    Receiver newReceiver = await IReceiverService.GetReceiverById(newReceiverId);
                    apprec.ReceiverID = newReceiver.Id;
                }
                else
                {
                    apprec.ReceiverID = receiverExists;
                }

                try
                {
                    await IAppRecService.InsertAppRecAsync(apprec);
                }
                catch
                {
                    return $"Problem occured while saving entities, reason - Apprec with ID {apprec.Id}";
                }
            }

            if (errorReasons.Any())
            {
                foreach (var errorReason in errorReasons)
                {
                    try
                    {
                        await IErrorReasonService.InsertErrorReasonAsync(errorReason);
                    }
                    catch
                    {
                        return $"Problem occured while saving entities, reason - Apprec with ID {apprec.Id}, Error reason problem";
                    }
                }
            }

            return "Success";
        }
    }
}

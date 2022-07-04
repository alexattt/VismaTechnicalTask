using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using VismaTechnicalTask.HelperMethods;
using VismaTechnicalTask.Models;
using VismaTechnicalTask.Services;

namespace VismaTechnicalTask.HelperFunctions
{
    public class XmlParsing
    {
        private IHelperInfoService IHelperInfoService;
        private IAppRecService IAppRecService;
        private IErrorReasonService IErrorReasonService;
        private IReceiverService IReceiverService;
        private ISenderService ISenderService;

        public XmlParsing()
        {
        }

        public XmlParsing(IAppRecService IAppRecService, IErrorReasonService IErrorReasonService, 
                          ISenderService ISenderService, IReceiverService IReceiverService, IHelperInfoService IHelperInfoService)
        {
            this.IHelperInfoService = IHelperInfoService;
            this.IAppRecService = IAppRecService;
            this.IErrorReasonService = IErrorReasonService;
            this.IReceiverService = IReceiverService;
            this.ISenderService = ISenderService;
        }

        public static AppRec apprec;
        public static Sender sender;
        public static Receiver receiver;
        public static List<ErrorReason> errorReasons;

        public async Task<String> ReadFiles(DateTime lastXmlAddDate)
        {
            EntitySaving EntitySaving = new EntitySaving(IAppRecService, IErrorReasonService, IReceiverService, ISenderService);

            var path = @"xml_receipts";

            IOrderedEnumerable<string> files;
            var lastFile = "";

            if (lastXmlAddDate.Equals(new DateTime(1900, 1, 1)))
            {
                files = Directory.GetFiles(path).OrderBy(f => new FileInfo(f).CreationTimeUtc);
            }
            else
            {
                files = Directory.GetFiles(path).Where(f => Convert.ToDateTime(new FileInfo(f).CreationTimeUtc) > lastXmlAddDate)
                                                .OrderBy(f => new FileInfo(f).CreationTimeUtc);
            }

            foreach (var file in files)
            {
                apprec = new AppRec();
                sender = new Sender();
                receiver = new Receiver();
                errorReasons = new List<ErrorReason>();

                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                ParseXmlFile(doc.DocumentElement);

                lastFile = file;

                String entitySavingRes = await EntitySaving.Save(apprec, errorReasons, sender, receiver);

                if (entitySavingRes != "Success")
                {
                    return entitySavingRes;
                }
            }

            if (lastFile != "")
            {

                await IHelperInfoService.UpdateLastAddedXmlDate(new HelperInfo()
                    { LastAddedXmlDate = new FileInfo(lastFile).CreationTimeUtc });
            }

            return "All XML receipts saved successfully!";
        }

        public static void ParseXmlFile(XmlNode appRecNodes)
        {
            foreach (XmlNode appRecNode in appRecNodes)
            {
                if (appRecNode.Name == "Id")
                {
                    apprec.Id = appRecNode.InnerXml;
                }
                if (appRecNode.Name == "GenDate")
                {
                    apprec.GenDate = Convert.ToDateTime(appRecNode.InnerXml);
                }
                if (appRecNode.Name == "MIGversion")
                {
                    apprec.MIGversion = appRecNode.InnerXml;
                }
                if (appRecNode.Name == "MsgType")
                {
                    if (appRecNode.Attributes["V"] != null)
                        apprec.MsgType = appRecNode.Attributes["V"].Value;
                }
                if (appRecNode.Name == "Status")
                {
                    if (appRecNode.Attributes["V"] != null)
                        apprec.Status = appRecNode.Attributes["V"].Value;
                }
                if (appRecNode.Name == "Error")
                {
                    ParseError(appRecNode);
                }
                if (appRecNode.Name == "Sender")
                {
                    ParseSender(appRecNode);
                }
                if (appRecNode.Name == "Receiver")
                {
                    ParseReceiver(appRecNode);
                }
                if (appRecNode.Name == "OriginalMsgId")
                {
                    ParseOriginalMsg(appRecNode);
                }
            }
        }

        public static void ParseError(XmlNode errorNode)
        {
            ErrorReason errorReason = new ErrorReason();
            if (errorNode.Attributes["OT"] != null)
                errorReason.Err_OT = errorNode.Attributes["OT"].Value;
            if (errorNode.Attributes["S"] != null)
                errorReason.Err_S = errorNode.Attributes["S"].Value;
            if (errorNode.Attributes["V"] != null)
                errorReason.Err_V = errorNode.Attributes["V"].Value;
            if (errorNode.Attributes["DN"] != null)
                errorReason.Err_DN = errorNode.Attributes["DN"].Value;

            errorReason.AppRecID = apprec.Id;
            errorReasons.Add(errorReason);
        }

        public static void ParseSender(XmlNode senderNodes)
        {
            foreach (XmlNode senderNode in senderNodes)
            {
                if (senderNode.Name == "Role")
                {
                    if (senderNode.Attributes["V"].Value != null)
                    {
                        sender.Role = senderNode.Attributes["V"].Value;
                    }
                }
                if (senderNode.Name == "HCP")
                {
                    ParseHCP(senderNode, "Sender");
                }
            }
        }

        public static void ParseReceiver(XmlNode receiverNodes)
        {
            foreach (XmlNode receiverNode in receiverNodes)
            {
                if (receiverNode.Name == "Role")
                {
                    receiver.Role = receiverNode.Attributes["V"].Value;
                }
                if (receiverNode.Name == "HCP")
                {
                    ParseHCP(receiverNode, "Receiver");
                }
            }
        }

        public static void ParseHCP(XmlNode hcpNodes, String type)
        {
            foreach (XmlNode hcpNode in hcpNodes)
            {
                if (hcpNode.Name == "MedSpeciality")
                {
                    if (type == "Sender")
                        sender.MedSpeciality = hcpNode.InnerXml;
                    else
                        receiver.MedSpeciality = hcpNode.InnerXml;
                }
                if (hcpNode.Name == "Inst")
                {
                    ParseInst(hcpNode, type);
                }
                if (hcpNode.Name == "HCProf")
                {
                    ParseHCProf(hcpNode, type);
                }
                if (hcpNode.Name == "Address")
                {
                    ParseAddress(hcpNode, type);
                }
            }
        }

        public static void ParseInst(XmlNode instNodes, String type)
        {
            foreach (XmlNode instNode in instNodes)
            {
                if (instNode.Name == "Name")
                {
                    if (type == "Sender")
                        sender.Name = instNode.InnerXml;
                    else
                        receiver.Name = instNode.InnerXml;
                }
                if (instNode.Name == "Id")
                {
                    if (type == "Sender")
                    {
                        //apprec.SenderID = instNode.InnerXml;
                        sender.SenderId = instNode.InnerXml;
                    }
                    else
                    {
                        //apprec.ReceiverID = instNode.InnerXml;
                        receiver.ReceiverId = instNode.InnerXml;
                    }
                }
                if (instNode.Name == "TypeId")
                {
                    if (instNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.TypeId = instNode.Attributes["V"].Value;
                        else
                            receiver.TypeId = instNode.Attributes["V"].Value;
                }
                if (instNode.Name == "HCPerson")
                {
                    ParseHCPerson(instNode, type);
                }
                if (instNode.Name == "Dept")
                {
                    ParseDept(instNode, type);
                }
            }
        }

        public static void ParseHCProf(XmlNode hcprofNodes, String type)
        {
            foreach (XmlNode hcprofNode in hcprofNodes)
            {
                if (hcprofNode.Name == "Type")
                {
                    if (hcprofNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.Type = hcprofNode.Attributes["V"].Value;
                        else
                            receiver.Type = hcprofNode.Attributes["V"].Value;
                }
                if (hcprofNode.Name == "Name")
                {
                    if (type == "Sender")
                        sender.Name = hcprofNode.InnerXml;
                    else
                        receiver.Name = hcprofNode.InnerXml;
                }
                if (hcprofNode.Name == "Id")
                {
                    if (type == "Sender")
                    {
                        //apprec.SenderID = hcprofNode.InnerXml;
                        sender.SenderId = hcprofNode.InnerXml;
                    }
                    else
                    {
                        //apprec.ReceiverID = hcprofNode.InnerXml;
                        receiver.ReceiverId = hcprofNode.InnerXml;
                    }
                }
                if (hcprofNode.Name == "TypeId")
                {
                    if (hcprofNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.TypeId = hcprofNode.Attributes["V"].Value;
                        else
                            receiver.TypeId = hcprofNode.Attributes["V"].Value;
                }
                // if (hcprofNode.Name == "AdditionalId")
                // {
                //     ParseAdditionalId(hcprofNode);
                // }
            }
        }

        public static void ParseHCPerson(XmlNode hcpersonNodes, String type)
        {
            foreach (XmlNode hcpersonNode in hcpersonNodes)
            {
                if (hcpersonNode.Name == "Name")
                {
                    if (type == "Sender")
                        sender.HCPersonName = hcpersonNode.InnerXml;
                    else
                        receiver.HCPersonName = hcpersonNode.InnerXml;
                }
                if (hcpersonNode.Name == "Id")
                {
                    if (type == "Sender")
                        sender.HCPersonId = hcpersonNode.InnerXml;
                    else
                        receiver.HCPersonId = hcpersonNode.InnerXml;
                }
                if (hcpersonNode.Name == "TypeId")
                {
                    if (hcpersonNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.HCPersonTypeId = hcpersonNode.InnerXml;
                        else
                            receiver.HCPersonTypeId = hcpersonNode.InnerXml;
                }
                // if (hcpersonNode.Name == "AdditionalId")
                // {
                //     ParseAdditionalId(hcpersonNode);
                // }
            }
        }

        public static void ParseDept(XmlNode deptNodes, String type)
        {
            foreach (XmlNode deptNode in deptNodes)
            {
                if (deptNode.Name == "Type")
                {
                    if (deptNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.DeptType = deptNode.InnerXml;
                        else
                            receiver.DeptType = deptNode.InnerXml;
                }
                if (deptNode.Name == "Name")
                {
                    if (type == "Sender")
                        sender.DeptName = deptNode.InnerXml;
                    else
                        receiver.DeptName = deptNode.InnerXml;
                }
                if (deptNode.Name == "Id")
                {
                    if (type == "Sender")
                        sender.DeptId = deptNode.InnerXml;
                    else
                        receiver.DeptId = deptNode.InnerXml;
                }
                if (deptNode.Name == "TypeId")
                {
                    if (deptNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.DeptTypeId = deptNode.Attributes["V"].Value;
                        else
                            receiver.DeptTypeId = deptNode.Attributes["V"].Value;
                }
            }
        }

        public static void ParseAddress(XmlNode addressNodes, String type)
        {
            foreach (XmlNode addressNode in addressNodes)
            {
                if (addressNode.Name == "Type")
                {
                    if (addressNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.AdrType = addressNode.Attributes["V"].Value;
                        else
                            receiver.AdrType = addressNode.Attributes["V"].Value;
                }
                if (addressNode.Name == "StreetAdr")
                {
                    if (type == "Sender")
                        sender.StreetAdr = addressNode.InnerXml;
                    else
                        receiver.StreetAdr = addressNode.InnerXml;
                }
                if (addressNode.Name == "PostalCode")
                {
                    if (type == "Sender")
                        sender.PostalCode = addressNode.InnerXml;
                    else
                        receiver.PostalCode = addressNode.InnerXml;
                }
                if (addressNode.Name == "City")
                {
                    if (type == "Sender")
                        sender.City = addressNode.InnerXml;
                    else
                        receiver.City = addressNode.InnerXml;
                }
                // TODO: should add county, country, CityDistr
                if (addressNode.Name == "TeleAddress")
                {
                    if (addressNode.Attributes["V"] != null)
                        if (type == "Sender")
                            sender.TeleAddress = addressNode.Attributes["V"].Value;
                        else
                            receiver.TeleAddress = addressNode.Attributes["V"].Value;
                }
            }
        }

        public static void ParseOriginalMsg(XmlNode originalMsgNodes)
        {
            foreach (XmlNode originalMsgNode in originalMsgNodes)
            {
                if (originalMsgNode.Name == "MsgType")
                {
                    // Console.WriteLine("Original message MsgType V: " + originalMsgNode.Attributes["V"].Value);
                    // if (originalMsgNode.Attributes["V"] != null)
                    //     Console.WriteLine("Original message MsgType DN: " + originalMsgNode.Attributes["DN"].Value);
                }
                if (originalMsgNode.Name == "Id")
                {
                    Console.WriteLine("Original message ID: " + originalMsgNode.InnerXml);
                }
                if (originalMsgNode.Name == "IssueDate")
                {
                    Console.WriteLine("Original message IssueDate: " + originalMsgNode.InnerXml);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using CheckListTool;
using System.Configuration;
using System.Linq;
using System.Windows.Forms.PropertyGridInternal;
using CheckListTool.Properties;
using Microsoft.Win32;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace CommitCheckList
{
    public class XmlManagerController
    {
        private static readonly string configPath = Directory.GetCurrentDirectory() + @"\ConfigFile.xml";
        private static readonly string checkListPathFromConfig = Settings.Default.CheckListPath;// GetCheckListPath();
        private static readonly string usageXmlPath = Settings.Default.UsersCommitsPath;//GetUsersCommitsPath();//checkListPathFromConfig + @"\Users.xml";
        private static readonly string checkListGroupXmlPath = Settings.Default.CheckListGroupPath;//GetCheckListGroupPath();//checkListPathFromConfig + @"\CheckListGroups";//@"R:\Users\idanp\Commit_CheckList\CheckListGroups";
        private static readonly string defaultCheckListXmlPath = checkListGroupXmlPath + @"\DefaultCheckList.xml";
        private static readonly string excelQuestionsXmlPath = checkListGroupXmlPath + @"\ExcelQuestions.xml";
        private static readonly string developresXmlPath = Settings.Default.DevelopersXmlPath;//Directory.GetCurrentDirectory() + @"\Developres.xml";

        public static void CreateNode(string UserId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(usageXmlPath);
            string UserIdElement = "UserId-" + UserId;
            //Create a new node and add it to the document.
            XmlNode elem = doc.CreateNode(XmlNodeType.Element, UserIdElement, null);
            elem.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            if (doc.DocumentElement != null)
            {
                doc.DocumentElement.AppendChild(elem);
            }
            doc.Save(usageXmlPath);
        }

        public static string GetDeveloperName()
        {
            string userName = String.Empty;
            var questList = new Dictionary<int, string>();
            XmlDocument xml = new XmlDocument();
            xml.Load(developresXmlPath);
            var xmlDevelopersCode = xml.SelectNodes("/Developers/Developer/Code1");
            foreach (var developerCode in xmlDevelopersCode)
            {
                var code1 = (developerCode as XmlNode);
                if (code1 != null && code1.InnerText.Equals(Environment.UserName))
                {
                    var userNameNode = (developerCode as XmlNode).NextSibling;
                    userName = userNameNode?.InnerText;
                }
            }

            xml.Save(developresXmlPath);
            return userName;
        }



        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return String.Empty;
        }

        public static Dictionary<int,string> GetExcelQuestions()
        {
            var questList = new Dictionary<int, string> ();
            XmlDocument xml = new XmlDocument();
            xml.Load(excelQuestionsXmlPath);
            var xmlExcelQuestons = xml.SelectNodes("/Questions/Question/QuestionDescription");
            foreach (var xmlExcelQueston in xmlExcelQuestons)
            {
                var columnNumber = (xmlExcelQueston as XmlNode)?.NextSibling;
                if (columnNumber != null)
                {

                    questList.Add((columnNumber.InnerText != String.Empty) ? int.Parse(columnNumber.InnerText) : 0, (xmlExcelQueston as XmlNode)?.InnerText);
                }
            }

            //if (xmlportalRun != null)
            //{
            //    quest = xmlportalRun.InnerText;
            //    if (quest != String.Empty)
            //        questList.Add(quest);
            //}
            xml.Save(excelQuestionsXmlPath);
            return questList;
        }

        public static List<string> GetQuestions()
        {
            var questList = new List<string>();
            foreach (var file in Directory.GetFiles(checkListGroupXmlPath).Where(f=>f!= excelQuestionsXmlPath))
            {
                string quest;
                XmlDocument xml = new XmlDocument();
                xml.Load(file);
                XmlNode xmlportalRun = xml.SelectSingleNode("/GroupChecks/Question");
                if (xmlportalRun != null)
                {
                    quest = xmlportalRun.InnerText;
                    if(quest != String.Empty)
                    questList.Add(quest);
                }
                xml.Save(file);
            }
            return questList;
        }
        public static Dictionary<string,bool> dictionaryChecks = new Dictionary<string, bool>();

        public static List<CheckModel> GetChecks()
        {
            var checkList = new List<CheckModel>();
            foreach (var file in Directory.GetFiles(checkListGroupXmlPath).Where(f => f != excelQuestionsXmlPath))
            {
                string quest = null;
                XmlDocument xml = new XmlDocument();
                xml.Load(file);
                XmlNode xmlportalRun = xml.SelectSingleNode("/GroupChecks/Question");
                if (xmlportalRun != null)
                {
                    quest = xmlportalRun.InnerText;
                }
                bool questIn;
                

                if ((dictionaryChecks.TryGetValue(quest, out questIn) && questIn) || (file == defaultCheckListXmlPath))
                {
                    var matchingElements = xml.SelectNodes("/GroupChecks/Checks/Check/CheckDescription");
                    //var excelColumnNumber = xml.SelectNodes("/GroupChecks/Checks/Check/ExcelColumnNumber");
                    if (matchingElements != null)
                    {
                        foreach (var matchingElement in matchingElements)
                        {
                            CheckModel check = new CheckModel();
                            if (matchingElement != null)
                            {
                                check.CheckDescription = (matchingElement as XmlNode)?.InnerText;
                                var nextSibling = (matchingElement as XmlNode)?.NextSibling;
                                if (nextSibling != null)
                                {
                                    check.CheckToolTip = nextSibling?.InnerText;
                                }
                                //var excelColumnNumberSibling = nextSibling?.NextSibling;
                                //if (excelColumnNumberSibling != null)
                                //{
                                //    check.ExcelColumnNumber = (excelColumnNumberSibling?.InnerText != String.Empty) ? int.Parse(excelColumnNumberSibling?.InnerText) : 0;
                                //}


                                if (check.CheckDescription != String.Empty)
                                {
                                    check.CheckFilePath = file;
                                    checkList.Add(check);
                                }
                            }
                        }
                    }
                }
                xml.Save(file);
            }
            return checkList;
        }

        //Get data Path
        public static string GetCheckListPath()
        {
            Logger.Log("Enter GetCheckListPath()  " + configPath);
            string checkListPath = String.Empty;
            XmlDocument xml = new XmlDocument();
            xml.Load(configPath);
            XmlNode xmlportalRun = xml.SelectSingleNode("/Config/CheckListPath");//.InnerText;
            if (xmlportalRun != null)
            {
                checkListPath = xmlportalRun.InnerText;
            }
            Logger.Log("Finish GetCheckListPath()");
            return checkListPath;
        }

        public static string GetCheckListGroupPath()
        {
            Logger.Log("Enter GetCheckListGroupPath()");
            string checkListGroupPath = String.Empty;
            XmlDocument xml = new XmlDocument();
            xml.Load(configPath);
            XmlNode xmlportalRun = xml.SelectSingleNode("/Config/CheckListGroupPath");//.InnerText;
            if (xmlportalRun != null)
            {
                checkListGroupPath = xmlportalRun.InnerText;
            }
            Logger.Log("Finish GetCheckListGroupPath()");
            return checkListGroupPath;
        }

        public static string GetUsersCommitsPath()
        {
            Logger.Log("Enter GetCheckListGroupPath()");
            string usersCommitsPath = String.Empty;
            XmlDocument xml = new XmlDocument();
            xml.Load(configPath);
            XmlNode xmlportalRun = xml.SelectSingleNode("/Config/UsersCommitsPath");//.InnerText;
            if (xmlportalRun != null)
            {
                usersCommitsPath = xmlportalRun.InnerText;
            }
            Logger.Log("Finish GetCheckListGroupPath()");
            return usersCommitsPath;
        }

        public static void SetCheckResult(CheckModel checkModel, CheckResult checkResult)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(checkModel.CheckFilePath);
            XmlNode xmlPortalRun = xml.SelectSingleNode("/GroupChecks/Checks/Check[CheckDescription ='" + checkModel.CheckDescription + "']/CoveredCount");
            switch (checkResult)
            {
                case CheckResult.AlreadyCovered:
                    xmlPortalRun = xml.SelectSingleNode("/GroupChecks/Checks/Check[CheckDescription ='" + checkModel.CheckDescription + "']/CoveredCount");
                    //if (checkModel.ExcelColumnNumber > 0)
                    //{
                    //    ExcelManagerController.OpenAndSet(checkModel.ExcelColumnNumber,"+");
                    //}
                    break;
                case CheckResult.Helpful:
                    xmlPortalRun = xml.SelectSingleNode("/GroupChecks/Checks/Check[CheckDescription ='" + checkModel.CheckDescription + "']/HelpfulCount");
                    //if (checkModel.ExcelColumnNumber > 0)
                    //{
                    //    ExcelManagerController.OpenAndSet(checkModel.ExcelColumnNumber, "+");
                    //}

                    break;
                case CheckResult.NotRelevant:
                    xmlPortalRun = xml.SelectSingleNode("/GroupChecks/Checks/Check[CheckDescription ='" + checkModel.CheckDescription + "']/NotRelevantCount");
                    //if (checkModel.ExcelColumnNumber > 0)
                    //{
                    //    ExcelManagerController.OpenAndSet(checkModel.ExcelColumnNumber, "-");
                    //}

                    break;
            }
            if (xmlPortalRun != null)
                xmlPortalRun.InnerText = (int.Parse(xmlPortalRun.InnerText) + 1).ToString();
            xml.Save(checkModel.CheckFilePath);
        }
    }
}

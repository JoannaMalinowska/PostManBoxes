using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PostManBoxes.Helpers
{
    public static class Logger
    {
        public static void CreateLog(string apiURL, string method, string status)
        {
            try
            {
                string logFilePath = ParametersRepo.LogFilePathName;

                StreamWriter sw = new StreamWriter(logFilePath, true);
                StringBuilder sb = new StringBuilder();

                sb.Append(IP.GetIPAddress())
                    .Append(" ")
                    .Append("[" + DateTime.UtcNow.ToLongDateString() +" "+DateTime.UtcNow.ToLongTimeString()+ "]")
                    .Append(" ")
                    .Append(method.ToUpper())
                    .Append(" ")
                    .Append(apiURL)
                    .Append(" ")
                    .Append(status);
                sw.WriteLine(sb.ToString());

                sw.Flush();
                sw.Close();
            }
            catch ( Exception ex)
            {

              
            }
            

        }


        public static void ErrorLog(string sPathName, string sErrMsg)
        {
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            string sErrorTime = sYear + sMonth + sDay;

            StreamWriter sw = new StreamWriter(sPathName + sErrorTime, true);
            sw.WriteLine(DateTime.Now.ToLongDateString()+"===>"+sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }
}
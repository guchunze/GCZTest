using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utility
{
    public class Utils
    {
        /// <summary>
        /// 增加错误日志
        /// </summary>
        /// <param name="errorMsg"></param>
        public static void ErrorLog(string errorMsg)
        {
            ErrorLog(errorMsg, DateTime.Now.ToString("yyyy-MM-dd") + ".txt",//+ new Random().Next(0x3e8, 0x270f)
                HttpContext.Current.Request.ApplicationPath + "/ErrorLog/");
        }

        /// <summary>
        /// 增加错误日志
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <param name="fileName"></param>
        /// <param name="savePath"></param>
        public static void ErrorLog(string errorMsg, string fileName, string savePath)
        {
            string str = "未获取到来源页面";
            HttpContext current = HttpContext.Current;
            string strOperatorName = string.Empty;

            //if (current.Session["OperatorName"] != null)
            //{
            //    strOperatorName = current.Session["OperatorName"].ToString();
            //}
            //else
            //{
            //    strOperatorName = "Session/OperatorName 失效";
            //}

            int iQuery = current.Request.Params.Count;
            string strQuery = string.Empty;

            strQuery = strQuery + current.Request.Params.ToString();

            if (HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] != null)
            {
                str = current.Request.ServerVariables["HTTP_REFERER"].ToString();
            }
            string path = current.Server.MapPath(savePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StreamWriter writer = new StreamWriter(path + fileName, true, Encoding.UTF8);
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n\r\n");
            builder.Append("用户名：\t\t\t" + strOperatorName);
            //builder.Append("\r\n\r\n");
            //builder.Append("输入参数：\t\t\t" + strQuery);
            builder.Append("\r\n\r\n");
            builder.Append("日志时间：\t\t\t" + DateTime.Now.ToString("yyyy-MM-dd HH:mm ss"));
            builder.Append("\r\n\r\n");
            builder.Append("请求来源：\t\t\t" + str);
            builder.Append("\r\n\r\n");
            builder.Append("IP地址：\t\t\t" + GetClientIP());
            builder.Append("\r\n\r\n");
            builder.Append("错误页面：\t\t\t" + current.Request.Url.ToString());
            builder.Append("\r\n\r\n");
            builder.Append("日志内容：");
            builder.Append("\r\n\r\n");
            builder.Append(errorMsg);
            builder.Append("\r\n\r\n");
            builder.Append("=========================================================");
            writer.Write(builder);
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// 得到客户IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            HttpContext current = HttpContext.Current;
            string str = "";
            if (current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                return current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            if ((str == "") && (current.Request.ServerVariables["REMOTE_ADDR"] != null))
            {
                return current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return current.Request.UserHostAddress;
        }

    }
}

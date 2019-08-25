using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Utility
{
    public class XmlHepler
    {
        /// <summary>
        /// 获取xml内容
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string xmlload(string name)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(HttpContext.Current.Server.MapPath("~/Config/Configs.xml"));
            XmlNodeList xmlNodes = xml.SelectNodes("/configuration/category");
            string datasoure = "";
            foreach (XmlNode xxNode in xmlNodes)
            {
                XmlNodeList lls = xxNode.ChildNodes;
                for (int i = 0; i < lls.Count; i++)
                {
                    if (lls.Item(i).Attributes[1].InnerText == name)
                    {
                        datasoure = lls.Item(i).Attributes[2].InnerText;
                    }
                }
            }
            return datasoure;
        }
    }
}

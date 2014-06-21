using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace Main.com.lk.util
{
    class Utils
    {
        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValue"></param>
        public static void EditConfig(string key, string keyValue)//编辑参数
        {
            XmlDocument xmlDoc = new XmlDocument();
            string configPath = System.Windows.Forms.Application.ExecutablePath + ".config";
            xmlDoc.Load(configPath);
            XmlNode xmlNode = xmlDoc.SelectSingleNode("configuration/appSettings/add[@key='" + key + "']");
            xmlNode.Attributes["value"].InnerText = keyValue;
            xmlDoc.Save(configPath);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}

using System;
using System.Windows.Forms;
using NLog;
using System.Xml.Serialization;
using DataCollector.Entities;
using System.IO;
using DataCollector.Services.BLL;

namespace DataCollector
{
    static class Program
    {
        const string XML_CONFIG_APP_FILE_NAME = "ConfigApp.xml";
        const string XML_SQL_CONNECT_FILE_NAME = "PgSQLConnectionSettings.xml";
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var connectSettings = new PgSQLConnectionSettings();
            var configApp = new ConfigApp();
            var nlog = LogManager.GetCurrentClassLogger();

            try
            {
                var config = DeserializeXML<ConfigApp>(XML_CONFIG_APP_FILE_NAME);

                configApp.AutoConnectionDataBase = config.AutoConnectionDataBase;
                configApp.IpAddress = config.IpAddress;
                configApp.PortAddress = config.PortAddress;
                configApp.CheckTime = config.CheckTime;
            }

            catch(Exception e)
            {
                nlog.Error(e.Message);
               // MessageBox.Show(e.Message, "Системная ошибка, обратитесь к системному администратору", MessageBoxButtons.OK, MessageBoxIcon.Error);

                configApp.AutoConnectionDataBase = false;
                configApp.IpAddress = "";
                configApp.PortAddress = 9600;
                configApp.CheckTime = 20;
                var xmlSer = new XmlSerializer(typeof(ConfigApp));

                using (var stream = new FileStream(XML_CONFIG_APP_FILE_NAME, FileMode.Create))
                {
                    xmlSer.Serialize(stream, configApp);
                }
            }

            try
            {
                var set = DeserializeXML<PgSQLConnectionSettings>(XML_SQL_CONNECT_FILE_NAME);
                connectSettings.HostName = set.HostName;
                connectSettings.PortAddress = set.PortAddress;
                connectSettings.DataBaseName = set.DataBaseName;
                connectSettings.UserName = set.UserName;
                connectSettings.Password = set.Password;

            }

            catch (Exception e)
            {
                nlog.Error(e.Message);
                //MessageBox.Show(e.Message, "Системная ошибка, обратитесь к системному администратору", MessageBoxButtons.OK, MessageBoxIcon.Error);

                connectSettings.HostName = "";
                connectSettings.PortAddress = "";
                connectSettings.DataBaseName = "";
                connectSettings.UserName = "";
                connectSettings.Password = "";

                var xmlSer = new XmlSerializer(typeof(PgSQLConnectionSettings));

                using (var stream = new FileStream(XML_SQL_CONNECT_FILE_NAME, FileMode.Create))
                {
                    xmlSer.Serialize(stream, connectSettings);
                }

            }

            finally
            {
                Application.Run(new MainForm(configApp,connectSettings,nlog));
            }
            
        }


        public static T DeserializeXML<T>(string XMLFileName)
        {
            var xml = new XmlSerializer(typeof(T));

            using (var stream = new FileStream(XMLFileName, FileMode.OpenOrCreate))
            {
                return (T)xml.Deserialize(stream);
            }

        }


    }
}


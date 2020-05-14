using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using DataCollector.Entities;
using System.IO;
using System.Xml.Serialization;
using System.Net.NetworkInformation;

namespace DataCollector
{
    public partial class PLCSettingsForm : Form
    {
        const string XML_CONFIG_APP_FILE_NAME = "ConfigApp.xml";

        private Logger _nlog = null;
        private ConfigApp _configApp = null;

        public PLCSettingsForm(Logger nlog, ConfigApp configApp)
        {
            InitializeComponent();
            _nlog = nlog ?? throw new ArgumentNullException(nameof(nlog));
            _configApp = configApp ?? throw new ArgumentNullException(nameof(configApp));
        }

        private void PLCSettingsForm_Load(object sender, EventArgs e)
        {
           
            try
            {
                _configApp = DeserializeXML(_nlog);
                AutoConnectCheckBox.Checked = _configApp.AutoConnectionDataBase;
                IpTextBox.Text = _configApp.IpAddress;
                PortTextBox.Text = Convert.ToString(_configApp.PortAddress);
                CycleTextBox.Text = Convert.ToString(_configApp.CheckTime);
            }
            catch (Exception err)
            {
                _nlog.Error(err.Message);
                MessageBox.Show("Не удалось прочитать настройки подключения","", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
           
            if (!PingPLC(IpTextBox.Text))
            {
                MessageBox.Show("Отсутствует Ping устройства! Устройство не найдено.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Устройство найдено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Вы действительно хотите сохранить изменения в подключении к ПЛК?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            if (dialog == DialogResult.OK)
            {
                try
                {
                    var config = new ConfigApp()
                    {
                        AutoConnectionDataBase = AutoConnectCheckBox.Checked,
                        IpAddress = IpTextBox.Text,
                        PortAddress = Convert.ToInt16(PortTextBox.Text),
                        CheckTime = Convert.ToInt16(CycleTextBox.Text)
                    };
                    var xmlSer = new XmlSerializer(typeof(ConfigApp));

                    using (var stream = new FileStream(XML_CONFIG_APP_FILE_NAME, FileMode.Create))
                    {
                        xmlSer.Serialize(stream, config);
                    }
                    this.Close();
                    MessageBox.Show("Перезапустите приложения для инициализации подключения к ПЛК", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    _nlog.Error(err.Message);
                    MessageBox.Show("Не удалось сохранить изменения. Проверьте ввод данных и повторите попытку.","", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

            if (dialog == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private ConfigApp DeserializeXML(Logger nlog)
        {
            var xml = new XmlSerializer(typeof(ConfigApp));
            Stream stream = null;
            try
            {
                stream = new FileStream(XML_CONFIG_APP_FILE_NAME, FileMode.Open);
                return (ConfigApp)xml.Deserialize(stream);

            }

            catch (Exception err)
            {
                nlog.Error(err.Message);
                return null;
            }

            finally
            {
                stream?.Dispose();
            }

        }

        private bool PingPLC(string ip)
        {
            Ping ping = new Ping();
            PingReply rep;
            rep = ping.Send(ip, 1000);
            if (rep.Status == IPStatus.Success)//проверка статуса пинга
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}

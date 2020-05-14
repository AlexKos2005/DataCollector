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
using System.Xml.Serialization;
using System.IO;
using System.Net.NetworkInformation;
using DataCollector.Servises.DAL;

namespace DataCollector
{
    public partial class SQLSettingsForm : Form
    {
        const string XML_SQL_CONNECT_FILE_NAME = "PgSQLConnectionSettings.xml";

        private Logger _nlog = null;
        private PgSQLConnectionSettings _pgSQLConnect = null;

        public SQLSettingsForm(Logger nlog, PgSQLConnectionSettings pgSQLConnect)
        {
            InitializeComponent();
            _nlog = nlog ?? throw new ArgumentNullException(nameof(nlog));
            _pgSQLConnect = pgSQLConnect ?? throw new ArgumentNullException(nameof(pgSQLConnect));
        }

        private void SQLSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                _pgSQLConnect = DeserializeXML(_nlog);
                HostTextBox.Text = _pgSQLConnect.HostName;
                PortTextBox.Text = _pgSQLConnect.PortAddress;
                NameDbTextBox.Text = _pgSQLConnect.DataBaseName;
                LoginTextBox.Text = _pgSQLConnect.UserName;
                PasswordTextBox.Text = _pgSQLConnect.Password;
            }
            catch (Exception err)
            {
                _nlog.Error(err.Message);
                MessageBox.Show(err.Message, "Не удалось прочитать настройки подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            var pgSQL = new PostgresDataAccess(_pgSQLConnect);
            var result = pgSQL.CheckConnectionWithResult();

            if (result.IsError)
            {
                MessageBox.Show("Отсутствует подключение к БД! Проверьте данные подключения.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Подключение к БД установлено!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Вы действительно хотите сохранить изменения в подключении к БД?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            if (dialog == DialogResult.OK)
            {
                try
                {
                    var pgSQLConnect = new PgSQLConnectionSettings()
                    {
                     HostName= HostTextBox.Text,
                    PortAddress = PortTextBox.Text,
                    DataBaseName= NameDbTextBox.Text,
                    UserName= LoginTextBox.Text,
                    Password= PasswordTextBox.Text
                    };

                    var xmlSer = new XmlSerializer(typeof(PgSQLConnectionSettings));

                    using (var stream = new FileStream(XML_SQL_CONNECT_FILE_NAME, FileMode.Create))
                    {
                        xmlSer.Serialize(stream, pgSQLConnect);
                    }
                    this.Close();
                    MessageBox.Show("Перезапустите приложения для инициализации подключения к БД", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    _nlog.Error(err.Message);
                    MessageBox.Show(err.Message, "Не удалось сохранить изменения, повторите попытку.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private PgSQLConnectionSettings DeserializeXML(Logger nlog)
        {
            var xml = new XmlSerializer(typeof(PgSQLConnectionSettings));
            Stream stream = null;
            try
            {
                stream = new FileStream(XML_SQL_CONNECT_FILE_NAME, FileMode.Open);
                return (PgSQLConnectionSettings)xml.Deserialize(stream);

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

    }
}

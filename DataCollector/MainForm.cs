using System.Windows.Forms;
using DataCollector.Entities;
using NLog;
using DataCollector.Services.BLL;
using DataCollector.Servises.DAL;
using System;
using System.Threading.Tasks;

namespace DataCollector
{
    public partial class MainForm : Form
    {
        private ConfigApp _configApp = null;
        private PgSQLConnectionSettings _connectSettings = null;
        private Logger _nlog = null;
        private Timer _timer = null;

        public MainForm(ConfigApp configApp, PgSQLConnectionSettings connectSettings, Logger nlog)
        {
            InitializeComponent();
            _configApp = configApp;
            _connectSettings = connectSettings;
            _nlog = nlog;
            ErrorPLCLabel.Text = "";
            ErrorSQLLabel.Text = "";

            _timer = new Timer();

            if (_configApp.CheckTime==0 || _configApp.CheckTime<=10) _timer.Interval = 30000;
            else _timer.Interval = _configApp.CheckTime * 1000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void _timer_Tick(object sender, System.EventArgs e)
        {
            if (_configApp.AutoConnectionDataBase)
            {
                var plc = new OmronPLCDataService(_configApp);
                var resultPLC = plc.GetData();
                if (resultPLC.IsError)
                {
                    _nlog.Error(resultPLC.ErrorMessage);
                    ErrorPLCLabel.Text = $"Последняя попытка подключения к PLC закончлась неудачей {DateTime.Now.ToShortTimeString()}";
                }
                ErrorPLCLabel.Text = $"Подключение к ПЛК Ок! {DateTime.Now.ToShortTimeString()}";

                if (resultPLC.Datas != null && resultPLC.Datas.Count>0)
                {
                    var resultSQL = new PostgresDataAccess(_connectSettings);
                    var resSetSQL = resultSQL.SetDosesWithResult(resultPLC.Datas);
                    if (resSetSQL.IsError)
                    {
                        _nlog.Error(resSetSQL.ErrorMessage);
                        ErrorSQLLabel.Text = $"Последняя попытка записи в БД закончилась неудачей {DateTime.Now.ToShortTimeString()}";
                    }
                    else
                    {
                        ErrorSQLLabel.Text = $"Подключение к БД Ок! {DateTime.Now.ToShortTimeString()}";
                    }
                }
                
            }
        }

        private void MakeReportButton_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                string path = folderBrowserDialog1.SelectedPath;
   
               
                var pgSQL = new PostgresDataAccess(_connectSettings);
                var result = pgSQL.GetDosesByDatesWithResult(DateStartTimePicker.Value, DateEndTimePicker.Value);

                if (result.IsError)
                {
                    _nlog.Error(result.ErrorMessage);
                    MessageBox.Show("Не удалось получить данные из БД","", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var report = new ExcelReportService(path);
               var reportResults = report.MakeReport(result.Values);
                if (reportResults.IsError)
                {
                    _nlog.Error(reportResults.ErrorMessage);
                    MessageBox.Show("Не удалось сформировать отчет!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    MessageBox.Show("Отчет успешно сформирован!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else
            {
                MessageBox.Show("Путь не выбран");

            }
           
        }

        private void ConnectToPLCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var plc_form = new PLCSettingsForm(_nlog,_configApp);
            plc_form.Show();
        }

        private void ConnectDbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var db_form = new SQLSettingsForm(_nlog, _connectSettings);
            db_form.Show();
        }

        //private static async Task GetPLCData()
        //{
        //    var plc = new OmronPLCDataService(_configApp.AutoConnectionDataBase);
        //    await Task.Run(() =>
        //    {
                
        //    }
        //   );
            
        //}

        //private void Method()
        //{
        //    if (_configApp.AutoConnectionDataBase)
        //    {
        //        var plc = new OmronPLCDataService(_configApp);
        //        var resultPLC = plc.GetData();
        //        if (resultPLC.IsError)
        //        {
        //            _nlog.Error(resultPLC.ErrorMessage);
        //            ErrorPLCLabel.Text = $"Последняя попытка подключения к PLC закончлась неудачей {DateTime.Now.ToShortTimeString()}";
        //        }
        //        ErrorPLCLabel.Text = $"Подключение Ок {DateTime.Now.ToShortTimeString()}";

        //        var resultSQL = new PostgresDataAccess(_connectSettings);
        //        var resSetSQL = resultSQL.SetDosesWithResult(resultPLC.Datas);
        //        if (resSetSQL.IsError)
        //        {
        //            _nlog.Error(resSetSQL.ErrorMessage);
        //            ErrorSQLLabel.Text = $"Последняя попытка записи в БД закончилась неудачей {DateTime.Now.ToShortTimeString()}";
        //        }
        //        ErrorSQLLabel.Text = $"Подключение к БД Ок {DateTime.Now.ToShortTimeString()}";
        //    }
        //}
    }

   
}

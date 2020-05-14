using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.Entities;
using Npgsql;

namespace DataCollector.Servises.DAL
{
    interface IDataAccessService
    {
        (bool IsError, string ErrorMessage, List<Dose> Values) GetDosesByDatesWithResult(DateTime startDate, DateTime endDate);
        (bool IsError, string ErrorMessage) CheckConnectionWithResult();
        (bool IsError, string ErrorMessage) SetDosesWithResult(List<Dose> Values);
    }

    public class PostgresDataAccess : IDataAccessService
    {
        private PgSQLConnectionSettings _pgSQLConnectionSettings = null;
        private PostgresDataContext db = null;

        public PostgresDataAccess(PgSQLConnectionSettings pgSQLConnectionSettings)
        {
            _pgSQLConnectionSettings = pgSQLConnectionSettings;
           
        }

        public (bool IsError, string ErrorMessage, List<Dose> Values) GetDosesByDatesWithResult(DateTime startDate, DateTime endDate)
        {
            try
            {
                //db = new PostgresDataContext(_pgSQLConnectionSettings);
                //var result = db.Doses.Where(p => p.Date >= startDate && p.Date <= endDate).ToList();
                var stDate = startDate.ToString("yyyy-MM-dd").Replace('.','/') + " " + "00:00:00";
                var enDate = endDate.ToString("yyyy-MM-dd").Replace('.', '/') + " " + "00:00:00";
                var result = new List<Dose>();
                //NpgsqlConnection conn = new NpgsqlConnection("Server=10.87.20.183;Port=5432;UserId=apcs_admin;Password=qwerty;Database=APCS1;");
                NpgsqlConnection conn = new NpgsqlConnection($"Host={_pgSQLConnectionSettings.HostName};Port={_pgSQLConnectionSettings.PortAddress};Database={_pgSQLConnectionSettings.DataBaseName};Username={_pgSQLConnectionSettings.UserName};Password={_pgSQLConnectionSettings.Password}");
                conn.Open();

                //NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.doses WHERE doses.datedose > '27/04/2020 00:00'", conn);
                // NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.doses WHERE doses.datedose BETWEEN '{stDate}' AND '{enDate}'", conn);
                NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.doses WHERE doses.datedose BETWEEN '{stDate}' AND '{enDate}' ORDER BY  doses.datedose, doses.timedose", conn);
                // Execute a query
                NpgsqlDataReader dr = cmd.ExecuteReader();


                // Read all rows and output the first column in each row
                while (dr.Read())
                {
                    var obj = new object[8];
                    var dose = new Dose();
                    dr.GetValues(obj);
                    dose.id = Convert.ToInt32(obj[0]);
                    dose.datedose = Convert.ToDateTime(obj[1]);
                    var dt = obj[2];
                    dose.timedose = Convert.ToDateTime(dt.ToString()).TimeOfDay;
                    dose.dosiernumber = Convert.ToInt32(obj[3]);
                    dose.componentnumber = Convert.ToInt32(obj[4]);
                    dose.setweight = Convert.ToInt32(obj[5]);
                    dose.realweight = Convert.ToInt32(obj[6]);
                    dose.errorweight = Convert.ToInt32(obj[7]);
                    result.Add(dose);
                }

                conn.Close();



                return (false, "", result);
            }

            catch (Exception err)
            {
                return (true, err.Message, null);
            }
            finally
            {
                db?.Dispose();
            }

        }

        public (bool IsError, string ErrorMessage) CheckConnectionWithResult()
        {

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection($"Host={_pgSQLConnectionSettings.HostName};Port={_pgSQLConnectionSettings.PortAddress};Database={_pgSQLConnectionSettings.DataBaseName};Username={_pgSQLConnectionSettings.UserName};Password={_pgSQLConnectionSettings.Password}");
                conn.Open();
                conn.Close();
                return (false, "");
            }

            catch (Exception err)
            {
                return (true, err.Message);
            }
            finally
            {
                db?.Dispose();
            }

        }

        public (bool IsError, string ErrorMessage) SetDosesWithResult(List<Dose> Values)
        {
           
           
            try
            {
                db = new PostgresDataContext(_pgSQLConnectionSettings);
                db.doses.AddRange(Values);
                db.SaveChanges();
                return (false, "");
            }

            catch (Exception err)
            {
                return (true, err.Message);
            }
            finally
            {
                db?.Dispose();
            }
        }

      
    }
}

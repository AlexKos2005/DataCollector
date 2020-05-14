using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Entities
{
   public class PgSQLConnectionSettings
    {
       
        private string _hostName = null;
        private string _portAddress = null;
        private string _dataBaseName = null;
        private string _userName = null;
        private string _password = null;

        public string HostName { get => _hostName; set => _hostName = value; }
        public string PortAddress { get => _portAddress; set => _portAddress = value; }
        public string DataBaseName { get => _dataBaseName; set => _dataBaseName = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }

        public PgSQLConnectionSettings()
        {

        }

        public PgSQLConnectionSettings (string hostName, string portAddress,string dataBaseName,string userName,string password)
        {
            _hostName = hostName;
            _portAddress = portAddress;
            _dataBaseName = dataBaseName;
            _userName = userName;
            _password = password;
        }

        
    }
}

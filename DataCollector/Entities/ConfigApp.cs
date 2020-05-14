using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Entities
{
   public class ConfigApp
    {
        private bool _autoConnectionDataBase = false;
        private string _ipAddres = null;
        private short _portAddress = 0;
        private int _checkTime = 0;

        public bool AutoConnectionDataBase { get => _autoConnectionDataBase; set => _autoConnectionDataBase = value; }
        public string IpAddress { get => _ipAddres; set => _ipAddres = value; }
        public short PortAddress { get => _portAddress; set => _portAddress = value; }
        public int CheckTime { get => _checkTime; set => _checkTime = value; }

    }
}

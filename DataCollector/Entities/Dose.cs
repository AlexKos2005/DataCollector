using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Entities
{
    public class Dose
    {
        public int id { get; set; }
        public DateTime datedose { get; set; }
        public TimeSpan timedose { get; set; }
        public int dosiernumber { get; set; }
        public int componentnumber { get; set; }
        public int setweight { get; set; }
        public int realweight { get; set; }
        public int errorweight { get; set; }
    }
}

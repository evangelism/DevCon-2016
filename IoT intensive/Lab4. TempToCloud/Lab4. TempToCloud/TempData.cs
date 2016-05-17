using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.TempToCloud
{
    public class TempData
    {
        public string Id { get; set; }
        public double Temperature { get; set; }
        public DateTime Time { get; set; }

        public int Table { get; set; }

        public int No { get; set; }
        public TempData(string id, double Temp, int Table, int No)
        {
            Time = DateTime.Now;
            Temperature = Temp;
            Id = id;
            this.Table = Table;
            this.No = No;
        }
    }
}

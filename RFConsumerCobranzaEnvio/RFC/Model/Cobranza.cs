using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC.Model
{
    public class Cobranza
    {
        private string incob;
        private string kunnr;
        private string bukrs;
        private string belnr;
        private int gjahr;
        private int buzei;
        private DateTime budat;
        private string zuonr;
        private string xblnr;
        private string blart;
        private string shkzg;
        private string waers;
        private decimal dmbtr;
        private decimal wrbtr;
        private DateTime zfbdt;
        private DateTime dats;
        private string uzeit;

        public string Kunnr { get => kunnr; set => kunnr = value; }
        public string Bukrs { get => bukrs; set => bukrs = value; }
        public string Belnr { get => belnr; set => belnr = value; }
        public int Gjahr { get => gjahr; set => gjahr = value; }
        public int Buzei { get => buzei; set => buzei = value; }
        public DateTime Budat { get => budat; set => budat = value; }
        public string Zuonr { get => zuonr; set => zuonr = value; }
        public string Xblnr { get => xblnr; set => xblnr = value; }
        public string Blart { get => blart; set => blart = value; }
        public string Shkzg { get => shkzg; set => shkzg = value; }
        public string Waers { get => waers; set => waers = value; }
        public decimal Dmbtr { get => dmbtr; set => dmbtr = value; }
        public decimal Wrbtr { get => wrbtr; set => wrbtr = value; }
        public DateTime Zfbdt { get => zfbdt; set => zfbdt = value; }
        public DateTime Dats { get => dats; set => dats = value; }
        public string Uzeit { get => uzeit; set => uzeit = value; }
    }
}

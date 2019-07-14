using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC.Model
{
    public class Pedido
    {
        private string bstkd;
        private string kunnr;
        private DateTime audat;

        private string zterm;
        private string auart;
        private string waerk;
        private decimal netwr;
        private string matnr;
        private decimal kwmeng;
        private decimal kbetr;
        private decimal netpr;
        private string pstyv;
        private string vbeln;

        public Pedido()
        {
        }

        public string Bstkd { get => bstkd; set => bstkd = value; }
        public string Kunnr { get => kunnr; set => kunnr = value; }
        public DateTime Audat { get => audat; set => audat = value; }
        public string Zterm { get => zterm; set => zterm = value; }
        public string Auart { get => auart; set => auart = value; }
        public string Waerk { get => waerk; set => waerk = value; }
        public decimal Netwr { get => netwr; set => netwr = value; }
        public string Matnr { get => matnr; set => matnr = value; }
        public decimal Kwmeng { get => kwmeng; set => kwmeng = value; }
        public decimal Kbetr { get => kbetr; set => kbetr = value; }
        public decimal Netpr { get => netpr; set => netpr = value; }
        public string Pstyv { get => pstyv; set => pstyv = value; }
        public string Vbeln { get => vbeln; set => vbeln = value; }
    }
}

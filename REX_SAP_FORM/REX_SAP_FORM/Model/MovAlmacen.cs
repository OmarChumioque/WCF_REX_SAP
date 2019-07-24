using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC.Model
{
    public class MovAlmacen
    {

        private string docejem;
        private string werks;
        private string lgort;
        private decimal labst;
        private string matnr;
        private string meins;
        private string date;
        private string tip;

        public MovAlmacen()
        {
            Docejem = "";
            Werks = "";
            Lgort = "";
            Labst = 0;
            Matnr = "";
            Meins = "";
            Date = "";
            Tip = "";

        }

        public string Docejem { get => docejem; set => docejem = value; }
        public string Werks { get => werks; set => werks = value; }
        public string Lgort { get => lgort; set => lgort = value; }
        public decimal Labst { get => labst; set => labst = value; }
        public string Matnr { get => matnr; set => matnr = value; }
        public string Meins { get => meins; set => meins = value; }
        public string Date { get => date; set => date = value; }
        public string Tip { get => tip; set => tip = value; }



    }


}

using RFC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFConsumer
{
    public partial class FrmPresentacion : Form
    {
        RfcProcesos rfc;
        public FrmPresentacion()
        {
            InitializeComponent();
        
        }

        private void BtnPedidos_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MessageBox.Show(rfc.ProcesarPedidos(dateTimePicker1.Value.Date), "Confirmación proceso");
            Cursor.Current = Cursors.Default;
        }

        private void BtnRecibirStock_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MessageBox.Show(rfc.ProcesarDatosStock(), "Confirmación proceso");
            Cursor.Current = Cursors.Default;

        }

        private void BtnRecibirCobranza_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MessageBox.Show(rfc.ProcesarCobranzasRecepcion(), "Confirmación proceso");
            Cursor.Current = Cursors.Default;
        }

        private void BtnEnviarCobranza_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MessageBox.Show(rfc.ProcesarCobranzasEnvio(), "Confirmación proceso");
            Cursor.Current = Cursors.Default;
        }

        private void BtnRecibirDatosAlmacen_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MessageBox.Show(rfc.ProcesarMovimientosAlmacen(dateTimePicker1.Value), "Confirmación proceso");
            Cursor.Current = Cursors.Default;
        }

        private void FrmPresentacion_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            rfc = new RfcProcesos();
            Cursor.Current = Cursors.Default;
        }
    }
}

namespace RFConsumer
{
    partial class FrmPresentacion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPedidos = new System.Windows.Forms.Button();
            this.btnRecibirStock = new System.Windows.Forms.Button();
            this.btnRecibirCobranza = new System.Windows.Forms.Button();
            this.btnEnviarCobranza = new System.Windows.Forms.Button();
            this.btnRecibirDatosAlmacen = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnPedidos
            // 
            this.btnPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPedidos.Location = new System.Drawing.Point(274, 68);
            this.btnPedidos.Name = "btnPedidos";
            this.btnPedidos.Size = new System.Drawing.Size(212, 45);
            this.btnPedidos.TabIndex = 0;
            this.btnPedidos.Text = "Enviar pedidos";
            this.btnPedidos.UseVisualStyleBackColor = true;
            this.btnPedidos.Click += new System.EventHandler(this.BtnPedidos_Click);
            // 
            // btnRecibirStock
            // 
            this.btnRecibirStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecibirStock.Location = new System.Drawing.Point(273, 15);
            this.btnRecibirStock.Name = "btnRecibirStock";
            this.btnRecibirStock.Size = new System.Drawing.Size(213, 45);
            this.btnRecibirStock.TabIndex = 1;
            this.btnRecibirStock.Text = "Recibir Stock";
            this.btnRecibirStock.UseVisualStyleBackColor = true;
            this.btnRecibirStock.Click += new System.EventHandler(this.BtnRecibirStock_Click);
            // 
            // btnRecibirCobranza
            // 
            this.btnRecibirCobranza.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecibirCobranza.Location = new System.Drawing.Point(41, 13);
            this.btnRecibirCobranza.Name = "btnRecibirCobranza";
            this.btnRecibirCobranza.Size = new System.Drawing.Size(212, 47);
            this.btnRecibirCobranza.TabIndex = 2;
            this.btnRecibirCobranza.Text = "Recibir datos cobranza";
            this.btnRecibirCobranza.UseVisualStyleBackColor = true;
            this.btnRecibirCobranza.Click += new System.EventHandler(this.BtnRecibirCobranza_Click);
            // 
            // btnEnviarCobranza
            // 
            this.btnEnviarCobranza.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarCobranza.Location = new System.Drawing.Point(41, 66);
            this.btnEnviarCobranza.Name = "btnEnviarCobranza";
            this.btnEnviarCobranza.Size = new System.Drawing.Size(213, 47);
            this.btnEnviarCobranza.TabIndex = 3;
            this.btnEnviarCobranza.Text = "Enviar datos cobranza";
            this.btnEnviarCobranza.UseVisualStyleBackColor = true;
            this.btnEnviarCobranza.Click += new System.EventHandler(this.BtnEnviarCobranza_Click);
            // 
            // btnRecibirDatosAlmacen
            // 
            this.btnRecibirDatosAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecibirDatosAlmacen.Location = new System.Drawing.Point(42, 119);
            this.btnRecibirDatosAlmacen.Name = "btnRecibirDatosAlmacen";
            this.btnRecibirDatosAlmacen.Size = new System.Drawing.Size(212, 48);
            this.btnRecibirDatosAlmacen.TabIndex = 4;
            this.btnRecibirDatosAlmacen.Text = "Recibir datos almacen";
            this.btnRecibirDatosAlmacen.UseVisualStyleBackColor = true;
            this.btnRecibirDatosAlmacen.Click += new System.EventHandler(this.BtnRecibirDatosAlmacen_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(274, 132);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(212, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // FrmPresentacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 239);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnRecibirDatosAlmacen);
            this.Controls.Add(this.btnEnviarCobranza);
            this.Controls.Add(this.btnRecibirCobranza);
            this.Controls.Add(this.btnRecibirStock);
            this.Controls.Add(this.btnPedidos);
            this.Name = "FrmPresentacion";
            this.Text = "REX SAP";
            this.Load += new System.EventHandler(this.FrmPresentacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPedidos;
        private System.Windows.Forms.Button btnRecibirStock;
        private System.Windows.Forms.Button btnRecibirCobranza;
        private System.Windows.Forms.Button btnEnviarCobranza;
        private System.Windows.Forms.Button btnRecibirDatosAlmacen;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}
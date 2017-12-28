using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sli1
{
    public partial class FrmMenuPpal : Form
    {
        private FrmIngresoTag frm;

        public FrmMenuPpal()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            frm = new FrmIngresoTag();
            frm.Show();
            this.Hide();
        }
    }
}
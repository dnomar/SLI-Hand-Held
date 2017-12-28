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
    public partial class frm_ingreso : Form
    {
        public frm_ingreso()
        {
            InitializeComponent();
        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMenuPpal frm = new FrmMenuPpal();
            frm.Show();
            this.Hide();
        }

        private void frm_ingreso_Load(object sender, EventArgs e)
        {

        }
    }
}
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
    public partial class FrmIngresoTag : Form
    {
        public FrmIngresoTag()
        {
            InitializeComponent();
        }

        private void FrmIngresoTag_Load(object sender, EventArgs e)
        {

        }

        private void label3_ParentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //ESta informacion deberia venir de un archivo de configuracion
            //flagAnti = 0x01;
            //iniAntiQ = (byte)int.Parse(this.cbqvalue.Text);
            Tag rfidTag = new Tag(0x01, (byte)int.Parse("3"));
            rfidTag.Read(true);
        }
    }
}
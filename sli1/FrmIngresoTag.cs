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
        private RfidTag rfidTag;
        private FrmMenuPpal frm2;

        public FrmIngresoTag()
        {
            InitializeComponent();
            connection();
            init();
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
            //this.rfidTag = new RfidTag(0x01, (byte)int.Parse("3"));
            this.rfidTag = new RfidTag(this);
            rfidTag.singleRead();
        }

        //bool isCon = false;//if being connected

        void connection()
        {
            try
            {
                //serial port switch
                //ComID  0 UHF；1 WIFI；2 Barcode；3 GPS
                UHF.SerialPortSwitch_Ex(0);
                //initialize the module
                UHF.UHFInit();
                //connect with the baudrate of 57600
                byte[] statusArr = new byte[1];
                statusArr[0] = 0;

                for (int i = 1; i < 5; i++)
                {
                    //baudrate 0-9600、1-19200、2-57600、3-115200
                    if (UHF.UHFOpenAndConnect(statusArr))
                    {
                        //isCon = true;
                        UHF.UHFFlagCrcOn();  //enable CRC in case of messy codes
                        Win32.PlayScanSound();
                        break;
                        //frmMain fm = new frmMain();
                        //if (fm.ShowDialog() == DialogResult.No)
                        //{

                        //    UHF.UHFCloseAndDisconnect();
                        //}
                    }
                    else
                    {
                        if (i == 5)
                        {
                            MessageBox.Show("Connect Fail!", "Notice");
                            this.Close();
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void init() {
            System.Threading.Thread.Sleep(100);
            //power
           // byte power = 0;

            /*if (UHF.UHFGetPower(ref power))
            {
                this.cbpower.Text = power.ToString();
            }
            else
            {
                this.cbpower.Text = "24";
            }*/
            System.Threading.Thread.Sleep(300);
            //MessageBox.Show("1");
            //frequency 
            //frequency
            byte[] uFreMode = new byte[1];
            byte[] uFreBase = new byte[1];
            byte[] uBaseFre = new byte[2];
            byte[] uChannNum = new byte[1];
            byte[] uChannSpc = new byte[1];
            byte[] uFreHop = new byte[1];

            /*if (UHF.UHFGetFrequency(uFreMode, uFreBase, uBaseFre, uChannNum, uChannSpc, uFreHop))
            {
                this.cbregulation.Text = UHFHelper.dicFreMode[uFreMode[0]];
            }
            else
            {
                cbregulation.SelectedIndex = 1;
            }*/
            //cbqvalue.SelectedIndex = 3;
            //MessageBox.Show("2");
            //software version
            System.Threading.Thread.Sleep(600);
            byte[] uSerial = new byte[6];
            byte[] uVersion = new byte[3];
            if (UHF.UHFGetVersion(uSerial, uVersion))
            {
                //BitConverter.ToString(uSerial).Replace("-", "") + BitConverter.ToString(uVersion).Replace("-", "");
                // this.labhv.Text += CommonConvert.ByteArray2String(uSerial).Replace(" ","");
                string vers = UHF.ByteArrayToHexString(uVersion);
                string hvers = UHF.ByteArrayToHexString(uSerial);
                //MessageBox.Show(hvers + " " + vers);
                //this.labsv.Text = vers;
                //this.labhv.Text = hvers;

            }
        }

       /* private void frmMain_Closing(object sender, CancelEventArgs e)
        {
            //if (run)
            //{
                if (thrReceiveUii != null)
                {
                    thrReceiveUii.Abort();
                    thrReceiveUii = null;
                }
                //stop reading
                UHF.UHFStopGet();
           // }
            UHF.UHFCloseAndDisconnect();
            UHF.UHFFree();
            //this.DialogResult = DialogResult.No;
        }*/

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.rfidTag.stopReading();           
        }

        private void btnClearlblStatus_Click(object sender, EventArgs e)
        {
            lblStatus.Text="";
        }

        public void cerrando(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Cerrando");
            frm2 = new FrmMenuPpal();
            frm2.Show();
        }
        
        
    }
}
using System;

using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace sli1
{
    /// <summary>
    /// Clase que maneja el comportamiento de los TAGS pudiendo ser RFID o Bar Code
    /// </summary>
    class RfidTag:ITag
    {
        private  Thread thrReceiveUii;
        private bool canReceive;
        private byte iniAntiQ;
        private byte flagAnti;
        private bool FlagSound;
        private int count;
        private string epc;
        private string uii;
        private int serial;
        private FrmIngresoTag form;

        private Dictionary<string, int> datalist; 
       

        public RfidTag() {
            this.thrReceiveUii = null;
            this.canReceive = false;
            this.FlagSound = true;
            this.count = 0;
            this.epc = string.Empty;
            this.uii = string.Empty;
            this.serial = 0;
        }
        public RfidTag(byte flagAnti,byte iniAntiQ):this()
        {
            this.iniAntiQ = iniAntiQ;
            this.flagAnti = flagAnti;
        }

        public RfidTag(FrmIngresoTag form) : this() 
        {
            this.form = form;
        }
        //Arreglar de aqui...

        /// <summary>
        /// serial number
        /// </summary>
        public int Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        /// <summary>
        /// UII
        /// </summary>
        public string Uii
        {
            get { return uii; }
            set { uii = value; }
        }

        /// <summary>
        /// EPC
        /// </summary>
        public string Epc
        {
            get { return epc; }
            set { epc = value; }
        }

        /// <summary>
        /// times
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public void write(bool por){
            return;
        }
        //Hasta aqui...

        public void singleRead() 
        {
            int[] uLenUii = new int[1];
            byte[] uUii = new byte[128];

            //single recognition
           // if (this.cbReadMode.SelectedIndex == 0)
            //{
                if (UHF.UHFInventorySingle(uLenUii, uUii))
                {
                    if (uLenUii[0] > 0)
                    {
                        Win32.PlayScanSound();
                        string uii = BitConverter.ToString(uUii, 0, uLenUii[0]).Replace("-", "");
                        //EPC is the uii except the ahead two bytes start with 3000EXXXXX
                        //this.txtReadData.Text = uii.Substring(4, uii.Length - 4);
                        //MessageBox.Show(uii.Substring(4,uii.Length-4));
                        this.form.setLblStatus(uii.Substring(4, uii.Length - 4));
                    }
                    else
                    {
                        //this.txtReadData.Text = "";
                        //Console.WriteLine("No leyo nada...1");
                        this.form.setLblStatus("Error en la lectura, largo de EPC = 0");
                    }
                }
                else
                {
                    //this.txtReadData.Text = "";
                    
                    this.form.setLblStatus("Error No hay Tag para Leer");
                }
         /*   }
            else
            {
                //cycle recognition
                if (UHF.UHFInventory(0x00, 0x03))
                {
                    if (UHF.UHFGetReceived(uLenUii, uUii))
                    {
                        if (uLenUii[0] > 0)
                        {
                            Win32.PlayScanSound();
                            string uii = BitConverter.ToString(uUii, 0, uLenUii[0]).Replace("-", "");
                            //EPC is the uii except the ahead two bytes start with 3000EXXXXX
                            this.txtReadData.Text = uii.Substring(4, uii.Length - 4);
                        }
                        else
                        {
                            this.txtReadData.Text = "";
                        }
                    }
                    else
                    {
                        this.txtReadData.Text = "";
                    }
                }
            }*/
            //stop reading
            UHF.UHFStopGet();
        }

        /// <summary>
        /// Genera el Thread para que se pueda ejecutar la lectura.
        /// </summary>
        /// <param name="run"></param>
        public void read(bool isConnected) {

                //MessageBox.Show("Reading Mode:" + flagAnti + ",Q" + iniAntiQ);
                //Activa el Escaneo Físico del Tag de forma continua para leer su data de acuerdo a los parámetros de información.
                //this.canReceive = UHF.UHFInventory(flagAnti, iniAntiQ);
                //this.canReceive = UHF.UHFInventory(0x01, 0x15);
                if (this.canReceive=UHF.UHFInventory(0x01, 0x03))
                {
                    //Abre un UII thread para recibir la data
                    if (thrReceiveUii == null)
                    {
                        //ReceiveUiiProc se genera como thread
                        thrReceiveUii = new System.Threading.Thread(receiveUiiProc);
                        thrReceiveUii.IsBackground = true;
                        thrReceiveUii.Start();
                    }
                    else
                    {
                        thrReceiveUii.Start();
                    }
                }
        }
       
        /// <summary>
        /// Detiene el thread de lectura.
        /// </summary>
        public void stopReading() {

            if (UHF.UHFInventory(0x01, 0x03)) //Desacoplar...
            {
                //close UII receiving thread
                if (thrReceiveUii != null)
                {
                    thrReceiveUii.Abort();
                    thrReceiveUii = null;
                }
                //stop get
                //Deja de escanear para leer físicamente el tag.
                UHF.UHFStopGet();
            }

        }

        /// <summary>
        /// Método que lee la data fisica de la tarjeta irradiada por la antena
        /// </summary>
        private void receiveUiiProc()
        {
            this.datalist = new Dictionary<string, int>();//the reading EPC list and times            
            int[] uLenUii = new int[1]; //largo del codigo o EPC.
            byte[] uUii = new byte[128]; //Codigo o EPC - UII should be at least 66 bytes
            while (canReceive)
            {
                //int[] uLenUii = new int[1];
                //byte[] uUii = new byte[128];   //UII should be at least 66 bytes
                //Se extrae la data de la capa física del Hand held
                if (UHF.UHFGetReceived(uLenUii, uUii))
                {
                    if (uLenUii[0] > 0)
                    {
                        if (FlagSound)
                        {
                            Win32.PlayScanSound();
                        }

                        //Se convierte la data de entrada en formato Base a un Array para su lectura
                        string uii = BitConverter.ToString(uUii, 0, uLenUii[0]).Replace("-", "");

                        //show to the interface
                        //TagRead ent = new TagRead();

                        Uii = uii;
                        Epc = uii.Substring(4, uii.Length - 4);  //Epc
                        Count = 1;
                        if (!datalist.ContainsKey(Epc))
                        {
                            datalist.Add(Epc, 1);
                            foreach(string s in datalist.Keys)
                            {
                                MessageBox.Show(s);
                            }
                            //MessageBox.Show(datalist.Values[0]);
                            //System.out
                            /*ListViewItem lv = new ListViewItem();
                            lv.Text = (datalist.Count).ToString();
                            lv.SubItems.Add(ent.Epc);
                            lv.SubItems.Add("1");
                            _lvReadID.Invoke(new EventHandler(delegate
                            {
                                _lvReadID.Items.Add(lv);
                            }));

                            _lbSumTagsReadID.Invoke(new EventHandler(delegate
                            {
                                _lbSumTagsReadID.Text = datalist.Count.ToString();
                            }));*/
                        }
                        else
                        {
                            datalist[Epc] += 1;
                           // DisplayTagID(ent);

                        }
                        //UpdateControl(ent);

                    }
                }

                System.Threading.Thread.Sleep(20);
            }
        }
    }
}

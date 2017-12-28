﻿using System;

using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace sli1
{
    /// <summary>
    /// read tags
    /// </summary>
    class Tag
    {
        private  Thread thrReceiveUii;
        private bool canReceive;
        private byte iniAntiQ;
        private byte flagAnti;
        private bool FlagSound;

        Dictionary<string, int> datalist = new Dictionary<string, int>();//the reading EPC list and times
       

        public Tag() {
            this.thrReceiveUii = null;
            this.canReceive = false;
            this.FlagSound = true;
        }
        public Tag(byte iniAntiQ, byte flagAnti):this()
        {
            this.iniAntiQ = iniAntiQ;
            this.flagAnti = flagAnti;
        }

       //Arreglar de aqui...
        private int serial = 0;
        /// <summary>
        /// serial number
        /// </summary>
        public int Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        private string uii = string.Empty;
        /// <summary>
        /// UII
        /// </summary>
        public string Uii
        {
            get { return uii; }
            set { uii = value; }
        }
        private string epc = string.Empty;
        /// <summary>
        /// EPC
        /// </summary>
        public string Epc
        {
            get { return epc; }
            set { epc = value; }
        }
        private int count = 0;
        /// <summary>
        /// times
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        //Hasta aqui...

        public void Read(Boolean run) {

            if (run)
            {
                //MessageBox.Show("Reading Mode:" + flagAnti + ",Q" + iniAntiQ);
                canReceive = UHF.UHFInventory(flagAnti, iniAntiQ);
                if (canReceive)
                {


                    //open UII receiving thread
                    if (thrReceiveUii == null)
                    {
                        thrReceiveUii = new System.Threading.Thread(ReceiveUiiProc);
                        thrReceiveUii.IsBackground = true;
                        thrReceiveUii.Start();
                    }
                    else
                    {
                        thrReceiveUii.Start();
                    }
                }
            }
            else
            {

                //close UII receiving thread
                if (thrReceiveUii != null)
                {
                    thrReceiveUii.Abort();
                    thrReceiveUii = null;
                }
                //stop get 
                UHF.UHFStopGet();
            }
        }

        private void ReceiveUiiProc()
        {
            Tag ent = new Tag();
            int[] uLenUii = new int[1];
            byte[] uUii = new byte[128]; //UII should be at least 66 bytes
            while (canReceive)
            {
                //int[] uLenUii = new int[1];
                //byte[] uUii = new byte[128];   //UII should be at least 66 bytes
                if (UHF.UHFGetReceived(uLenUii, uUii))
                {
                    if (uLenUii[0] > 0)
                    {
                        if (FlagSound)
                        {
                            Win32.PlayScanSound();
                        }

                        string uii = BitConverter.ToString(uUii, 0, uLenUii[0]).Replace("-", "");

                        //show to the interface
                        //TagRead ent = new TagRead();
                        ent.Uii = uii;
                        ent.Epc = uii.Substring(4, uii.Length - 4);  //Epc
                        ent.Count = 1;
                        if (!datalist.ContainsKey(ent.Epc))
                        {

                            datalist.Add(ent.Epc, 1);
                            ListViewItem lv = new ListViewItem();
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
                            }));
                        }
                        else
                        {
                            datalist[ent.Epc] += 1;
                            DisplayTagID(ent);

                        }
                        //UpdateControl(ent);

                    }
                }

                System.Threading.Thread.Sleep(20);
            }
        }
    }
}

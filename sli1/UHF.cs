using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace sli1
{
    /// <summary>
    /// class of uhf
    /// </summary>
    public static class UHF
    {
        /// <summary>
        /// port swtich   
        /// </summary>
        /// <param name="ComID"> ComID  0 UHF；1 WIFI；2 Barcode；3 GPS
        /// </param>
        [DllImport("DeviceAPI.dll")]
        public static extern void SerialPortSwitch_Ex(byte ComID);

        /// <summary>
        /// initialize
        /// </summary>
        [DllImport("DeviceAPI.dll")]
        public static extern void UHFInit();

        /// <summary>
        /// free UHF module
        /// </summary>
        [DllImport("DeviceAPI.dll")]
        public static extern void UHFFree();

        /// <summary>
        /// get the power
        /// </summary>
        /// <param name="BaudRate"></param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFGetPower(byte[] BaudRate);

        /// <summary>
        /// get the power
        /// </summary>
        /// <param name="BaudRate"></param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFGetPower(ref byte BaudRate);

        /// <summary>
        /// set power 
        /// </summary>
        /// <param name="uPower">Power</param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFSetPower(byte uPower);

        /// <summary>
        /// get frequency
        /// </summary>
        /// <param name="uFreMode">frequency mode
        /// 0x00-China Standard 920-925MHz、0x01-China Standard 840-845MHz、
        /// 0x02-ETSI Standard、0x03-Fixed Mode（915MHz）、0x04-Customized Mode
        /// </param>
        /// <param name="uFreBase">frequency base
        /// （0x00-50KHz、0x01-125KHZ）
        /// when the frequency base is 50KHz, the bandwidth should be less than 12MHz; when the frequency base is 125KHz, the bandwidth should be less than 32MHz
        /// </param>
        /// <param name="uBaseFre">starting frequency（2 bytes）
        /// bit14~5 as the integer part of the starting frequency, bit3~0 as the decimal part
        /// </param>
        /// <param name="uChannNum">channel</param>
        /// <param name="uChannSpc">channel bandwidth（bit3~0-channel bandwidth product）</param>
        /// <param name="uFreHop">frequency hopping sequency
        /// （0x00-random frequency hopping、 0x01-frequency hopping from high to low、 0x10-frequency hopping from low to high、 FALSE-random frequency hopping）
        /// </param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFGetFrequency(byte[] uFreMode, byte[] uFreBase, byte[] uBaseFre, byte[] uChannNum, byte[] uChannSpc, byte[] uFreHop);

        /// <summary>
        /// set the default parameters
        /// </summary>
        /// <param name="FreMode">
        /// 0x00 -- （China Standard、125KHz frequency base、starting frequency 840.625MHz、 channels 16、bandwidth product 2,、random frequency hopping）---China standard 840-845
        /// 0x01 -- （China Standard、125KHz frequency base、starting frequency 920.625MHz、channels 16、bandwidth product 2,、random frequency hopping）---China Stardard 920-925
        /// 0x02 -- （ETSI Standard 、50KHz frequency base、  starting frequency 865.1MHz、channels 12、bandwidth product 4、random frequency hopping）   ---ETSI Stanrdard 865-868
        /// 0x03 -- （Top frequency mode、125KHz frequency base、starting frequency 915MHz、channel 1、bandwidth product 0、random frequency hopping）            ---Fixed mode 915
        /// </param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFSetFrequency_EX(byte FreMode);
        /// <summary>
        /// get the UHF hardware version and software version
        /// </summary>
        /// <param name="uSerial">hardware serial number: 6 byts</param>
        /// <param name="uVersion">software version: 3 bytes</param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFGetVersion(byte[] uSerial, byte[] uVersion);

        /// <summary>
        /// open and connect
        /// </summary>
        /// <param name="BaudRate">baudrate 0-9600、1-19200、2-57600、3-115200</param>
        /// <param name="status"></param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFOpenAndConnect(byte[] status);
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFOpenAndConnect(byte BaudRate, ref byte status);

        /// <summary>
        /// close and disconnect
        /// </summary>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFCloseAndDisconnect();


        [DllImport("DeviceAPI.dll")]
        public static extern void UHFFlagCrcOn();

        /// <summary>
        /// uhf cycle recoginition command
        /// </summary>
        /// <param name="flagAnti">0x01-enable anti-collision、 0x00-disable anti-collision</param>
        /// <param name="initQ">Q value for anti-collision mode、Q value0~15、
        /// scan the target tag continuously by the times of the power of 2 Q in anti-collision mode, the scanning time is in direct proportion to the index of Q.
        /// </param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFInventory(byte flagAnti, byte initQ);



        /// <summary>
        /// receive the returned UII
        /// </summary>
        /// <param name="uLenUii">LenUii</param>
        /// <param name="uUii">uii, at least 66 bytes</param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFGetReceived(int[] uLenUii, byte[] uUii);

        /// <summary>
        /// stop reading
        /// </summary>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFStopGet();

        /// <summary>
        /// single recognition(only operate only regardless failure or success)
        /// </summary>
        /// <param name="uLenUii">LenUii</param>
        /// <param name="uUii">uii, at least 64 bytes</param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFInventorySingle(int[] uLenUii, byte[] uUii);


        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFWriteData(byte[] uAccessPwd, byte uBank, int uPtr, byte uCnt, byte[] uUii, byte[] uWriteData, byte[] uErrorCode);

        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFReadData(byte[] uAccessPwd, byte uBank, int uPtr, byte uCnt, byte[] uUii, byte[] uReadData, byte[] uErrorCode);


        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFWriteDataSingle(byte[] uAccessPwd, byte uBank, int uPtr, byte uCnt, byte[] uWriteData, byte[] uUii, byte[] uLenUii, byte[] uErrorCode);

        /// <summary>
        /// read tag data(no specified UII)
        /// </summary>
        /// <param name="uAccessPwd"></param>
        /// <param name="uBank"></param>
        /// <param name="uPtr"></param>
        /// <param name="uCnt"></param>
        /// <param name="uReadData"></param>
        /// <param name="uUii"></param>
        /// <param name="uLenUii"></param>
        /// <param name="uErrorCode"></param>
        /// <returns></returns>
        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFReadDataSingle(byte[] uAccessPwd, byte uBank, int uPtr, byte uCnt, byte[] uReadData, byte[] uUii, byte[] uLenUii, byte[] uErrorCode);





        [DllImport("DeviceAPI.dll")]
        public static extern bool UHFReadDataSingle(byte[] uAccessPwd, byte uBank, int uPtr, int uCnt, byte[] uReadData, byte[] uUii, int[] uLenUii, byte[] uErrorCode);








        /// <summary>
        /// Write EPC
        /// </summary>
        /// <param name="uPtr">2~7 value</param>
        /// <param name="uUii"></param>
        /// <param name="uWriteData"></param>
        /// <returns></returns>
        public static bool WriteEpcDataFree(int uPtr, ref byte[] uUii, byte[] uWriteData)
        {
            byte[] uErrorCode = new byte[8];
            byte[] len = new byte[2];
            byte nCnt = 0x01;
            byte[] uAccessPwd = new byte[4];
            byte bank = 0x01;
            return UHFWriteDataSingle(uAccessPwd, bank, uPtr, nCnt, uWriteData, uUii, len, uErrorCode);
        }

        /// <summary>
        /// hex string to byte array 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        //bytearray to hex string
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }
    }
}

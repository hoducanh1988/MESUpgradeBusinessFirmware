using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows;

namespace EW12CG.Protocol
{
    public class RS232
    {
        public SerialPort Port1 = null;
        public SerialPort Port2 = null;
        public SerialPort Port3 = null;
        public SerialPort Port4 = null;
        public SerialPort Port5 = null;
        public SerialPort Port6 = null;
        public SerialPort Port7 = null;
        public SerialPort Port8 = null;

        public Dictionary<string, string> dictCOM = new Dictionary<string, string>();
        public Dictionary<string, SerialPort> dictPort = new Dictionary<string, SerialPort>();

        public bool openSerialPort(string ONT_Numb)
        {
            try
            {
                ////thêm 
                //dictCOM.Add("1", GlobalData.initSetting.COMPort_1);
                //dictCOM.Add("2", GlobalData.initSetting.COMPort_2);
                //dictCOM.Add("3", GlobalData.initSetting.COMPort_3);
                //dictCOM.Add("4", GlobalData.initSetting.COMPort_4);
                //dictCOM.Add("5", GlobalData.initSetting.COMPort_5);
                //dictCOM.Add("6", GlobalData.initSetting.COMPort_6);
                //dictCOM.Add("7", GlobalData.initSetting.COMPort_7);
                //dictCOM.Add("8", GlobalData.initSetting.COMPort_8);

                //thêm 
                dictPort.Add("1", Port1);
                dictPort.Add("2", Port2);
                dictPort.Add("3", Port3);
                dictPort.Add("4", Port4);
                dictPort.Add("5", Port5);
                dictPort.Add("6", Port6);
                dictPort.Add("7", Port7);
                dictPort.Add("8", Port8);
            }
            catch
            { }

            string message = "";
            try
            {
                dictPort[ONT_Numb] = new SerialPort();
                dictPort[ONT_Numb].PortName = dictCOM[ONT_Numb];
                dictPort[ONT_Numb].BaudRate = 57600;
                dictPort[ONT_Numb].Parity = Parity.None;
                dictPort[ONT_Numb].DataBits = 8;
                dictPort[ONT_Numb].StopBits = StopBits.One;
                try
                {
                    dictPort[ONT_Numb].Open();
                    //switch (ONT_Numb)
                    //{
                    //    //case "1":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData1);
                    //    //        break;
                    //    //    }
                    //    //case "2":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData2);
                    //    //        break;
                    //    //    }
                    //    //case "3":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData3);
                    //    //        break;
                    //    //    }
                    //    //case "4":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData4);
                    //    //        break;
                    //    //    }
                    //    //case "5":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData5);
                    //    //        break;
                    //    //    }
                    //    //case "6":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData6);
                    //    //        break;
                    //    //    }
                    //    //case "7":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData7);
                    //    //        break;
                    //    //    }
                    //    //case "8":
                    //    //    {
                    //    //        dictPort[ONT_Numb].DataReceived += new SerialDataReceivedEventHandler(port_OnReceiveData8);
                    //    //        break;
                    //    //    }
                    //}

                    //MessageBox.Show("Opened " + dictPort[ONT_Numb].PortName);
                    return dictPort[ONT_Numb].IsOpen == true ? true : false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                message = ex.ToString();
                return false;
            }
        }

        //private void port_OnReceiveData1(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo1.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}

        //private void port_OnReceiveData2(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo2.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}

        //private void port_OnReceiveData3(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo3.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}

        //private void port_OnReceiveData4(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo4.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}

        //private void port_OnReceiveData5(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo5.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}

        //private void port_OnReceiveData6(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo6.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}

        //private void port_OnReceiveData7(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo7.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}

        //private void port_OnReceiveData8(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort s = (SerialPort)sender;
        //    string receiveData = s.ReadExisting();
        //    if (receiveData != string.Empty)
        //    {
        //        GlobalData.testingInfo8.LOGCOM += receiveData;
        //    }
        //    Thread.Sleep(100);
        //}


        public bool closeSerialPort(string ONT_Numb)
        {
            string message = "";
            try
            {
                dictPort[ONT_Numb].Close();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharp.CrestronSockets;

namespace ChenSmart
{

    /// <summary>
    /// 施德朗5寸触摸屏
    /// </summary>
    public class ILiveTPC5
    {
       // TCPClient tcpClient = null;
       

        private ComPort com;

        public delegate void PTCIHandler(int id, int btnid);

        public event PTCIHandler PushTPCIEvent;
        #region UDP口
        private UDPServer server = new UDPServer();

        public ILiveTPC5(int port)
        {
            try
            {

                server.EnableUDPServer("192.168.188.25", 6004, port);
                SocketErrorCodes code = server.ReceiveDataAsync(this.Read);

            }
            catch (Exception)
            {

            }

       

        }
        private void Read(UDPServer myUDPServer, int numberOfBytesReceived)
        {
            byte[] rbytes = new byte[numberOfBytesReceived];

            if (numberOfBytesReceived > 0)
            {
               string messageReceived = Encoding.GetEncoding(28591).GetString(myUDPServer.IncomingDataBuffer, 0, numberOfBytesReceived);      
               OnDataReceived(messageReceived);
            }
            try
            {
                SocketErrorCodes code = myUDPServer.ReceiveDataAsync(this.Read);
                Thread.Sleep(300);
            }
            catch (Exception)
            {
            }
       

        }
        #endregion
        

        #region COM
        public ILiveTPC5(ComPort com)
        {
            #region 注册串口
            this.com = com;
            com.SerialDataReceived += new ComPortDataReceivedEvent(ILiveTPC5_SerialDataReceived);
            if (!com.Registered)
            {
                if (com.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    ErrorLog.Error("COM Port couldn't be registered. Cause: {0}", com.DeviceRegistrationFailureReason);
                if (com.Registered)
                    com.SetComPortSpec(ComPort.eComBaudRates.ComspecBaudRate9600,
                                                                     ComPort.eComDataBits.ComspecDataBits8,
                                                                     ComPort.eComParityType.ComspecParityNone,
                                                                     ComPort.eComStopBits.ComspecStopBits1,
                                         ComPort.eComProtocolType.ComspecProtocolRS232,
                                         ComPort.eComHardwareHandshakeType.ComspecHardwareHandshakeNone,
                                         ComPort.eComSoftwareHandshakeType.ComspecSoftwareHandshakeNone,
                                         false);
            }

            #endregion
        }
        List<byte> rdata = new List<byte>(6);

        void ILiveTPC5_SerialDataReceived(ComPort ReceivingComPort, ComPortSerialDataEventArgs args)
        {
            this.OnDataReceived(args.SerialData);
            //if (rdata.Count>50)
            //{
            //    rdata.Clear();
            //}
            //byte[] sendBytes = Encoding.ASCII.GetBytes(args.SerialData);
            //ILiveDebug.Instance.WriteLine("485Length:" + sendBytes.Length.ToString() + "data:" + ILiveUtil.ToHexString(sendBytes));

            //try
            //{
            //    foreach (var item in sendBytes)
            //    {
            //        rdata.Add(item);
            //        if (item == 0x0D&&rdata.Count>5)
            //        {
            //            this.ProcessData();
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ILiveDebug.Instance.WriteLine(ex.Message);

            //   // throw;
            //}


        }

        #endregion

        void OnDataReceived(string serialData)
        {
            if (rdata.Count > 20)
            {
                rdata.Clear();
            }
            byte[] sendBytes = Encoding.ASCII.GetBytes(serialData);
            try
            {
                foreach (var item in sendBytes)
                {
                    rdata.Add(item);
                    if (item == 0x0D && rdata.Count > 5)
                    {
                        this.ProcessData();
                    }
                }

            }
            catch (Exception)
            {
               //ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }
        void ProcessData()
        {
            try
            {
                if (rdata.Count == 6 && rdata[0]==0x55&&rdata[1]==0x10)
                {

                    byte iChanIdx = rdata[2];

                    int h = rdata[3];

                    int l = rdata[4];

                    if (rdata[5] == 0x0D)
                    {
                        this.PushTPCIEvent(iChanIdx, (h*256) + l);
                    }

                }

                rdata.Clear();

            }
            catch (Exception )
            {

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharp.CrestronSockets;
using ILiveLib;

namespace ChenSmart
{
    /// <summary>
    /// 聪普16I
    /// </summary>
    public class ILiveGRODIGY16I
    {
        TCPClient tcpClient = null;
       

        private int port = 6100;
        private ILiveTCPServer tcpgy = null;
        /// <summary>
        /// 接收事件
        /// </summary>
        private Thread tcpListenHandler;


        private ComPort com;

        public delegate void Push16IHandler(int id,int btnid, bool iChanStatus);

        public event Push16IHandler Push16IEvent;

        #region UDP口
        private UDPServer server = new UDPServer();

        public ILiveGRODIGY16I(int port)
        {
            try
            {

                server.EnableUDPServer("192.168.188.25", 6005, port);
                SocketErrorCodes code = server.ReceiveDataAsync(this.Read);

            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine(ex.Message);
                // ILiveDebug.Instance.WriteLine(ex.Message);
            }

       

        }
        private void Read(UDPServer myUDPServer, int numberOfBytesReceived)
        {
          //  byte[] rbytes = new byte[numberOfBytesReceived];

            if (numberOfBytesReceived > 0)
            {

                string messageReceived = Encoding.GetEncoding(28591).GetString(myUDPServer.IncomingDataBuffer, 0, numberOfBytesReceived);


                OnDataReceived(messageReceived);

            }

            //  CrestronConsole.PrintLine("Recv:" + ILiveUtil.ToHexString(rbytes));
            SocketErrorCodes code = myUDPServer.ReceiveDataAsync(this.Read);
            Thread.Sleep(300);

        }
        #endregion
     /*   public ILiveGRODIGY16I(int port)
        {
            tcpClient = new TCPClient("192.168.188.25", port, 4096);
            tcpClient.SocketStatusChange += new TCPClientSocketStatusChangeEventHandler(tcpClient_SocketStatusChange);
            this.Connect();
        }
        private void Connect()
        {
            try
            {
                if (tcpClient.ClientStatus != SocketStatus.SOCKET_STATUS_CONNECTED)
                {
                    SocketErrorCodes codes = tcpClient.ConnectToServer();
                }

            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine("16idebug:" + ex.Message);
            }

            new Thread(tcpListenMethod, null, Thread.eThreadStartOptions.Running);
        }

        void tcpClient_SocketStatusChange(TCPClient myTCPClient, SocketStatus clientSocketStatus)
        {

            if (clientSocketStatus != SocketStatus.SOCKET_STATUS_CONNECTED)
            {
                try
                {
                    myTCPClient.ConnectToServer();
                    new Thread(tcpListenMethod, null, Thread.eThreadStartOptions.Running);
                }
                catch (Exception)
                {

                }

            }

            //throw new NotImplementedException();
        }

        /// <summary>
        /// 数据队列处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal object tcpListenMethod(object obj)
        {
            tcpClient.ReceiveDataAsync(this.Read);

            return null;
        }

        public void Read(TCPClient myTCPClient, int numberOfBytesReceived)
        {
            if (myTCPClient.ClientStatus != SocketStatus.SOCKET_STATUS_CONNECTED)
            {

                return;
            }
            string messageReceived = string.Empty;

            try
            {

                messageReceived = Encoding.GetEncoding(28591).GetString(myTCPClient.IncomingDataBuffer, 0, numberOfBytesReceived);

                OnDataReceived(messageReceived);

                myTCPClient.ReceiveDataAsync(this.Read, 0);


            }
            catch (Exception ex)
            {

                // if (Disconnected != null)
                //   Disconnected(this, EventArgs.Empty);
            }
        }
        */

        public ILiveGRODIGY16I(ComPort com)
        {
            #region 注册串口
            this.com = com;
            com.SerialDataReceived += new ComPortDataReceivedEvent(ILiveGRODIGY16I_SerialDataReceived);
            if (!com.Registered)
            {
                if (com.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    ErrorLog.Error("COM Port couldn't be registered. Cause: {0}", com.DeviceRegistrationFailureReason);
                if (com.Registered)
                    com.SetComPortSpec(ComPort.eComBaudRates.ComspecBaudRate115200,
                                                                     ComPort.eComDataBits.ComspecDataBits8,
                                                                     ComPort.eComParityType.ComspecParityNone,
                                                                     ComPort.eComStopBits.ComspecStopBits1,
                                         ComPort.eComProtocolType.ComspecProtocolRS485,
                                         ComPort.eComHardwareHandshakeType.ComspecHardwareHandshakeNone,
                                         ComPort.eComSoftwareHandshakeType.ComspecSoftwareHandshakeNone,
                                         false);
            }
            #endregion
        }

        void ILiveGRODIGY16I_SerialDataReceived(ComPort ReceivingComPort, ComPortSerialDataEventArgs args)
        {

            this.OnDataReceived(args.SerialData);
        }


        void OnDataReceived(string serialData)
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(serialData);
              ILiveDebug.Instance.WriteLine("16IData:"+ILiveUtil.ToHexString(sendBytes));
            if (sendBytes != null && sendBytes.Length == 3)
            {
                // if (sendBytes[0] == 0x1E)
                // {
                byte iChanIdx = sendBytes[1];
                bool iChanStatus = Convert.ToBoolean(sendBytes[2]);
                if (iChanIdx > 8)
                {
                    if (9 == iChanIdx)/*RD[16]*/
                    {
                        this.Push16IEvent(sendBytes[0], 16, iChanStatus);
                        //  Push_16I(16, iChanStatus);
                    }
                    else if ((iChanIdx <= 22) && (iChanIdx > 15))	/*RD[9] ~ RD[15]*/
                    {
                        /*iChanIdx 属于[16,22]*/
                        // Push_16I(31 - iChanIdx, iChanStatus);
                        if (this.Push16IEvent != null)
                        {
                            this.Push16IEvent(sendBytes[0], 31 - iChanIdx, iChanStatus);
                        }
                    }
                }
                else
                {
                    if (iChanIdx > 0)/*RD[1] ~ RD[8]*/
                    {
                        //Push_16I(9 - iChanIdx, iChanStatus);
                        this.Push16IEvent(sendBytes[0], 9 - iChanIdx, iChanStatus);
                    }

                }
                // }
            }
        }

    }
}
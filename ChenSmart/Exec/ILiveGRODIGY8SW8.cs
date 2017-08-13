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
    /// 聪普8路继电器
    /// </summary>
    public class ILiveGRODIGY8SW8
    {
        public CongPu8SW8Status Status = new CongPu8SW8Status();


        int addr = 0x12;


        private ComPort com;

       /* TCPClient tcpClient = null;

        /// <summary>
        /// 接收事件
        /// </summary>
        private Thread tcpListenHandler;

        public ILiveGRODIGY8SW8(int addr, int tcpport)
        {
            this.addr = addr;

            tcpClient = new TCPClient("192.168.188.25", tcpport, 4096);
            tcpClient.SocketStatusChange += new TCPClientSocketStatusChangeEventHandler(tcpClient_SocketStatusChange);
            this.Connect();

        }
        private void Connect()
        {
            try
            {
                ILiveDebug.Instance.WriteLine("tcpclientconn");
                if (tcpClient.ClientStatus != SocketStatus.SOCKET_STATUS_CONNECTED)
                {
                    SocketErrorCodes codes = tcpClient.ConnectToServer();
                    ILiveDebug.Instance.WriteLine("tcpclientconn" + codes.ToString());

                }

            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine("8sw8:" + ex.Message);
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
        */
         #region UDP口
        private UDPServer server = new UDPServer();

        public ILiveGRODIGY8SW8(int addr, int port)
        {
            this.addr = addr;
            try
            {

                server.EnableUDPServer("192.168.188.25", 6006, port);
              //  SocketErrorCodes code = server.ReceiveDataAsync(this.Read);

            }
            catch (Exception)
            {
               // CrestronConsole.PrintLine(ex.Message);
                // ILiveDebug.Instance.WriteLine(ex.Message);
            }

       

        }
        private void Read(UDPServer myUDPServer, int numberOfBytesReceived)
        {
           // byte[] rbytes = new byte[numberOfBytesReceived];

            if (numberOfBytesReceived > 0)
            {

                //Array.Copy(myUDPServer.IncomingDataBuffer, rbytes, numberOfBytesReceived);

                OnDataReceived(Encoding.GetEncoding(28591).GetString(myUDPServer.IncomingDataBuffer, 0, numberOfBytesReceived));

            }

            //  CrestronConsole.PrintLine("Recv:" + ILiveUtil.ToHexString(rbytes));
            SocketErrorCodes code = myUDPServer.ReceiveDataAsync(this.Read);
            Thread.Sleep(500);

        }
        #endregion
        public ILiveGRODIGY8SW8(int addr, ComPort com):this(com)
        {
            this.addr = addr;
        }
        public ILiveGRODIGY8SW8(ComPort com)
        {
            #region 注册串口
            this.com = com;
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
                this.com.SerialDataReceived += new ComPortDataReceivedEvent(com_SerialDataReceived);
            }
            #endregion
        }

        void com_SerialDataReceived(ComPort ReceivingComPort, ComPortSerialDataEventArgs args)
        {
           // ILiveDebug.Instance.WriteLine("8sw8rec:"+args.SerialData);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 数据队列处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal object tcpListenMethod(object obj)
        {
            //tcpClient.ReceiveDataAsync(this.Read);

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
            catch (Exception )
            {

                // if (Disconnected != null)
                //   Disconnected(this, EventArgs.Empty);
            }
        }
        void OnDataReceived(string serialData)
        { }
        public void RelayOpen()
        {
            this.Status.Relay0 = true;
            this.Status.Relay1 = true;
            this.Status.Relay2 = true;
            this.Status.Relay3 = true;
            this.Status.Relay4 = true;
            this.Status.Relay5 = true;
            this.Status.Relay6 = true;
            this.Status.Relay7 = true;
            for (int i = 0; i < 8; i++)
            {

                this.Relay8SW8(addr, i, true);
            }
        }
        public void RelayClose()
        {
            this.Status.Relay0 = false;
            this.Status.Relay1 = false;
            this.Status.Relay2 = false;
            this.Status.Relay3 = false;
            this.Status.Relay4 = false;
            this.Status.Relay5 = false;
            this.Status.Relay6 = false;
            this.Status.Relay7 = false;
            for (int i = 0; i < 8; i++)
            {
                this.Relay8SW8(addr, i, false);
            }
          
        }

        public void RelayOpen(params int[] address)
        {
            foreach (var item in address)
            {
                this.Relay8SW8(item, 0, true);
                this.Relay8SW8(item, 1, true);
                this.Relay8SW8(item, 2, true);
                this.Relay8SW8(item, 3, true);
                this.Relay8SW8(item, 4, true);
                this.Relay8SW8(item, 5, true);
                this.Relay8SW8(item, 6, true);
                this.Relay8SW8(item, 7, true);
            }
        }
        public void RelayClose(params int[] address)
        {
            foreach (var item in address)
            {
                this.Relay8SW8(item, 0, false);
                this.Relay8SW8(item, 1, false);
                this.Relay8SW8(item, 2, false);
                this.Relay8SW8(item, 3, false);
                this.Relay8SW8(item, 4, false);
                this.Relay8SW8(item, 5, false);
                this.Relay8SW8(item, 6, false);
                this.Relay8SW8(item, 7, false);
            }
        }


        public void Relay8SW8(int port, bool states)
        {
            switch (port)
            {
                case 0:
                    this.Status.Relay0 = states;
                    break;
                case 1:
                    this.Status.Relay1 = states;
                    break;
                case 2:
                    this.Status.Relay2 = states;
                    break;
                case 3:
                    this.Status.Relay3 = states;
                    break;
                case 4:
                    this.Status.Relay4 = states;
                    break;
                case 5:
                    this.Status.Relay5 = states;
                    break;
                case 6:
                    this.Status.Relay6 = states;
                    break;
                case 7:
                    this.Status.Relay7 = states;
                    break;
                default:
                    break;
            }


            this.Relay8SW8(addr, port, states);
        }

        /// <summary>
        /// 聪普继电器
        /// </summary>
        /// <param name="address">地址码</param>
        /// <param name="port">第几路 0-7</param>
        /// <param name="states">true：闭合 false：断开</param>
        private void Relay8SW8(int address, int port, bool states)
        {
            byte[] sendBytes = new byte[] { 0x52, (byte)address, (byte)port, 0x00, (byte)(address + port), 0xAA };
            if (states)
            {
                sendBytes = new byte[] { 0x52, (byte)address, (byte)port, 0x01, (byte)(address + port + 1), 0xAA };
            }
           // string cmd = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
          /*  if (this.com!=null)
            {
                ILiveDebug.Instance.WriteLine("congpucom" + this.com.Registered.ToString() + this.com.BaudRate + cmd);
                
                this.com.Send(cmd);
            }
            else
            {*/
                //ILiveDebug.Instance.WriteLine("cingpugy" + cmd);
                this.server.SendData(sendBytes, sendBytes.Length);
               // this.tcpClient.SendData(sendBytes, sendBytes.Length);
                //this.tcpgy.Send(cmd);
          //  }
            Thread.Sleep(200);
        }

    }

    public class CongPu8SW8Status
    {
        public bool Relay0 = false;
        public bool Relay1 = false;
        public bool Relay2 = false;
        public bool Relay3 = false;
        public bool Relay4 = false;
        public bool Relay5 = false;
        public bool Relay6 = false;
        public bool Relay7 = false;
    }
}
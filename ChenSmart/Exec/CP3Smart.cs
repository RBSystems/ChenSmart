using Crestron.SimplSharpPro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.Lighting.Din;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib;

namespace ChenSmart
{
    public class CP3Smart
    {
        private CrestronControlSystem controlSystem = null;
        public Din1Dim4 dim4_3 = null;
        public Din1Dim4 dim4_4 = null;
        //public event YelaPressHandler YelaPressEvent;

        public DigitalInput myDigitalInputPort1;
        public DigitalInput myDigitalInputPort2;
        public DigitalInput myDigitalInputPort3;
        public DigitalInput myDigitalInputPort4;
        public DigitalInput myDigitalInputPort5;
        public DigitalInput myDigitalInputPort6;
        public DigitalInput myDigitalInputPort7;
        public DigitalInput myDigitalInputPort8;

        public IROutputPort myIROutputPort1;//
        public IROutputPort myIROutputPort2;//
        public IROutputPort myIROutputPort3;//
        public IROutputPort myIROutputPort4;//
        public IROutputPort myIROutputPort5;//
        public IROutputPort myIROutputPort6;
        public IROutputPort myIROutputPort7;
        public IROutputPort myIROutputPort8;

        public Relay relay1;//大厅布帘1开
        public Relay relayWindow1Close;//大厅布帘1关
        public Relay relayWindow2Open;//大厅纱帘1开
        public Relay relayWindow2Close;//大厅纱帘1关
        public Relay relayWindow3Open;//大厅布帘2开
        public Relay relayWindow3Close;//大厅布帘2关
        public Relay relayWindow4Open;//大厅纱帘2开
        public Relay relayWindow4Close;//大厅纱帘2关
        public Relay relayWindow5Open;//侧边布帘开
        public Relay relayWindow5Close;//侧边布帘关
        public Relay relayWindow6Open;//侧边纱帘开
        public Relay relayWindow6Close;//侧边纱帘关


        //public ILiveGRODIGY8SW8 grodigy8SW8;
        //public ILiveDM838 dm8381;
        //public ILiveDM838 dm8382;
        //public ILiveLDI3 musicI3;
        public ILiveIRACC iracc;
        public CP3Smart(CrestronControlSystem system)
        {
            this.controlSystem = system;

        }
        public void RegisterDevices()
        {
            #region 注册串口
            if (this.controlSystem.SupportsComPort)
            {
                //this.iracc = new ILiveIRACC(this.controlSystem.ComPorts[1]);
            //    this.grodigy8SW8 = new ILiveGRODIGY8SW8(this.controlSystem.ComPorts[1]);
               // this.isin = new ILiveIsin(this.controlSystem.ComPorts[1]);
              //  this.musicI3 = new ILiveLDI3(this.controlSystem.ComPorts[3]);
           //    this.iracc = new ILiveIRACC(this.controlSystem.ComPorts[3]);
           
             //   this.dim4_3 = new Din1Dim4(0x03, this.controlSystem);
              //  if (this.dim4_3.Register() != eDeviceRegistrationUnRegistrationResponse.Success)

             //       ErrorLog.Error("din1Dim4_10 failed registration. Cause: {0}", this.dim4_3.RegistrationFailureReason);
             //   this.dim4_4 = new Din1Dim4(0x04, this.controlSystem);
             //   if (this.dim4_4.Register() != eDeviceRegistrationUnRegistrationResponse.Success)

              //      ErrorLog.Error("din1Dim4_10 failed registration. Cause: {0}", this.dim4_4.RegistrationFailureReason);

                //this.comMusicAV = this.controlSystem.ComPorts[3];
                //comMusicAV.SerialDataReceived += new ComPortDataReceivedEvent(comTemp_SerialDataReceived);
                //if (!comMusicAV.Registered)
                //{
                //    if (comMusicAV.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                //        ErrorLog.Error("COM Port couldn't be registered. Cause: {0}", comMusicAV.DeviceRegistrationFailureReason);
                //    if (comMusicAV.Registered)
                //        comMusicAV.SetComPortSpec(ComPort.eComBaudRates.ComspecBaudRate9600,
                //                                                         ComPort.eComDataBits.ComspecDataBits8,
                //                                                         ComPort.eComParityType.ComspecParityNone,
                //                                                         ComPort.eComStopBits.ComspecStopBits1,
                //                             ComPort.eComProtocolType.ComspecProtocolRS232,
                //                             ComPort.eComHardwareHandshakeType.ComspecHardwareHandshakeNone,
                //                             ComPort.eComSoftwareHandshakeType.ComspecSoftwareHandshakeNone,
                //                             false);
                //}
            }
            #endregion

            #region 注册红外
            if (this.controlSystem.SupportsIROut)
            {
               // this.myIROutputPort1 = this.controlSystem.IROutputPorts[1];
               // this.myIROutputPort2 = this.controlSystem.IROutputPorts[2];
               // this.myIROutputPort3 = this.controlSystem.IROutputPorts[3];
               // //this.myIROutputPort4 = this.controlSystem.IROutputPorts[4];
               // //this.myIROutputPort5 = this.controlSystem.IROutputPorts[5];
               // //this.myIROutputPort6 = this.controlSystem.IROutputPorts[6];
               // //this.myIROutputPort7 = this.controlSystem.IROutputPorts[7];
               // //this.myIROutputPort8 = this.controlSystem.IROutputPorts[8];
               // this.myIROutputPort1.SetIRSerialSpec(eIRSerialBaudRates.ComspecBaudRate19200, eIRSerialDataBits.ComspecDataBits8, eIRSerialParityType.ComspecParityNone, eIRSerialStopBits.ComspecStopBits1, Encoding.ASCII);
               // this.myIROutputPort2.LoadIRDriver(Crestron.SimplSharp.CrestronIO.Directory.GetApplicationDirectory() + "\\IR\\Optoma.ir");
               // this.myIROutputPort3.SetIRSerialSpec(eIRSerialBaudRates.ComspecBaudRate9600, eIRSerialDataBits.ComspecDataBits8, eIRSerialParityType.ComspecParityNone, eIRSerialStopBits.ComspecStopBits1, Encoding.ASCII);
               // //this.myIROutputPort4.SetIRSerialSpec(eIRSerialBaudRates.ComspecBaudRate9600, eIRSerialDataBits.ComspecDataBits8, eIRSerialParityType.ComspecParityNone, eIRSerialStopBits.ComspecStopBits1, Encoding.ASCII);
               // //this.myIROutputPort5.SetIRSerialSpec(eIRSerialBaudRates.ComspecBaudRate19200, eIRSerialDataBits.ComspecDataBits8, eIRSerialParityType.ComspecParityNone, eIRSerialStopBits.ComspecStopBits1, Encoding.ASCII);
               // //this.myIROutputPort6.SetIRSerialSpec(eIRSerialBaudRates.ComspecBaudRate9600, eIRSerialDataBits.ComspecDataBits8, eIRSerialParityType.ComspecParityNone, eIRSerialStopBits.ComspecStopBits1, Encoding.ASCII);
               // //this.myIROutputPort7.SetIRSerialSpec(eIRSerialBaudRates.ComspecBaudRate9600, eIRSerialDataBits.ComspecDataBits8, eIRSerialParityType.ComspecParityNone, eIRSerialStopBits.ComspecStopBits1, Encoding.ASCII);

               // //this.myOneS2 = new ILiveS2(this.controlSystem);
               //// this.mySeconedS2 = new ILiveS2(this.controlSystem);
            }
            #endregion

            #region 注册继电器
            //窗帘
            relay1 = this.controlSystem.RelayPorts[1];
            if (relay1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relay1.DeviceRegistrationFailureReason);

            relayWindow1Close = this.controlSystem.RelayPorts[2];
            //relayBedRoomScreenDown.StateChange += new RelayEventHandler(relayBedRoomScreenDown_StateChange);
            if (relayWindow1Close.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relayWindow1Close.DeviceRegistrationFailureReason);

            relayWindow2Open = this.controlSystem.RelayPorts[3];
            if (relayWindow2Open.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relayWindow2Open.DeviceRegistrationFailureReason);

            relayWindow2Close = this.controlSystem.RelayPorts[4];
            if (relayWindow2Close.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relayWindow2Close.DeviceRegistrationFailureReason);

            relayWindow3Open = this.controlSystem.RelayPorts[5];
            if (relayWindow3Open.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relayWindow3Open.DeviceRegistrationFailureReason);

            relayWindow3Close = this.controlSystem.RelayPorts[6];
            if (relayWindow3Close.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relayWindow3Close.DeviceRegistrationFailureReason);

            relayWindow4Open = this.controlSystem.RelayPorts[7];
            if (relayWindow4Open.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relayWindow4Open.DeviceRegistrationFailureReason);

            relayWindow4Close = this.controlSystem.RelayPorts[8];
            if (relayWindow4Close.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("Relay Port couldn't be registered. Cause: {0}", relayWindow4Close.DeviceRegistrationFailureReason);


            #endregion
            #region 注册网络设备
            //this.dm8381 = new ILiveDM838("192.168.1.36");
            //this.dm8382 = new ILiveDM838("192.168.1.35");
            #endregion

        }
    }
}
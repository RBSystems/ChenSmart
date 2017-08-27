using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib;

namespace ChenSmart
{
    public class ILiveCurtains
    {
        private ILiveWintom wintom = null;

        private ILiveGRODIGY8SW8 CongPu_7;
        private ILiveGRODIGY8SW8 CongPu_8;
        public ILiveCurtains()
        {
        }
        public void RegisterDevices()
        {
            //UDPClient client = new UDPClient();
            //this.wintom = new ILiveWintom();
            #region 注册串口
 
            this.CongPu_7 = new ILiveGRODIGY8SW8(7, 8006);
            this.CongPu_8 = new ILiveGRODIGY8SW8(8, 8006);
            #endregion

        }
        /// <summary>
        /// 左侧纱帘
        /// </summary>
        public void Windows1Open()
        {
            this.CongPu_7.Relay8SW8(4, true);
            Thread.Sleep(500);
            this.CongPu_7.Relay8SW8(4, false);

        }
        public void Windows1Close()
        {
            this.CongPu_7.Relay8SW8(5, true);
            Thread.Sleep(500);
            this.CongPu_7.Relay8SW8(5, false);
        }
        public void Windows1Stop()
        {
            this.CongPu_7.Relay8SW8(4, true);
            this.CongPu_7.Relay8SW8(5, true);
            Thread.Sleep(1000);
            this.CongPu_7.Relay8SW8(4, false);
            this.CongPu_7.Relay8SW8(5, false);

        }

        /// <summary>
        /// 左侧布帘
        /// </summary>
        public void Windows2Open()
        {
            this.CongPu_7.Relay8SW8(2, true);
            Thread.Sleep(500);
            this.CongPu_7.Relay8SW8(2, false);
        }
        public void Windows2Close()
        {
            this.CongPu_7.Relay8SW8(3, true);
            Thread.Sleep(500);
            this.CongPu_7.Relay8SW8(3, false);
        }
        public void Windows2Stop()
        {
            this.CongPu_7.Relay8SW8(2, true);
            this.CongPu_7.Relay8SW8(3, true);
            Thread.Sleep(1000);
            this.CongPu_7.Relay8SW8(2, false);
            this.CongPu_7.Relay8SW8(3, false);

        }

        /// <summary>
        /// 右侧布帘
        /// </summary>
        public void Windows4Open()
        {
            this.CongPu_7.Relay8SW8(6, true);
            Thread.Sleep(500);
            this.CongPu_7.Relay8SW8(6, false);
        }
        public void Windows4Close()
        {
            this.CongPu_7.Relay8SW8(7, true);
            Thread.Sleep(500);
            this.CongPu_7.Relay8SW8(7, false);
        }
        public void Windows4Stop()
        {
            this.CongPu_7.Relay8SW8(6, true);
            this.CongPu_7.Relay8SW8(7, true);
            Thread.Sleep(1000);
            this.CongPu_7.Relay8SW8(6, false);
            this.CongPu_7.Relay8SW8(7, false);

        }


        /// <summary>
        /// 右侧纱帘
        /// </summary>
        public void Windows3Open()
        {
            this.CongPu_8.Relay8SW8(0, true);
            Thread.Sleep(500);
            this.CongPu_8.Relay8SW8(0, false);
        }
        public void Windows3Close()
        {
            this.CongPu_8.Relay8SW8(1, true);
            Thread.Sleep(500);
            this.CongPu_8.Relay8SW8(1, false);
        }
        public void Windows3Stop()
        {
            this.CongPu_8.Relay8SW8(0, true);
            this.CongPu_8.Relay8SW8(1, true);
            Thread.Sleep(1000);
            this.CongPu_8.Relay8SW8(0, false);
            this.CongPu_8.Relay8SW8(1, false);

        }


        /// <summary>
        /// 侧布帘
        /// </summary>
        public void Windows6Open()
        {
            this.CongPu_8.Relay8SW8(2, true);
            Thread.Sleep(500);
            this.CongPu_8.Relay8SW8(2, false);
        }
        public void Windows6Close()
        {
            this.CongPu_8.Relay8SW8(3, true);
            Thread.Sleep(500);
            this.CongPu_8.Relay8SW8(3, false);
        }
        public void Windows6Stop()
        {
            this.CongPu_8.Relay8SW8(2, true);
            this.CongPu_8.Relay8SW8(3, true);
            Thread.Sleep(1000);
            this.CongPu_8.Relay8SW8(2, false);
            this.CongPu_8.Relay8SW8(3, false);

        }


        /// <summary>
        /// 侧纱帘
        /// </summary>
        public void Windows5Open()
        {
            this.CongPu_8.Relay8SW8(4, true);
            Thread.Sleep(500);
            this.CongPu_8.Relay8SW8(4, false);
        }
        public void Windows5Close()
        {
            this.CongPu_8.Relay8SW8(5, true);
            Thread.Sleep(500);
            this.CongPu_8.Relay8SW8(5, false);
        }
        public void Windows5Stop()
        {
            this.CongPu_8.Relay8SW8(4, true);
            this.CongPu_8.Relay8SW8(5, true);                   
            Thread.Sleep(1000);
            this.CongPu_8.Relay8SW8(4, false);
            this.CongPu_8.Relay8SW8(5, false);

        }
    }
}
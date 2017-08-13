using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using ILiveLib;
using Crestron.SimplSharpPro;

namespace ChenSmart
{
    public class ILiveRuntime
    {
        private CrestronControlSystem _controlSystem = null;

        private ILiveSmartAPI logic = null;
        private UISmart ui = null;

        public ILiveRuntime(CrestronControlSystem system)
        {
            this._controlSystem = system;

            this.Init();

        }
        /// <summary>
        /// 注册所有模块设备
        /// </summary>
        private void Init()
        {

            try
            {


                this.logic = new ILiveSmartAPI(this._controlSystem);

                ui = new UISmart(this._controlSystem, logic);
                ui.Start();

               // new Thread(tcpListenMethod, null, Thread.eThreadStartOptions.Running);

            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }



        internal void StartServices()
        {
            //启动调试服务
            ILiveDebug.Instance.StartDebug("192.168.188.43", 8801, 8801);
            ILiveDebug.Instance.DebugDataReceived = client_DebugDataReceived;

           // this.cp3.RegisterDevices();
        }
    

        #region 系统调试
        void client_DebugDataReceived(INetPortDevice device, NetPortSerialDataEventArgs args)
        {
            switch (args.SerialData)
            {
                case "t1":
                    //this.logic.movie.LivingProjectorPower(true);
                    break;
      
                default:
                    break;
            }
        }
        #endregion

    }
}
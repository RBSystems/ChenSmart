﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using ILiveLib;
using Crestron.SimplSharpPro;
using ILiveLib.Remoting;

namespace ChenSmart
{
    public class ILiveRuntime
    {
        private CrestronControlSystem _controlSystem = null;

        private ILiveSmartAPI logic = null;
        private UISmart ui = null;
        ILiveRemoting remoting = null;
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


                 remoting = new ILiveRemoting(GlobalInfo.Instance.client);
              
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }



        internal void StartServices()
        {
            //启动调试服务
            ILiveDebug.Instance.StartDebug("192.168.1.35", 8801, 8801);
            ILiveDebug.Instance.DebugDataReceived = client_DebugDataReceived;

           // this.cp3.RegisterDevices();
        }
    

        #region 系统调试
        void client_DebugDataReceived(Object sender, string message, EventArgs e)
        {
            switch (message)
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
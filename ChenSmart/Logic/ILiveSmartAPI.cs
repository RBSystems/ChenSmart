using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib;
using Crestron.SimplSharpPro;

namespace ChenSmart
{
    public class ILiveSmartAPI
    {
        public bool ShowScenceIsBusy = false;
        public bool LeaveScenceIsBusy = false;
        public bool ScenceIsBusy
        {
            get
            {
                return this.LightScenceIsBusy;
            }
        }

        public bool MediaIsBusy = false;
        public bool LightScenceIsBusy = false;
        public bool TempScenceIsBusy = false;

        public bool LightIsOn = false;
        
        public bool MediaIsOn = false;
        private CP3Smart cp3 = null;
        public ILiveLight light = null;
        public ILiveMusic Muisc = null;
        public ILiveCurtains Curtains = null;

        public ILiveIRACC iracc = null;

        public ILiveSmartAPI(CrestronControlSystem system)
        {
            this.cp3 = new CP3Smart(system);
            this.cp3.RegisterDevices();
            this.light = new ILiveLight(system);
            this.light.RegisterDevices();

            this.Curtains = new ILiveCurtains();
            this.Curtains.RegisterDevices();

            this.Muisc = new ILiveMusic(system);
            this.Muisc.RegisterDevices();

            //IRACC接CP3 com1   

            ILiveComPort com = new ILiveComPort(system.ComPorts[1]);
            com.Register();
            this.iracc = new ILiveIRACC(com);

            //this.iracc = new ILiveIRACC(system.ComPorts[1]);
            

            
          //this.cp3.CongPu.Push16IEvent += new ILiveCongPu.Push16IHandler(CongPu_Push16IEvent);
        }

        void CongPu_Push16IEvent(int id, bool iChanStatus)
        {
         
            ILiveDebug.Instance.WriteLine(string.Format("ID:{0} status:{1}", id, iChanStatus));
            //throw new NotImplementedException();
        }
        
        #region 场景
        //离家
        public void ScenceLeave()
        {
            this.light.LightOneAll(false);
            Thread.Sleep(500);
            this.light.LightThreeAll(false);
            Thread.Sleep(500);
            this.light.LightFourAll(false);
            Thread.Sleep(500);
            this.light.LightFiveAll(false);
            Thread.Sleep(500);
            ////this.DaTingBu1Close();
           // Thread.Sleep(500);
            this.DaTingCeSha1Close();
            Thread.Sleep(500);
            this.DaTingBu2Close();
            Thread.Sleep(500);
            this.DaTingSha2Close();
            Thread.Sleep(500);
            this.DaTingCeBu1Close();
            Thread.Sleep(500);
            this.DaTingCeSha1Close();
            Thread.Sleep(500);
        }
        #endregion

        #region 灯光
       /* #region 一楼
        internal void LightOneAll(bool p)
        {
            this.LightOneDaDengDai(p);
            this.LightOneDaDiaoDeng(p);
            this.LightOneDaTongDeng(p);
            this.LightOneXiaoBiDeng(p);
            this.LightOneXiaoDengDai(p);
            this.LightOneXiaoDiaoDeng(p);
            this.LightOneXiaoTongDeng(p);
            this.LightOneZhongTongDeng(p);
        }
        /// <summary>
        /// 一楼大厅小吊灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneXiaoDiaoDeng(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 0, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 0, false);

            }
        }
        /// <summary>
        /// 一楼大厅大灯带
        /// </summary>
        /// <param name="on"></param>
        public void LightOneDaDengDai(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 1, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 1, false);

            }
        }
        /// <summary>
        /// 一楼大厅小壁灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneXiaoBiDeng(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 2, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 2, false);

            }
        }
        /// <summary>
        /// 一楼大厅小灯带
        /// </summary>
        /// <param name="on"></param>
        public void LightOneXiaoDengDai(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 3, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 3, false);

            }
        }
        /// <summary>
        /// 一楼大厅大吊灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneDaDiaoDeng(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 4, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(3, 4, false);

            }
        }
        /// <summary>
        /// 进门小筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneXiaoTongDeng(bool on)
        {
            if (on)
            {
                this.cp3.dim4_3.DinLoads[1].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.cp3.dim4_3.DinLoads[1].LevelIn.UShortValue = 0;

            }
        }
        /// <summary>
        /// 大筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneDaTongDeng(bool on)
        {
            if (on)
            {
                this.cp3.dim4_3.DinLoads[2].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.cp3.dim4_3.DinLoads[2].LevelIn.UShortValue = 0;

            }
        }
        /// <summary>
        /// 中
        /// </summary>
        /// <param name="on"></param>
        public void LightOneZhongTongDeng(bool on)
        {
            if (on)
            {
                this.cp3.dim4_3.DinLoads[3].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.cp3.dim4_3.DinLoads[3].LevelIn.UShortValue = 0;

            }
        }
        #endregion
        #region 四楼
        internal void LightThreeAll(bool p)
        {
            this.LightThreeBiDeng(p);
            this.LightThreeChuanTou(p);
            this.LightThreeDengDai(p);
            this.LightThreeDiaoDeng(p);
            this.LightThreeJinMen(p);
            this.LightThreeKongTiao(p);
        }
        /// <summary>
        /// 3楼吊灯
        /// </summary>
        /// <param name="on"></param>
        public void LightThreeDiaoDeng(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 0, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 0, false);

            }
        }
        /// <summary>
        /// 3楼壁灯
        /// </summary>
        /// <param name="on"></param>
        public void LightThreeBiDeng(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 1, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 1, false);

            }
        }
        /// <summary>
        /// 3楼灯带
        /// </summary>
        /// <param name="on"></param>
        public void LightThreeDengDai(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 2, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 2, false);

            }
        }
        /// <summary>
        /// 3楼空调口筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightThreeKongTiao(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 3, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 3, false);

            }
        }
        /// <summary>
        /// 3楼进门筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightThreeJinMen(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 4, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 4, false);

            }
        }
        /// <summary>
        /// 3楼床头筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightThreeChuanTou(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 5, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 5, false);

            }
        }

        internal void LightFourAll(bool p)
        {
            this.LightFourBiDeng(p);
            this.LightFourDengDai(p);
            this.LightFourChuanTou(p);
            
            this.LightFourDiaoDeng(p);
            this.LightFourJinMen(p);
            this.LightFourKongTiao(p);
        }


        /// <summary>
        /// 4楼空调口筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightFourKongTiao(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 6, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 6, false);

            }

        }
        /// <summary>
        /// 4楼床头筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightFourChuanTou(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 7, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(4, 7, false);

            }
        }
        /// <summary>
        /// 4楼壁灯
        /// </summary>
        /// <param name="on"></param>
        public void LightFourBiDeng(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 0, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 0, false);

            }
        }
        /// <summary>
        /// 4楼吊灯
        /// </summary>
        /// <param name="on"></param>
        public void LightFourDiaoDeng(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 1, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 1, false);

            }
        }
        /// <summary>
        /// 4楼进门筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightFourJinMen(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 2, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 2, false);

            }
        }
        /// <summary>
        /// 4楼灯带
        /// </summary>
        /// <param name="on"></param>
        public void LightFourDengDai(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 3, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 3, false);

            }
        }
        internal void LightFiveAll(bool p)
        {
            this.LightFiveBiDeng(p);
            this.LightFiveDengDai(p);
            this.LightFiveDiaoDeng(p);
            this.LightFiveJinMen(p);
            this.LightFiveKongTiao(p);
            this.cp3.grodigy8SW8.Relay8SW8(5, 6, p);//5楼床头筒灯


        }
        //5楼床头
        public void LightFiveChuanTou(bool on) 
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 6, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 6, false);
            }
        }
        /// <summary>
        /// 5楼灯带
        /// </summary>
        /// <param name="on"></param>
        public void LightFiveDengDai(bool on)
        {
            if (on)
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 5, true);
            }
            else
            {
                this.cp3.grodigy8SW8.Relay8SW8(5, 5, false);
            }
        }
        /// <summary>
        /// 5楼空调口
        /// </summary>
        /// <param name="on"></param>
        public void LightFiveKongTiao(bool on)
        {
            if (on)
            {
                this.cp3.dim4_4.DinLoads[1].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.cp3.dim4_4.DinLoads[1].LevelIn.UShortValue = 0;

            }
        }

        public void LightFiveDiaoDeng(bool on)
        {
            if (on)
            {
                this.cp3.dim4_4.DinLoads[2].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.cp3.dim4_4.DinLoads[2].LevelIn.UShortValue = 0;

            }
        }

        public void LightFiveBiDeng(bool on)
        {
            if (on)
            {
                this.cp3.dim4_4.DinLoads[3].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.cp3.dim4_4.DinLoads[3].LevelIn.UShortValue = 0;

            }
        }

        public void LightFiveJinMen(bool on)
        {
            if (on)
            {
                this.cp3.dim4_4.DinLoads[4].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.cp3.dim4_4.DinLoads[4].LevelIn.UShortValue = 0;

            }
        }
        #endregion*/
        #endregion

        #region 窗帘
        #region 布帘1
        //public void DaTingBu1Open() 
        //{
        //    this.cp3.relayWindow1Open.Close();
        //    Thread.Sleep(1000);
        //    this.cp3.relayWindow1Open.Open();
        //}
        //public void DaTingBu1Close()
        //{
        //    this.cp3.relayWindow1Close.Close();
        //    Thread.Sleep(1000);
        //    this.cp3.relayWindow1Close.Open();
        //}
        //public void DaTingBu1Stop()
        //{
        //    this.cp3.relayWindow1Open.Close();
        //    Thread.Sleep(200);
        //    this.cp3.relayWindow1Close.Close();
        //}
        #endregion
        #region 纱帘1
        public void DaTingSha1Open()
        {
            this.cp3.relayWindow2Open.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow2Open.Open();
        }
        public void DaTingSha1Close()
        {
            this.cp3.relayWindow2Close.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow2Close.Open();
        }
        public void DaTingSha1Stop()
        {
            this.cp3.relayWindow2Open.Close();
            Thread.Sleep(200);
            this.cp3.relayWindow2Close.Close();
        }
        #endregion
        #region 布帘2
        public void DaTingBu2Open()
        {
            this.cp3.relayWindow3Open.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow3Open.Open();
        }
        public void DaTingBu2Close()
        {
            this.cp3.relayWindow3Close.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow3Close.Open();
        }
        public void DaTingBu2Stop()
        {
            this.cp3.relayWindow3Open.Close();
            Thread.Sleep(200);
            this.cp3.relayWindow3Close.Close();
        }
        #endregion
        #region 纱帘2
        public void DaTingSha2Open()
        {
            this.cp3.relayWindow4Open.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow4Open.Open();
        }
        public void DaTingSha2Close()
        {
            this.cp3.relayWindow4Close.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow4Close.Open();
        }
        public void DaTingSha2Stop()
        {
            this.cp3.relayWindow4Open.Close();
            Thread.Sleep(200);
            this.cp3.relayWindow4Close.Close();
        }
        #endregion
        #region 侧边布帘
        public void DaTingCeBu1Open()
        {
            this.cp3.relayWindow5Open.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow5Open.Open();
        }
        public void DaTingCeBu1Close()
        {
            this.cp3.relayWindow5Close.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow5Close.Open();
        }
        public void DaTingCeBu1Stop()
        {
            this.cp3.relayWindow5Open.Close();
            Thread.Sleep(200);
            this.cp3.relayWindow5Close.Close();
        }
        #endregion
        #region 侧边纱帘
        public void DaTingCeSha1Open()
        {
            this.cp3.relayWindow6Open.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow6Open.Open();
        }
        public void DaTingCeSha1Close()
        {
            this.cp3.relayWindow6Close.Close();
            Thread.Sleep(1000);
            this.cp3.relayWindow6Close.Open();
        }
        public void DaTingCeSha1Stop()
        {
            this.cp3.relayWindow6Open.Close();
            Thread.Sleep(200);
            this.cp3.relayWindow6Close.Close();
        }
        #endregion
        #endregion
        #region 音乐

        #endregion
        #region 安防
        public void DoorMuMenOpen()
        {
            this.cp3.relay1.Close();
            Thread.Sleep(500);
            this.cp3.relay1.Open();
        }
        #endregion
        #region 空调
        //internal void ClimatePower(int group, bool on)
        //{
        //    this.cp3.iracc.SendIRACCPower(group, on, GlobalInfo.Instance.CurrentClimateFL);
        //}
        #endregion

        #region 函数
        private string GetMusicPlayerCMDString(params byte[] sendBytes)
        {
            int sum = 0;
            for (int i = 0; i < sendBytes.Length; i++)
            {
                sum += sendBytes[i];
            }
            sum = 0 - sum;
            int hValue = (sum >> 8) & 0xFF;
            int lValue = sum & 0xFF;
            byte[] arr = new byte[sendBytes.Length + 4];
            Buffer.BlockCopy(sendBytes, 0, arr, 1, sendBytes.Length);
            Buffer.SetByte(arr, 0, 0x7E);
            Buffer.SetByte(arr, sendBytes.Length + 1, (byte)hValue);
            Buffer.SetByte(arr, sendBytes.Length + 2, (byte)lValue);
            Buffer.SetByte(arr, sendBytes.Length + 3, 0xEF);
            return Encoding.GetEncoding(28591).GetString(arr, 0, arr.Length);

        }

        /// <summary>
        /// auxdio功放
        /// </summary>
        /// <param name="room">房间号：00-09 </param>
        /// <param name="source">节目源 mp3 01 tuner 02 dvd 03 pc 04 tv 05 aux 06</param>
        /// <param name="fun">功能
        /// 开启：A1 关闭：A0 查询：D2 停止：A4 下一曲：A6 上一曲：A5 播放：A2 音量：A7
        /// </param>
        /// <param name="p">参数</param>
        /// <returns></returns>
        private string GetMusicAVCMDString(byte room, byte source, byte fun, byte p)
        {
            /*
         *   起始位：FF
             *   房间号：00-09 
         *     未知：00 （节目源） /
         *    功能：
         *     未知：00 音量+：01 音量-：00
         *   校验位：除去起始和终止 
         *     终止：FE
         *     
         * 房间打开 FF 00 00 A1 00 A1 FE./
         * 房间关闭 FF 00 00 A0 00 A0 FE
         */
            byte check = (byte)(room + source + fun + p);
            byte[] data = new byte[] { 0xFF, room, source, fun, p, check, 0xFE };

            return Encoding.GetEncoding(28591).GetString(data, 0, data.Length);

        }
        private string GetCMDString(params byte[] sendBytes)
        {
            // sendBytes = new byte[] { 0x01, 0x06, 0x07, 0xDC, 0x50, 0x61, 0xB4, 0xAC };
            return Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
        }
        #endregion

        public string GetWeather()
        {
            string ret = "21";
            try
            {
                HttpUtil http = new HttpUtil();
                ret = http.GetWenDu();
            }
            catch (Exception ex)
            {

                ILiveDebug.Instance.WriteLine(ex.Message);
            }

            return ret;
        }
















    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Newtonsoft.Json.Linq;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib.WebSocketServer;
using ILiveLib;

namespace ChenSmart
{
    public class ILiveIpad
    {
        private ILiveSmartAPI _logic = null;

        public IPadWebSocketServer ipad;
        public ILiveIpad(ILiveSmartAPI logic)
        {
            this._logic = logic;
        }

        public void RegisterDevices()
        {

            #region 注册网络设备

            #region 注册IPad（WebSocket）
            this.ipad = new IPadWebSocketServer();
            this.ipad.DataReceived += new IPadWebSocketServer.DataEventHandler(Ipad_DataReceived);
            this.ipad.Register();
            #endregion
            #endregion

        }

        //void music8250_MusicScenceEvent(bool val)
        //{
        //    //this.ipad.WSServer_Send("MusicPower", new { state = val });
        //}

        //void media_MediaZoneEvent(int zone, bool vl)
        //{
        //    //this.ipad.WSServer_Send("Media", new { zone = zone, state = vl });
        //}

        //void _logic_ScenceEvent(string evt)
        //{
        //    //this.ipad.WSServer_Send("Scence", new { data = evt });
        //}

        //void light_ZoneLightEvent(int zone, bool vl)
        //{
        //    //this.ipad.WSServer_Send("Light", new { zone = zone, state = vl });
        //}


        #region IPAD事件

        void Ipad_DataReceived(string service, JObject data)
        {
            try
            {

                switch (service)
                {
                    case "scence":
                        this.ScencePorcess(data.Value<string>("data"));
                        break;
                    case "light":
                        this.LightProcess(data.Value<string>("zone"), data.Value<bool>("value"));
                        break;
                    case "temp":
                        this.TempProcess(data.Value<string>("data"));
                        break;
                    case "curtain":
                        this.CurtainProcess(data.Value<string>("data"));
                        break;
                    case "music":
                        this.MusicProcess(data.Value<string>("data"));
                        break;

                    default:
                        break;
                }


            }
            catch (Exception)
            {
                //ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }


        //private void SendInitData(string p)
        //{
        //    if (p == "Init")
        //    {
        //        //this._logic.light.Zone1State = this._logic.light.Zone1State;

        //    }
        //}
        

        #region 场景处理
        /// <summary>
        /// 场景处理
        /// </summary>
        /// <param name="name"></param>
        private void ScencePorcess(string name)
        {
            switch (name)
            {
                #region 一楼场景
                //CC 5.9
                //一楼全开                
                case "OneQuanKai":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "OneQuanKai");
                        this._logic.light.LightOneAllOn();
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                //一楼全关
                case "OneQuanGuan":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "OneQuanGuan");
                        this._logic.light.LightOneAllOff();
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                //一楼照明
                case "OneZhaoMing":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "OneZhaoMing");
                        this._logic.light.LightScenceZM();
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                //一楼休闲
                case "OneXiuXian":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "OneXiuXian");
                        this._logic.light.LightScenceXX();
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                #endregion
                #region 三楼场景
                //三楼全开
                case "ThreeQuanKai":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "ThreeQuanKai");
                        this._logic.light.LightThreeAll(true);
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                //三楼全关
                case "ThreeQuanGuan":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "ThreeQuanGuan");
                        this._logic.light.LightThreeAll(false);
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                #endregion
                #region 四楼场景
                //四楼全开
                case "FourQuanKai":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "FourQuanKai");
                        this._logic.light.LightFourAll(true);
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                //四楼全关
                case "FourQuanGuan":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "FourQuanGuan");
                        this._logic.light.LightFourAll(false);
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                #endregion
                #region 五楼场景
                //五楼全开
                case "FiveQuanKai":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "FiveQuanKai");
                        this._logic.light.LightFiveAll(true);
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                //五楼全关
                case "FiveQuanGuan":
                    if (!this._logic.ScenceIsBusy)
                    {
                        //new Thread(new ThreadCallbackFunction(this._logic.LightOneAllOn), this, Thread.eThreadStartOptions.Running);
                        this.ipad.WSServer_Send("Scence", "FiveQuanGuan");
                        this._logic.light.LightFiveAll(false);
                    }
                    else
                    {
                        this.ipad.WSServer_Send("SystemBusy", new { status = 0, msg = "SystemBusy" });

                    }
                    break;
                #endregion



                default:
                    break;
            }
        }
        #endregion
        #region 灯光控制
        /// <summary>
        /// 灯光控制
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="value"></param>
        private void LightProcess(string zone, bool value)
        {
            //this.ipad.WSServer_Send("Light", new { zone = zone, value = value });

            if (value)
            {
                switch (zone)
                {
                    #region 一楼灯光开
                    //小吊灯
                    case "OneXiaoDiaoDengOn":
                        this._logic.light.LightOneXiaoDiaoDeng(true);
                        break;
                        //大吊灯
                    case "OneDaDiaoDengOn":
                        // this._logic.light.LightZone1On();
                        this._logic.light.LightOneDaDiaoDeng(true);
                        break;
                        //小灯带
                    case "OneXiaoDengDaiOn":
                        this._logic.light.LightOneXiaoDengDai(true);
                        break;
                        //大灯带
                    case "OneDaDengDaiOn":
                        this._logic.light.LightOneDaDengDai(true);
                        break;
                        //壁灯
                    case "OneBiDengOn":
                        this._logic.light.LightOneBiDeng(true);
                        break;
                        //筒灯一
                    case "OneTongDeng1On":
                        this._logic.light.LightOneXiaoTongDeng=65535;
                        break;
                        //筒灯二
                    case "OneTongDeng2On":
                        this._logic.light.LightOneZhongTongDeng = 65535;
                        break;
                        //筒灯三
                    case"OneTongDeng3On":
                        this._logic.light.LightOneDaTongDeng = 65535;
                        break;
                    #endregion
                    #region 一楼调光灯
                    case "OneTongDeng1Down":
                        //筒灯一 暗
                        this._logic.light.LightOneXiaoTongDeng -= 6553;
                        break;
                    case "OneTongDeng1Up":
                        //筒灯一 亮
                        this._logic.light.LightOneXiaoTongDeng += 6553;
                        break;
                    case "OneTongDeng2Down":
                        //筒灯二 暗
                        this._logic.light.LightOneZhongTongDeng -= 6553;
                        break;
                    case "OneTongDeng2Up":
                        //筒灯二 亮
                        this._logic.light.LightOneZhongTongDeng += 6553;
                        break;
                    case "OneTongDeng3Down":
                        //筒灯三 暗
                        this._logic.light.LightOneDaTongDeng -= 6553;
                        break;
                    case "OneTongDeng3Up":
                        //筒灯三 亮
                        this._logic.light.LightOneDaTongDeng += 6553;
                        break;
                    #endregion
                    #region 三楼灯光开
                    case "ThreeDiaoDengOn":
                        this._logic.light.LightThreeDiaoDeng(true);
                        break;
                    case "ThreeBiDengOn":
                        this._logic.light.LightThreeBiDeng(true);
                        break;
                    case "ThreeDengDaiOn":
                        this._logic.light.LightThreeDengDai(true);
                        break;
                    case "ThreeTempKouOn":
                        this._logic.light.LightThreeKongTiao(true);
                        break;
                    case "ThreeJinMenOn":
                        this._logic.light.LightThreeJinMen(true);
                        break;
                    case "ThreeChuangTouOn":
                        this._logic.light.LightThreeChuanTou(true);
                        break;                    
                    #endregion
                    #region 四楼灯光开
                    case "FourDiaoDengOn":
                        this._logic.light.LightFourDiaoDeng(true);
                        break;
                    case "FourBiDengOn":
                        this._logic.light.LightFourBiDeng(true);
                        break;
                    case "FourDengDaiOn":
                        this._logic.light.LightFourDengDai(true);
                        break;
                    case "FourTempKouOn":
                        this._logic.light.LightFourKongTiao(true);
                        break;
                    case "FourJinMenOn":
                        this._logic.light.LightFourJinMen(true);
                        break;
                    case "FourChuangTouOn":
                        this._logic.light.LightFourChuanTou(true);
                        break;
                    #endregion
                    #region 五楼灯光开
                    case "FiveDiaoDengOn":
                        this._logic.light.LightFiveDiaoDeng = 65535;
                        break;
                    case "FiveBiDengOn":
                        this._logic.light.LightFiveBiDeng = 65535;
                        break;
                    case "FiveDengDaiOn":
                        this._logic.light.LightFiveDengDai(true);
                        break;
                    case "FiveTempKouOn":
                        this._logic.light.LightFiveKongTiao = 65535;
                        break;
                    case "FiveJinMenOn":
                        this._logic.light.LightFiveJinMen = 65535;
                        break;
                    #endregion
                    #region 五楼调光灯
                    case "FiveDiaoDengDown":
                        //吊灯 暗
                        this._logic.light.LightFiveDiaoDeng -= 6553;
                        break;
                    case "FiveDiaoDengUp":
                        //吊灯 亮
                        this._logic.light.LightFiveDiaoDeng += 6553;
                        break;
                    case "FiveBiDengDown":
                        //壁灯 暗
                        this._logic.light.LightFiveBiDeng -= 6553;
                        break;
                    case "FiveBiDengUp":
                        //壁灯 亮
                        this._logic.light.LightFiveBiDeng += 6553;
                        break;
                    case "FiveTempKouDown":
                        //空调口 暗
                        this._logic.light.LightFiveKongTiao -= 6553;
                        break;
                    case "FiveTempKouUp":
                        //空调口 亮
                        this._logic.light.LightFiveKongTiao += 6553;
                        break;
                    case "FiveJinMenDown":
                        //进门 暗
                        this._logic.light.LightFiveJinMen -= 6553;
                        break;
                    case "FiveJinMenUp":
                        //进门 亮
                        this._logic.light.LightFiveJinMen += 6553;
                        break;
                    #endregion

                    default:
                        break;
                }
            }
            else
            {
                switch (zone)
                {
                    #region 一楼灯光关
                    //小吊灯
                    case "OneXiaoDiaoDengOff":
                        this._logic.light.LightOneXiaoDiaoDeng(false);
                        break;
                    //大吊灯
                    case "OneDaDiaoDengOff":
                        // this._logic.light.LightZone1On();
                        this._logic.light.LightOneDaDiaoDeng(false);
                        break;
                    //小灯带
                    case "OneXiaoDengDaiOff":
                        this._logic.light.LightOneXiaoDengDai(false);
                        break;
                    //大灯带
                    case "OneDaDengDaiOff":
                        this._logic.light.LightOneDaDengDai(false);
                        break;
                    //壁灯
                    case "OneBiDengOff":
                        this._logic.light.LightOneBiDeng(false);
                        break;
                    //筒灯一
                    case "OneTongDeng1Off":
                        this._logic.light.LightOneXiaoTongDeng = 0;
                        break;
                    //筒灯二
                    case "OneTongDeng2Off":
                        this._logic.light.LightOneZhongTongDeng = 0;
                        break;
                    //筒灯三
                    case "OneTongDeng3Off":
                        this._logic.light.LightOneDaTongDeng = 0;
                        break;
                    #endregion
                    #region 三楼灯光关
                    case "ThreeDiaoDengOff":
                        this._logic.light.LightThreeDiaoDeng(false);
                        break;
                    case "ThreeBiDengOff":
                        this._logic.light.LightThreeBiDeng(false);
                        break;
                    case "ThreeDengDaiOff":
                        this._logic.light.LightThreeDengDai(false);
                        break;
                    case "ThreeTempKouOff":
                        this._logic.light.LightThreeKongTiao(false);
                        break;
                    case "ThreeJinMenOff":
                        this._logic.light.LightThreeJinMen(false);
                        break;
                    case "ThreeChuangTouOff":
                        this._logic.light.LightThreeChuanTou(false);
                        break;
                    #endregion
                    #region 四楼灯光关
                    case "FourDiaoDengOff":
                        this._logic.light.LightFourDiaoDeng(false);
                        break;
                    case "FourBiDengOff":
                        this._logic.light.LightFourBiDeng(false);
                        break;
                    case "FourDengDaiOff":
                        this._logic.light.LightFourDengDai(false);
                        break;
                    case "FourTempKouOff":
                        this._logic.light.LightFourKongTiao(false);
                        break;
                    case "FourJinMenOff":
                        this._logic.light.LightFourJinMen(false);
                        break;
                    case "FourChuangTouOff":
                        this._logic.light.LightFourChuanTou(false);
                        break;
                    #endregion
                    #region 五楼灯光关
                    case "FiveDiaoDengOff":
                        this._logic.light.LightFiveDiaoDeng = 0;
                        break;
                    case "FiveBiDengOff":
                        this._logic.light.LightFiveBiDeng = 0;
                        break;
                    case "FiveDengDaiOff":
                        this._logic.light.LightFiveDengDai(false);
                        break;
                    case "FiveTempKouOff":
                        this._logic.light.LightFiveKongTiao = 0;
                        break;
                    case "FiveJinMenOff":
                        this._logic.light.LightFiveJinMen = 0;
                        break;
                    #endregion


                    default:
                        break;
                }
            }
        }
        #endregion
        #region 窗帘控制
        private void CurtainProcess(string name)
        {
            switch (name)
            {
                case "ZuoShaLianOn":
                    this._logic.Curtains.Windows1Open();
                    break;
                case "ZuoShaLianStop":
                    this._logic.Curtains.Windows1Stop();
                    break;
                case "ZuoShaLianOff":
                    this._logic.Curtains.Windows1Close();
                    break;
                case "ZuoBuLianOn":
                    this._logic.Curtains.Windows2Open();
                    break;
                case "ZuoBuLianStop":
                    this._logic.Curtains.Windows2Stop();
                    break;
                case "ZuoBuLianOff":
                    this._logic.Curtains.Windows2Close();
                    break;
                case "YouShaLianOn":
                    this._logic.Curtains.Windows3Open();
                    break;
                case "YouShaLianStop":
                    this._logic.Curtains.Windows3Stop();
                    break;
                case "YouShaLianOff":
                    this._logic.Curtains.Windows3Close();
                    break;
                case "YouBuLianOn":
                    this._logic.Curtains.Windows4Open();
                    break;
                case "YouBuLianStop":
                    this._logic.Curtains.Windows4Stop();
                    break;
                case "YouBuLianOff":
                    this._logic.Curtains.Windows4Close();
                    break;
                case "CeShaLianOn":
                    this._logic.Curtains.Windows5Open();
                    break;
                case "CeShaLianStop":
                    this._logic.Curtains.Windows5Stop();
                    break;
                case "CeShaLianOff":
                    this._logic.Curtains.Windows5Close();
                    break;
                case "CeBuLianOn":
                    this._logic.Curtains.Windows6Open();
                    break;
                case "CeBuLianStop":
                    this._logic.Curtains.Windows6Stop();
                    break;
                case "CeBuLianOff":
                    this._logic.Curtains.Windows6Close();
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region 音乐控制
        private void MusicProcess(string name)
        {
            switch (name)
            {
                #region 地下室音乐
                case "DiXiaShiOn":
                    this._logic.Muisc.MusicPower(5, true);                    
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case "DiXiaShiOff":
                    this._logic.Muisc.MusicPower(5, false);
                    break;
                case "DiXiaShiUp":
                    GlobalInfo.Instance.MusicVol5 += 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol5);

                    break;
                case "DiXiaShiDown":
                    GlobalInfo.Instance.MusicVol5 -= 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol5);


                    break;
                case "DiXiaShiLast"://上一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x01, 0x81);
                    break;
                case "DiXiaShiNext"://下一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x10, 0x81);

                    break;
                case "DiXiaShiPause"://暂停
                    this._logic.Muisc.PlaySet(5, 0x02, 0x81);
                    break;
                case "DiXiaShiPlay"://播放
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case "DiXiaShiLieBiao"://列表
                    this._logic.Muisc.PlayModeSet(5, 0x03);
                    break;
                case "DiXiaShiDanQu"://单曲
                    this._logic.Muisc.PlayModeSet(5, 0x02);

                    break;
                case "DiXiaShiSuiJi"://随机
                    this._logic.Muisc.PlayModeSet(5, 0x05);
                    break;
                case "DiXiaShiStop"://停止
                    this._logic.Muisc.PlaySet(5, 0x04, 0x81);
                    break; 
                #endregion
                #region 茶室音乐
                case "ChaShiOn":
                    this._logic.Muisc.MusicPower(7, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case "ChaShiOff":
                    this._logic.Muisc.MusicPower(7, false);
                    break;
                case "ChaShiUp":
                    GlobalInfo.Instance.MusicVol7 += 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol7);

                    break;
                case "ChaShiDown":
                    GlobalInfo.Instance.MusicVol7 -= 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol7);


                    break;
                case "ChaShiLast"://上一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x01, 0x81);
                    break;
                case "ChaShiNext"://下一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x10, 0x81);

                    break;
                case "ChaShiPause"://暂停
                    this._logic.Muisc.PlaySet(7, 0x02, 0x81);
                    break;
                case "ChaShiPlay"://播放
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case "ChaShiLieBiao"://列表
                    this._logic.Muisc.PlayModeSet(7, 0x03);
                    break;
                case "ChaShiDanQu"://单曲
                    this._logic.Muisc.PlayModeSet(7, 0x02);

                    break;
                case "ChaShiSuiJi"://随机
                    this._logic.Muisc.PlayModeSet(7, 0x05);
                    break;
                case "ChaShiStop"://停止
                    this._logic.Muisc.PlaySet(7, 0x04, 0x81);
                    break; 
                #endregion
                #region 户外音乐
                case "HuWaiOn":
                    this._logic.Muisc.MusicPower(1, true);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(2, true);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(3, true);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(4, true);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(1, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x01, 0x81);
                    break;
                case "HuWaiOff":
                    this._logic.Muisc.MusicPower(1, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(2, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(3, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(4, false);
                    Thread.Sleep(300);
                    break;
                case "HuWaiUp":
                    GlobalInfo.Instance.MusicVol1 += 10;

                    this._logic.Muisc.VolSet(1, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(2, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(3, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(4, (byte)GlobalInfo.Instance.MusicVol1);
                    break; 
                case "HuWaiDown":
                    GlobalInfo.Instance.MusicVol1 -= 10;


                    this._logic.Muisc.VolSet(1, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(2, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(3, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(4, (byte)GlobalInfo.Instance.MusicVol1);


                    break;
                case "HuWaiLast"://上一曲
                    this._logic.Muisc.MusicChangeSet(1, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(2, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(3, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(4, 0x01, 0x81);
                    break;
                case "HuWaiNext"://下一曲
                    this._logic.Muisc.MusicChangeSet(1, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(2, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(3, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(4, 0x10, 0x81);

                    break;
                case "HuWaiPause"://暂停
                    this._logic.Muisc.PlaySet(1, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x02, 0x81);
                    break;
                case "HuWaiPlay"://播放
                    this._logic.Muisc.PlaySet(1, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x01, 0x81);
                    break;
                case "HuWaiLieBiao"://列表
                    this._logic.Muisc.PlayModeSet(1, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x03);
                    break;
                case "HuWaiDanQu"://单曲
                    this._logic.Muisc.PlayModeSet(1, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x02);

                    break;
                case "HuWaiSuiJi"://随机
                    this._logic.Muisc.PlayModeSet(1, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x05);
                    break;
                case "HuWaiStop"://停止
                    this._logic.Muisc.PlaySet(1, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x04, 0x81);
                    break; 
                #endregion
                #region 阳台音乐
                case "YangTaiOn":
                    this._logic.Muisc.MusicPower(6, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(6, 0x01, 0x81);
                    break;
                case "YangTaiOff":
                    this._logic.Muisc.MusicPower(6, false);
                    break;
                case "YangTaiUp":
                    GlobalInfo.Instance.MusicVol6 += 10;

                    this._logic.Muisc.VolSet(6, (byte)GlobalInfo.Instance.MusicVol6);

                    break;
                case "YangTaiDown":
                    GlobalInfo.Instance.MusicVol6 -= 10;

                    this._logic.Muisc.VolSet(6, (byte)GlobalInfo.Instance.MusicVol6);


                    break;
                case "YangTaiLast"://上一曲
                    this._logic.Muisc.MusicChangeSet(6, 0x01, 0x81);
                    break;
                case "YangTaiNext"://下一曲
                    this._logic.Muisc.MusicChangeSet(6, 0x10, 0x81);

                    break;
                case "YangTaiPause"://暂停
                    this._logic.Muisc.PlaySet(6, 0x02, 0x81);
                    break;
                case "YangTaiPlay"://播放
                    this._logic.Muisc.PlaySet(6, 0x01, 0x81);
                    break;
                case "YangTaiLieBiao"://列表
                    this._logic.Muisc.PlayModeSet(6, 0x03);
                    break;
                case "YangTaiDanQu"://单曲
                    this._logic.Muisc.PlayModeSet(6, 0x02);

                    break;
                case "YangTaiSuiJi"://随机
                    this._logic.Muisc.PlayModeSet(6, 0x05);
                    break;
                case "YangTaiStop"://停止
                    this._logic.Muisc.PlaySet(6, 0x04, 0x81);
                    break; 
                #endregion

                default:
                    break;
            }
        }
        #endregion        
        #region 空调控制
        private void TempProcess(string name)
        {
            #region 空调temp
            var temp0 = GlobalInfo.Instance.CurrentClimateTemp0;
            var temp1 = GlobalInfo.Instance.CurrentClimateTemp1;
            var temp2 = GlobalInfo.Instance.CurrentClimateTemp2;
            var temp3 = GlobalInfo.Instance.CurrentClimateTemp3;
            var temp4 = GlobalInfo.Instance.CurrentClimateTemp4;
            var temp5 = GlobalInfo.Instance.CurrentClimateTemp5;
            var temp6 = GlobalInfo.Instance.CurrentClimateTemp6;
            var temp7 = GlobalInfo.Instance.CurrentClimateTemp7;
            var temp8 = GlobalInfo.Instance.CurrentClimateTemp8;
            var temp9 = GlobalInfo.Instance.CurrentClimateTemp9;
            var temp10 = GlobalInfo.Instance.CurrentClimateTemp10;
            var temp11 = GlobalInfo.Instance.CurrentClimateTemp11;
            var temp12 = GlobalInfo.Instance.CurrentClimateTemp12;
            var temp13 = GlobalInfo.Instance.CurrentClimateTemp13;
            var temp14 = GlobalInfo.Instance.CurrentClimateTemp14;
            var temp15 = GlobalInfo.Instance.CurrentClimateTemp15;
            var temp16 = GlobalInfo.Instance.CurrentClimateTemp16;
            var temp17 = GlobalInfo.Instance.CurrentClimateTemp17;
            var temp18 = GlobalInfo.Instance.CurrentClimateTemp18;
            var temp19 = GlobalInfo.Instance.CurrentClimateTemp19;
            var temp20 = GlobalInfo.Instance.CurrentClimateTemp20;
            var temp21 = GlobalInfo.Instance.CurrentClimateTemp21;
            var temp22 = GlobalInfo.Instance.CurrentClimateTemp22;
            var temp23 = GlobalInfo.Instance.CurrentClimateTemp23;
            var temp24 = GlobalInfo.Instance.CurrentClimateTemp24;
            var temp25 = GlobalInfo.Instance.CurrentClimateTemp25;
            var temp26 = GlobalInfo.Instance.CurrentClimateTemp26;
            var temp27 = GlobalInfo.Instance.CurrentClimateTemp27;
            var temp28 = GlobalInfo.Instance.CurrentClimateTemp28;
            var temp29 = GlobalInfo.Instance.CurrentClimateTemp29;
            var temp30 = GlobalInfo.Instance.CurrentClimateTemp30;
            var temp31 = GlobalInfo.Instance.CurrentClimateTemp31;
            var temp32 = GlobalInfo.Instance.CurrentClimateTemp32;
            var temp33 = GlobalInfo.Instance.CurrentClimateTemp33;
            var temp34 = GlobalInfo.Instance.CurrentClimateTemp34;
            var temp35 = GlobalInfo.Instance.CurrentClimateTemp35;
            var temp36 = GlobalInfo.Instance.CurrentClimateTemp36;
            var temp37 = GlobalInfo.Instance.CurrentClimateTemp37;
            var temp38 = GlobalInfo.Instance.CurrentClimateTemp38;
            var temp39 = GlobalInfo.Instance.CurrentClimateTemp39;
            var temp40 = GlobalInfo.Instance.CurrentClimateTemp40;
            var temp41 = GlobalInfo.Instance.CurrentClimateTemp41;
            var temp42 = GlobalInfo.Instance.CurrentClimateTemp42;
            var temp43 = GlobalInfo.Instance.CurrentClimateTemp43;
            var temp44 = GlobalInfo.Instance.CurrentClimateTemp44;
            var temp45 = GlobalInfo.Instance.CurrentClimateTemp45;
            var temp46 = GlobalInfo.Instance.CurrentClimateTemp46;
            var temp47 = GlobalInfo.Instance.CurrentClimateTemp47;
            var temp48 = GlobalInfo.Instance.CurrentClimateTemp48;
            var temp49 = GlobalInfo.Instance.CurrentClimateTemp49;
            #endregion
            switch (name)
            {
                #region 地下室
                #region 视听室门口
                case "BasementSTSMKOn"://开机
                    this._logic.iracc.SendIRACCPower(0, true);
                    break;
                case "BasementSTSMKOff"://关机
                    this._logic.iracc.SendIRACCPower(0, false);
                    break;
                case "BasementSTSMKZd"://自动
                    this._logic.iracc.SendIRACCSetMode(0, IRACCMode.ZD);
                    break;
                case "BasementSTSMKCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(0, IRACCMode.ZL);
                    break;
                case "BasementSTSMKHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(0, IRACCMode.ZR);
                    break;
                case "BasementSTSMKHigh"://高风
                    this._logic.iracc.SendIRACCFL(0, IRACCFL.HH);
                    break;
                case "BasementSTSMKIn"://中风
                    this._logic.iracc.SendIRACCFL(0, IRACCFL.M);
                    break;
                case "BasementSTSMKLow"://低风
                    this._logic.iracc.SendIRACCFL(0, IRACCFL.LL);
                    break;
                case "BasementSTSMKUp"://温度加
                    temp0 = GlobalInfo.Instance.CurrentClimateTemp0++;
                    if (temp0 < 31 && temp0 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(0, temp0);
                    }
                    else if (temp0 == 31)
                    {
                        temp0 = 30;
                        this._logic.iracc.SendIRACCTemp(0, temp0);
                    }
                    else if (temp0 == 17)
                    {
                        temp0 = 18;
                        this._logic.iracc.SendIRACCTemp(0, temp0);
                    }
                    //发送温度
                    //this.ipad.WSServer_Send(string.Format("wd{0}", temp0));
                    break;
                case "BasementSTSMKDown"://温度减
                    temp0 = GlobalInfo.Instance.CurrentClimateTemp0--;
                    if (temp0 < 31 && temp0 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(0, temp0);
                    }
                    else if (temp0 == 31)
                    {
                        temp0 = 30;
                        this._logic.iracc.SendIRACCTemp(0, temp0);
                    }
                    else if (temp0 == 17)
                    {
                        temp0 = 18;
                        this._logic.iracc.SendIRACCTemp(0, temp0);
                    }
                    //发送温度
                    //this.ipad.WSServer_Send(string.Format("wd{0}", temp0));
                    break;
                #endregion
                #region 视听室
                case "BasementSTSOn"://开机
                    this._logic.iracc.SendIRACCPower(1, true);
                    this.ipad.WSServer_Send("received");
                    //this._logic.iracc.SendIRACCTest();
                    break;
                case "BasementSTSOff"://关机
                    this._logic.iracc.SendIRACCPower(1, false);
                    break;
                case "BasementSTSZd"://自动
                    this._logic.iracc.SendIRACCSetMode(1, IRACCMode.ZD);
                    break;
                case "BasementSTSCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(1, IRACCMode.ZL);
                    break;
                case "BasementSTSHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(1, IRACCMode.ZR);
                    break;
                case "BasementSTSHigh"://高风
                    this._logic.iracc.SendIRACCFL(1, IRACCFL.HH);
                    break;
                case "BasementSTSIn"://中风
                    this._logic.iracc.SendIRACCFL(1, IRACCFL.M);
                    break;
                case "BasementSTSLow"://低风
                    this._logic.iracc.SendIRACCFL(1, IRACCFL.LL);
                    break;
                case "BasementSTSUp"://温度加
                    temp1 = GlobalInfo.Instance.CurrentClimateTemp1++;
                    if (temp1 < 31 && temp1 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(1, temp1);
                    }
                    else if (temp1 == 31)
                    {
                        temp1 = 30;
                        this._logic.iracc.SendIRACCTemp(1, temp1);
                    }
                    else if (temp1 == 17)
                    {
                        temp1 = 18;
                        this._logic.iracc.SendIRACCTemp(1, temp1);
                    }
                    break;
                case "BasementSTSDown"://温度减
                    temp1 = GlobalInfo.Instance.CurrentClimateTemp1--;
                    if (temp1 < 31 && temp1 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(1, temp1);
                    }
                    else if (temp1 == 31)
                    {
                        temp1 = 30;
                        this._logic.iracc.SendIRACCTemp(1, temp1);
                    }
                    else if (temp1 == 17)
                    {
                        temp1 = 18;
                        this._logic.iracc.SendIRACCTemp(1, temp1);
                    }
                    break;
                #endregion
                #region 地下室卫生间
                case "BasementWSJOn"://开机
                    this._logic.iracc.SendIRACCPower(2, true);
                    break;
                case "BasementWSJOff"://关机
                    this._logic.iracc.SendIRACCPower(2, false);
                    break;
                case "BasementWSJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(2, IRACCMode.ZD);
                    break;
                case "BasementWSJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(2, IRACCMode.ZL);
                    break;
                case "BasementWSJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(2, IRACCMode.ZR);
                    break;
                case "BasementWSJHigh"://高风
                    this._logic.iracc.SendIRACCFL(2, IRACCFL.HH);
                    break;
                case "BasementWSJIn"://中风
                    this._logic.iracc.SendIRACCFL(2, IRACCFL.M);
                    break;
                case "BasementWSJLow"://低风
                    this._logic.iracc.SendIRACCFL(2, IRACCFL.LL);
                    break;
                case "BasementWSJUp"://温度加
                    temp2 = GlobalInfo.Instance.CurrentClimateTemp2++;
                    if (temp2 < 31 && temp2 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(2, temp2);
                    }
                    else if (temp2 == 31)
                    {
                        temp2 = 30;
                        this._logic.iracc.SendIRACCTemp(2, temp2);
                    }
                    else if (temp2 == 17)
                    {
                        temp2 = 18;
                        this._logic.iracc.SendIRACCTemp(2, temp2);
                    }
                    break;
                case "BasementWSJDown"://温度减
                    temp2 = GlobalInfo.Instance.CurrentClimateTemp2--;
                    if (temp2 < 31 && temp2 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(2, temp2);
                    }
                    else if (temp2 == 31)
                    {
                        temp2 = 30;
                        this._logic.iracc.SendIRACCTemp(2, temp2);
                    }
                    else if (temp2 == 17)
                    {
                        temp2 = 18;
                        this._logic.iracc.SendIRACCTemp(2, temp2);
                    }
                    break;
                #endregion
                #region 地下室左边
                case "BasementDXSZBOn"://开机
                    this._logic.iracc.SendIRACCPower(3, true);
                    break;
                case "BasementDXSZBOff"://关机
                    this._logic.iracc.SendIRACCPower(3, false);
                    break;
                case "BasementDXSZBZd"://自动
                    this._logic.iracc.SendIRACCSetMode(3, IRACCMode.ZD);
                    break;
                case "BasementDXSZBCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(3, IRACCMode.ZL);
                    break;
                case "BasementDXSZBHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(3, IRACCMode.ZR);
                    break;
                case "BasementDXSZBHigh"://高风
                    this._logic.iracc.SendIRACCFL(3, IRACCFL.HH);
                    break;
                case "BasementDXSZBIn"://中风
                    this._logic.iracc.SendIRACCFL(3, IRACCFL.M);
                    break;
                case "BasementDXSZBLow"://低风
                    this._logic.iracc.SendIRACCFL(3, IRACCFL.LL);
                    break;
                case "BasementDXSZBUp"://温度加
                    temp3 = GlobalInfo.Instance.CurrentClimateTemp3++;
                    if (temp3 < 31 && temp3 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(3, temp3);
                    }
                    else if (temp3 == 31)
                    {
                        temp3 = 30;
                        this._logic.iracc.SendIRACCTemp(3, temp3);
                    }
                    else if (temp3 == 17)
                    {
                        temp3 = 18;
                        this._logic.iracc.SendIRACCTemp(3, temp3);
                    }
                    break;
                case "BasementDXSZBDown"://温度减
                    temp3 = GlobalInfo.Instance.CurrentClimateTemp3--;
                    if (temp3 < 31 && temp3 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(3, temp3);
                    }
                    else if (temp3 == 31)
                    {
                        temp3 = 30;
                        this._logic.iracc.SendIRACCTemp(3, temp3);
                    }
                    else if (temp3 == 17)
                    {
                        temp3 = 18;
                        this._logic.iracc.SendIRACCTemp(3, temp3);
                    }
                    break;
                #endregion
                #region 酒柜门口
                case "BasementJGMKOn"://开机
                    this._logic.iracc.SendIRACCPower(4, true);
                    break;
                case "BasementJGMKOff"://关机
                    this._logic.iracc.SendIRACCPower(4, false);
                    break;
                case "BasementJGMKZd"://自动
                    this._logic.iracc.SendIRACCSetMode(4, IRACCMode.ZD);
                    break;
                case "BasementJGMKCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(4, IRACCMode.ZL);
                    break;
                case "BasementJGMKHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(4, IRACCMode.ZR);
                    break;
                case "BasementJGMKHigh"://高风
                    this._logic.iracc.SendIRACCFL(4, IRACCFL.HH);
                    break;
                case "BasementJGMKIn"://中风
                    this._logic.iracc.SendIRACCFL(4, IRACCFL.M);
                    break;
                case "BasementJGMKLow"://低风
                    this._logic.iracc.SendIRACCFL(4, IRACCFL.LL);
                    break;
                case "BasementJGMKUp"://温度加
                    temp4 = GlobalInfo.Instance.CurrentClimateTemp4++;
                    if (temp4 < 31 && temp4 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(4, temp4);
                    }
                    else if (temp4 == 31)
                    {
                        temp4 = 30;
                        this._logic.iracc.SendIRACCTemp(4, temp4);
                    }
                    else if (temp4 == 17)
                    {
                        temp4 = 18;
                        this._logic.iracc.SendIRACCTemp(4, temp4);
                    }
                    break;
                case "BasementJGMKDown"://温度减
                    temp4 = GlobalInfo.Instance.CurrentClimateTemp4--;
                    if (temp4 < 31 && temp4 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(4, temp4);
                    }
                    else if (temp4 == 31)
                    {
                        temp4 = 30;
                        this._logic.iracc.SendIRACCTemp(4, temp4);
                    }
                    else if (temp4 == 17)
                    {
                        temp4 = 18;
                        this._logic.iracc.SendIRACCTemp(4, temp4);
                    }
                    break;
                #endregion 
                #region 棋牌室
                case "BasementQPSOn"://开机
                    this._logic.iracc.SendIRACCPower(5, true);
                    break;
                case "BasementQPSOff"://关机
                    this._logic.iracc.SendIRACCPower(5, false);
                    break;
                case "BasementQPSZd"://自动
                    this._logic.iracc.SendIRACCSetMode(5, IRACCMode.ZD);
                    break;
                case "BasementQPSCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(5, IRACCMode.ZL);
                    break;
                case "BasementQPSHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(5, IRACCMode.ZR);
                    break;
                case "BasementQPSHigh"://高风
                    this._logic.iracc.SendIRACCFL(5, IRACCFL.HH);
                    break;
                case "BasementQPSIn"://中风
                    this._logic.iracc.SendIRACCFL(5, IRACCFL.M);
                    break;
                case "BasementQPSLow"://低风
                    this._logic.iracc.SendIRACCFL(5, IRACCFL.LL);
                    break;
                case "BasementQPSUp"://温度加
                    temp5 = GlobalInfo.Instance.CurrentClimateTemp5++;
                    if (temp5 < 31 && temp5 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(5, temp5);
                    }
                    else if (temp5 == 31)
                    {
                        temp5 = 30;
                        this._logic.iracc.SendIRACCTemp(5, temp5);
                    }
                    else if (temp5 == 17)
                    {
                        temp5 = 18;
                        this._logic.iracc.SendIRACCTemp(5, temp5);
                    }
                    break;
                case "BasementQPSDown"://温度减
                    temp5 = GlobalInfo.Instance.CurrentClimateTemp5--;
                    if (temp5 < 31 && temp5 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(5, temp5);
                    }
                    else if (temp5 == 31)
                    {
                        temp5 = 30;
                        this._logic.iracc.SendIRACCTemp(5, temp5);
                    }
                    else if (temp5 == 17)
                    {
                        temp5 = 18;
                        this._logic.iracc.SendIRACCTemp(5, temp5);
                    }
                    break;
                #endregion
                #endregion

                #region 一楼
                #region 楼梯口
                case "OneLTKOn"://开机
                    this._logic.iracc.SendIRACCPower(6, true);
                    break;
                case "OneLTKOff"://关机
                    this._logic.iracc.SendIRACCPower(6, false);
                    break;
                case "OneLTKZd"://自动
                    this._logic.iracc.SendIRACCSetMode(6, IRACCMode.ZD);
                    break;
                case "OneLTKCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(6, IRACCMode.ZL);
                    break;
                case "OneLTKHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(6, IRACCMode.ZR);
                    break;
                case "OneLTKHigh"://高风
                    this._logic.iracc.SendIRACCFL(6, IRACCFL.HH);
                    break;
                case "OneLTKIn"://中风
                    this._logic.iracc.SendIRACCFL(6, IRACCFL.M);
                    break;
                case "OneLTKLow"://低风
                    this._logic.iracc.SendIRACCFL(6, IRACCFL.LL);
                    break;
                case "OneLTKUp"://温度加
                    temp6 = GlobalInfo.Instance.CurrentClimateTemp6++;
                    if (temp6 < 31 && temp6 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(6, temp6);
                    }
                    else if (temp6 == 31)
                    {
                        temp6 = 30;
                        this._logic.iracc.SendIRACCTemp(6, temp6);
                    }
                    else if (temp6 == 17)
                    {
                        temp6 = 18;
                        this._logic.iracc.SendIRACCTemp(6, temp6);
                    }
                    break;
                case "OneLTKDown"://温度减
                    temp6 = GlobalInfo.Instance.CurrentClimateTemp6--;
                    if (temp6 < 31 && temp6 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(6, temp6);
                    }
                    else if (temp6 == 31)
                    {
                        temp6 = 30;
                        this._logic.iracc.SendIRACCTemp(6, temp6);
                    }
                    else if (temp6 == 17)
                    {
                        temp6 = 18;
                        this._logic.iracc.SendIRACCTemp(6, temp6);
                    }
                    break;
                #endregion
                #region 机房门口
                case "OneJFMKOn"://开机
                    this._logic.iracc.SendIRACCPower(7, true);
                    break;
                case "OneJFMKOff"://关机
                    this._logic.iracc.SendIRACCPower(7, false);
                    break;
                case "OneJFMKZd"://自动
                    this._logic.iracc.SendIRACCSetMode(7, IRACCMode.ZD);
                    break;
                case "OneJFMKCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(7, IRACCMode.ZL);
                    break;
                case "OneJFMKHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(7, IRACCMode.ZR);
                    break;
                case "OneJFMKHigh"://高风
                    this._logic.iracc.SendIRACCFL(7, IRACCFL.HH);
                    break;
                case "OneJFMKIn"://中风
                    this._logic.iracc.SendIRACCFL(7, IRACCFL.M);
                    break;
                case "OneJFMKLow"://低风
                    this._logic.iracc.SendIRACCFL(7, IRACCFL.LL);
                    break;
                case "OneJFMKUp"://温度加
                    temp7 = GlobalInfo.Instance.CurrentClimateTemp7++;
                    if (temp7 < 31 && temp7 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(7, temp7);
                    }
                    else if (temp7 == 31)
                    {
                        temp7 = 30;
                        this._logic.iracc.SendIRACCTemp(7, temp7);
                    }
                    else if (temp7 == 17)
                    {
                        temp7 = 18;
                        this._logic.iracc.SendIRACCTemp(7, temp7);
                    }
                    break;
                case "OneJFMKDown"://温度减
                    temp7 = GlobalInfo.Instance.CurrentClimateTemp7--;
                    if (temp7 < 31 && temp7 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(7, temp7);
                    }
                    else if (temp7 == 31)
                    {
                        temp7 = 30;
                        this._logic.iracc.SendIRACCTemp(7, temp7);
                    }
                    else if (temp7 == 17)
                    {
                        temp7 = 18;
                        this._logic.iracc.SendIRACCTemp(7, temp7);
                    }
                    break;
                #endregion
                #region 机房
                case "OneJFOn"://开机
                    this._logic.iracc.SendIRACCPower(8, true);
                    break;
                case "OneJFOff"://关机
                    this._logic.iracc.SendIRACCPower(8, false);
                    break;
                case "OneJFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(8, IRACCMode.ZD);
                    break;
                case "OneJFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(8, IRACCMode.ZL);
                    break;
                case "OneJFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(8, IRACCMode.ZR);
                    break;
                case "OneJFHigh"://高风
                    this._logic.iracc.SendIRACCFL(8, IRACCFL.HH);
                    break;
                case "OneJFIn"://中风
                    this._logic.iracc.SendIRACCFL(8, IRACCFL.M);
                    break;
                case "OneJFLow"://低风
                    this._logic.iracc.SendIRACCFL(8, IRACCFL.LL);
                    break;
                case "OneJFUp"://温度加
                    temp8 = GlobalInfo.Instance.CurrentClimateTemp8++;
                    if (temp8 < 31 && temp8 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(8, temp8);
                    }
                    else if (temp8 == 31)
                    {
                        temp8 = 30;
                        this._logic.iracc.SendIRACCTemp(8, temp8);
                    }
                    else if (temp8 == 17)
                    {
                        temp8 = 18;
                        this._logic.iracc.SendIRACCTemp(8, temp8);
                    }
                    break;
                case "OneJFDown"://温度减
                    temp8 = GlobalInfo.Instance.CurrentClimateTemp8--;
                    if (temp8 < 31 && temp8 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(8, temp8);
                    }
                    else if (temp8 == 31)
                    {
                        temp8 = 30;
                        this._logic.iracc.SendIRACCTemp(8, temp8);
                    }
                    else if (temp8 == 17)
                    {
                        temp8 = 18;
                        this._logic.iracc.SendIRACCTemp(8, temp8);
                    }
                    break;
                #endregion
                #region 餐厅
                case "OneCTOn"://开机
                    this._logic.iracc.SendIRACCPower(9, true);
                    break;
                case "OneCTOff"://关机
                    this._logic.iracc.SendIRACCPower(9, false);
                    break;
                case "OneCTZd"://自动
                    this._logic.iracc.SendIRACCSetMode(9, IRACCMode.ZD);
                    break;
                case "OneCTCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(9, IRACCMode.ZL);
                    break;
                case "OneCTHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(9, IRACCMode.ZR);
                    break;
                case "OneCTHigh"://高风
                    this._logic.iracc.SendIRACCFL(9, IRACCFL.HH);
                    break;
                case "OneCTIn"://中风
                    this._logic.iracc.SendIRACCFL(9, IRACCFL.M);
                    break;
                case "OneCTLow"://低风
                    this._logic.iracc.SendIRACCFL(9, IRACCFL.LL);
                    break;
                case "OneCTUp"://温度加
                    temp9 = GlobalInfo.Instance.CurrentClimateTemp9++;
                    if (temp9 < 31 && temp9 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(9, temp9);
                    }
                    else if (temp9 == 31)
                    {
                        temp9 = 30;
                        this._logic.iracc.SendIRACCTemp(9, temp9);
                    }
                    else if (temp9 == 17)
                    {
                        temp9 = 18;
                        this._logic.iracc.SendIRACCTemp(9, temp9);
                    }
                    break;
                case "OneCTDown"://温度减
                    temp9 = GlobalInfo.Instance.CurrentClimateTemp9--;
                    if (temp9 < 31 && temp9 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(9, temp9);
                    }
                    else if (temp9 == 31)
                    {
                        temp9 = 30;
                        this._logic.iracc.SendIRACCTemp(9, temp9);
                    }
                    else if (temp9 == 17)
                    {
                        temp9 = 18;
                        this._logic.iracc.SendIRACCTemp(9, temp9);
                    }
                    break;
                #endregion
                #region 卫生间
                case "OneWSJOn"://开机
                    this._logic.iracc.SendIRACCPower(10, true);
                    break;
                case "OneWSJOff"://关机
                    this._logic.iracc.SendIRACCPower(10, false);
                    break;
                case "OneWSJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(10, IRACCMode.ZD);
                    break;
                case "OneWSJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(10, IRACCMode.ZL);
                    break;
                case "OneWSJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(10, IRACCMode.ZR);
                    break;
                case "OneWSJHigh"://高风
                    this._logic.iracc.SendIRACCFL(10, IRACCFL.HH);
                    break;
                case "OneWSJIn"://中风
                    this._logic.iracc.SendIRACCFL(10, IRACCFL.M);
                    break;
                case "OneWSJLow"://低风
                    this._logic.iracc.SendIRACCFL(10, IRACCFL.LL);
                    break;
                case "OneWSJUp"://温度加
                    temp10 = GlobalInfo.Instance.CurrentClimateTemp10++;
                    if (temp10 < 31 && temp10 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(10, temp10);
                    }
                    else if (temp10 == 31)
                    {
                        temp10 = 30;
                        this._logic.iracc.SendIRACCTemp(10, temp10);
                    }
                    else if (temp10 == 17)
                    {
                        temp10 = 18;
                        this._logic.iracc.SendIRACCTemp(10, temp10);
                    }
                    break;
                case "OneWSJDown"://温度减
                    temp10 = GlobalInfo.Instance.CurrentClimateTemp10--;
                    if (temp10 < 31 && temp10 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(10, temp10);
                    }
                    else if (temp10 == 31)
                    {
                        temp10 = 30;
                        this._logic.iracc.SendIRACCTemp(10, temp10);
                    }
                    else if (temp10 == 17)
                    {
                        temp10 = 18;
                        this._logic.iracc.SendIRACCTemp(10, temp10);
                    }
                    break;
                #endregion
                #region 茶室
                case "OneCSOn"://开机
                    this._logic.iracc.SendIRACCPower(11, true);
                    break;
                case "OneCSOff"://关机
                    this._logic.iracc.SendIRACCPower(11, false);
                    break;
                case "OneCSZd"://自动
                    this._logic.iracc.SendIRACCSetMode(11, IRACCMode.ZD);
                    break;
                case "OneCSCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(11, IRACCMode.ZL);
                    break;
                case "OneCSHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(11, IRACCMode.ZR);
                    break;
                case "OneCSHigh"://高风
                    this._logic.iracc.SendIRACCFL(11, IRACCFL.HH);
                    break;
                case "OneCSIn"://中风
                    this._logic.iracc.SendIRACCFL(11, IRACCFL.M);
                    break;
                case "OneCSLow"://低风
                    this._logic.iracc.SendIRACCFL(11, IRACCFL.LL);
                    break;
                case "OneCSUp"://温度加
                    temp11 = GlobalInfo.Instance.CurrentClimateTemp11++;
                    if (temp11 < 31 && temp11 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(11, temp11);
                    }
                    else if (temp11 == 31)
                    {
                        temp11 = 30;
                        this._logic.iracc.SendIRACCTemp(11, temp11);
                    }
                    else if (temp11 == 17)
                    {
                        temp11 = 18;
                        this._logic.iracc.SendIRACCTemp(11, temp11);
                    }
                    break;
                case "OneCSDown"://温度减
                    temp11 = GlobalInfo.Instance.CurrentClimateTemp11--;
                    if (temp11 < 31 && temp11 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(11, temp11);
                    }
                    else if (temp11 == 31)
                    {
                        temp11 = 30;
                        this._logic.iracc.SendIRACCTemp(11, temp11);
                    }
                    else if (temp11 == 17)
                    {
                        temp11 = 18;
                        this._logic.iracc.SendIRACCTemp(11, temp11);
                    }
                    break;
                #endregion
                #region 茶室检修口
                case "OneCSJXKOn"://开机
                    this._logic.iracc.SendIRACCPower(12, true);
                    break;
                case "OneCSJXKOff"://关机
                    this._logic.iracc.SendIRACCPower(12, false);
                    break;
                case "OneCSJXKZd"://自动
                    this._logic.iracc.SendIRACCSetMode(12, IRACCMode.ZD);
                    break;
                case "OneCSJXKCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(12, IRACCMode.ZL);
                    break;
                case "OneCSJXKHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(12, IRACCMode.ZR);
                    break;
                case "OneCSJXKHigh"://高风
                    this._logic.iracc.SendIRACCFL(12, IRACCFL.HH);
                    break;
                case "OneCSJXKIn"://中风
                    this._logic.iracc.SendIRACCFL(12, IRACCFL.M);
                    break;
                case "OneCSJXKLow"://低风
                    this._logic.iracc.SendIRACCFL(12, IRACCFL.LL);
                    break;
                case "OneCSJXKUp"://温度加
                    temp12 = GlobalInfo.Instance.CurrentClimateTemp12++;
                    if (temp12 < 31 && temp12 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(12, temp12);
                    }
                    else if (temp12 == 31)
                    {
                        temp12 = 30;
                        this._logic.iracc.SendIRACCTemp(12, temp12);
                    }
                    else if (temp12 == 17)
                    {
                        temp12 = 18;
                        this._logic.iracc.SendIRACCTemp(12, temp12);
                    }
                    break;
                case "OneCSJXKDown"://温度减
                    temp12 = GlobalInfo.Instance.CurrentClimateTemp12--;
                    if (temp12 < 31 && temp12 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(12, temp12);
                    }
                    else if (temp12 == 31)
                    {
                        temp12 = 30;
                        this._logic.iracc.SendIRACCTemp(12, temp12);
                    }
                    else if (temp12 == 17)
                    {
                        temp12 = 18;
                        this._logic.iracc.SendIRACCTemp(12, temp12);
                    }
                    break;
                #endregion
                #region 储物间
                case "OneCWJOn"://开机
                    this._logic.iracc.SendIRACCPower(13, true);
                    break;
                case "OneCWJOff"://关机
                    this._logic.iracc.SendIRACCPower(13, false);
                    break;
                case "OneCWJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(13, IRACCMode.ZD);
                    break;
                case "OneCWJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(13, IRACCMode.ZL);
                    break;
                case "OneCWJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(13, IRACCMode.ZR);
                    break;
                case "OneCWJHigh"://高风
                    this._logic.iracc.SendIRACCFL(13, IRACCFL.HH);
                    break;
                case "OneCWJIn"://中风
                    this._logic.iracc.SendIRACCFL(13, IRACCFL.M);
                    break;
                case "OneCWJLow"://低风
                    this._logic.iracc.SendIRACCFL(13, IRACCFL.LL);
                    break;
                case "OneCWJUp"://温度加
                    temp13 = GlobalInfo.Instance.CurrentClimateTemp13++;
                    if (temp13 < 31 && temp13 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(13, temp13);
                    }
                    else if (temp13 == 31)
                    {
                        temp13 = 30;
                        this._logic.iracc.SendIRACCTemp(13, temp13);
                    }
                    else if (temp13 == 17)
                    {
                        temp13 = 18;
                        this._logic.iracc.SendIRACCTemp(13, temp13);
                    }
                    break;
                case "OneCWJDown"://温度减
                    temp13 = GlobalInfo.Instance.CurrentClimateTemp13--;
                    if (temp13 < 31 && temp13 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(13, temp13);
                    }
                    else if (temp13 == 31)
                    {
                        temp13 = 30;
                        this._logic.iracc.SendIRACCTemp(13, temp13);
                    }
                    else if (temp13 == 17)
                    {
                        temp13 = 18;
                        this._logic.iracc.SendIRACCTemp(13, temp13);
                    }
                    break;
                #endregion
                #endregion

                #region 二楼
                #region 楼梯口
                case "TwoLTKOn"://开机
                    this._logic.iracc.SendIRACCPower(30, true);
                    break;
                case "TwoLTKOff"://关机
                    this._logic.iracc.SendIRACCPower(30, false);
                    break;
                case "TwoLTKZd"://自动
                    this._logic.iracc.SendIRACCSetMode(30, IRACCMode.ZD);
                    break;
                case "TwoLTKCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(30, IRACCMode.ZL);
                    break;
                case "TwoLTKHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(30, IRACCMode.ZR);
                    break;
                case "TwoLTKHigh"://高风
                    this._logic.iracc.SendIRACCFL(30, IRACCFL.HH);
                    break;
                case "TwoLTKIn"://中风
                    this._logic.iracc.SendIRACCFL(30, IRACCFL.M);
                    break;
                case "TwoLTKLow"://低风
                    this._logic.iracc.SendIRACCFL(30, IRACCFL.LL);
                    break;
                case "TwoLTKUp"://温度加
                    temp30 = GlobalInfo.Instance.CurrentClimateTemp30++;
                    if (temp30 < 31 && temp30 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(30, temp30);
                    }
                    else if (temp30 == 31)
                    {
                        temp30 = 30;
                        this._logic.iracc.SendIRACCTemp(30, temp30);
                    }
                    else if (temp30 == 17)
                    {
                        temp30 = 18;
                        this._logic.iracc.SendIRACCTemp(30, temp30);
                    }
                    break;
                case "TwoLTKDown"://温度减
                    temp30 = GlobalInfo.Instance.CurrentClimateTemp30--;
                    if (temp30 < 31 && temp30 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(30, temp30);
                    }
                    else if (temp30 == 31)
                    {
                        temp30 = 30;
                        this._logic.iracc.SendIRACCTemp(30, temp30);
                    }
                    else if (temp30 == 17)
                    {
                        temp30 = 18;
                        this._logic.iracc.SendIRACCTemp(30, temp30);
                    }
                    break;
                #endregion
                #region 机房
                case "TwoJFOn"://开机
                    this._logic.iracc.SendIRACCPower(31, true);
                    break;
                case "TwoJFOff"://关机
                    this._logic.iracc.SendIRACCPower(31, false);
                    break;
                case "TwoJFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(31, IRACCMode.ZD);
                    break;
                case "TwoJFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(31, IRACCMode.ZL);
                    break;
                case "TwoJFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(31, IRACCMode.ZR);
                    break;
                case "TwoJFHigh"://高风
                    this._logic.iracc.SendIRACCFL(31, IRACCFL.HH);
                    break;
                case "TwoJFIn"://中风
                    this._logic.iracc.SendIRACCFL(31, IRACCFL.M);
                    break;
                case "TwoJFLow"://低风
                    this._logic.iracc.SendIRACCFL(31, IRACCFL.LL);
                    break;
                case "TwoJFUp"://温度加
                    temp31 = GlobalInfo.Instance.CurrentClimateTemp31++;
                    if (temp31 < 31 && temp31 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(31, temp31);
                    }
                    else if (temp31 == 31)
                    {
                        temp31 = 30;
                        this._logic.iracc.SendIRACCTemp(31, temp31);
                    }
                    else if (temp31 == 17)
                    {
                        temp31 = 18;
                        this._logic.iracc.SendIRACCTemp(31, temp31);
                    }
                    break;
                case "TwoJFDown"://温度减
                    temp31 = GlobalInfo.Instance.CurrentClimateTemp31--;
                    if (temp31 < 31 && temp31 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(31, temp31);
                    }
                    else if (temp31 == 31)
                    {
                        temp31 = 30;
                        this._logic.iracc.SendIRACCTemp(31, temp31);
                    }
                    else if (temp31 == 17)
                    {
                        temp31 = 18;
                        this._logic.iracc.SendIRACCTemp(31, temp31);
                    }
                    break;
                #endregion
                #region 房间
                case "TwoFJOn"://开机
                    this._logic.iracc.SendIRACCPower(32, true);
                    break;
                case "TwoFJOff"://关机
                    this._logic.iracc.SendIRACCPower(32, false);
                    break;
                case "TwoFJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(32, IRACCMode.ZD);
                    break;
                case "TwoFJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(32, IRACCMode.ZL);
                    break;
                case "TwoFJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(32, IRACCMode.ZR);
                    break;
                case "TwoFJHigh"://高风
                    this._logic.iracc.SendIRACCFL(32, IRACCFL.HH);
                    break;
                case "TwoFJIn"://中风
                    this._logic.iracc.SendIRACCFL(32, IRACCFL.M);
                    break;
                case "TwoFJLow"://低风
                    this._logic.iracc.SendIRACCFL(32, IRACCFL.LL);
                    break;
                case "TwoFJUp"://温度加
                    temp32 = GlobalInfo.Instance.CurrentClimateTemp32++;
                    if (temp32 < 31 && temp32 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(32, temp32);
                    }
                    else if (temp32 == 31)
                    {
                        temp32 = 30;
                        this._logic.iracc.SendIRACCTemp(32, temp32);
                    }
                    else if (temp32 == 17)
                    {
                        temp32 = 18;
                        this._logic.iracc.SendIRACCTemp(32, temp32);
                    }
                    break;
                case "TwoFJDown"://温度减
                    temp32 = GlobalInfo.Instance.CurrentClimateTemp32--;
                    if (temp32 < 31 && temp32 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(32, temp32);
                    }
                    else if (temp32 == 31)
                    {
                        temp32 = 30;
                        this._logic.iracc.SendIRACCTemp(32, temp32);
                    }
                    else if (temp32 == 17)
                    {
                        temp32 = 18;
                        this._logic.iracc.SendIRACCTemp(32, temp32);
                    }
                    break;
                #endregion
                #region 房间靠近客厅
                case "TwoFJKJKTOn"://开机
                    this._logic.iracc.SendIRACCPower(33, true);
                    break;
                case "TwoFJKJKTOff"://关机
                    this._logic.iracc.SendIRACCPower(33, false);
                    break;
                case "TwoFJKJKTZd"://自动
                    this._logic.iracc.SendIRACCSetMode(33, IRACCMode.ZD);
                    break;
                case "TwoFJKJKTCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(33, IRACCMode.ZL);
                    break;
                case "TwoFJKJKTHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(33, IRACCMode.ZR);
                    break;
                case "TwoFJKJKTHigh"://高风
                    this._logic.iracc.SendIRACCFL(33, IRACCFL.HH);
                    break;
                case "TwoFJKJKTIn"://中风
                    this._logic.iracc.SendIRACCFL(33, IRACCFL.M);
                    break;
                case "TwoFJKJKTLow"://低风
                    this._logic.iracc.SendIRACCFL(33, IRACCFL.LL);
                    break;
                case "TwoFJKJKTUp"://温度加
                    temp33 = GlobalInfo.Instance.CurrentClimateTemp33++;
                    if (temp33 < 31 && temp33 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(33, temp33);
                    }
                    else if (temp33 == 31)
                    {
                        temp33 = 30;
                        this._logic.iracc.SendIRACCTemp(33, temp33);
                    }
                    else if (temp33 == 17)
                    {
                        temp33 = 18;
                        this._logic.iracc.SendIRACCTemp(33, temp33);
                    }
                    break;
                case "TwoFJKJKTDown"://温度减
                    temp33 = GlobalInfo.Instance.CurrentClimateTemp33--;
                    if (temp33 < 31 && temp33 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(33, temp33);
                    }
                    else if (temp33 == 31)
                    {
                        temp33 = 30;
                        this._logic.iracc.SendIRACCTemp(33, temp33);
                    }
                    else if (temp33 == 17)
                    {
                        temp33 = 18;
                        this._logic.iracc.SendIRACCTemp(33, temp33);
                    }
                    break;
                #endregion
                #endregion

                #region 三楼
                #region 机房
                case "ThreeJFOn"://开机
                    this._logic.iracc.SendIRACCPower(34, true);
                    break;
                case "ThreeJFOff"://关机
                    this._logic.iracc.SendIRACCPower(34, false);
                    break;
                case "ThreeJFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(34, IRACCMode.ZD);
                    break;
                case "ThreeJFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(34, IRACCMode.ZL);
                    break;
                case "ThreeJFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(34, IRACCMode.ZR);
                    break;
                case "ThreeJFHigh"://高风
                    this._logic.iracc.SendIRACCFL(34, IRACCFL.HH);
                    break;
                case "ThreeJFIn"://中风
                    this._logic.iracc.SendIRACCFL(34, IRACCFL.M);
                    break;
                case "ThreeJFLow"://低风
                    this._logic.iracc.SendIRACCFL(34, IRACCFL.LL);
                    break;
                case "ThreeJFUp"://温度加
                    temp34 = GlobalInfo.Instance.CurrentClimateTemp34++;
                    if (temp34 < 31 && temp34 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(34, temp34);
                    }
                    else if (temp34 == 31)
                    {
                        temp34 = 30;
                        this._logic.iracc.SendIRACCTemp(34, temp34);
                    }
                    else if (temp34 == 17)
                    {
                        temp34 = 18;
                        this._logic.iracc.SendIRACCTemp(34, temp34);
                    }
                    break;
                case "ThreeJFDown"://温度减
                    temp34 = GlobalInfo.Instance.CurrentClimateTemp34--;
                    if (temp34 < 31 && temp34 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(34, temp34);
                    }
                    else if (temp34 == 31)
                    {
                        temp34 = 30;
                        this._logic.iracc.SendIRACCTemp(34, temp34);
                    }
                    else if (temp34 == 17)
                    {
                        temp34 = 18;
                        this._logic.iracc.SendIRACCTemp(34, temp34);
                    }
                    break;
                #endregion
                #region 客房
                case "ThreeKFOn"://开机
                    this._logic.iracc.SendIRACCPower(35, true);
                    break;
                case "ThreeKFOff"://关机
                    this._logic.iracc.SendIRACCPower(35, false);
                    break;
                case "ThreeKFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(35, IRACCMode.ZD);
                    break;
                case "ThreeKFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(35, IRACCMode.ZL);
                    break;
                case "ThreeKFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(35, IRACCMode.ZR);
                    break;
                case "ThreeKFHigh"://高风
                    this._logic.iracc.SendIRACCFL(35, IRACCFL.HH);
                    break;
                case "ThreeKFIn"://中风
                    this._logic.iracc.SendIRACCFL(35, IRACCFL.M);
                    break;
                case "ThreeKFLow"://低风
                    this._logic.iracc.SendIRACCFL(35, IRACCFL.LL);
                    break;
                case "ThreeKFUp"://温度加
                    temp35 = GlobalInfo.Instance.CurrentClimateTemp35++;
                    if (temp35 < 31 && temp35 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(35, temp35);
                    }
                    else if (temp35 == 31)
                    {
                        temp35 = 30;
                        this._logic.iracc.SendIRACCTemp(35, temp35);
                    }
                    else if (temp35 == 17)
                    {
                        temp35 = 18;
                        this._logic.iracc.SendIRACCTemp(35, temp35);
                    }
                    break;
                case "ThreeKFDown"://温度减
                    temp35 = GlobalInfo.Instance.CurrentClimateTemp35--;
                    if (temp35 < 31 && temp35 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(35, temp35);
                    }
                    else if (temp35 == 31)
                    {
                        temp35 = 30;
                        this._logic.iracc.SendIRACCTemp(35, temp35);
                    }
                    else if (temp35 == 17)
                    {
                        temp35 = 18;
                        this._logic.iracc.SendIRACCTemp(35, temp35);
                    }
                    break;
                #endregion
                #region 客房卫生间
                case "ThreeKFWSJOn"://开机
                    this._logic.iracc.SendIRACCPower(36, true);
                    break;
                case "ThreeKFWSJOff"://关机
                    this._logic.iracc.SendIRACCPower(36, false);
                    break;
                case "ThreeKFWSJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(36, IRACCMode.ZD);
                    break;
                case "ThreeKFWSJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(36, IRACCMode.ZL);
                    break;
                case "ThreeKFWSJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(36, IRACCMode.ZR);
                    break;
                case "ThreeKFWSJHigh"://高风
                    this._logic.iracc.SendIRACCFL(36, IRACCFL.HH);
                    break;
                case "ThreeKFWSJIn"://中风
                    this._logic.iracc.SendIRACCFL(36, IRACCFL.M);
                    break;
                case "ThreeKFWSJLow"://低风
                    this._logic.iracc.SendIRACCFL(36, IRACCFL.LL);
                    break;
                case "ThreeKFWSJUp"://温度加
                    temp36 = GlobalInfo.Instance.CurrentClimateTemp36++;
                    if (temp36 < 31 && temp36 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(36, temp36);
                    }
                    else if (temp36 == 31)
                    {
                        temp36 = 30;
                        this._logic.iracc.SendIRACCTemp(36, temp36);
                    }
                    else if (temp36 == 17)
                    {
                        temp36 = 18;
                        this._logic.iracc.SendIRACCTemp(36, temp36);
                    }
                    break;
                case "ThreeKFWSJDown"://温度减
                    temp36 = GlobalInfo.Instance.CurrentClimateTemp36--;
                    if (temp36 < 31 && temp36 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(36, temp36);
                    }
                    else if (temp36 == 31)
                    {
                        temp36 = 30;
                        this._logic.iracc.SendIRACCTemp(36, temp36);
                    }
                    else if (temp36 == 17)
                    {
                        temp36 = 18;
                        this._logic.iracc.SendIRACCTemp(36, temp36);
                    }
                    break;
                #endregion
                #region 客房书房
                case "ThreeKFSFOn"://开机
                    this._logic.iracc.SendIRACCPower(37, true);
                    break;
                case "ThreeKFSFOff"://关机
                    this._logic.iracc.SendIRACCPower(37, false);
                    break;
                case "ThreeKFSFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(37, IRACCMode.ZD);
                    break;
                case "ThreeKFSFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(37, IRACCMode.ZL);
                    break;
                case "ThreeKFSFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(37, IRACCMode.ZR);
                    break;
                case "ThreeKFSFHigh"://高风
                    this._logic.iracc.SendIRACCFL(37, IRACCFL.HH);
                    break;
                case "ThreeKFSFIn"://中风
                    this._logic.iracc.SendIRACCFL(37, IRACCFL.M);
                    break;
                case "ThreeKFSFLow"://低风
                    this._logic.iracc.SendIRACCFL(37, IRACCFL.LL);
                    break;
                case "ThreeKFSFUp"://温度加
                    temp37 = GlobalInfo.Instance.CurrentClimateTemp37++;
                    if (temp37 < 31 && temp37 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(37, temp37);
                    }
                    else if (temp37 == 31)
                    {
                        temp37 = 30;
                        this._logic.iracc.SendIRACCTemp(37, temp37);
                    }
                    else if (temp37 == 17)
                    {
                        temp37 = 18;
                        this._logic.iracc.SendIRACCTemp(37, temp37);
                    }
                    break;
                case "ThreeKFSFDown"://温度减
                    temp37 = GlobalInfo.Instance.CurrentClimateTemp37--;
                    if (temp37 < 31 && temp37 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(37, temp37);
                    }
                    else if (temp37 == 31)
                    {
                        temp37 = 30;
                        this._logic.iracc.SendIRACCTemp(37, temp37);
                    }
                    else if (temp37 == 17)
                    {
                        temp37 = 18;
                        this._logic.iracc.SendIRACCTemp(37, temp37);
                    }
                    break;
                #endregion
                #region 主卧书房
                case "ThreeZWSFOn"://开机
                    this._logic.iracc.SendIRACCPower(38, true);
                    break;
                case "ThreeZWSFOff"://关机
                    this._logic.iracc.SendIRACCPower(38, false);
                    break;
                case "ThreeZWSFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(38, IRACCMode.ZD);
                    break;
                case "ThreeZWSFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(38, IRACCMode.ZL);
                    break;
                case "ThreeZWSFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(38, IRACCMode.ZR);
                    break;
                case "ThreeZWSFHigh"://高风
                    this._logic.iracc.SendIRACCFL(38, IRACCFL.HH);
                    break;
                case "ThreeZWSFIn"://中风
                    this._logic.iracc.SendIRACCFL(38, IRACCFL.M);
                    break;
                case "ThreeZWSFLow"://低风
                    this._logic.iracc.SendIRACCFL(38, IRACCFL.LL);
                    break;
                case "ThreeZWSFUp"://温度加
                    temp38 = GlobalInfo.Instance.CurrentClimateTemp38++;
                    if (temp38 < 31 && temp38 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(38, temp38);
                    }
                    else if (temp38 == 31)
                    {
                        temp38 = 30;
                        this._logic.iracc.SendIRACCTemp(38, temp38);
                    }
                    else if (temp38 == 17)
                    {
                        temp38 = 18;
                        this._logic.iracc.SendIRACCTemp(38, temp38);
                    }
                    break;
                case "ThreeZWSFDown"://温度减
                    temp38 = GlobalInfo.Instance.CurrentClimateTemp38--;
                    if (temp38 < 31 && temp38 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(38, temp38);
                    }
                    else if (temp38 == 31)
                    {
                        temp38 = 30;
                        this._logic.iracc.SendIRACCTemp(38, temp38);
                    }
                    else if (temp38 == 17)
                    {
                        temp38 = 18;
                        this._logic.iracc.SendIRACCTemp(38, temp38);
                    }
                    break;
                #endregion
                #region 主卧
                case "ThreeZWOn"://开机
                    this._logic.iracc.SendIRACCPower(39, true);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(40, true);
                    break;
                case "ThreeZWOff"://关机
                    this._logic.iracc.SendIRACCPower(39, false);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(40, false);
                    break;
                case "ThreeZWZd"://自动
                    this._logic.iracc.SendIRACCSetMode(39, IRACCMode.ZD);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(40, IRACCMode.ZD);
                    break;
                case "ThreeZWCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(39, IRACCMode.ZL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(40, IRACCMode.ZL);
                    break;
                case "ThreeZWHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(39, IRACCMode.ZR);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(40, IRACCMode.ZR);
                    break;
                case "ThreeZWHigh"://高风
                    this._logic.iracc.SendIRACCFL(39, IRACCFL.HH);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(40, IRACCFL.HH);
                    break;
                case "ThreeZWIn"://中风
                    this._logic.iracc.SendIRACCFL(39, IRACCFL.M);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(40, IRACCFL.M);
                    break;
                case "ThreeZWLow"://低风
                    this._logic.iracc.SendIRACCFL(39, IRACCFL.LL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(40, IRACCFL.LL);
                    break;
                case "ThreeZWUp"://温度加
                    temp39 = GlobalInfo.Instance.CurrentClimateTemp39++;
                    if (temp39 < 31 && temp39 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(39, temp39);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(40, temp39);
                    }
                    else if (temp39 == 31)
                    {
                        temp39 = 30;
                        this._logic.iracc.SendIRACCTemp(39, temp39);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(40, temp39);
                    }
                    else if (temp39 == 17)
                    {
                        temp39 = 18;
                        this._logic.iracc.SendIRACCTemp(39, temp39);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(40, temp39);
                    }
                    break;
                case "ThreeZWDown"://温度减
                    temp39 = GlobalInfo.Instance.CurrentClimateTemp39--;
                    if (temp39 < 31 && temp39 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(39, temp39);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(40, temp39);
                    }
                    else if (temp39 == 31)
                    {
                        temp39 = 30;
                        this._logic.iracc.SendIRACCTemp(39, temp39);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(40, temp39);
                    }
                    else if (temp39 == 17)
                    {
                        temp39 = 18;
                        this._logic.iracc.SendIRACCTemp(39, temp39);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(40, temp39);
                    }
                    break;
                #endregion
                #region 主卧卫生间
                case "ThreeZWWSJOn"://开机
                    this._logic.iracc.SendIRACCPower(41, true);
                    break;
                case "ThreeZWWSJOff"://关机
                    this._logic.iracc.SendIRACCPower(41, false);
                    break;
                case "ThreeZWWSJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(41, IRACCMode.ZD);
                    break;
                case "ThreeZWWSJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(41, IRACCMode.ZL);
                    break;
                case "ThreeZWWSJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(41, IRACCMode.ZR);
                    break;
                case "ThreeZWWSJHigh"://高风
                    this._logic.iracc.SendIRACCFL(41, IRACCFL.HH);
                    break;
                case "ThreeZWWSJIn"://中风
                    this._logic.iracc.SendIRACCFL(41, IRACCFL.M);
                    break;
                case "ThreeZWWSJLow"://低风
                    this._logic.iracc.SendIRACCFL(41, IRACCFL.LL);
                    break;
                case "ThreeZWWSJUp"://温度加
                    temp41 = GlobalInfo.Instance.CurrentClimateTemp41++;
                    if (temp41 < 31 && temp41 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(41, temp41);
                    }
                    else if (temp41 == 31)
                    {
                        temp41 = 30;
                        this._logic.iracc.SendIRACCTemp(41, temp41);
                    }
                    else if (temp41 == 17)
                    {
                        temp41 = 18;
                        this._logic.iracc.SendIRACCTemp(41, temp41);
                    }
                    break;
                case "ThreeZWWSJDown"://温度减
                    temp41 = GlobalInfo.Instance.CurrentClimateTemp41--;
                    if (temp41 < 31 && temp41 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(41, temp41);
                    }
                    else if (temp41 == 31)
                    {
                        temp41 = 30;
                        this._logic.iracc.SendIRACCTemp(41, temp41);
                    }
                    else if (temp41 == 17)
                    {
                        temp41 = 18;
                        this._logic.iracc.SendIRACCTemp(41, temp41);
                    }
                    break;
                #endregion
                #region 更衣室
                case "ThreeGYSOn"://开机
                    this._logic.iracc.SendIRACCPower(42, true);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(43, true);
                    break;
                case "ThreeGYSOff"://关机
                    this._logic.iracc.SendIRACCPower(42, false);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(43, false);
                    break;
                case "ThreeGYSZd"://自动
                    this._logic.iracc.SendIRACCSetMode(42, IRACCMode.ZD);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(43, IRACCMode.ZD);
                    break;
                case "ThreeGYSCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(42, IRACCMode.ZL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(43, IRACCMode.ZL);
                    break;
                case "ThreeGYSHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(42, IRACCMode.ZR);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(43, IRACCMode.ZR);
                    break;
                case "ThreeGYSHigh"://高风
                    this._logic.iracc.SendIRACCFL(42, IRACCFL.HH);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(43, IRACCFL.HH);
                    break;
                case "ThreeGYSIn"://中风
                    this._logic.iracc.SendIRACCFL(42, IRACCFL.M);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(43, IRACCFL.M);
                    break;
                case "ThreeGYSLow"://低风
                    this._logic.iracc.SendIRACCFL(42, IRACCFL.LL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(43, IRACCFL.LL);
                    break;
                case "ThreeGYSUp"://温度加
                    temp42 = GlobalInfo.Instance.CurrentClimateTemp42++;
                    if (temp42 < 31 && temp42 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(42, temp42);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(43, temp42);
                    }
                    else if (temp42 == 31)
                    {
                        temp42 = 30;
                        this._logic.iracc.SendIRACCTemp(42, temp42);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(43, temp42);
                    }
                    else if (temp42 == 17)
                    {
                        temp42 = 18;
                        this._logic.iracc.SendIRACCTemp(42, temp42);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(43, temp42);
                    }
                    break;
                case "ThreeGYSDown"://温度减
                    temp42 = GlobalInfo.Instance.CurrentClimateTemp42--;
                    if (temp42 < 31 && temp42 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(42, temp42);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(43, temp42);
                    }
                    else if (temp42 == 31)
                    {
                        temp42 = 30;
                        this._logic.iracc.SendIRACCTemp(42, temp42);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(43, temp42);
                    }
                    else if (temp42 == 17)
                    {
                        temp42 = 18;
                        this._logic.iracc.SendIRACCTemp(42, temp42);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(43, temp42);
                    }
                    break;
                #endregion	
                #endregion

                #region 四楼
                #region 机房
                case "FourJFOn"://开机
                    this._logic.iracc.SendIRACCPower(14, true);
                    break;
                case "FourJFOff"://关机
                    this._logic.iracc.SendIRACCPower(14, false);
                    break;
                case "FourJFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(14, IRACCMode.ZD);
                    break;
                case "FourJFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(14, IRACCMode.ZL);
                    break;
                case "FourJFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(14, IRACCMode.ZR);
                    break;
                case "FourJFHigh"://高风
                    this._logic.iracc.SendIRACCFL(14, IRACCFL.HH);
                    break;
                case "FourJFIn"://中风
                    this._logic.iracc.SendIRACCFL(14, IRACCFL.M);
                    break;
                case "FourJFLow"://低风
                    this._logic.iracc.SendIRACCFL(14, IRACCFL.LL);
                    break;
                case "FourJFUp"://温度加
                    temp14 = GlobalInfo.Instance.CurrentClimateTemp14++;
                    if (temp14 < 31 && temp14 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(14, temp14);
                    }
                    else if (temp14 == 31)
                    {
                        temp14 = 30;
                        this._logic.iracc.SendIRACCTemp(14, temp14);
                    }
                    else if (temp14 == 17)
                    {
                        temp14 = 18;
                        this._logic.iracc.SendIRACCTemp(14, temp14);
                    }
                    break;
                case "FourJFDown"://温度减
                    temp14 = GlobalInfo.Instance.CurrentClimateTemp14--;
                    if (temp14 < 31 && temp14 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(14, temp14);
                        }
                    else if (temp14 == 31)
                    {
                        temp14 = 30;
                        this._logic.iracc.SendIRACCTemp(14, temp14);
                    }
                    else if (temp14 == 17)
                    {
                        temp14 = 18;
                        this._logic.iracc.SendIRACCTemp(14, temp14);
                    }
                    break;
                #endregion 
			    #region 客房
                case "FourKFOn"://开机
                    this._logic.iracc.SendIRACCPower(15, true);
                    break;
                case "FourKFOff"://关机
                    this._logic.iracc.SendIRACCPower(15, false);
                    break;
                case "FourKFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(15, IRACCMode.ZD);
                    break;
                case "FourKFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(15, IRACCMode.ZL);
                    break;
                case "FourKFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(15, IRACCMode.ZR);
                    break;
                case "FourKFHigh"://高风
                    this._logic.iracc.SendIRACCFL(15, IRACCFL.HH);
                    break;
                case "FourKFIn"://中风
                    this._logic.iracc.SendIRACCFL(15, IRACCFL.M);
                    break;
                case "FourKFLow"://低风
                    this._logic.iracc.SendIRACCFL(15, IRACCFL.LL);
                    break;
                case "FourKFUp"://温度加
                    temp15 = GlobalInfo.Instance.CurrentClimateTemp15++;
                    if (temp15 < 31 && temp15 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(15, temp15);
                    }
                    else if (temp15 == 31)
                    {
                        temp15 = 30;
                        this._logic.iracc.SendIRACCTemp(15, temp15);
                    }
                    else if (temp15 == 17)
                    {
                        temp15 = 18;
                        this._logic.iracc.SendIRACCTemp(15, temp15);
                    }
                    break;
                case "FourKFDown"://温度减
                    temp15 = GlobalInfo.Instance.CurrentClimateTemp15--;
                    if (temp15 < 31 && temp15 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(15, temp15);
                        }
                    else if (temp15 == 31)
                    {
                        temp15 = 30;
                        this._logic.iracc.SendIRACCTemp(15, temp15);
                    }
                    else if (temp15 == 17)
                    {
                        temp15 = 18;
                        this._logic.iracc.SendIRACCTemp(15, temp15);
                    }
                    break;
                #endregion
			    #region 客房卫生间
                case "FourKFWSJOn"://开机
                    this._logic.iracc.SendIRACCPower(16, true);
                    break;
                case "FourKFWSJOff"://关机
                    this._logic.iracc.SendIRACCPower(16, false);
                    break;
                case "FourKFWSJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(16, IRACCMode.ZD);
                    break;
                case "FourKFWSJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(16, IRACCMode.ZL);
                    break;
                case "FourKFWSJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(16, IRACCMode.ZR);
                    break;
                case "FourKFWSJHigh"://高风
                    this._logic.iracc.SendIRACCFL(16, IRACCFL.HH);
                    break;
                case "FourKFWSJIn"://中风
                    this._logic.iracc.SendIRACCFL(16, IRACCFL.M);
                    break;
                case "FourKFWSJLow"://低风
                    this._logic.iracc.SendIRACCFL(16, IRACCFL.LL);
                    break;
                case "FourKFWSJUp"://温度加
                    temp16 = GlobalInfo.Instance.CurrentClimateTemp16++;
                    if (temp16 < 31 && temp16 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(16, temp16);
                    }
                    else if (temp16 == 31)
                    {
                        temp16 = 30;
                        this._logic.iracc.SendIRACCTemp(16, temp16);
                    }
                    else if (temp16 == 17)
                    {
                        temp16 = 18;
                        this._logic.iracc.SendIRACCTemp(16, temp16);
                    }
                    break;
                case "FourKFWSJDown"://温度减
                    temp16 = GlobalInfo.Instance.CurrentClimateTemp16--;
                    if (temp16 < 31 && temp16 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(16, temp16);
                        }
                    else if (temp16 == 31)
                    {
                        temp16 = 30;
                        this._logic.iracc.SendIRACCTemp(16, temp16);
                    }
                    else if (temp16 == 17)
                    {
                        temp16 = 18;
                        this._logic.iracc.SendIRACCTemp(16, temp16);
                    }                    
                    break;
                #endregion
			    #region 客房书房
                case "FourKFSFOn"://开机
                    this._logic.iracc.SendIRACCPower(17, true);
                    break;
                case "FourKFSFOff"://关机
                    this._logic.iracc.SendIRACCPower(17, false);
                    break;
                case "FourKFSFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(17, IRACCMode.ZD);
                    break;
                case "FourKFSFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(17, IRACCMode.ZL);
                    break;
                case "FourKFSFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(17, IRACCMode.ZR);
                    break;
                case "FourKFSFHigh"://高风
                    this._logic.iracc.SendIRACCFL(17, IRACCFL.HH);
                    break;
                case "FourKFSFIn"://中风
                    this._logic.iracc.SendIRACCFL(17, IRACCFL.M);
                    break;
                case "FourKFSFLow"://低风
                    this._logic.iracc.SendIRACCFL(17, IRACCFL.LL);
                    break;
                case "FourKFSFUp"://温度加
                    temp17 = GlobalInfo.Instance.CurrentClimateTemp17++;
                    if (temp17 < 31 && temp17 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(17, temp17);
                    }
                    else if (temp17 == 31)
                    {
                        temp17 = 30;
                        this._logic.iracc.SendIRACCTemp(17, temp17);
                    }
                    else if (temp17 == 17)
                    {
                        temp17 = 18;
                        this._logic.iracc.SendIRACCTemp(17, temp17);
                    }
                    break;
                case "FourKFSFDown"://温度减
                    temp17 = GlobalInfo.Instance.CurrentClimateTemp17--;
                    if (temp17 < 31 && temp17 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(17, temp17);
                        }
                    else if (temp17 == 31)
                    {
                        temp17 = 30;
                        this._logic.iracc.SendIRACCTemp(17, temp17);
                    }
                    else if (temp17 == 17)
                    {
                        temp17 = 18;
                        this._logic.iracc.SendIRACCTemp(17, temp17);
                    }
                    break;
                #endregion
    			#region 主卧书房
                case "FourZWSFOn"://开机
                    this._logic.iracc.SendIRACCPower(24, true);
                    break;
                case "FourZWSFOff"://关机
                    this._logic.iracc.SendIRACCPower(24, false);
                    break;
                case "FourZWSFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(24, IRACCMode.ZD);
                    break;
                case "FourZWSFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(24, IRACCMode.ZL);
                    break;
                case "FourZWSFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(24, IRACCMode.ZR);
                    break;
                case "FourZWSFHigh"://高风
                    this._logic.iracc.SendIRACCFL(24, IRACCFL.HH);
                    break;
                case "FourZWSFIn"://中风
                    this._logic.iracc.SendIRACCFL(24, IRACCFL.M);
                    break;
                case "FourZWSFLow"://低风
                    this._logic.iracc.SendIRACCFL(24, IRACCFL.LL);
                    break;
                case "FourZWSFUp"://温度加
                    temp24 = GlobalInfo.Instance.CurrentClimateTemp24++;
                    if (temp24 < 31 && temp24 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(24, temp24);
                    }
                    else if (temp24 == 31)
                    {
                        temp24 = 30;
                        this._logic.iracc.SendIRACCTemp(24, temp24);
                    }
                    else if (temp24 == 17)
                    {
                        temp24 = 18;
                        this._logic.iracc.SendIRACCTemp(24, temp24);
                    }
                    break;
                case "FourZWSFDown"://温度减
                    temp24 = GlobalInfo.Instance.CurrentClimateTemp24--;
                    if (temp24 < 31 && temp24 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(24, temp24);
                        }
                    else if (temp24 == 31)
                    {
                        temp24 = 30;
                        this._logic.iracc.SendIRACCTemp(24, temp24);
                    }
                    else if (temp24 == 17)
                    {
                        temp24 = 18;
                        this._logic.iracc.SendIRACCTemp(24, temp24);
                    }
                    break;
                #endregion
	    		#region 主卧房间
                case "FourZWFJOn"://开机
                    this._logic.iracc.SendIRACCPower(25, true);
		    Thread.Sleep(1000);
		    this._logic.iracc.SendIRACCPower(26, true);
                    break;
                case "FourZWFJOff"://关机
                    this._logic.iracc.SendIRACCPower(25, false);
		    Thread.Sleep(1000);
		    this._logic.iracc.SendIRACCPower(26, false);
                    break;
                case "FourZWFJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(25, IRACCMode.ZD);
		    Thread.Sleep(1000);
  		    this._logic.iracc.SendIRACCSetMode(26, IRACCMode.ZD);
                    break;
                case "FourZWFJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(25, IRACCMode.ZL);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(26, IRACCMode.ZL);
                    break;
                case "FourZWFJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(25, IRACCMode.ZR);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(26, IRACCMode.ZR);
                    break;
                case "FourZWFJHigh"://高风
                    this._logic.iracc.SendIRACCFL(25, IRACCFL.HH);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(26, IRACCFL.HH);
                    break;
                case "FourZWFJIn"://中风
                    this._logic.iracc.SendIRACCFL(25, IRACCFL.M);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(26, IRACCFL.M);
                    break;
                case "FourZWFJLow"://低风
                    this._logic.iracc.SendIRACCFL(25, IRACCFL.LL);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(26, IRACCFL.LL);
                    break;
                case "FourZWFJUp"://温度加
                    temp25 = GlobalInfo.Instance.CurrentClimateTemp25++;
                    if (temp25 < 31 && temp25 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(25, temp25);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(26, temp25);
                    }
                    else if (temp25 == 31)
                    {
                        temp25 = 30;
                        this._logic.iracc.SendIRACCTemp(25, temp25);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(26, temp25);
                    }
                    else if (temp25 == 17)
                    {
                        temp25 = 18;
                        this._logic.iracc.SendIRACCTemp(25, temp25);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(26, temp25);
                    }
                    break;
                case "FourZWFJDown"://温度减
                    temp25 = GlobalInfo.Instance.CurrentClimateTemp25--;
                    if (temp25 < 31 && temp25 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(25, temp25);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(26, temp25);
                        }
                    else if (temp25 == 31)
                    {
                        temp25 = 30;
                        this._logic.iracc.SendIRACCTemp(25, temp25);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(26, temp25);
                    }
                    else if (temp25 == 17)
                    {
                        temp25 = 18;
                        this._logic.iracc.SendIRACCTemp(25, temp25);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(26, temp25);
                    }
                    break;
                #endregion
		    	#region 主卧卫生间
                case "FourZWWSJOn"://开机
                    this._logic.iracc.SendIRACCPower(27, true);
                    break;
                case "FourZWWSJOff"://关机
                    this._logic.iracc.SendIRACCPower(27, false);
                    break;
                case "FourZWWSJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(27, IRACCMode.ZD);
                    break;
                case "FourZWWSJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(27, IRACCMode.ZL);
                    break;
                case "FourZWWSJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(27, IRACCMode.ZR);
                    break;
                case "FourZWWSJHigh"://高风
                    this._logic.iracc.SendIRACCFL(27, IRACCFL.HH);
                    break;
                case "FourZWWSJIn"://中风
                    this._logic.iracc.SendIRACCFL(27, IRACCFL.M);
                    break;
                case "FourZWWSJLow"://低风
                    this._logic.iracc.SendIRACCFL(27, IRACCFL.LL);
                    break;
                case "FourZWWSJUp"://温度加
                    temp27 = GlobalInfo.Instance.CurrentClimateTemp27++;
                    if (temp27 < 31 && temp27 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(27, temp27);
                    }
                    else if (temp27 == 31)
                    {
                        temp27 = 30;
                        this._logic.iracc.SendIRACCTemp(27, temp27);
                    }
                    else if (temp27 == 17)
                    {
                        temp27 = 18;
                        this._logic.iracc.SendIRACCTemp(27, temp27);
                    }
                    break;
                case "FourZWWSJDown"://温度减
                    temp27 = GlobalInfo.Instance.CurrentClimateTemp27--;
                    if (temp27 < 31 && temp27 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(27, temp27);
                        }
                    else if (temp27 == 31)
                    {
                        temp27 = 30;
                        this._logic.iracc.SendIRACCTemp(27, temp27);
                    }
                    else if (temp27 == 17)
                    {
                        temp27 = 18;
                        this._logic.iracc.SendIRACCTemp(27, temp27);
                    }
                    break;
                #endregion
			    #region 更衣室
                case "FourGYSOn"://开机
                    this._logic.iracc.SendIRACCPower(28, true);
		    Thread.Sleep(1000);
		    this._logic.iracc.SendIRACCPower(29, true);
                    break;
                case "FourGYSOff"://关机
                    this._logic.iracc.SendIRACCPower(28, false);
		    Thread.Sleep(1000);
		    this._logic.iracc.SendIRACCPower(29, false);
                    break;
                case "FourGYSZd"://自动
                    this._logic.iracc.SendIRACCSetMode(28, IRACCMode.ZD);
		    Thread.Sleep(1000);
  		    this._logic.iracc.SendIRACCSetMode(29, IRACCMode.ZD);
                    break;
                case "FourGYSCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(28, IRACCMode.ZL);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(29, IRACCMode.ZL);
                    break;
                case "FourGYSHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(28, IRACCMode.ZR);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(29, IRACCMode.ZR);
                    break;
                case "FourGYSHigh"://高风
                    this._logic.iracc.SendIRACCFL(28, IRACCFL.HH);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(29, IRACCFL.HH);
                    break;
                case "FourGYSIn"://中风
                    this._logic.iracc.SendIRACCFL(28, IRACCFL.M);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(29, IRACCFL.M);
                    break;
                case "FourGYSLow"://低风
                    this._logic.iracc.SendIRACCFL(28, IRACCFL.LL);
		    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(29, IRACCFL.LL);
                    break;
                case "FourGYSUp"://温度加
                    temp28 = GlobalInfo.Instance.CurrentClimateTemp28++;
                    if (temp28 < 31 && temp28 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(28, temp28);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(29, temp28);
                    }
                    else if (temp28 == 31)
                    {
                        temp28 = 30;
                        this._logic.iracc.SendIRACCTemp(28, temp28);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(29, temp28);
                    }
                    else if (temp28 == 17)
                    {
                        temp28 = 18;
                        this._logic.iracc.SendIRACCTemp(28, temp28);
		            Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(29, temp28);
                    }
                    break;
                case "FourGYSDown"://温度减
                    temp28 = GlobalInfo.Instance.CurrentClimateTemp28--;
                    if (temp28 < 31 && temp28 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(28, temp28);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(29, temp28);
                        }
                    else if (temp28 == 31)
                    {
                        temp28 = 30;
                        this._logic.iracc.SendIRACCTemp(28, temp28);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(29, temp28);
                    }
                    else if (temp28 == 17)
                    {
                        temp28 = 18;
                        this._logic.iracc.SendIRACCTemp(28, temp28);
		        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(29, temp28);
                    }
                    break;
                #endregion
                #endregion

                #region 五楼
                #region 机房
                case "FiveJFOn"://开机
                    this._logic.iracc.SendIRACCPower(18, true);
                    break;
                case "FiveJFOff"://关机
                    this._logic.iracc.SendIRACCPower(18, false);
                    break;
                case "FiveJFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(18, IRACCMode.ZD);
                    break;
                case "FiveJFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(18, IRACCMode.ZL);
                    break;
                case "FiveJFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(18, IRACCMode.ZR);
                    break;
                case "FiveJFHigh"://高风
                    this._logic.iracc.SendIRACCFL(18, IRACCFL.HH);
                    break;
                case "FiveJFIn"://中风
                    this._logic.iracc.SendIRACCFL(18, IRACCFL.M);
                    break;
                case "FiveJFLow"://低风
                    this._logic.iracc.SendIRACCFL(18, IRACCFL.LL);
                    break;
                case "FiveJFUp"://温度加
                    temp18 = GlobalInfo.Instance.CurrentClimateTemp18++;
                    if (temp18 < 31 && temp18 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(18, temp18);
                    }
                    else if (temp18 == 31)
                    {
                        temp18 = 30;
                        this._logic.iracc.SendIRACCTemp(18, temp18);
                    }
                    else if (temp18 == 17)
                    {
                        temp18 = 18;
                        this._logic.iracc.SendIRACCTemp(18, temp18);
                    }
                    break;
                case "FiveJFDown"://温度减
                    temp18 = GlobalInfo.Instance.CurrentClimateTemp18--;
                    if (temp18 < 31 && temp18 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(18, temp18);
                    }
                    else if (temp18 == 31)
                    {
                        temp18 = 30;
                        this._logic.iracc.SendIRACCTemp(18, temp18);
                    }
                    else if (temp18 == 17)
                    {
                        temp18 = 18;
                        this._logic.iracc.SendIRACCTemp(18, temp18);
                    }
                    break;
                #endregion
                #region 书房
                case "FiveSFOn"://开机
                    this._logic.iracc.SendIRACCPower(19, true);
                    break;
                case "FiveSFOff"://关机
                    this._logic.iracc.SendIRACCPower(19, false);
                    break;
                case "FiveSFZd"://自动
                    this._logic.iracc.SendIRACCSetMode(19, IRACCMode.ZD);
                    break;
                case "FiveSFCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(19, IRACCMode.ZL);
                    break;
                case "FiveSFHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(19, IRACCMode.ZR);
                    break;
                case "FiveSFHigh"://高风
                    this._logic.iracc.SendIRACCFL(19, IRACCFL.HH);
                    break;
                case "FiveSFIn"://中风
                    this._logic.iracc.SendIRACCFL(19, IRACCFL.M);
                    break;
                case "FiveSFLow"://低风
                    this._logic.iracc.SendIRACCFL(19, IRACCFL.LL);
                    break;
                case "FiveSFUp"://温度加
                    temp19 = GlobalInfo.Instance.CurrentClimateTemp19++;
                    if (temp19 < 31 && temp19 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(19, temp19);
                    }
                    else if (temp19 == 31)
                    {
                        temp19 = 30;
                        this._logic.iracc.SendIRACCTemp(19, temp19);
                    }
                    else if (temp19 == 17)
                    {
                        temp19 = 18;
                        this._logic.iracc.SendIRACCTemp(19, temp19);
                    }
                    break;
                case "FiveSFDown"://温度减
                    temp19 = GlobalInfo.Instance.CurrentClimateTemp19--;
                    if (temp19 < 31 && temp19 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(19, temp19);
                    }
                    else if (temp19 == 31)
                    {
                        temp19 = 30;
                        this._logic.iracc.SendIRACCTemp(19, temp19);
                    }
                    else if (temp19 == 17)
                    {
                        temp19 = 18;
                        this._logic.iracc.SendIRACCTemp(19, temp19);
                    }
                    break;
                #endregion
                #region 主卧
                case "FiveZWOn"://开机
                    this._logic.iracc.SendIRACCPower(20, true);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(21, true);
                    break;
                case "FiveZWOff"://关机
                    this._logic.iracc.SendIRACCPower(20, false);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(21, false);
                    break;
                case "FiveZWZd"://自动
                    this._logic.iracc.SendIRACCSetMode(20, IRACCMode.ZD);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(21, IRACCMode.ZD);
                    break;
                case "FiveZWCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(20, IRACCMode.ZL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(21, IRACCMode.ZL);
                    break;
                case "FiveZWHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(20, IRACCMode.ZR);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(21, IRACCMode.ZR);
                    break;
                case "FiveZWHigh"://高风
                    this._logic.iracc.SendIRACCFL(20, IRACCFL.HH);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(21, IRACCFL.HH);
                    break;
                case "FiveZWIn"://中风
                    this._logic.iracc.SendIRACCFL(20, IRACCFL.M);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(21, IRACCFL.M);
                    break;
                case "FiveZWLow"://低风
                    this._logic.iracc.SendIRACCFL(20, IRACCFL.LL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(21, IRACCFL.LL);
                    break;
                case "FiveZWUp"://温度加
                    temp20 = GlobalInfo.Instance.CurrentClimateTemp20++;
                    if (temp20 < 31 && temp20 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(20, temp20);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(21, temp20);
                    }
                    else if (temp20 == 31)
                    {
                        temp20 = 30;
                        this._logic.iracc.SendIRACCTemp(20, temp20);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(21, temp20);
                    }
                    else if (temp20 == 17)
                    {
                        temp20 = 18;
                        this._logic.iracc.SendIRACCTemp(20, temp20);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(21, temp20);
                    }
                    break;
                case "FiveZWDown"://温度减
                    temp20 = GlobalInfo.Instance.CurrentClimateTemp20--;
                    if (temp20 < 31 && temp20 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(20, temp20);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(21, temp20);
                    }
                    else if (temp20 == 31)
                    {
                        temp20 = 30;
                        this._logic.iracc.SendIRACCTemp(20, temp20);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(21, temp20);
                    }
                    else if (temp20 == 17)
                    {
                        temp20 = 18;
                        this._logic.iracc.SendIRACCTemp(20, temp20);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(21, temp20);
                    }
                    break;
                #endregion
                #region 更衣室
                case "FiveGYSOn"://开机
                    this._logic.iracc.SendIRACCPower(22, true);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(23, true);
                    break;
                case "FiveGYSOff"://关机
                    this._logic.iracc.SendIRACCPower(22, false);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCPower(23, false);
                    break;
                case "FiveGYSZd"://自动
                    this._logic.iracc.SendIRACCSetMode(22, IRACCMode.ZD);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(23, IRACCMode.ZD);
                    break;
                case "FiveGYSCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(22, IRACCMode.ZL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(23, IRACCMode.ZL);
                    break;
                case "FiveGYSHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(22, IRACCMode.ZR);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCSetMode(23, IRACCMode.ZR);
                    break;
                case "FiveGYSHigh"://高风
                    this._logic.iracc.SendIRACCFL(22, IRACCFL.HH);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(23, IRACCFL.HH);
                    break;
                case "FiveGYSIn"://中风
                    this._logic.iracc.SendIRACCFL(22, IRACCFL.M);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(23, IRACCFL.M);
                    break;
                case "FiveGYSLow"://低风
                    this._logic.iracc.SendIRACCFL(22, IRACCFL.LL);
                    Thread.Sleep(1000);
                    this._logic.iracc.SendIRACCFL(23, IRACCFL.LL);
                    break;
                case "FiveGYSUp"://温度加
                    temp22 = GlobalInfo.Instance.CurrentClimateTemp22++;
                    if (temp22 < 31 && temp22 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(22, temp22);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(23, temp22);
                    }
                    else if (temp22 == 31)
                    {
                        temp22 = 30;
                        this._logic.iracc.SendIRACCTemp(22, temp22);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(23, temp22);
                    }
                    else if (temp22 == 17)
                    {
                        temp22 = 18;
                        this._logic.iracc.SendIRACCTemp(22, temp22);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(23, temp22);
                    }
                    break;
                case "FiveGYSDown"://温度减
                    temp22 = GlobalInfo.Instance.CurrentClimateTemp22--;
                    if (temp22 < 31 && temp22 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(22, temp22);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(23, temp22);
                    }
                    else if (temp22 == 31)
                    {
                        temp22 = 30;
                        this._logic.iracc.SendIRACCTemp(22, temp22);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(23, temp22);
                    }
                    else if (temp22 == 17)
                    {
                        temp22 = 18;
                        this._logic.iracc.SendIRACCTemp(22, temp22);
                        Thread.Sleep(1000);
                        this._logic.iracc.SendIRACCTemp(23, temp22);
                    }
                    break;
                #endregion
                #region 卫生间
                case "FiveWSJOn"://开机
                    this._logic.iracc.SendIRACCPower(44, true);
                    break;
                case "FiveWSJOff"://关机
                    this._logic.iracc.SendIRACCPower(44, false);
                    break;
                case "FiveWSJZd"://自动
                    this._logic.iracc.SendIRACCSetMode(44, IRACCMode.ZD);
                    break;
                case "FiveWSJCold"://制冷
                    this._logic.iracc.SendIRACCSetMode(44, IRACCMode.ZL);
                    break;
                case "FiveWSJHeat"://制热
                    this._logic.iracc.SendIRACCSetMode(44, IRACCMode.ZR);
                    break;
                case "FiveWSJHigh"://高风
                    this._logic.iracc.SendIRACCFL(44, IRACCFL.HH);
                    break;
                case "FiveWSJIn"://中风
                    this._logic.iracc.SendIRACCFL(44, IRACCFL.M);
                    break;
                case "FiveWSJLow"://低风
                    this._logic.iracc.SendIRACCFL(44, IRACCFL.LL);
                    break;
                case "FiveWSJUp"://温度加
                    temp44 = GlobalInfo.Instance.CurrentClimateTemp44++;
                    if (temp44 < 31 && temp44 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(44, temp44);
                    }
                    else if (temp44 == 31)
                    {
                        temp44 = 30;
                        this._logic.iracc.SendIRACCTemp(44, temp44);
                    }
                    else if (temp44 == 17)
                    {
                        temp44 = 18;
                        this._logic.iracc.SendIRACCTemp(44, temp44);
                    }
                    break;
                case "FiveWSJDown"://温度减
                    temp44 = GlobalInfo.Instance.CurrentClimateTemp44--;
                    if (temp44 < 31 && temp44 > 17)
                    {
                        this._logic.iracc.SendIRACCTemp(44, temp44);
                    }
                    else if (temp44 == 31)
                    {
                        temp44 = 30;
                        this._logic.iracc.SendIRACCTemp(44, temp44);
                    }
                    else if (temp44 == 17)
                    {
                        temp44 = 18;
                        this._logic.iracc.SendIRACCTemp(44, temp44);
                    }
                    break;
                #endregion
	
                #endregion
                default:
                    break;
            }
        }
        #endregion

        #region Json处理

        #endregion

        #endregion
    }
}
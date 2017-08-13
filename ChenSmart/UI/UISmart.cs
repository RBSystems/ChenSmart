using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib;

namespace ChenSmart
{
    public class UISmart
    {
        //public delegate void DigitalPressHandler(bool button);
        //public event DigitalPressHandler DigitalPressEvent;

        private CrestronControlSystem controlSystem = null;
        private ILiveSmartAPI _logic = null;
     //   public CrestronMobile mobile;
        public ILiveIpad ipad;

        public ILiveTPC5 tpc5; //5寸触摸屏
        private ILiveGRODIGY16I crodigy16I = null;
        public UISmart(CrestronControlSystem system, ILiveSmartAPI logic)
        {
            this.controlSystem = system;
            this._logic = logic;
        }
        internal void Start()
        {
            this.RegisterDevices();
        }

        public void RegisterDevices()
        {
            #region 注册串口
            if (this.controlSystem.SupportsComPort)
            {
               // this.crodigy16I = new ILiveGRODIGY16I(this.controlSystem.ComPorts[1]);
                this.crodigy16I = new ILiveGRODIGY16I(8005);
                this.crodigy16I.Push16IEvent += new ILiveGRODIGY16I.Push16IHandler(crodigy16I_Push16IEvent);

                //UDPClient port = new UDPClient("192.168.188.25", 6004, 8004);
                //port.Connect();

                this.tpc5 = new ILiveTPC5(8004);
                this.tpc5.PushTPCIEvent += new ILiveTPC5.PTCIHandler(tpc5_PushTPCIEvent);
            }
            #endregion

            #region 注册IPad（WebSocket）
            this.ipad = new ILiveIpad(this._logic);
            //this.ipad.DataReceived += new IPadWebSocketServer.DataEventHandler(ipad_DataReceived);
            this.ipad.RegisterDevices();
            #endregion
        }



        #region 5寸屏事件
        void tpc5_PushTPCIEvent(int id, int btnid)
        {
            ILiveDebug.Instance.WriteLine("TPCIEvent"+id.ToString() + "|" + btnid.ToString());

            switch (id)
            {
                case 0:
                    this.tpc5_OneEvent(btnid);
                    break;
                case 1:
                    this.tpc5_TwoEvent(btnid);
                    break;
                case 2:
                    this.tpc5_ThreeEvent(btnid);

                    break;
                case 3:
                    this.tpc5_FourEvent(btnid);
                    break;
                case 4://三楼
                    this.tpc5_FiveEvent(btnid);
                    break;
                case 5://4楼
                    this.tpc5_SixEvent(btnid);
                    break;
                case 6: //5楼
                    this.tpc5_SevenEvent(btnid);
                    break;
                default:
                    break;
            }
        }

        //三楼触摸屏
        private void tpc5_FiveEvent(int btnid)
        {
            switch (btnid)
            {
                case 1 * 256 + 1:
                    this._logic.light.LightThreeDiaoDeng(true);
                    break;
                case 1 * 256 + 2:
                    this._logic.light.LightThreeDiaoDeng(false);

                    break;
                case 1 * 256 + 3:
                    this._logic.light.LightThreeBiDeng(true);
                    break;
                case 1 * 256 + 4:
                    this._logic.light.LightThreeBiDeng(false);
                    break;
                case 1 * 256 + 5:
                    this._logic.light.LightThreeDengDai(true);
                    break;
                case 1 * 256 + 6:
                    this._logic.light.LightThreeDengDai(false);
                    break;
                case 1 * 256 + 7:
                    this._logic.light.LightThreeKongTiao(true);
                    break;
                case 1 * 256 + 8:
                    this._logic.light.LightThreeKongTiao(false);
                    break;
                case 1 * 256 + 9:
                    this._logic.light.LightThreeJinMen(true);

                    break;
                case 1 * 256 + 10:
                    this._logic.light.LightThreeJinMen(false);

                    break;
                case 1 * 256 + 11:
                    this._logic.light.LightThreeChuanTou(true);

                    break;
                case 1 * 256 + 12:
                    this._logic.light.LightThreeChuanTou(false);

                    break;
                case 1 * 256 + 13:
                    this._logic.light.LightThreeAll(true);

                    break;
                case 1 * 256 + 14:
                    this._logic.light.LightThreeAll(false);

                    break;
                case 1 * 256 + 15:
                    this._logic.light.LightThreeAll(true);

                    break;
                case 5 * 256 + 1://布防:
                    break;
                case 5 * 256 + 2://撤防
                    break;
                case 5 * 256 + 3://大门
                    break;
                case 5 * 256 + 4://校门
                    break;
                case 5 * 256 + 5://木门
                    this._logic.DoorMuMenOpen();
                    break;
                default:
                    break;
            }
            //throw new NotImplementedException();
        }

        //五楼触摸屏
        private void tpc5_SevenEvent(int btnid)
        {

            switch (btnid)
            {
                #region 音乐
                #region 客厅音乐
                #region 地下室
                case 1:
                    this._logic.Muisc.MusicPower(5, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case 2:
                    this._logic.Muisc.MusicPower(5, false);
                    break;
                case 3:
                    GlobalInfo.Instance.MusicVol5 += 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol7);

                    break;
                case 4:
                    GlobalInfo.Instance.MusicVol5 -= 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol7);


                    break;
                case 5://上一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x01, 0x81);
                    break;
                case 6://下一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x10, 0x81);

                    break;
                case 7://暂停
                    this._logic.Muisc.PlaySet(5, 0x02, 0x81);
                    break;
                case 8://播放
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case 9://列表
                    this._logic.Muisc.PlayModeSet(5, 0x03);
                    break;
                case 10://单曲
                    this._logic.Muisc.PlayModeSet(5, 0x02);

                    break;
                case 11://随机
                    this._logic.Muisc.PlayModeSet(5, 0x05);
                    break;
                case 12:
                    this._logic.Muisc.PlaySet(5, 0x04, 0x81);
                    break; //停止

                #endregion
                #region 茶室
                case 17:
                    this._logic.Muisc.MusicPower(7, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 18:
                    this._logic.Muisc.MusicPower(7, false);
                    break;
                case 19:
                    GlobalInfo.Instance.MusicVol7 += 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol5);

                    break;
                case 20:
                    GlobalInfo.Instance.MusicVol7 -= 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol5);
                    break;
                case 21://上一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x01, 0x81);
                    break;
                case 22://下一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x10, 0x81);

                    break;
                case 23://暂停
                    this._logic.Muisc.PlaySet(7, 0x02, 0x81);
                    break;
                case 24://播放
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 25://列表
                    this._logic.Muisc.PlayModeSet(7, 0x03);
                    break;
                case 26://单曲
                    this._logic.Muisc.PlayModeSet(7, 0x02);

                    break;
                case 27://随机
                    this._logic.Muisc.PlayModeSet(7, 0x05);
                    break;
                case 28:
                    this._logic.Muisc.PlaySet(7, 0x04, 0x81);
                    break; //停止

                #endregion
                #region 户外
                case 33:
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
                case 34:
                    this._logic.Muisc.MusicPower(1, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(2, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(3, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(4, false);
                    Thread.Sleep(300);
                    break;
                case 35:
                    GlobalInfo.Instance.MusicVol1 += 10;

                    this._logic.Muisc.VolSet(1, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(2, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(3, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(4, (byte)GlobalInfo.Instance.MusicVol1);
                    break;
                case 36:
                    GlobalInfo.Instance.MusicVol1 -= 10;


                    this._logic.Muisc.VolSet(1, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(2, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(3, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(4, (byte)GlobalInfo.Instance.MusicVol1);
                    break;
                case 37://上一曲
                    this._logic.Muisc.MusicChangeSet(1, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(2, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(3, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(4, 0x01, 0x81);
                    break;
                case 38://下一曲
                    this._logic.Muisc.MusicChangeSet(1, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(2, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(3, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(4, 0x10, 0x81);
                    break;
                case 39://暂停
                    this._logic.Muisc.PlaySet(1, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x02, 0x81);
                    break;
                case 40://播放
                    this._logic.Muisc.PlaySet(1, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x01, 0x81);
                    break;
                case 41://列表
                    this._logic.Muisc.PlayModeSet(1, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x03);

                    break;
                case 42://单曲
                    this._logic.Muisc.PlayModeSet(1, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x02);

                    break;
                case 43://随机
                    this._logic.Muisc.PlayModeSet(1, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x05);

                    break;
                case 44:
                    this._logic.Muisc.PlaySet(1, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x04, 0x81);

                    break; //停止

                #endregion
                #region 阳台
                case 49:
                    this._logic.Muisc.MusicPower(6, true);
                    break;
                case 50:
                    this._logic.Muisc.MusicPower(6, false);

                    break;
                case 51:
                    this._logic.Muisc.MusicChangeSet(6, 0x01, 0x81);

                    break;
                case 52:
                    this._logic.Muisc.MusicChangeSet(6, 0x10, 0x81);
                    break;
                case 53:
                    this._logic.Muisc.PlaySet(6, 0x02, 0x81);

                    break;
                case 54:
                    this._logic.Muisc.PlaySet(6, 0x01, 0x81);

                    break;
                case 55:
                    GlobalInfo.Instance.MusicVol6 -= 10;

                    this._logic.Muisc.VolSet(6, (byte)GlobalInfo.Instance.MusicVol6);
                    break;
                case 56:
                    GlobalInfo.Instance.MusicVol6 += 10;

                    this._logic.Muisc.VolSet(6, (byte)GlobalInfo.Instance.MusicVol6);
                    break;
                case 57:
                    this._logic.Muisc.PlayModeSet(6, 0x03);

                    break;
                case 58:
                    this._logic.Muisc.PlayModeSet(6, 0x02);

                    break;
                case 59:
                    this._logic.Muisc.PlayModeSet(6, 0x05);

                    break;
                case 60:
                    this._logic.Muisc.PlaySet(6, 0x04, 0x81);
                    break;
                #endregion
                #endregion
                #endregion
                #region 5楼灯光
                case 1 * 256 + 1:
                    this._logic.light.LightFiveDiaoDeng=65535;
                    break;
                case 1 * 256 + 2:
                    this._logic.light.LightFiveDiaoDeng=0;
                    break;
                case 1 * 256 + 3:
                    this._logic.light.LightFiveBiDeng=65535;
                    break;
                case 1 * 256 + 4:
                    this._logic.light.LightFiveBiDeng=0;
                    break;
                case 1 * 256 + 5:
                    this._logic.light.LightFiveDengDai(true);

                    break;
                case 1 * 256 + 6:
                    this._logic.light.LightFiveDengDai(false);

                    break;
                case 1 * 256 + 7:
                    this._logic.light.LightFiveKongTiao=65535;

                    break;
                case 1 * 256 + 8:
                    this._logic.light.LightFiveKongTiao=0;

                    break;
                case 1 * 256 + 9:
                    this._logic.light.LightFiveJinMen=65535;

                    break;
                case 1 * 256 + 10:
                    this._logic.light.LightFiveJinMen=0;

                    break;
                case 1 * 256 + 11:
                    this._logic.light.LightFiveAll(true);
                    break;
                case 1 * 256 + 12:
                    this._logic.light.LightFiveAll(false);
                    //this._logic.light.LightOneXiaoTongDeng(false);
                    break;
                case 2 * 256 + 1:
                    //吊灯 暗
                    this._logic.light.LightFiveDiaoDeng -= 6553;
                    break;
                case 2 * 256 + 2:
                    //吊灯 亮
                    this._logic.light.LightFiveDiaoDeng += 6553;

                    break;
                case 2 * 256 + 3:
                    //壁灯 暗
                    this._logic.light.LightFiveBiDeng -= 6553;
                    break;
                case 2 * 256 + 4:
                    //壁灯 亮
                    this._logic.light.LightFiveBiDeng += 6553;

                    break;
                case 2 * 256 + 5:
                    //空调口 暗
                    this._logic.light.LightFiveKongTiao -= 6553;
                    break;
                case 2 * 256 + 6:
                    //空调口 亮
                    this._logic.light.LightFiveKongTiao += 6553;

                    break;

                case 2 * 256 + 7:
                    //进门 暗
                    this._logic.light.LightFiveJinMen -= 6553;
                    break;
                case 2 * 256 + 8:
                    //进门 亮
                    this._logic.light.LightFiveJinMen += 6553;

                    break;
                #endregion
                #region 客厅安防
                case 5 * 256 + 1:
                    //布防
                    break;
                case 5 * 256 + 2:
                    //撤防
                    break;
                case 5 * 256 + 3:
                    //大门
                    break;
                case 5 * 256 + 4:
                    //小门
                    break;
                case 5 * 256 + 5:
                    this._logic.DoorMuMenOpen();
                    //木门
                    break;
                #endregion
                default:
                    break;
            }
        }
        //四楼触摸屏
        private void tpc5_SixEvent(int btnid)
        {
            switch (btnid)
            {
                case 1 * 256 + 1:
                    this._logic.light.LightFourDiaoDeng(true);
                    break;
                case 1 * 256 + 2:
                    this._logic.light.LightFourDiaoDeng(false);

                    break;
                case 1 * 256 + 3:
                    this._logic.light.LightFourBiDeng(true);
                    break;
                case 1 * 256 + 4:
                    this._logic.light.LightFourBiDeng(false);
                    break;
                case 1 * 256 + 5:
                    this._logic.light.LightFourDengDai(true);
                    break;
                case 1 * 256 + 6:
                    this._logic.light.LightFourDengDai(false);
                    break;
                case 1 * 256 + 7:
                    this._logic.light.LightFourKongTiao(true);
                    break;
                case 1 * 256 + 8:
                    this._logic.light.LightFourKongTiao(false);
                    break;
                case 1 * 256 + 9:
                    this._logic.light.LightFourJinMen(true);

                    break;
                case 1 * 256 + 10:
                    this._logic.light.LightFourJinMen(false);

                    break;
                case 1 * 256 + 11:
                    this._logic.light.LightFourChuanTou(true);

                    break;
                case 1 * 256 + 12:
                    this._logic.light.LightFourChuanTou(false);

                    break;
                case 1 * 256 + 13:
                    this._logic.light.LightFourAll(true);

                    break;
                case 1 * 256 + 14:
                    this._logic.light.LightFourAll(false);

                    break;
                case 1 * 256 + 15:
                    //this._logic.light.LightThreeAll(true);

                    break;
                case 5 * 256 + 1://布防:
                    break;
                case 5 * 256 + 2://撤防
                    break;
                case 5 * 256 + 3://大门
                    break;
                case 5 * 256 + 4://校门
                    break;
                case 5 * 256 + 5://木门
                    break;
                default:
                    break;
            }
        }
        #region 客厅触摸屏
        /// <summary>
        /// 客厅触摸屏
        /// </summary>
        /// <param name="btnid"></param>
        private void tpc5_FourEvent(int btnid)
        {
            switch (btnid)
            {
                #region 客厅灯光
                case 1 * 256 + 1:
                    this._logic.light.LightOneXiaoDiaoDeng(true);
                    break;
                case 1 * 256 + 2:
                    this._logic.light.LightOneXiaoDiaoDeng(false);
                    break;
                case 1 * 256 + 3:
                    this._logic.light.LightOneDaDiaoDeng(true);
                    break;
                case 1 * 256 + 4:
                    this._logic.light.LightOneDaDiaoDeng(false);
                    break;
                case 1 * 256 + 5:
                    this._logic.light.LightOneXiaoDengDai(true);

                    break;
                case 1 * 256 + 6:
                    this._logic.light.LightOneXiaoDengDai(false);

                    break;
                case 1 * 256 + 7:
                    this._logic.light.LightOneDaDengDai(true);

                    break;
                case 1 * 256 + 8:
                    this._logic.light.LightOneDaDengDai(false);

                    break;
                case 1 * 256 + 9:
                    this._logic.light.LightOneBiDeng(true);

                    break;
                case 1 * 256 + 10:
                    this._logic.light.LightOneBiDeng(false);

                    break;
                case 1 * 256 + 11:
                    this._logic.light.LightOneXiaoTongDeng = 65535;
                    break;
                case 1 * 256 + 12:
                        this._logic.light.LightOneXiaoTongDeng = 0;
                    //this._logic.light.LightOneXiaoTongDeng(false);
                    break;
                case 1 * 256 + 13:
                    this._logic.light.LightOneZhongTongDeng = 65535;


                    break;
                case 1 * 256 + 14:
                    this._logic.light.LightOneZhongTongDeng = 0;

                    break;
                case 1 * 256 + 15:
                    this._logic.light.LightOneDaTongDeng = 65535;


                    break;
                case 1 * 256 + 16:
                    this._logic.light.LightOneDaTongDeng = 0;

                    break;
                case 1 * 256 + 17:
                    this._logic.light.LightOneAll(true);

                    break;
                case 1 * 256 + 18:
                    this._logic.light.LightOneAll(false);

                    break;
                case 1 * 256 + 19:
                    //照明
                    this._logic.light.LightOneZhaoMing();
                    break;
                case 1 * 256 + 20:
                    //休闲
                    this._logic.light.LightOneXiuXian();
                    break;
                case 2 * 256 + 1:
                    //筒灯一 暗
                    this._logic.light.LightOneXiaoTongDeng -= 6553;
                    break;
                case 2 * 256 + 2:
                    //筒灯一 亮
                    this._logic.light.LightOneXiaoTongDeng += 6553;

                    break;
                case 2 * 256 + 3:
                    //筒灯二 暗
                    this._logic.light.LightOneZhongTongDeng -= 6553;
                    break;
                case 2 * 256 + 4:
                    //筒灯二 亮
                    this._logic.light.LightOneZhongTongDeng += 6553;

                    break;
                case 2 * 256 + 5:
                    //筒灯三 暗
                    this._logic.light.LightOneDaTongDeng -= 6553;
                    break;
                case 2 * 256 + 6:
                    //筒灯三 亮
                    this._logic.light.LightOneDaTongDeng += 6553;

                    break;
                #endregion
                #region 客厅窗帘
                case 3 * 256 + 1:
                    this._logic.Curtains.Windows1Open();
                    break;
                case 3 * 256 + 2:
                    this._logic.Curtains.Windows1Stop();
                    break;
                case 3 * 256 + 3:
                    this._logic.Curtains.Windows1Close();
                    break;
                case 3 * 256 + 4:
                    this._logic.Curtains.Windows2Open();
                    break;
                case 3 * 256 + 5:
                    this._logic.Curtains.Windows2Stop();
                    break;
                case 3 * 256 + 6:
                    this._logic.Curtains.Windows2Close();
                    break;
                case 3 * 256 + 7:
                    this._logic.Curtains.Windows3Open();
                    break;
                case 3 * 256 + 8:
                    this._logic.Curtains.Windows3Stop();
                    break;
                case 3 * 256 + 9:
                    this._logic.Curtains.Windows3Close();
                    break;
                case 3 * 256 + 10:
                    this._logic.Curtains.Windows4Open();
                    break;
                case 3 * 256 + 11:
                    this._logic.Curtains.Windows4Stop();
                    break;
                case 3 * 256 + 12:
                    this._logic.Curtains.Windows4Close();
                    break;
                case 3 * 256 + 13:
                    this._logic.Curtains.Windows5Open();
                    break;
                case 3 * 256 + 14:
                    this._logic.Curtains.Windows5Stop();
                    break;
                case 3 * 256 + 15:
                    this._logic.Curtains.Windows5Close();
                    break;
                case 3 * 256 + 16:
                    this._logic.Curtains.Windows6Open();
                    break;
                case 3 * 256 + 17:
                    this._logic.Curtains.Windows6Stop();
                    break;
                case 3 * 256 + 18:
                    this._logic.Curtains.Windows6Close();
                    break;
                #endregion
                #region 客厅音乐
                #region 地下室
                case 1:
                    this._logic.Muisc.MusicPower(5, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case 2:
                    this._logic.Muisc.MusicPower(5, false);
                    break;
                case 3:
                    GlobalInfo.Instance.MusicVol5 += 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol7);

                    break;
                case 4:
                    GlobalInfo.Instance.MusicVol5 -= 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol7);


                    break;
                case 5://上一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x01, 0x81);
                    break;
                case 6://下一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x10, 0x81);

                    break;
                case 7://暂停
                    this._logic.Muisc.PlaySet(5, 0x02, 0x81);
                    break;
                case 8://播放
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case 9://列表
                    this._logic.Muisc.PlayModeSet(5, 0x03);
                    break;
                case 10://单曲
                    this._logic.Muisc.PlayModeSet(5, 0x02);

                    break;
                case 11://随机
                    this._logic.Muisc.PlayModeSet(5, 0x05);
                    break;
                case 12:
                    this._logic.Muisc.PlaySet(5, 0x04, 0x81);
                    break; //停止
              
                #endregion
                #region 茶室
                case 17:
                    this._logic.Muisc.MusicPower(7, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 18:
                    this._logic.Muisc.MusicPower(7, false);
                    break;
                case 19:
                    GlobalInfo.Instance.MusicVol7 += 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol5);

                    break;
                case 20:
                    GlobalInfo.Instance.MusicVol7 -= 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol5);
                    break;
                case 21://上一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x01, 0x81);
                    break;
                case 22://下一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x10, 0x81);

                    break;
                case 23://暂停
                    this._logic.Muisc.PlaySet(7, 0x02, 0x81);
                    break;
                case 24://播放
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 25://列表
                    this._logic.Muisc.PlayModeSet(7, 0x03);
                    break;
                case 26://单曲
                    this._logic.Muisc.PlayModeSet(7, 0x02);

                    break;
                case 27://随机
                    this._logic.Muisc.PlayModeSet(7, 0x05);
                    break;
                case 28:
                    this._logic.Muisc.PlaySet(7, 0x04, 0x81);
                    break; //停止

                #endregion
                #region 户外
                case 33:
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
                case 34:
                    this._logic.Muisc.MusicPower(1, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(2, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(3, false);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicPower(4, false);
                    Thread.Sleep(300);
                    break;
                case 40:
                    GlobalInfo.Instance.MusicVol1 += 10;

                    this._logic.Muisc.VolSet(1, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(2, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(3, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(4, (byte)GlobalInfo.Instance.MusicVol1);
                    break;
                case 39:
                    GlobalInfo.Instance.MusicVol1 -= 10;


                    this._logic.Muisc.VolSet(1, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(2, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(3, (byte)GlobalInfo.Instance.MusicVol1);
                    Thread.Sleep(300);
                    this._logic.Muisc.VolSet(4, (byte)GlobalInfo.Instance.MusicVol1);
                     break;
                case 35://上一曲
                    this._logic.Muisc.MusicChangeSet(1, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(2, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(3, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(4, 0x01, 0x81);
                    break;
                case 36://下一曲
                    this._logic.Muisc.MusicChangeSet(1, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(2, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(3, 0x10, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.MusicChangeSet(4, 0x10, 0x81);
                    break;
                case 37://暂停
                    this._logic.Muisc.PlaySet(1, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x02, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x02, 0x81);
                    break;
                case 38://播放
                    this._logic.Muisc.PlaySet(1, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x01, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x01, 0x81);
                    break;
                case 41://列表
                    this._logic.Muisc.PlayModeSet(1, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x03);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x03);

                    break;
                case 42://单曲
                    this._logic.Muisc.PlayModeSet(1, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x02);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x02);

                    break;
                case 43://随机
                    this._logic.Muisc.PlayModeSet(1, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(2, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(3, 0x05);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlayModeSet(4, 0x05);

                    break;
                case 44:
                    this._logic.Muisc.PlaySet(1, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(2, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(3, 0x04, 0x81);
                    Thread.Sleep(300);
                    this._logic.Muisc.PlaySet(4, 0x04, 0x81);

                    break; //停止

                #endregion
                #region 阳台
                case 49:
                    this._logic.Muisc.MusicPower(6, true);
                    break;
                case 50:
                    this._logic.Muisc.MusicPower(6, false);

                    break;
                case 51:
                    this._logic.Muisc.MusicChangeSet(6, 0x01, 0x81);

                    break;
                case 52:
                    this._logic.Muisc.MusicChangeSet(6, 0x10, 0x81);
                    break;
                case 53 :
                    this._logic.Muisc.PlaySet(6, 0x02, 0x81);

                    break;
                case 54:
                    this._logic.Muisc.PlaySet(6, 0x01, 0x81);

                    break;
                case 55:
                    GlobalInfo.Instance.MusicVol6 -= 10;

                    this._logic.Muisc.VolSet(6, (byte)GlobalInfo.Instance.MusicVol6);
                    break;
                case 56:
                    GlobalInfo.Instance.MusicVol6 += 10;

                    this._logic.Muisc.VolSet(6, (byte)GlobalInfo.Instance.MusicVol6);
                    break;
                case 57:
                    this._logic.Muisc.PlayModeSet(6, 0x03);

                    break;
                case 58: 
                    this._logic.Muisc.PlayModeSet(6, 0x02);

                    break;
                case 59:
                    this._logic.Muisc.PlayModeSet(6, 0x05);

                    break;
                case 60:
                    this._logic.Muisc.PlaySet(6, 0x04, 0x81);
                    break;
                #endregion
                #endregion
                #region 客厅空调

                #endregion
                #region 客厅安防
                case 5 * 256 + 1:
                    //布防
                    break;
                case 5 * 256 + 2:
                    //撤防
                    break;
                case 5 * 256 + 3:
                    //大门
                    break;
                case 5 * 256 + 4:
                    //小门
                    break;
                case 5 * 256 + 5:
                    this._logic.DoorMuMenOpen();
                    //木门
                    break;
                #endregion
                default:
                    break;
            }
        }
        #endregion
        #region 地下室触摸屏
        /// <summary>
        /// 地下室触摸屏
        /// </summary>
        /// <param name="btnid"></param>
        private void tpc5_OneEvent(int btnid)
        {
            switch (btnid)
            {
                case 1:
                    this._logic.Muisc.MusicPower(5, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case 2:
                    this._logic.Muisc.MusicPower(5, false);
                    break;
                case 11:
                    GlobalInfo.Instance.MusicVol5 += 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol5);

                    break;
                case 10:
                    GlobalInfo.Instance.MusicVol5 -= 10;

                    this._logic.Muisc.VolSet(5, (byte)GlobalInfo.Instance.MusicVol5);


                    break;
                case 5://上一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x01, 0x81);
                    break;
                case 6://下一曲
                    this._logic.Muisc.MusicChangeSet(5, 0x10, 0x81);

                    break;
                case 7://暂停
                    this._logic.Muisc.PlaySet(5, 0x02, 0x81);
                    break;
                case 8://播放
                    this._logic.Muisc.PlaySet(5, 0x01, 0x81);
                    break;
                case 22://列表
                    this._logic.Muisc.PlayModeSet(5, 0x03);
                    break;
                case 23://单曲
                    this._logic.Muisc.PlayModeSet(5, 0x02);

                    break;
                case 24://随机
                    this._logic.Muisc.PlayModeSet(5, 0x05);
                    break;
                case 25:
                    this._logic.Muisc.PlaySet(5, 0x04, 0x81);
                    break; //停止
                default:
                    break;
            }
        }
        #endregion
        #region 餐厅触摸屏
        /// <summary>
        /// 餐厅触摸屏1
        /// </summary>
        /// <param name="btnid"></param>
        private void tpc5_TwoEvent(int btnid)
        {
            switch (btnid)
            {
                case 1:
                    this._logic.Muisc.MusicPower(7, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 2:
                    this._logic.Muisc.MusicPower(7, false);
                    break;
                case 11:
                    GlobalInfo.Instance.MusicVol7 += 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol7);

                    break;
                case 10:
                    GlobalInfo.Instance.MusicVol7 -= 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol7);


                    break;
                case 5://上一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x01, 0x81);
                    break;
                case 6://下一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x10, 0x81);

                    break;
                case 7://暂停
                    this._logic.Muisc.PlaySet(7, 0x02, 0x81);
                    break;
                case 8://播放
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 22://列表
                    this._logic.Muisc.PlayModeSet(7, 0x03);
                    break;
                case 23://单曲
                    this._logic.Muisc.PlayModeSet(7, 0x02);

                    break;
                case 24://随机
                    this._logic.Muisc.PlayModeSet(7, 0x05);
                    break;
                case 25:
                    this._logic.Muisc.PlaySet(7, 0x04, 0x81);
                    break; //停止
                default:
                    break;
            }
        }

        /// <summary>
        /// 餐厅触摸屏2
        /// </summary>
        /// <param name="btnid"></param>
        private void tpc5_ThreeEvent(int btnid)
        {
            switch (btnid)
            {
                case 1:
                    this._logic.Muisc.MusicPower(7, true);
                    Thread.Sleep(1000);
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 2:
                    this._logic.Muisc.MusicPower(7, false);
                    break;
                case 11:
                    GlobalInfo.Instance.MusicVol7 += 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol7);

                    break;
                case 10:
                    GlobalInfo.Instance.MusicVol7 -= 10;

                    this._logic.Muisc.VolSet(7, (byte)GlobalInfo.Instance.MusicVol7);


                    break;
                case 5://上一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x01, 0x81);
                    break;
                case 6://下一曲
                    this._logic.Muisc.MusicChangeSet(7, 0x10, 0x81);

                    break;
                case 7://暂停
                    this._logic.Muisc.PlaySet(7, 0x02, 0x81);
                    break;
                case 8://播放
                    this._logic.Muisc.PlaySet(7, 0x01, 0x81);
                    break;
                case 22://列表
                    this._logic.Muisc.PlayModeSet(7, 0x03);
                    break;
                case 23://单曲
                    this._logic.Muisc.PlayModeSet(7, 0x02);

                    break;
                case 24://随机
                    this._logic.Muisc.PlayModeSet(7, 0x05);
                    break;
                case 25:
                    this._logic.Muisc.PlaySet(7, 0x04, 0x81);
                    break; //停止
                default:
                    break;
            }
        }
        #endregion

        

        #region 面板不同页面
        //void tpc5_OneLight(int btnid)
        //{
        //    switch (btnid)
        //    {
        //        case 1:
        //            this._logic.light.LightOneXiaoDiaoDeng(true);
        //            break;
        //        case 2:
        //            this._logic.light.LightOneXiaoDiaoDeng(false);
        //            break;
        //        case 3:
        //            this._logic.light.LightOneDaDiaoDeng(true);

        //            break;
        //        case 4:
        //            this._logic.light.LightOneDaDiaoDeng(false);

        //            break;
        //        case 5:
        //            this._logic.light.LightOneXiaoDengDai(true);

        //            break;
        //        case 6:
        //            this._logic.light.LightOneXiaoDengDai(false);

        //            break;
        //        case 7:
        //            this._logic.light.LightOneDaDengDai(true);

        //            break;
        //        case 8:
        //            this._logic.light.LightOneDaDengDai(false);

        //            break;
        //        case 9:
        //            this._logic.light.LightOneBiDeng(true);

        //            break;
        //        case 10:
        //            this._logic.light.LightOneBiDeng(false);

        //            break;
        //        case 11:
        //            this._logic.light.LightOneXiaoTongDeng = 65535;

        //        //    this._logic.light.LightOneXiaoTongDeng(true);

        //            break;
        //        case 12:
        //            //this._logic.light.LightOneXiaoTongDeng(false);
        //            this._logic.light.LightOneXiaoTongDeng = 0;

        //            break;
        //        case 13:
        //            this._logic.light.LightOneZhongTongDeng = 65535;


        //            break;
        //        case 14:
        //            this._logic.light.LightOneZhongTongDeng = 65535;

        //            break;
        //        case 15:
        //            this._logic.light.LightOneDaTongDeng = 65535;

        //            break;
        //        case 16:
        //            this._logic.light.LightOneDaTongDeng = 0;

        //            break;
        //        case 17:
        //            this._logic.light.LightOneAll(true);

        //            break;
        //        case 18:
        //            this._logic.light.LightOneAll(false);

        //            break;
        //        default:
        //            break;
        //    } 
        //}
        //void tpc5_ThreeLight(int btnid)
        //{
        //    switch (btnid)
        //    {
        //        case 1:
        //            this._logic.light.LightThreeDiaoDeng(true);
        //            break;
        //        case 2:
        //            this._logic.light.LightThreeDiaoDeng(false);
        //            break;
        //        case 3:
        //            this._logic.light.LightThreeBiDeng(true);

        //            break;
        //        case 4:
        //            this._logic.light.LightThreeBiDeng(false);

        //            break;
        //        case 5:
        //            this._logic.light.LightThreeDengDai(true);

        //            break;
        //        case 6:
        //            this._logic.light.LightThreeDengDai(false);

        //            break;
        //        case 7:
        //            this._logic.light.LightThreeKongTiao(true);

        //            break;
        //        case 8:
        //            this._logic.light.LightThreeKongTiao(false);

        //            break;
        //        case 9:
        //            this._logic.light.LightThreeJinMen(true);

        //            break;
        //        case 10:
        //            this._logic.light.LightThreeJinMen(false);

        //            break;
        //        case 11:
        //            this._logic.light.LightThreeChuanTou(true);

        //            break;
        //        case 12:
        //            this._logic.light.LightThreeChuanTou(false);

        //            break;

        //        case 13:
        //            this._logic.light.LightThreeAll(true);

        //            break;
        //        case 14:
        //            this._logic.light.LightThreeAll(false);

        //            break;
        //        default:
        //            break;
        //    }
        //}
        //void tpc5_FourLight(int btnid)
        //{
        //    switch (btnid)
        //    {
        //        case 1:
        //            this._logic.light.LightFourDiaoDeng(true);
        //            break;
        //        case 2:
        //            this._logic.light.LightFourDiaoDeng(false);
        //            break;
        //        case 3:
        //            this._logic.light.LightFourBiDeng(true);

        //            break;
        //        case 4:
        //            this._logic.light.LightFourBiDeng(false);

        //            break;
        //        case 5:
        //            this._logic.light.LightFourDengDai(true);

        //            break;
        //        case 6:
        //            this._logic.light.LightFourDengDai(false);

        //            break;
        //        case 7:
        //            this._logic.light.LightFourKongTiao(true);

        //            break;
        //        case 8:
        //            this._logic.light.LightFourKongTiao(false);

        //            break;
        //        case 9:
        //            this._logic.light.LightFourJinMen(true);

        //            break;
        //        case 10:
        //            this._logic.light.LightFourJinMen(false);

        //            break;
        //        case 11:
        //            this._logic.light.LightFourChuanTou(true);

        //            break;
        //        case 12:
        //            this._logic.light.LightFourChuanTou(false);

        //            break;
        //        case 13:
        //            this._logic.light.LightFourAll(true);

        //            break;
        //        case 14:
        //            this._logic.light.LightFourAll(false);

        //            break;
        //        default:
        //            break;
        //    }
        //}
        //void tpc5_FiveLight(int btnid)
        //{
        //    switch (btnid)
        //    {
        //        case 1:
        //            this._logic.light.LightFiveDiaoDeng=65535;//.LightFiveDiaoDeng(true);
        //            break;
        //        case 2:
        //            this._logic.light.LightFiveDiaoDeng=0;//.LightFiveDiaoDeng(false);
        //            break;
        //        case 3:
        //            this._logic.light.LightFiveBiDeng = 65535;// (true);

        //            break;
        //        case 4:
        //            this._logic.light.LightFiveBiDeng = 0;// (false);

        //            break;
        //        case 5:
        //            this._logic.light.LightFiveDengDai(true);

        //            break;
        //        case 6:
        //            this._logic.light.LightFiveDengDai(false);

        //            break;
        //        case 7:
        //            this._logic.light.LightFiveKongTiao = 65535;// (true);

        //            break;
        //        case 8:
        //            this._logic.light.LightFiveKongTiao = 0;// (false);

        //            break;
        //        case 9:
        //            this._logic.light.LightFiveJinMen = 65535;// (true);

        //            break;
        //        case 10:
        //            this._logic.light.LightFiveJinMen = 0;// (false);

        //            break;
        //        case 13:
        //            this._logic.light.LightFiveAll(true);

        //            break;
        //        case 14:
        //            this._logic.light.LightFiveAll(false);

        //            break;
        //        default:
        //            break;
        //    }
        //}
        /*void tpc5_PushTPCIEvent(int id, int scrid, int btnid)
        {   
            //大厅餐厅
            if (id == 1)
            {
                this.tpc5_OneFloor(scrid,btnid);
            }            
            else if (id == 4)
            {
                this.tpc5_FiveFloor(scrid,btnid);

            }
            ILiveDebug.Instance.WriteLine(string.Format("TPCEvent ID:{0},Screen:{1},Button:{2}", id, scrid, btnid));
            // throw new NotImplementedException();
        }
        void tpc5_OneFloor(int scrid, int btnid) 
        {
            if (scrid == 1)
            {
                this.tpc5_OneDim(btnid);
            }
            else if (scrid == 2)
            {
                this.tpc5_CL(btnid);
            }
            else if (scrid == 3)
            {
                this.tpc5_Music(btnid);
            }
            else if (scrid == 4) 
            {
                this.tpc5_Mode(btnid);
            }
        }
        void tpc5_FiveFloor(int scrid, int btnid)
        {
            if (scrid == 1) 
            {
                this.tpc5_FiveDim(btnid);
            }
        }
        //一楼调光
        void tpc5_OneDim(int btnid)
        {
            switch (btnid)
            {
                case 1:
                    this._logic.light.LightOneXiaoTongDeng(false);
                    break;
                case 7:
                    this._logic.light.LightOneXiaoTongDeng(false);
                    break;
                case 13:
                    this._logic.light.LightOneZhongTongDeng(false);
                        break;
                case 19:
                        this._logic.light.LightOneZhongTongDeng(false);
                    break;
                case 25:
                    this._logic.light.LightOneDaTongDeng(false);
                    break;
                case 31:
                    this._logic.light.LightOneDaTongDeng(false);
                    break;
                default:
                    break;
            }
        }
        //窗帘
        void tpc5_CL(int btnid)
        {
            switch (btnid)
            {
                    //纱帘左
                case 1:
                    switch(btnid)
                    {
                        case 13:
                            this._logic.DaTingSha1Open();
                            break;
                        case 15:
                            this._logic.DaTingSha1Stop();
                            break;
                        case 17:
                            this._logic.DaTingSha1Close();
                            break;
                    }
                    break;
                    //布帘左
                case 3:
                    switch (btnid)
                    {
                        case 13:
                            this._logic.DaTingBu1Open();
                            break;
                        case 15:
                            this._logic.DaTingBu1Stop();
                            break;
                        case 17:
                            this._logic.DaTingBu1Close();
                            break;
                    }
                    break;
                    //纱帘右
                case 5:
                    switch (btnid)
                    {
                        case 13:
                            this._logic.DaTingSha2Open();
                            break;
                        case 15:
                            this._logic.DaTingSha2Stop();
                            break;
                        case 17:
                            this._logic.DaTingSha2Close();
                            break;
                    }
                    break;
                //布帘右
                case 7:
                    switch (btnid)
                    {
                        case 13:
                            this._logic.DaTingBu2Open();
                            break;
                        case 15:
                            this._logic.DaTingBu2Stop();
                            break;
                        case 17:
                            this._logic.DaTingBu2Close();
                            break;
                    }
                    break;
                //侧边纱帘
                case 9:
                    switch (btnid)
                    {
                        case 13:
                            this._logic.DaTingBu2Open();
                            break;
                        case 15:
                            this._logic.DaTingBu2Stop();
                            break;
                        case 17:
                            this._logic.DaTingBu2Close();
                            break;
                    }
                    break;
                //侧边布帘
                case 11:
                    switch (btnid)
                    {
                        case 13:
                            this._logic.DaTingBu2Open();
                            break;
                        case 15:
                            this._logic.DaTingBu2Stop();
                            break;
                        case 17:
                            this._logic.DaTingBu2Close();
                            break;
                    }
                    break;
                    
                default:
                    switch (btnid)
                    {
                        case 13:
                            this._logic.DaTingSha1Open();
                            this._logic.DaTingBu1Open();
                            this._logic.DaTingSha2Open();
                            this._logic.DaTingBu2Open();
                            this._logic.DaTingCeSha1Open();
                            this._logic.DaTingCeBu1Open();
                            break;
                        case 15:
                            this._logic.DaTingSha1Stop();
                            this._logic.DaTingBu1Stop();
                            this._logic.DaTingSha2Stop();
                            this._logic.DaTingBu2Stop();
                            this._logic.DaTingCeSha1Stop();
                            this._logic.DaTingCeBu1Stop();
                            break;
                        case 17:
                            this._logic.DaTingSha1Close();
                            this._logic.DaTingBu1Close();
                            this._logic.DaTingSha2Close();
                            this._logic.DaTingBu2Close();
                            this._logic.DaTingCeSha1Close();
                            this._logic.DaTingCeBu1Close();
                            break;
                    }
                    break;
            }
            
        }
        //背景音乐
        void tpc5_Music(int btnid)
        {
            switch (btnid)
            {
                //主卧
                case 11:
                    switch (btnid)
                    {
                        case 13:
                            this._logic.DaTingBu2Open();
                            break;
                        case 15:
                            this._logic.DaTingBu2Stop();
                            break;
                        case 17:
                            this._logic.DaTingBu2Close();
                            break;
                    }
                    break;
                //阳台
                //大厅
                //餐厅
                //地下室
                //休闲区
            }
        }
        //一楼模式
        void tpc5_Mode(int btnid)
        {
            switch (btnid)
            {
                case 5:
                    this._logic.ScenceLeave();
                    break;
                default:
                    break;
            }
        }
        //五楼调光
        void tpc5_FiveDim(int btnid)
        {
            switch (btnid)
            {
                case 1:
                    this._logic.light.LightFiveDiaoDeng(true);
                    break;
                default:
                    break;
            }
        }*/
        #endregion

        #endregion
        #region 16I事件
        void crodigy16I_Push16IEvent(int id,int btnid, bool iChanStatus)
        {
            if (id==27)
            {
                if (iChanStatus)
                {
                    switch (btnid)
                    {
                        case 1:
                            //一楼大厅灯带
                            this._logic.light.LightOneDaDengDai(true);
                            break;
                        case 2:
                            //一楼灯光全开
                            this._logic.light.LightOneAll(true);
                            break;
                        case 3:
                            //一楼灯光全关
                            this._logic.light.LightOneAll(false);
                            break;
                        default:
                            break;
                    }
                }
            }
 
            if (id == 28)
            {
                if (iChanStatus)
                {
                    switch (btnid)
                    {

                        case 1:
                            this._logic.light.LightFiveBiDeng = 65535;// (true);
                            break;
                        case 2:
                            this._logic.light.LightFiveDengDai(true);
                            break;
                        case 3:
                            this._logic.light.LightFiveAll(false);
                            break;
                        case 4:
                            this._logic.light.LightFiveBiDeng = 65535;// (true);
                            break;
                        case 5:
                            this._logic.light.LightFiveDengDai(true);
                            break;
                        case 6:
                            this._logic.light.LightFiveAll(false);
                            break;
                        case 7:
                            this._logic.light.LightFourBiDeng(true);
                            break;
                        case 8:
                            this._logic.light.LightFourDengDai(true);
                            break;
                        case 9:
                            this._logic.light.LightFourAll(false);
                            break;
                        case 10:
                            this._logic.light.LightFourBiDeng(true);
                            break;
                        case 11:
                            this._logic.light.LightFourDengDai(true);
                            break;
                        case 12:
                            this._logic.light.LightFourAll(false);
                            break;
                        case 13:
                            this._logic.light.LightThreeBiDeng(true);
                            break;
                        case 14:
                            this._logic.light.LightThreeDengDai(true);

                            break;
                        case 15:
                            this._logic.light.LightThreeAll(false);

                            break;
                        default:
                            break;
                    }
                }
            }

        }
        #endregion
        #region 场景
        public object ScenceLeave(object o)
        {
          //  this._logic.ScenceLeave();
            return o;
        }
        public object ScenceWelcome(object o)
        {
           // this._logic.ScenceWelcome();
            return o;
        }
        
        private object TempToggle(object o)
        {
            return o;
        }
        #endregion

        
        
        //#region IPAD事件

        //void Ipad_DataReceived(object sender, string message, EventArgs e)
        //{
        //    ILiveDebug.Instance.WriteLine("IpadReceiveed:" + message);
        //    if (!message.StartsWith("CP3IPADCMD:"))
        //    {
        //        return;
        //    }
        //    message = message.Replace("CP3IPADCMD:", "");

        //    switch (message)
        //    {
        //        #region 场景
        //        case "ScenceLeaveMode":
        //            this.ipad.WSServer_Send("CP3IPADRET:ScenceLeaveModeStart");
        //            this._logic.ScenceLeave();
        //            this.ipad.WSServer_Send("CP3IPADRET:ScenceLeaveModeEnd");
        //            break;
        //        #endregion



        //        #region 灯光
        //        #region 一楼
        //            //全开
        //        case "LightAllOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightAllOpenStart");
        //            this._logic.light.LightOneAll(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightAllOpenEnd");
        //            break;
        //            //全关
        //        case "LightAllClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightAllCloseStart");
        //            this._logic.light.LightOneAll(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightAllCloseEnd");
        //            break;
        //            //大厅小吊灯
        //        case "LightOneXiaoDiaoDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDiaoDengOpenStart");
        //            this._logic.light.LightOneXiaoDiaoDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDiaoDengOpenEnd");
        //            break;
        //        case "LightOneXiaoDiaoDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDiaoDengCloseStart");
        //            this._logic.light.LightOneXiaoDiaoDeng(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDiaoDengCloseEnd");
        //            break;
        //            //大厅大灯带
        //        case "LightOneDaDengDaiOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDengDaiOpenStart");
        //            this._logic.light.LightOneDaDengDai(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDengDaiOpenEnd");
        //            break;
        //        case "LightOneDaDengDaiClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDengDaiCloseStart");
        //            this._logic.light.LightOneDaDengDai(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDengDaiCloseEnd");
        //            break;
        //            //大厅小壁灯
        //        case "LightOneXiaoBiDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoBiDengOpenStart");
        //            this._logic.light.LightOneBiDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoBiDengOpenEnd");
        //            break;
        //        case "LightOneXiaoBiDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoBiDengCloseStart");
        //            this._logic.light.LightOneBiDeng(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoBiDengCloseEnd");
        //            break;
        //            //大厅小灯带
        //        case "LightOneXiaoDengDaiOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDengDaiOpenStart");
        //            this._logic.light.LightOneXiaoDengDai(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDengDaiOpenEnd");
        //            break;
        //        case "LightOneXiaoDengDaiClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDengDaiCloseStart");
        //            this._logic.light.LightOneXiaoDengDai(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoDengDaiCloseEnd");
        //            break;
        //            //大吊灯
        //        case "LightOneDaDiaoDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDiaoDengOpenStart");
        //            this._logic.light.LightOneDaDiaoDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDiaoDengOpenEnd");
        //            break;
        //        case "LightOneDaDiaoDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDiaoDengCloseStart");
        //            this._logic.light.LightOneDaDiaoDeng(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaDiaoDengCloseEnd");
        //            break;
        //            //进门小筒灯
        //        case "LightOneXiaoTongDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoTongDengOpenStart");
        //            this._logic.light.LightOneXiaoTongDeng = 65535;

        //            //this._logic.light.LightOneXiaoTongDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoTongDengOpenEnd");
        //            break;
        //        case "LightOneXiaoTongDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoTongDengCloseStart");
        //            this._logic.light.LightOneXiaoTongDeng = 0;

        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneXiaoTongDengCloseEnd");
        //            break;
        //            //大筒灯
        //        case "LightOneDaTongDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaTongDengOpenStart");
        //            this._logic.light.LightOneXiaoTongDeng = 65535;

        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaTongDengOpenEnd");
        //            break;
        //        case "LightOneDaTongDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaTongDengCloseStart");
        //            this._logic.light.LightOneXiaoTongDeng = 0;
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneDaTongDengCloseEnd");
        //            break;
        //            //中筒灯
        //        case "LightOneZhongTongDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneZhongTongDengOpenStart");
        //            this._logic.light.LightOneZhongTongDeng = 65535;

        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneZhongTongDengOpenEnd");
        //            break;
        //        case "LightOneZhongTongDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneZhongTongDengCloseStart");
        //            this._logic.light.LightOneZhongTongDeng = 0;
        //            this.ipad.WSServer_Send("CP3IPADRET:LightOneZhongTongDengCloseEnd");
        //            break;
        //        #endregion
        //        #region 三楼
        //            //全开
        //        case "LightThreeAllOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeAllOpenStart");
        //            this._logic.light.LightThreeAll(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeAllOpenEnd");
        //            break;
        //            //全关
        //        case "LightThreeAllClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeAllCloseStart");
        //            this._logic.light.LightThreeAll(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeAllCloseEnd");
        //            break;
        //            //三楼吊灯
        //        case "LightThreeDiaoDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDiaoDengOpenStart");
        //            this._logic.light.LightThreeDiaoDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDiaoDengOpenEnd");
        //            break;
        //        case "LightThreeDiaoDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDiaoDengCloseStart");
        //            this._logic.light.LightThreeDiaoDeng(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDiaoDengCloseEnd");
        //            break;
        //            //三楼壁灯
        //        case "LightThreeBiDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeBiDengOpenStart");
        //            this._logic.light.LightThreeBiDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeBiDengOpenEnd");
        //            break;
        //        case "LightThreeBiDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeBiDengCloseStart");
        //            this._logic.light.LightThreeBiDeng(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeBiDengCloseEnd");
        //            break;
        //            //三楼灯带
        //        case "LightThreeDengDaiOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDengDaiOpenStart");
        //            this._logic.light.LightThreeDengDai(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDengDaiOpenEnd");
        //            break;
        //        case "LightThreeDengDaiClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDengDaiCloseStart");
        //            this._logic.light.LightThreeDengDai(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeDengDaiCloseEnd");
        //            break;
        //            //三楼空调口筒灯
        //        case "LightThreeKongTiaoOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeKongTiaoOpenStart");
        //            this._logic.light.LightThreeKongTiao(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeKongTiaoOpenEnd");
        //            break;
        //        case "LightThreeKongTiaoClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeKongTiaoCloseStart");
        //            this._logic.light.LightThreeKongTiao(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeKongTiaoCloseEnd");
        //            break;
        //            //三楼进门筒灯
        //        case "LightThreeJinMenOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeJinMenOpenStart");
        //            this._logic.light.LightThreeJinMen(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeJinMenOpenEnd");
        //            break;
        //        case "LightThreeJinMenClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeJinMenCloseStart");
        //            this._logic.light.LightThreeJinMen(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeJinMenCloseEnd");
        //            break;
        //            //三楼床头筒灯
        //        case "LightThreeChuanTouOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeChuanTouOpenStart");
        //            this._logic.light.LightThreeChuanTou(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeChuanTouOpenEnd");
        //            break;
        //        case "LightThreeChuanTouClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeChuanTouCloseStart");
        //            this._logic.light.LightThreeChuanTou(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightThreeChuanTouCloseEnd");
        //            break;
        //        #endregion
        //        #region 四楼
        //        //全开
        //        case "LightFourAllOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourAllOpenStart");
        //            this._logic.light.LightFourAll(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourAllOpenEnd");
        //            break;
        //        //全关
        //        case "LightFourAllClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourAllCloseStart");
        //            this._logic.light.LightFourAll(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourAllCloseEnd");
        //            break;
        //        //四楼吊灯
        //        case "LightFourDiaoDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDiaoDengOpenStart");
        //            this._logic.light.LightFourDiaoDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDiaoDengOpenEnd");
        //            break;
        //        case "LightFourDiaoDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDiaoDengCloseStart");
        //            this._logic.light.LightFourDiaoDeng(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDiaoDengCloseEnd");
        //            break;
        //        //四楼壁灯
        //        case "LightFourBiDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourBiDengOpenStart");
        //            this._logic.light.LightFourBiDeng(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourBiDengOpenEnd");
        //            break;
        //        case "LightFourBiDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourBiDengCloseStart");
        //            this._logic.light.LightFourBiDeng(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourBiDengCloseEnd");
        //            break;
        //        //四楼灯带
        //        case "LightFourDengDaiOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDengDaiOpenStart");
        //            this._logic.light.LightFourDengDai(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDengDaiOpenEnd");
        //            break;
        //        case "LightFourDengDaiClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDengDaiCloseStart");
        //            this._logic.light.LightFourDengDai(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourDengDaiCloseEnd");
        //            break;
        //        //四楼空调口筒灯
        //        case "LightFourKongTiaoOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourKongTiaoOpenStart");
        //            this._logic.light.LightFourKongTiao(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourKongTiaoOpenEnd");
        //            break;
        //        case "LightFourKongTiaoClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourKongTiaoCloseStart");
        //            this._logic.light.LightFourKongTiao(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourKongTiaoCloseEnd");
        //            break;
        //        //四楼进门筒灯
        //        case "LightFourJinMenOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourJinMenOpenStart");
        //            this._logic.light.LightFourJinMen(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourJinMenOpenEnd");
        //            break;
        //        case "LightFourJinMenClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourJinMenCloseStart");
        //            this._logic.light.LightFourJinMen(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourJinMenCloseEnd");
        //            break;
        //        //四楼床头筒灯
        //        case "LightFourChuanTouOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourChuanTouOpenStart");
        //            this._logic.light.LightFourChuanTou(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourChuanTouOpenEnd");
        //            break;
        //        case "LightFourChuanTouClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourChuanTouCloseStart");
        //            this._logic.light.LightFourChuanTou(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFourChuanTouCloseEnd");
        //            break;
        //        #endregion
        //        #region 五楼
        //        //全开
        //        case "LightFiveAllOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveAllOpenStart");
        //            this._logic.light.LightFiveAll(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveAllOpenEnd");
        //            break;
        //        //全关
        //        case "LightFiveAllClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveAllCloseStart");
        //            this._logic.light.LightFiveAll(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveAllCloseEnd");
        //            break;
        //        //五楼吊灯
        //        case "LightFiveDiaoDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDiaoDengOpenStart");
        //            this._logic.light.LightFiveDiaoDeng = 65535;// (true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDiaoDengOpenEnd");
        //            break;
        //        case "LightFiveDiaoDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDiaoDengCloseStart");
        //            this._logic.light.LightFiveDiaoDeng = 0;// (false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDiaoDengCloseEnd");
        //            break;
        //        //五楼壁灯
        //        case "LightFiveBiDengOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveBiDengOpenStart");
        //            this._logic.light.LightFiveBiDeng = 65535;// (true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveBiDengOpenEnd");
        //            break;
        //        case "LightFiveBiDengClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveBiDengCloseStart");
        //            this._logic.light.LightFiveBiDeng = 0;// (false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveBiDengCloseEnd");
        //            break;
        //        //五楼灯带
        //        case "LightFiveDengDaiOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDengDaiOpenStart");
        //            this._logic.light.LightFiveDengDai(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDengDaiOpenEnd");
        //            break;
        //        case "LightFiveDengDaiClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDengDaiCloseStart");
        //            this._logic.light.LightFiveDengDai(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveDengDaiCloseEnd");
        //            break;
        //        //五楼空调口筒灯
        //        case "LightFiveKongTiaoOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveKongTiaoOpenStart");
        //            this._logic.light.LightFiveKongTiao = 65535;// (true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveKongTiaoOpenEnd");
        //            break;
        //        case "LightFiveKongTiaoClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveKongTiaoCloseStart");
        //            this._logic.light.LightFiveKongTiao = 0;// (false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveKongTiaoCloseEnd");
        //            break;
        //        //五楼进门筒灯
        //        case "LightFiveJinMenOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveJinMenOpenStart");
        //            this._logic.light.LightFiveJinMen = 65535;// (true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveJinMenOpenEnd");
        //            break;
        //        case "LightFiveJinMenClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveJinMenCloseStart");
        //            this._logic.light.LightFiveJinMen = 0;// (false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveJinMenCloseEnd");
        //            break;
        //        //五楼床头筒灯
        //        case "LightFiveChuanTouOpen":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveChuanTouOpenStart");
        //            this._logic.light.LightFiveChuanTou(true);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveChuanTouOpenEnd");
        //            break;
        //        case "LightFiveChuanTouClose":
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveChuanTouCloseStart");
        //            this._logic.light.LightFiveChuanTou(false);
        //            this.ipad.WSServer_Send("CP3IPADRET:LightFiveChuanTouCloseEnd");
        //            break;
        //        #endregion
        //        #endregion
        //        #region 窗帘
        //            //布帘1
        //        case "DaTingBu1Open":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu1OpenStart");
        //            ////this._logic.DaTingBu1Open();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu1OpenEnd");
        //            break;
        //        case "DaTingBu1Close":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu1CloseStart");
        //           // this._logic.DaTingBu1Close();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu1CloseEnd");
        //            break;
        //        case "DaTingBu1Stop":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu1StopStart");
        //            //this._logic.DaTingBu1Stop();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu1StopEnd");
        //            break;
        //        //纱帘1
        //        case "DaTingSha1Open":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha1OpenStart");
        //            this._logic.DaTingSha1Open();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha1OpenEnd");
        //            break;
        //        case "DaTingSha1Close":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha1CloseStart");
        //            this._logic.DaTingSha1Close();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha1CloseEnd");
        //            break;
        //        case "DaTingSha1Stop":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha1StopStart");
        //            this._logic.DaTingSha1Stop();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha1StopEnd");
        //            break;
        //        //布帘2
        //        case "DaTingBu2Open":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu2OpenStart");
        //            this._logic.DaTingBu2Open();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu2OpenEnd");
        //            break;
        //        case "DaTingBu2Close":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu2CloseStart");
        //            this._logic.DaTingBu2Close();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu2CloseEnd");
        //            break;
        //        case "DaTingBu2Stop":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu2StopStart");
        //            this._logic.DaTingBu2Stop();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingBu2StopEnd");
        //            break;
        //        //纱帘2
        //        case "DaTingSha2Open":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha2OpenStart");
        //            this._logic.DaTingSha2Open();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha2OpenEnd");
        //            break;
        //        case "DaTingSha2Close":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha2CloseStart");
        //            this._logic.DaTingSha2Close();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha2CloseEnd");
        //            break;
        //        case "DaTingSha2Stop":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha2StopStart");
        //            this._logic.DaTingSha2Stop();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingSha2StopEnd");
        //            break;
        //        //侧边布帘
        //        case "DaTingCeBu1Open":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeBu1OpenStart");
        //            this._logic.DaTingCeBu1Open();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeBu1OpenEnd");
        //            break;
        //        case "DaTingCeBu1Close":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeBu1CloseStart");
        //            this._logic.DaTingCeBu1Close();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeBu1CloseEnd");
        //            break;
        //        case "DaTingCeBu1Stop":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeBu1StopStart");
        //            this._logic.DaTingCeBu1Stop();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeBu1StopEnd");
        //            break;
        //        //侧边纱帘
        //        case "DaTingCeSha1Open":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeSha1OpenStart");
        //            this._logic.DaTingCeSha1Open();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeSha1OpenEnd");
        //            break;
        //        case "DaTingCeSha1Close":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeSha1CloseStart");
        //            this._logic.DaTingCeSha1Close();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeSha1CloseEnd");
        //            break;
        //        case "DaTingCeSha1Stop":
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeSha1StopStart");
        //            this._logic.DaTingCeSha1Stop();
        //            this.ipad.WSServer_Send("CP3IPADRET:DaTingCeSha1StopEnd");
        //            break;
        //        #endregion
        //        #region 空调

        //        #endregion


        //        default:
        //            break;
        //    }
        ////    //this.WSServer_Send(DateTime.Now.ToLongTimeString() + "CP3:" + message);
        ////    //WebSocket接收消息
        //}
        //#endregion
        //#region 手机事件
        /////// <summary>
        /////// IPAD事件
        /////// </summary>
        /////// <param name="currentDevice"></param>
        /////// <param name="args"></param>
        ////void mobile_SigChange(BasicTriList currentDevice, SigEventArgs args)
        ////{
        ////    ILiveDebug.Instance.WriteLine("mobile_SigChange" + args.Sig.Type + args.Sig.Number);
        ////    switch (args.Sig.Type)
        ////    {
        ////        case eSigType.Bool:
        ////            {
        ////                this.ProcessMobileBool(args.Sig.Number, args.Sig.BoolValue);
        ////            }
        ////            break;
        ////        case eSigType.UShort:
        ////            {
        ////                //this.ProcessTsw1052UShort(args.Sig.Number, args.Sig.UShortValue);

        ////            }
        ////            break;
        ////    }

        ////}


        //#endregion



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.Lighting.Din;

namespace ChenSmart
{
    /// <summary>
    /// 灯光控制
    /// </summary>
    public class ILiveLight
    {
        public delegate void LightZoneEventHandler(int zone, bool vl);
        public delegate void LightScenceEventHandler(string scence);

        public event LightScenceEventHandler LightScenceEvent = null;
        public event LightZoneEventHandler ZoneLightEvent = null;

        private CrestronControlSystem controlSystem = null;

        public bool LightScenceIsBusy = false;

        public bool LightIsOn = false;

        private Din1Dim4 din1Dim4_3;
        private Din1Dim4 din1Dim4_4;

        private ILiveGRODIGY8SW8 CongPu_3;
        private ILiveGRODIGY8SW8 CongPu_4;
        private ILiveGRODIGY8SW8 CongPu_5;


       
        private bool _zone1state = false;
        public bool Zone1State
        {
            get
            {
                return this._zone1state;
            }
            set
            {
                if (this.ZoneLightEvent != null)
                {
                    this.ZoneLightEvent(1, value);
                }
                this._zone1state = value;
            }
        }

        public ILiveLight(CrestronControlSystem system)
        {
            this.controlSystem = system;

        }
        public void RegisterDevices()
        {
            #region 注册调光模块

            din1Dim4_3 = new Din1Dim4(0x03, this.controlSystem);
            if (din1Dim4_3.Register() != eDeviceRegistrationUnRegistrationResponse.Success)

                ErrorLog.Error("din1Dim4_10 failed registration. Cause: {0}", din1Dim4_3.RegistrationFailureReason);

            din1Dim4_4 = new Din1Dim4(0x04, this.controlSystem);
            if (din1Dim4_4.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                ErrorLog.Error("din1Dim4_11 failed registration. Cause: {0}", din1Dim4_4.RegistrationFailureReason);
            //din1Dim4_10.DinLoads[0].ParameterDimmable = eDimmable.No;

            #endregion
            #region 注册串口
            if (this.controlSystem.SupportsComPort)
            {

                this.CongPu_3 = new ILiveGRODIGY8SW8(3,8006);
                this.CongPu_4 = new ILiveGRODIGY8SW8(4, 8006);
                this.CongPu_5 = new ILiveGRODIGY8SW8(5, 8006);
            }
            #endregion

        }


        #region 灯光
        #region 一楼

        #region CC
        //CC5.10
        //一楼全开
        public void LightOneAllOn()
        {
            this.LightScenceIsBusy = true;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightOnStart");
            }
            this.LightOneAll(true);
            this.LightIsOn = true;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightOnEnd");
            }
            this.LightScenceIsBusy = false;
        }
        //一楼全关
        public void LightOneAllOff()
        {
            this.LightScenceIsBusy = true;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightOffStart");
            }
            this.LightOneAll(false);
            this.LightIsOn = false;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightOffEnd");
            }
            this.LightScenceIsBusy = false;
        }
        //一楼照明
        public void LightScenceZM()
        {
            this.LightScenceIsBusy = true;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightZMStart");
            }
            this.LightOneZhaoMing();
            this.LightIsOn = true;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightZMEnd");
            }
            this.LightScenceIsBusy = false;
        }
        //一楼休闲
        public void LightScenceXX()
        {
            this.LightScenceIsBusy = true;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightXXStart");
            }
            this.LightOneXiuXian();
            this.LightIsOn = true;
            if (this.LightScenceEvent != null)
            {
                this.LightScenceEvent("LightXXEnd");
            }
            this.LightScenceIsBusy = false;
        }
        #endregion

        internal void LightOneAll(bool p)
        {
            this.LightOneDaDengDai(p);
            this.LightOneDaDiaoDeng(p);
            this.LightOneBiDeng(p);
            this.LightOneXiaoDengDai(p);
            this.LightOneXiaoDiaoDeng(p);
            if (p)
            {
                this.LightOneXiaoTongDeng = 65535;
                this.LightOneDaTongDeng = 65535;
                this.LightOneZhongTongDeng = 65535;
            }
            else
            {
                this.LightOneXiaoTongDeng = 0;
                this.LightOneDaTongDeng = 0;
                this.LightOneZhongTongDeng = 0;

            }
        }
        internal void LightOneZhaoMing()
        {
            this.LightOneDaDengDai(true);
            this.LightOneDaDiaoDeng(false);
            this.LightOneBiDeng(false);
            this.LightOneXiaoDengDai(true);
            this.LightOneXiaoDiaoDeng(false);
                this.LightOneXiaoTongDeng = 0;
                this.LightOneDaTongDeng = 0;
                this.LightOneZhongTongDeng = 0;
        }
        internal void LightOneXiuXian()
        {
            this.LightOneDaDengDai(false);
            this.LightOneDaDiaoDeng(true);
            this.LightOneBiDeng(true);
            this.LightOneXiaoDengDai(false);
            this.LightOneXiaoDiaoDeng(true);
            this.LightOneXiaoTongDeng = 30000;
            this.LightOneDaTongDeng = 30000;
            this.LightOneZhongTongDeng = 30000;
        }
  
        /// <summary>
        /// 一楼大厅小吊灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneXiaoDiaoDeng(bool on)
        {
            if (on)
            {
                this.CongPu_3.Relay8SW8(0, true);
            }
            else
            {
                this.CongPu_3.Relay8SW8(0, false);

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
                this.CongPu_3.Relay8SW8( 1, true);
            }
            else
            {
                this.CongPu_3.Relay8SW8( 1, false);

            }
        }
        /// <summary>
        /// 一楼大厅壁灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneBiDeng(bool on)
        {
            if (on)
            {
                this.CongPu_3.Relay8SW8(2, true);
            }
            else
            {
                this.CongPu_3.Relay8SW8(2, false);

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
                this.CongPu_3.Relay8SW8(3, true);
            }
            else
            {
                this.CongPu_3.Relay8SW8(3, false);

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
                this.CongPu_3.Relay8SW8( 4, true);
            }
            else
            {
                this.CongPu_3.Relay8SW8( 4, false);

            }
        }


        private int _LightOneXiaoTongDeng = 0;
        /// <summary>
        /// 一楼进门筒灯
        /// </summary>
        public int LightOneXiaoTongDeng
        {
            get
            {
                return this._LightOneXiaoTongDeng;
            }
            set
            {
                if (value < 0)
                {
                    this._LightOneXiaoTongDeng = 0;
                }
                else if (value > 65535)
                {
                    this._LightOneXiaoTongDeng = 65535;
                }
                else
                {
                    this._LightOneXiaoTongDeng = value;

                }
                this.din1Dim4_3.DinLoads[1].LevelIn.UShortValue = (ushort)this._LightOneXiaoTongDeng;
            }
        }
        private int _LightOneDaTongDeng = 0;

        public int LightOneDaTongDeng
        {
            get
            {
                return this._LightOneDaTongDeng;
            }
            set
            {

                if (value<0)
                {
                    this._LightOneDaTongDeng = 0;
                }
                else if (value > 65535)
                {
                    this._LightOneDaTongDeng = 65535;
                }
                else
                {
                    this._LightOneDaTongDeng = value;

                }
                this.din1Dim4_3.DinLoads[2].LevelIn.UShortValue = (ushort)this._LightOneDaTongDeng;
            }
        }
      /*  /// <summary>
        /// 大筒灯
        /// </summary>
        /// <param name="on"></param>
        public void LightOneDaTongDeng(bool on)
        {
            if (on)
            {
                this.din1Dim4_3.DinLoads[2].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.din1Dim4_3.DinLoads[2].LevelIn.UShortValue = 0;

            }
        }*/

        private int _LightOneZhongTongDeng = 0;

        public int LightOneZhongTongDeng
        {
            get
            {
                return this._LightOneZhongTongDeng;
            }
            set
            {
                //ILiveDebug.Instance.WriteLine("value:" + value.ToString());


                if (value < 0)
                {
                    this._LightOneZhongTongDeng = 0;
                }
                else if (value > 65535)
                {
                    this._LightOneZhongTongDeng = 65535;
                }
                else
                {
                    this._LightOneZhongTongDeng = value;

                }
                //ILiveDebug.Instance.WriteLine("_LightOneDaTongDeng:" + _LightOneDaTongDeng.ToString());

                this.din1Dim4_3.DinLoads[3].LevelIn.UShortValue = (ushort)this._LightOneZhongTongDeng;
            }
        }
       /* /// <summary>
        /// 中
        /// </summary>
        /// <param name="on"></param>
        public void LightOneZhongTongDeng(bool on)
        {
            if (on)
            {
                this.din1Dim4_3.DinLoads[3].LevelIn.UShortValue = 65535;
            }
            else
            {
                this.din1Dim4_3.DinLoads[3].LevelIn.UShortValue = 0;

            }
        }*/
        #endregion
        #region 三楼
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
                this.CongPu_4.Relay8SW8( 0, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 0, false);

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
                this.CongPu_4.Relay8SW8( 1, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 1, false);

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
                this.CongPu_4.Relay8SW8( 2, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 2, false);

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
                this.CongPu_4.Relay8SW8( 3, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 3, false);

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
                this.CongPu_4.Relay8SW8( 4, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 4, false);

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
                this.CongPu_4.Relay8SW8( 5, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 5, false);

            }
        }

        #endregion
        #region 四楼
     
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
                this.CongPu_4.Relay8SW8( 6, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 6, false);

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
                this.CongPu_4.Relay8SW8( 7, true);
            }
            else
            {
                this.CongPu_4.Relay8SW8( 7, false);

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
                this.CongPu_5.Relay8SW8( 0, true);
            }
            else
            {
                this.CongPu_5.Relay8SW8( 0, false);

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
                this.CongPu_5.Relay8SW8( 1, true);
            }
            else
            {
                this.CongPu_5.Relay8SW8( 1, false);

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
                this.CongPu_5.Relay8SW8( 2, true);
            }
            else
            {
                this.CongPu_5.Relay8SW8( 2, false);

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
                this.CongPu_5.Relay8SW8( 3, true);
            }
            else
            {
                this.CongPu_5.Relay8SW8( 3, false);

            }
        }
     
        #endregion
        #region 五楼
        internal void LightFiveAll(bool p)
        {
            if (p)
            {
                this.LightFiveBiDeng = 65535;
                this.LightFiveDiaoDeng = 65535;
                this.LightFiveJinMen = 65535;
                this.LightFiveKongTiao = 65535;

            }
            else
            {
                this.LightFiveBiDeng = 0;
                this.LightFiveDiaoDeng = 0;
                this.LightFiveJinMen = 0;
                this.LightFiveKongTiao = 0;
            }

            this.LightFiveDengDai(p);

            this.CongPu_5.Relay8SW8( 6, p);//5楼床头筒灯


        }
        //5楼床头
        public void LightFiveChuanTou(bool on)
        {
            if (on)
            {
                this.CongPu_5.Relay8SW8( 6, true);
            }
            else
            {
                this.CongPu_5.Relay8SW8( 6, false);
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
                this.CongPu_5.Relay8SW8( 5, true);
            }
            else
            {
                this.CongPu_5.Relay8SW8( 5, false);
            }
        }


        private int _LightFiveKongTiao = 0;
        public int LightFiveKongTiao
        {
            get
            {
                return this._LightFiveKongTiao;
            }
            set
            {
                if (value < 0)
                {
                    this._LightFiveKongTiao = 0;
                }
                else if (value > 65535)
                {
                    this._LightFiveKongTiao = 65535;
                }
                else
                {
                    this._LightFiveKongTiao = value;

                }
                // if (this.ZoneLightEvent != null)
                // {
                //     this.ZoneLightEvent(1, value);
                // }
                this.din1Dim4_4.DinLoads[1].LevelIn.UShortValue = (ushort)this._LightFiveKongTiao;
            }
        }

        private int _LightFiveDiaoDeng = 0;
        public int LightFiveDiaoDeng
        {
            get
            {
                return this._LightFiveDiaoDeng;
            }
            set
            {
                if (value < 0)
                {
                    this._LightFiveDiaoDeng = 0;
                }
                else if (value > 65535)
                {
                    this._LightFiveDiaoDeng = 65535;
                }
                else
                {
                    this._LightFiveDiaoDeng = value;

                }
                // if (this.ZoneLightEvent != null)
                // {
                //     this.ZoneLightEvent(1, value);
                // }
                this.din1Dim4_4.DinLoads[2].LevelIn.UShortValue = (ushort)this._LightFiveDiaoDeng;
            }
        }


        private int _LightFiveBiDeng = 0;
        public int LightFiveBiDeng
        {
            get
            {
                return this._LightFiveBiDeng;
            }
            set
            {
                if (value < 0)
                {
                    this._LightFiveBiDeng = 0;
                }
                else if (value > 65535)
                {
                    this._LightFiveBiDeng = 65535;
                }
                else
                {
                    this._LightFiveBiDeng = value;

                }
                // if (this.ZoneLightEvent != null)
                // {
                //     this.ZoneLightEvent(1, value);
                // }
                this.din1Dim4_4.DinLoads[3].LevelIn.UShortValue = (ushort)this._LightFiveBiDeng;
            }
        }

        private int _LightFiveJinMen = 0;
        public int LightFiveJinMen
        {
            get
            {
                return this._LightFiveJinMen;
            }
            set
            {
                if (value < 0)
                {
                    this._LightFiveJinMen = 0;
                }
                else if (value > 65535)
                {
                    this._LightFiveJinMen = 65535;
                }
                else
                {
                    this._LightFiveJinMen = value;

                }
                // if (this.ZoneLightEvent != null)
                // {
                //     this.ZoneLightEvent(1, value);
                // }
                this.din1Dim4_4.DinLoads[4].LevelIn.UShortValue = (ushort)this._LightFiveJinMen;
            }
        }
        #endregion
        #endregion

    }
}
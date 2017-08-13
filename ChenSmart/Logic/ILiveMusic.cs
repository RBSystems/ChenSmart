using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using ILiveLib;

namespace ChenSmart
{
    public class ILiveMusic
    {
        private CrestronControlSystem controlSystem = null;

        private ILiveDM8318 music = null;
        public ILiveMusic(CrestronControlSystem system)
        {
            this.controlSystem = system;

        }
        public void RegisterDevices()
        {
            #region 注册串口
            if (this.controlSystem.SupportsComPort)
            {
                ILiveComPort com = new ILiveComPort(this.controlSystem.ComPorts[2]);
                com.Register();
                this.music = new ILiveDM8318(com );
            }
            #endregion

        }

        /// <summary>
        /// 开关机
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="onoff"></param>
        public void MusicPower(int zone, bool onoff)
        {
            if (this.music!=null)
            {
                this.music.MusicPower(zone, onoff);
            }

        }
        /// <summary>
        /// 设置音源
        /// </summary>
        /// <param name="zone">区域</param>
        /// <param name="souce">FM:0x11 TUNER:0x21 TV:0x31 DVD:041 AUX:0x51 PC iPOD MP3/USB:0x81 SD:0x91 BLUETOOH:0xA1 DLAN:0xB1 Internet radio:0xC1 </param>
        public void MusicSource(int zone, byte souce)
        {
            if (this.music != null)
            {
                this.music.MusicSource(zone, souce);
            }

        }
        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="zone">区域</param>
        /// <param name="souce">音量（0-100）</param>
        public void VolSet(int zone, byte vol)
        {
            if (this.music != null)
            {
                this.music.VolSet(zone, vol);
            }
        }
        /// <summary>
        /// 音量加减
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="change"></param>
        public void VolSet(int zone, bool change)
        {
            if (this.music != null)
            {
                this.music.VolSet(zone, change);
            }
        }
        /// <summary>
        /// 播放模式设置
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="change">单曲播放：0x01 单曲循环：0x02 顺序播放:0x03 列表循环：0x04 随机播放：0x05</param>
        public void PlayModeSet(int zone, byte mode)
        {
            if (this.music != null)
            {
                this.music.PlayModeSet(zone, mode);
            }
        }
        /// <summary>
        /// 播放模式设置
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="change">播放：0x01 暂停：0x02 停止:0x04 </param>
        ///  <param name="source"> </param>
        public void PlaySet(int zone, byte mode, byte source)
        {

            if (this.music != null)
            {
                this.music.PlaySet(zone,mode, source);
            }
        }
        /// <summary>
        /// 上一曲 下一曲
        /// </summary>
        /// <param name="zone">0x1A</param>
        /// <param name="mode">0x01:上一曲 0x10:下一曲</param>
        /// <param name="source"> </param>
        public void MusicChangeSet(int zone, byte mode,byte source)
        {

            if (this.music != null)
            {
                this.music.MusicChangeSet(zone,mode,source );
            }
        }
    }
}
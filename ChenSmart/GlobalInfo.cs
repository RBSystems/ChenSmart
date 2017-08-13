using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using ILiveLib;

namespace ChenSmart
{
    public class GlobalInfo
    {
        public static readonly GlobalInfo Instance = new GlobalInfo();
        private GlobalInfo()
        {
        }
        private int _MusicVol1 = 40;
        private int _MusicVol2 = 40;
        private int _MusicVol3 = 40;
        private int _MusicVol4 = 40;
        private int _MusicVol5 = 40;
        private int _MusicVol6 = 40;
        private int _MusicVol7 = 40;
        private int _MusicVol8 = 40;

        public int MusicVol1
        {
            get
            {
                return this._MusicVol1;
            }
            set
            {
                if (value >= 100)
                {
                    this._MusicVol1 = 100;
                }
                else if (value <= 0)
                {
                    this._MusicVol1 = value;
                }
                else
                {
                    this._MusicVol1 = value;
                }
            }
        }
        public int MusicVol2
        {
            get
            {
                return this._MusicVol2;
            }
            set
            {
                if (value >= 100)
                {
                    this._MusicVol2 = 100;
                }
                else if (value <= 0)
                {
                    this._MusicVol2 = value;
                }
                else
                {
                    this._MusicVol2 = value;
                }
            }
        }
        public int MusicVol3
        {
            get
            {
                return this._MusicVol3;
            }
            set
            {
                if (value >= 100)
                {
                    this._MusicVol3 = 100;
                }
                else if (value <= 0)
                {
                    this._MusicVol3 = value;
                }
                else
                {
                    this._MusicVol3 = value;
                }
            }
        }
        public int MusicVol4
        {
            get
            {
                return this._MusicVol4;
            }
            set
            {
                if (value >= 100)
                {
                    this._MusicVol4 = 100;
                }
                else if (value <= 0)
                {
                    this._MusicVol4 = value;
                }
                else
                {
                    this._MusicVol4 = value;
                }
            }
        }
 
        public int MusicVol5
        {
            get
            {
                return this._MusicVol5;
            }
            set
            {
                if (value>=100)
                {
                    this._MusicVol5 = 100;
                }
                else if(value<=0)
                {
                    this._MusicVol5 = value;
                }
                else
                {
                    this._MusicVol5 = value;
                }
            }
        }
        public int MusicVol6
        {
            get
            {
                return this._MusicVol6;
            }
            set
            {
                if (value >= 100)
                {
                    this._MusicVol6 = 100;
                }
                else if (value <= 0)
                {
                    this._MusicVol6 = value;
                }
                else
                {
                    this._MusicVol6 = value;
                }
            }
        }
        public int MusicVol7
        {
            get
            {
                return this._MusicVol7;
            }
            set
            {
                if (value >= 100)
                {
                    this._MusicVol7 = 100;
                }
                else if (value <= 0)
                {
                    this._MusicVol7 = value;
                }
                else
                {
                    this._MusicVol7 = value;
                }
            }
        }
        public int MusicVol8
        {
            get
            {
                return this._MusicVol8;
            }
            set
            {
                if (value >= 100)
                {
                    this._MusicVol8 = 100;
                }
                else if (value <= 0)
                {
                    this._MusicVol8 = value;
                }
                else
                {
                    this._MusicVol8 = value;
                }
            }
        }

        /// <summary>
        /// 当前音乐区域
        /// </summary>
        public int CurrentMusicZone = 0;
        /// <summary>
        /// 当前空调区域
        /// </summary>
        public int CurrentClimateZone = -1;
        public IRACCFL CurrentClimateFL = IRACCFL.M;
        public IRACCMode CurrentClimateMode = IRACCMode.ZD;

        #region 空调临时温度
        #region 空调临时温度0
        private int _CurrentClimateTemp0 = 24;
        public int CurrentClimateTemp0
        {
            get
            {
                return this._CurrentClimateTemp0;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp0 = value;

                }
            }
        }
        #endregion
        #region 空调临时温度1
        private int _CurrentClimateTemp1 = 24;
        public int CurrentClimateTemp1
        {
            get
            {
                return this._CurrentClimateTemp1;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp1 = value;

                }
            }
        }
        #endregion
        private int _CurrentClimateTemp2 = 24;
        public int CurrentClimateTemp2
        {
            get
            {
                return this._CurrentClimateTemp2;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp2 = value;

                }
            }
        }

        private int _CurrentClimateTemp3 = 24;
        public int CurrentClimateTemp3
        {
            get
            {
                return this._CurrentClimateTemp3;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp3 = value;

                }
            }
        }

        private int _CurrentClimateTemp4 = 24;
        public int CurrentClimateTemp4
        {
            get
            {
                return this._CurrentClimateTemp4;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp4 = value;

                }
            }
        }

        private int _CurrentClimateTemp5 = 24;
        public int CurrentClimateTemp5
        {
            get
            {
                return this._CurrentClimateTemp5;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp5 = value;

                }
            }
        }

        private int _CurrentClimateTemp6 = 24;
        public int CurrentClimateTemp6
        {
            get
            {
                return this._CurrentClimateTemp6;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp6 = value;

                }
            }
        }

        private int _CurrentClimateTemp7 = 24;
        public int CurrentClimateTemp7
        {
            get
            {
                return this._CurrentClimateTemp7;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp7 = value;

                }
            }
        }

        private int _CurrentClimateTemp8 = 24;
        public int CurrentClimateTemp8
        {
            get
            {
                return this._CurrentClimateTemp8;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp8 = value;

                }
            }
        }

        private int _CurrentClimateTemp9 = 24;
        public int CurrentClimateTemp9
        {
            get
            {
                return this._CurrentClimateTemp9;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp9 = value;

                }
            }
        }

        #region 空调临时温度0
        private int _CurrentClimateTemp10 = 24;
        public int CurrentClimateTemp10
        {
            get
            {
                return this._CurrentClimateTemp10;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp10 = value;

                }
            }
        }
        #endregion
        #region 空调临时温度1
        private int _CurrentClimateTemp11 = 24;
        public int CurrentClimateTemp11
        {
            get
            {
                return this._CurrentClimateTemp11;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp11 = value;

                }
            }
        }
        #endregion
        private int _CurrentClimateTemp12 = 24;
        public int CurrentClimateTemp12
        {
            get
            {
                return this._CurrentClimateTemp12;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp12 = value;

                }
            }
        }

        private int _CurrentClimateTemp13 = 24;
        public int CurrentClimateTemp13
        {
            get
            {
                return this._CurrentClimateTemp13;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp13 = value;

                }
            }
        }

        private int _CurrentClimateTemp14 = 24;
        public int CurrentClimateTemp14
        {
            get
            {
                return this._CurrentClimateTemp14;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp14 = value;

                }
            }
        }

        private int _CurrentClimateTemp15 = 24;
        public int CurrentClimateTemp15
        {
            get
            {
                return this._CurrentClimateTemp15;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp15 = value;

                }
            }
        }

        private int _CurrentClimateTemp16 = 24;
        public int CurrentClimateTemp16
        {
            get
            {
                return this._CurrentClimateTemp16;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp16 = value;

                }
            }
        }

        private int _CurrentClimateTemp17 = 24;
        public int CurrentClimateTemp17
        {
            get
            {
                return this._CurrentClimateTemp17;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp17 = value;

                }
            }
        }

        private int _CurrentClimateTemp18 = 24;
        public int CurrentClimateTemp18
        {
            get
            {
                return this._CurrentClimateTemp18;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp18 = value;

                }
            }
        }

        private int _CurrentClimateTemp19 = 24;
        public int CurrentClimateTemp19
        {
            get
            {
                return this._CurrentClimateTemp19;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp19 = value;

                }
            }
        }

        #region 空调临时温度0
        private int _CurrentClimateTemp20 = 24;
        public int CurrentClimateTemp20
        {
            get
            {
                return this._CurrentClimateTemp20;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp20 = value;

                }
            }
        }
        #endregion
        #region 空调临时温度1
        private int _CurrentClimateTemp21 = 24;
        public int CurrentClimateTemp21
        {
            get
            {
                return this._CurrentClimateTemp21;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp21 = value;

                }
            }
        }
        #endregion
        private int _CurrentClimateTemp22 = 24;
        public int CurrentClimateTemp22
        {
            get
            {
                return this._CurrentClimateTemp22;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp22 = value;

                }
            }
        }

        private int _CurrentClimateTemp23 = 24;
        public int CurrentClimateTemp23
        {
            get
            {
                return this._CurrentClimateTemp23;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp23 = value;

                }
            }
        }

        private int _CurrentClimateTemp24 = 24;
        public int CurrentClimateTemp24
        {
            get
            {
                return this._CurrentClimateTemp24;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp24 = value;

                }
            }
        }

        private int _CurrentClimateTemp25 = 24;
        public int CurrentClimateTemp25
        {
            get
            {
                return this._CurrentClimateTemp25;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp25 = value;

                }
            }
        }

        private int _CurrentClimateTemp26 = 24;
        public int CurrentClimateTemp26
        {
            get
            {
                return this._CurrentClimateTemp26;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp26 = value;

                }
            }
        }

        private int _CurrentClimateTemp27 = 24;
        public int CurrentClimateTemp27
        {
            get
            {
                return this._CurrentClimateTemp27;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp27 = value;

                }
            }
        }

        private int _CurrentClimateTemp28 = 24;
        public int CurrentClimateTemp28
        {
            get
            {
                return this._CurrentClimateTemp28;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp28 = value;

                }
            }
        }

        private int _CurrentClimateTemp29 = 24;
        public int CurrentClimateTemp29
        {
            get
            {
                return this._CurrentClimateTemp29;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp29 = value;

                }
            }
        }

        #region 空调临时温度0
        private int _CurrentClimateTemp30 = 24;
        public int CurrentClimateTemp30
        {
            get
            {
                return this._CurrentClimateTemp30;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp30 = value;

                }
            }
        }
        #endregion
        #region 空调临时温度1
        private int _CurrentClimateTemp31 = 24;
        public int CurrentClimateTemp31
        {
            get
            {
                return this._CurrentClimateTemp31;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp31 = value;

                }
            }
        }
        #endregion
        private int _CurrentClimateTemp32 = 24;
        public int CurrentClimateTemp32
        {
            get
            {
                return this._CurrentClimateTemp32;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp32 = value;

                }
            }
        }

        private int _CurrentClimateTemp33 = 24;
        public int CurrentClimateTemp33
        {
            get
            {
                return this._CurrentClimateTemp33;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp33 = value;

                }
            }
        }

        private int _CurrentClimateTemp34 = 24;
        public int CurrentClimateTemp34
        {
            get
            {
                return this._CurrentClimateTemp34;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp34 = value;

                }
            }
        }

        private int _CurrentClimateTemp35 = 24;
        public int CurrentClimateTemp35
        {
            get
            {
                return this._CurrentClimateTemp35;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp35 = value;

                }
            }
        }

        private int _CurrentClimateTemp36 = 24;
        public int CurrentClimateTemp36
        {
            get
            {
                return this._CurrentClimateTemp36;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp36 = value;

                }
            }
        }

        private int _CurrentClimateTemp37 = 24;
        public int CurrentClimateTemp37
        {
            get
            {
                return this._CurrentClimateTemp37;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp37 = value;

                }
            }
        }

        private int _CurrentClimateTemp38 = 24;
        public int CurrentClimateTemp38
        {
            get
            {
                return this._CurrentClimateTemp38;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp38 = value;

                }
            }
        }

        private int _CurrentClimateTemp39 = 24;
        public int CurrentClimateTemp39
        {
            get
            {
                return this._CurrentClimateTemp39;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp39 = value;

                }
            }
        }

        #region 空调临时温度0
        private int _CurrentClimateTemp40 = 24;
        public int CurrentClimateTemp40
        {
            get
            {
                return this._CurrentClimateTemp40;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp40 = value;

                }
            }
        }
        #endregion
        #region 空调临时温度1
        private int _CurrentClimateTemp41 = 24;
        public int CurrentClimateTemp41
        {
            get
            {
                return this._CurrentClimateTemp41;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp41 = value;

                }
            }
        }
        #endregion
        private int _CurrentClimateTemp42 = 24;
        public int CurrentClimateTemp42
        {
            get
            {
                return this._CurrentClimateTemp42;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp42 = value;

                }
            }
        }

        private int _CurrentClimateTemp43 = 24;
        public int CurrentClimateTemp43
        {
            get
            {
                return this._CurrentClimateTemp43;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp43 = value;

                }
            }
        }

        private int _CurrentClimateTemp44 = 24;
        public int CurrentClimateTemp44
        {
            get
            {
                return this._CurrentClimateTemp44;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp44 = value;

                }
            }
        }

        private int _CurrentClimateTemp45 = 24;
        public int CurrentClimateTemp45
        {
            get
            {
                return this._CurrentClimateTemp45;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp45 = value;

                }
            }
        }

        private int _CurrentClimateTemp46 = 24;
        public int CurrentClimateTemp46
        {
            get
            {
                return this._CurrentClimateTemp46;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp46 = value;

                }
            }
        }

        private int _CurrentClimateTemp47 = 24;
        public int CurrentClimateTemp47
        {
            get
            {
                return this._CurrentClimateTemp47;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp47 = value;

                }
            }
        }

        private int _CurrentClimateTemp48 = 24;
        public int CurrentClimateTemp48
        {
            get
            {
                return this._CurrentClimateTemp48;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp48 = value;

                }
            }
        }

        private int _CurrentClimateTemp49 = 24;
        public int CurrentClimateTemp49
        {
            get
            {
                return this._CurrentClimateTemp49;
            }
            set
            {
                if (value < 31 && value > 17)
                {
                    this._CurrentClimateTemp49 = value;

                }
            }
        }
#endregion

    }
}
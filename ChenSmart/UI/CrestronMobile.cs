using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace GJHFSmart
{
    public enum CrestronMobileBool
    {
        Scence_Show=31,
        Scence_XiuXi = 32,
        Scence_Leave = 33,



        Light_AllOpen = 41,
        Light_AllClose = 42,
        Light_AllShow = 43,
        Light_XiuXi = 44,

        Light_Zone1 = 45,
        Light_Zone2 = 46,
        Light_Zone3 = 47,
        Light_Zone4 = 48,
        Light_Zone5 = 49,
        Light_Zone6 = 50,

        Media_On=61,
        Media_Off=62,
        ShaPan = 63,
        TV = 64,
        LED=65,

        
        GuangGao32 = 67,
        GuangGao22 = 66,
        GuangGao80 = 68,
        GuangGao42 = 69,

        ShaPanVolUp=81,
        ShaPanVolDown=82,

        TVVolUp=83,
        TVVolDown=84,

        Temp=73,
        Temp_On = 71,
        Temp_Off = 72,

        MusicPlay1=91,
        MusicPlay2 = 92,
        MusicPlay3 = 93,
        MusicPlay4 = 94,
        MusicPlay5 = 95,
        MusicPlay6 = 96,
        MusicPlay7 = 97,
        MusicPause = 98,
        MusicPlay=99,
        MusicVolUp = 100,
        MusicVolDown = 101,
        MusicPower=102,
        MusicPlay12=103
    }

    public enum CrestronMobileUShort
    {
        Foyer_Light_Level = 20,
        Living_DropLight_Level = 21,
        Living_LightBelt_Level = 22,
        Living_FrontLight_Level = 23,
        Living_RightLight_Level = 24,
        Living_BackLight_Level = 25
    }
    public enum CrestronMobileString
    {
        Foyer_Light_Percent_FB = 20,
        Living_DropLight_Percent_FB = 21,
        Living_LightBelt_Percent_FB = 22,
        Living_FrontLight_Percent_FB = 23,
        Living_RightLight_Percent_FB = 24,
        Living_BackLight_Percent_FB = 25
    }
}
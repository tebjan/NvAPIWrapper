using System;

namespace NvAPIWrapper.DRS.SettingValues
{
#pragma warning disable 1591
    public enum AnisotropicModeLevel : UInt32
    {
        Mask = 0xFFFF,

        NonePoint = 0x0,

        NoneLinear = 0x1,

        Maximum = 0x10,

        Default = 0x1
    }
#pragma warning restore 1591
}

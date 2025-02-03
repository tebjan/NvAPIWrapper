using System;

namespace NvAPIWrapper.DRS.SettingValues
{
#pragma warning disable 1591
    public enum WKSStereoSwapMode : UInt32
    {
        ApplicationControl = 0x0,

        PerEye = 0x1,

        PerEyePair = 0x2,

        LegacyBehavior = 0x3,

        PerEyeForSwapGroup = 0x4,

        Default = 0x0
    }
#pragma warning restore 1591
}

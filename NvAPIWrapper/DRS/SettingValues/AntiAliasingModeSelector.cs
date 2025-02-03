using System;

namespace NvAPIWrapper.DRS.SettingValues
{
#pragma warning disable 1591
    public enum AntiAliasingModeSelector : UInt32
    {
        Mask = 0x3,

        ApplicationControl = 0x0,

        Override = 0x1,

        Enhance = 0x2,

        Maximum = 0x2,

        Default = 0x0
    }
#pragma warning restore 1591
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NvAPIWrapper.Native.GSync.Enums;
// NVAPI_GSYNC_GPU_TOPOLOGY_CONNECTOR
public enum GSyncGpuTopologyConnector : uint
{
    None = 0,       // NVAPI_GSYNC_GPU_TOPOLOGY_CONNECTOR_NONE
    Primary = 1,    // NVAPI_GSYNC_GPU_TOPOLOGY_CONNECTOR_PRIMARY
    Secondary = 2,  // NVAPI_GSYNC_GPU_TOPOLOGY_CONNECTOR_SECONDARY
    Tertiary = 3,   // NVAPI_GSYNC_GPU_TOPOLOGY_CONNECTOR_TERTIARY
    Quarternary = 4 // NVAPI_GSYNC_GPU_TOPOLOGY_CONNECTOR_QUARTERNARY
}


// NVAPI_GSYNC_MULTIPLY_DIVIDE_MODE
public enum GSyncMultiplyDivideMode : uint
{
    UndefinedMode = 0, // NVAPI_GSYNC_UNDEFINED_MODE
    MultiplyMode = 1,  // NVAPI_GSYNC_MULTIPLY_MODE
    DivideMode = 2     // NVAPI_GSYNC_DIVIDE_MODE
}


[Flags]
public enum GSyncControlFlagsV1 : uint
{
    None = 0,
    InterlaceMode = 1 << 0,      // interlaceMode:1
    SyncSourceIsOutput = 1 << 1  // syncSourceIsOutput:1
                                 // reserved:30
}

[Flags]
public enum GSyncControlFlagsV2 : uint
{
    None = 0,
    InterlaceMode = 1 << 0,      // interlaceMode:1
    SyncSourceIsOutput = 1 << 1  // syncSourceIsOutput:1
                                 // reserved:30
                                 // Note: The C++ NV_GSYNC_CONTROL_PARAMS_V2 shows the same bitfields as V1.
                                 // The multiplyDivideMode and multiplyDivideValue are separate fields, not bitfields.
}

[Flags]
public enum GSyncStatusFlagsV2 : uint 
{
    None = 0,
    InternalSlave = 1 << 0 // bInternalSlave : 1
                           // reserved : 31
}

[Flags]
public enum GSyncControlBitFieldsV1 : uint
{
    None = 0,
    InterlaceMode = 1 << 0,
    SyncSourceIsOutput = 1 << 1,
    StereoLock = 1 << 2,
    FrameSlaveEnabled = 1 << 3,
    FrameMasterEnabled = 1 << 4,
    ExternalHouseSyncEnabled = 1 << 5,
    RJ45OutputImpedance50Ohm = 1 << 6,
    RJ45InputImpedance50Ohm = 1 << 7
}

[Flags]
public enum GSyncControlBitFieldsV2 : uint
{
    None = 0,
    InterlaceMode = 1 << 0,
    SyncSourceIsOutput = 1 << 1,
    StereoLock = 1 << 2,
    FrameSlaveEnabled = 1 << 3,
    FrameMasterEnabled = 1 << 4,
    ExternalHouseSyncEnabled = 1 << 5,
    RJ45OutputImpedance50Ohm = 1 << 6,
    RJ45InputImpedance50Ohm = 1 << 7,
    FrameNumberEmulationEnabled = 1 << 8,
    SyncOutSignalEnabled = 1 << 9,
    ForceSoftwareInterlacedMode = 1 << 10,
    EnableStereoExpansionMode = 1 << 11
}

public enum GSyncPolarity : uint
{
    RisingEdge = 0,    // Was NVAPI_GSYNC_POLARITY_RISING_EDGE
    FallingEdge = 1,   // Was NVAPI_GSYNC_POLARITY_FALLING_EDGE
    BothEdges = 2,     // Was NVAPI_GSYNC_POLARITY_BOTH_EDGES
}

public enum GSyncVideoMode : uint
{
    None = 0,          // Was NVAPI_GSYNC_VIDEO_MODE_NONE
    TTL = 1,           // Was NVAPI_GSYNC_VIDEO_MODE_TTL
    NtscPalSecam = 2,  // Was NVAPI_GSYNC_VIDEO_MODE_NTSCPALSECAM
    HDTV = 3,          // Was NVAPI_GSYNC_VIDEO_MODE_HDTV
    Composite = 4,     // Was NVAPI_GSYNC_VIDEO_MODE_COMPOSITE
}

public enum GSyncSyncSource : uint
{
    VSync = 0,         // Was NVAPI_GSYNC_SYNC_SOURCE_VSYNC
    HouseSync = 1,     // Was NVAPI_GSYNC_SYNC_SOURCE_HOUSESYNC
}

public enum GSyncDelayType : uint
{
    Unknown = 0,       // Was NVAPI_GSYNC_DELAY_TYPE_UNKNOWN
    SyncSkew = 1,      // Was NVAPI_GSYNC_DELAY_TYPE_SYNC_SKEW
    StartupDelay = 2,  // Was NVAPI_GSYNC_DELAY_TYPE_STARTUP_DELAY
}

public enum GSyncRJ45IO : uint
{
    Output = 0,        // Was NVAPI_GSYNC_RJ45_OUTPUT
    Input = 1,         // Was NVAPI_GSYNC_RJ45_INPUT
    Unused = 2,        // Was NVAPI_GSYNC_RJ45_UNUSED
}

public enum GSyncDisplaySyncState : uint
{
    Unsynced = 0,      // Was NV_GSYNC_DISPLAY_SYNC_STATE_UNSYNCED
    Slave = 1,         // Was NV_GSYNC_DISPLAY_SYNC_STATE_SLAVE
    Master = 2,        // Was NV_GSYNC_DISPLAY_SYNC_STATE_MASTER
}

/// <summary>
/// Defines the physical connector type for a GPU.
/// Used in NV_GPU_CONNECTOR_DATA and other structures.
/// </summary>
public enum GpuConnectorType : uint // NV_GPU_CONNECTOR_TYPE
{
    /// <summary>
    /// VGA 15-pin connector.
    /// </summary>
    VGA15Pin = 0x00000000, // NVAPI_GPU_CONNECTOR_VGA_15_PIN

    /// <summary>
    /// TV Composite connector.
    /// </summary>
    TVComposite = 0x00000010, // NVAPI_GPU_CONNECTOR_TV_COMPOSITE

    /// <summary>
    /// TV S-Video connector.
    /// </summary>
    TVSVideo = 0x00000011, // NVAPI_GPU_CONNECTOR_TV_SVIDEO

    /// <summary>
    /// TV HDTV Component connector.
    /// </summary>
    TVHDTVComponent = 0x00000013, // NVAPI_GPU_CONNECTOR_TV_HDTV_COMPONENT

    /// <summary>
    /// TV SCART connector.
    /// </summary>
    TVSCART = 0x00000014, // NVAPI_GPU_CONNECTOR_TV_SCART

    /// <summary>
    /// TV Composite SCART on EIAJ4120 connector.
    /// </summary>
    TVCompositeSCARTOnEIAJ4120 = 0x00000016, // NVAPI_GPU_CONNECTOR_TV_COMPOSITE_SCART_ON_EIAJ4120

    /// <summary>
    /// TV HDTV EIAJ4120 connector.
    /// </summary>
    TVHDTVEIAJ4120 = 0x00000017, // NVAPI_GPU_CONNECTOR_TV_HDTV_EIAJ4120

    /// <summary>
    /// PC Pod HDTV YPrPb connector.
    /// </summary>
    PCPodHDTVYPrPb = 0x00000018, // NVAPI_GPU_CONNECTOR_PC_POD_HDTV_YPRPB

    /// <summary>
    /// PC Pod S-Video connector.
    /// </summary>
    PCPodSVideo = 0x00000019, // NVAPI_GPU_CONNECTOR_PC_POD_SVIDEO

    /// <summary>
    /// PC Pod Composite connector.
    /// </summary>
    PCPodComposite = 0x0000001A, // NVAPI_GPU_CONNECTOR_PC_POD_COMPOSITE

    /// <summary>
    /// DVI-I TV S-Video connector.
    /// </summary>
    DVIITVSVideo = 0x00000020, // NVAPI_GPU_CONNECTOR_DVI_I_TV_SVIDEO

    /// <summary>
    /// DVI-I TV Composite connector.
    /// </summary>
    DVIITVComposite = 0x00000021, // NVAPI_GPU_CONNECTOR_DVI_I_TV_COMPOSITE

    /// <summary>
    /// DVI-I connector.
    /// </summary>
    DVII = 0x00000030, // NVAPI_GPU_CONNECTOR_DVI_I

    /// <summary>
    /// DVI-D connector.
    /// </summary>
    DVID = 0x00000031, // NVAPI_GPU_CONNECTOR_DVI_D

    /// <summary>
    /// ADC connector.
    /// </summary>
    ADC = 0x00000032, // NVAPI_GPU_CONNECTOR_ADC

    /// <summary>
    /// LFH DVI-I (first) connector.
    /// </summary>
    LFHDVII1 = 0x00000038, // NVAPI_GPU_CONNECTOR_LFH_DVI_I_1

    /// <summary>
    /// LFH DVI-I (second) connector.
    /// </summary>
    LFHDVII2 = 0x00000039, // NVAPI_GPU_CONNECTOR_LFH_DVI_I_2

    /// <summary>
    /// SPWG connector.
    /// </summary>
    SPWG = 0x00000040, // NVAPI_GPU_CONNECTOR_SPWG

    /// <summary>
    /// OEM specific connector.
    /// </summary>
    OEM = 0x00000041, // NVAPI_GPU_CONNECTOR_OEM

    /// <summary>
    /// External DisplayPort connector.
    /// </summary>
    DisplayPortExternal = 0x00000046, // NVAPI_GPU_CONNECTOR_DISPLAYPORT_EXTERNAL

    /// <summary>
    /// Internal DisplayPort connector.
    /// </summary>
    DisplayPortInternal = 0x00000047, // NVAPI_GPU_CONNECTOR_DISPLAYPORT_INTERNAL

    /// <summary>
    /// Mini DisplayPort (external) connector.
    /// </summary>
    DisplayPortMiniExternal = 0x00000048, // NVAPI_GPU_CONNECTOR_DISPLAYPORT_MINI_EXT

    /// <summary>
    /// HDMI Type A connector.
    /// </summary>
    HDMIA = 0x00000061, // NVAPI_GPU_CONNECTOR_HDMI_A

    /// <summary>
    /// HDMI Type C (Mini HDMI) connector.
    /// </summary>
    HDMICMini = 0x00000063, // NVAPI_GPU_CONNECTOR_HDMI_C_MINI

    /// <summary>
    /// LFH DisplayPort (first) connector.
    /// </summary>
    LFHDisplayPort1 = 0x00000064, // NVAPI_GPU_CONNECTOR_LFH_DISPLAYPORT_1

    /// <summary>
    /// LFH DisplayPort (second) connector.
    /// </summary>
    LFHDisplayPort2 = 0x00000065, // NVAPI_GPU_CONNECTOR_LFH_DISPLAYPORT_2

    /// <summary>
    /// Virtual Wireless Fidelity (Wi-Fi Display) connector.
    /// </summary>
    VirtualWFD = 0x00000070, // NVAPI_GPU_CONNECTOR_VIRTUAL_WFD

    /// <summary>
    /// Unknown connector type.
    /// </summary>
    Unknown = 0xFFFFFFFF // NVAPI_GPU_CONNECTOR_UNKNOWN
}
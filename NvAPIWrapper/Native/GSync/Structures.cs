using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GSync.Enums;
using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;

namespace NvAPIWrapper.Native.GSync.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncDelay
    {
        internal StructureVersion _Version;
        public uint NumLines;
        public uint NumPixels;
        public uint MaxLines;
        public uint MinLines;
        public uint MaxPixels;
        public uint MinPixels;

        public GSyncDelay()
        {
            this = typeof(GSyncDelay).Instantiate<GSyncDelay>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncCapabilitiesV1
    {
        internal StructureVersion _Version;
        public uint BoardId;
        public uint Revision;
        public uint CapFlags;

        public GSyncCapabilitiesV1()
        {
            this = typeof(GSyncCapabilitiesV1).Instantiate<GSyncCapabilitiesV1>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncCapabilitiesV2
    {
        internal StructureVersion _Version;
        public uint BoardId;
        public uint Revision;
        public uint CapFlags;
        public uint MaxNumGpus;
        public uint GSyncGPUPhysIdMask;

        public GSyncCapabilitiesV2()
        {
            this = typeof(GSyncCapabilitiesV2).Instantiate<GSyncCapabilitiesV2>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(3)]
    public struct GSyncCapabilitiesV3
    {
        internal StructureVersion _Version;
        public uint BoardId;
        public uint Revision;
        public uint CapFlags;
        public uint MaxNumGpus;
        public uint GSyncGPUPhysIdMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_GSYNC_OUTPUTS)]
        public GpuConnectorType[] GSyncConnectorType;

        public GSyncCapabilitiesV3()
        {
            this = typeof(GSyncCapabilitiesV3).Instantiate<GSyncCapabilitiesV3>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)] // Changed from Pack = 1 to Pack = 8
    [StructureVersion(1)]
    public struct GSyncControlParametersV1
    {
        internal StructureVersion _Version;
        public GSyncPolarity Polarity;
        public GSyncVideoMode VideoMode;
        public uint Interval;
        public GSyncSyncSource Source;
        public uint PackedBitFields1; // C compiler handles bitfield layout within this uint
        public GSyncDelay SyncSkew;
        public GSyncDelay StartupDelay;
        public uint WatchDogTimer;

        public GSyncControlParametersV1()
        {
            this = typeof(GSyncControlParametersV1).Instantiate<GSyncControlParametersV1>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)] // Changed from Pack = 1 to Pack = 8
    [StructureVersion(2)]
    public struct GSyncControlParametersV2
    {
        internal StructureVersion _Version;
        public GSyncPolarity Polarity;
        public GSyncVideoMode VideoMode;
        public uint Interval;
        public GSyncSyncSource Source;
        public uint PackedBitFields1; // C compiler handles bitfield layout within this uint
        public GSyncDelay SyncSkew;
        public GSyncDelay StartupDelay;
        public uint WatchDogTimer;
        public uint SyncOutPulseWidth;
        public uint SyncOutPolarity;

        public GSyncControlParametersV2()
        {
            this = typeof(GSyncControlParametersV2).Instantiate<GSyncControlParametersV2>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncStatusParametersV1
    {
        internal StructureVersion _Version;
        public uint RefreshRate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC, ArraySubType = UnmanagedType.U4)]
        public GSyncRJ45IO[] RJ45PortStates;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC, ArraySubType = UnmanagedType.U4)]
        public uint[] RJ45EthernetConnections;
        public uint HouseSyncIncomingFrequencyHz;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsHouseSyncSignalPresent;

        public GSyncStatusParametersV1()
        {
            this = typeof(GSyncStatusParametersV1).Instantiate<GSyncStatusParametersV1>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncStatusParametersV2
    {
        internal StructureVersion _Version;
        public uint RefreshRate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC, ArraySubType = UnmanagedType.U4)]
        public GSyncRJ45IO[] RJ45PortStates;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC, ArraySubType = UnmanagedType.U4)]
        public uint[] RJ45EthernetConnections;
        public uint HouseSyncIncomingFrequencyHz;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsHouseSyncSignalPresent;
        public uint SyncOutPulseWidth;
        public uint SyncOutPolarity;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSyncOutSignalEnabled;

        public GSyncStatusParametersV2()
        {
            this = typeof(GSyncStatusParametersV2).Instantiate<GSyncStatusParametersV2>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncBoardStatus
    {
        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsTimingInSync;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsStereoSyncedToMaster;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSyncSignalAvailable;

        public GSyncBoardStatus()
        {
            this = typeof(GSyncBoardStatus).Instantiate<GSyncBoardStatus>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncGpuV1
    {
        internal StructureVersion _Version;
        public IntPtr PhysicalGpuHandle;
        public uint DisplayId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSynced;

        public GSyncGpuV1()
        {
            this = typeof(GSyncGpuV1).Instantiate<GSyncGpuV1>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncGpuV2
    {
        internal StructureVersion _Version;
        public IntPtr PhysicalGpuHandle;
        public uint DisplayId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSynced;
        public GpuConnectorType ConnectorType;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsMultiGPUBoard;

        public GSyncGpuV2()
        {
            this = typeof(GSyncGpuV2).Instantiate<GSyncGpuV2>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncDisplayV1
    {
        internal StructureVersion _Version;
        public IntPtr DisplayHandle;
        public GSyncDisplaySyncState SyncState;

        public GSyncDisplayV1()
        {
            this = typeof(GSyncDisplayV1).Instantiate<GSyncDisplayV1>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncDisplayV2
    {
        internal StructureVersion _Version;
        public IntPtr DisplayHandle;
        public GSyncDisplaySyncState SyncState;
        public GpuConnectorType ConnectorType;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsMultiGPUBoard;

        public GSyncDisplayV2()
        {
            this = typeof(GSyncDisplayV2).Instantiate<GSyncDisplayV2>();
        }
    }
}
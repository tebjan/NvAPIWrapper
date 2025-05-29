using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GSync.Enums;
using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GSync.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncDelay : IInitializable
    {
        internal StructureVersion _Version;
        public uint NumLines;
        public uint NumPixels;
        public uint MaxLines;
        public uint MinLines;
        public uint MaxPixels;
        public uint MinPixels;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncCapabilitiesV1 : IInitializable
    {
        internal StructureVersion _Version; // Initialized by Instantiate<T>
        public uint BoardId;
        public uint Revision;       // In V1, this is likely the full revision
        public uint CapFlags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncCapabilitiesV2 : IInitializable
    {
        internal StructureVersion _Version; // Initialized by Instantiate<T>
        public uint BoardId;
        public uint Revision;       // FPGA major revision
        public uint CapFlags;
        public uint ExtendedRevision; // FPGA minor revision
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(3)]
    public struct GSyncCapabilitiesV3 : IInitializable
    {
        internal StructureVersion _Version; // Initialized by Instantiate<T>
        public uint BoardId;
        public uint Revision;       // FPGA major revision
        public uint CapFlags;
        public uint ExtendedRevision; // FPGA minor revision

        // NvU32 bIsMulDivSupported : 1;
        // NvU32 reserved           : 31;
        private uint _bitFields;

        public uint MaxMulDivValue;

        public bool IsMulDivSupported
        {
            get => (_bitFields & 0x1) != 0;
            // No public setter for reserved bits or if this is read-only from API
        }
        // Add a property for Reserved if needed, though usually not.
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncControlParametersV1 : IInitializable
    {
        internal StructureVersion _Version;
        public GSyncPolarity Polarity;
        public GSyncVideoMode VideoMode;
        public uint Interval;
        public GSyncSyncSource Source;
        public GSyncControlBitFieldsV1 ControlFlags;
        public GSyncDelay SyncSkew;
        public GSyncDelay StartupDelay;
        public uint WatchDogTimer;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncControlParametersV2 : IInitializable
    {
        internal StructureVersion _Version;
        public GSyncPolarity Polarity;
        public GSyncVideoMode VideoMode;
        public uint Interval;
        public GSyncSyncSource Source;
        public GSyncControlBitFieldsV2 ControlFlags;
        public GSyncDelay SyncSkew;
        public GSyncDelay StartupDelay;
        public uint WatchDogTimer;
        public uint SyncOutPulseWidth;
        public GSyncPolarity SyncOutPolarity;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncStatusParametersV1 : IInitializable
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
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncStatusParametersV2 : IInitializable
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
        public GSyncPolarity SyncOutPolarity;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSyncOutSignalEnabled;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncBoardStatus : IInitializable
    {
        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsTimingInSync;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsStereoSyncedToMaster;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSyncSignalAvailable;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncGpuV1 : IInitializable
    {
        internal StructureVersion _Version;
        public IntPtr PhysicalGpuHandle;
        public uint DisplayId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSynced;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncGpuV2 : IInitializable
    {
        internal StructureVersion _Version;
        public IntPtr PhysicalGpuHandle;
        public uint DisplayId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsSynced;
        public GpuConnectorType ConnectorType;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsMultiGPUBoard;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct GSyncDisplayV1 : IInitializable
    {
        internal StructureVersion _Version;
        public IntPtr DisplayHandle;
        public GSyncDisplaySyncState SyncState;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GSyncDisplayV2 : IInitializable
    {
        internal StructureVersion _Version;
        public IntPtr DisplayHandle;
        public GSyncDisplaySyncState SyncState;
        public GpuConnectorType ConnectorType;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsMultiGPUBoard;
    }
}
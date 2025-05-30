using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GSync.Enums;
using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GSync.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)] // NV_GSYNC_DELAY_VER
    public struct GSyncDelay : IInitializable
    {
        internal StructureVersion _Version;
        public uint NumLines;
        public uint NumPixels;
        public uint MaxLines;    // Read-only, updated by GetControlParameters
        public uint MinPixels;   // Read-only, updated by GetControlParameters
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

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)] // NV_GSYNC_GPU_VER
    public struct GSyncGpu : IInitializable // Renamed from GSyncGpuV1, as this is THE GSyncGpu for GetTopology
    {
        internal StructureVersion _Version;
        public IntPtr PhysicalGpuHandle;       // NvPhysicalGpuHandle hPhysicalGpu;
        public GSyncGpuTopologyConnector Connector; // NVAPI_GSYNC_GPU_TOPOLOGY_CONNECTOR connector;
        public IntPtr ProxyPhysicalGpuHandle;  // NvPhysicalGpuHandle hProxyPhysicalGpu;
        private uint _bitFields;                // For isSynced : 1; reserved : 31;

        public bool IsSynced
        {
            get => (_bitFields & 0x1) != 0;
            set => _bitFields = (_bitFields & ~0x1u) | (value ? 1u : 0u);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)] // NV_GSYNC_DISPLAY_VER
    public struct GSyncDisplay : IInitializable // Renamed from GSyncDisplayV1
    {
        internal StructureVersion _Version;
        public uint DisplayId;                  // NvU32 displayId;
        private uint _bitFields;                // For isMasterable : 1; reserved : 31;
        public GSyncDisplaySyncState SyncState; // NVAPI_GSYNC_DISPLAY_SYNC_STATE syncState;

        public bool IsMasterable // Read-only property as per C++ comment
        {
            get => (_bitFields & 0x1) != 0;
            // No public setter for read-only or reserved bits
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)] // NV_GSYNC_CONTROL_PARAMS_VER1
    public struct GSyncControlParametersV1 : IInitializable
    {
        internal StructureVersion _Version;
        public GSyncPolarity Polarity;
        public GSyncVideoMode VideoMode; // vmode
        public uint Interval;
        public GSyncSyncSource Source;
        public GSyncControlFlagsV1 ControlFlags; // Replaces PackedBitFields1
        public GSyncDelay SyncSkew;
        public GSyncDelay StartupDelay;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)] // NV_GSYNC_CONTROL_PARAMS_VER2 (typedef'd to NV_GSYNC_CONTROL_PARAMS)
    public struct GSyncControlParametersV2 : IInitializable
    {
        internal StructureVersion _Version;
        public GSyncPolarity Polarity;
        public GSyncVideoMode VideoMode; // vmode
        public uint Interval;
        public GSyncSyncSource Source;
        public GSyncControlFlagsV2 ControlFlags; // Replaces PackedBitFields1, uses V2 flags enum
        public GSyncDelay SyncSkew;
        public GSyncDelay StartupDelay;
        public GSyncMultiplyDivideMode MultiplyDivideMode; // New in C++ V2
        public byte MultiplyDivideValue;                 // New in C++ V2 (NvU8)
        // Ensure no other fields are accidentally carried over from previous incorrect definitions
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)] // NV_GSYNC_STATUS_VER
    public struct GSyncBoardStatus : IInitializable // Renamed from GSyncStatus
    {
        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.Bool)] // NvU32 bIsSynced
        public bool IsTimingInSync;
        [MarshalAs(UnmanagedType.Bool)] // NvU32 bIsStereoSynced
        public bool IsStereoSyncedToMaster;
        [MarshalAs(UnmanagedType.Bool)] // NvU32 bIsSyncSignalAvailable
        public bool IsSyncSignalAvailable;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)] // NV_GSYNC_STATUS_PARAMS_VER1
    public struct GSyncStatusParametersV1 : IInitializable
    {
        internal StructureVersion _Version;
        public uint RefreshRate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC)]
        public GSyncRJ45IO[] RJ45_IO; // Field name matches C++
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC)]
        public uint[] RJ45_Ethernet; // Field name matches C++
        public uint HouseSyncIncoming; // Renamed to match C++ (was HouseSyncIncomingFrequencyHz)
        [MarshalAs(UnmanagedType.Bool)] // NvU32 bHouseSync
        public bool IsHouseSyncConnected; // Renamed to match C++ (was IsHouseSyncSignalPresent)
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)] // NV_GSYNC_STATUS_PARAMS_VER2 (typedef'd to NV_GSYNC_STATUS_PARAMS)
    public struct GSyncStatusParametersV2 : IInitializable
    {
        internal StructureVersion _Version;
        public uint RefreshRate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC)]
        public GSyncRJ45IO[] RJ45_IO;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GSyncConstants.NVAPI_MAX_RJ45_PER_GSYNC)]
        public uint[] RJ45_Ethernet;
        public uint HouseSyncIncoming;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IsHouseSyncConnected; // bHouseSync
        private uint _bitFields;          // For bInternalSlave : 1; reserved : 31;

        public bool IsInternalSlave
        {
            get => (_bitFields & 0x1) != 0;
            set => _bitFields = (_bitFields & ~0x1u) | (value ? 1u : 0u);
        }
    }
}
}
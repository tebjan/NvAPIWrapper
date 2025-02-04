using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.D3D.Structures
{
    /// <summary>
    /// Represents the statistics for a Present Barrier frame.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PresentBarrierFrameStatistics
    {
        /// <summary>
        /// Version of the statistics structure, should be set to NV_PRESENT_BARRIER_FRAME_STATICS_VER1.
        /// </summary>
        public uint Version;

        /// <summary>
        /// The presentBarrier sync mode of this client from the last present call.
        /// </summary>
        public PresentBarrierSyncMode SyncMode;

        /// <summary>
        /// The total count of times that a frame has been presented since the client joined the presentBarrier.
        /// </summary>
        public uint PresentCount;

        /// <summary>
        /// The count of times that a frame has been presented from this client while in sync with the system or cluster.
        /// </summary>
        public uint PresentInSyncCount;

        /// <summary>
        /// The count of flips from this client while in sync with the system or cluster.
        /// </summary>
        public uint FlipInSyncCount;

        /// <summary>
        /// The count of v-blanks since the returned sync mode is system or cluster sync.
        /// </summary>
        public uint RefreshCount;
    }

    /// <summary>
    /// Enum for the sync modes in Present Barrier synchronization.
    /// </summary>
    public enum PresentBarrierSyncMode
    {
        NotJoined = 0x00000000,  // The client hasn't joined presentBarrier
        SyncClient = 0x00000001, // The client is in sync with no other clients
        SyncSystem = 0x00000002, // The client is synced with other clients within the system
        SyncCluster = 0x00000003  // The client is synced across systems through QSync devices
    }


}
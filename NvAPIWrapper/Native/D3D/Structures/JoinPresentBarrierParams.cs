using NvAPIWrapper.Native.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NvAPIWrapper.Native.D3D.Structures
{
    /// <summary>
    /// Parameters for joining a Present Barrier.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [StructureVersion(1)]
    public struct JoinPresentBarrierParams
    {
        /// <summary>
        /// Must be set to <see cref="Version"/> which corresponds to NV_JOIN_PRESENT_BARRIER_PARAMS_VER1.
        /// </summary>
        public uint Version;

        /// <summary>
        /// Gets the default version for this structure.
        /// </summary>
        public static uint DefaultVersion => MakeNvApiVersion(typeof(JoinPresentBarrierParams), 1);

        /// <summary>
        /// Creates an instance with the default version set.
        /// </summary>
        public static JoinPresentBarrierParams CreateDefault()
        {
            return new JoinPresentBarrierParams
            {
                Version = DefaultVersion
            };
        }

        /// <summary>
        /// Helper function to construct the version field.
        /// Equivalent to the C macro: MAKE_NVAPI_VERSION(NV_JOIN_PRESENT_BARRIER_PARAMS, 1)
        /// </summary>
        private static uint MakeNvApiVersion(Type structType, uint version)
        {
            // Typically, NVAPI uses a macro where the version is determined by struct size.
            // If needed, implement logic here to match the C macro behavior.
            return ((uint)Marshal.SizeOf(structType) | (version << 16));
        }
    }
}

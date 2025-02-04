using System;
using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.D3D.Structures
{
    /// <summary>
    /// Holds a handle representing a Present Barrier Client
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PresentBarrierClientHandle : IEquatable<PresentBarrierClientHandle>
    {
        internal readonly IntPtr _MemoryAddress;

        /// <inheritdoc />
        public bool Equals(PresentBarrierClientHandle other)
        {
            return _MemoryAddress.Equals(other._MemoryAddress);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is PresentBarrierClientHandle handle && Equals(handle);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _MemoryAddress.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"PresentBarrierClientHandle #{_MemoryAddress.ToInt64()}";
        }

        /// <inheritdoc />
        public IntPtr MemoryAddress
        {
            get => _MemoryAddress;
        }

        /// <inheritdoc />
        public bool IsNull
        {
            get => _MemoryAddress == IntPtr.Zero;
        }

        /// <summary>
        /// Checks for equality between two PresentBarrierClientHandles.
        /// </summary>
        public static bool operator ==(PresentBarrierClientHandle left, PresentBarrierClientHandle right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks for inequality between two PresentBarrierClientHandles.
        /// </summary>
        public static bool operator !=(PresentBarrierClientHandle left, PresentBarrierClientHandle right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Gets the default PresentBarrierClientHandle with a null pointer.
        /// </summary>
        public static PresentBarrierClientHandle DefaultHandle
        {
            get => default(PresentBarrierClientHandle);
        }
    }
}

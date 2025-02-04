namespace NvAPIWrapper.Native.D3D
{
    /// <summary>
    ///    Holds a list of valid values for Vsync mode
    /// </summary>
    public enum VsyncMode
    {
        /// <summary>
        ///    Fall back to the default settings
        /// </summary>
        Default = 0,

        /// <summary>
        ///   Force vertical sync off when performance is more important than image quality and for benchmarking
        /// </summary>
        Off = 1,

        /// <summary>
        ///  Force vertical sync on when image quality is more important than performance
        /// </summary>
        On = 2,

        /// <summary>
        /// Select adaptive to turn vertical sync on or off based on the frame rate.
        /// </summary>
        Adaptive = 3,

        /// <summary>
        /// Vertical sync will only be on for frame rates above the monitor refresh rate.
        /// </summary>
        AdaptiveHalfRefreshRate = 4
    }
}
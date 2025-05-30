using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.GSync; // For GSyncConstants
using NvAPIWrapper.Native.GSync.Enums;
using NvAPIWrapper.Native.GSync.Structures;
using NvAPIWrapper.Native.Helpers;


// ReSharper disable InconsistentNaming
namespace NvAPIWrapper.Native.Delegates;

internal static class GSync
{
    [FunctionId(FunctionId.NvAPI_GSync_AdjustSyncDelay)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_AdjustSyncDelay(
        [In] IntPtr hNvGSyncDevice,
        [In] GSyncDelayType delayType,
        [In, Out] ref GSyncDelay gsyncDelay,
        [Out] out uint syncSteps
    );

    [FunctionId(FunctionId.NvAPI_GSync_EnumSyncDevices)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_EnumSyncDevices(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = GSyncConstants.NVAPI_MAX_GSYNC_DEVICES)] IntPtr[] nvGSyncHandles,
        [Out] out uint gsyncCount
    );

    [FunctionId(FunctionId.NvAPI_GSync_GetControlParameters)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_GetControlParameters(
        [In] IntPtr hNvGSyncDevice,
        [In, Out] ref GSyncControlParametersV2 gsyncControls
    );

    [FunctionId(FunctionId.NvAPI_GSync_GetStatusParameters)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_GetStatusParameters(
        [In] IntPtr hNvGSyncDevice,
        [In, Out] ref GSyncStatusParametersV2 statusParams
    );

    [FunctionId(FunctionId.NvAPI_GSync_GetSyncStatus)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_GetSyncStatus(
        [In] IntPtr hNvGSyncDevice,
        [In] IntPtr hPhysicalGpu, // NvPhysicalGpuHandle hPhysicalGpu
        [In, Out] ref GSyncBoardStatus syncStatus
    );

    [FunctionId(FunctionId.NvAPI_GSync_GetTopology)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_GetTopology(
        [In] IntPtr hNvGSyncDevice,
        [In, Out] ref uint gsyncGpuCount,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] GSyncGpu[] gsyncGPUs,
        [In, Out] ref uint gsyncDisplayCount,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] GSyncDisplay[] gsyncDisplays
    );

    [FunctionId(FunctionId.NvAPI_GSync_QueryCapabilities)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_QueryCapabilities(
        [In] IntPtr hNvGSyncDevice,
        [In, Out] ref GSyncCapabilitiesV3 nvGSyncCapabilities
    );

    [FunctionId(FunctionId.NvAPI_GSync_SetControlParameters)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_SetControlParameters(
        [In] IntPtr hNvGSyncDevice,
        [In, Out] ref GSyncControlParametersV2 gsyncControls
    );

    [FunctionId(FunctionId.NvAPI_GSync_SetSyncStateSettings)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Status NvAPI_GSync_SetSyncStateSettings(
        [In] uint gsyncDisplayCount,
        [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] GSyncDisplay[] pGsyncDisplays,
        [In] uint flags
    );
}
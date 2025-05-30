using System;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.Delegates; // For Delegates.GSync
using NvAPIWrapper.Native.GSync.Enums;
using NvAPIWrapper.Native.GSync.Structures;
using NvAPIWrapper.Native.GSync; // For GSyncConstants
using NvAPIWrapper.Native.Helpers; // For .Instantiate<T>() extension method

namespace NvAPIWrapper.Native;

/// <summary>
///     Contains GSync static functions
/// </summary>
// ReSharper disable once ClassTooBig
public static class GSyncApi
{
    private static readonly Delegates.GSync.NvAPI_GSync_AdjustSyncDelay _gsyncAdjustSyncDelayDelegate =
       DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_AdjustSyncDelay>();

    public static void GSyncAdjustSyncDelay(
        IntPtr hNvGSyncDevice,
        GSyncDelayType delayType,
        ref GSyncDelay gsyncDelay, // This is correctly 'ref' as it's truly in/out for adjustment
        out uint syncSteps
    )
    {
        // Caller is responsible for gsyncDelay's initial _Version if creating it new for this call.
        // If gsyncDelay is an existing struct being modified, its _Version should be intact.
        // Your Instantiate<T> on caller side handles new ones.
        var status = _gsyncAdjustSyncDelayDelegate(
            hNvGSyncDevice,
            delayType,
            ref gsyncDelay,
            out syncSteps
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }

    public static void GSyncAdjustSyncDelay(
        IntPtr hNvGSyncDevice,
        GSyncDelayType delayType,
        ref GSyncDelay gsyncDelay // This is correctly 'ref'
    )
    {
        uint dummySyncSteps;
        GSyncAdjustSyncDelay(hNvGSyncDevice, delayType, ref gsyncDelay, out dummySyncSteps);
    }

    private static readonly Delegates.GSync.NvAPI_GSync_EnumSyncDevices _gsyncEnumSyncDevicesDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_EnumSyncDevices>();

    public static void GSyncEnumSyncDevices(
        out IntPtr[] nvGSyncHandles,
        out uint gsyncCount
    )
    {
        var handles = new IntPtr[GSyncConstants.NVAPI_MAX_GSYNC_DEVICES];
        var status = _gsyncEnumSyncDevicesDelegate(
            handles,
            out gsyncCount
        );

        if (status != Status.Ok)
        {
            nvGSyncHandles = Array.Empty<IntPtr>();
            gsyncCount = 0;
            throw new NVIDIAApiException(status);
        }

        if (gsyncCount < GSyncConstants.NVAPI_MAX_GSYNC_DEVICES)
        {
            nvGSyncHandles = new IntPtr[gsyncCount];
            Array.Copy(handles, nvGSyncHandles, (int)gsyncCount);
        }
        else
        {
            nvGSyncHandles = handles;
        }
    }

    private static readonly Delegates.GSync.NvAPI_GSync_GetControlParameters _gsyncGetControlParametersDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_GetControlParameters>();

    public static void GSyncGetControlParameters(
        IntPtr hNvGSyncDevice,
        out GSyncControlParametersV2 gsyncControls // Public API uses out for convenience
    )
    {
        // Instantiate the struct here. Instantiate<T> sets up _Version and nested structs/arrays.
        gsyncControls = typeof(GSyncControlParametersV2).Instantiate<GSyncControlParametersV2>();
        var status = _gsyncGetControlParametersDelegate(
            hNvGSyncDevice,
            ref gsyncControls // Delegate expects ref, gsyncControls is now initialized with _Version
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }

    private static readonly Delegates.GSync.NvAPI_GSync_GetStatusParameters _gsyncGetStatusParametersDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_GetStatusParameters>();

    public static void GSyncGetStatusParameters(
        IntPtr hNvGSyncDevice,
        out GSyncStatusParametersV2 statusParams // Public API uses out for convenience
    )
    {
        // Instantiate the struct here.
        statusParams = typeof(GSyncStatusParametersV2).Instantiate<GSyncStatusParametersV2>();
        var status = _gsyncGetStatusParametersDelegate(
            hNvGSyncDevice,
            ref statusParams // Delegate expects ref
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }

    private static readonly Delegates.GSync.NvAPI_GSync_GetSyncStatus _gsyncGetSyncStatusDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_GetSyncStatus>();

    public static void GSyncGetSyncStatus(
        IntPtr hNvGSyncDevice,
        IntPtr hPhysicalGpu,
        out GSyncBoardStatus syncStatus // Public API uses out for convenience
    )
    {
        // Instantiate the struct here.
        syncStatus = typeof(GSyncBoardStatus).Instantiate<GSyncBoardStatus>();
        var status = _gsyncGetSyncStatusDelegate(
            hNvGSyncDevice,
            hPhysicalGpu,
            ref syncStatus // Delegate expects ref
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }

    private static readonly Delegates.GSync.NvAPI_GSync_GetTopology _gsyncGetTopologyDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_GetTopology>();

    public static void GSyncGetTopology(
        IntPtr hNvGSyncDevice,
        out GSyncGpu[] gsyncGPUs,
        out GSyncDisplay[] gsyncDisplays
    )
    {
        uint gpuCount = 0;
        uint displayCount = 0;
        GSyncGpu[] tempGpus = null;
        GSyncDisplay[] tempDisplays = null;

        var status = _gsyncGetTopologyDelegate(
            hNvGSyncDevice,
            ref gpuCount,
            tempGpus, // Null for count query
            ref displayCount,
            tempDisplays // Null for count query
        );

        if (status != Status.Ok && status != Status.InvalidArgument && status != Status.ArgumentExceedMaxSize)
        {
            gsyncGPUs = Array.Empty<GSyncGpu>();
            gsyncDisplays = Array.Empty<GSyncDisplay>();
            throw new NVIDIAApiException(status);
        }

        gsyncGPUs = new GSyncGpu[gpuCount];
        gsyncDisplays = new GSyncDisplay[displayCount];

        if (gpuCount == 0 && displayCount == 0)
        {
            return;
        }

        var gpuStructType = typeof(GSyncGpu);
        for (int i = 0; i < gpuCount; i++)
        {
            gsyncGPUs[i] = gpuStructType.Instantiate<GSyncGpu>(); // Instantiate sets _Version
        }

        var displayStructType = typeof(GSyncDisplay);
        for (int i = 0; i < displayCount; i++)
        {
            gsyncDisplays[i] = displayStructType.Instantiate<GSyncDisplay>(); // Instantiate sets _Version
        }

        status = _gsyncGetTopologyDelegate(
            hNvGSyncDevice,
            ref gpuCount,
            gsyncGPUs,
            ref displayCount,
            gsyncDisplays
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }

    private static readonly Delegates.GSync.NvAPI_GSync_QueryCapabilities _gsyncQueryCapabilitiesDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_QueryCapabilities>();

    public static void GSyncQueryCapabilities(
        IntPtr hNvGSyncDevice,
        out GSyncCapabilitiesV3 gsyncCapabilities // Public API uses out for convenience
    )
    {
        // Instantiate the struct here.
        gsyncCapabilities = typeof(GSyncCapabilitiesV3).Instantiate<GSyncCapabilitiesV3>();
        var status = _gsyncQueryCapabilitiesDelegate(
            hNvGSyncDevice,
            ref gsyncCapabilities // Delegate expects ref
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }

    private static readonly Delegates.GSync.NvAPI_GSync_SetControlParameters _gsyncSetControlParametersDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_SetControlParameters>();

    public static void GSyncSetControlParameters(
        IntPtr hNvGSyncDevice,
        ref GSyncControlParametersV2 gsyncControls // This is correctly 'ref' as it's truly in/out
    )
    {
        // Caller is responsible for gsyncControls's full initialization (including _Version)
        // using typeof(GSyncControlParametersV2).Instantiate<GSyncControlParametersV2>()
        // and then setting desired fields.
        var status = _gsyncSetControlParametersDelegate(
            hNvGSyncDevice,
            ref gsyncControls
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }

    private static readonly Delegates.GSync.NvAPI_GSync_SetSyncStateSettings _gsyncSetSyncStateSettingsDelegate =
        DelegateFactory.GetDelegate<Delegates.GSync.NvAPI_GSync_SetSyncStateSettings>();

    public static void GSyncSetSyncStateSettings(
        GSyncDisplay[] pGsyncDisplays, // Caller creates array and uses Instantiate<GSyncDisplay>() for elements
        uint flags = 0
    )
    {
        uint gsyncDisplayCount = (uint)(pGsyncDisplays?.Length ?? 0);

        // Caller is responsible for ensuring each element in pGsyncDisplays is properly initialized
        // (including its _Version) via typeof(GSyncDisplay).Instantiate<GSyncDisplay>().
        var status = _gsyncSetSyncStateSettingsDelegate(
            gsyncDisplayCount,
            pGsyncDisplays,
            flags
        );

        if (status != Status.Ok)
        {
            throw new NVIDIAApiException(status);
        }
    }
}
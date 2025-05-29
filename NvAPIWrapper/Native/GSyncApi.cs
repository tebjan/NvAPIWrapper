using System;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.GSync.Enums;
using NvAPIWrapper.Native.GSync.Structures;
using NvAPIWrapper.Native.GSync;
using NvAPIWrapper.Native.Helpers; // For GSyncConstants


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
        ref GSyncDelay gsyncDelay, // Caller creates and passes this, constructor handles _Version
        out uint syncSteps
    )
    {
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
        ref GSyncDelay gsyncDelay // Caller creates and passes this, constructor handles _Version
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
        gsyncControls = new GSyncControlParametersV2(); // Constructor handles _Version and nested _Versions via Instantiate
        var status = _gsyncGetControlParametersDelegate(
            hNvGSyncDevice,
            ref gsyncControls // Delegate expects ref
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
        statusParams = new GSyncStatusParametersV2(); // Constructor handles _Version and array allocations via Instantiate
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
        out GSyncBoardStatus syncStatus // Public API uses out for convenience
    )
    {
        syncStatus = new GSyncBoardStatus(); // Constructor handles _Version via Instantiate
        var status = _gsyncGetSyncStatusDelegate(
            hNvGSyncDevice,
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
        out GSyncGpuV2[] gsyncGPUs,
        out GSyncDisplayV2[] gsyncDisplays
    )
    {
        uint gpuCount = 0;
        uint displayCount = 0;
        GSyncGpuV2[] tempGpus = null;
        GSyncDisplayV2[] tempDisplays = null;

        var status = _gsyncGetTopologyDelegate(
            hNvGSyncDevice,
            ref gpuCount,
            tempGpus,
            ref displayCount,
            tempDisplays
        );

        if (status != Status.Ok)
        {
            gsyncGPUs = Array.Empty<GSyncGpuV2>();
            gsyncDisplays = Array.Empty<GSyncDisplayV2>();
            throw new NVIDIAApiException(status);
        }

        gsyncGPUs = new GSyncGpuV2[gpuCount];
        gsyncDisplays = new GSyncDisplayV2[displayCount];

        if (gpuCount == 0 && displayCount == 0)
        {
            return;
        }

        // For the second call, NVAPI expects the _Version of each struct in the array to be initialized.
        // The default constructor of GSyncGpuV2/GSyncDisplayV2 (which calls Instantiate) handles this when we new them up.
        for (int i = 0; i < gpuCount; i++)
        {
            gsyncGPUs[i] = new GSyncGpuV2(); // Constructor sets _Version via Instantiate
        }

        for (int i = 0; i < displayCount; i++)
        {
            gsyncDisplays[i] = new GSyncDisplayV2(); // Constructor sets _Version via Instantiate
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
        gsyncCapabilities = new GSyncCapabilitiesV3(); // Constructor handles _Version and array allocations via Instantiate
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
        ref GSyncControlParametersV2 gsyncControls // Caller creates and passes this, constructor handles _Version
    )
    {
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
        GSyncDisplayV2[] pGsyncDisplays, // Caller creates and passes this, elements' constructors handle _Version
        uint flags = 0
    )
    {
        uint gsyncDisplayCount = (uint)(pGsyncDisplays?.Length ?? 0);

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
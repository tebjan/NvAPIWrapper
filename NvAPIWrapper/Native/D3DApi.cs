using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.D3D;
using NvAPIWrapper.Native.D3D.Structures;

namespace NvAPIWrapper.Native
{
    /// <summary>
    ///     Contains Stereo static functions
    /// </summary>
    // ReSharper disable once ClassTooBig
    public static class D3DApi
    {
        // Cache the delegate to avoid multiple lookups
        private static readonly Delegates.D3D.NvAPI_D3D1x_Present _presentDelegate =
        DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D1x_Present>();

        //if we need the performance, we can avoid virtual call by using the following unsafe code
        //private static readonly unsafe delegate* unmanaged[Cdecl]<IntPtr, IntPtr, uint, uint, Status> _presentPtr =
        //(delegate* unmanaged[Cdecl]<IntPtr, IntPtr, uint, uint, Status>)Marshal.GetFunctionPointerForDelegate(_presentDelegate);

        /// <summary>
        ///    Presents the contents of the next buffer in the sequence of back buffers owned by a D3D device.
        ///    This Present operation supports using a SwapGroup and SwapBarrier on the SwapChain that owns the back buffer to be presented.
        ///    NOTE: NvAPI_D3D1x_Present is a wrapper of the method IDXGISwapChain::Present which additionally notifies the D3D driver of the SwapChain used by the runtime for presentation, 
        ///    thus allowing the D3D driver to apply SwapGroup and SwapBarrier functionality to that SwapChain.
        /// </summary>
        /// <param name="d3dDevice">The D3D device interface that is used to issue the Present operation, using the following IDirect3DDevice9::Present input parameters.</param>
        /// <param name="dxgiSwapChain">The IDXGISwapChain interface that is intended to present</param>
        /// <param name="syncInterval">An integer that specifies the how to synchronize presentation of a frame with the vertical blank.
        /// Values are: 
        /// - 0:  The presentation occurs immediately, there is no synchronization.
        /// - 1,2,3,4 : Synchronize presentation after the n'th vertical blank.</param>
        /// <param name="flags">An integer value that contains swap-chain presentation options as defined in DXGI_PRESENT.</param>
        /// <returns>Ok: the Present operation was successfully executed
        /// DeviceBusy: the Present operation failed with an error DXGI_ERROR_DEVICE_RESET or DXGI_ERROR_DEVICE_REMOVED
        /// DXGI_STATUS_OCCLUDED, or D3DDDIERR_DEVICEREMOVED.
        /// Error: the communication with the D3D driver failed, SwapGroup/SwapBarrier may not be possible.
        /// ApiNotInitialized: NvAPI was not yet initialized.</returns>
        ///</returns> 
        public static void D3D1XPresent(
            IntPtr d3dDevice,
            IntPtr dxgiSwapChain,
            uint syncInterval,
            uint flags
        )
        {
            var status = _presentDelegate(
                d3dDevice,
                dxgiSwapChain,
                syncInterval,
                flags
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

        }

        private static readonly Delegates.D3D.NvAPI_D3D1x_QueryFrameCount _queryFrameCountDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D1x_QueryFrameCount>();

        /// <summary>
        ///     This API queries the universal framecounter of the Quadro-Sync master device.
        /// </summary>
        /// <param name="d3dDevice">The caller provides the D3D device that has access to the Quadro-Sync device,
        /// pDevice can be either ID3D10Device or ID3D10Device1 or ID3D11Device or ID3D12Device.</param>
        /// <param name="frameCount">The caller provides the storage space where the framecount is stored.</param>
        /// <returns>NVAPI_OK if the frame count was successfully populated, NVAPI_ERROR if the operation failed, 
        /// NVAPI_INVALID_ARGUMENT if one or more args passed in are invalid, 
        /// NVAPI_API_NOT_INITIALIZED if NvAPI was not yet initialized.</returns>
        public static void D3D1XQueryFrameCount(
            IntPtr d3dDevice,
            out uint frameCount
        )
        {
            var status = _queryFrameCountDelegate(
                d3dDevice,
                out frameCount
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D1x_ResetFrameCount _resetFrameCountDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D1x_ResetFrameCount>();

        /// <summary>
        ///     This API resets the universal framecounter on the Quadro-Sync master device.
        /// </summary>
        /// <param name="d3dDevice">The caller provides the D3D device that has access to the Quadro-Sync device,
        /// pDevice can be either ID3D10Device or ID3D10Device1 or ID3D11Device or ID3D12Device.</param>
        /// <returns>NVAPI_OK if the frame counter has been reset, NVAPI_ERROR if the operation failed,
        /// NVAPI_INVALID_ARGUMENT if pDevice is invalid, NVAPI_API_NOT_INITIALIZED if NvAPI was not yet initialized.</returns>
        public static void D3D1XResetFrameCount(
            IntPtr d3dDevice
        )
        {
            var status = _resetFrameCountDelegate(
                d3dDevice
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D1x_QueryMaxSwapGroup _queryMaxSwapGroupDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D1x_QueryMaxSwapGroup>();

        /// <summary>
        ///     This API queries the number of supported SwapGroups and SwapBarriers in the graphics system.
        /// </summary>
        /// <param name="d3dDevice">The caller provides the D3D device that is intended to use SwapGroup functionality.
        /// pDevice can be either ID3D10Device or ID3D10Device1 or ID3D11Device or ID3D12Device.</param>
        /// <param name="maxGroups">The caller provides the storage space where the number of available SwapGroups is stored.</param>
        /// <param name="maxBarriers">The caller provides the storage space where the number of available SwapBarriers is stored.</param>
        /// <returns>NVAPI_OK if the number of SwapGroups and SwapBarriers has been successfully stored, 
        /// NVAPI_ERROR if the operation failed, 
        /// NVAPI_INVALID_ARGUMENT if one or more arguments passed in are invalid, 
        /// NVAPI_API_NOT_INITIALIZED if NvAPI was not yet initialized.</returns>
        public static void D3D1XQueryMaxSwapGroup(
            IntPtr d3dDevice,
            out uint maxGroups,
            out uint maxBarriers
        )
        {
            var status = _queryMaxSwapGroupDelegate(
                d3dDevice,
                out maxGroups,
                out maxBarriers
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D1x_QuerySwapGroup _querySwapGroupDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D1x_QuerySwapGroup>();

        /// <summary>
        ///     This API queries the current SwapGroup and SwapBarrier that a SwapChain of a specific client device is bound to.
        /// </summary>
        /// <param name="d3dDevice">The caller provides the D3D device that owns the SwapChain used as a SwapGroup client.
        /// pDevice can be either ID3D10Device or ID3D10Device1 or ID3D11Device or ID3D12Device.</param>
        /// <param name="swapChain">The IDXGISwapChain interface that is used as the SwapGroup client.</param>
        /// <param name="swapGroup">The caller provides the storage space where the current SwapGroup is stored.</param>
        /// <param name="swapBarrier">The caller provides the storage space where the current SwapBarrier is stored.</param>
        /// <returns>NVAPI_OK if the current SwapGroup and SwapBarrier have been successfully stored, 
        /// NVAPI_ERROR if the operation failed, 
        /// NVAPI_INVALID_ARGUMENT if one or more arguments passed in are invalid, 
        /// NVAPI_API_NOT_INITIALIZED if NvAPI was not yet initialized.</returns>
        public static void D3D1XQuerySwapGroup(
            IntPtr d3dDevice,
            IntPtr swapChain,
            out uint swapGroup,
            out uint swapBarrier
        )
        {
            var status = _querySwapGroupDelegate(
                d3dDevice,
                swapChain,
                out swapGroup,
                out swapBarrier
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D1x_JoinSwapGroup _joinSwapGroupDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D1x_JoinSwapGroup>();

        /// <summary>
        ///     This API causes the SwapChain of a SwapGroup client to join or leave the specified SwapGroup.
        /// </summary>
        /// <param name="d3dDevice">The caller provides the D3D device that owns the SwapChain used as a SwapGroup client.
        /// pDevice can be either ID3D10Device or ID3D10Device1 or ID3D11Device or ID3D12Device.</param>
        /// <param name="swapChain">The IDXGISwapChain interface that is used as the SwapGroup client.</param>
        /// <param name="group">The caller specifies the SwapGroup which the SwapChain should join.
        /// If the value of group is zero, the SwapChain leaves the SwapGroup.
        /// The SwapChain joins a SwapGroup if the SwapGroup number is a positive integer less than or equal to the maximum number of SwapGroups queried by NvAPI_SwapGroup_QueryMaxSwapGroup.</param>
        /// <param name="blocking">The caller specifies whether the presentation should block or return immediately until all members of the SwapGroup are ready.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void D3D1XJoinSwapGroup(
            IntPtr d3dDevice,
            IntPtr swapChain,
            uint group,
            bool blocking
        )
        {
            var status = _joinSwapGroupDelegate(
                d3dDevice,
                swapChain,
                group,
                blocking
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D1x_BindSwapBarrier _bindSwapBarrierDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D1x_BindSwapBarrier>();

        /// <summary>
        ///     This API causes a SwapGroup to be bound to or released from the specified SwapBarrier.
        /// </summary>
        /// <param name="d3dDevice">The caller provides the D3D device that owns the SwapChain used as a SwapGroup client.
        /// pDevice can be either ID3D10Device or ID3D10Device1 or ID3D11Device or ID3D12Device.</param>
        /// <param name="group">The caller specifies the SwapGroup to be bound to the SwapBarrier.</param>
        /// <param name="barrier">The caller specifies the SwapBarrier that the SwapGroup should be bound to.
        /// If the value of barrier is zero, the SwapGroup releases the SwapBarrier.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void D3D1XBindSwapBarrier(
            IntPtr d3dDevice,
            uint group,
            uint barrier
        )
        {
            var status = _bindSwapBarrierDelegate(
                d3dDevice,
                group,
                barrier
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D12_QueryPresentBarrierSupport _queryPresentBarrierSupportDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D12_QueryPresentBarrierSupport>();

        /// <summary>
        ///     This API returns if presentBarrier feature is supported on the specified device.
        /// </summary>
        /// <param name="d3dDevice">The ID3D12Device device which owns the SwapChain as a PresentBarrier client.</param>
        /// <param name="supported">Pointer to a boolean returning true if supported, false otherwise.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void D3D12QueryPresentBarrierSupport(
            IntPtr d3dDevice,
            out bool supported
        )
        {
            var status = _queryPresentBarrierSupportDelegate(
                d3dDevice,
                out supported
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D12_CreatePresentBarrierClient _createPresentBarrierClientDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D12_CreatePresentBarrierClient>();

        /// <summary>
        ///     This API returns an NvPresentBarrierClientHandle handle, which
        ///     owns the swapchain to be synchronized through PresentBarrier.
        ///     This handle is used in other PresentBarrier functions.
        /// </summary>
        /// <param name="d3dDevice">The ID3D12Device device which owns the SwapChain as a PresentBarrier client.</param>
        /// <param name="swapChain">The IDXGISwapChain interface that PresentBarrier is operated on.</param>
        /// <param name="presentBarrierClient">Pointer to an NvPresentBarrierClientHandle handle created by the driver on success.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void D3D12CreatePresentBarrierClient(
            IntPtr d3dDevice,
            IntPtr swapChain,
            out PresentBarrierClientHandle presentBarrierClient
        )
        {
            var status = _createPresentBarrierClientDelegate(
                d3dDevice,
                swapChain,
                out presentBarrierClient
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D12_RegisterPresentBarrierResources _registerPresentBarrierResourcesDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D12_RegisterPresentBarrierResources>();

        /// <summary>
        ///     This API registers scanout resources of a presentBarrier client
        ///     to the presentBarrier, and a fence object which is used for
        ///     presentBarrier synchronization. Once the registration has completed
        ///     successfully, it is not allowed to add additional resources, i.e. the
        ///     number of back buffers and fence object are not allowed to be
        ///     changed. However, the application must call this function whenever the
        ///     back buffers are changed, e.g. ResizeBuffers() is called.
        /// </summary>
        /// <param name="presentBarrierClient">An NvPresentBarrierClientHandle client handle that owns the resources.</param>
        /// <param name="fence">An ID3D12Fence object created by the application and used for present synchronization through presentBarrier.</param>
        /// <param name="resources">An array of ID3D12Resource to be synchronized through presentBarrier.</param>
        /// <param name="numResources">The number of ID3D12Resource elements in the resources array.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void D3D12RegisterPresentBarrierResources(
            PresentBarrierClientHandle presentBarrierClient,
            IntPtr fence,
            IntPtr[] resources,
            uint numResources
        )
        {
            var status = _registerPresentBarrierResourcesDelegate(
                presentBarrierClient,
                fence,
                resources,
                numResources
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_DestroyPresentBarrierClient _destroyPresentBarrierClientDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_DestroyPresentBarrierClient>();

        /// <summary>
        ///     This API destroys a presentBarrier client, and must be called
        ///     after the client leaves presentBarrier to avoid memory leaks.
        /// </summary>
        /// <param name="presentBarrierClient">An NvPresentBarrierClientHandle handle created by NvAPI_xxxx_CreatePresentBarrierClient.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void DestroyPresentBarrierClient(
            PresentBarrierClientHandle presentBarrierClient
        )
        {
            var status = _destroyPresentBarrierClientDelegate(
                presentBarrierClient
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_JoinPresentBarrier _joinPresentBarrierDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_JoinPresentBarrier>();

        private static JoinPresentBarrierParams _joinParams = JoinPresentBarrierParams.CreateDefault();

        /// <summary>
        ///     This API adds a registered PresentBarrier client to the presentBarrier.
        ///     If the call succeeds, image present of the registered scanout resources
        ///     from this client is under the synchronization of presentBarrier.
        /// </summary>
        /// <param name="presentBarrierClient">An NvPresentBarrierClientHandle handle created by NvAPI_xxxx_CreatePresentBarrierClient.</param>
        /// <param name="pParams">Parameters to join presentBarrier.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void JoinPresentBarrier(
            PresentBarrierClientHandle presentBarrierClient
        )
        {
            var status = _joinPresentBarrierDelegate(
                presentBarrierClient,
                _joinParams
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_LeavePresentBarrier _leavePresentBarrierDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_LeavePresentBarrier>();

        /// <summary>
        ///     This API removes a registered client from presentBarrier. If this
        ///     client does not join presentBarrier, this function does nothing.
        /// </summary>
        /// <param name="presentBarrierClient">An NvPresentBarrierClientHandle handle created by NvAPI_xxxxx_CreatePresentBarrierClient.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void LeavePresentBarrier(
            PresentBarrierClientHandle presentBarrierClient
        )
        {
            var status = _leavePresentBarrierDelegate(
                presentBarrierClient
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_QueryPresentBarrierFrameStatistics _queryPresentBarrierFrameStatisticsDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_QueryPresentBarrierFrameStatistics>();

        /// <summary>
        ///     This API returns the presentBarrier frame statistics of the last
        ///     present call from this client. If the client did not join
        ///     presentBarrier, the SyncMode is returned as PRESENT_BARRIER_NOT_JOINED,
        ///     and all other fields are reset. Driver does not retain any
        ///     presentBarrier info of the client once it leaves presentBarrier.
        /// </summary>
        /// <param name="presentBarrierClient">An NvPresentBarrierClientHandle handle created by NvAPI_xxxxx_CreatePresentBarrierClient.</param>
        /// <param name="pFrameStats">Pointer to NV_PRESENT_BARRIER_FRAME_STATISTICS structure about presentBarrier statistics.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void QueryPresentBarrierFrameStatistics(
            PresentBarrierClientHandle presentBarrierClient,
            out PresentBarrierFrameStatistics pFrameStats
        )
        {
            var status = _queryPresentBarrierFrameStatisticsDelegate(
                presentBarrierClient,
                out pFrameStats
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        private static readonly Delegates.D3D.NvAPI_D3D12_CreateDDisplayPresentBarrierClient _createDDisplayPresentBarrierClientDelegate =
            DelegateFactory.GetDelegate<Delegates.D3D.NvAPI_D3D12_CreateDDisplayPresentBarrierClient>();

        /// <summary>
        ///     This API returns an NvPresentBarrierClientHandle handle.
        /// </summary>
        /// <param name="d3dDevice">The ID3D12Device device which executes the rendering commands of this PresentBarrier client. It must be created on the same adapter as DisplayDevice.</param>
        /// <param name="sourceId">The adapter-relative identifier for the DisplaySource obtained from DisplaySource.SourceId().</param>
        /// <param name="presentBarrierClient">Pointer to an NvPresentBarrierClientHandle handle created by the driver on success.</param>
        /// <exception cref="NVIDIAApiException">Thrown if the operation failed.</exception>
        public static void D3D12CreateDDisplayPresentBarrierClient(
            IntPtr d3dDevice,
            uint sourceId,
            out PresentBarrierClientHandle presentBarrierClient
        )
        {
            var status = _createDDisplayPresentBarrierClientDelegate(
                d3dDevice,
                sourceId,
                out presentBarrierClient
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }


    }
}
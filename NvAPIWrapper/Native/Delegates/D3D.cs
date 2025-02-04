using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.DRS.SettingValues;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.D3D;
using NvAPIWrapper.Native.D3D.Structures;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Stereo;
using NvAPIWrapper.Native.Stereo.Structures;

// ReSharper disable InconsistentNaming
namespace NvAPIWrapper.Native.Delegates
{
    internal static class D3D
    {
        [FunctionId(FunctionId.NvAPI_D3D9_CreateSwapChain)]
        public delegate Status NvAPI_D3D9_CreateSwapChain(
            [In] StereoHandle stereoHandle,
            [In] IntPtr d3dPresentParameters,
            [Out] out IntPtr direct3DSwapChain9,
            [In] StereoSwapChainMode mode
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_CreateSwapChain)]
        public delegate Status NvAPI_D3D1x_CreateSwapChain(
            [In] StereoHandle stereoHandle,
            [In] IntPtr dxgiSwapChainDescription,
            [Out] out IntPtr dxgiSwapChain,
            [In] StereoSwapChainMode mode
        );

        [FunctionId(FunctionId.NvAPI_D3D_SetFPSIndicatorState)]
        public delegate Status NvAPI_D3D_SetFPSIndicatorState(
            [In] IntPtr d3dDevice,
            [In] byte doEnable
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_Present)]
        public delegate Status NvAPI_D3D1x_Present(
            [In] IntPtr d3dDevice,
            [In] IntPtr dxgiSwapChain,
            [In] uint syncInterval,
            [In] uint flags
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_QueryFrameCount)]
        public delegate Status NvAPI_D3D1x_QueryFrameCount(
            [In] IntPtr d3dDevice,
            [Out] out uint frameCount
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_ResetFrameCount)]
        public delegate Status NvAPI_D3D1x_ResetFrameCount(
            [In] IntPtr d3dDevice
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_QueryMaxSwapGroup)]
        public delegate Status NvAPI_D3D1x_QueryMaxSwapGroup(
            [In] IntPtr d3dDevice,
            [Out] out uint maxGroups,
            [Out] out uint maxBarriers
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_QuerySwapGroup)]
        public delegate Status NvAPI_D3D1x_QuerySwapGroup(
            [In] IntPtr d3dDevice,
            [In] IntPtr dxgiSwapChain,
            [Out] out uint swapGroup,
            [Out] out uint swapBarrier
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_JoinSwapGroup)]
        public delegate Status NvAPI_D3D1x_JoinSwapGroup(
            [In] IntPtr d3dDevice,
            [In] IntPtr dxgiSwapChain,
            [In] uint group,
            [In] bool blocking
        );

        [FunctionId(FunctionId.NvAPI_D3D1x_BindSwapBarrier)]
        public delegate Status NvAPI_D3D1x_BindSwapBarrier(
            [In] IntPtr d3dDevice,
            [In] uint group,
            [In] uint barrier
        );

        [FunctionId(FunctionId.NvAPI_D3D12_QueryPresentBarrierSupport)]
        public delegate Status NvAPI_D3D12_QueryPresentBarrierSupport(
            [In] IntPtr d3dDevice,
            [Out] out bool supported
        );

        [FunctionId(FunctionId.NvAPI_D3D12_CreatePresentBarrierClient)]
        public delegate Status NvAPI_D3D12_CreatePresentBarrierClient(
            [In] IntPtr d3dDevice,
            [In] IntPtr dxgiSwapChain,
            [Out] out PresentBarrierClientHandle presentBarrierClient
        );

        [FunctionId(FunctionId.NvAPI_D3D12_RegisterPresentBarrierResources)]
        public delegate Status NvAPI_D3D12_RegisterPresentBarrierResources(
            [In] PresentBarrierClientHandle presentBarrierClient,
            [In] IntPtr fence,
            [In] IntPtr[] resources,
            [In] uint numResources
        );

        [FunctionId(FunctionId.NvAPI_DestroyPresentBarrierClient)]
        public delegate Status NvAPI_DestroyPresentBarrierClient(
            [In] PresentBarrierClientHandle presentBarrierClient
        );

        [FunctionId(FunctionId.NvAPI_JoinPresentBarrier)]
        public delegate Status NvAPI_JoinPresentBarrier(
            [In] PresentBarrierClientHandle presentBarrierClient,
            [In] JoinPresentBarrierParams pParams
        );

        [FunctionId(FunctionId.NvAPI_LeavePresentBarrier)]
        public delegate Status NvAPI_LeavePresentBarrier(
            [In] PresentBarrierClientHandle presentBarrierClient
        );

        [FunctionId(FunctionId.NvAPI_QueryPresentBarrierFrameStatistics)]
        public delegate Status NvAPI_QueryPresentBarrierFrameStatistics(
            [In] PresentBarrierClientHandle presentBarrierClient,
            [Out] out PresentBarrierFrameStatistics pFrameStats
        );

        [FunctionId(FunctionId.NvAPI_D3D12_CreateDDisplayPresentBarrierClient)]
        public delegate Status NvAPI_D3D12_CreateDDisplayPresentBarrierClient(
            [In] IntPtr d3dDevice,
            [In] uint sourceId,
            [Out] out PresentBarrierClientHandle presentBarrierClient
        );

    }
}
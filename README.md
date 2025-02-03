## <img src="NvAPIWrapper/Icon.png" width="24" alt="NvAPIWrapper"> NvAPIWrapper (for NVAPI R560)
[![](https://img.shields.io/github/license/tebjan/NvAPIWrapper.svg?style=flat-square)](https://github.com/tebjan/NvAPIWrapper/blob/master/LICENSE)
[![](https://img.shields.io/github/commit-activity/y/tebjan/NvAPIWrapper.svg?style=flat-square)](https://github.com/tebjan/NvAPIWrapper/commits/master)
[![](https://img.shields.io/github/issues/tebjan/NvAPIWrapper.svg?style=flat-square)](https://github.com/tebjan/NvAPIWrapper/issues)

### **Note:**
This is a new fork of the NvAPIWrapper project. The repository has been updated to support **NVAPI R560**, incorporating the latest changes and improvements.

This fork also introduces **initial support for D3D capabilities**, specifically the **Quadro Sync swap group functionality**. Currently, only the **low-level API** is implemented. 

NvAPIWrapper is a .NET wrapper for NVIDIA's public API, capable of managing all aspects of a display setup using NVIDIA GPUs.

This project is licensed under LGPL and can be used in closed-source or commercial projects. However, any modifications to the main code must be public, and a README file must accompany the DLL to clarify the license terms and include a hyperlink to this repository. [Read more about LGPL](https://github.com/tebjan/NvAPIWrapper/blob/master/LICENSE).

## How to get
**Note:** There is currently no new NuGet package for this fork. The previous version is still available, but an updated release is planned for the future.
[![](https://img.shields.io/nuget/dt/NvAPIWrapper.Net.svg?style=flat-square)](https://www.nuget.org/packages/NvAPIWrapper.Net)
[![](https://img.shields.io/nuget/v/NvAPIWrapper.Net.svg?style=flat-square)](https://www.nuget.org/packages/NvAPIWrapper.Net)

This library is available for download and use through the <a href="https://www.nuget.org/packages/NvAPIWrapper.Net">NuGet Gallery</a>.

## What's Supported
NvAPIWrapper is not a complete wrapper of NVAPI yet. Below is a list of supported features:

- [x] **General:** Full Support
  - [x] Chipset Information
  - [x] Driver Information and Restart
  - [x] Lid and Dock Information

- [x] **Surround:** Full Support
  - [x] Topology Configuration (Mosaic Phase 2)
  - [x] Grid Configuration (Mosaic Phase 1)
  - [x] Warping Control
  - [x] Color Intensity Control

- [x] **Display:** Full Support
  - [x] Display Information and Capabilities
  - [x] Path Configuration
  - [x] Custom Resolutions
  - [x] Refresh Rate Override
  - [x] HDMI Support & Capabilities
  - [x] DisplayPort Color Capabilities
  - [x] HDR Capabilities

- [x] **Display Control:** Full Support
  - [x] Color Control
  - [x] Saturation Control (Vibrance)
  - [x] HUE Control
  - [x] HDMI InfoFrame Control
  - [x] HDR and HDR Color Control
  - [x] ScanOut Information and Configuration
  - [x] View Control
  - [x] EDID Retrieval and Modification

- [x] **GPU:** Full Support
  - [x] GPU Information
  - [x] GPU Architecture Information
  - [x] Output Information
  - [x] ECC Memory Reporting & Management
  - [x] PCI-E Information
  - [x] Performance Monitoring
  - [x] Cooler Information & Management (Including RTX+)
  - [x] GPU Illumination Management
  - [x] Usage Monitoring
  - [x] Power & Thermal Limit Management (Low-Level API)
  - [x] Performance State Management (Low-Level API)
  - [x] Clock & Voltage Boost Configurations (Low-Level API)

- [x] **DRS:** Full Support
  - [x] Session, Profile, and Application Management
  - [x] Documented & Managed Settings

- [x] **Stereo (3D):** Full Support
  - [x] Stereo Control & Configurations

- [x] **D3D:** Partial Support
  - [x] Quadro Sync Swap Group (Low-Level API)
- [ ] **GSync:** No Support
- [ ] **OpenGL:** No Support
- [ ] **Video:** No Support

If a feature you need is missing, feel free to open an [issue](https://github.com/tebjan/NvAPIWrapper/issues).

## How to use
NvAPIWrappr allows you to use the NVAPI functions directly (a.k.a. the low-level API) using the `NvAPIWrapper.Native` namespace. However, there is also a .Net friendly implementation of the NVAPI features (a.k.a. the high-level API) that can be used to minimize the complexity of your code and makes it more compatible with later releases of the library, therefore, I strongly recommend using these classes instead of using the native functions directly.

Currently, you can access different parts of the high level API as follow:

* Namespace `NvAPIWrapper.Display`: Display and Display Control API
* Namespace `NvAPIWrapper.GPU`: GPU specific API
* Namespace `NvAPIWrapper.Mosaic`: Mosaic API - Surround
* Namespace `NvAPIWrapper.DRS`: NVIDIA Driver settings and application profiles
* Namespace `NvAPIWrapper.Stereo`: Stereo specific settings and configurations
* Class `NvAPIWrapper.NVIDIA`: General Information And Methods

Please also take a look at the `NvAPISample` project for a number of simple examples.

This library is fully documented and this makes your journey through it as easy as it is possible with NVAPI.

## Related Projects

- [**WindowsDisplayAPI**](https://github.com/falahati/WindowsDisplayAPI/): WindowsDisplayAPI is a .Net wrapper for Windows Display and Windows CCD APIs

- [**EDIDParser**](https://github.com/falahati/EDIDParser/): EDIDParser is a library allowing all .Net developers to parse and to extract information from raw EDID binary data. (Extended Display Identification Data)

- [**HeliosDisplayManagement**](https://github.com/falahati/HeliosDisplayManagement/): An open source display profile management program for Windows with support for NVIDIA Surround

## License
Copyright (C) 2017-2020 Soroush Falahati

This project is licensed under the GNU Lesser General Public License ("LGPL") and therefore can be used in closed source or commercial projects. 
However, any commit or change to the main code must be public and there should be a read me file along with the DLL clarifying the license and its terms as part of your project as well as a hyperlink to this repository. [Read more about LGPL](LICENSE).

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeviceService.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class GlobalStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GlobalStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DeviceService.Domain.Resources.GlobalStrings", typeof(GlobalStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IVR device for specified patient already exists.
        /// </summary>
        internal static string CreteDeviceStatus_IVRAlreadyExists {
            get {
                return ResourceManager.GetString("CreteDeviceStatus_IVRAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certificate signing request invalid.
        /// </summary>
        internal static string SetDecomissionStatus_CertificateSigningRequestInvalid {
            get {
                return ResourceManager.GetString("SetDecomissionStatus_CertificateSigningRequestInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Device not found.
        /// </summary>
        internal static string SetDecomissionStatus_DeviceNotFound {
            get {
                return ResourceManager.GetString("SetDecomissionStatus_DeviceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid decomission status.
        /// </summary>
        internal static string SetDecomissionStatus_InvalidDecomissionStatus {
            get {
                return ResourceManager.GetString("SetDecomissionStatus_InvalidDecomissionStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid device status.
        /// </summary>
        internal static string SetDecomissionStatus_InvalidDeviceStatus {
            get {
                return ResourceManager.GetString("SetDecomissionStatus_InvalidDeviceStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Status was changed successfuly.
        /// </summary>
        internal static string SetDecomissionStatus_Success {
            get {
                return ResourceManager.GetString("SetDecomissionStatus_Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PinCode should be provided if IsPinCodeRequired = true and PinCode wasn&apos;t set before.
        /// </summary>
        internal static string UpdateDeviceStatus_PinCodeRequired {
            get {
                return ResourceManager.GetString("UpdateDeviceStatus_PinCodeRequired", resourceCulture);
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventMaker.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ExceptionResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EventMaker.Common.Resources.ExceptionResource", typeof(ExceptionResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You already participant or owner of this event or Author of this event.
        /// </summary>
        public static string AlreadyParticipant {
            get {
                return ResourceManager.GetString("AlreadyParticipant", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event not found or you have no permissions to this action.
        /// </summary>
        public static string EventNotFound {
            get {
                return ResourceManager.GetString("EventNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name already exist.
        /// </summary>
        public static string NameAlreadyExist {
            get {
                return ResourceManager.GetString("NameAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not Changed.
        /// </summary>
        public static string NotChanged {
            get {
                return ResourceManager.GetString("NotChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not Created.
        /// </summary>
        public static string NotCreated {
            get {
                return ResourceManager.GetString("NotCreated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cant delete or already deleted.
        /// </summary>
        public static string NotDeleted {
            get {
                return ResourceManager.GetString("NotDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have less participant places than already participants in your event .
        /// </summary>
        public static string OwerflowException {
            get {
                return ResourceManager.GetString("OwerflowException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Profile not found.
        /// </summary>
        public static string ProfileNotFound {
            get {
                return ResourceManager.GetString("ProfileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User not found.
        /// </summary>
        public static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}

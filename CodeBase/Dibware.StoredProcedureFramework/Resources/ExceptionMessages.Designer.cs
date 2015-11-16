﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dibware.StoredProcedureFramework.Resources {
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
    public class ExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Dibware.StoredProcedureFramework.Resources.ExceptionMessages", typeof(ExceptionMessages).Assembly);
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
        ///   Looks up a localized string similar to Error reading from target stored procedure &apos;{0}&apos; :  &apos;{1}&apos;.
        /// </summary>
        public static string ErrorReadingStoredProcedure {
            get {
                return ResourceManager.GetString("ErrorReadingStoredProcedure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The ResultSet returned from the stored procedure does not contain a field of the name &apos;{0}&apos; found in return type &apos;{1}&apos;.
        /// </summary>
        public static string FieldNotFoundForName {
            get {
                return ResourceManager.GetString("FieldNotFoundForName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field &apos;{0}&apos; had an expected non-nullable type of &apos;{1}&apos; but DBNull.Value was returned for a nullable type of &apos;{2}&apos;.
        /// </summary>
        public static string FieldNotNullableTypeFormat {
            get {
                return ResourceManager.GetString("FieldNotNullableTypeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wrong return data type for field {0} in return type {1}.
        /// </summary>
        public static string IncorrectReturnType {
            get {
                return ResourceManager.GetString("IncorrectReturnType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;{0}&apos; had an expected type of &apos;{1}&apos;, but actual type was &apos;{2}&apos;. .
        /// </summary>
        public static string ParameterInvalidTypeFormat {
            get {
                return ResourceManager.GetString("ParameterInvalidTypeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;{0}&apos; had an expected length of &apos;{1}&apos;, actual length was &apos;{2}&apos;. .
        /// </summary>
        public static string ParameterLengthOutOfRangeFormat {
            get {
                return ResourceManager.GetString("ParameterLengthOutOfRangeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;{0}&apos; had an expected precision of &apos;{1}&apos;, but actual precision was &apos;{3}&apos;. Expected scales was &apos;{2}&apos;, but actual scale was &apos;{4}&apos;..
        /// </summary>
        public static string ParameterPrecisionAndScaleOutOfRangeFormat {
            get {
                return ResourceManager.GetString("ParameterPrecisionAndScaleOutOfRangeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception processing return column {0} in {1}.
        /// </summary>
        public static string ProcessingReturnColumnError {
            get {
                return ResourceManager.GetString("ProcessingReturnColumnError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The RecordSet list property &apos;{0}&apos; in ResultSet object &apos;{1}&apos; was not instantiated!.
        /// </summary>
        public static string RecordSetListNotInstatiated {
            get {
                return ResourceManager.GetString("RecordSetListNotInstatiated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stored procedure does not havea name set!.
        /// </summary>
        public static string StoredProcedureDoesNotHaveName {
            get {
                return ResourceManager.GetString("StoredProcedureDoesNotHaveName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stored procedure is not fully constructed! Ensure stored procedure has name and return type. .
        /// </summary>
        public static string StoredProcedureIsNotFullyConstructed {
            get {
                return ResourceManager.GetString("StoredProcedureIsNotFullyConstructed", resourceCulture);
            }
        }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pomelo.EntityFrameworkCore.MySql {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    强类型资源类，用于查找本地化字符串，等等。
    /// </summary>
    // 此类已由 StronglyTypedResourceBuilder 自动生成
    // 通过 ResGen 或 Visual Studio 之类的工具提供的类。
    // 若要添加或删除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (使用 /str 选项)，或重新生成 VS 项目。
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal Strings() {
        }
        
        /// <summary>
        ///    返回此类使用的缓存 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Pomelo.EntityFrameworkCore.MySql.Strings", typeof(Strings).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    重写所有项的当前线程的 CurrentUICulture 属性
        ///    使用此强类型资源类进行资源查找。
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
        ///    查找与 Identity value generation cannot be used for the property &apos;{property}&apos; on entity type &apos;{entityType}&apos; because the property type is &apos;{propertyType}&apos;. Identity value generation can only be used with signed integer properties. 类似的本地化字符串。
        /// </summary>
        public static string IdentityBadType {
            get {
                return ResourceManager.GetString("IdentityBadType", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The value provided for argument &apos;{argumentName}&apos; must be a valid value of enum type &apos;{enumType}&apos;. 类似的本地化字符串。
        /// </summary>
        public static string InvalidEnumValue {
            get {
                return ResourceManager.GetString("InvalidEnumValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The increment value of &apos;{increment}&apos; for sequence &apos;{sequenceName}&apos; cannot be used for value generation. Sequences used for value generation must have positive increments. 类似的本地化字符串。
        /// </summary>
        public static string SequenceBadBlockSize {
            get {
                return ResourceManager.GetString("SequenceBadBlockSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 SQL Server sequences cannot be used to generate values for the property &apos;{property}&apos; on entity type &apos;{entityType}&apos; because the property type is &apos;{propertyType}&apos;. Sequences can only be used with integer properties. 类似的本地化字符串。
        /// </summary>
        public static string SequenceBadType {
            get {
                return ResourceManager.GetString("SequenceBadType", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 SQL Server-specific methods can only be used when the context is using a SQL Server data store. 类似的本地化字符串。
        /// </summary>
        public static string SqlServerNotInUse {
            get {
                return ResourceManager.GetString("SqlServerNotInUse", resourceCulture);
            }
        }
    }
}

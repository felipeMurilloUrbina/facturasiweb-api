﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Facturasiweb.Factura.TimbradoDLL.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.6.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://facturacion.finkok.com/servicios/soap/utilities")]
        public string Facturasiweb_Factura_TimbradoDLL_Utilerias_UtilitiesSOAP {
            get {
                return ((string)(this["Facturasiweb_Factura_TimbradoDLL_Utilerias_UtilitiesSOAP"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://facturacion.finkok.com/servicios/soap/stamp")]
        public string Facturasiweb_Factura_TimbradoDLL_Facturacion_StampSOAP {
            get {
                return ((string)(this["Facturasiweb_Factura_TimbradoDLL_Facturacion_StampSOAP"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://facturacion.finkok.com/servicios/soap/cancel")]
        public string Facturasiweb_Factura_TimbradoDLL_Cancelacion_CancelSOAP {
            get {
                return ((string)(this["Facturasiweb_Factura_TimbradoDLL_Cancelacion_CancelSOAP"]));
            }
        }
    }
}

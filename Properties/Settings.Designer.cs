﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LukMachine.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int p1Max {
            get {
                return ((int)(this["p1Max"]));
            }
            set {
                this["p1Max"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double pen1Max {
            get {
                return ((double)(this["pen1Max"]));
            }
            set {
                this["pen1Max"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double pen2Max {
            get {
                return ((double)(this["pen2Max"]));
            }
            set {
                this["pen2Max"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double PressureConversionFactor {
            get {
                return ((double)(this["PressureConversionFactor"]));
            }
            set {
                this["PressureConversionFactor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Demo")]
        public string COMM {
            get {
                return ((string)(this["COMM"]));
            }
            set {
                this["COMM"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2000")]
        public double pen1MinCounts {
            get {
                return ((double)(this["pen1MinCounts"]));
            }
            set {
                this["pen1MinCounts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("62000")]
        public double pen1MaxCounts {
            get {
                return ((double)(this["pen1MaxCounts"]));
            }
            set {
                this["pen1MaxCounts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2000")]
        public double pen2MinCounts {
            get {
                return ((double)(this["pen2MinCounts"]));
            }
            set {
                this["pen2MinCounts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("62000")]
        public double pen2MaxCounts {
            get {
                return ((double)(this["pen2MaxCounts"]));
            }
            set {
                this["pen2MaxCounts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("PSI")]
        public string currentPressureUnit {
            get {
                return ((string)(this["currentPressureUnit"]));
            }
            set {
                this["currentPressureUnit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double currentPressureConversion {
            get {
                return ((double)(this["currentPressureConversion"]));
            }
            set {
                this["currentPressureConversion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>PSI:1</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection pressureUnits {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["pressureUnits"]));
            }
            set {
                this["pressureUnits"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>Water:1</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection fluids {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["fluids"]));
            }
            set {
                this["fluids"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("PSI")]
        public string defaultPressureUnit {
            get {
                return ((string)(this["defaultPressureUnit"]));
            }
            set {
                this["defaultPressureUnit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool mustRunReport {
            get {
                return ((bool)(this["mustRunReport"]));
            }
            set {
                this["mustRunReport"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mySample1")]
        public string TestSampleID {
            get {
                return ((string)(this["TestSampleID"]));
            }
            set {
                this["TestSampleID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TestLotNumber {
            get {
                return ((string)(this["TestLotNumber"]));
            }
            set {
                this["TestLotNumber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int TestMaximumPressure {
            get {
                return ((int)(this["TestMaximumPressure"]));
            }
            set {
                this["TestMaximumPressure"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("900")]
        public int maxPressure {
            get {
                return ((int)(this["maxPressure"]));
            }
            set {
                this["maxPressure"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double TestRate {
            get {
                return ((double)(this["TestRate"]));
            }
            set {
                this["TestRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int TestDetection {
            get {
                return ((int)(this["TestDetection"]));
            }
            set {
                this["TestDetection"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\data.pmi")]
        public string TestData {
            get {
                return ((string)(this["TestData"]));
            }
            set {
                this["TestData"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool autoReport {
            get {
                return ((bool)(this["autoReport"]));
            }
            set {
                this["autoReport"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool paper {
            get {
                return ((bool)(this["paper"]));
            }
            set {
                this["paper"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int paperSheets {
            get {
                return ((int)(this["paperSheets"]));
            }
            set {
                this["paperSheets"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double grammage {
            get {
                return ((double)(this["grammage"]));
            }
            set {
                this["grammage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoDataFile {
            get {
                return ((bool)(this["AutoDataFile"]));
            }
            set {
                this["AutoDataFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoSampleID {
            get {
                return ((bool)(this["AutoSampleID"]));
            }
            set {
                this["AutoSampleID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool promptSafety {
            get {
                return ((bool)(this["promptSafety"]));
            }
            set {
                this["promptSafety"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double defaultPressureConversion {
            get {
                return ((double)(this["defaultPressureConversion"]));
            }
            set {
                this["defaultPressureConversion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SubtractExpansion {
            get {
                return ((bool)(this["SubtractExpansion"]));
            }
            set {
                this["SubtractExpansion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double ExpansionPressure {
            get {
                return ((double)(this["ExpansionPressure"]));
            }
            set {
                this["ExpansionPressure"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public double MaxPumpVolume {
            get {
                return ((double)(this["MaxPumpVolume"]));
            }
            set {
                this["MaxPumpVolume"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int ExportOption {
            get {
                return ((int)(this["ExportOption"]));
            }
            set {
                this["ExportOption"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ExportPath {
            get {
                return ((string)(this["ExportPath"]));
            }
            set {
                this["ExportPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Chamber {
            get {
                return ((string)(this["Chamber"]));
            }
            set {
                this["Chamber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SelectedFlowRate {
            get {
                return ((string)(this["SelectedFlowRate"]));
            }
            set {
                this["SelectedFlowRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int selectedTemp {
            get {
                return ((int)(this["selectedTemp"]));
            }
            set {
                this["selectedTemp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool useTemperature {
            get {
                return ((bool)(this["useTemperature"]));
            }
            set {
                this["useTemperature"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public string LowPumpSetting {
            get {
                return ((string)(this["LowPumpSetting"]));
            }
            set {
                this["LowPumpSetting"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public string MediumPumpSetting {
            get {
                return ((string)(this["MediumPumpSetting"]));
            }
            set {
                this["MediumPumpSetting"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public string HighPumpSetting {
            get {
                return ((string)(this["HighPumpSetting"]));
            }
            set {
                this["HighPumpSetting"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60000")]
        public double twoVolt {
            get {
                return ((double)(this["twoVolt"]));
            }
            set {
                this["twoVolt"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2000")]
        public double ground {
            get {
                return ((double)(this["ground"]));
            }
            set {
                this["ground"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("99")]
        public int MaxPent3PercentFull {
            get {
                return ((int)(this["MaxPent3PercentFull"]));
            }
            set {
                this["MaxPent3PercentFull"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60000")]
        public int MaxReservoirCount {
            get {
                return ((int)(this["MaxReservoirCount"]));
            }
            set {
                this["MaxReservoirCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60000")]
        public int MaxCollectedCount {
            get {
                return ((int)(this["MaxCollectedCount"]));
            }
            set {
                this["MaxCollectedCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int MinReservoirCount {
            get {
                return ((int)(this["MinReservoirCount"]));
            }
            set {
                this["MinReservoirCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int MinCollectedCount {
            get {
                return ((int)(this["MinCollectedCount"]));
            }
            set {
                this["MinCollectedCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int maxEmptyCollectedPercentFull {
            get {
                return ((int)(this["maxEmptyCollectedPercentFull"]));
            }
            set {
                this["maxEmptyCollectedPercentFull"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string flowRate {
            get {
                return ((string)(this["flowRate"]));
            }
            set {
                this["flowRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SelectedPumpSpeed {
            get {
                return ((string)(this["SelectedPumpSpeed"]));
            }
            set {
                this["SelectedPumpSpeed"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve1State {
            get {
                return ((bool)(this["Valve1State"]));
            }
            set {
                this["Valve1State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve2State {
            get {
                return ((bool)(this["Valve2State"]));
            }
            set {
                this["Valve2State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve3State {
            get {
                return ((bool)(this["Valve3State"]));
            }
            set {
                this["Valve3State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve4State {
            get {
                return ((bool)(this["Valve4State"]));
            }
            set {
                this["Valve4State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve5State {
            get {
                return ((bool)(this["Valve5State"]));
            }
            set {
                this["Valve5State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve6State {
            get {
                return ((bool)(this["Valve6State"]));
            }
            set {
                this["Valve6State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve7State {
            get {
                return ((bool)(this["Valve7State"]));
            }
            set {
                this["Valve7State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool RefillPumpState {
            get {
                return ((bool)(this["RefillPumpState"]));
            }
            set {
                this["RefillPumpState"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int MainPumpStatePercent {
            get {
                return ((int)(this["MainPumpStatePercent"]));
            }
            set {
                this["MainPumpStatePercent"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TargetPressure {
            get {
                return ((string)(this["TargetPressure"]));
            }
            set {
                this["TargetPressure"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public string TextboxPressure {
            get {
                return ((string)(this["TextboxPressure"]));
            }
            set {
                this["TextboxPressure"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public string TextboxDuration {
            get {
                return ((string)(this["TextboxDuration"]));
            }
            set {
                this["TextboxDuration"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>0</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection CollectionPressure {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["CollectionPressure"]));
            }
            set {
                this["CollectionPressure"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>0</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection CollectionDuration {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["CollectionDuration"]));
            }
            set {
                this["CollectionDuration"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>0</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection CollectionTemperature {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["CollectionTemperature"]));
            }
            set {
                this["CollectionTemperature"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int StepCount {
            get {
                return ((int)(this["StepCount"]));
            }
            set {
                this["StepCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C")]
        public string TempCorF {
            get {
                return ((string)(this["TempCorF"]));
            }
            set {
                this["TempCorF"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("650")]
        public string MaxCapacityInML {
            get {
                return ((string)(this["MaxCapacityInML"]));
            }
            set {
                this["MaxCapacityInML"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Valve8State {
            get {
                return ((bool)(this["Valve8State"]));
            }
            set {
                this["Valve8State"] = value;
            }
        }
    }
}

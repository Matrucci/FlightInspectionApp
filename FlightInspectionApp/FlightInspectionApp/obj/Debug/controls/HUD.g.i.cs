﻿#pragma checksum "..\..\..\controls\HUD.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "49B51368A49FEB2ED9063C326D007321E4A98ECBFF33709D86ADAB48DA859936"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FlightInspectionApp.controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FlightInspectionApp.controls {
    
    
    /// <summary>
    /// HUD
    /// </summary>
    public partial class HUD : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock direction_deg;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock direction_deg_left;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock direction_deg_right;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock airspeed_top;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock airspeed_mid;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock airspeed;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock altimeter_top;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock altimeter;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\controls\HUD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock altimeter_bot;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FlightInspectionApp;component/controls/hud.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\controls\HUD.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.direction_deg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.direction_deg_left = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.direction_deg_right = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.airspeed_top = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.airspeed_mid = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.airspeed = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.altimeter_top = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.altimeter = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.altimeter_bot = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


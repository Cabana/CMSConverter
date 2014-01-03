﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18052.
// 
#pragma warning disable 1591

namespace CMSConverter.Core.Umbraco6xWebService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="webServiceSoap", Namespace="http://umbraco.org/webservices/")]
    public partial class webService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetNodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetNodeValidateOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDocumentOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMediaOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMediaValidateOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDocumentValidateOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDocumentsBySearchValidateOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDocumentsBySearchOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public webService() {
            this.Url = global::CMSConverter.Core.Properties.Settings.Default.Core_Umbraco6xWebService_webService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetNodeCompletedEventHandler GetNodeCompleted;
        
        /// <remarks/>
        public event GetNodeValidateCompletedEventHandler GetNodeValidateCompleted;
        
        /// <remarks/>
        public event GetDocumentCompletedEventHandler GetDocumentCompleted;
        
        /// <remarks/>
        public event GetMediaCompletedEventHandler GetMediaCompleted;
        
        /// <remarks/>
        public event GetMediaValidateCompletedEventHandler GetMediaValidateCompleted;
        
        /// <remarks/>
        public event GetDocumentValidateCompletedEventHandler GetDocumentValidateCompleted;
        
        /// <remarks/>
        public event GetDocumentsBySearchValidateCompletedEventHandler GetDocumentsBySearchValidateCompleted;
        
        /// <remarks/>
        public event GetDocumentsBySearchCompletedEventHandler GetDocumentsBySearchCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetNode", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetNode(int NodeId, string ContextID) {
            object[] results = this.Invoke("GetNode", new object[] {
                        NodeId,
                        ContextID});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetNodeAsync(int NodeId, string ContextID) {
            this.GetNodeAsync(NodeId, ContextID, null);
        }
        
        /// <remarks/>
        public void GetNodeAsync(int NodeId, string ContextID, object userState) {
            if ((this.GetNodeOperationCompleted == null)) {
                this.GetNodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNodeOperationCompleted);
            }
            this.InvokeAsync("GetNode", new object[] {
                        NodeId,
                        ContextID}, this.GetNodeOperationCompleted, userState);
        }
        
        private void OnGetNodeOperationCompleted(object arg) {
            if ((this.GetNodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNodeCompleted(this, new GetNodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetNodeValidate", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetNodeValidate(int NodeId, string Login, string Password) {
            object[] results = this.Invoke("GetNodeValidate", new object[] {
                        NodeId,
                        Login,
                        Password});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetNodeValidateAsync(int NodeId, string Login, string Password) {
            this.GetNodeValidateAsync(NodeId, Login, Password, null);
        }
        
        /// <remarks/>
        public void GetNodeValidateAsync(int NodeId, string Login, string Password, object userState) {
            if ((this.GetNodeValidateOperationCompleted == null)) {
                this.GetNodeValidateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNodeValidateOperationCompleted);
            }
            this.InvokeAsync("GetNodeValidate", new object[] {
                        NodeId,
                        Login,
                        Password}, this.GetNodeValidateOperationCompleted, userState);
        }
        
        private void OnGetNodeValidateOperationCompleted(object arg) {
            if ((this.GetNodeValidateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNodeValidateCompleted(this, new GetNodeValidateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetDocument", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetDocument(int NodeId, string ContextID) {
            object[] results = this.Invoke("GetDocument", new object[] {
                        NodeId,
                        ContextID});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetDocumentAsync(int NodeId, string ContextID) {
            this.GetDocumentAsync(NodeId, ContextID, null);
        }
        
        /// <remarks/>
        public void GetDocumentAsync(int NodeId, string ContextID, object userState) {
            if ((this.GetDocumentOperationCompleted == null)) {
                this.GetDocumentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDocumentOperationCompleted);
            }
            this.InvokeAsync("GetDocument", new object[] {
                        NodeId,
                        ContextID}, this.GetDocumentOperationCompleted, userState);
        }
        
        private void OnGetDocumentOperationCompleted(object arg) {
            if ((this.GetDocumentCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDocumentCompleted(this, new GetDocumentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetMedia", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetMedia(int NodeId, string ContextID) {
            object[] results = this.Invoke("GetMedia", new object[] {
                        NodeId,
                        ContextID});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetMediaAsync(int NodeId, string ContextID) {
            this.GetMediaAsync(NodeId, ContextID, null);
        }
        
        /// <remarks/>
        public void GetMediaAsync(int NodeId, string ContextID, object userState) {
            if ((this.GetMediaOperationCompleted == null)) {
                this.GetMediaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMediaOperationCompleted);
            }
            this.InvokeAsync("GetMedia", new object[] {
                        NodeId,
                        ContextID}, this.GetMediaOperationCompleted, userState);
        }
        
        private void OnGetMediaOperationCompleted(object arg) {
            if ((this.GetMediaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMediaCompleted(this, new GetMediaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetMediaValidate", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetMediaValidate(int NodeId, string Login, string Password) {
            object[] results = this.Invoke("GetMediaValidate", new object[] {
                        NodeId,
                        Login,
                        Password});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetMediaValidateAsync(int NodeId, string Login, string Password) {
            this.GetMediaValidateAsync(NodeId, Login, Password, null);
        }
        
        /// <remarks/>
        public void GetMediaValidateAsync(int NodeId, string Login, string Password, object userState) {
            if ((this.GetMediaValidateOperationCompleted == null)) {
                this.GetMediaValidateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMediaValidateOperationCompleted);
            }
            this.InvokeAsync("GetMediaValidate", new object[] {
                        NodeId,
                        Login,
                        Password}, this.GetMediaValidateOperationCompleted, userState);
        }
        
        private void OnGetMediaValidateOperationCompleted(object arg) {
            if ((this.GetMediaValidateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMediaValidateCompleted(this, new GetMediaValidateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetDocumentValidate", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetDocumentValidate(int NodeId, string Login, string Password) {
            object[] results = this.Invoke("GetDocumentValidate", new object[] {
                        NodeId,
                        Login,
                        Password});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetDocumentValidateAsync(int NodeId, string Login, string Password) {
            this.GetDocumentValidateAsync(NodeId, Login, Password, null);
        }
        
        /// <remarks/>
        public void GetDocumentValidateAsync(int NodeId, string Login, string Password, object userState) {
            if ((this.GetDocumentValidateOperationCompleted == null)) {
                this.GetDocumentValidateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDocumentValidateOperationCompleted);
            }
            this.InvokeAsync("GetDocumentValidate", new object[] {
                        NodeId,
                        Login,
                        Password}, this.GetDocumentValidateOperationCompleted, userState);
        }
        
        private void OnGetDocumentValidateOperationCompleted(object arg) {
            if ((this.GetDocumentValidateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDocumentValidateCompleted(this, new GetDocumentValidateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetDocumentsBySearchValidate", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetDocumentsBySearchValidate(string Query, int StartNodeId, string Login, string Password) {
            object[] results = this.Invoke("GetDocumentsBySearchValidate", new object[] {
                        Query,
                        StartNodeId,
                        Login,
                        Password});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetDocumentsBySearchValidateAsync(string Query, int StartNodeId, string Login, string Password) {
            this.GetDocumentsBySearchValidateAsync(Query, StartNodeId, Login, Password, null);
        }
        
        /// <remarks/>
        public void GetDocumentsBySearchValidateAsync(string Query, int StartNodeId, string Login, string Password, object userState) {
            if ((this.GetDocumentsBySearchValidateOperationCompleted == null)) {
                this.GetDocumentsBySearchValidateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDocumentsBySearchValidateOperationCompleted);
            }
            this.InvokeAsync("GetDocumentsBySearchValidate", new object[] {
                        Query,
                        StartNodeId,
                        Login,
                        Password}, this.GetDocumentsBySearchValidateOperationCompleted, userState);
        }
        
        private void OnGetDocumentsBySearchValidateOperationCompleted(object arg) {
            if ((this.GetDocumentsBySearchValidateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDocumentsBySearchValidateCompleted(this, new GetDocumentsBySearchValidateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://umbraco.org/webservices/GetDocumentsBySearch", RequestNamespace="http://umbraco.org/webservices/", ResponseNamespace="http://umbraco.org/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetDocumentsBySearch(string Query, int StartNodeId, string ContextID) {
            object[] results = this.Invoke("GetDocumentsBySearch", new object[] {
                        Query,
                        StartNodeId,
                        ContextID});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetDocumentsBySearchAsync(string Query, int StartNodeId, string ContextID) {
            this.GetDocumentsBySearchAsync(Query, StartNodeId, ContextID, null);
        }
        
        /// <remarks/>
        public void GetDocumentsBySearchAsync(string Query, int StartNodeId, string ContextID, object userState) {
            if ((this.GetDocumentsBySearchOperationCompleted == null)) {
                this.GetDocumentsBySearchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDocumentsBySearchOperationCompleted);
            }
            this.InvokeAsync("GetDocumentsBySearch", new object[] {
                        Query,
                        StartNodeId,
                        ContextID}, this.GetDocumentsBySearchOperationCompleted, userState);
        }
        
        private void OnGetDocumentsBySearchOperationCompleted(object arg) {
            if ((this.GetDocumentsBySearchCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDocumentsBySearchCompleted(this, new GetDocumentsBySearchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetNodeCompletedEventHandler(object sender, GetNodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetNodeValidateCompletedEventHandler(object sender, GetNodeValidateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNodeValidateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNodeValidateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetDocumentCompletedEventHandler(object sender, GetDocumentCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDocumentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDocumentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetMediaCompletedEventHandler(object sender, GetMediaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMediaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMediaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetMediaValidateCompletedEventHandler(object sender, GetMediaValidateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMediaValidateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMediaValidateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetDocumentValidateCompletedEventHandler(object sender, GetDocumentValidateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDocumentValidateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDocumentValidateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetDocumentsBySearchValidateCompletedEventHandler(object sender, GetDocumentsBySearchValidateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDocumentsBySearchValidateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDocumentsBySearchValidateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetDocumentsBySearchCompletedEventHandler(object sender, GetDocumentsBySearchCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDocumentsBySearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDocumentsBySearchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
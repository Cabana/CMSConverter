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

namespace CMSConverter.Core.ExtendedSitecoreAPI {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="SecuritySoap", Namespace="http://tempuri.org/")]
    public partial class Security : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CreateUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateRoleOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRolesOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddRoleToRoleOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteRoleOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteUsersOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetRightOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRightOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAccessRightsOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateStandardValuesOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Security() {
            this.Url = global::CMSConverter.Core.Properties.Settings.Default.Core_sitecore6_Security;
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
        public event CreateUserCompletedEventHandler CreateUserCompleted;
        
        /// <remarks/>
        public event CreateRoleCompletedEventHandler CreateRoleCompleted;
        
        /// <remarks/>
        public event GetRolesCompletedEventHandler GetRolesCompleted;
        
        /// <remarks/>
        public event AddRoleToRoleCompletedEventHandler AddRoleToRoleCompleted;
        
        /// <remarks/>
        public event DeleteRoleCompletedEventHandler DeleteRoleCompleted;
        
        /// <remarks/>
        public event DeleteUsersCompletedEventHandler DeleteUsersCompleted;
        
        /// <remarks/>
        public event SetRightCompletedEventHandler SetRightCompleted;
        
        /// <remarks/>
        public event GetRightCompletedEventHandler GetRightCompleted;
        
        /// <remarks/>
        public event GetAccessRightsCompletedEventHandler GetAccessRightsCompleted;
        
        /// <remarks/>
        public event CreateStandardValuesCompletedEventHandler CreateStandardValuesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CreateUser(string sDomainUser, string sPassword, string sEmail, string sFullName, string sRoles, bool bIsAdmin, Credentials credentials) {
            object[] results = this.Invoke("CreateUser", new object[] {
                        sDomainUser,
                        sPassword,
                        sEmail,
                        sFullName,
                        sRoles,
                        bIsAdmin,
                        credentials});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CreateUserAsync(string sDomainUser, string sPassword, string sEmail, string sFullName, string sRoles, bool bIsAdmin, Credentials credentials) {
            this.CreateUserAsync(sDomainUser, sPassword, sEmail, sFullName, sRoles, bIsAdmin, credentials, null);
        }
        
        /// <remarks/>
        public void CreateUserAsync(string sDomainUser, string sPassword, string sEmail, string sFullName, string sRoles, bool bIsAdmin, Credentials credentials, object userState) {
            if ((this.CreateUserOperationCompleted == null)) {
                this.CreateUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateUserOperationCompleted);
            }
            this.InvokeAsync("CreateUser", new object[] {
                        sDomainUser,
                        sPassword,
                        sEmail,
                        sFullName,
                        sRoles,
                        bIsAdmin,
                        credentials}, this.CreateUserOperationCompleted, userState);
        }
        
        private void OnCreateUserOperationCompleted(object arg) {
            if ((this.CreateUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateUserCompleted(this, new CreateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateRole", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CreateRole(string sDomainRole, Credentials credentials) {
            object[] results = this.Invoke("CreateRole", new object[] {
                        sDomainRole,
                        credentials});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CreateRoleAsync(string sDomainRole, Credentials credentials) {
            this.CreateRoleAsync(sDomainRole, credentials, null);
        }
        
        /// <remarks/>
        public void CreateRoleAsync(string sDomainRole, Credentials credentials, object userState) {
            if ((this.CreateRoleOperationCompleted == null)) {
                this.CreateRoleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateRoleOperationCompleted);
            }
            this.InvokeAsync("CreateRole", new object[] {
                        sDomainRole,
                        credentials}, this.CreateRoleOperationCompleted, userState);
        }
        
        private void OnCreateRoleOperationCompleted(object arg) {
            if ((this.CreateRoleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateRoleCompleted(this, new CreateRoleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetRoles", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] GetRoles(Credentials credentials) {
            object[] results = this.Invoke("GetRoles", new object[] {
                        credentials});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void GetRolesAsync(Credentials credentials) {
            this.GetRolesAsync(credentials, null);
        }
        
        /// <remarks/>
        public void GetRolesAsync(Credentials credentials, object userState) {
            if ((this.GetRolesOperationCompleted == null)) {
                this.GetRolesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRolesOperationCompleted);
            }
            this.InvokeAsync("GetRoles", new object[] {
                        credentials}, this.GetRolesOperationCompleted, userState);
        }
        
        private void OnGetRolesOperationCompleted(object arg) {
            if ((this.GetRolesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRolesCompleted(this, new GetRolesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddRoleToRole", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddRoleToRole(string sDomainRole, string sTargetRole, Credentials credentials) {
            object[] results = this.Invoke("AddRoleToRole", new object[] {
                        sDomainRole,
                        sTargetRole,
                        credentials});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddRoleToRoleAsync(string sDomainRole, string sTargetRole, Credentials credentials) {
            this.AddRoleToRoleAsync(sDomainRole, sTargetRole, credentials, null);
        }
        
        /// <remarks/>
        public void AddRoleToRoleAsync(string sDomainRole, string sTargetRole, Credentials credentials, object userState) {
            if ((this.AddRoleToRoleOperationCompleted == null)) {
                this.AddRoleToRoleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddRoleToRoleOperationCompleted);
            }
            this.InvokeAsync("AddRoleToRole", new object[] {
                        sDomainRole,
                        sTargetRole,
                        credentials}, this.AddRoleToRoleOperationCompleted, userState);
        }
        
        private void OnAddRoleToRoleOperationCompleted(object arg) {
            if ((this.AddRoleToRoleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddRoleToRoleCompleted(this, new AddRoleToRoleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteRole", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteRole(string sDomainRole, Credentials credentials) {
            object[] results = this.Invoke("DeleteRole", new object[] {
                        sDomainRole,
                        credentials});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteRoleAsync(string sDomainRole, Credentials credentials) {
            this.DeleteRoleAsync(sDomainRole, credentials, null);
        }
        
        /// <remarks/>
        public void DeleteRoleAsync(string sDomainRole, Credentials credentials, object userState) {
            if ((this.DeleteRoleOperationCompleted == null)) {
                this.DeleteRoleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteRoleOperationCompleted);
            }
            this.InvokeAsync("DeleteRole", new object[] {
                        sDomainRole,
                        credentials}, this.DeleteRoleOperationCompleted, userState);
        }
        
        private void OnDeleteRoleOperationCompleted(object arg) {
            if ((this.DeleteRoleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteRoleCompleted(this, new DeleteRoleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteUsers", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteUsers(string sDomainUsers, Credentials credentials) {
            object[] results = this.Invoke("DeleteUsers", new object[] {
                        sDomainUsers,
                        credentials});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteUsersAsync(string sDomainUsers, Credentials credentials) {
            this.DeleteUsersAsync(sDomainUsers, credentials, null);
        }
        
        /// <remarks/>
        public void DeleteUsersAsync(string sDomainUsers, Credentials credentials, object userState) {
            if ((this.DeleteUsersOperationCompleted == null)) {
                this.DeleteUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteUsersOperationCompleted);
            }
            this.InvokeAsync("DeleteUsers", new object[] {
                        sDomainUsers,
                        credentials}, this.DeleteUsersOperationCompleted, userState);
        }
        
        private void OnDeleteUsersOperationCompleted(object arg) {
            if ((this.DeleteUsersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteUsersCompleted(this, new DeleteUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SetRight", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SetRight(string strDatabase, string strItem, string strAccount, string strRights, AccessPermission rightState, PropagationType propagationType, Credentials credentials) {
            this.Invoke("SetRight", new object[] {
                        strDatabase,
                        strItem,
                        strAccount,
                        strRights,
                        rightState,
                        propagationType,
                        credentials});
        }
        
        /// <remarks/>
        public void SetRightAsync(string strDatabase, string strItem, string strAccount, string strRights, AccessPermission rightState, PropagationType propagationType, Credentials credentials) {
            this.SetRightAsync(strDatabase, strItem, strAccount, strRights, rightState, propagationType, credentials, null);
        }
        
        /// <remarks/>
        public void SetRightAsync(string strDatabase, string strItem, string strAccount, string strRights, AccessPermission rightState, PropagationType propagationType, Credentials credentials, object userState) {
            if ((this.SetRightOperationCompleted == null)) {
                this.SetRightOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetRightOperationCompleted);
            }
            this.InvokeAsync("SetRight", new object[] {
                        strDatabase,
                        strItem,
                        strAccount,
                        strRights,
                        rightState,
                        propagationType,
                        credentials}, this.SetRightOperationCompleted, userState);
        }
        
        private void OnSetRightOperationCompleted(object arg) {
            if ((this.SetRightCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetRightCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetRight", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetRight(string strDatabase, string strItem, string strAccount, SecurityPermission rightState, Credentials credentials) {
            object[] results = this.Invoke("GetRight", new object[] {
                        strDatabase,
                        strItem,
                        strAccount,
                        rightState,
                        credentials});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetRightAsync(string strDatabase, string strItem, string strAccount, SecurityPermission rightState, Credentials credentials) {
            this.GetRightAsync(strDatabase, strItem, strAccount, rightState, credentials, null);
        }
        
        /// <remarks/>
        public void GetRightAsync(string strDatabase, string strItem, string strAccount, SecurityPermission rightState, Credentials credentials, object userState) {
            if ((this.GetRightOperationCompleted == null)) {
                this.GetRightOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRightOperationCompleted);
            }
            this.InvokeAsync("GetRight", new object[] {
                        strDatabase,
                        strItem,
                        strAccount,
                        rightState,
                        credentials}, this.GetRightOperationCompleted, userState);
        }
        
        private void OnGetRightOperationCompleted(object arg) {
            if ((this.GetRightCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRightCompleted(this, new GetRightCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAccessRights", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetAccessRights() {
            object[] results = this.Invoke("GetAccessRights", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetAccessRightsAsync() {
            this.GetAccessRightsAsync(null);
        }
        
        /// <remarks/>
        public void GetAccessRightsAsync(object userState) {
            if ((this.GetAccessRightsOperationCompleted == null)) {
                this.GetAccessRightsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAccessRightsOperationCompleted);
            }
            this.InvokeAsync("GetAccessRights", new object[0], this.GetAccessRightsOperationCompleted, userState);
        }
        
        private void OnGetAccessRightsOperationCompleted(object arg) {
            if ((this.GetAccessRightsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAccessRightsCompleted(this, new GetAccessRightsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateStandardValues", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateStandardValues(string strItem, Credentials credentials) {
            object[] results = this.Invoke("CreateStandardValues", new object[] {
                        strItem,
                        credentials});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CreateStandardValuesAsync(string strItem, Credentials credentials) {
            this.CreateStandardValuesAsync(strItem, credentials, null);
        }
        
        /// <remarks/>
        public void CreateStandardValuesAsync(string strItem, Credentials credentials, object userState) {
            if ((this.CreateStandardValuesOperationCompleted == null)) {
                this.CreateStandardValuesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateStandardValuesOperationCompleted);
            }
            this.InvokeAsync("CreateStandardValues", new object[] {
                        strItem,
                        credentials}, this.CreateStandardValuesOperationCompleted, userState);
        }
        
        private void OnCreateStandardValuesOperationCompleted(object arg) {
            if ((this.CreateStandardValuesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateStandardValuesCompleted(this, new CreateStandardValuesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Credentials {
        
        private string customDataField;
        
        private string passwordField;
        
        private string userNameField;
        
        /// <remarks/>
        public string CustomData {
            get {
                return this.customDataField;
            }
            set {
                this.customDataField = value;
            }
        }
        
        /// <remarks/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum AccessPermission {
        
        /// <remarks/>
        NotSet,
        
        /// <remarks/>
        Allow,
        
        /// <remarks/>
        Deny,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum PropagationType {
        
        /// <remarks/>
        Unknown,
        
        /// <remarks/>
        Descendants,
        
        /// <remarks/>
        Entity,
        
        /// <remarks/>
        Any,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum SecurityPermission {
        
        /// <remarks/>
        NotSet,
        
        /// <remarks/>
        AllowAccess,
        
        /// <remarks/>
        DenyAccess,
        
        /// <remarks/>
        AllowInheritance,
        
        /// <remarks/>
        DenyInheritance,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void CreateUserCompletedEventHandler(object sender, CreateUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void CreateRoleCompletedEventHandler(object sender, CreateRoleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateRoleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateRoleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetRolesCompletedEventHandler(object sender, GetRolesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRolesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRolesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void AddRoleToRoleCompletedEventHandler(object sender, AddRoleToRoleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddRoleToRoleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddRoleToRoleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void DeleteRoleCompletedEventHandler(object sender, DeleteRoleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteRoleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteRoleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void DeleteUsersCompletedEventHandler(object sender, DeleteUsersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void SetRightCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetRightCompletedEventHandler(object sender, GetRightCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRightCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRightCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetAccessRightsCompletedEventHandler(object sender, GetAccessRightsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAccessRightsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAccessRightsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void CreateStandardValuesCompletedEventHandler(object sender, CreateStandardValuesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateStandardValuesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateStandardValuesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
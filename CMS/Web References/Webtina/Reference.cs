﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace CMS.Webtina {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SupportSoap", Namespace="http://tempuri.org/")]
    public partial class Support : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RespondeTicketOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddTicketOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetTicketAsReadOperationCompleted;
        
        private System.Threading.SendOrPostCallback UnreadTicketsCountOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteTicketOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTicketListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTicketRepliesOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Support() {
            this.Url = global::CMS.Properties.Settings.Default.CMS_Webtina_Support;
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
        public event RespondeTicketCompletedEventHandler RespondeTicketCompleted;
        
        /// <remarks/>
        public event AddTicketCompletedEventHandler AddTicketCompleted;
        
        /// <remarks/>
        public event SetTicketAsReadCompletedEventHandler SetTicketAsReadCompleted;
        
        /// <remarks/>
        public event UnreadTicketsCountCompletedEventHandler UnreadTicketsCountCompleted;
        
        /// <remarks/>
        public event DeleteTicketCompletedEventHandler DeleteTicketCompleted;
        
        /// <remarks/>
        public event GetTicketListCompletedEventHandler GetTicketListCompleted;
        
        /// <remarks/>
        public event GetTicketRepliesCompletedEventHandler GetTicketRepliesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RespondeTicket", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool RespondeTicket(int UserID, int TicketID, string text, string secKey) {
            object[] results = this.Invoke("RespondeTicket", new object[] {
                        UserID,
                        TicketID,
                        text,
                        secKey});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void RespondeTicketAsync(int UserID, int TicketID, string text, string secKey) {
            this.RespondeTicketAsync(UserID, TicketID, text, secKey, null);
        }
        
        /// <remarks/>
        public void RespondeTicketAsync(int UserID, int TicketID, string text, string secKey, object userState) {
            if ((this.RespondeTicketOperationCompleted == null)) {
                this.RespondeTicketOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRespondeTicketOperationCompleted);
            }
            this.InvokeAsync("RespondeTicket", new object[] {
                        UserID,
                        TicketID,
                        text,
                        secKey}, this.RespondeTicketOperationCompleted, userState);
        }
        
        private void OnRespondeTicketOperationCompleted(object arg) {
            if ((this.RespondeTicketCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RespondeTicketCompleted(this, new RespondeTicketCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddTicket", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddTicket(int UserID, string priority, string part, string text, string title, string secKey) {
            object[] results = this.Invoke("AddTicket", new object[] {
                        UserID,
                        priority,
                        part,
                        text,
                        title,
                        secKey});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddTicketAsync(int UserID, string priority, string part, string text, string title, string secKey) {
            this.AddTicketAsync(UserID, priority, part, text, title, secKey, null);
        }
        
        /// <remarks/>
        public void AddTicketAsync(int UserID, string priority, string part, string text, string title, string secKey, object userState) {
            if ((this.AddTicketOperationCompleted == null)) {
                this.AddTicketOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddTicketOperationCompleted);
            }
            this.InvokeAsync("AddTicket", new object[] {
                        UserID,
                        priority,
                        part,
                        text,
                        title,
                        secKey}, this.AddTicketOperationCompleted, userState);
        }
        
        private void OnAddTicketOperationCompleted(object arg) {
            if ((this.AddTicketCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddTicketCompleted(this, new AddTicketCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SetTicketAsRead", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SetTicketAsRead(int UserID, int tickeID, string secKey) {
            this.Invoke("SetTicketAsRead", new object[] {
                        UserID,
                        tickeID,
                        secKey});
        }
        
        /// <remarks/>
        public void SetTicketAsReadAsync(int UserID, int tickeID, string secKey) {
            this.SetTicketAsReadAsync(UserID, tickeID, secKey, null);
        }
        
        /// <remarks/>
        public void SetTicketAsReadAsync(int UserID, int tickeID, string secKey, object userState) {
            if ((this.SetTicketAsReadOperationCompleted == null)) {
                this.SetTicketAsReadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetTicketAsReadOperationCompleted);
            }
            this.InvokeAsync("SetTicketAsRead", new object[] {
                        UserID,
                        tickeID,
                        secKey}, this.SetTicketAsReadOperationCompleted, userState);
        }
        
        private void OnSetTicketAsReadOperationCompleted(object arg) {
            if ((this.SetTicketAsReadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetTicketAsReadCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UnreadTicketsCount", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int UnreadTicketsCount(int userID, string secKey) {
            object[] results = this.Invoke("UnreadTicketsCount", new object[] {
                        userID,
                        secKey});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void UnreadTicketsCountAsync(int userID, string secKey) {
            this.UnreadTicketsCountAsync(userID, secKey, null);
        }
        
        /// <remarks/>
        public void UnreadTicketsCountAsync(int userID, string secKey, object userState) {
            if ((this.UnreadTicketsCountOperationCompleted == null)) {
                this.UnreadTicketsCountOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUnreadTicketsCountOperationCompleted);
            }
            this.InvokeAsync("UnreadTicketsCount", new object[] {
                        userID,
                        secKey}, this.UnreadTicketsCountOperationCompleted, userState);
        }
        
        private void OnUnreadTicketsCountOperationCompleted(object arg) {
            if ((this.UnreadTicketsCountCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UnreadTicketsCountCompleted(this, new UnreadTicketsCountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteTicket", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteTicket(int userID, int tickeID, string secKey) {
            object[] results = this.Invoke("DeleteTicket", new object[] {
                        userID,
                        tickeID,
                        secKey});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteTicketAsync(int userID, int tickeID, string secKey) {
            this.DeleteTicketAsync(userID, tickeID, secKey, null);
        }
        
        /// <remarks/>
        public void DeleteTicketAsync(int userID, int tickeID, string secKey, object userState) {
            if ((this.DeleteTicketOperationCompleted == null)) {
                this.DeleteTicketOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteTicketOperationCompleted);
            }
            this.InvokeAsync("DeleteTicket", new object[] {
                        userID,
                        tickeID,
                        secKey}, this.DeleteTicketOperationCompleted, userState);
        }
        
        private void OnDeleteTicketOperationCompleted(object arg) {
            if ((this.DeleteTicketCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteTicketCompleted(this, new DeleteTicketCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTicketList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TicKetList[] GetTicketList(int userID, int take, int skip, string secKey) {
            object[] results = this.Invoke("GetTicketList", new object[] {
                        userID,
                        take,
                        skip,
                        secKey});
            return ((TicKetList[])(results[0]));
        }
        
        /// <remarks/>
        public void GetTicketListAsync(int userID, int take, int skip, string secKey) {
            this.GetTicketListAsync(userID, take, skip, secKey, null);
        }
        
        /// <remarks/>
        public void GetTicketListAsync(int userID, int take, int skip, string secKey, object userState) {
            if ((this.GetTicketListOperationCompleted == null)) {
                this.GetTicketListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTicketListOperationCompleted);
            }
            this.InvokeAsync("GetTicketList", new object[] {
                        userID,
                        take,
                        skip,
                        secKey}, this.GetTicketListOperationCompleted, userState);
        }
        
        private void OnGetTicketListOperationCompleted(object arg) {
            if ((this.GetTicketListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTicketListCompleted(this, new GetTicketListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTicketReplies", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TicketReplyViewModel GetTicketReplies(int userID, int ticketID, string secKey) {
            object[] results = this.Invoke("GetTicketReplies", new object[] {
                        userID,
                        ticketID,
                        secKey});
            return ((TicketReplyViewModel)(results[0]));
        }
        
        /// <remarks/>
        public void GetTicketRepliesAsync(int userID, int ticketID, string secKey) {
            this.GetTicketRepliesAsync(userID, ticketID, secKey, null);
        }
        
        /// <remarks/>
        public void GetTicketRepliesAsync(int userID, int ticketID, string secKey, object userState) {
            if ((this.GetTicketRepliesOperationCompleted == null)) {
                this.GetTicketRepliesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTicketRepliesOperationCompleted);
            }
            this.InvokeAsync("GetTicketReplies", new object[] {
                        userID,
                        ticketID,
                        secKey}, this.GetTicketRepliesOperationCompleted, userState);
        }
        
        private void OnGetTicketRepliesOperationCompleted(object arg) {
            if ((this.GetTicketRepliesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTicketRepliesCompleted(this, new GetTicketRepliesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class TicKetList {
        
        private int idField;
        
        private string titleField;
        
        private string partField;
        
        private string lastUpdateField;
        
        private string statusField;
        
        private bool isNewField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public string Part {
            get {
                return this.partField;
            }
            set {
                this.partField = value;
            }
        }
        
        /// <remarks/>
        public string LastUpdate {
            get {
                return this.lastUpdateField;
            }
            set {
                this.lastUpdateField = value;
            }
        }
        
        /// <remarks/>
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public bool IsNew {
            get {
                return this.isNewField;
            }
            set {
                this.isNewField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Replies {
        
        private int idField;
        
        private string sendDateField;
        
        private string textField;
        
        private string fileNameField;
        
        private bool isManageReplyField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string SendDate {
            get {
                return this.sendDateField;
            }
            set {
                this.sendDateField = value;
            }
        }
        
        /// <remarks/>
        public string Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
        
        /// <remarks/>
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
            }
        }
        
        /// <remarks/>
        public bool IsManageReply {
            get {
                return this.isManageReplyField;
            }
            set {
                this.isManageReplyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class TicketReplyViewModel {
        
        private Replies[] replyListField;
        
        private string ticketTitleField;
        
        private string ticketStatusField;
        
        private string ticketLastUpdateField;
        
        private string ticketPartField;
        
        /// <remarks/>
        public Replies[] ReplyList {
            get {
                return this.replyListField;
            }
            set {
                this.replyListField = value;
            }
        }
        
        /// <remarks/>
        public string TicketTitle {
            get {
                return this.ticketTitleField;
            }
            set {
                this.ticketTitleField = value;
            }
        }
        
        /// <remarks/>
        public string TicketStatus {
            get {
                return this.ticketStatusField;
            }
            set {
                this.ticketStatusField = value;
            }
        }
        
        /// <remarks/>
        public string TicketLastUpdate {
            get {
                return this.ticketLastUpdateField;
            }
            set {
                this.ticketLastUpdateField = value;
            }
        }
        
        /// <remarks/>
        public string TicketPart {
            get {
                return this.ticketPartField;
            }
            set {
                this.ticketPartField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void RespondeTicketCompletedEventHandler(object sender, RespondeTicketCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RespondeTicketCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RespondeTicketCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void AddTicketCompletedEventHandler(object sender, AddTicketCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddTicketCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddTicketCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void SetTicketAsReadCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UnreadTicketsCountCompletedEventHandler(object sender, UnreadTicketsCountCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UnreadTicketsCountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UnreadTicketsCountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void DeleteTicketCompletedEventHandler(object sender, DeleteTicketCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteTicketCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteTicketCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void GetTicketListCompletedEventHandler(object sender, GetTicketListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTicketListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTicketListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TicKetList[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TicKetList[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void GetTicketRepliesCompletedEventHandler(object sender, GetTicketRepliesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTicketRepliesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTicketRepliesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TicketReplyViewModel Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TicketReplyViewModel)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
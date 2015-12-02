﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Osciloskopas.ChatService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChatService.ISendChatService", CallbackContract=typeof(Osciloskopas.ChatService.ISendChatServiceCallback))]
    public interface ISendChatService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/SendMessage")]
        void SendMessage(string msg, string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string msg, string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/Start")]
        void Start(string Name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/Start")]
        System.Threading.Tasks.Task StartAsync(string Name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/Stop")]
        void Stop(string Name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/Stop")]
        System.Threading.Tasks.Task StopAsync(string Name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISendChatServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/ReceiveMessage")]
        void ReceiveMessage(string msg, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ISendChatService/SendNames")]
        void SendNames(string[] names);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISendChatServiceChannel : Osciloskopas.ChatService.ISendChatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SendChatServiceClient : System.ServiceModel.DuplexClientBase<Osciloskopas.ChatService.ISendChatService>, Osciloskopas.ChatService.ISendChatService {
        
        public SendChatServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public SendChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public SendChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SendChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SendChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SendMessage(string msg, string sender, string receiver) {
            base.Channel.SendMessage(msg, sender, receiver);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string msg, string sender, string receiver) {
            return base.Channel.SendMessageAsync(msg, sender, receiver);
        }
        
        public void Start(string Name) {
            base.Channel.Start(Name);
        }
        
        public System.Threading.Tasks.Task StartAsync(string Name) {
            return base.Channel.StartAsync(Name);
        }
        
        public void Stop(string Name) {
            base.Channel.Stop(Name);
        }
        
        public System.Threading.Tasks.Task StopAsync(string Name) {
            return base.Channel.StopAsync(Name);
        }
    }
}
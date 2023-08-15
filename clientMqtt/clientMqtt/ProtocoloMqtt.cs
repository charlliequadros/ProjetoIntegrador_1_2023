using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace clientMqtt
{
    public class ProtocoloMqtt
    {
        IManagedMqttClient _mqttClient;
        ManagedMqttClientOptions managedOptions;
        public static event EventHandler RecebeMensagemMqtt;

        public static void RecebeMensagemEvento(string mensagem)
        {
            RecebeMensagemMqtt?.Invoke(null, new EventoCustomizado(mensagem));
        }

        string clientId = Guid.NewGuid().ToString();
        string mqttURI = "jackal.rmq.cloudamqp.com";
        string mqttUser = "gnkpnwbp:gnkpnwbp";
        string mqttPassword = "q7FJQym6iO7bA1c_wt-bKqSfi55spSKM";
        int mqttPort = 1883;
        bool mqttSecure = false;

        public ProtocoloMqtt()
        {
            inicializarMqtt();
        }

         void inicializarMqtt()
        {
            _mqttClient = new MqttFactory().CreateManagedMqttClient();

            // Create client options object
            var messageBuilder = new MqttClientOptionsBuilder()
                    .WithClientId(clientId)
                     .WithCredentials(mqttUser, mqttPassword)
                    .WithTcpServer(mqttURI, mqttPort)
                    .WithCleanSession();

            var options = mqttSecure
              ? messageBuilder
                .WithTls()
                .Build()
              : messageBuilder
                .Build();

            managedOptions = new ManagedMqttClientOptionsBuilder()
             .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
             .WithClientOptions(options)
             .Build();

            _mqttClient = new MqttFactory().CreateManagedMqttClient();

            _mqttClient.ApplicationMessageReceivedAsync += _mqttClient_ApplicationMessageReceivedAsync;
        }

        private Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            var mensagem = arg.ApplicationMessage.Payload;
            string utfString = Encoding.UTF8.GetString(mensagem, 0, mensagem.Length);
            RecebeMensagemEvento(utfString);
            return Task.CompletedTask;
        }

        public void fecharConexao()
        {
            _mqttClient.StopAsync();
        }


        public async Task IniciarAsync()
        {
            await _mqttClient.StartAsync(managedOptions);
        }


        public bool EstaConectado() {
            return _mqttClient.IsConnected;
        }


        public void PublicarAsync(string topic, string payload, bool retainFlag = true, int qos = 1)
        {
            _mqttClient.InternalClient.PublishAsync(new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
            .WithRetainFlag(retainFlag)
            .Build());
        }

        public async Task InscrecerAsync(string topic, int qos = 1)
        {
            await _mqttClient.InternalClient.SubscribeAsync(new MqttTopicFilterBuilder()
              .WithTopic(topic)
              .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
              .Build());
        }

    }

  

}

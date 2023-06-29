using clientMqtt.Context;
using clientMqtt.Model;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;

namespace clientMqtt
{
    public partial class Form1 : Form
    {

        public delegate void crossMessage(String message);


        private void mensagemCruzadaLabelContador(String mensagem)
        {
            if (this.InvokeRequired)
            {
                crossMessage dlg = new crossMessage(mensagemCruzadaAuxiliar);
                this.Invoke(dlg, new object[] { mensagem });
            }
            else
                mensagemCruzadaAuxiliar(mensagem);
        }

        private void mensagemCruzadaAuxiliar(String mensagem)
        {
            richTextBox1.Text += mensagem;
        }







        IManagedMqttClient _mqttClient;
        ManagedMqttClientOptions managedOptions;

        string clientId = Guid.NewGuid().ToString();
        string mqttURI = "jackal.rmq.cloudamqp.com";
        string mqttUser = "ilzlfxcm:ilzlfxcm";
        string mqttPassword = "cKwGXuuNWrNXhsbYs5oHRuNKQOkpGHTw";
        int mqttPort = 1883;
        bool mqttSecure = false;
        public Form1()
        {
            InitializeComponent();
            button1.BackColor = Color.Red;
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

            mensagemCruzadaLabelContador($"{utfString}\n");
            criarNovaCoordenada(utfString);
            

            return Task.CompletedTask;
        }

        void criarNovaCoordenada(string data)
        {
            using (var db = new Contexto())
            {
                Coordenadas coordenadas = new Coordenadas();

                var separandoDados = data.Split(',');
                coordenadas.MensagemTag = separandoDados.LastOrDefault();
                coordenadas.Latitude = Convert.ToDouble(separandoDados[0].Replace(".",","));
                coordenadas.Longitude = Convert.ToDouble(separandoDados[1].Replace(".", ","));
                try {

                    string input = separandoDados[2];
                    string format = "hhmmss";

                    TimeSpan time = TimeSpan.ParseExact(input, format, CultureInfo.InvariantCulture);
                    coordenadas.HorarioUtc = time;
                }
                catch { }
                

                db.Coordenadas.Add(coordenadas);
                db.SaveChanges();
            }
        }


        void fecharConexao()
        {
            _mqttClient.StopAsync();
        }


        async Task startAsync(ManagedMqttClientOptions options)
        {
            await _mqttClient.StartAsync(options);

        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (!_mqttClient.IsConnected)
            {
                button1.BackColor = Color.Green;
                startAsync(managedOptions).Wait();
            }
            else
            {
                button1.BackColor = Color.Red;

                fecharConexao();
            }


        }
        public void PublishAsync(string topic, string payload, bool retainFlag = true, int qos = 1)
        {
            _mqttClient.InternalClient.PublishAsync(new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
            .WithRetainFlag(retainFlag)
            .Build());
        }

        public async Task SubscribeAsync(string topic, int qos = 1)
        {
            await _mqttClient.InternalClient.SubscribeAsync(new MqttTopicFilterBuilder()
              .WithTopic(topic)
              .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
              .Build());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            PublishAsync(textBox1.Text, textBox2.Text);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            SubscribeAsync(textBox3.Text);
        }
    }
}

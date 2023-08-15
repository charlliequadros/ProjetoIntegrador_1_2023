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
using MQTTnet.Server;

namespace clientMqtt
{
    public partial class TelaPrincipal : Form
    {

        public delegate void crossMessage(String message);
        ProtocoloMqtt protocolo = new ProtocoloMqtt();

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
            messagemInscricao_richTextBox.Text += mensagem;
        }


        public TelaPrincipal()
        {
            InitializeComponent();
            conectar_button.BackColor = Color.Red;
            ProtocoloMqtt.RecebeMensagemMqtt += ProtocoloMqtt_RecebeMensagemMqtt; ;
        }

        private void ProtocoloMqtt_RecebeMensagemMqtt(object sender, EventArgs e)
        {
            var evento = (EventoCustomizado)e;
            mensagemCruzadaLabelContador($"{evento.Mensagem}\n");
            criarNovaCoordenada(evento.Mensagem);
        }

        void criarNovaCoordenada(string data)
        {
            using (var db = new Contexto())
            {
                Coordenadas coordenadas = new Coordenadas();
                var separandoDados = data.Split(',');
                coordenadas.MensagemTag = separandoDados.LastOrDefault();
                coordenadas.Latitude = Convert.ToDouble(separandoDados[0].Replace(".", ","));
                coordenadas.Longitude = Convert.ToDouble(separandoDados[1].Replace(".", ","));
                try
                {
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!protocolo.EstaConectado())
            {
                conectar_button.BackColor = Color.Green;
                protocolo.IniciarAsync().Wait();
            }
            else
            {
                conectar_button.BackColor = Color.Red;

                protocolo.fecharConexao();
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            protocolo.PublicarAsync(topicoPublicar_textBox.Text, messagem_textBox.Text);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            protocolo.InscrecerAsync(topicoInscricao_textBox.Text);
        }
    }
}

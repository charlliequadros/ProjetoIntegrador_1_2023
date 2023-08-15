using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientMqtt
{
    public class EventoCustomizado:EventArgs
    {
        public EventoCustomizado(string mensagem)
        {
            Mensagem = mensagem;
        }
        public String Mensagem { get; set; }
    }
}

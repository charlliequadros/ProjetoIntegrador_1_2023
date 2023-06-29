using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientMqtt.Model
{
    public class Coordenadas
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MensagemTag { get; set; }
        public TimeSpan HorarioUtc { get; set; }
        public DateTime create { get; set; }

        public Coordenadas()
        {
            create = DateTime.Now;
        }
    }
}

namespace WebApplication3.Model
{
    public class Coordenadas
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MensagemTag { get; set; }
        public TimeSpan HorarioUtc { get; set; }
        public DateTime create { get; set; }
    }
}

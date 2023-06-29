namespace WebApplication3.DTOs
{
    public class UsuarioToken
    {
        public bool Autenticado { get; set; }
        public DateTime Exp { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}

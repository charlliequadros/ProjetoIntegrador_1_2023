using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Globalization;
using WebApplication3.contexto;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
  //  [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors("teste1")]
    
    public class CoordenadasController : ControllerBase
    {
        

        private readonly ILogger<CoordenadasController> _logger;
        private readonly AppDbContext contexto;

        public CoordenadasController(ILogger<CoordenadasController> logger, AppDbContext dbContext)
        {
            contexto = dbContext;
            _logger = logger;
        }

        [HttpGet(Name = "ObterTodasCoordenadas")]
        public IEnumerable<Coordenadas> Get()
        {

            return contexto.Coordenadas;
        }


        [HttpGet("ObterTodasCoordenadas/{inicio}/{fim}")]

        public IEnumerable<Coordenadas> Get(string inicio, string fim)
        {
            inicio = Uri.UnescapeDataString(inicio);
            fim = Uri.UnescapeDataString(fim);

            string format = "dd-MM-yyyy";
            DateTime dataInicio = DateTime.ParseExact(inicio, format, CultureInfo.InvariantCulture);
            DateTime dataFim = DateTime.ParseExact(fim, format, CultureInfo.InvariantCulture);

            return contexto.Coordenadas.Where(x=>x.create >= dataInicio && x.create <= dataFim);
        }

        [HttpGet("ObterCoordenadasPorId/{id}")]
        public Coordenadas Get(int id)
        {
            return contexto.Coordenadas.FirstOrDefault(x => x.Id == id);
        }





    }
}
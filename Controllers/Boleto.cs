using Api_brazaletes.DAO;
using Api_brazaletes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_brazaletes.Controllers
{
    [Route("api/validaBoleto")]
    [ApiController]
    public class Boleto : Controller
    {
        [HttpPost]

        public IActionResult Insertar([FromBody] Models.Boleto data)
        {
            operacion objeto = new operacion();

           bool rsp = objeto.ValidarRegistroExistente(data.codigo.ToString(),Convert.ToInt32(data.zona));
            //respuesta = objeto.Insertar(data);

            if (!rsp )
            {
                return Ok(rsp);
            }
            else
            {
                return BadRequest();
            }


        }
    }
}

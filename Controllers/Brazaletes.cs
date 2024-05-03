using Api_brazaletes.DAO;
using Api_brazaletes.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api_brazaletes.Controllers
{
    [Route("api/brazalete")]
    [ApiController]
    public class Brazaletes : Controller
    {
        [HttpPost]

        public IActionResult Insertar([FromBody] Operacion data)
        {
            operacion objeto = new operacion();

            List<Respuesta> rsp = objeto.Insertar(data);
            //respuesta = objeto.Insertar(data);

            if (rsp.Count > 0)
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

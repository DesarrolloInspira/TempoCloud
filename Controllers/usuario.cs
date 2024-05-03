using Api_brazaletes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_brazaletes.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class usuario : Controller
    {
        public IActionResult Insertar([FromBody] Usuarios data)
        {
            DAO.login objeto = new DAO.login();
            List<Usuarios> rsp = objeto.Login(data);
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

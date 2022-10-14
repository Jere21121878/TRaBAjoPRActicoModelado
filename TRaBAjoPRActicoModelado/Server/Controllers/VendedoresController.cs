using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaBD;
using TiendaBD.Data.Entidades;

namespace TRaBAjoPRActicoModelado.Server.Controllers
{
    [ApiController]
    [Route("api/Vendedores")]
    public class VendedoresController : ControllerBase
    {
        private readonly BD dbcontex;

        public VendedoresController(BD dbcontex)
        {
            this.dbcontex = dbcontex;
        }
        [HttpGet]
        public async Task<ActionResult<List<Vendedor>>> Get()
        {
            return await dbcontex.Vendedores.ToListAsync();
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Vendedor>> Get(int Id)
        {
            var vendedor = await dbcontex.Vendedores.Where(
                                     e => e.Id == Id).FirstOrDefaultAsync();
            if (vendedor == null)
            {
                return NotFound($"No el vendedor de Id{Id}");
            }
            return vendedor;
        }
        [HttpPost]
        public async Task<ActionResult<Vendedor>?> Post(Vendedor vendedor)
        {
            try
            {
                dbcontex.Vendedores.Add(vendedor);
                await dbcontex.SaveChangesAsync();
                return null;

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{Id:int}")]
        public ActionResult Put(int Id, [FromBody] Vendedor vendedor)
        {
            if (Id != vendedor.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var jere = dbcontex.Vendedores.Where(e => e.Id == Id).FirstOrDefault();
            if (jere == null)
            {
                return NotFound("No existe el vendedor a modificar");
            }
            jere.NombreUsuario = vendedor.NombreUsuario;
            jere.Clave = vendedor.Clave;
            try
            {
                dbcontex.Vendedores.Update(jere);
                dbcontex.SaveChanges();
                return Ok();
            }
            catch (Exception )
            {

                return BadRequest("Los datos NO han sido actualizados");
            }
        }
        [HttpDelete("{Id:int}")]
        public ActionResult Delete(int Id)
        {
            var jere = dbcontex.Vendedores.Where(x => x.Id == Id).FirstOrDefault();
            if (jere == null)
            {
                return NotFound($"El Vendedor {Id} no fue encontrado");
            }
            try
            {
                dbcontex.Vendedores.Remove(jere);
                dbcontex.SaveChanges();
                return Ok($"El dato del vendedor '{jere.NombreCompleto}' ha sido borrado exitosamente");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por:{e.Message}");
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaBD;
using TiendaBD.Data.Entidades;

namespace TRaBAjoPRActicoModelado.Server.Controllers
{
    [ApiController]
    [Route("api/Tiendas")]
    public class TiendasController : ControllerBase
    {
        private readonly BD dbcontex;
        public TiendasController(BD dbcontex)
        {
            this.dbcontex = dbcontex;
        }
        [HttpGet]
        public async Task<ActionResult<List<Tiendas>>> Get()
        {
            return await dbcontex.Tiendaz.ToListAsync();
        }
        [HttpGet("{IdTienda:int}")]
        public async Task<ActionResult<Tiendas>> Get(int Id)
        {
            var tiendas = await dbcontex.Tiendaz.Where(
                                     e => e.Id == Id).FirstOrDefaultAsync();
            if (tiendas == null)
            {
                return NotFound($"No existe la tienda de Id{Id}");
            }
            return tiendas;
        }
        [HttpPost]
        public async Task<ActionResult<Tiendas>?> Post(Tiendas tiendas)
        {
            try
            {
                dbcontex.Tiendaz.Add(tiendas);
                await dbcontex.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return null;

        }
        [HttpPut("{Id:int}")]
        public ActionResult Put(int Id, [FromBody] Tiendas tiendas)
        {
            if (Id != tiendas.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var jere = dbcontex.Tiendaz.Where(e => e.Id == Id).FirstOrDefault();
            if (jere == null)
            {
                return NotFound("No existe la tienda a modificar");
            }
            jere.NombreTienda = tiendas.NombreTienda;
            jere.DireccionTienda = tiendas.DireccionTienda;
            jere.Clave = tiendas.Clave;
            try
            {
                dbcontex.Tiendaz.Update(jere);
                dbcontex.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest("Los datos NO han sido actualizados");
            }
        }
        [HttpDelete("{Id:int}")]
        public ActionResult Delete(int Id)
        {
            var jere = dbcontex.Tiendaz.Where(x => x.Id == Id).FirstOrDefault();
            if (jere == null)
            {
                return NotFound($"La Tienda{Id}no fue encontrada");
            }
            try
            {
                dbcontex.Tiendaz.Remove(jere);
                dbcontex.SaveChanges();
                return Ok($"El dato de la tienda '{jere.NombreTienda}' ha sido borrado exitosamente");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por:{e.Message}");
            }

        }
    }
}

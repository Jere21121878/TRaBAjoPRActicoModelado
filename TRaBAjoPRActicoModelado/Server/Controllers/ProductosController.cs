using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaBD;
using TiendaBD.Data.Entidades;

namespace TRaBAjoPRActicoModelado.Server.Controllers
{
    [ApiController]
    [Route("api/Productos")]
    public class ProductosController : ControllerBase
    {
        private readonly BD dbcontex;

        public ProductosController(BD dbcontex)
        {
            this.dbcontex = dbcontex;
        }
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            var resp = await dbcontex.Productos.ToListAsync();
            return resp;
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Producto>> Get(int Id)
        {
          var producto = await dbcontex.Productos.Where(
                                   e=> e.Id == Id       ).FirstOrDefaultAsync();
            if (producto == null)
            {
                return NotFound($"No existe el producto de Id{Id}");
            }
            return producto;
        }
        [HttpPost]
        public async Task<ActionResult<Producto>?> Post(Producto producto)
        {
            try
            {
                dbcontex.Productos.Add(producto);
                await dbcontex.SaveChangesAsync();
                return null;

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{Id:int}")]
        public ActionResult Put(int Id, [FromBody] Producto producto)
        {
            if (Id != producto.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var jere = dbcontex.Productos.Where(e => e.Id == Id).FirstOrDefault();
            if (jere == null)
            {
                return NotFound("No existe el producto a modificar");
            }
            jere.Nombre = producto.Nombre;
            jere.Precio = producto.Precio;
            jere.Stock = producto.Stock;
            try
            {
                dbcontex.Productos.Update(jere);
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
            var jere = dbcontex.Productos.Where(x => x.Id == Id).FirstOrDefault();
            if (jere == null)
            {
                return NotFound($"El producto{Id}no fue encontrado");
            }
            try
            {
                dbcontex.Productos.Remove(jere);
                dbcontex.SaveChanges();
                return Ok($"El dato del producto '{jere.Nombre}' ha sido borrado exitosamente");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por:{e.Message}");
            }

        }
    }
    
   
}

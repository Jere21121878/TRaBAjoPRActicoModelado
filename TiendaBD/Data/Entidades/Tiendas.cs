using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaBD.Data.Entidades
{
    public class Tiendas
    {
        public int Id { get; set; }
        public string NombreTienda { get; set; }
        public int Cuit { get; set; }
        public string DireccionTienda { get; set; }
        public int Clave { get; set; }
        
    }
}

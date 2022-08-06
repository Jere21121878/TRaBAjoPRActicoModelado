using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaBD.Data.Entidades
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string NombreCompleto  { get; set; }
        public int Dni { get; set; }
        public string NombreUsuario { get; set; }
        public int Clave { get; set; }

    }
}

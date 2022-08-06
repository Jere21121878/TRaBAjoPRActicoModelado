using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaBD.Data.Entidades;

namespace TiendaBD
{
    public class BD : DbContext
    {
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public BD(DbContextOptions options) : base(options)
        { }
    }

}

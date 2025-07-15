using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using TallerApp.Models;

namespace TallerApp.Services
{
    

    public class TallerContext : DbContext
    {
        public TallerContext() : base("TallerDb") { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<Camion> Camiones { get; set; }
    }
}

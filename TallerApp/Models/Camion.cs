using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerApp.Models
{
    public class Camion
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public int Año { get; set; }
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}

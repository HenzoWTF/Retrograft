using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class ProductoMasVendido
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadVendida { get; set; }
    }
}

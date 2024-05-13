using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }

        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float Precio { get; set; }
        public float Total { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Cantidad { get; set; }
        public bool Realizado { get; set; }


    }
}

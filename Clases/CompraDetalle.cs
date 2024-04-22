using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases;
public class CompraDetalle
{
    [Key]
    public int CompraDetalleId { get; set; }
    public int ProductoId { get; set; }
    [Range(0, 1000000, ErrorMessage = "El campo {0} debe ser mayor que 0 y menor que 1000000.")]
    public int Cantidad { get; set; }
    public float PrecioCompra { get; set; }
}

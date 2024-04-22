using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases;
public class Ventas
{

    [Key]
    public int VentaId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public DateTime Fecha { get; set; } = DateTime.Today;

    [Range(1, int.MaxValue, ErrorMessage = "El campo debe ser mayor que cero")]
    public float Valor { get; set; }

    [ForeignKey("VentaId")]
    public List<VentaDetalle> VentaDetalle { get; set; } = new List<VentaDetalle>();
}

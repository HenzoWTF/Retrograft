using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases;
public class Cliente
{
    [Key]
    public int IdCliente { get; set; }
    public string NombreCliente { get; set; }
    public string DireccionCliente { get; set; }

    [ForeignKey("IdCliente")]
    public ICollection<CompraDetalle> CompraDetalles { get; set; } = new List<CompraDetalle>();
}

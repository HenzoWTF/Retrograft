using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases;

public class Facturas
{
    [Key]
    public int FacturaId { get; set; }
    public string Cliente { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string FormaDePago { get; set; }
    public float Total { get; set; }
    public bool Cobrada { get; set; }
    public float Deuda { get; set; }
}

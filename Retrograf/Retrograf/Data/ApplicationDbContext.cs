using Clases;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Retrograf.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<Facturas> Facturas { get; set; }
    public DbSet<Productos> Productos { get; set; }
    public DbSet<Ventas> Ventas { get; set; }
    public DbSet<Cuadre> Cuadres { get; set; }
}

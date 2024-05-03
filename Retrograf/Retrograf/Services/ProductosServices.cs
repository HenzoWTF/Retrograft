using Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Retrograf.Data;

namespace Retrograf.Services;

public class ProductosServices(ApplicationDbContext context)
{
    public async Task<List<Productos>> GetProductos()
    {
        return await context.Productos
        .Select(d => new Productos()
        {
            ProductoId = d.ProductoId,
            Nombre = d.Nombre,
            Descripcion = d.Descripcion,
            FechaCreacion = d.FechaCreacion,
            Cantidad = d.Cantidad,
            PrecioVenta = d.PrecioVenta,
            PrecioCompra = d.PrecioCompra,
        }).ToListAsync();
    }

    public async Task<Productos?> GetProducto(int id)
    {
        return await context.Productos.FindAsync(id);
    }

    public async Task<Productos> PostProducto(Productos producto)
    {
        context.Productos.Add(producto);
        await context.SaveChangesAsync();
        return producto;
    }
    public async Task<IActionResult> PutProducto(int id, Productos producto)
    {
        if (id != producto.ProductoId)
        {
            return new BadRequestResult();
        }

        context.Entry(producto).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(id))
            {
                return new NotFoundResult();
            }
            else
            {
                throw;
            }
        }

        return new NoContentResult();
    }

    public async Task DeleteProducto(int id)
    {
        var Producto = await context.Productos
            .FirstOrDefaultAsync(p => p.ProductoId == id);

        if (Producto != null)
        {
            context.Productos.Remove(Producto);
            await context.SaveChangesAsync();
        }
    }
    public bool ProductoExists(int id)
    {
        return context.Productos.Any(e => e.ProductoId == id);
    }

}




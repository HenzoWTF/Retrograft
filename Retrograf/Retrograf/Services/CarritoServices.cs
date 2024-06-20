using Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Retrograf.Data;

namespace Retrograf.Services;
public class CarritoServices(ApplicationDbContext context)
{
    public async Task<List<Carrito>> GetCarritos()
    {
        return await context.Carrito
        .Select(d => new Carrito()
        {
            IdCarrito = d.IdCarrito,
            Nombre = d.Nombre,
            ProductoId = d.ProductoId,
            Cantidad = d.Cantidad,
            Total = d.Total,
            Precio = d.Precio,
            Realizado = d.Realizado
        }).ToListAsync();
    }
    public async Task<Carrito?> GetCarrito(int id)
    {
        return await context.Carrito.FirstOrDefaultAsync(b => b.IdCarrito == id);
    }
    public async Task<Carrito> PostCarrito(Carrito carrito)
    {
        var existingCarrito = await context.Carrito.FirstOrDefaultAsync(c => c.ProductoId == carrito.ProductoId);
        if (existingCarrito != null)
        {
            existingCarrito.Cantidad += carrito.Cantidad;
            existingCarrito.Total += carrito.Total;
            await context.SaveChangesAsync();
            return existingCarrito;
        }
        else
        {
            context.Carrito.Add(carrito);
            await context.SaveChangesAsync();
            return carrito;
        }
    }
    public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
    {
        if (id != carrito.IdCarrito)
        {
            return new BadRequestResult();
        }

        context.Entry(carrito).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CarritoExists(id))
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

    public async Task DeleteCarrito(int id)
    {
        var carrito = await context.Carrito
            .FirstOrDefaultAsync(p => p.IdCarrito == id);

        if (carrito != null)
        {
            context.Carrito.Remove(carrito);
            await context.SaveChangesAsync();
        }
    }
    public bool CarritoExists(int id)
    {
        return context.Carrito.Any(e => e.IdCarrito == id);
    }
}

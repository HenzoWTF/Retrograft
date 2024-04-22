using Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Retrograf.Data;

namespace Retrograf.Services;

public class ComprasServices(ApplicationDbContext context)
{
    public async Task<IEnumerable<Compra>> GetCompras()
    {
        return await context.Compras.Include(p => p.CompraDetalles)
        .Select(d => new Compra()
        {
            CompraId = d.CompraId,
            FecheDeCompra = d.FecheDeCompra,
            CompraDetalles = d.CompraDetalles
        }).ToListAsync();
    }

    public async Task<Compra?> GetCompra(int id)
    {
        return await context.Compras.Include(c => c.CompraDetalles)
            .Where(c => c.CompraId == id).FirstOrDefaultAsync();
    }

    public async Task<Compra> PostCompras(Compra Compra)
    {
        context.Compras.Add(Compra);
        await context.SaveChangesAsync();
        return Compra;

    }
    public async Task<IActionResult> PutCompras(int id, Compra Compra)
    {
        if (id != Compra.CompraId)
        {
            return new BadRequestResult();
        }

        context.Entry(Compra).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ComprasExists(id))
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

    public async Task DeleteCompras(int id)
    {
        var Compra = await context.Compras
            .FirstOrDefaultAsync(p => p.CompraId == id);

        if (Compra != null)
        {
            context.Compras.Remove(Compra);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteDetalle(int CompraId, int detalleId)
    {
        var Compra = await context.Compras
            .Include(d => d.CompraDetalles)
            .FirstOrDefaultAsync(p => p.CompraId == CompraId);

        if (Compra != null)
        {
            var detalle = Compra.CompraDetalles.FirstOrDefault(d => d.CompraDetalleId == detalleId);
            if (detalle != null)
            {
                Compra.CompraDetalles.Remove(detalle);
                await context.SaveChangesAsync();
            }
        }
    }
    public bool ComprasExists(int id)
    {
        return context.Compras.Any(e => e.CompraId == id);
    }
}

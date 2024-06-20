using Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Retrograf.Data;

namespace Retrograf.Services;
public class FacturasServices(ApplicationDbContext context)
{
    public async Task<IEnumerable<Facturas>> GetFacturas()
    {
        return await context.Facturas
        .Select(d => new Facturas()
        {
            FacturaId = d.FacturaId,
            Cliente = d.Cliente,
            FormaDePago = d.FormaDePago,
            Total = d.Total,
            Fecha = d.Fecha,
            Cobrada = d.Cobrada,
            Deuda = d.Deuda
        }).ToListAsync();
    }
    public async Task<Facturas?> GetFactura(int id)
    {
        return await context.Facturas.FirstOrDefaultAsync(b => b.FacturaId == id);
    }


    public async Task<Facturas> PostFacturas(Facturas Facturas)
    {
        context.Facturas.Add(Facturas);
        await context.SaveChangesAsync();
        return Facturas;

    }
    public async Task<IActionResult> PutFactura(int id, Facturas Facturas)
    {
        if (id != Facturas.FacturaId)
        {
            return new BadRequestResult();
        }

        context.Entry(Facturas).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FacturasExists(id))
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

    public async Task DeleteFacturas(int id)
    {
        var Facturas = await context.Facturas
            .FirstOrDefaultAsync(p => p.FacturaId == id);

        if (Facturas != null)
        {
            context.Facturas.Remove(Facturas);
            await context.SaveChangesAsync();
        }
    }
    public bool FacturasExists(int id)
    {
        return context.Facturas.Any(e => e.FacturaId == id);
    }
}

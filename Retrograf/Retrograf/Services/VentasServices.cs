﻿using Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Retrograf.Data;
using System.Linq.Expressions;

namespace Retrograf.Services;
public class VentasServices(ApplicationDbContext context)
{
    public async Task<List<Ventas>> GetVentas()
    {
        return await context.Ventas.Include(p => p.VentaDetalle)
        .Select(d => new Ventas()
        {
            VentaId = d.VentaId,
            Fecha = d.Fecha,
            Total = d.Total,
            Cobrada = d.Cobrada,
            Deuda = d.Deuda,
            FormaDePago = d.FormaDePago,
            Cliente = d.Cliente,
            VentaDetalle = d.VentaDetalle
        }).ToListAsync();
    }
    public async Task<List<Ventas>> GetVentasDeudas()
    {
        return await context.Ventas
        .Where(v => !v.Cobrada)
        .Select(d => new Ventas()
        {
            VentaId = d.VentaId,
            Fecha = d.Fecha,
            Total = d.Total,
            Cobrada = d.Cobrada,
            Deuda = d.Deuda,
            Cliente = d.Cliente,
            FormaDePago = d.FormaDePago
        }).ToListAsync();
    }
    public async Task<Ventas?> GetVenta(int id)
    {
        return await context.Ventas.Include(p => p.VentaDetalle)
            .Where(c => c.VentaId == id).FirstOrDefaultAsync();
    }
    public async Task<Ventas?> GetVentaDeuda(int id)
    {
        return await context.Ventas.FindAsync(id);
    }

    public async Task<Ventas> PostVentas(Ventas Venta)
    {
        context.Ventas.Add(Venta);
        await context.SaveChangesAsync();
        return Venta;
    }

    public async Task<IActionResult> PutCompras(int id, Ventas Venta)
    {
        if (id != Venta.VentaId)
        {
            return new BadRequestResult();
        }

        context.Entry(Venta).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VentaExists(id))
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

    public async Task DeleteVentas(int id)
    {
        var Compra = await context.Ventas
            .FirstOrDefaultAsync(p => p.VentaId == id);

        if (Compra != null)
        {
            context.Ventas.Remove(Compra);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteDetalle(int VentaId, int detalleId)
    {
        var Venta = await context.Ventas
        .Include(d => d.VentaDetalle)
            .FirstOrDefaultAsync(p => p.VentaId == VentaId);

        if (Venta != null)
        {
            var detalle = Venta.VentaDetalle.FirstOrDefault(d => d.VentaDetalleId == detalleId);
            if (detalle != null)
            {
                Venta.VentaDetalle.Remove(detalle);
                await context.SaveChangesAsync();
            }
        }
    }

    public bool VentaExists(int id)
    {
        return context.Ventas.Any(e => e.VentaId == id);
    }

    public async Task<List<Ventas>> GetList(Expression<Func<Ventas, bool>> criterio)
    {
        return await context.Ventas
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
    public async Task<List<Ventas>> GetListDeuda(Expression<Func<Ventas, bool>> criterio)
    {
        return await context.Ventas
            .AsNoTracking()
            .Where(criterio)
            .Where(v => !v.Cobrada)
            .ToListAsync();
    }
}


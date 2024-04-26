using Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Retrograf.Data;
using System.Linq.Expressions;

namespace Retrograf.Services;

public class ClienteServices(ApplicationDbContext context)
{
    public async Task<IEnumerable<Cliente>> GetClientes()
    {
        return await context.Clientes
        .Select(d => new Cliente()
        {
            IdCliente = d.IdCliente,
            NombreCliente = d.NombreCliente,
            DireccionCliente = d.DireccionCliente
        }).ToListAsync();
    }
    public async Task<Cliente?> GetCliente(int id)
    {
        return await context.Clientes.FirstOrDefaultAsync(b => b.IdCliente == id);
    }


    public async Task<Cliente> PostClientes(Cliente Clientes)
    {
        context.Clientes.Add(Clientes);
        await context.SaveChangesAsync();
        return Clientes;
    }
    public async Task<IActionResult> PutClientes(int id, Cliente Clientes)
    {
        if (id != Clientes.IdCliente)
        {
            return new BadRequestResult();
        }

        context.Entry(Clientes).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientesExists(id))
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

    public async Task DeleteClientes(int id)
    {
        var Clientes = await context.Clientes
            .FirstOrDefaultAsync(p => p.IdCliente == id);

        if (Clientes != null)
        {
            context.Clientes.Remove(Clientes);
            await context.SaveChangesAsync();
        }
    }
    public bool ClientesExists(int id)
    {
        return context.Clientes.Any(e => e.IdCliente == id);
    }
    public async Task<List<Cliente>> GetList(Expression<Func<Cliente, bool>> criterio)
    {
        return await context.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}

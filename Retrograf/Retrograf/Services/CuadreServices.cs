﻿using Clases;
using Microsoft.EntityFrameworkCore;
using Retrograf.Data;

namespace Retrograf.Services;
public class CuadreServices(ApplicationDbContext _context)
{
    public async Task GuardarCuadreEnBaseDeDatos(DateTime fecha)
    {
        var cuadreDiario = await CalcularCuadreDiario(fecha);
        _context.Cuadres.Add(cuadreDiario);
        await _context.SaveChangesAsync();
    }
    public async Task GuardarCuadreEnBaseDeDatosSemanal(DateTime fecha)
    {
        var cuadreDiario = await CalcularCuadreSemanal(fecha);
        _context.Cuadres.Add(cuadreDiario);
        await _context.SaveChangesAsync();
    }
    public async Task GuardarCuadreEnBaseDeDatosMensual(DateTime fecha)
    {
        var cuadreDiario = await CalcularCuadreMensual(fecha);
        _context.Cuadres.Add(cuadreDiario);
        await _context.SaveChangesAsync();
    }

    public async Task<Cuadre> CalcularCuadreDiario(DateTime fecha)
    {
        var cuadre = new Cuadre
        {
            FechaCuadre = fecha.Date
        };

        cuadre.Ingresos = await CalcularTotalIngresos(fecha);
        cuadre.Gastos = await CalcularTotalGastos(fecha);
        cuadre.Total = cuadre.Ingresos - cuadre.Gastos;
        cuadre.Nombre = "diario";

        return cuadre;
    }

    public async Task<Cuadre> CalcularCuadreSemanal(DateTime fechaInicioSemana)
    {
        var fechaFinSemana = fechaInicioSemana.AddDays(6);
        var cuadreSemana = new Cuadre
        {
            FechaCuadre = fechaInicioSemana
        };


        cuadreSemana.Ingresos = await CalcularTotalIngresosSemana(fechaInicioSemana, fechaFinSemana);
        cuadreSemana.Gastos = await CalcularTotalGastosSemana(fechaInicioSemana, fechaFinSemana);
        cuadreSemana.Total = cuadreSemana.Ingresos - cuadreSemana.Gastos;
        cuadreSemana.Nombre = "semanal";

        return cuadreSemana;
    }
    private async Task<float> CalcularTotalIngresos(DateTime fecha)
    {
        var ventas = await _context.Ventas
            .Where(v => v.Fecha.Date == fecha.Date)
            .ToListAsync();


        return ventas.Sum(v => v.Total);
    }


    private async Task<float> CalcularTotalGastos(DateTime fecha)
    {
        var compras = await _context.Compras
            .Include(c => c.CompraDetalles)
            .Where(c => c.FecheDeCompra.Date == fecha.Date)
            .ToListAsync();

        float totalGastos = 0;

        foreach (var compra in compras)
        {
            totalGastos += compra.CompraDetalles.Sum(cd => cd.PrecioCompra * cd.Cantidad);
        }

        return totalGastos;
    }

    private async Task<float> CalcularTotalIngresosSemana(DateTime fechaInicio, DateTime fechaFin)
    {
        var ventasSemana = await _context.Ventas
            .Where(v => v.Fecha.Date >= fechaInicio.Date && v.Fecha.Date <= fechaFin.Date)
            .ToListAsync();

        float totalVentas = ventasSemana.Sum(v => v.Total);

        return totalVentas;
    }

    private async Task<float> CalcularTotalGastosSemana(DateTime fechaInicio, DateTime fechaFin)
    {
        var comprasSemana = await _context.Compras
            .Include(c => c.CompraDetalles)
            .Where(c => c.FecheDeCompra.Date >= fechaInicio.Date && c.FecheDeCompra.Date <= fechaFin.Date)
            .ToListAsync();

        float totalGastosSemana = 0;

        foreach (var compra in comprasSemana)
        {
            totalGastosSemana += compra.CompraDetalles.Sum(cd => cd.PrecioCompra * cd.Cantidad);
        }

        return totalGastosSemana;
    }

    public async Task<Cuadre> CalcularCuadreMensual(DateTime primerDiaMes)
    {
        var ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);
        var cuadreMes = new Cuadre
        {
            FechaCuadre = primerDiaMes
        };

        cuadreMes.Ingresos = await CalcularTotalIngresosMes(primerDiaMes, ultimoDiaMes);
        cuadreMes.Gastos = await CalcularTotalGastosMes(primerDiaMes, ultimoDiaMes);
        cuadreMes.Total = cuadreMes.Ingresos - cuadreMes.Gastos;
        cuadreMes.Nombre = "semanal"; 

        return cuadreMes;
    }

    private async Task<float> CalcularTotalIngresosMes(DateTime primerDiaMes, DateTime ultimoDiaMes)
    {
        var ventasMes = await _context.Ventas
            .Where(v => v.Fecha.Date >= primerDiaMes.Date && v.Fecha.Date <= ultimoDiaMes.Date)
            .ToListAsync();

        float totalVentasMes = ventasMes.Sum(v => v.Total);

        return totalVentasMes;
    }

    private async Task<float> CalcularTotalGastosMes(DateTime primerDiaMes, DateTime ultimoDiaMes)
    {
        var comprasMes = await _context.Compras
            .Include(c => c.CompraDetalles)
            .Where(c => c.FecheDeCompra.Date >= primerDiaMes.Date && c.FecheDeCompra.Date <= ultimoDiaMes.Date)
            .ToListAsync();

        float totalGastosMes = 0;

        foreach (var compra in comprasMes)
        {
            totalGastosMes += compra.CompraDetalles.Sum(cd => cd.PrecioCompra * cd.Cantidad);
        }

        return totalGastosMes;
    }

}
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public interface IReporteRepository
{
    List<DetallePedidoReporte> ReporteDetallePedidosPorFechas(DateTime fechaInicio, DateTime fechaFin);
    Task<List<DetallePedidoReporte>> ReporteDetallePedidosPorFechasAsync(DateTime fechaInicio, DateTime fechaFin);

    // MODO DESCONECTADO (SqlDataAdapter y DataTable)
    DataTable ReporteDetallePedidosPorFechasDesconectado(DateTime fechaInicio, DateTime fechaFin);
    Task<DataTable> ReporteDetallePedidosPorFechasDesconectadoAsync(DateTime fechaInicio, DateTime fechaFin);
}

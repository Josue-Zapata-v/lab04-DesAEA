using System;
using System.Collections.Generic;
using WPF_SP.Models;

namespace WPF_SP.Data;

public interface IReporteRepository
{
    List<DetallePedidoReporte> ReporteDetallePedidosPorFechas(DateTime fechaInicio, DateTime fechaFin);
}

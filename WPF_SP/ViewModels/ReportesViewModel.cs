using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF_SP.Models;
using WPF_SP.Data;
using System;

namespace WPF_SP.ViewModels;

public partial class ReportesViewModel : ObservableObject
{
    private readonly IReporteRepository _repo = new ReporteRepository();

    [ObservableProperty]
    private ObservableCollection<DetallePedidoReporte> _reporte = new();

    [ObservableProperty]
    private DateTime _fechaInicio = new DateTime(1996, 1, 1);

    [ObservableProperty]
    private DateTime _fechaFin = new DateTime(1998, 12, 31);

    [RelayCommand]
    private void GenerarReporte()
    {
        try
        {
            Reporte = new ObservableCollection<DetallePedidoReporte>(_repo.ReporteDetallePedidosPorFechas(FechaInicio, FechaFin));
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }
}

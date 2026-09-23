using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Neptuno.Data.Models;
using Neptuno.Data.Data;
using System;
using System.Threading.Tasks;

namespace WPF_SP.ViewModels;

public partial class ReportesViewModel : ObservableObject
{
    private readonly IReporteRepository _repo = new ReporteRepository();

    [ObservableProperty]
    private ObservableCollection<DetallePedidoReporte> _reporte = new();

    [ObservableProperty]
    private DateTime _fechaInicio = new DateTime(2026, 1, 1);

    [ObservableProperty]
    private DateTime _fechaFin = new DateTime(2026, 12, 31);

    public ReportesViewModel()
    {
        // Carga asíncrona del reporte inicial
        _ = GenerarReporteAsync();
    }

    [RelayCommand]
    private async Task GenerarReporteAsync()
    {
        try
        {
            var list = await _repo.ReporteDetallePedidosPorFechasAsync(FechaInicio, FechaFin);
            Reporte = new ObservableCollection<DetallePedidoReporte>(list);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al generar reporte", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }
}

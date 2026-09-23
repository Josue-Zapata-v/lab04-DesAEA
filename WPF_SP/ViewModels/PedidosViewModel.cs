using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Neptuno.Data.Models;
using Neptuno.Data.Data;
using System;
using System.Threading.Tasks;

namespace WPF_SP.ViewModels;

public partial class PedidosViewModel : ObservableObject
{
    private readonly IPedidoRepository _repo = new PedidoRepository();

    [ObservableProperty]
    private ObservableCollection<Pedido> _pedidos = new();

    [ObservableProperty]
    private Pedido _pedidoSeleccionado = new();

    public PedidosViewModel()
    {
        // Carga asíncrona sin bloquear la UI
        _ = CargarAsync();
    }

    [RelayCommand]
    private async Task CargarAsync()
    {
        try
        {
            var list = await _repo.ListarPedidosAsync();
            Pedidos = new ObservableCollection<Pedido>(list);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al cargar pedidos", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        try
        {
            if (PedidoSeleccionado.PedidoID == 0)
                await _repo.InsertarPedidoAsync(PedidoSeleccionado);
            else
                await _repo.ActualizarPedidoAsync(PedidoSeleccionado);

            await CargarAsync();
            Nuevo();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al guardar pedido", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task EliminarAsync()
    {
        try
        {
            if (PedidoSeleccionado.PedidoID > 0)
            {
                await _repo.EliminarPedidoAsync(PedidoSeleccionado.PedidoID);
                await CargarAsync();
                Nuevo();
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al eliminar pedido", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Nuevo() => PedidoSeleccionado = new Pedido();
}

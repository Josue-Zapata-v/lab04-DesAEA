using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF_SP.Models;
using WPF_SP.Data;
using System;

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
        Cargar();
    }

    [RelayCommand]
    private void Cargar() => Pedidos = new ObservableCollection<Pedido>(_repo.ListarPedidos());

    [RelayCommand]
    private void Guardar()
    {
        try
        {
            if (PedidoSeleccionado.PedidoID == 0) _repo.InsertarPedido(PedidoSeleccionado);
            else _repo.ActualizarPedido(PedidoSeleccionado);
            Cargar();
            Nuevo();
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }

    [RelayCommand]
    private void Eliminar()
    {
        try
        {
            if (PedidoSeleccionado.PedidoID > 0)
            {
                _repo.EliminarPedido(PedidoSeleccionado.PedidoID);
                Cargar();
                Nuevo();
            }
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }

    [RelayCommand]
    private void Nuevo() => PedidoSeleccionado = new Pedido();
}

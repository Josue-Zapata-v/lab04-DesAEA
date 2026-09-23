using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Neptuno.Data.Models;
using Neptuno.Data.Data;
using System;
using System.Threading.Tasks;

namespace WPF_SP.ViewModels;

public partial class ProveedoresViewModel : ObservableObject
{
    private readonly IProveedorRepository _repo = new ProveedorRepository();

    [ObservableProperty]
    private ObservableCollection<Proveedor> _proveedores = new();

    [ObservableProperty]
    private Proveedor _proveedorSeleccionado = new();

    [ObservableProperty]
    private string _busquedaNombre = string.Empty;

    [ObservableProperty]
    private string _busquedaCiudad = string.Empty;

    public ProveedoresViewModel()
    {
        // Carga asíncrona sin bloquear la ventana
        _ = CargarAsync();
    }

    [RelayCommand]
    private async Task CargarAsync()
    {
        try
        {
            var list = await _repo.ListarProveedoresAsync();
            Proveedores = new ObservableCollection<Proveedor>(list);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al cargar proveedores", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task BuscarAsync()
    {
        try
        {
            var list = await _repo.BuscarProveedoresAsync(BusquedaNombre, BusquedaCiudad);
            Proveedores = new ObservableCollection<Proveedor>(list);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al buscar proveedores", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task LimpiarBusquedaAsync()
    {
        BusquedaNombre = string.Empty;
        BusquedaCiudad = string.Empty;
        await CargarAsync();
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        try
        {
            if (ProveedorSeleccionado.ProveedorID == 0)
                await _repo.InsertarProveedorAsync(ProveedorSeleccionado);
            else
                await _repo.ActualizarProveedorAsync(ProveedorSeleccionado);

            await BuscarAsync(); // Refresca con los filtros actuales
            Nuevo();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al guardar proveedor", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task EliminarAsync()
    {
        try
        {
            if (ProveedorSeleccionado.ProveedorID > 0)
            {
                await _repo.EliminarProveedorAsync(ProveedorSeleccionado.ProveedorID);
                await BuscarAsync();
                Nuevo();
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al eliminar proveedor", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Nuevo() => ProveedorSeleccionado = new Proveedor();
}

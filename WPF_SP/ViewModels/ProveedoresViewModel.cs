using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF_SP.Models;
using WPF_SP.Data;
using System;

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
        Cargar();
    }

    [RelayCommand]
    private void Cargar() => Proveedores = new ObservableCollection<Proveedor>(_repo.ListarProveedores());

    [RelayCommand]
    private void Buscar()
    {
        try
        {
            Proveedores = new ObservableCollection<Proveedor>(_repo.BuscarProveedores(BusquedaNombre, BusquedaCiudad));
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }

    [RelayCommand]
    private void LimpiarBusqueda()
    {
        BusquedaNombre = string.Empty;
        BusquedaCiudad = string.Empty;
        Cargar();
    }

    [RelayCommand]
    private void Guardar()
    {
        try
        {
            if (ProveedorSeleccionado.ProveedorID == 0) _repo.InsertarProveedor(ProveedorSeleccionado);
            else _repo.ActualizarProveedor(ProveedorSeleccionado);
            Buscar(); // Refresh with current filters
            Nuevo();
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }

    [RelayCommand]
    private void Eliminar()
    {
        try
        {
            if (ProveedorSeleccionado.ProveedorID > 0)
            {
                _repo.EliminarProveedor(ProveedorSeleccionado.ProveedorID);
                Buscar();
                Nuevo();
            }
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }

    [RelayCommand]
    private void Nuevo() => ProveedorSeleccionado = new Proveedor();
}

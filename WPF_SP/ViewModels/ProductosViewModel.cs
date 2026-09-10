using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF_SP.Models;
using WPF_SP.Data;
using System;

namespace WPF_SP.ViewModels;

public partial class ProductosViewModel : ObservableObject
{
    private readonly IProductoRepository _repo = new ProductoRepository();

    [ObservableProperty]
    private ObservableCollection<Producto> _productos = new();

    [ObservableProperty]
    private Producto _productoSeleccionado = new();

    public ProductosViewModel()
    {
        Cargar();
    }

    [RelayCommand]
    private void Cargar() => Productos = new ObservableCollection<Producto>(_repo.ListarProductos());

    [RelayCommand]
    private void Guardar()
    {
        try
        {
            if (ProductoSeleccionado.ProductoID == 0) _repo.InsertarProducto(ProductoSeleccionado);
            else _repo.ActualizarProducto(ProductoSeleccionado);
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
            if (ProductoSeleccionado.ProductoID > 0)
            {
                _repo.EliminarProducto(ProductoSeleccionado.ProductoID);
                Cargar();
                Nuevo();
            }
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }

    [RelayCommand]
    private void Nuevo() => ProductoSeleccionado = new Producto();
}

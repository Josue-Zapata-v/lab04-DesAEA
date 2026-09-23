using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Neptuno.Data.Models;
using Neptuno.Data.Data;
using System;
using System.Threading.Tasks;

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
        // Carga asíncrona sin congelar la interfaz de usuario
        _ = CargarAsync();
    }

    [RelayCommand]
    private async Task CargarAsync()
    {
        try
        {
            var list = await _repo.ListarProductosAsync();
            Productos = new ObservableCollection<Producto>(list);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al cargar productos", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        try
        {
            if (ProductoSeleccionado.ProductoID == 0)
                await _repo.InsertarProductoAsync(ProductoSeleccionado);
            else
                await _repo.ActualizarProductoAsync(ProductoSeleccionado);

            await CargarAsync();
            Nuevo();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al guardar producto", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task EliminarAsync()
    {
        try
        {
            if (ProductoSeleccionado.ProductoID > 0)
            {
                await _repo.EliminarProductoAsync(ProductoSeleccionado.ProductoID);
                await CargarAsync();
                Nuevo();
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al eliminar producto", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Nuevo() => ProductoSeleccionado = new Producto();
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Neptuno.Data.Models;
using Neptuno.Data.Data;
using System;
using System.Threading.Tasks;

namespace WPF_SP.ViewModels;

public partial class CategoriasViewModel : ObservableObject
{
    private readonly ICategoriaRepository _repo = new CategoriaRepository();

    [ObservableProperty]
    private ObservableCollection<Categoria> _categorias = new();

    [ObservableProperty]
    private Categoria _categoriaSeleccionada = new();

    public CategoriasViewModel()
    {
        // Carga asíncrona sin bloquear el hilo principal de la ventana
        _ = CargarAsync();
    }

    [RelayCommand]
    private async Task CargarAsync()
    {
        try
        {
            var list = await _repo.ListarCategoriasAsync();
            Categorias = new ObservableCollection<Categoria>(list);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al cargar categorías", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        try
        {
            if (CategoriaSeleccionada.CategoriaID == 0)
                await _repo.InsertarCategoriaAsync(CategoriaSeleccionada);
            else
                await _repo.ActualizarCategoriaAsync(CategoriaSeleccionada);

            await CargarAsync();
            Nuevo();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al guardar categoría", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task EliminarAsync()
    {
        try
        {
            if (CategoriaSeleccionada.CategoriaID > 0)
            {
                await _repo.EliminarCategoriaAsync(CategoriaSeleccionada.CategoriaID);
                await CargarAsync();
                Nuevo();
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Error al eliminar categoría", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Nuevo() => CategoriaSeleccionada = new Categoria();
}

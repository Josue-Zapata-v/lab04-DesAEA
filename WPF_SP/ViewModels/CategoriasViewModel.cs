using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WPF_SP.Models;
using WPF_SP.Data;
using System;

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
        Cargar();
    }

    [RelayCommand]
    private void Cargar() => Categorias = new ObservableCollection<Categoria>(_repo.ListarCategorias());

    [RelayCommand]
    private void Guardar()
    {
        try
        {
            if (CategoriaSeleccionada.CategoriaID == 0) _repo.InsertarCategoria(CategoriaSeleccionada);
            else _repo.ActualizarCategoria(CategoriaSeleccionada);
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
            if (CategoriaSeleccionada.CategoriaID > 0)
            {
                _repo.EliminarCategoria(CategoriaSeleccionada.CategoriaID);
                Cargar();
                Nuevo();
            }
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
    }

    [RelayCommand]
    private void Nuevo() => CategoriaSeleccionada = new Categoria();
}

using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF_SP.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public CategoriasViewModel CategoriasVM { get; } = new();
    public ProveedoresViewModel ProveedoresVM { get; } = new();
    public ProductosViewModel ProductosVM { get; } = new();
    public PedidosViewModel PedidosVM { get; } = new();
    public ReportesViewModel ReportesVM { get; } = new();
}

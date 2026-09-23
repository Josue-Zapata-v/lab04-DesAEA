using System.Configuration;

namespace Neptuno.Data.Data;

/// <summary>
/// Configuración de acceso a datos y resolución de cadena de conexión.
/// 
/// AUDITORÍA - "GOTCHA" DE APP.CONFIG:
/// En arquitecturas .NET con bibliotecas de clases (Class Library), ConfigurationManager 
/// resuelve la configuración leyendo EXCLUSIVAMENTE el archivo de configuración del proceso ejecutable 
/// de inicio (WPF_SP/App.config).
/// Si un desarrollador coloca el App.config en la Class Library (Neptuno.Data), ConfigurationManager 
/// no lo encontrará en tiempo de ejecución y retornará null. Por ello, la cadena de conexión 'NeptunoConnection' 
/// vive en el proyecto ejecutable WPF_SP.
/// </summary>
public static class DbConfig
{
    private static string? _connectionString;

    public static string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                // Busca la cadena de conexión en el App.config del proyecto de inicio (WPF_SP)
                var configConn = ConfigurationManager.ConnectionStrings["NeptunoConnection"]?.ConnectionString;
                _connectionString = !string.IsNullOrWhiteSpace(configConn)
                    ? configConn
                    : @"Server=.\SQLEXPRESS;Database=NeptunoDB;Integrated Security=True;TrustServerCertificate=True;";
            }
            return _connectionString;
        }
        set => _connectionString = value;
    }
}

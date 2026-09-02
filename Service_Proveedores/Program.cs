using Service_Proveedores;
using SQLSIVEV.Infrastructure.Utils;
using System.Security.Principal;

const string EventSource = "Service_Proveedores";
if (args.Contains("--install-log", StringComparer.OrdinalIgnoreCase)) {
    bool correcto = SivevLogger.InicializarOrigen(EventSource);
    Console.WriteLine(correcto ? "EventSource instalado correctamente." : "No se pudo instalar el EventSource.");
    return;
}

Directory.CreateDirectory(@"C:\ProgramData\SIVEV");

File.AppendAllText(
    @"C:\ProgramData\SIVEV\Service_Proveedores_boot.txt",
    $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} | " +
    $"Usuario: {WindowsIdentity.GetCurrent().Name} | " +
    $"Versión: {Environment.Version}" +
    Environment.NewLine
);


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options => {
    options.ServiceName = "Service_Proveedores";
});

// MUY IMPORTANTE:
// AddWindowsService agrega automáticamente EventLog.
// Nosotros usamos SivevLogger.
builder.Logging.ClearProviders();
builder.Services.AddHostedService<Worker>();
IHost host = builder.Build();
host.Run();
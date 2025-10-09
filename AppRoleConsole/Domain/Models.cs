namespace AppRoleConsole.Domain.Models;

public sealed class SpAppRollClaveGetResult {
    public int ReturnCode { get; init; }
    public int MensajeId { get; init; }
    public short Resultado { get; init; }
    public string FuncionAplicacion { get; init; } = "";
    public string ClaveAcceso { get; init; } = "";
}


public sealed class CredencialExisteHuellaResult {
    public int MensajeId { get; init; }
    public short Resultado { get; init; }
    public bool ExisteHuella { get; init; }
    public byte[] Huella { get; init; } = Array.Empty<byte>();
}
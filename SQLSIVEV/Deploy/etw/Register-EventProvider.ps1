# 1) Limpiar canal viejo si existía
wevtutil del-log "SIVEV/Visual" 2>$null

# 2) Importar el manifest corregido (ajusta la ruta)
wevtutil im "H:\Apps C#\AppRoleConsole\SQLSIVEV\Deploy\etw\SivevVisual.man"

# 3) Refuerza ACL (por si el 'access=' del .man no se aplica en tu build)
wevtutil sl "SIVEV" /e:true `
  /ca:"O:BAG:SYD:(A;;0x3;;;SY)(A;;0x3;;;BA)(A;;0x3;;;BU)"

# 4) Verifica
wevtutil gli "SIVEV"




## INSTALACION  

# 1) Crear el log y la fuente de eventos para Serilog (Event Log clásico)
#if (-not [System.Diagnostics.EventLog]::SourceExists("SIVEV")) {
#    New-EventLog -LogName "SIVEV" -Source "SIVEV"
#}

# 2) Habilitar log y dar permisos a usuarios estándar
#wevtutil sl "SIVEV" /e:true `
#  /ca:"O:BAG:SYD:(A;;0x3;;;SY)(A;;0x3;;;BA)(A;;0x3;;;BU)"

# (Opcional) 3) Registrar tu proveedor ETW si usas EventSource/manifest
# wevtutil im "RutaAlManifest\Sivev.man"


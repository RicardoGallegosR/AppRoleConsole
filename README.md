# AppRoleConsole

# Ejecuta PowerShell como ADMIN
Para el visor de eventos
```
$src = 'SIVEV'     # Origen
$log = 'SIVEV'     # Nombre del Log (aparece como "Registros de Windows\SIVEV")

# Rutas del message file .NET (elige 64 o 32 según exista)
$mf64 = "$env:WinDir\Microsoft.NET\Framework64\v4.0.30319\EventLogMessages.dll"
$mf32 = "$env:WinDir\Microsoft.NET\Framework\v4.0.30319\EventLogMessages.dll"
$mf = $null
if (Test-Path $mf64) { $mf = $mf64 } else { $mf = $mf32 }

# Crear el origen si no existe, asignando el message file
if (-not [System.Diagnostics.EventLog]::SourceExists($src)) {
    $data = New-Object System.Diagnostics.EventSourceCreationData($src, $log)
    $data.MessageResourceFile = $mf
    [System.Diagnostics.EventLog]::CreateEventSource($data)
} else {
    # Reparar el origen existente (agregar MessageFile/TypesSupported si faltan)
    $key = "HKLM:\SYSTEM\CurrentControlSet\Services\EventLog\$log\$src"
    if (Test-Path $key) {
        Set-ItemProperty -Path $key -Name EventMessageFile -Value $mf
        Set-ItemProperty -Path $key -Name TypesSupported -Value 7 -Type DWord
    } else {
        # Si por alguna razón falta la clave, recrea el origen correctamente
        [System.Diagnostics.EventLog]::DeleteEventSource($src)
        $data = New-Object System.Diagnostics.EventSourceCreationData($src, $log)
        $data.MessageResourceFile = $mf
        [System.Diagnostics.EventLog]::CreateEventSource($data)
    }
}

Write-Host "Listo. Cierra y vuelve a abrir el Visor de eventos (eventvwr.msc)."
```

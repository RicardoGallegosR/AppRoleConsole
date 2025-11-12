param(
  [string]$ManifestPath = "$PSScriptRoot\SivevVisual.man"
)

Write-Host "Eliminando proveedor ETW de $ManifestPath ..."
wevtutil um "$ManifestPath"
Write-Host "Proveedor eliminado."

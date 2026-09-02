using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps_Regedit.Services {
    public sealed class ActivadorBatCreator {
        public const string DefaultFolder = @"C:\Program Files (x86)\Bin";
        public string FolderPath { get; }
        public string FileName { get; }
        public string ApplicationUrl { get; }

        public string FullPath =>  Path.Combine(FolderPath, FileName);

        public ActivadorBatCreator(string applicationUrl, string fileName = "DPDevTS.bat", string? folderPath = null) {
            if (string.IsNullOrWhiteSpace(applicationUrl))
                throw new ArgumentException("La URL de la aplicación no puede estar vacía.",  nameof(applicationUrl));

            ApplicationUrl = applicationUrl;
            FileName = string.IsNullOrWhiteSpace(fileName) ? "DPDevTS.bat" : fileName;
            FolderPath = string.IsNullOrWhiteSpace(folderPath) ? DefaultFolder : folderPath;
        }

        public BatCreationResult CreateOrUpdate() {
            try {
                Directory.CreateDirectory(FolderPath);
                File.WriteAllText(FullPath, BuildContent(), new UTF8Encoding(false));
                return new BatCreationResult(true, FullPath, "Activador creado o actualizado correctamente.");
            } catch (UnauthorizedAccessException ex) {
                return new BatCreationResult(false, FullPath, $"Acceso denegado al crear el activador: {ex.Message}");
            } catch (Exception ex) {
                return new BatCreationResult(false, FullPath, $"Error al crear el activador: {ex.Message}");
            }
        }

        public bool Exists() => File.Exists(FullPath);

        private string BuildContent() {
            var sb = new StringBuilder();
            sb.AppendLine("@echo off");
            sb.AppendLine( $@"rundll32.exe dfshim.dll,ShOpenVerbApplication {ApplicationUrl}");
            sb.AppendLine();
            sb.AppendLine(@"taskkill /f /im explorer.exe");
            return sb.ToString();
        }
    }


    public sealed class BatCreationResult {
        public bool Success { get; }
        public string FilePath { get; }
        public string Message { get; }

        public BatCreationResult(bool success, string filePath, string message) {
            Success = success;
            FilePath = filePath;
            Message = message;
        }
    }
}

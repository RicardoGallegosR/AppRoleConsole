using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Apps_Administrativa.Paneles.FisicoMecanica.FisicoMecanicaRegistro;

namespace Apps_Administrativa.Paneles.FisicoMecanica {
    public enum TipoEstatus : byte {
        Aprobado = 0,
        Cancelado = 1,
        Rechazado = 2
    }
    public enum TipoServicio : byte {
        Taxi = 0,
        Aplicacion = 1
    }
    public enum TipoN : byte {
        SinDefecto = 0,
        DefectoLeve = 1,
        DefectoGrave = 2,
        DefectoCritico = 3
    }
    public sealed class ResultadoRevision {
        public TipoN N1 { get; set; }
        public TipoN N2 { get; set; }
        public TipoN N3 { get; set; }
    }
    public sealed partial class FisicoMecanicaRegistro {
        public short Centro { get; set; } = 0;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; } = DateTime.Today;
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan Hora { get; set; } = DateTime.Now.TimeOfDay;
        public int Semana  => ISOWeek.GetWeekOfYear(Fecha);

        [Required(ErrorMessage = "La placa es obligatoria.")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{5,10}$", ErrorMessage = "La placa contiene caracteres no válidos.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "El VIN es obligatorio.")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "El VIN debe contener 17 caracteres válidos y no incluir I, O o Q.")]
        public string VIN { get; set; } = string.Empty;
        public bool Participa { get; set; } = true;
        public TipoServicio Servicio { get; set; }
        public int Folio { get; set; } = 0;
        public TipoEstatus Estatus { get; set; }


        public ResultadoRevision Direccion { get; set; } = new();
        public TipoN DireccionN1 { get => Direccion.N1; set => Direccion.N1 = value; } 
        public TipoN DireccionN2 { get => Direccion.N2; set => Direccion.N2 = value; } 
        public TipoN DireccionN3 { get => Direccion.N3; set => Direccion.N3 = value; }


        public ResultadoRevision Suspension { get; set; } = new();
        public TipoN SuspensionN1 { get => Suspension.N1; set => Suspension.N1 = value; }
        public TipoN SuspensionN2 { get => Suspension.N2; set => Suspension.N2 = value; }
        public TipoN SuspensionN3 { get => Suspension.N3; set => Suspension.N3 = value; }


        public ResultadoRevision Frenos { get; set; } = new();
        public TipoN FrenosN1 { get => Frenos.N1; set => Frenos.N1 = value; }
        public TipoN FrenosN2 { get => Frenos.N2; set => Frenos.N2 = value; }
        public TipoN FrenosN3 { get => Frenos.N3; set => Frenos.N3 = value; }

        public ResultadoRevision EquipoDeSeguridad { get; set; } = new();
        public TipoN EquipoDeSeguridadN1 { get => EquipoDeSeguridad.N1; set => EquipoDeSeguridad.N1 = value; }
        public TipoN EquipoDeSeguridadN2 { get => EquipoDeSeguridad.N2; set => EquipoDeSeguridad.N2 = value; }
        public TipoN EquipoDeSeguridadN3 { get => EquipoDeSeguridad.N3; set => EquipoDeSeguridad.N3 = value; }


        public ResultadoRevision ParabrisasYLimpiaparabrisas { get; set; } = new();
        public TipoN ParabrisasYLimpiaparabrisasN1 { get => ParabrisasYLimpiaparabrisas.N1; set => ParabrisasYLimpiaparabrisas.N1 = value; }
        public TipoN ParabrisasYLimpiaparabrisasN2 { get => ParabrisasYLimpiaparabrisas.N2; set => ParabrisasYLimpiaparabrisas.N2 = value; }
        public TipoN ParabrisasYLimpiaparabrisasN3 { get => ParabrisasYLimpiaparabrisas.N3; set => ParabrisasYLimpiaparabrisas.N3 = value; }


        public ResultadoRevision CristalesLateralesYTrasero { get; set; } = new();
        public TipoN CristalesLateralesYTraseroN1 { get => CristalesLateralesYTrasero.N1; set => CristalesLateralesYTrasero.N1 = value; }
        public TipoN CristalesLateralesYTraseroN2 { get => CristalesLateralesYTrasero.N2; set => CristalesLateralesYTrasero.N2 = value; }
        public TipoN CristalesLateralesYTraseroN3 { get => CristalesLateralesYTrasero.N3; set => CristalesLateralesYTrasero.N3 = value; }


        public ResultadoRevision LucesDelanteras { get; set; } = new();
        public TipoN LucesDelanterasN1 { get => LucesDelanteras.N1; set => LucesDelanteras.N1 = value; }
        public TipoN LucesDelanterasN2 { get => LucesDelanteras.N2; set => LucesDelanteras.N2 = value; }
        public TipoN LucesDelanterasN3 { get => LucesDelanteras.N3; set => LucesDelanteras.N3 = value; }


        public ResultadoRevision LucesTraseras { get; set; } = new();
        public TipoN LucesTraserasN1 { get => LucesTraseras.N1; set => LucesTraseras.N1 = value; }
        public TipoN LucesTraserasN2 { get => LucesTraseras.N2; set => LucesTraseras.N2 = value; }
        public TipoN LucesTraserasN3 { get => LucesTraseras.N3; set => LucesTraseras.N3 = value; }



        public ResultadoRevision Carroceria { get; set; } = new();
        public TipoN CarroceriaN1 { get => Carroceria.N1; set => Carroceria.N1 = value; }
        public TipoN CarroceriaN2 { get => Carroceria.N2; set => Carroceria.N2 = value; }
        public TipoN CarroceriaN3 { get => Carroceria.N3; set => Carroceria.N3 = value; }


        public ResultadoRevision AireAcondicionado { get; set; } = new();
        public TipoN AireAcondicionadoN1 { get => AireAcondicionado.N1; set => AireAcondicionado.N1 = value; }
        public TipoN AireAcondicionadoN2 { get => AireAcondicionado.N2; set => AireAcondicionado.N2 = value; }
        public TipoN AireAcondicionadoN3 { get => AireAcondicionado.N3; set => AireAcondicionado.N3 = value; }



        public ResultadoRevision Llantas { get; set; } = new();
        public TipoN LlantasN1 { get => Llantas.N1; set => Llantas.N1 = value; }
        public TipoN LlantasN2 { get => Llantas.N2; set => Llantas.N2 = value; }
        public TipoN LlantasN3 { get => Llantas.N3; set => Llantas.N3 = value; }


        public ResultadoRevision Puertas { get; set; } = new();
        public TipoN PuertasN1 { get => Puertas.N1; set => Puertas.N1 = value; }
        public TipoN PuertasN2 { get => Puertas.N2; set => Puertas.N2 = value; }
        public TipoN PuertasN3 { get => Puertas.N3; set => Puertas.N3 = value; }
        
        public ResultadoRevision Taximetro { get; set; } = new();
        public TipoN TaximetroN1 { get => Taximetro.N1; set => Taximetro.N1 = value; }
        public TipoN TaximetroN2 { get => Taximetro.N2; set => Taximetro.N2 = value; }
        public TipoN TaximetroN3 { get => Taximetro.N3; set => Taximetro.N3 = value; }
    }
}

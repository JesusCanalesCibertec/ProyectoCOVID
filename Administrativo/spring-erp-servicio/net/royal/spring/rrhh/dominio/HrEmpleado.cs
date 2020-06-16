using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

    /**
     * 
     * 
     * @tabla dbo.HR_Empleado
*/
    [Table("HR_EMPLEADO")]
    public class HrEmpleado : HrEmpleadoPk
    {

        [Display(Name = "Postulante")]
        [MaxLength(10)]
        [Column("POSTULANTE")]
        public String Postulante { get; set; }


        [Display(Name = "NumeroContrato")]
        [MaxLength(10)]
        [Column("NUMEROCONTRATO")]
        public String Numerocontrato { get; set; }

        [Display(Name = "FechaEstadoCivil")]
        [Column("FECHAESTADOCIVIL")]
        public Nullable<DateTime> Fechaestadocivil { get; set; }

        [Display(Name = "FechaDefuncion")]
        [Column("FECHADEFUNCION")]
        public Nullable<DateTime> Fechadefuncion { get; set; }

        [Display(Name = "NivelGerencia")]
        [MaxLength(1)]
        [Column("NIVELGERENCIA")]
        public String Nivelgerencia { get; set; }

        [Display(Name = "GrupoSanguineo")]
        [MaxLength(4)]
        [Column("GRUPOSANGUINEO")]
        public String Gruposanguineo { get; set; }

        [Display(Name = "Peso")]
        [Column("PESO")]
        public Nullable<Single> Peso { get; set; }

        [Display(Name = "Talla")]
        [Column("TALLA")]
        public Nullable<Single> Talla { get; set; }

        [Display(Name = "OtroEstadoCivil")]
        [MaxLength(30)]
        [Column("OTROESTADOCIVIL")]
        public String Otroestadocivil { get; set; }

        [Display(Name = "SituacionDomicilio")]
        [MaxLength(1)]
        [Column("SITUACIONDOMICILIO")]
        public String Situaciondomicilio { get; set; }

        [Display(Name = "OtraSituacionDomicilio")]
        [MaxLength(30)]
        [Column("OTRASITUACIONDOMICILIO")]
        public String Otrasituaciondomicilio { get; set; }

        [Display(Name = "Impedimentos")]
        [MaxLength(100)]
        [Column("IMPEDIMENTOS")]
        public String Impedimentos { get; set; }

        [Display(Name = "FlagActBeneficas")]
        [MaxLength(1)]
        [Column("FLAGACTBENEFICAS")]
        public String Flagactbeneficas { get; set; }

        [Display(Name = "ActBeneficas")]
        [MaxLength(30)]
        [Column("ACTBENEFICAS")]
        public String Actbeneficas { get; set; }

        [Display(Name = "FlagActCulturales")]
        [MaxLength(1)]
        [Column("FLAGACTCULTURALES")]
        public String Flagactculturales { get; set; }

        [Display(Name = "ActCulturales")]
        [MaxLength(30)]
        [Column("ACTCULTURALES")]
        public String Actculturales { get; set; }

        [Display(Name = "FlagActReligiosas")]
        [MaxLength(1)]
        [Column("FLAGACTRELIGIOSAS")]
        public String Flagactreligiosas { get; set; }

        [Display(Name = "ActReligiosas")]
        [MaxLength(30)]
        [Column("ACTRELIGIOSAS")]
        public String Actreligiosas { get; set; }

        [Display(Name = "FlagActLaborales")]
        [MaxLength(1)]
        [Column("FLAGACTLABORALES")]
        public String Flagactlaborales { get; set; }

        [Display(Name = "ActLaborales")]
        [MaxLength(30)]
        [Column("ACTLABORALES")]
        public String Actlaborales { get; set; }

        [Display(Name = "FlagActDeportivas")]
        [MaxLength(1)]
        [Column("FLAGACTDEPORTIVAS")]
        public String Flagactdeportivas { get; set; }

        [Display(Name = "ActDeportivas")]
        [MaxLength(30)]
        [Column("ACTDEPORTIVAS")]
        public String Actdeportivas { get; set; }

        [Display(Name = "FlagActSociales")]
        [MaxLength(1)]
        [Column("FLAGACTSOCIALES")]
        public String Flagactsociales { get; set; }

        [Display(Name = "ActSociales")]
        [MaxLength(30)]
        [Column("ACTSOCIALES")]
        public String Actsociales { get; set; }

        [Display(Name = "InformacionAdicional")]
        [MaxLength(200)]
        [Column("INFORMACIONADICIONAL")]
        public String Informacionadicional { get; set; }

        [Display(Name = "FlagTrabajoFuera")]
        [MaxLength(1)]
        [Column("FLAGTRABAJOFUERA")]
        public String Flagtrabajofuera { get; set; }

        [Display(Name = "FlagDeConfianza")]
        [MaxLength(1)]
        [Column("FLAGDECONFIANZA")]
        public String Flagdeconfianza { get; set; }

        [Display(Name = "FlagPrestamoVacacional")]
        [MaxLength(1)]
        [Column("FLAGPRESTAMOVACACIONAL")]
        public String Flagprestamovacacional { get; set; }

        [Display(Name = "FlagBonificacion")]
        [MaxLength(1)]
        [Column("FLAGBONIFICACION")]
        public String Flagbonificacion { get; set; }

        [Display(Name = "FlagPrestacionSalud")]
        [MaxLength(1)]
        [Column("FLAGPRESTACIONSALUD")]
        public String Flagprestacionsalud { get; set; }

        [Display(Name = "FechaAcumulacion")]
        [Column("FECHAACUMULACION")]
        public Nullable<DateTime> Fechaacumulacion { get; set; }

        [Display(Name = "GradoInstruccion")]
        [Column("GRADOINSTRUCCION")]
        public Nullable<Int32> Gradoinstruccion { get; set; }

        [Display(Name = "Profesion")]
        [Column("PROFESION")]
        public Nullable<Int32> Profesion { get; set; }

        [Display(Name = "Anexo")]
        [MaxLength(10)]
        [Column("ANEXO")]
        public String Anexo { get; set; }

        [Display(Name = "Estamento")]
        [Column("ESTAMENTO")]
        public Nullable<Int32> Estamento { get; set; }

        [Display(Name = "Vigencia")]
        [MaxLength(4)]
        [Column("VIGENCIA")]
        public String Vigencia { get; set; }

        [Display(Name = "Gerencia")]
        [Column("GERENCIA")]
        public Nullable<Int32> Gerencia { get; set; }

        [Display(Name = "Subgerencia")]
        [Column("SUBGERENCIA")]
        public Nullable<Int32> Subgerencia { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(255)]
        [Column("OBSERVACIONES")]
        public String Observaciones { get; set; }

        [Display(Name = "Posicion")]
        [Column("POSICION")]
        public Nullable<Int32> Posicion { get; set; }

        [Display(Name = "ProvinciaNacimiento")]
        [MaxLength(3)]
        [Column("PROVINCIANACIMIENTO")]
        public String Provincianacimiento { get; set; }

        [Display(Name = "GastoPlanilla")]
        [Column("GASTOPLANILLA")]
        public Nullable<Int32> Gastoplanilla { get; set; }

        [Display(Name = "DistritoNacimiento")]
        [MaxLength(3)]
        [Column("DISTRITONACIMIENTO")]
        public String Distritonacimiento { get; set; }

        [Display(Name = "RegistroFiscal")]
        [Column("REGISTROFISCAL")]
        public Nullable<Int32> Registrofiscal { get; set; }

        [Display(Name = "LicenciaConducirFecha")]
        [Column("LICENCIACONDUCIRFECHA")]
        public Nullable<DateTime> Licenciaconducirfecha { get; set; }

        [Display(Name = "PaisNacimiento")]
        [MaxLength(3)]
        [Column("PAISNACIMIENTO")]
        public String Paisnacimiento { get; set; }

        [Display(Name = "NombreCarnet")]
        [MaxLength(100)]
        [Column("NOMBRECARNET")]
        public String Nombrecarnet { get; set; }

        [Display(Name = "CarnetSeguroSocialNA")]
        [MaxLength(25)]
        [Column("CARNETSEGUROSOCIALNA")]
        public String Carnetsegurosocialna { get; set; }



        [Display(Name = "DireccionExtranjera")]
        [MaxLength(100)]
        [Column("DIRECCIONEXTRANJERA")]
        public String DireccionExtranjera { get; set; }

        [Display(Name = "PaisExtranjero")]
        [MaxLength(3)]
        [Column("PaisExtranjero")]
        public String PaisExtranjero { get; set; }

        [Display(Name = "DepartamentoExtranjero")]
        [MaxLength(3)]
        [Column("DepartamentoExtranjero")]
        public String DepartamentoExtranjero { get; set; }

        [Display(Name = "ProvinciaExtranjera")]
        [MaxLength(3)]
        [Column("ProvinciaExtranjera")]
        public String ProvinciaExtranjera { get; set; }

        [Display(Name = "CodigoPostalExtranjero")]
        [MaxLength(3)]
        [Column("CodigoPostalExtranjero")]
        public String CodigoPostalExtranjero { get; set; }

        [Display(Name = "TelefonoExtranjero")]
        [MaxLength(15)]
        [Column("TelefonoExtranjero")]
        public String TelefonoExtranjero { get; set; }

        [Display(Name = "CelularExtranjero")]
        [MaxLength(15)]
        [Column("CelularExtranjero")]
        public String CelularExtranjero { get; set; }




        [Display(Name = "NombreEmergencia")]
        [MaxLength(50)]
        [Column("NOMBREEMERGENCIA")]
        public String Nombreemergencia { get; set; }

        [Display(Name = "DireccionEmergencia")]
        [MaxLength(60)]
        [Column("DIRECCIONEMERGENCIA")]
        public String Direccionemergencia { get; set; }

        [Display(Name = "TelefonoEmergencia")]
        [MaxLength(60)]
        [Column("TELEFONOEMERGENCIA")]
        public String Telefonoemergencia { get; set; }

        [Display(Name = "ParentescoEmergencia")]
        [MaxLength(10)]
        [Column("PARENTESCOEMERGENCIA")]
        public String Parentescoemergencia { get; set; }

        [Display(Name = "PuntoOrigen")]
        [MaxLength(50)]
        [Column("PUNTOORIGEN")]
        public String Puntoorigen { get; set; }

        [Display(Name = "CelularEmergencia")]
        [MaxLength(15)]
        [Column("CELULAREMERGENCIA")]
        public String Celularemergencia { get; set; }

        [Display(Name = "ApellidoCasada")]
        [MaxLength(40)]
        [Column("APELLIDOCASADA")]
        public String Apellidocasada { get; set; }

        [Display(Name = "ApellidoCarnet")]
        [MaxLength(40)]
        [Column("APELLIDOCARNET")]
        public String Apellidocarnet { get; set; }

        [Display(Name = "PuestoCarnet")]
        [MaxLength(40)]
        [Column("PUESTOCARNET")]
        public String Puestocarnet { get; set; }

        [Display(Name = "FlagLicenciaEnsenanza")]
        [MaxLength(1)]
        [Column("FLAGLICENCIAENSENANZA")]
        public String Flaglicenciaensenanza { get; set; }

        [Display(Name = "LugarLicenciaEnsenanza")]
        [MaxLength(50)]
        [Column("LUGARLICENCIAENSENANZA")]
        public String Lugarlicenciaensenanza { get; set; }

        [Display(Name = "CodigoMacSchool")]
        [MaxLength(20)]
        [Column("CODIGOMACSCHOOL")]
        public String Codigomacschool { get; set; }

        [Display(Name = "FlagObservadorAula")]
        [MaxLength(1)]
        [Column("FLAGOBSERVADORAULA")]
        public String Flagobservadoraula { get; set; }

        [Display(Name = "FlagDocencia")]
        [MaxLength(1)]
        [Column("FLAGDOCENCIA")]
        public String Flagdocencia { get; set; }

        [Display(Name = "PoseeAuto")]
        [MaxLength(1)]
        [Column("POSEEAUTO")]
        public String Poseeauto { get; set; }

        [Display(Name = "Donante")]
        [MaxLength(1)]
        [Column("DONANTE")]
        public String Donante { get; set; }

        [Display(Name = "FechaIngresoOrganizacion")]
        [Column("FECHAINGRESOORGANIZACION")]
        public Nullable<DateTime> Fechaingresoorganizacion { get; set; }

        [Display(Name = "CarnetSeguroSocialEX")]
        [MaxLength(25)]
        [Column("CARNETSEGUROSOCIALEX")]
        public String Carnetsegurosocialex { get; set; }

        [Display(Name = "FlagRCM")]
        [MaxLength(1)]
        [Column("FLAGRCM")]
        public String Flagrcm { get; set; }

        [Display(Name = "FechaSMF")]
        [Column("FECHASMF")]
        public Nullable<DateTime> Fechasmf { get; set; }

        [Display(Name = "FlagSMF")]
        [MaxLength(1)]
        [Column("FLAGSMF")]
        public String Flagsmf { get; set; }

        [Display(Name = "LugarExpedicionCedula")]
        [MaxLength(20)]
        [Column("LUGAREXPEDICIONCEDULA")]
        public String Lugarexpedicioncedula { get; set; }

        [Display(Name = "AlergicoA")]
        [MaxLength(100)]
        [Column("ALERGICOA")]
        public String Alergicoa { get; set; }

        [Display(Name = "MedicamentoEmergencia")]
        [MaxLength(50)]
        [Column("MEDICAMENTOEMERGENCIA")]
        public String Medicamentoemergencia { get; set; }

        [Display(Name = "PadeceDe")]
        [MaxLength(100)]
        [Column("PADECEDE")]
        public String Padecede { get; set; }

        [Display(Name = "TipoVivienda")]
        [MaxLength(1)]
        [Column("TIPOVIVIENDA")]
        public String Tipovivienda { get; set; }

        [Display(Name = "DepartamentoNacimiento")]
        [MaxLength(3)]
        [Column("DEPARTAMENTONACIMIENTO")]
        public String Departamentonacimiento { get; set; }

        [Display(Name = "NumeroRegistroCivil")]
        [MaxLength(15)]
        [Column("NUMEROREGISTROCIVIL")]
        public String Numeroregistrocivil { get; set; }

        [Display(Name = "NombreNotaria")]
        [MaxLength(100)]
        [Column("NOMBRENOTARIA")]
        public String Nombrenotaria { get; set; }

        [Display(Name = "TipoRemuneracion")]
        [MaxLength(4)]
        [Column("TIPOREMUNERACION")]
        public String Tiporemuneracion { get; set; }

        [Display(Name = "AlivioTributario")]
        [MaxLength(3)]
        [Column("ALIVIOTRIBUTARIO")]
        public String Aliviotributario { get; set; }

        [Display(Name = "MediSalud")]
        [Column("MEDISALUD")]
        public Nullable<Decimal> Medisalud { get; set; }

        [Display(Name = "SeguroLiberty")]
        [Column("SEGUROLIBERTY")]
        public Nullable<Decimal> Seguroliberty { get; set; }

        [Display(Name = "PolizaHC")]
        [Column("POLIZAHC")]
        public Nullable<Decimal> Polizahc { get; set; }

        [Display(Name = "FlagSeguroLiberty")]
        [MaxLength(1)]
        [Column("FLAGSEGUROLIBERTY")]
        public String Flagseguroliberty { get; set; }

        [Display(Name = "FlagPolizaHC")]
        [MaxLength(1)]
        [Column("FLAGPOLIZAHC")]
        public String Flagpolizahc { get; set; }

        [Display(Name = "FechaSeguroLiberty")]
        [Column("FECHASEGUROLIBERTY")]
        public Nullable<DateTime> Fechaseguroliberty { get; set; }

        [Display(Name = "FechaPolizaHC")]
        [Column("FECHAPOLIZAHC")]
        public Nullable<DateTime> Fechapolizahc { get; set; }

        [Display(Name = "FechaAyudaEducativa")]
        [Column("FECHAAYUDAEDUCATIVA")]
        public Nullable<DateTime> Fechaayudaeducativa { get; set; }

        [Display(Name = "AyudaEducativa")]
        [Column("AYUDAEDUCATIVA")]
        public Nullable<Decimal> Ayudaeducativa { get; set; }

        [Display(Name = "MilitarClase")]
        [MaxLength(15)]
        [Column("MILITARCLASE")]
        public String Militarclase { get; set; }

        [Display(Name = "MontoAlivioTributario")]
        [Column("MONTOALIVIOTRIBUTARIO")]
        public Nullable<Decimal> Montoaliviotributario { get; set; }

        [Display(Name = "TieneAntecedentesPenales")]
        [MaxLength(1)]
        [Column("TIENEANTECEDENTESPENALES")]
        public String Tieneantecedentespenales { get; set; }

        [Display(Name = "TieneAntecedentespoliciales")]
        [MaxLength(1)]
        [Column("TIENEANTECEDENTESPOLICIALES")]
        public String Tieneantecedentespoliciales { get; set; }

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public HrEmpleado()
        {
        }
    }
}

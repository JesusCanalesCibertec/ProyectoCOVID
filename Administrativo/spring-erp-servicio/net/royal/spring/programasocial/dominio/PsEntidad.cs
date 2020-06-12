using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_ENTIDAD
*/
    [Table("PS_ENTIDAD", Schema = "sgseguridadsys")]
    public class PsEntidad : PsEntidadPk
    {

        [Display(Name = "APELLIDO_PATERNO")]
        [MaxLength(30)]
        [Column("APELLIDO_PATERNO")]
        public String ApellidoPaterno { get; set; }

        [Display(Name = "APELLIDO_MATERNO")]
        [MaxLength(30)]
        [Column("APELLIDO_MATERNO")]
        public String ApellidoMaterno { get; set; }

        [Display(Name = "NOMBRES")]
        [MaxLength(30)]
        [Column("NOMBRES")]
        public String Nombres { get; set; }

        [Display(Name = "NOMBRECOMPLETO")]
        [MaxLength(90)]
        [Column("NOMBRECOMPLETO")]
        public String Nombrecompleto { get; set; }

        [Display(Name = "ID_NACIMIENTO_PAIS")]
        [MaxLength(3)]
        [Column("ID_NACIMIENTO_PAIS")]
        public String IdNacimientoPais { get; set; }

        [Display(Name = "ID_NACIMIENTO_UBIGEO")]
        [MaxLength(6)]
        [Column("ID_NACIMIENTO_UBIGEO")]
        public String IdNacimientoUbigeo { get; set; }

        [Display(Name = "ID_ESTADO_CIVIL")]
        [MaxLength(1)]
        [Column("ID_ESTADO_CIVIL")]
        public String IdEstadoCivil { get; set; }

        [Display(Name = "ID_SEXO")]
        [MaxLength(1)]
        [Column("ID_SEXO")]
        public String IdSexo { get; set; }

        [Display(Name = "ID_DISCAPACIDAD")]
        [MaxLength(2)]
        [Column("ID_DISCAPACIDAD")]
        public String IdDiscapacidad { get; set; }

        [Display(Name = "ID_AREA_TRABAJO")]
        [MaxLength(10)]
        [Column("ID_AREA_TRABAJO")]
        public String IdAreaTrabajo { get; set; }

        [Display(Name = "FECHA_NACIMIENTO")]
        [Column("FECHA_NACIMIENTO")]
        public Nullable<DateTime> FechaNacimiento { get; set; }

        [Display(Name = "EDAD")]
        [Column("EDAD")]
        public Nullable<Int32> Edad { get; set; }

        [Column("EDAD_MESES")]
        public Nullable<Int32> EdadMeses { get; set; }

        [Display(Name = "ID_GRUPO_SANGUINEO")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_GRUPO_SANGUINEO")]
        public String IdGrupoSanguineo { get; set; }

        [Display(Name = "ID_FACTOR_RH")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_FACTOR_RH")]
        public String IdFactorRh { get; set; }

        [Display(Name = "ID_TIPO_DOCUMENTO")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_TIPO_DOCUMENTO")]
        public String IdTipoDocumento { get; set; }

        [Display(Name = "DOCUMENTO")]
        [MaxLength(20)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("DOCUMENTO")]
        public String Documento { get; set; }

        [Display(Name = "ID_CARNET_CONADIS")]
        [MaxLength(5)]
        [Column("ID_CARNET_CONADIS")]
        public String IdCarnetConadis { get; set; }

        [Display(Name = "COMENTARIOS")]
        [MaxLength(1000)]
        [Column("COMENTARIOS")]
        public String Comentarios { get; set; }

        [Display(Name = "CORREO1")]
        [MaxLength(50)]
        [Column("CORREO1")]
        public String Correo1 { get; set; }

        [Display(Name = "ID_NIVEL_ACADEMICO")]
        [MaxLength(5)]
        [Column("ID_NIVEL_ACADEMICO")]
        public String IdNivelAcademico { get; set; }

        [Display(Name = "ID_ESPECIALIDAD_ACADEMICA")]
        [MaxLength(5)]
        [Column("ID_ESPECIALIDAD_ACADEMICA")]
        public String IdEspecialidadAcademica { get; set; }

        [Display(Name = "DOMICILIO_ID_UBIGEO")]
        [MaxLength(6)]
        [Column("DOMICILIO_ID_UBIGEO")]
        public String DomicilioIdUbigeo { get; set; }

        [Display(Name = "DOMICILIO_DIRECCION")]
        [MaxLength(100)]
        [Column("DOMICILIO_DIRECCION")]
        public String DomicilioDireccion { get; set; }

        [Display(Name = "DOMICILIO_REFERENCIA")]
        [MaxLength(100)]
        [Column("DOMICILIO_REFERENCIA")]
        public String DomicilioReferencia { get; set; }

        [Display(Name = "TELEFONO1")]
        [MaxLength(10)]
        [Column("TELEFONO1")]
        public String Telefono1 { get; set; }

        [Display(Name = "TELEFONO2")]
        [MaxLength(10)]
        [Column("TELEFONO2")]
        public String Telefono2 { get; set; }

        [Display(Name = "ID_TENENCIA")]
        [MaxLength(5)]
        [Column("ID_TENENCIA")]
        public String IdTenencia { get; set; }

        [Display(Name = "ID_MATERIAL_CONSTRUCCION")]
        [MaxLength(5)]
        [Column("ID_MATERIAL_CONSTRUCCION")]
        public String IdMaterialConstruccion { get; set; }

        [Display(Name = "ID_SERVICIO_AGUA")]
        [MaxLength(5)]
        [Column("ID_SERVICIO_AGUA")]
        public String IdServicioAgua { get; set; }

        [Display(Name = "ID_SERVICIO_DESAGUE")]
        [MaxLength(5)]
        [Column("ID_SERVICIO_DESAGUE")]
        public String IdServicioDesague { get; set; }

        [Display(Name = "ID_SERVICIO_ELECTRICIDAD")]
        [MaxLength(5)]
        [Column("ID_SERVICIO_ELECTRICIDAD")]
        public String IdServicioElectricidad { get; set; }

        [Display(Name = "PADRE_NOMBRE")]
        [MaxLength(70)]
        [Column("PADRE_NOMBRE")]
        public String PadreNombre { get; set; }

        [Display(Name = "PADRE_ID_VIVE")]
        [MaxLength(3)]
        [Column("PADRE_ID_VIVE")]
        public String PadreIdVive { get; set; }

        [Display(Name = "PADRE_NRO_DOCUMENTO")]
        [MaxLength(15)]
        [Column("PADRE_NRO_DOCUMENTO")]
        public String PadreNroDocumento { get; set; }

        [Display(Name = "PADRE_ID_OCUPACION")]
        [MaxLength(5)]
        [Column("PADRE_ID_OCUPACION")]
        public String PadreIdOcupacion { get; set; }

        [Display(Name = "MADRE_NOMBRE")]
        [MaxLength(70)]
        [Column("MADRE_NOMBRE")]
        public String MadreNombre { get; set; }

        [Display(Name = "MADRE_ID_VIVE")]
        [MaxLength(3)]
        [Column("MADRE_ID_VIVE")]
        public String MadreIdVive { get; set; }

        [Display(Name = "MADRE_NRO_DOCUMENTO")]
        [MaxLength(15)]
        [Column("MADRE_NRO_DOCUMENTO")]
        public String MadreNroDocumento { get; set; }

        [Display(Name = "MADRE_ID_OCUPACION")]
        [MaxLength(5)]
        [Column("MADRE_ID_OCUPACION")]
        public String MadreIdOcupacion { get; set; }

        [Display(Name = "APODERADO_NOMBRE")]
        [MaxLength(70)]
        [Column("APODERADO_NOMBRE")]
        public String ApoderadoNombre { get; set; }

        [Display(Name = "APODERADO_NRO_DOCUMENTO")]
        [MaxLength(15)]
        [Column("APODERADO_NRO_DOCUMENTO")]
        public String ApoderadoNroDocumento { get; set; }

        [Display(Name = "APODERADO_ESPOSOA")]
        [MaxLength(70)]
        [Column("APODERADO_ESPOSOA")]
        public String ApoderadoEsposoa { get; set; }

        [MaxLength(15)]
        [Column("APODERADO_ESPOSOA_NRO_DOCUMENTO")]
        public String apoderadoEsposoaNroDocumento { get; set; }

        [Display(Name = "INGRESO_MENSUAL")]
        [Column("INGRESO_MENSUAL")]
        public Nullable<Decimal> IngresoMensual { get; set; }

        [Display(Name = "COMENTARIOS_REFERENCIA_FAMILIAR")]
        [MaxLength(1000)]
        [Column("COMENTARIOS_REFERENCIA_FAMILIAR")]
        public String ComentariosReferenciaFamiliar { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("FLG_PENSIONISTA")]
        public String flgPensionista { get; set; }

        [Column("FLG_FAMILIARES")]
        public String flgFamiliares { get; set; }

        [Display(Name = "CREACION_USUARIO")]
        [MaxLength(50)]
        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }

        [Display(Name = "CREACION_FECHA")]
        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }

        [Display(Name = "CREACION_TERMINAL")]
        [MaxLength(50)]
        [Column("CREACION_TERMINAL")]
        public String CreacionTerminal { get; set; }

        [Display(Name = "MODIFICACION_USUARIO")]
        [MaxLength(50)]
        [Column("MODIFICACION_USUARIO")]
        public String ModificacionUsuario { get; set; }

        [Display(Name = "MODIFICACION_FECHA")]
        [Column("MODIFICACION_FECHA")]
        public Nullable<DateTime> ModificacionFecha { get; set; }

        [Display(Name = "MODIFICACION_TERMINAL")]
        [MaxLength(50)]
        [Column("MODIFICACION_TERMINAL")]
        public String ModificacionTerminal { get; set; }

        [NotMapped]
        public PsBeneficiarioIngreso ingreso { get; set; }


        [NotMapped]
        public PsNutricion nutricion { get; set; }

        [NotMapped]
        public PsCapacidad capacidad { get; set; }

        [NotMapped]
        public PsSocioAmbiental socioAmbiental { get; set; }

        [NotMapped]
        public PsBeneficiario beneficiario { get; set; }

        [NotMapped]
        public PsSalud salud { get; set; }
        [NotMapped]
        public String auxInstitucion { get; set; }
        [NotMapped]
        public String auxInstitucionOrigen { get; set; }
        [NotMapped]
        public String auxPrograma { get; set; }
        [NotMapped]
        public String auxUbigeo { get; set; }
        [NotMapped]
        public String auxUbigeoConocido { get; set; }

        [NotMapped]
        public String EstadoBeneficiario { get; set; }

        [NotMapped]
        public List<PsEntidadPariente> listaPariente { get; set; }
        [NotMapped]
        public List<PsEntidadDocumento> listaDocumento { get; set; }
        [NotMapped]
        public List<PsEntidadEquipamiento> listaEquipamiento { get; set; }
        [NotMapped]
        public List<PsEntidadSeguroSocial> listaSeguro { get; set; }
        [NotMapped]
        public List<PsEntidadProgramaSocial> listaPrograma { get; set; }

        public PsEntidad()
        {
            socioAmbiental = new PsSocioAmbiental();
            beneficiario = new PsBeneficiario();
            salud = new PsSalud();
            capacidad = new PsCapacidad();
            nutricion = new PsNutricion();
            ingreso = new PsBeneficiarioIngreso();
            listaPariente = new List<PsEntidadPariente>();
            listaDocumento = new List<PsEntidadDocumento>();
            listaEquipamiento = new List<PsEntidadEquipamiento>();
            listaSeguro = new List<PsEntidadSeguroSocial>();
            listaPrograma = new List<PsEntidadProgramaSocial>();
        }
    }
}

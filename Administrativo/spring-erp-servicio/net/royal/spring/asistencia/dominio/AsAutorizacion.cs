using net.royal.spring.sistema.dominio.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.asistencia.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AS_Autorizacion
*/
    [Table("AS_AUTORIZACION")]
    public class AsAutorizacion // : AsAutorizacionPk
    {

        public static String MSG_ASAUTORIZACION_ESREQUERIDO = "No se envío la solicitud";

        public static String MSG_HASTA_ESREQUERIDO = "El campo HASTA no puede estar vacio";
        public static String MSG_TIPOAUTORIZACION_LONGITUD = "El campo TIPOAUTORIZACION solo permite {max} caracteres";
        public static String MSG_TIPOAUTORIZACION_ESREQUERIDO = "El campo TIPOAUTORIZACION no puede estar vacio";
        public static String MSG_AUTORIZADOPOR_LONGITUD = "El campo AUTORIZADOPOR solo permite {max} caracteres";
        public static String MSG_ESTADO_LONGITUD = "El campo ESTADO solo permite {max} caracteres";
        public static String MSG_ULTIMOUSUARIO_LONGITUD = "El campo ULTIMOUSUARIO solo permite {max} caracteres";
        public static String MSG_OBSERVACION_LONGITUD = "El campo OBSERVACION solo permite {max} caracteres";
        public static String MSG_FLAGPLANILLAS_LONGITUD = "El campo FLAGPLANILLAS solo permite {max} caracteres";
        public static String MSG_CODIGODIAGNOSTICO_LONGITUD = "El campo CODIGODIAGNOSTICO solo permite {max} caracteres";
        public static String MSG_CODIGOCENTROMEDICO_LONGITUD = "El campo CODIGOCENTROMEDICO solo permite {max} caracteres";
        public static String MSG_CITT_LONGITUD = "El campo CITT solo permite {max} caracteres";
        public static String MSG_FLAGREGULARIZADO_LONGITUD = "El campo FLAGREGULARIZADO solo permite {max} caracteres";
        public static String MSG_AUTOGENERADO_ESREQUERIDO = "El campo AUTOGENERADO no puede estar vacio";
        public static String MSG_SOLICITADOPOR_LONGITUD = "El campo SOLICITADOPOR solo permite {max} caracteres";
        public static String MSG_MOTIVORECHAZO_LONGITUD = "El campo MOTIVORECHAZO solo permite {max} caracteres";
        public static String MSG_FECHA_ESREQUERIDO = "El campo FECHA no puede estar vacio";
        public static String MSG_CONCEPTOACCESO_LONGITUD = "El campo CONCEPTOACCESO solo permite {max} caracteres";
        public static String MSG_DESDE_ESREQUERIDO = "El campo DESDE no puede estar vacio";



        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("EMPLEADO")]
        public Nullable<Decimal> Empleado { get; set; }


        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("FECHA")]
        public Nullable<DateTime> Fecha { get; set; }


        [Display(Name = "ConceptoAcceso")]
        [MaxLength(4)]
        [Column("CONCEPTOACCESO")]
        public String Conceptoacceso { get; set; }

        [Display(Name = "Desde")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("DESDE")]
        public Nullable<DateTime> Desde { get; set; }






        [Display(Name = "companyowner")]
        [MaxLength(10)]
        [Column("COMPANYOWNER")]
        public String Companyowner { get; set; }

        [NotMapped]
        [Display(Name = "AUTOGENERADO")]
        [Column("AUTOGENERADO")]
        public Nullable<Int32> Autogenerado { get; set; }

        [Display(Name = "TIPOAUTORIZACION")]
        [MaxLength(1)]
        [Column("TIPOAUTORIZACION")]
        public String Tipoautorizacion { get; set; }

        [Display(Name = "archivo")]
        [Column("archivo")]
        public String archivo { get; set; }

        [Display(Name = "FECHAFIN")]
        [Column("FECHAFIN")]
        public Nullable<DateTime> Fechafin { get; set; }

        [Display(Name = "HASTA")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("HASTA")]
        public Nullable<DateTime> Hasta { get; set; }

        [Display(Name = "AUTORIZADOPOR")]
        [MaxLength(20)]
        [Column("AUTORIZADOPOR")]
        public String Autorizadopor { get; set; }

        [Display(Name = "ID_TRANSACCION")]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }


        [Display(Name = "FECHAAUTORIZACION")]
        [Column("FECHAAUTORIZACION")]
        public Nullable<DateTime> Fechaautorizacion { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "REFRIGERIO")]
        [MaxLength(1)]
        [Column("REFRIGERIO")]
        public String Refrigerio { get; set; }

        [Display(Name = "PERIODO")]
        [MaxLength(6)]
        [Column("PERIODO")]
        public String Periodo { get; set; }

        [Display(Name = "MANDATORIO")]
        [MaxLength(1)]
        [Column("MANDATORIO")]
        public String Mandatorio { get; set; }

        [Display(Name = "OBSERVACION")]
        [MaxLength(100)]
        [Column("OBSERVACION")]
        public String Observacion { get; set; }

        [Display(Name = "CITT")]
        [MaxLength(16)]
        [Column("CITT")]
        public String Citt { get; set; }

        [Display(Name = "estadosolicitud")]
        [MaxLength(2)]
        [Column("ESTADOSOLICITUD")]
        public String Estadosolicitud { get; set; }

        [Display(Name = "SolicitadoPor")]
        [MaxLength(20)]
        [Column("SOLICITADOPOR")]
        public String Solicitadopor { get; set; }

        [Display(Name = "FechaSolicitud")]
        [Column("FECHASOLICITUD")]
        public Nullable<DateTime> Fechasolicitud { get; set; }

        [Display(Name = "SobretiempoPosicion")]
        [Column("SOBRETIEMPOPOSICION")]
        public Nullable<Int32> Sobretiempoposicion { get; set; }

        [Display(Name = "ComportamientoSobretiempo")]
        [MaxLength(1)]
        [Column("COMPORTAMIENTOSOBRETIEMPO")]
        public String Comportamientosobretiempo { get; set; }

        [Display(Name = "CategoriaAutorizacion")]
        [MaxLength(4)]
        [Column("CATEGORIAAUTORIZACION")]
        public String Categoriaautorizacion { get; set; }

        [Display(Name = "VisadoPor")]
        [MaxLength(20)]
        [Column("VISADOPOR")]
        public String Visadopor { get; set; }

        [Display(Name = "FechaVisado")]
        [Column("FECHAVISADO")]
        public Nullable<DateTime> Fechavisado { get; set; }

        [Display(Name = "FlagCorrido")]
        [MaxLength(1)]
        [Column("FLAGCORRIDO")]
        public String Flagcorrido { get; set; }

        [Display(Name = "MotivoRechazo")]
        [MaxLength(255)]
        [Column("MOTIVORECHAZO")]
        public String Motivorechazo { get; set; }

        [Display(Name = "TipoHorario")]
        [Column("TIPOHORARIO")]
        public Nullable<Int32> Tipohorario { get; set; }

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [Display(Name = "NivelAprobacion")]
        [Column("NIVELAPROBACION")]
        public Nullable<Int32> Nivelaprobacion { get; set; }

        [Display(Name = "CODIGOPROCESO")]
        [MaxLength(2)]
        [Column("CODIGOPROCESO")]
        public String Codigoproceso { get; set; }

        [Display(Name = "NUMEROPROCESO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Key]
        [Column("NUMEROPROCESO")]
     
        public Nullable<Int32> Numeroproceso { get; set; }

        [Display(Name = "MOTIVO_RECHAZO")]
        [MaxLength(5000)]
        [Column("MOTIVO_RECHAZO")]
        public String MotivoRechazo { get; set; }

        [NotMapped]
        public String AuxNombreEmpleado { get; set; }

        [NotMapped]
        public DtoSolicitud DtoSolicitud { get; set; }

        [NotMapped]
        public String auxArchivo { get; set; }

        [NotMapped]
        public Nullable<DateTime> AuxHoraInicio { get; set; }

        [NotMapped]
        public Nullable<DateTime> AuxHoraFin { get; set; }

        [NotMapped]
        public String AuxHorarioEmpleado { get; set; }

        [NotMapped]
        public Nullable<DateTime> AuxFechaNacimiento { get; set; }

        [NotMapped]
        public Nullable<DateTime> AuxFechaAniversario { get; set; }

        [NotMapped]
        public List<Int32> registrosGenerados { get; set; }

        [NotMapped]
        public bool conAdjunto { get; set; }

        [NotMapped]
        public bool habilitarhoraInicio { get; set; }
        [NotMapped]
        public bool habilitarhoraFin { get; set; }
        [NotMapped]
        public bool habilitarFechaInicio { get; set; }
        [NotMapped]
        public bool habilitarFechaFin { get; set; }
        [NotMapped]
        public bool verhoraFin { get; set; }
        [NotMapped]
        public bool verFechaFin { get; set; }
        [NotMapped]
        public bool verCompromiso { get; set; }
        [NotMapped]
        public bool verTodasHoras { get; set; }
        [NotMapped]
        public bool verAdjunto { get; set; }
        [NotMapped]
        public String auxComentario { get; set; }




        public AsAutorizacion()
        {
        }
    }
}

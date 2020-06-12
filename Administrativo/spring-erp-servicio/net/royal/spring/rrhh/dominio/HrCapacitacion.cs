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
     * @tabla dbo.HR_Capacitacion
*/
    [Table("HR_CAPACITACION")]
    public class HrCapacitacion : HrCapacitacionPk
    {

        [Column("NUMEROVACANTES")]
        public Nullable<Int32> Numerovacantes { get; set; }

        [Column("CURSO")]
        public Nullable<Int32> Curso { get; set; }

        [MaxLength(10)]
        [Column("NOMBREGRUPO")]
        public String Nombregrupo { get; set; }

        [MaxLength(1)]
        [Column("FLAGHORARIOINDIVIDUAL")]
        public String Flaghorarioindividual { get; set; }

        [MaxLength(255)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("ID_INSTITUCION")]
        public String IdInstitucion { get; set; }

        [Column("COSTOSUBTOTAL")]
        public Nullable<Double> Costosubtotal { get; set; }

        [Column("COSTOTOTAL")]
        public Nullable<Double> Costototal { get; set; }

        [Column("COSTOTOTALEXT")]
        public Nullable<Double> Costototalext { get; set; }

        [Column("COSTOSUBTOTALEXT")]
        public Nullable<Double> Costosubtotalext { get; set; }

        [Column("COSTOIMPUESTOSEXT")]
        public Nullable<Double> Costoimpuestosext { get; set; }

        [Column("COSTOIMPUESTOS")]
        public Nullable<Double> Costoimpuestos { get; set; }

        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [MaxLength(20)]
        [Column("PROYECTO")]
        public String Proyecto { get; set; }

        [Column("FECHADESDE")]
        public Nullable<DateTime> Fechadesde { get; set; }

        [Column("FECHAHASTA")]
        public Nullable<DateTime> Fechahasta { get; set; }

        [Column("HORADESDE")]
        public Nullable<DateTime> Horadesde { get; set; }

        [Column("HORAHASTA")]
        public Nullable<DateTime> Horahasta { get; set; }

        [MaxLength(6)]
        [Column("PERIODOINICIAL")]
        public String Periodoinicial { get; set; }

        [MaxLength(6)]
        [Column("PERIODOFINAL")]
        public String Periodofinal { get; set; }

        [MaxLength(4)]
        [Column("UNIDADNEGOCIO")]
        public String Unidadnegocio { get; set; }

        [Column("CENTROCAPACITACION")]
        public Nullable<Int32> Centrocapacitacion { get; set; }

        [Column("NUMEROPARTICIPANTES")]
        public Nullable<Int32> Numeroparticipantes { get; set; }

        [Column("TOTALDIAS")]
        public Nullable<Int32> Totaldias { get; set; }

        [Column("TOTALHORAS")]
        public Nullable<Int32> Totalhoras { get; set; }

        [MaxLength(2)]
        [Column("LOGISTICATIPOCOMPROMISO")]
        public String Logisticatipocompromiso { get; set; }

        [MaxLength(10)]
        [Column("LOGISTICANUMEROCOMPROMISO")]
        public String Logisticanumerocompromiso { get; set; }

        [MaxLength(1)]
        [Column("FLAGLOGISTICA")]
        public String Flaglogistica { get; set; }

        [MaxLength(1)]
        [Column("TIPOCURSO")]
        public String Tipocurso { get; set; }

        [Column("ASIGNADO")]
        public String asignado { get; set; }

        [Column("CERTIFICADO")]
        public String certificado { get; set; }

        [Column("FINANCIADOPS")]
        public String financiadoPs { get; set; }

        [MaxLength(100)]
        [Column("AULA")]
        public String Aula { get; set; }

        [MaxLength(50)]
        [Column("EXPOSITOR")]
        public String Expositor { get; set; }

        [Column("PLANCAPACITACION")]
        public Nullable<Int32> Plancapacitacion { get; set; }

        [MaxLength(20)]
        [Column("LUGARCAPACITACION")]
        public String Lugarcapacitacion { get; set; }

        [Column("SOLICITANTE")]
        public Nullable<Int32> Solicitante { get; set; }

        [MaxLength(60)]
        [Column("CURSODESCRIPCION")]
        public String Cursodescripcion { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION1")]
        public String Fundamentacion1 { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION2")]
        public String Fundamentacion2 { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION3")]
        public String Fundamentacion3 { get; set; }

        [Column("COSTOTOTALESTIMADOLOCAL")]
        public Nullable<Double> Costototalestimadolocal { get; set; }

        [Column("COSTOTOTALESTIMADOEXTRANJERO")]
        public Nullable<Double> Costototalestimadoextranjero { get; set; }

        [Column("COSTOUNITARIOLOCAL")]
        public Nullable<Double> Costounitariolocal { get; set; }

        [Column("COSTOUNITARIOEXTRANJERO")]
        public Nullable<Double> Costounitarioextranjero { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION4")]
        public String Fundamentacion4 { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION5")]
        public String Fundamentacion5 { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION6")]
        public String Fundamentacion6 { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION7")]
        public String Fundamentacion7 { get; set; }


        [MaxLength(500)]
        [Column("FUNDAMENTACION8")]
        public String Fundamentacion8 { get; set; }

        [MaxLength(500)]
        [Column("FUNDAMENTACION9")]
        public String Fundamentacion9 { get; set; }


        [MaxLength(500)]
        [Column("FUNDAMENTACION10")]
        public String Fundamentacion10 { get; set; }


        [MaxLength(30)]
        [Column("TELEFONOCONTACTO")]
        public String Telefonocontacto { get; set; }


        [Column("EMPLEADO")]
        public Nullable<Int32> Empleado { get; set; }


        [Column("MONTOMAXASUMIDO")]
        public Nullable<Double> Montomaxasumido { get; set; }


        [Column("MONTOASUMIDO")]
        public Nullable<Double> Montoasumido { get; set; }

        [MaxLength(10)]
        [Column("MODALIDAD")]
        public String Modalidad { get; set; }


        [MaxLength(10)]
        [Column("GRUPOOCUPACIONAL")]
        public String Grupoocupacional { get; set; }

        [MaxLength(4)]
        [Column("ANO")]
        public String Ano { get; set; }


        [Column("CAPACITADOR")]
        public Nullable<Double> Capacitador { get; set; }


        [MaxLength(10)]
        [Column("TIPO")]
        public String Tipo { get; set; }

        [MaxLength(10)]
        [Column("FINANCIAMIENTO")]
        public String Financiamiento { get; set; }


        [MaxLength(10)]
        [Column("BECAS")]
        public String Becas { get; set; }


        [Column("BENEFTOTLOCAL")]
        public Nullable<Double> Beneftotlocal { get; set; }


        [Column("BENEFTOTEXTRANJERO")]
        public Nullable<Double> Beneftotextranjero { get; set; }


        [Column("ANIOPLAN")]
        public Nullable<Decimal> Anioplan { get; set; }


        [Column("FECHASOLICITUD")]
        public Nullable<DateTime> Fechasolicitud { get; set; }

        [MaxLength(60)]
        [Column("NOMBRECAPACITADOR")]
        public String Nombrecapacitador { get; set; }


        [MaxLength(10)]
        [Column("CENTROCOSTO")]
        public String Centrocosto { get; set; }

        [MaxLength(15)]
        [Column("AFE")]
        public String Afe { get; set; }

        [Column("PRIORIDAD")]
        public Nullable<Int32> Prioridad { get; set; }

        [MaxLength(2)]
        [Column("MONEDA")]
        public String Moneda { get; set; }

        [Column("PORCENTAJEEMPRESA")]
        public Nullable<Double> Porcentajeempresa { get; set; }

        [Column("PORCENTAJEEMPLEADO")]
        public Nullable<Double> Porcentajeempleado { get; set; }


        [MaxLength(10)]
        [Column("INDSUSTENTO1")]
        public String Indsustento1 { get; set; }


        [MaxLength(10)]
        [Column("INDSUSTENTO2")]
        public String Indsustento2 { get; set; }


        [Column("DIA")]
        public Nullable<Int32> Dia { get; set; }


        [Column("EMPLEADOSOLICITANTE")]
        public Nullable<Int32> Empleadosolicitante { get; set; }


        [MaxLength(1)]
        [Column("FLAGEVALUACION")]
        public String Flagevaluacion { get; set; }


        [MaxLength(10)]
        [Column("DEPARTAMENTO")]
        public String Departamento { get; set; }


        [Column("AREAPOSICION")]
        public Nullable<Int32> Areaposicion { get; set; }


        [Column("SECCIONPOSICION")]
        public Nullable<Int32> Seccionposicion { get; set; }


        [Column("CODIGOUNIDAD")]
        public Nullable<Int32> Codigounidad { get; set; }


        [Column("CODIGOUNIDADSOLICITANTE")]
        public Nullable<Int32> Codigounidadsolicitante { get; set; }


        [MaxLength(1)]
        [Column("FLAGPROGRAMADO")]
        public String Flagprogramado { get; set; }


        [Column("IDTRANSACCION")]
        public Nullable<Int32> Idtransaccion { get; set; }


        [Column("JEFEUNIDAD")]
        public Nullable<Int32> Jefeunidad { get; set; }


        [MaxLength(10)]
        [Column("REQUISICIONNUMERO")]
        public String Requisicionnumero { get; set; }


        [Column("TIPOCAMBIO")]
        public Nullable<Double> Tipocambio { get; set; }


        [MaxLength(800)]
        [Column("MOTIVO_RECHAZO")]
        public String MotivoRechazo { get; set; }


        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }


        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }


        [Column("NIVELAPROBACION")]
        public Nullable<Int32> Nivelaprobacion { get; set; }


        [MaxLength(100)]
        [Column("REPRESENTANTE1")]
        public String Representante1 { get; set; }


        [MaxLength(100)]
        [Column("REPRESENTANTE2")]
        public String Representante2 { get; set; }


        [Column("SESIONES")]
        public Nullable<Int32> Sesiones { get; set; }


        [MaxLength(30)]
        [Column("VINCULOLABORAL")]
        public String Vinculolaboral { get; set; }


        [MaxLength(10)]
        [Column("COLOR")]
        public String Color { get; set; }

        [MaxLength(1)]
        [Column("SUSPENDIDO")]
        public String Suspendido { get; set; }


        [MaxLength(1000)]
        [Column("MOTIVOSUSPENSION")]
        public String Motivosuspension { get; set; }


        [MaxLength(1000)]
        [Column("MOTIVONOSUSPENSION")]
        public String Motivonosuspension { get; set; }


        [MaxLength(1)]
        [Column("CURSO_NUEVO")]
        public String CursoNuevo { get; set; }

        [MaxLength(1)]
        [Column("ESTADOLOGISTICA")]
        public String Estadologistica { get; set; }

        [MaxLength(2)]
        [Column("CODIGOPROCESO")]
        public String Codigoproceso { get; set; }


        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("NUMEROPROCESO")]
        public Nullable<Int32> Numeroproceso { get; set; }

        [Column("FECHATERMINO")]
        public Nullable<DateTime> Fechatermino { get; set; }

        [Column("FECHAENVIOCORREO")]
        public Nullable<DateTime> Fechaenviocorreo { get; set; }


        [NotMapped]
        public String direccion { get; set; }

        [NotMapped]
        public List<HrEmpleadocapacitacion> listaEmpleados;

        [NotMapped]
        public List<HrEmpleadocaphorario> listaHorarios;

        [NotMapped]
        public List<HrCapacitacionEmpleado> listaPsEmpleados;

        [NotMapped]
        public List<HrCapacitacionBeneficiario> listaPsBeneficiarios;

        [NotMapped]
        public List<HrCapacitacionFolios> listaFolios;

        public HrCapacitacion()
        {
            listaEmpleados = new List<HrEmpleadocapacitacion>();
            listaPsEmpleados = new List<HrCapacitacionEmpleado>();
            listaPsBeneficiarios = new List<HrCapacitacionBeneficiario>();
            listaHorarios = new List<HrEmpleadocaphorario>();
            listaFolios = new List<HrCapacitacionFolios>();
        }
    }
}

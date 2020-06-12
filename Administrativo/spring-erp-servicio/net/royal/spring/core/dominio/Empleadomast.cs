using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.EmpleadoMast
*/
    [Table("EMPLEADOMAST")]
    public class Empleadomast : EmpleadomastPk
    {

        [Display(Name = "CentroCostos")]
        [MaxLength(10)]
        [Column("CENTROCOSTOS")]
        public String Centrocostos { get; set; }

        [Display(Name = "Sucursal")]
        [MaxLength(4)]
        [Column("SUCURSAL")]
        public String Sucursal { get; set; }

        [Display(Name = "Cargo")]
        [MaxLength(3)]
        [Column("CARGO")]
        public String Cargo { get; set; }

        [Display(Name = "CodigoCargo")]
        [Column("CODIGOCARGO")]
        public Nullable<Int32> Codigocargo { get; set; }

        [Display(Name = "TipoPlanilla")]
        [MaxLength(2)]
        [Column("TIPOPLANILLA")]
        public String Tipoplanilla { get; set; }

        [Display(Name = "DepartamentoOperacional")]
        [MaxLength(5)]
        [Column("DEPARTAMENTOOPERACIONAL")]
        public String Departamentooperacional { get; set; }

        [Display(Name = "FechaIngreso")]
        [Column("FECHAINGRESO")]
        public Nullable<DateTime> Fechaingreso { get; set; }

        [Display(Name = "FechaCese")]
        [Column("FECHACESE")]
        public Nullable<DateTime> Fechacese { get; set; }

        [Display(Name = "EstadoEmpleado")]
        [MaxLength(1)]
        [Column("ESTADOEMPLEADO")]
        public String Estadoempleado { get; set; }

        [Display(Name = "LocaciondePago")]
        [MaxLength(4)]
        [Column("LOCACIONDEPAGO")]
        public String Locaciondepago { get; set; }

        [Display(Name = "UnidadNegocioAsignada")]
        [MaxLength(4)]
        [Column("UNIDADNEGOCIOASIGNADA")]
        public String Unidadnegocioasignada { get; set; }

        [Display(Name = "LocacionAsignada")]
        [MaxLength(4)]
        [Column("LOCACIONASIGNADA")]
        public String Locacionasignada { get; set; }

        [Display(Name = "ResponsableEmpleado")]
        [MaxLength(4)]
        [Column("RESPONSABLEEMPLEADO")]
        public String Responsableempleado { get; set; }

        [Display(Name = "ResponsableJefe")]
        [MaxLength(4)]
        [Column("RESPONSABLEJEFE")]
        public String Responsablejefe { get; set; }

        [Display(Name = "DeptoOrganizacion")]
        [MaxLength(3)]
        [Column("DEPTOORGANIZACION")]
        public String Deptoorganizacion { get; set; }

        [Display(Name = "TipoContrato")]
        [MaxLength(2)]
        [Column("TIPOCONTRATO")]
        public String Tipocontrato { get; set; }

        [Display(Name = "TipoHorario")]
        [Column("TIPOHORARIO")]
        public Nullable<Int32> Tipohorario { get; set; }

        [Display(Name = "TipoTrabajador")]
        [MaxLength(2)]
        [Column("TIPOTRABAJADOR")]
        public String Tipotrabajador { get; set; }

        [Display(Name = "CodigoUnidad")]
        [Column("CODIGOUNIDAD")]
        public Nullable<Int32> Codigounidad { get; set; }

        [Display(Name = "division")]
        [MaxLength(4)]
        [Column("DIVISION")]
        public String Division { get; set; }

        [Display(Name = "oficina")]
        [MaxLength(10)]
        [Column("OFICINA")]
        public String Oficina { get; set; }

        /*[Display(Name = "UnidadNegocio")]
        [MaxLength(4)]
        [Column("UNIDADNEGOCIO")]
        public String Unidadnegocio { get; set; }*/

        [Display(Name = "CorreoInterno")]
        [MaxLength(50)]
        [Column("CORREOINTERNO")]
        public String Correointerno { get; set; }

        [Display(Name = "CodigoUsuario")]
        [MaxLength(10)]
        [Column("CODIGOUSUARIO")]
        public String Codigousuario { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "FechaInicioPension")]
        [Column("FECHAINICIOPENSION")]
        public Nullable<DateTime> Fechainiciopension { get; set; }

        [Display(Name = "FlagIPSSVIDA")]
        [MaxLength(1)]
        [Column("FLAGIPSSVIDA")]
        public String Flagipssvida { get; set; }

        [Display(Name = "BancoCTS")]
        [MaxLength(3)]
        [Column("BANCOCTS")]
        public String Bancocts { get; set; }

        [Display(Name = "FlagDiscapacidad")]
        [MaxLength(1)]
        [Column("FLAGDISCAPACIDAD")]
        public String Flagdiscapacidad { get; set; }

        [Display(Name = "TipoMonedaCTS")]
        [MaxLength(2)]
        [Column("TIPOMONEDACTS")]
        public String Tipomonedacts { get; set; }

        [Display(Name = "NumeroCuentaCTS")]
        [MaxLength(20)]
        [Column("NUMEROCUENTACTS")]
        public String Numerocuentacts { get; set; }

        [Display(Name = "TipoAsistenciaSocial")]
        [MaxLength(3)]
        [Column("TIPOASISTENCIASOCIAL")]
        public String Tipoasistenciasocial { get; set; }

        [Display(Name = "TipoPension")]
        [MaxLength(3)]
        [Column("TIPOPENSION")]
        public String Tipopension { get; set; }

        [Display(Name = "FlagTrabajadorPensionista")]
        [MaxLength(1)]
        [Column("FLAGTRABAJADORPENSIONISTA")]
        public String Flagtrabajadorpensionista { get; set; }

        /*[Column("FOTOGRAFIA")]
        public byte[] Fotografia { get; set; }*/

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "NivelEducativoRTPS")]
        [MaxLength(2)]
        [Column("NIVELEDUCATIVORTPS")]
        public String NivelEducativoRTPS { get; set; }

        [Column("UNIDADOPERATIVA")]
        public String Unidadoperativa { get; set; }
        

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public Empleadomast()
        {
        }
    }
}

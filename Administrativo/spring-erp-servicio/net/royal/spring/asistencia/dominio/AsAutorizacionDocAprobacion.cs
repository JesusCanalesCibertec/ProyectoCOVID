using net.royal.spring.sistema.dominio.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PR_PRESTAMODOCAPROBACION
*/
    [Table("AS_AUTORIZACIONDOCAPROBACION")]
    public class AsAutorizacionDocAprobacion : AsAutorizacionDocAprobacionPk
    {
        [Display(Name = "Id Agrupaci�n Aprobaci�n")]
        [Column("IDGRUPOAPROBACION")]
        public Nullable<Int32> IdGrupoAprobacion { get; set; }

        [Display(Name = "Responsable")]
        [Column("RESPONSABLE")]
        public Nullable<Int32> Responsable { get; set; }

        [Display(Name = "Indicador de Aprobaci�n")]
        [Column("INDICADORAPROBACION")]
        public Nullable<Int32> IndicadorAprobacion { get; set; }

        [Display(Name = "Fecha de aprobaci�n")]
        [Column("FECHAAPROBACION")]
        public Nullable<DateTime> FechaAprobacion { get; set; }

        [Display(Name = "Comentario")]
        [Column("COMENTARIO")]
        public String Comentario { get; set; }

        [Display(Name = "Estado")]
        [Column("ESTADO")]
        public String Estado { get; set; }
        
        [Display(Name = "Usuario creaci�n")]
        [Column("USUARIOCREACION")]
        public String UsuarioCreacion { get; set; }

        [Display(Name = "Fecha de creaci�n")]
        [Column("FECHACREACION")]
        public Nullable<DateTime> FechaCreacion { get; set; }

        [Display(Name = "Usuario modificaci�n")]
        [Column("USUARIOMODIFICACION")]
        public String UsuarioModificacion { get; set; }

        [Display(Name = "Fecha de modificaci�n")]
        [Column("FECHAMODIFICACION")]
        public Nullable<DateTime> FechaModificacion { get; set; }

        public AsAutorizacionDocAprobacion()
        {
        }
    }
}

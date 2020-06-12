using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.SY_Reporte
    */
    [Table("HR_PuestoEmpresa")]
    public class HrPuestoempresa: HrPuestoempresaPk {

        [Display(Name = "GradoSalario")]
        [Column("GradoSalario")]
	    public String GradoSalario { get; set; }

        [Display(Name = "Descripcion")]
        [Column("Descripcion")]
        public String Descripcion { get; set; }

        [Display(Name = "DescripcionIngles")]
        [Column("DescripcionIngles")]
	    public String DescripcionIngles { get; set; }

        [Display(Name = "Comentarios")]
        [Column("Comentarios")]
        public String Comentarios { get; set; }

        [Display(Name = "Estado")]
        [Column("Estado")]
        public String Estado { get; set; }

        [Display(Name = "UnidadNegocio")]
        [Column("UnidadNegocio")]
        public String UnidadNegocio { get; set; }

        [Display(Name = "UltimoUsuario")]
        [Column("UltimoUsuario")]
        public String UltimoUsuario { get; set; }

        [Display(Name = "UltimaFechaModif")]
        [Column("UltimaFechaModif")]
        public Nullable<DateTime> UltimaFechaModif { get; set; }

        [Display(Name = "UnidadReplicacion")]
        [Column("UnidadReplicacion")]
        public String UnidadReplicacion { get; set; }

        [Display(Name = "TipoPuesto")]
        [Column("TipoPuesto")]
        public Nullable<Int32> TipoPuesto { get; set; }

        [Display(Name = "CategoriaFuncional")]
        [Column("CategoriaFuncional")]
        public String CategoriaFuncional { get; set; }

        [Display(Name = "PuestoSuperior")]
        [Column("PuestoSuperior")]
        public Nullable<Int32> PuestoSuperior { get; set; }

        [Display(Name = "PlantillaEvaluacion")]
        [Column("PlantillaEvaluacion")]
        public String PlantillaEvaluacion { get; set; }

        [Display(Name = "PlantillaDocumento")]
        [Column("PlantillaDocumento")]
        public Nullable<Int32> PlantillaDocumento { get; set; }

        [Display(Name = "CodAnterior")]
        [Column("CodAnterior")]
        public String CodAnterior { get; set; }

        [Display(Name = "CodigoCAP")]
        [Column("CodigoCAP")]
        public Nullable<Int32> CodigoCAP { get; set; }

        [Display(Name = "descripcioncap")]
        [Column("descripcioncap")]
        public String descripcioncap { get; set; }

        [Display(Name = "GrupoOcupacional")]
        [Column("GrupoOcupacional")]
        public Nullable<Int32> GrupoOcupacional { get; set; }

        [Display(Name = "PlantillaPotencial")]
        [Column("PlantillaPotencial")]
        public String PlantillaPotencial { get; set; }

        [Display(Name = "codigoRTPS")]
        [Column("codigoRTPS")]
        public String codigoRTPS { get; set; }

        [Display(Name = "PlantillaEntregas")]
        [Column("PlantillaEntregas")]
        public String PlantillaEntregas { get; set; }

        [Display(Name = "LineaCarrera")]
        [Column("LineaCarrera")]
        public String LineaCarrera { get; set; }

        [Display(Name = "Peso")]
        [Column("Peso")]
        public Nullable<Int32> Peso { get; set; }

        [Display(Name = "PlantillaCompetencias")]
        [Column("PlantillaCompetencias")]
        public String PlantillaCompetencias { get; set; }

        [Display(Name = "EdadDesde")]
        [Column("EdadDesde")]
        public Nullable<Int32> EdadDesde { get; set; }

        [Display(Name = "EdadHasta")]
        [Column("EdadHasta")]
        public Nullable<Int32> EdadHasta { get; set; }

        [Display(Name = "Sexo")]
        [Column("Sexo")]
        public String Sexo { get; set; }

        [Display(Name = "OtrosDatos")]
        [Column("OtrosDatos")]
        public String OtrosDatos { get; set; }

        [Display(Name = "Observaciones")]
        [Column("Observaciones")]
        public String Observaciones { get; set; }


        public HrPuestoempresa() {
	    }
    }
}

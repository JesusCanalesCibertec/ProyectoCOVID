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
     * @tabla sgseguridadsys.PS_NUTRICION
*/
    [Table("PS_NUTRICION", Schema = "sgseguridadsys")]
    public class PsNutricion : PsNutricionPk
    {

        [Display(Name = "ID_PERIODO")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }

        [Display(Name = "ID_ORIGEN")]
        [MaxLength(3)]
        [Column("ID_ORIGEN")]
        public String IdOrigen { get; set; }

        [Display(Name = "EVALUADO")]
        [MaxLength(2)]
        [Column("EVALUADO")]
        public String Evaluado { get; set; }

        [Display(Name = "FECHA_INFORME")]
        [Column("FECHA_INFORME")]
        public Nullable<DateTime> FechaInforme { get; set; }


        [Display(Name = "PESO")]
        [Column("PESO")]
        public Nullable<Decimal> Peso { get; set; }


        [Display(Name = "TALLA")]
        [Column("TALLA")]
        public Nullable<Decimal> Talla { get; set; }

        [Display(Name = "VARIACION_PESO")]
        [Column("VARIACION_PESO")]
        public Nullable<Decimal> VariacionPeso { get; set; }



        [Display(Name = "IMC")]
        [MaxLength(50)]
        [Column("IMC")]
        public String Imc { get; set; }

        [Display(Name = "PESO_EDAD")]
        [MaxLength(50)]
        [Column("PESO_EDAD")]
        public String PesoEdad { get; set; }

        [Display(Name = "TALLA_EDAD")]
        [MaxLength(50)]
        [Column("TALLA_EDAD")]
        public String TallaEdad { get; set; }

        [Display(Name = "PESO_TALLA")]
        [MaxLength(50)]
        [Column("PESO_TALLA")]
        public String PesoTalla { get; set; }


        [Display(Name = "ID_VALORACION")]
        [MaxLength(4)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_VALORACION")]
        public String IdValoracion { get; set; }


        [Display(Name = "ID_PERIMETRO_PIERNA")]
        [MaxLength(4)]
        [Column("ID_PERIMETRO_PIERNA")]
        public String IdPerimetroPierna { get; set; }


        [Display(Name = "ID_PRESION_MENSUAL")]
        [MaxLength(4)]
        [Column("ID_PRESION_MENSUAL")]
        public String IdPresionMensual { get; set; }


        [Display(Name = "ID_SUPLEMENTO_NUTRICIONAL")]
        [MaxLength(4)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_SUPLEMENTO_NUTRICIONAL")]
        public String IdSuplementoNutricional { get; set; }


        [Display(Name = "SUPLEMENTO_NUTRICIONAL_POR_DIA")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("SUPLEMENTO_NUTRICIONAL_POR_DIA")]
        public Nullable<Int32> SuplementoNutricionalPorDia { get; set; }


        [Display(Name = "ID_TIPO_DIETA")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_TIPO_DIETA")]
        public String IdTipoDieta { get; set; }


        [Display(Name = "TIPO_DIETA_POR_DIA")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("TIPO_DIETA_POR_DIA")]
        public Nullable<Int32> TipoDietaPorDia { get; set; }


        [Display(Name = "COMENTARIOS")]
        [MaxLength(1000)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COMENTARIOS")]
        public String Comentarios { get; set; }


        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }


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
        public String NombreCompleto { get; set; }
        [NotMapped]
        public String InstitucionNombre { get; set; }

        [NotMapped]
        public Boolean ValidarNinos { get; set; }


        [NotMapped]
        public Boolean EvaluadoBoolean { get; set; }

        [NotMapped]
        public String NombreValoracionAdultos { get; set; }
        [NotMapped]
        public String NombreTipoDietaAdultos { get; set; }
        [NotMapped]
        public String NombreValoracionNinos { get; set; }
        [NotMapped]
        public String NombreTipoDietaNinos { get; set; }
        [NotMapped]
        public String NombreSuplemento { get; set; }
        [NotMapped]
        public String NombrePerimetro { get; set; }
        [NotMapped]

        public Nullable<Int32> Edad { get; set; }
        [NotMapped]
        public String NombrePresion { get; set; }

        [NotMapped]
        public String sexo { get; set; }

        [NotMapped]
        public Int32 secuencia { get; set; }

        public PsNutricion()
        {
        }
    }
}

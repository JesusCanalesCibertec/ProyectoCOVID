using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.covid.dominio
{

    [Table("TRIAJE")]
    public class Triaje : TriajePk
    {
        [Column("COD_CIUDADANO")]
        public Nullable<Int32> IdCiudadano { get; set; }

        [Column("DIS_GUS")]
        public String Disgus { get; set; }

        [Column("TOS")]
        public String Tos { get; set; }

        [Column("DOLOR")]
        public String Dolor { get; set; }

        [Column("DIFI")]
        public String Difi { get; set; }

        [Column("NASAL")]
        public String Nasal { get; set; }

        [Column("FIEBRE")]
        public String Fiebre { get; set; }

        [Column("FECHA_INICIO")]
        public Nullable<DateTime> Fechainicio { get; set; }

        [Column("SITUACION1")]
        public String Situacion1 { get; set; }

        [Column("SITUACION2")]
        public String Situacion2 { get; set; }

        [Column("SITUACION3")]
        public String Situacion3 { get; set; }

        [Column("OBESIDAD")]
        public String Obesidad { get; set; }

        [Column("PULMONAR")]
        public String Pulmonar { get; set; }

        [Column("ASMA")]
        public String Asma { get; set; }

        [Column("DIABETES")]
        public String Diabetes { get; set; }

        [Column("HIPERTENSION")]
        public String Hipertension { get; set; }

        [Column("CARDIO")]
        public String Cardio { get; set; }

        [Column("RENAL")]
        public String Renal { get; set; }

        [Column("CANCER")]
        public String Cancer { get; set; }

        [Column("ESTADO")]
        public  Nullable<Int32> Estado { get; set; }

        [Column("FECHA_REGISTRO")]
        public Nullable<DateTime> Fecharegistro { get; set; }
    }

    
}

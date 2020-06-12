using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.minedu.dominio
{

    [Table("PERSONA",Schema ="minedu")]
    public class MpPersona : MpPersonaPk
    {

        [Column("DNI")]
        public String Dni { get; set; }

        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("APELLIDO")]
        public String Apellido { get; set; }


        [Column("ANEXO")]
        public String Anexo { get; set; }

        [Column("CELULAR")]
        public String Celular { get; set; }

        [Column("CORREO")]
        public String Correo { get; set; }

        [Column("CORREO_ALTERNO")]
        public String Correoalterno { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("USUARIO")]
        public String Usuario { get; set; }

        [Column("COMPANIA")]
        public String Compania { get; set; }

        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }

        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }

        [Column("CREACION_TERMINAL")]
        public String CreacionTerminal { get; set; }

        [Column("MODIFICACION_USUARIO")]
        public String ModificacionUsuario { get; set; }

        [Column("MODIFICACION_FECHA")]
        public Nullable<DateTime> ModificacionFecha { get; set; }

        [Column("MODIFICACION_TERMINAL")]
        public String ModificacionTerminal { get; set; }


        //Campos auxiliares de Contratación
        [NotMapped]
        public Nullable<Int32> AuxIdContratacion;

        [NotMapped]
        public Nullable<Int32> Numeroplazo;

        [NotMapped]
        public Nullable<DateTime> Fechainicio;

        [NotMapped]
        public Nullable<DateTime> Fechafin;

        [NotMapped]
        public String IdModalidad;

        [NotMapped]
        public String Numeroorden;

        [NotMapped]
        public String Cargo;

        [NotMapped]
        public Nullable<DateTime> Fechacese;

        [NotMapped]
        public String Motivocese;
    }

    
}

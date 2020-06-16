using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dominio.dto
{
    public class DtoPersona
    {
        public Int32? Id { get; set; }
        public String DNI { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }    
        public String Instruccion { get; set; }
        public String Anexo { get; set; }
        public String Celular { get; set; }
        public String Correo { get; set; }
        public String Estado { get; set; }
        public String Centro { get; set; }
        public Int32? Canttitulos { get; set; }
        public Int32? Cantconocimientos { get; set; }
        public Int32? Cantadendasentregable { get; set; }
        public String IdModalidad { get; set; }
        public String NomModalidad { get; set; }
        public Nullable<DateTime> Desde { get; set; }
        public Nullable<DateTime> Hasta { get; set; }
        public Int32? IdContratacion { get; set; }
        public Nullable<DateTime> Ultimafecha { get; set; }
        public Nullable<DateTime> Fechacese { get; set; }


    }
}

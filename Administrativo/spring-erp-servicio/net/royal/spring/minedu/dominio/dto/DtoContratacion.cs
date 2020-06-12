using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio.dto
{
    public class DtoContratacion
    {
        public Int32? IdContratacion { get; set; }
        public Int32? IdPersona { get; set; }
        public string Persona { get; set; }
        public Nullable<DateTime> Fechainicio { get; set; }
        public Nullable<DateTime> Fechafin { get; set; }
        public String IdModalidad { get; set; }
        public String Modalidad { get; set; }
        public String Numeroorden { get; set; }
        public String IdCargo { get; set; }
        public String Cargo { get; set; }
        public String Conocimientos { get; set; }
        public String Estado { get; set; }
        public Int32? Secuencia { get; set; }
        public Int32? Plazo { get; set; }

        [NotMapped]
        public List<DateTime?> diasocupados;

        public DtoContratacion()
        {
            diasocupados = new List<DateTime?>();
        }
    }
}

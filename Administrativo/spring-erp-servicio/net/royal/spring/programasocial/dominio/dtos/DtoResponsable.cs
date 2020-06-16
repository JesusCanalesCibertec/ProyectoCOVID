using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.programasocial.dominio.dtos {
    public class DtoResponsable {
        public String Responsable { get; set; }
        public String Estado { get; set; }
        public Nullable<Int32> Tareas { get; set; }
        public Nullable<Int32> Vencidos { get; set; }
        public Nullable<Int32> Terminados { get; set; }
    }
}

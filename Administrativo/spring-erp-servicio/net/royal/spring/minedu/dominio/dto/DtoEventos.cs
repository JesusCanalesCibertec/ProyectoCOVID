using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dominio.dto
{
    public class DtoEventos
    {
        public Int32? id { get; set; }
        public String title { get; set; }
        public Nullable<DateTime> start { get; set; }
        public Nullable<DateTime> end { get; set; }
        public Boolean allDay { get; set; }
        public String backgroundColor { get; set; }
        public String borderColor { get; set; }
    }
}

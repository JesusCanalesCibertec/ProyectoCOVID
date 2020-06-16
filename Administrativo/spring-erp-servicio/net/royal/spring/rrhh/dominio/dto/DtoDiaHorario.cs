using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    class DtoDiaHorario
    {

        public DtoDiaHorario()
        {
            this.minutos = new List<Int32>();
            this.minutos.Add(0);
            this.minutos.Add(0);
            this.minutos.Add(0);
            this.minutos.Add(0);
            this.minutos.Add(0);
            this.minutos.Add(0);
            this.minutos.Add(0);
            this.minutos.Add(0);
        }

        public string dias { get; set; }
        public List<Int32> minutos { get; set; }

    }
}

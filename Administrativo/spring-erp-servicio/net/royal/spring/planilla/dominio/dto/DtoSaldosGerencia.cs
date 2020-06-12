using System;
using System.Collections.Generic;
using System.Text;


namespace net.royal.spring.planilla.dominio.dto
{
    public class DtoSaldosGerencia
    {
        public Int32 codigo { get; set; }
        public String documento { get; set; }
        public String nombreCompleto { get; set; }
        public String gerencia { get; set; }
        public String area { get; set; }
        public DateTime? fechaIngreso { get; set; }
        public String aniversario { get; set; }
        public string truncos { get; set; }
        public string pendiente { get; set; }
        public string saldoHoy { get; set; }
        public string saldoDisponible { get; set; }
        public string unidad { get; set; }
    }
}

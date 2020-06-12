using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoValoresNutricionales
    {
        public String codInstitucion { get; set; }
        public String nomInstitucion { get; set; }
        public Nullable<Int32> poblacion { get; set; }
        public Nullable<Int32> normalCant { get; set; }
        public Nullable<Decimal> normalPorc { get; set; }
        public Nullable<Int32> adelgaSeveroCant { get; set; }
        public Nullable<Decimal> adelgaSeveroPorc { get; set; }
        public Nullable<Int32> adelgaModeradoCant { get; set; }
        public Nullable<Decimal> adelgaModeradoPorc { get; set; }
        public Nullable<Int32> sobrepesoCant { get; set; }
        public Nullable<Decimal> sobrepesoPorc { get; set; }
        public Nullable<Int32> obesidadCant { get; set; }
        public Nullable<Decimal> obesidadPorc { get; set; }
        public Nullable<Int32> noEvaluadosCant { get; set; }
        public Nullable<Decimal> noEvaluadosPorc { get; set; }
    }

    public class ResultadoResGenerico
    {
        public String idPrimario { get; set; }
        public String nombrePrimario { get; set; }
        public String idSecundario { get; set; }
        public String nombreSecundario { get; set; }
        public String idTerciario { get; set; }
        public String nombreTerciario { get; set; }
        public Int32 cantidad { get; set; }
        public Decimal porcentaje { get; set; }
        public Int32 maximo { get; set; }
    }
}

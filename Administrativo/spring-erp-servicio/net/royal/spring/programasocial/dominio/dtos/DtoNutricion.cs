using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto {
    public class DtoNutricion {
        public String IdTipoProceso { get; set; }
        public Nullable<Int32> IdProceso { get; set; }

        public String IdPeriodo { get; set; }
        public Nullable<DateTime> FechaInforme { get; set; }
        public Nullable<Int32> IdBeneficiario { get; set; }

        public Nullable<Decimal> Peso { get; set; }

        public Nullable<Decimal> Talla { get; set; }

        public String Imc { get; set; }

        public String PesoEdad { get; set; }

        public String TallaEdad { get; set; }
        public String PesoTalla { get; set; }
        public Nullable<Decimal> VariacionPeso { get; set; }

        public Nullable<Decimal> Hemoglobina { get; set; }

        public String HemoglobinaNombre { get; set; }

        public String IdValoracion { get; set; }
        public Nullable<Int32> PerimetroPierna { get; set; }
        public Nullable<Int32> PresionMensual { get; set; }
        public String IdSuplementoNutricional { get; set; }
        public Nullable<Int32> SuplementoNutricionalPorDia { get; set; }
        public String IdTipoDieta { get; set; }
        public Nullable<Int32> TipoDietaPorDia { get; set; }

        public String Comentarios { get; set; }

        public String Estado { get; set; }
        public String NombreCompleto { get; set; }
    }
}

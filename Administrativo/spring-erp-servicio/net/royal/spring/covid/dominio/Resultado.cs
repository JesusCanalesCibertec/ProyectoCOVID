using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.covid.dominio
{

    [Table("RESULTADO")]
    public class Resultado : ResultadoPk
    {
        [Column("NOM_ESTADO")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("RECOMENDACION")]
        public String Recomendacion { get; set; }

    }

    
}

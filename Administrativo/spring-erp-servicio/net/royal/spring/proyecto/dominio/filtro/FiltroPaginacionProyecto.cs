using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio.filtro
{
    public class FiltroTarea
    {
        public Int32? idProyecto { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public Int32? idResponsable { get; set; }
        public String estado { get; set; }
        public String idTipo1 { get; set; }
        public String idProceso { get; set; }
        public String idTipoProyecto { get; set; }
        public String externoIdGrupo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpProcesoconexion
    {
        public String idProceso { get; set; }
        public String nomProceso { get; set; }
        public Int32 idConexion { get; set; }
        public String idUnico { get; set; }
        public String accion { get; set; }
        public String idEntradaElemento { get; set; }
        public String nomEntradaElemento { get; set; }
        public String idSalidaElemento { get; set; }
        public String nomSalidaElemento { get; set; }
        public Int32 idVersion { get; set; }

        public bool flgVerGuardar { get; set; }
        public List<BpProcesoconexion> lista { get; set; }

        public DtoBpProcesoconexion()
        {
            lista = new List<BpProcesoconexion>();
        }

    }
}

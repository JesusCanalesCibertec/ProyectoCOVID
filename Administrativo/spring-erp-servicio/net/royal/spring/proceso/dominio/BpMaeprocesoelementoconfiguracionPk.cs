using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesoelementoconfiguracionPk
    {

        [Key]
        [Column("ID_ELEMENTO")]
        public String IdElemento { get; set; }

        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Key]
        [Column("ID_CONFIGURACION")]
        public Nullable<Int32> IdConfiguracion { get; set; }

        public BpMaeprocesoelementoconfiguracionPk()
        {

        }
        public BpMaeprocesoelementoconfiguracionPk(String IdProceso, String IdElemento, Int32 IdConfiguracion)
        {
            this.IdElemento = IdElemento;
            this.IdProceso = IdProceso;
            this.IdConfiguracion = IdConfiguracion;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdElemento, IdConfiguracion };
            return myObjArray;
        }

    }
}

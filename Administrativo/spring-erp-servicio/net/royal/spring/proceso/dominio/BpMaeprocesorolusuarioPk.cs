using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesorolusuarioPk
    {
        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Key]
        [Column("ID_ROL")]
        public String IdRol { get; set; }

        [Key]
        [Column("ID_TIPO_SEGURIDAD")]
        public String IdTipoSeguridad { get; set; }

        [Key]
        [Column("ID_CONCEPTO")]
        public String IdConcepto { get; set; }

        public BpMaeprocesorolusuarioPk()
        {

        }
        public BpMaeprocesorolusuarioPk(String IdProceso, String IdRol, String IdTipoSeguridad, String IdConcepto)
        {
            this.IdRol = IdRol;
            this.IdProceso = IdProceso;
            this.IdTipoSeguridad = IdTipoSeguridad;
            this.IdConcepto = IdConcepto;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdRol, IdTipoSeguridad, IdConcepto };
            return myObjArray;
        }

    }
}

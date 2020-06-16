using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpProcesoconexioneventoPk
    {

        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Key]
        [Column("ID_VERSION")]
        public Int32 IdVersion { get; set; }

        [Key]
        [Column("ID_CONEXION")]
        public Int32 IdConexion { get; set; }

        [Key]
        [Column("ID_EVENTO")]
        public String IdEvento { get; set; }

        public BpProcesoconexioneventoPk()
        {

        }
        public BpProcesoconexioneventoPk(String IdProceso, Int32 IdVersion, Int32 IdConexion, String IdEvento)
        {
            this.IdProceso = IdProceso;
            this.IdVersion = IdVersion;
            this.IdConexion = IdConexion;
            this.IdEvento = IdEvento;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdVersion, IdConexion, IdEvento };
            return myObjArray;
        }

    }
}

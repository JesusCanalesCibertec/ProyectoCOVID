using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpProcesoconexionPk
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

        public BpProcesoconexionPk()
        {

        }
        public BpProcesoconexionPk(String IdProceso, Int32 IdVersion, Int32 IdConexion)
        {
            this.IdProceso = IdProceso;
            this.IdVersion = IdVersion;
            this.IdConexion = IdConexion;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdVersion, IdConexion };
            return myObjArray;
        }

    }
}

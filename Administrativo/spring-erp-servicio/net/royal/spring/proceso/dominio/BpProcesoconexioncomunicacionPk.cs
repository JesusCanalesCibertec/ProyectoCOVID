using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpProcesoconexioncomunicacionPk
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
        [Column("ID_USUARIO")]
        public String IdUsuario { get; set; }

        public BpProcesoconexioncomunicacionPk()
        {

        }
        public BpProcesoconexioncomunicacionPk(String IdProceso, Int32 IdVersion, Int32 IdConexion, String IdUsuario)
        {
            this.IdProceso = IdProceso;
            this.IdVersion = IdVersion;
            this.IdConexion = IdConexion;
            this.IdUsuario = IdUsuario;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdVersion, IdConexion };
            return myObjArray;
        }

    }
}

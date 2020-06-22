using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.covid.dominio
{

    [Table("CIUDADANO")]
    public class Ciudadano : CiudadanoPk
    {
        [Column("NOM_CIUDADANO")]
        public String Nombre { get; set; }

        [Column("APE_CIUDADANO")]
        public String Apellido { get; set; }

        [Column("ID_PAIS")]
        public String IdPais { get; set; }

        [Column("TIPO_DOCUMENTO")]
        public Nullable<Int32> TipoDocumento { get; set; }

        [Column("NRO_DOC")]
        public String NroDocumento { get; set; }

        [Column("FECHA_NAC")]
        public Nullable<DateTime> FechaNacimiento { get; set; }

        [Column("DIRECCION")]
        public String Direccion { get; set; }

        [Column("ID_DEPARTAMENTO")]
        public String IdDepartamento { get; set; }

        [Column("ID_PROVINCIA")]
        public String IdProvincia { get; set; }

        [Column("ID_DISTRITO")]
        public String IdDistrito { get; set; }

        [Column("ESTADO")]
        public  Nullable<Int32> Estado { get; set; }
    }

    
}

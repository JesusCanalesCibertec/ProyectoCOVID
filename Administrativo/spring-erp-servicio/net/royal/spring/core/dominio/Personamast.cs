using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PersonaMast
*/
    [Table("PERSONAMAST")]
    public class Personamast : PersonamastPk
    {

        [Display(Name = "Origen")]
        [MaxLength(4)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ORIGEN")]
        public String Origen { get; set; }

        [Display(Name = "ApellidoPaterno")]
        [MaxLength(100)]
        [Column("APELLIDOPATERNO")]
        public String Apellidopaterno { get; set; }

        [Display(Name = "ApellidoMaterno")]
        [MaxLength(100)]
        [Column("APELLIDOMATERNO")]
        public String Apellidomaterno { get; set; }

        [Display(Name = "lugarnacimiento")]
        [MaxLength(255)]
        [Column("lugarnacimiento")]
        public String lugarnacimiento { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(15)]
        [Column("Celular")]
        public String Celular { get; set; }

        [Display(Name = "Fax")]
        [MaxLength(15)]
        [Column("Fax")]
        public String Fax { get; set; }

        [Display(Name = "direccionreferencia")]
        [MaxLength(255)]
        [Column("direccionreferencia")]
        public String direccionreferencia { get; set; }



        [Display(Name = "Nombres")]
        [MaxLength(100)]
        [Column("NOMBRES")]
        public String Nombres { get; set; }

        [Display(Name = "NombreCompleto")]
        [MaxLength(300)]
        [Column("NOMBRECOMPLETO")]
        public String Nombrecompleto { get; set; }

        [Display(Name = "Busqueda")]
        [MaxLength(300)]
        [Column("BUSQUEDA")]
        public String Busqueda { get; set; }

        [Display(Name = "TipoDocumento")]
        [MaxLength(1)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("TIPODOCUMENTO")]
        public String Tipodocumento { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("DOCUMENTO")]
        public String Documento { get; set; }

        [Display(Name = "FechaNacimiento")]
        [Column("FECHANACIMIENTO")]
        public Nullable<DateTime> Fechanacimiento { get; set; }

        [Display(Name = "Sexo")]
        [MaxLength(1)]
        [Column("SEXO")]
        public String Sexo { get; set; }

        [Display(Name = "Nacionalidad")]
        [MaxLength(20)]
        [Column("NACIONALIDAD")]
        public String Nacionalidad { get; set; }

        [Display(Name = "EstadoCivil")]
        [MaxLength(1)]
        [Column("ESTADOCIVIL")]
        public String Estadocivil { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(190)]
        [Column("DIRECCION")]
        public String Direccion { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(3)]
        [Column("DEPARTAMENTO")]
        public String Departamento { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(3)]
        [Column("PROVINCIA")]
        public String Provincia { get; set; }

        [Display(Name = "CodigoPostal")]
        [MaxLength(3)]
        [Column("CODIGOPOSTAL")]
        public String Codigopostal { get; set; }

        [Display(Name = "Telefono")]
        [MaxLength(150)]
        [Column("TELEFONO")]
        public String Telefono { get; set; }



        [Display(Name = "DocumentoFiscal")]
        [MaxLength(20)]
        [Column("DOCUMENTOFISCAL")]
        public String Documentofiscal { get; set; }

        [Display(Name = "DocumentoIdentidad")]
        [MaxLength(20)]
        [Column("DOCUMENTOIDENTIDAD")]
        public String Documentoidentidad { get; set; }

        [Display(Name = "Pasaporte")]
        [MaxLength(18)]
        [Column("PASAPORTE")]
        public String Pasaporte { get; set; }

        [Display(Name = "CorreoElectronico")]
        [MaxLength(150)]
        [Column("CORREOELECTRONICO")]
        public String Correoelectronico { get; set; }

        [Display(Name = "EsCliente")]
        [MaxLength(1)]
        [Column("ESCLIENTE")]
        public String Escliente { get; set; }

        [Display(Name = "EsProveedor")]
        [MaxLength(1)]
        [Column("ESPROVEEDOR")]
        public String Esproveedor { get; set; }

        [Display(Name = "EsEmpleado")]
        [MaxLength(1)]
        [Column("ESEMPLEADO")]
        public String Esempleado { get; set; }

        [Display(Name = "EsOtro")]
        [MaxLength(1)]
        [Column("ESOTRO")]
        public String Esotro { get; set; }

        [Display(Name = "BancoMonedaLocal")]
        [MaxLength(3)]
        [Column("BANCOMONEDALOCAL")]
        public String Bancomonedalocal { get; set; }

        [Display(Name = "TipoCuentaLocal")]
        [MaxLength(3)]
        [Column("TIPOCUENTALOCAL")]
        public String Tipocuentalocal { get; set; }

        [Display(Name = "CuentaMonedaLocal")]
        [MaxLength(20)]
        [Column("CUENTAMONEDALOCAL")]
        public String Cuentamonedalocal { get; set; }

        [Display(Name = "CiudadNacimiento")]
        [MaxLength(20)]
        [Column("CIUDADNACIMIENTO")]
        public String Ciudadnacimiento { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }



        [Display(Name = "DocumentoMilitarFA")]
        [MaxLength(10)]
        [Column("DocumentoMilitarFA")]
        public String DocumentoMilitarFA { get; set; }

        /*[Display(Name = "LugarDocumentoIdentidad")]
        [MaxLength(30)]
        [Column("LugarDocumentoIdentidad")]
        public String LugarDocumentoIdentidad { get; set; }*/

        /*[Display(Name = "CorreoElectronicoTexto")]
        [MaxLength(100)]
        [Column("CorreoElectronicoTexto")]
        public String CorreoElectronicoTexto { get; set; }*/
        public Personamast()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.Usuario
    */
    [Table("USUARIO")]
    public class Usuario : UsuarioPk
    {

        public static string REPORTE_COMPROMISO_DATOS = "REPORTE_COMPROMISO_DATOS";
        public static string REPORTE_CONFORMIDAD_BOLETAS = "REPORTE_CONFORMIDAD_BOLETAS";

        public static String MFG_COMPANIA_REQUERIDA = "La compañia es obligatoria";
        public static String MFG_COMPANIA_REQUERIDA_USUARIO = "EL usuario no existe o no tiene una compañia";
        public static String MFG_CAPTCHA = "El código captcha es erróneo";
        public static String MSG_USUARIO_ACTUAL_NOEXISTE = "El usuario actual no existe";
        //public static String MSG_USUARIO_NOEXISTE = "Usuario no existente o está bloqueado, comunicarse al anexo 0667";
        //public static String MSG_USUARIO_INACTIVO = "Usuario no existente o está bloqueado, comunicarse al anexo 0667";
        public static String MFG_EMPLEADO_NOEXISTE = "El empleado no existe";
        public static String MFG_CLAVE_NOESIGUAL = "Contraseña incorrecta (Bloqueo de usuarios ante [p_intentos] intentos fallidos)";
        public static String MFG_EMPLEADO_NOESTAACTIVO = "El empleado no esta activo";
        public static String MFG_USUARIO_EXPIRADO = "La clave ha expirado, por favor cambiarlo por uno nuevo";

        [Display(Name = "UsuarioPerfil")]
        [MaxLength(2)]
        [Column("USUARIOPERFIL")]
        public String Usuarioperfil { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(65)]
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Display(Name = "Clave")]
        [MaxLength(10)]
        [Column("CLAVE")]
        public String Clave { get; set; }

        [Display(Name = "ExpirarPasswordFlag")]
        [MaxLength(1)]
        [Column("EXPIRARPASSWORDFLAG")]
        public String Expirarpasswordflag { get; set; }







        [Display(Name = "FechaExpiracion")]
        [Column("FECHAEXPIRACION")]
        public Nullable<DateTime> Fechaexpiracion { get; set; }

        [Display(Name = "UltimoLogin")]
        [Column("ULTIMOLOGIN")]
        public Nullable<DateTime> Ultimologin { get; set; }

        [Display(Name = "NumeroLoginsDisponible")]
        [Column("NUMEROLOGINSDISPONIBLE")]
        public Nullable<Int32> Numerologinsdisponible { get; set; }

        [Display(Name = "NumeroLoginsUsados")]
        [Column("NUMEROLOGINSUSADOS")]
        public Nullable<Int32> Numerologinsusados { get; set; }

        [Display(Name = "SQLLogin")]
        [MaxLength(20)]
        [Column("SQLLOGIN")]
        public String Sqllogin { get; set; }

        [Display(Name = "SQLPassword")]
        [MaxLength(10)]
        [Column("SQLPASSWORD")]
        public String Sqlpassword { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String UltimoUsuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public DateTime? UltimaFechaModif { get; set; }

        public Usuario()
        {
        }
    }
}

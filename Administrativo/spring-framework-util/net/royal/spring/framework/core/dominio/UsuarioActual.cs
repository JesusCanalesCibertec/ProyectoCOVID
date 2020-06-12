using System;

namespace net.royal.spring.framework.core.dominio
{
    public class UsuarioActual
    {
        public bool esJefe { get; set; }
        public String esAdministradorWeb { get; set; }
        public String Unidadoperativa { get; set; }
        public Nullable<DateTime> FechaExpiracion { get; set; }
        public String ExpirarPasswordFlag { get; set; }
        public String carnetId { get; set; }
        public String estadoEmpleado { get; set; }
        public String estado { get; set; }
        public String PreferenciasPeriodoContableActual { get; set; }
        public String mensaje { get; set; }
        /**
* codigo de usuario establecido en la tabla de Persona o Empleado
* dependiendo del negocio
*/
        public String UsuarioCodigo { get; set; }

        /**
         * informacion del correo interno o principal con al cual se puede trabajar
         * en todo el sistema
         */
        public String UsuarioCorreoInterno { get; set; }

        /**
         * login o usuario con el que ingreso al sistema esto viene de la tabla
         * Usuario
         */
        public String UsuarioLogin { get; set; }

        /**
         * indica cual es el nombre de la pc desde la cual se ha ingresado a la
         * aplicacion
         */
        public String UsuarioHostName { get; set; }

        /**
         * indica cual es la direcion ip desde la cual se ha ingresado a la
         * aplicacion
         */
        public String UsuarioIpAddress { get; set; }

        /**
         * login o usuario de base de datos que se uso para entrar al sistema
         * Usuario
         */
        public String UsuarioLoginBaseDatos { get; set; }

        public String UsuarioUniqueIdString { get; set; }
        public long UsuarioUniqueIdInteger { get; set; }

        /**
         * identificador unico de la entidad principal tipo persona
         */
        public Int32? PersonaId { get; set; }

        /**
         * tipo de documento del usuario logeado
         */
        public String PersonaTipoDocumento { get; set; }

        /**
         * nro del documento de identificacion del usuario actual
         */
        public String PersonaNroDocumento { get; set; }

        /**
         * Nombre completo del usuario incluye nombres y apellidos
         */
        public String PersonaNombreCompleto { get; set; }

        /**
         * nombre o nombres del usuario
         */
        public String PersonaNombres { get; set; }

        /**
         * apellidos del usuario logeado paterno y materno
         */
        public String PersonaApellidos { get; set; }

        /**
         * apellido Paterno del usuario logeado
         */
        public String PersonaApellidoPaterno { get; set; }

        /**
         * apellido Materno del usuario logeado
         */
        public String PersonaApellidoMaterno { get; set; }

        /**
         * empleadomast.companiasocio = COMPANYOWNER.COMPANYOWNER
         * 
         * @return
         */
        public String CompaniaSocioCodigo { get; set; }
        public String CompaniaSocioNombre { get; set; }

        /**
         * empleadomast.unidadnegocioasignada = MA_UnidadNegocio.UnidadNegocio
         * 
         * @return
         */
        public String UnidadNegocioAsignadaCodigo { get; set; }
        public String UnidadNegocioAsignadaNombre { get; set; }

        /**
         * empleadomast.sucursal = AC_Sucursal.sucursal
         * 
         * @return
         */
        public String SucursalCodigo { get; set; }
        public String SucursalNombre { get; set; }

        /**
         * empleadomast.deptoorganizacion = DEPARTMENTMST.department
         * 
         * @return
         */
        public String DepartamentoCodigo { get; set; }
        public String DepartamentoNombre { get; set; }

        /**
         * empleadomast.codigocargo = HR_PUESTOEMPRESA.codigopuesto
         * 
         * @return
         */
        public Int32? PuestoEmpresaCodigo { get; set; }
        public String PuestoEmpresaNombre { get; set; }

        /**
         * empleadomast.CentroCostos = AC_COSTCENTERMST.COSTCENTER
         * 
         * @return
         */
        public String CentroCostosCodigo { get; set; }
        public String CentroCostosNombre { get; set; }
        public String AplicacionCodigo { get; set; }
        public String AplicacionNombre { get; set; }
        public String foto { get; set; }
        public String tipoPlanilla { get; set; }
        public String postulante { get; set; }
        public String unidadReplicacion { get; set; }
        public int? CodigoPosicion { get; set; }
        public Int32 paginacion { get; set; }
        public String flagConformidadDatos { get; set; }
        public String idInstitucion { get; set; }
        public String idInstitucionNombre { get; set; }
        public String idPrograma { get; set; }
        public String idProgramaNombre { get; set; }
        public String Origen { get; set; }
        /* se llena en el login y se usa para las consultas de encuestas */
        public String idPeriodoPreferencia { get; set; }
        public String clave { get; set; }
    }
}

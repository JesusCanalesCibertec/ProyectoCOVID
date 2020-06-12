using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.dto
{
    public class DtoEmpleadoBasico
    {
        public String correo { get; set; }
        public String CompaniaId { get; set; }
        public String CompaniaNombre { get; set; }
        public Int32? PersonaId { get; set; }
        public String PersonaNombre { get; set; }
        public String Sexo { get; set; }
        public String Documento { get; set; }
        public Nullable<DateTime> FechaIngreso { get; set; }
        public Nullable<DateTime> FechaNacimiento { get; set; }
        public String CodigoUsuario { get; set; }
        public String CargoId { get; set; }
        public String CargoNombre { get; set; }
        public String CentroCostosId { get; set; }
        public String CentroCostosNombre { get; set; }
        public String SucursalId { get; set; }
        public String SucursalNombre { get; set; }
        public String DeptoOrganizacionId { get; set; }
        public String DeptoOrganizacionNombre { get; set; }
        public String UnidadNegocioAsignadaId { get; set; }
        public String UnidadNegocioAsignadaNombre { get; set; }
        public String TipoPlanillaId { get; set; }
        public String TipoPlanillaNombre { get; set; }
        public String EstadoEmpleado { get; set; }
        public String EstadoEmpleadoNombre { get; set; }
        public String Personaant { get; set; }
        public Int32? PuestoEmpresaCodigo { get; set; }
        public String PuestoEmpresaNombre { get; set; }
        public String NombrePersonaCts { get; set; }
        public String UnidadOperativaNombre { get; set; }
        public String TipoContratoNombre { get; set; }
        public Nullable<DateTime> FechaInicioContrato { get; set; }
        public Nullable<DateTime> FechaFinContrato { get; set; }
        public Nullable<DateTime> Fecha { get; set; }
        public String nombreJefeDirecto { get; set; }
        public String horarioDescripcion { get; set; }

        public String esJefe{ get; set; }
        public String idInstitucion { get; set; }
        public String idInstitucionNombre { get; set; }
        public String periodoActual { get; set; }
    }
}

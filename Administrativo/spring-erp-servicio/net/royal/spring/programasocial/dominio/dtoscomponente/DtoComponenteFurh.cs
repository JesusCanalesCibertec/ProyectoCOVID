using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dtoscomponente {
    public class DtoComponenteFurh {
        public String IdInstitucion { get; set; }
        public Nullable<Int32> IdEmpleado { get; set; }
        public Nullable<DateTime> FechaIngreso { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public String Nombres { get; set; }
        public String Nombrecompleto { get; set; }
        public String Busqueda { get; set; }
        public String IdTipoDocumento { get; set; }
        public String Documento { get; set; }
        public Nullable<DateTime> Fechanacimiento { get; set; }
        public String IdSexo { get; set; }
        public String IdEstadoCivil { get; set; }
        public String Estado { get; set; }
        public String Correo1 { get; set; }
        public String IdDiscapacidad { get; set; }
        public String IdNivelAcademico { get; set; }
        public String IdEspecialidadAcademica { get; set; }
        public String comentarios { get; set; }
        public String IdFactorRh { get; set; }
        public String IdGrupoSanguineo { get; set; }
        public String CodigoUsuario { get; set; }
        public bool conusuario { get; set; }

        public Nullable<Int32> IdEntidad { get; set; }
        public String IdNivelAcademicoNombre { get; set; }
        public String IdEspecialidadAcademicaNombre { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }
        public String EstadoNombre { get; set; }
        public String TiempoPermanencia { get; set; }
        public String IdInstitucionNombre { get; set; }
        public String IdAreaTrabajo { get; set; }
        public Nullable<DateTime> FechaNacimiento { get; set; }
        public String Edad { get; set; }

        public Nullable<Int32> orden { get; set; }
        public Nullable<Int32> sentido { get; set; }


        public DtoComponenteFurh() {
            paginacion = new ParametroPaginacionGenerico();
        }


    }
}

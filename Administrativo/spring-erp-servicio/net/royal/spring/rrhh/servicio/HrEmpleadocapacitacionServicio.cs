using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEmpleadocapacitacionServicio : GenericoServicio {
        HrEmpleadocapacitacion obtenerPorId(HrEmpleadocapacitacionPk id);
        HrEmpleadocapacitacion obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado);
        HrEmpleadocapacitacion coreActualizar(UsuarioActual usuarioActual, HrEmpleadocapacitacion bean);
        HrEmpleadocapacitacion coreInsertar(UsuarioActual usuarioActual, HrEmpleadocapacitacion bean);
        void coreEliminar(HrEmpleadocapacitacionPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEmpleadocaphorarioServicio : GenericoServicio {
        HrEmpleadocaphorario obtenerPorId(HrEmpleadocaphorarioPk id);
        HrEmpleadocaphorario obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
        HrEmpleadocaphorario coreActualizar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean);
        HrEmpleadocaphorario coreInsertar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean);
        void coreEliminar(HrEmpleadocaphorarioPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
        HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual,HrEmpleadocaphorarioPk id);
        HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
    }
}

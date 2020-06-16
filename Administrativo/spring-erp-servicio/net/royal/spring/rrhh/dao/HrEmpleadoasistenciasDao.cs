using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrEmpleadoasistenciasDao : GenericoDao<HrEmpleadoasistencias>
    {
        HrEmpleadoasistencias obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
        HrEmpleadoasistencias coreActualizar(UsuarioActual usuarioActual, HrEmpleadoasistencias bean);
        HrEmpleadoasistencias coreInsertar(UsuarioActual usuarioActual, HrEmpleadoasistencias bean);
        void coreEliminar(HrEmpleadoasistenciasPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
    }
}

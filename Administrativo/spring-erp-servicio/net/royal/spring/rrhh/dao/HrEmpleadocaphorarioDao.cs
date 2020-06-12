using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrEmpleadocaphorarioDao : GenericoDao<HrEmpleadocaphorario>
    {
        HrEmpleadocaphorario obtenerPorId(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
        HrEmpleadocaphorario coreActualizar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean, String estado);
        HrEmpleadocaphorario coreInsertar(UsuarioActual usuarioActual, HrEmpleadocaphorario bean, String estado);
        void coreEliminar(HrEmpleadocaphorarioPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
        HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual,HrEmpleadocaphorarioPk id);
        HrEmpleadocaphorario coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia);
        List<HrEmpleadocaphorario> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk);
    }
}

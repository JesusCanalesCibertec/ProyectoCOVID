using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionEmpleadoDao : GenericoDao<HrCapacitacionEmpleado>
    {
        HrCapacitacionEmpleado obtenerPorId(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdEmpleado);
        HrCapacitacionEmpleado coreActualizar(UsuarioActual usuarioActual, HrCapacitacionEmpleado bean);
        HrCapacitacionEmpleado coreInsertar(UsuarioActual usuarioActual, HrCapacitacionEmpleado bean);
        void coreEliminar(HrCapacitacionEmpleadoPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion,String pIdInstitucion,Nullable<Int32> pIdEmpleado);
        List<HrCapacitacionEmpleado> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk);
    }
}

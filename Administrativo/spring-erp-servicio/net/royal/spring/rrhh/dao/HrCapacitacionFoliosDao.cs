using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionFoliosDao : GenericoDao<HrCapacitacionFolios>
    {
        Int32 generarCodigo(HrCapacitacionPk pk);
        List<HrCapacitacionFolios> listarPorCapacitacion(HrCapacitacionPk hrCapacitacionPk);
    }
}

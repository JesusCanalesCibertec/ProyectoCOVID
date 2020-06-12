using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrTipocontratoDao : GenericoDao<HrTipocontrato>
    {
        List<HrTipocontrato> listar(DtoFiltro filtro);
        List<HrTipocontrato> listarActivos();
         HrTipocontrato obtenerPorId(HrTipocontratoPk hrTipocontratoPk);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.servicio
{

    public interface SyUnidadreplicacionServicio : GenericoServicio {
        SyUnidadreplicacion obtenerPorId(SyUnidadreplicacionPk syUnidadreplicacionPk);

        List<SyUnidadreplicacion> listarTodos();

        List<SyUnidadreplicacion> listar(DtoFiltro filtro);

        List<SyUnidadreplicacion> listarActivos();
    }
}

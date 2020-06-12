using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio
{

public interface BancoServicio : GenericoServicio {
        List<Banco> listarTodos();

        List<Banco> listar(DtoFiltro filtro);

        List<Banco> listarActivos();

        Banco obtenerPorId(BancoPk pk);
    }
}

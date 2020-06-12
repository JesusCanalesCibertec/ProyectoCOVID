using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.salud.dominio;

namespace net.royal.spring.salud.servicio
{

    public interface SsCie10grupoServicio : GenericoServicio
    {
        List<SsCie10grupo> listarPorCapitulo(string codigo);
    }
}

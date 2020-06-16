using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.proyecto.dominio;

namespace net.royal.spring.proyecto.dao
{

    public interface PmNotificacionpersonaDao : GenericoDao<PmNotificacionpersona>
    {
        List<PmNotificacionpersona> listarPorNotificacion(PmNotificacionPk pmNotificacionPk);
        List<PmNotificacionpersona> listarPorPersona(int? idPersona);
    }
}

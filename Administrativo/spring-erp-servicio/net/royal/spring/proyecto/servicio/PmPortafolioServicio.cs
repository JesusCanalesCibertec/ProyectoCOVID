using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.servicio
{

    public interface PmPortafolioServicio : GenericoServicio
    {
        PmPortafolio obtenerPortafolioActual(UsuarioActual usuarioActual);
    }
}

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.servicio
{
    public interface MpProyectorecursoServicio : GenericoServicio
    {
        MpProyectorecurso registrar(UsuarioActual usuarioActual, MpProyectorecurso bean);
        MpProyectorecurso obtenerPorId(int pIdPersona);
        MpProyectorecurso actualizar(UsuarioActual usuarioActual, MpProyectorecurso bean);

    }
}

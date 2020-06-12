using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dao
{
    public interface MpProyectorecursoDao : GenericoDao<MpProyectorecurso>
    {
        MpProyectorecurso registrar(UsuarioActual usuarioActual, MpProyectorecurso bean);
        MpProyectorecurso actualizar(UsuarioActual usuarioActual, MpProyectorecurso bean);
        List<MpProyectorecurso> listarRecursos(int pIdProyecto);

        void eliminarPeriodos(int pIdProyecto, int pIdPersona, int pIdContratacion);
    }
}

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dao
{
    public interface MpProyectorecursoperiodoDao : GenericoDao<MpProyectorecursoperiodo>
    {
        MpProyectorecursoperiodo registrar(UsuarioActual usuarioActual, MpProyectorecursoperiodo bean);
        MpProyectorecursoperiodo actualizar(UsuarioActual usuarioActual, MpProyectorecursoperiodo bean);
        List<MpProyectorecursoperiodo> listarPeriodos(int pIdProyecto, int pIdPersona);
    }
}

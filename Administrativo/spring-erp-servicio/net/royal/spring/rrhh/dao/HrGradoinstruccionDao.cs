using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{
    public interface HrGradoinstruccionDao : GenericoDao<HrGradoinstruccion>
    {
        List<HrGradoinstruccion> listar(DtoFiltro filtro);
        List<HrGradoinstruccion> listarActivos();
        String obtenerNombreGrado(String nivelacademico);
    }
}

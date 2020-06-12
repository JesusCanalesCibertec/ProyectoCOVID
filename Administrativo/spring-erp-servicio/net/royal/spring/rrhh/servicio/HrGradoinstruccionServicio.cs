using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.sistema.dominio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{
    public interface HrGradoinstruccionServicio
    {
        void registrar(HrGradoinstruccion enity);
        void actualizar(HrGradoinstruccion enity);
        void eliminar(HrGradoinstruccion entity);
        HrGradoinstruccion obtenerPorId(HrGradoinstruccionPk pk);
        List<HrGradoinstruccion> listarActivos();
        List<HrGradoinstruccion> listarTodos();
        List<HrGradoinstruccion> listar(DtoFiltro filtro);



    }
}

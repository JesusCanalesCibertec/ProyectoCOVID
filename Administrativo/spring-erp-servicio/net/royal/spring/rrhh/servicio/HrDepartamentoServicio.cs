using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.sistema.dominio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{
    public interface HrDepartamentoServicio
    {

        void registrar(HrDepartamento enity);
        void actualizar(HrDepartamento enity);
        void eliminar(HrDepartamento entity);
        HrDepartamento obtenerPorId(HrDepartamentoPk pk);
        List<HrDepartamento> listar(DtoFiltro filtro);
        List<HrDepartamento> listarActivos();
        List<HrDepartamento> listarTodos();


    }
}

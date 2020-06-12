using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio.impl
{
    public class HrDepartamentoServicioImpl : HrDepartamentoServicio
    {
        private HrDepartamentoDao hrDepartamentoDao;

        public HrDepartamentoServicioImpl(HrDepartamentoDao _HrDepartamentoDao) {
            hrDepartamentoDao = _HrDepartamentoDao;
        }

        public void registrar(HrDepartamento enity)
        {
            hrDepartamentoDao.registrar(enity);
        }

        public void actualizar(HrDepartamento enity)
        {
            hrDepartamentoDao.actualizar(enity);
        }

        public void eliminar(HrDepartamento entity)
        {
            hrDepartamentoDao.eliminar(entity);
        }

        public HrDepartamento obtenerPorId(HrDepartamentoPk pk)
        {
            return hrDepartamentoDao.obtenerPorId(pk.Departamento);
        }

        public List<HrDepartamento> listarActivos()
        {
            return hrDepartamentoDao.listarActivos();
        }

        public List<HrDepartamento> listar(DtoFiltro filtro)
        {
            return hrDepartamentoDao.listar(filtro);
        }

        public List<HrDepartamento> listarTodos()
        {
            return hrDepartamentoDao.listarTodos();
        }
    }
}

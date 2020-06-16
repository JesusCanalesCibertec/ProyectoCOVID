using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core;

namespace net.royal.spring.rrhh.servicio.impl
{

    public class HrUnidadoperativaServicioImpl : GenericoServicioImpl, HrUnidadoperativaServicio
    {

        private IServiceProvider servicioProveedor;
        private HrUnidadoperativaDao hrUnidadoperativaDao;

        public HrUnidadoperativaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            hrUnidadoperativaDao = servicioProveedor.GetService<HrUnidadoperativaDao>();
        }

        public void actualizar(HrUnidadoperativa hrUnidadoperativa)
        {
            hrUnidadoperativaDao.actualizar(hrUnidadoperativa);
        }

        public void eliminar(HrUnidadoperativa hrUnidadoperativa)
        {
            hrUnidadoperativaDao.eliminar(hrUnidadoperativa);
        }

        public List<HrUnidadoperativa> listarActivos()
        {
            return hrUnidadoperativaDao.listarActivos();
        }

        public List<HrUnidadoperativa> listarBusqueda(DtoFiltro filtroPaginacionJefatura)
        {
            return hrUnidadoperativaDao.listarBusqueda(filtroPaginacionJefatura);
        }

        public List<HrUnidadoperativa> listarTodos()
        {
            return hrUnidadoperativaDao.listarTodos();
        }

        public HrUnidadoperativa obtenerPorId(HrUnidadoperativaPk hrUnidadoperativaPk)
        {
            return hrUnidadoperativaDao.obtenerPorId(hrUnidadoperativaPk);
        }

        public void registrar(HrUnidadoperativa hrUnidadoperativa)
        {
            hrUnidadoperativaDao.registrar(hrUnidadoperativa);
        }

        public List<HrUnidadoperativa> listarJerarquico(String unidadOperativa)
        {
            unidadOperativa = UString.obtenerValorCadenaSinNulo(unidadOperativa); 
            List<HrUnidadoperativa> lstResultado = new List<HrUnidadoperativa>();
            List<HrUnidadoperativa> lstPrincipal = hrUnidadoperativaDao.listarActivos();

            List<HrUnidadoperativa> lstEncontrados = lstPrincipal.FindAll(uo => uo.UnidadoperativaSuperior.Equals(unidadOperativa));
            lstResultado.AddRange(lstEncontrados);

            foreach (HrUnidadoperativa item1 in lstEncontrados)
            {
                List<HrUnidadoperativa> lstTmp1 = lstPrincipal.FindAll(uo => uo.UnidadoperativaSuperior.Equals(item1.Unidadoperativa));
                lstResultado.AddRange(lstTmp1);
                if (lstTmp1.Count > 0)
                {
                    List<HrUnidadoperativa> lstTmp2 = listarJerarquicoInterno(lstPrincipal, lstTmp1);
                    lstResultado.AddRange(lstTmp2);
                }
            }

            HrUnidadoperativa bean = hrUnidadoperativaDao.obtenerPorId(new HrUnidadoperativaPk(unidadOperativa));
            if (bean != null)
                lstResultado.Add(bean);

            lstResultado.Sort((u1,u2)=> String.Compare(u1.Descripcion,u2.Descripcion) );
            return lstResultado;
        }
        private List<HrUnidadoperativa> listarJerarquicoInterno(List<HrUnidadoperativa> lstPrincipal, List<HrUnidadoperativa> lstEncontrados)
        {
            List<HrUnidadoperativa> lstResultado = new List<HrUnidadoperativa>();
            foreach (HrUnidadoperativa item1 in lstEncontrados)
            {
                List<HrUnidadoperativa> lstTmp1 = lstPrincipal.FindAll(uo => uo.UnidadoperativaSuperior.Equals(item1.Unidadoperativa));
                lstResultado.AddRange(lstTmp1);
                if (lstTmp1.Count > 0)
                {
                    List<HrUnidadoperativa> lstTmp2 = listarJerarquicoInterno(lstPrincipal, lstTmp1);
                    lstResultado.AddRange(lstTmp2);
                }
            }
            return lstResultado;
        }
    }
}

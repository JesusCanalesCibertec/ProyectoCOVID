using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dominio;

namespace net.royal.spring.minedu.servicio.impl
{
    public class MpProyectorecursoServicioImpl: GenericoServicioImpl, MpProyectorecursoServicio
    {
        private IServiceProvider servicioProveedor;
        private MpProyectorecursoDao MpProyectorecursoDao;

        public MpProyectorecursoServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            MpProyectorecursoDao = servicioProveedor.GetService<MpProyectorecursoDao>();
        }

        public MpProyectorecurso registrar(UsuarioActual usuarioActual, MpProyectorecurso bean)
        {
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Cargo = UString.Mayusculas(bean.Cargo);
            return MpProyectorecursoDao.registrar(usuarioActual, bean);
        }

        public MpProyectorecurso obtenerPorId(int pIdPersona)
        {
            //return MpProyectorecursoDao.obtenerPorId(new MpProyectorecurso() { IdPersona = pIdPersona}.obtenerArreglo());
            return null;
        }

        public MpProyectorecurso actualizar(UsuarioActual usuarioActual, MpProyectorecurso bean)
        {
            bean.Nombre = UString.Mayusculas(bean.Nombre);
            bean.Cargo = UString.Mayusculas(bean.Cargo);
            return MpProyectorecursoDao.actualizar(usuarioActual, bean);
        }

      
    }
}

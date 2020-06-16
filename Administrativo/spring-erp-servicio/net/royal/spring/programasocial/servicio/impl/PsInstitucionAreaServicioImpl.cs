using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Text.RegularExpressions;

namespace net.royal.spring.programasocial.servicio.impl
{

    public class PsInstitucionAreaServicioImpl : GenericoServicioImpl, PsInstitucionAreaServicio
    {

        private IServiceProvider servicioProveedor;
        private PsInstitucionAreaDao PsInstitucionAreaDao;

        public PsInstitucionAreaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            PsInstitucionAreaDao = servicioProveedor.GetService<PsInstitucionAreaDao>();
        }

        public PsInstitucionArea coreInsertar(UsuarioActual usuarioActual, PsInstitucionArea bean)
        {
            string cadena, codigo;
            codigo = PsInstitucionAreaDao.obtenercodigo(bean.IdInstitucion, bean.IdArea);
            cadena = PsInstitucionAreaDao.obtenercadena(bean.IdInstitucion, bean.Nombre);


            if (codigo != null)
            {
                throw new UException("El código ingresado ya se encuentra registrado");
            }
            if (cadena != null)
            {
                throw new UException("El nombre ingresado ya se encuentra registrado");
            }
            else
            {
                if (UString.estaVacio(bean.Estado))
                    bean.Estado = ConstanteEstadoGenerico.ACTIVO;
                bean.IdInstitucion = bean.IdInstitucion.ToUpper();
                return PsInstitucionAreaDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }

        }

        public PsInstitucionArea coreActualizar(UsuarioActual usuarioActual, PsInstitucionArea bean)
        {
            return PsInstitucionAreaDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }


        public PsInstitucionArea obtenerPorId(PsInstitucionAreaPk llave)
        {
            return PsInstitucionAreaDao.obtenerPorId(llave.obtenerArreglo());
        }


        public void eliminar(string pIdInstitucion, string pIdArea)
        {

            PsInstitucionArea objeto = new PsInstitucionArea();

            objeto.IdInstitucion = pIdInstitucion;
            objeto.IdArea = pIdArea;

            PsInstitucionAreaDao.eliminar(objeto);

        }

        public List<PsInstitucionArea> listado(string pIdInstitucion)
        {
            return PsInstitucionAreaDao.listado(pIdInstitucion);
        }

        public List<PsInstitucionArea> listadoPorPrograma(string pIdInstitucion, string idPrograma)
        {
            return PsInstitucionAreaDao.listadoPorPrograma(pIdInstitucion, idPrograma);
        }

        public List<PsInstitucionArea> listarTodo()
        {
            return PsInstitucionAreaDao.listarTodo();
        }

        public PsInstitucionArea coreAnular(UsuarioActual usuarioActual, PsInstitucionAreaPk id)
        {
            return PsInstitucionAreaDao.coreAnular(usuarioActual, id);
        }

        public PsInstitucionArea coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, String pIdArea)
        {
            return PsInstitucionAreaDao.coreAnular(usuarioActual, pIdInstitucion, pIdArea);
        }


    }
}


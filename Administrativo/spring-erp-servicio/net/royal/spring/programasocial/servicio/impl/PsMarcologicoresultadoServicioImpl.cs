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

public class PsMarcologicoresultadoServicioImpl : GenericoServicioImpl, PsMarcologicoresultadoServicio {

        private IServiceProvider servicioProveedor;
        private PsMarcologicoresultadoDao PsMarcologicoresultadoDao;

        public PsMarcologicoresultadoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            PsMarcologicoresultadoDao = servicioProveedor.GetService<PsMarcologicoresultadoDao>();
        }
        
        public PsMarcologicoresultado coreInsertar(UsuarioActual usuarioActual, PsMarcologicoresultado bean)
        {
            /*
            string cadena, codigo;
            codigo = PsMarcologicoresultadoDao.obtenercodigo(bean.IdInstitucion,bean.IdArea);
            cadena = PsMarcologicoresultadoDao.obtenercadena(bean.IdInstitucion,bean.Nombre);

            
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
                return PsMarcologicoresultadoDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }
            */
            return PsMarcologicoresultadoDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }


        public PsMarcologicoresultado coreActualizar(UsuarioActual usuarioActual, PsMarcologicoresultado bean)
        {
            return PsMarcologicoresultadoDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }
 

        public PsMarcologicoresultado obtenerPorId(PsMarcologicoresultadoPk llave)
        {
            return PsMarcologicoresultadoDao.obtenerPorId(llave.obtenerArreglo());
        }

      
        public void eliminar(string pIdMarcologico)
        {

            PsMarcologicoresultado objeto = new PsMarcologicoresultado();

            objeto.IdMarcologico = pIdMarcologico;
            

            PsMarcologicoresultadoDao.eliminar(objeto);

        }

       

       
    }
}


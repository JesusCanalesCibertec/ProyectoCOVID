using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.constante;
using Microsoft.Extensions.Logging;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.constante;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpMaeprocesoServicioImpl : GenericoServicioImpl, BpMaeprocesoServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaeprocesoDao bpMaeprocesoDao;


        public BpMaeprocesoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesoDao = servicioProveedor.GetService<BpMaeprocesoDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroBpMaeproceso filtro)
        {
            return bpMaeprocesoDao.listarPaginacion(paginacion, filtro);
        }

        public BpMaeproceso coreInsertar(UsuarioActual usuarioActual, BpMaeproceso bean)
        {
            string cadena, codigo;
            codigo = bpMaeprocesoDao.obtenercodigo(bean.IdProceso);
            cadena = bpMaeprocesoDao.obtenercadena(bean.Nombre);

            
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
                bean.IdProceso = bean.IdProceso.ToUpper();
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpMaeprocesoDao.registrar(bean);
            }

        }

        public BpMaeproceso coreActualizar(UsuarioActual usuarioActual, BpMaeproceso bean)
        {
            if (bpMaeprocesoDao.obtenercadena(bean.Nombre) != null)
            {
                throw new UException("El nombre ingresado ya se encuentra registrado");
            }
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpMaeprocesoDao.actualizar(bean);
            return bean;
        }


        public BpMaeproceso obtenerPorId(string codigo)
        {
            BpMaeprocesoPk llave = new BpMaeprocesoPk();
            llave.IdProceso = codigo;
            return bpMaeprocesoDao.obtenerPorId(llave.obtenerArreglo());
        }

        public List<BpMaeproceso> listarTodos()
        {
            return bpMaeprocesoDao.listarTodos();
        }
    }

}

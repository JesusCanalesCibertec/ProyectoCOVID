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
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpMaeprocesoestadoServicioImpl : GenericoServicioImpl, BpMaeprocesoestadoServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaeprocesoestadoDao bpMaeprocesoestadoDao;


        public BpMaeprocesoestadoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesoestadoDao = servicioProveedor.GetService<BpMaeprocesoestadoDao>();
        }

        public List<BpMaeprocesoestado> listarEstadoPorProceso(string idProceso)
        {
            return bpMaeprocesoestadoDao.listarEstadoPorProceso(idProceso);
        }

        public BpMaeprocesoestado coreInsertar(UsuarioActual usuarioActual, BpMaeprocesoestado bean)
        {
            string cadena, codigo;
            codigo = bpMaeprocesoestadoDao.obtenercodigo(bean.IdProceso, bean.IdEstado);
            cadena = bpMaeprocesoestadoDao.obtenercadena(bean.IdProceso, bean.Nombre);


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
         
                bean.IdEstado = bean.IdEstado.ToUpper();
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpMaeprocesoestadoDao.registrar(bean);
            }

        }

        public BpMaeprocesoestado coreActualizar(UsuarioActual usuarioActual, BpMaeprocesoestado bean)
        {
            
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpMaeprocesoestadoDao.actualizar(bean);
            return bean;
        }


        public BpMaeprocesoestado obtenerPorId(BpMaeprocesoestadoPk llave)
        {
            return bpMaeprocesoestadoDao.obtenerPorId(llave.obtenerArreglo());
        }

        public List<DtoBpMaeprocesoestado> listado(string codigo)
        {
            return bpMaeprocesoestadoDao.listado(codigo);
        }

        public void eliminar(string pIdProceso, string IdEstado)
        {
            BpMaeprocesoestado objeto = new BpMaeprocesoestado();

            objeto.IdProceso = pIdProceso;
            objeto.IdEstado = IdEstado;

            bpMaeprocesoestadoDao.eliminar(objeto);
        }
    }
}

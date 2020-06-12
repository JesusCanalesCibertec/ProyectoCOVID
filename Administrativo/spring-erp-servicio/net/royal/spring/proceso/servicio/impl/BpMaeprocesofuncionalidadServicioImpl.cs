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

    public class BpMaeprocesofuncionalidadServicioImpl : GenericoServicioImpl, BpMaeprocesofuncionalidadServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaeprocesofuncionalidadDao bpMaeprocesofuncionalidadDao;


        public BpMaeprocesofuncionalidadServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesofuncionalidadDao = servicioProveedor.GetService<BpMaeprocesofuncionalidadDao>();
        }

        public BpMaeprocesofuncionalidad coreInsertar(UsuarioActual usuarioActual, BpMaeprocesofuncionalidad bean)
        {
            string cadena, codigo;
            codigo = bpMaeprocesofuncionalidadDao.obtenercodigo(bean.IdProceso,bean.IdFuncionalidad);
            cadena = bpMaeprocesofuncionalidadDao.obtenercadena(bean.IdProceso,bean.Nombre);


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
               
                bean.IdFuncionalidad = bean.IdFuncionalidad.ToUpper();
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpMaeprocesofuncionalidadDao.registrar(bean);
            }

        }

        public BpMaeprocesofuncionalidad coreActualizar(UsuarioActual usuarioActual, BpMaeprocesofuncionalidad bean)
        {

            
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpMaeprocesofuncionalidadDao.actualizar(bean);
            return bean;
        }


        public BpMaeprocesofuncionalidad obtenerPorId(BpMaeprocesofuncionalidadPk llave)
        {
            return bpMaeprocesofuncionalidadDao.obtenerPorId(llave.obtenerArreglo());
        }

        public List<DtoBpMaeprocesofuncionalidad> listado(string codigo)
        {
            return bpMaeprocesofuncionalidadDao.listado(codigo);
        }

        public void eliminar(string pIdProceso, string pIdfuncionalidad)
        {
            BpMaeprocesofuncionalidad objeto = new BpMaeprocesofuncionalidad();

            objeto.IdProceso = pIdProceso;
            objeto.IdFuncionalidad = pIdfuncionalidad;

            bpMaeprocesofuncionalidadDao.eliminar(objeto);
        }
    }
}

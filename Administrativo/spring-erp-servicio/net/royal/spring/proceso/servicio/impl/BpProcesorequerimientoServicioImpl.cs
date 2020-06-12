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

    public class BpProcesorequerimientoServicioImpl : GenericoServicioImpl, BpProcesorequerimientoServicio
    {

        private IServiceProvider servicioProveedor;
        private BpProcesorequerimientoDao bpProcesorequerimientoDao;


        public BpProcesorequerimientoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpProcesorequerimientoDao = servicioProveedor.GetService<BpProcesorequerimientoDao>();
        }

        public BpProcesorequerimiento coreInsertar(UsuarioActual usuarioActual, BpProcesorequerimiento bean)
        {
            string codigo;

            codigo = bpProcesorequerimientoDao.obtenercodigo(bean.IdProceso,bean.IdRequerimiento, bean.IdVersion);
           


            if (codigo != null)
            {
                throw new UException("El requerimiento seleccionado ya se encuentra registrado");
            }

            else
            {
               
               
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpProcesorequerimientoDao.registrar(bean);
            }

        }

        public BpProcesorequerimiento coreActualizar(UsuarioActual usuarioActual, BpProcesorequerimiento bean)
        {

            
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpProcesorequerimientoDao.actualizar(bean);
            return bean;
        }


        public BpProcesorequerimiento obtenerPorId(BpProcesorequerimientoPk llave)
        {
            return bpProcesorequerimientoDao.obtenerPorId(llave.obtenerArreglo());
        }

        public List<DtoBpProcesorequerimiento> listado(string codigo)
        {
            return bpProcesorequerimientoDao.listado(codigo);
        }

        public void eliminar(string pIdProceso, string pIdRequerimiento, int pIdVersion)
        {
            BpProcesorequerimiento objeto = new BpProcesorequerimiento();

            objeto.IdProceso = pIdProceso;
            objeto.IdRequerimiento = pIdRequerimiento;
            objeto.IdVersion = pIdVersion;

            bpProcesorequerimientoDao.eliminar(objeto);
        }

    }
}

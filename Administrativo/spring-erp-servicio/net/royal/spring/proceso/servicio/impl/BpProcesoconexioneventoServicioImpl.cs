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

    public class BpProcesoconexioneventoServicioImpl : GenericoServicioImpl, BpProcesoconexioneventoServicio
    {

        private IServiceProvider servicioProveedor;
        private BpProcesoconexioneventoDao BpProcesoconexioneventoDao;


        public BpProcesoconexioneventoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            BpProcesoconexioneventoDao = servicioProveedor.GetService<BpProcesoconexioneventoDao>();
        }

        public BpProcesoconexionevento coreInsertar(UsuarioActual usuarioActual, BpProcesoconexionevento bean)
        {
            string codigo;

            BpProcesoconexioneventoPk llave = new BpProcesoconexioneventoPk();

            llave.IdProceso = bean.IdProceso;
            llave.IdVersion = bean.IdVersion;
            llave.IdConexion = bean.IdConexion;
            llave.IdEvento = bean.IdEvento;

            codigo = BpProcesoconexioneventoDao.obtenercodigo(llave);
           


            if (codigo != null)
            {
                throw new UException("Ya existe ese evento");
            }

            else
            {
               
               
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return BpProcesoconexioneventoDao.registrar(bean);
            }

        }

        public BpProcesoconexionevento coreActualizar(UsuarioActual usuarioActual, BpProcesoconexionevento bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.BpProcesoconexioneventoDao.actualizar(bean);
            return bean;
        }


        public BpProcesoconexionevento obtenerPorId(BpProcesoconexioneventoPk llave)
        {
            return BpProcesoconexioneventoDao.obtenerPorId(llave.obtenerArreglo());
        }

        public List<DtoBpProcesoconexionevento> listado(string pIdProceso, int pIdConexion)
        {
            return BpProcesoconexioneventoDao.listado(pIdProceso, pIdConexion);
        }

        public void eliminar(string pIdProceso, int pIdVersion, int pIdConexion, string pIdEvento)
        {
            BpProcesoconexionevento objeto = new BpProcesoconexionevento();

            objeto.IdProceso = pIdProceso;
            objeto.IdVersion = pIdVersion;
            objeto.IdConexion = pIdConexion;
            objeto.IdEvento = pIdEvento;

            BpProcesoconexioneventoDao.eliminar(objeto);
        }

    }
}

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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpMaeprocesorolServicioImpl : GenericoServicioImpl, BpMaeprocesorolServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaeprocesorolDao bpMaeprocesorolDao;


        public BpMaeprocesorolServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesorolDao = servicioProveedor.GetService<BpMaeprocesorolDao>();
        }

        public BpMaeprocesorol coreInsertar(UsuarioActual usuarioActual, BpMaeprocesorol bean)
        {
            string cadena, codigo;
            codigo = bpMaeprocesorolDao.obtenercodigo(bean.IdProceso,bean.IdRol);
            cadena = bpMaeprocesorolDao.obtenercadena(bean.IdProceso,bean.Nombre);


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
                bean.IdUnico = String.Concat("ROL",bean.IdProceso,bean.IdRol);
                bean.IdRol = bean.IdRol.ToUpper();
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpMaeprocesorolDao.registrar(bean);
            }

        }

        public BpMaeprocesorol coreActualizar(UsuarioActual usuarioActual, BpMaeprocesorol bean)
        {

            

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpMaeprocesorolDao.actualizar(bean);
            return bean;
        }


        public BpMaeprocesorol obtenerPorId(BpMaeprocesorolPk llave)
        {
            return bpMaeprocesorolDao.obtenerPorId(llave.obtenerArreglo());
        }

        public List<BpMaeprocesorol> listado(string codigo)
        {
            return bpMaeprocesorolDao.listado(codigo);
        }

        public void eliminar(string pIdProceso, string pIdRol)
        {
            BpMaeprocesorol objeto = new BpMaeprocesorol();

            objeto.IdProceso = pIdProceso;
            objeto.IdRol = pIdRol;

            bpMaeprocesorolDao.eliminar(objeto);
        }

        
    }
}

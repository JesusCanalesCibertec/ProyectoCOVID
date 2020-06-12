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

    public class BpMaeprocesorolusuarioServicioImpl : GenericoServicioImpl, BpMaeprocesorolusuarioServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaeprocesorolusuarioDao bpMaeprocesorolusuario;


        public BpMaeprocesorolusuarioServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesorolusuario = servicioProveedor.GetService<BpMaeprocesorolusuarioDao>();
        }

        public BpMaeprocesorolusuario coreInsertar(UsuarioActual usuarioActual, BpMaeprocesorolusuario bean)
        {
            string codigo;

            BpMaeprocesorolusuarioPk llave = new BpMaeprocesorolusuarioPk();

            llave.IdProceso = bean.IdProceso;
            llave.IdRol = bean.IdRol;
            llave.IdTipoSeguridad = bean.IdTipoSeguridad;
            llave.IdConcepto = bean.IdConcepto;

            codigo = bpMaeprocesorolusuario.obtenercodigo(llave);



            if (codigo != null)
            {
                throw new UException("El concepto seleccionado ya se encuentra registrado");
            }

            else
            {


                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpMaeprocesorolusuario.registrar(bean);
            }

        }

        public BpMaeprocesorolusuario coreActualizar(UsuarioActual usuarioActual, BpMaeprocesorolusuario bean)
        {
            

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpMaeprocesorolusuario.actualizar(bean);
            return bean;
        }


        public BpMaeprocesorolusuario obtenerPorId(BpMaeprocesorolusuarioPk llave)
        {
            return bpMaeprocesorolusuario.obtenerPorId(llave.obtenerArreglo());
        }

        public List<DtoBpMaeprocesorolusuario> listado(string pIdProceso, string pIdRol, string pIdTipoSeguridad)
        {
            
            return bpMaeprocesorolusuario.listado(pIdProceso, pIdRol, pIdTipoSeguridad);
        }

        public void eliminar(string pIdProceso, string pIdRol, string pIdTipoSeguridad, string pIdConcepto)
        {
            BpMaeprocesorolusuario objeto = new BpMaeprocesorolusuario();

            objeto.IdProceso = pIdProceso;
            objeto.IdRol = pIdRol;
            objeto.IdTipoSeguridad = pIdTipoSeguridad;
            objeto.IdConcepto = pIdConcepto;

            bpMaeprocesorolusuario.eliminar(objeto);
        }

    }
}

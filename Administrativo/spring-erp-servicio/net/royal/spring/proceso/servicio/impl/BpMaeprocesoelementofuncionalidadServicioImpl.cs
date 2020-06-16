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

    public class BpMaeprocesoelementofuncionalidadServicioImpl : GenericoServicioImpl, BpMaeprocesoelementofuncionalidadServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaeprocesoelementofuncionalidadDao bpMaeprocesoelementofuncionalidad;


        public BpMaeprocesoelementofuncionalidadServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesoelementofuncionalidad = servicioProveedor.GetService<BpMaeprocesoelementofuncionalidadDao>();
        }

        public BpMaeprocesoelementofuncionalidad coreInsertar(UsuarioActual usuarioActual, BpMaeprocesoelementofuncionalidad bean)
        {
            string codigo;

            BpMaeprocesoelementofuncionalidadPk llave = new BpMaeprocesoelementofuncionalidadPk();

            llave.IdProceso = bean.IdProceso;
            llave.IdElemento = bean.IdElemento;
            llave.IdFuncionalidad = bean.IdFuncionalidad;


            codigo = bpMaeprocesoelementofuncionalidad.obtenercodigo(llave);



            if (codigo != null)
            {
                throw new UException("La funcionalidad seleccionada ya se encuentra registrada");
            }

            else
            {


                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpMaeprocesoelementofuncionalidad.registrar(bean);
            }

        }

        public BpMaeprocesoelementofuncionalidad coreActualizar(UsuarioActual usuarioActual, BpMaeprocesoelementofuncionalidad bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpMaeprocesoelementofuncionalidad.actualizar(bean);
            return bean;
        }


        public BpMaeprocesoelementofuncionalidad obtenerPorId(BpMaeprocesoelementofuncionalidadPk llave)
        {
            return bpMaeprocesoelementofuncionalidad.obtenerPorId(llave.obtenerArreglo());
        }

        public List<DtoBpMaeprocesoelementofuncionalidad> listado(string pIdProceso, string pIdElemento)
        {
            
            return bpMaeprocesoelementofuncionalidad.listado(pIdProceso, pIdElemento);
        }

        public void eliminar(string pIdProceso, string pIdElemento, string pIdFuncionalidad)
        {
            BpMaeprocesoelementofuncionalidad objeto = new BpMaeprocesoelementofuncionalidad();

            objeto.IdProceso = pIdProceso;
            objeto.IdElemento = pIdElemento;
            objeto.IdFuncionalidad = pIdFuncionalidad;
            
            bpMaeprocesoelementofuncionalidad.eliminar(objeto);
        }

    }
}

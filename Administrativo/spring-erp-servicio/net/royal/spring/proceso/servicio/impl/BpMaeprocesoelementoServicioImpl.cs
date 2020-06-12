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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpMaeprocesoelementoServicioImpl : GenericoServicioImpl, BpMaeprocesoelementoServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaeprocesoelementoDao bpMaeprocesoelementoDao;


        public BpMaeprocesoelementoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesoelementoDao = servicioProveedor.GetService<BpMaeprocesoelementoDao>();
        }

        public BpMaeprocesoelemento obtenerPorId(BpMaeprocesoelementoPk pk)
        {
            return bpMaeprocesoelementoDao.obtenerPorId(pk.obtenerArreglo());
        }

        public BpMaeprocesoelemento coreInsertar(UsuarioActual usuarioActual, BpMaeprocesoelemento bean)
        {
            string cadena, codigo;
            codigo = bpMaeprocesoelementoDao.obtenercodigo(bean.IdProceso, bean.IdElemento);
            cadena = bpMaeprocesoelementoDao.obtenercadena(bean.IdProceso, bean.Nombre);


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
                bean.IdElemento = bean.IdElemento.ToUpper();
                bean.IdUnico = String.Concat("ELE", bean.IdProceso, bean.IdElemento); 
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                return bpMaeprocesoelementoDao.registrar(bean);
            }

        }

        public BpMaeprocesoelemento coreActualizar(UsuarioActual usuarioActual, BpMaeprocesoelemento bean)
        {

            
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpMaeprocesoelementoDao.actualizar(bean);
            return bean;
        }

        public List<DtoBpMaeprocesoelemento> listado(string codigo)
        {
            return bpMaeprocesoelementoDao.listado(codigo);
        }

        public void eliminar(string pIdProceso, string pIdelemento)
        {
            BpMaeprocesoelemento objeto = new BpMaeprocesoelemento();

            objeto.IdProceso = pIdProceso;
            objeto.IdElemento = pIdelemento;

            bpMaeprocesoelementoDao.eliminar(objeto);
        }

        public List<BpMaeprocesoelemento> listaSeg()
        {
            List<BpMaeprocesoelemento> lst = new List<BpMaeprocesoelemento>();


            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "TODOS" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "SOLOSOLICITANTE" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "SOLORESPONSABLE" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "SOLOAPROBADOR" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "SOLOACTUAL" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "SOLICI_Y_RESPON" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "INCLUIRSOLICITA" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "INCLUIRRESPONSA" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "INCLUIRAPROBADO" });
            lst.Add(new BpMaeprocesoelemento() { IdNivelSeguridad = "INCLUIRACTUAL" });


            return lst;
        }
    }
}

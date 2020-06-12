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

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpStatemachineServicioImpl : GenericoServicioImpl, BpStatemachineServicio
    {

        private IServiceProvider servicioProveedor;
        private BpProcesoDao bpProcesoDao;
        private BpProcesoconexionDao bpProcesoconexionDao;
        private BpMaeprocesoelementoDao bpMaeprocesoelementoDao;
        private BpTransaccionDao bpTransaccionDao;
        private BpTransaccionseguimientoDao bpTransaccionseguimientoDao;
        private BpMaeprocesoDao bpMaeprocesoDao;
        private BpTransaccionelementoDao bpTransaccionelementoDao;
        private BpProcesorequerimientoDao bpProcesorequerimientoDao;
        private BpTransaccionrequerimientoDao bpTransaccionrequerimientoDao;

        public BpStatemachineServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpProcesoconexionDao = servicioProveedor.GetService<BpProcesoconexionDao>();
            bpMaeprocesoelementoDao = servicioProveedor.GetService<BpMaeprocesoelementoDao>();
            bpProcesoDao = servicioProveedor.GetService<BpProcesoDao>();
            bpTransaccionDao = servicioProveedor.GetService<BpTransaccionDao>();
            bpMaeprocesoDao = servicioProveedor.GetService<BpMaeprocesoDao>();
            bpTransaccionseguimientoDao = servicioProveedor.GetService<BpTransaccionseguimientoDao>();
            bpTransaccionelementoDao = servicioProveedor.GetService<BpTransaccionelementoDao>();
            bpProcesorequerimientoDao = servicioProveedor.GetService<BpProcesorequerimientoDao>();
            bpTransaccionrequerimientoDao = servicioProveedor.GetService<BpTransaccionrequerimientoDao>();
        }

        public List<BpProcesoconexion> listarAccionesDisponiblesPorUsuario(UsuarioActual usuarioActual, string idProceso)
        {
            BpMaeprocesoelemento elementoEntrada = null;
            BpProceso proc = bpProcesoDao.obtenerActual(idProceso);
            Int32 idVersion = proc.IdVersion.Value;

            List<BpMaeprocesoelemento> lstElementoInicial = bpMaeprocesoelementoDao.listarElementosIniciales(idProceso);
            if (lstElementoInicial.Count == 1)
            {
                elementoEntrada = lstElementoInicial[0];
            }

            if (UString.estaVacio(elementoEntrada.IdRol))
            {
                return bpProcesoconexionDao.listarPorProcesoVersionElementoEntradaUsuario(idProceso, idVersion,
                        elementoEntrada.IdElemento);
            }

            return bpProcesoconexionDao.listarPorProcesoVersionElementoEntradaUsuario(idProceso, idVersion,
                    elementoEntrada.IdElemento, usuarioActual, 0, 0, 0);
        }

        public List<BpProcesoconexion> listarAccionesDisponiblesPorUsuario(UsuarioActual usuarioActual, string idProceso, BpTransaccion transaccion)
        {
            List<BpProcesoconexion> lstResultado = new List<BpProcesoconexion>();

            BpMaeprocesoelemento elementoEntrada = bpMaeprocesoelementoDao.obtenerPorId(
                    new BpMaeprocesoelementoPk(transaccion.ActualIdProceso, transaccion.ActualIdElemento).obtenerArreglo());

            if (UString.estaVacio(elementoEntrada.IdRol))
            {
                lstResultado = bpProcesoconexionDao.listarPorProcesoVersionElementoEntradaUsuario(idProceso,
                        transaccion.IdVersion.Value, elementoEntrada.IdElemento);
                return ejecutarValidacionRequerimiento(usuarioActual, transaccion, lstResultado);
            }

            lstResultado = bpProcesoconexionDao.listarPorProcesoVersionElementoEntradaUsuario(idProceso,
                    transaccion.IdVersion.Value, elementoEntrada.IdElemento, usuarioActual,
                    transaccion.IdPersonaSolicitante.Value, transaccion.IdPersonaReponsable,
                    transaccion.IdPersonaAprobador);

            return ejecutarValidacionRequerimiento(usuarioActual, transaccion, lstResultado);
        }

        public int registrarTransaccion(UsuarioActual usuarioActual, string idProceso, string comentarios)
        {
            BpProceso bpProceso = null;
            BpMaeproceso bpMaeProceso = null;
            List<BpProcesoconexion> listaConexion = null;
            BpMaeprocesoelemento elementoEntrada = null;
            BpMaeprocesoelemento elementoSalida = null;
            BpTransaccion transaccion = new BpTransaccion();
            BpTransaccionseguimiento transaccionSeguimiento = new BpTransaccionseguimiento();
            Nullable<Int32> idTransaccion = null;

            bpProceso = bpProcesoDao.obtenerActual(idProceso);
            idTransaccion = bpTransaccionDao.generarIdTransaccion();
            bpMaeProceso = bpMaeprocesoDao.obtenerPorId(new BpMaeprocesoPk() { IdProceso = idProceso }.obtenerArreglo());

            List<BpMaeprocesoelemento> lstElementoInicial = bpMaeprocesoelementoDao.listarElementosIniciales(idProceso);

            if (lstElementoInicial.Count == 1)
            {
                elementoEntrada = lstElementoInicial[0];

                List<BpMaeprocesoelemento> lstElementoSalida = bpProcesoconexionDao
                        .listarElementoPorElementoEntrada(elementoEntrada as BpMaeprocesoelementoPk);
                if (lstElementoSalida.Count == 1)
                    elementoSalida = lstElementoSalida[0];
            }
            else
            {
                throw new UException(BpTransaccion.REQUIERE_ELEMENTO_INICIAL);
            }

            BpProcesoconexion con = bpProcesoconexionDao.obtener(bpProceso.IdProceso,
                    bpProceso.IdVersion, elementoEntrada as BpMaeprocesoelementoPk, elementoSalida as BpMaeprocesoelementoPk);

            /*MensajeResultado resultado = ejecutarEventos(usuarioActual, transaccion, con, BpMaeEvento.TIPO_EVENTO_VALIDACION, mensajeGenerico);

            if (!resultado.FlgValido)
            {
                throw new UException(resultado.Mensajes);
            }*/

            transaccion.IdTransaccion = idTransaccion;
            transaccion.IdProceso = bpProceso.IdProceso;
            transaccion.IdVersion = bpProceso.IdVersion;
            transaccion.IdEstado = elementoSalida.IdEstado;
            transaccion.FechaInicio = DateTime.Today;
            transaccion.FechaFin = null;
            transaccion.FechaFinReal = null;
            transaccion.PorcentajeAvance = elementoSalida.PorcentajeAvance;
            if (elementoSalida != null)
            {
                transaccion.ActualIdProceso = elementoSalida.IdProceso;
                transaccion.ActualIdElemento = elementoSalida.IdElemento;
            }
            transaccion.CreacionFecha = DateTime.Today;
            transaccion.CreacionUsuario = usuarioActual.UsuarioLogin;
            transaccion.Descripcion = comentarios;
            transaccion.ActualIdPersona = usuarioActual.PersonaId;
            transaccion.IdPersonaSolicitante = usuarioActual.PersonaId;
            transaccion.FlagOcultarBandejaPendiente = bpMaeProceso.FlagOcultarBandejaPendiente;

            bpTransaccionDao.registrar(transaccion);

            transaccionSeguimiento = bpTransaccionseguimientoDao.registrar(usuarioActual, transaccion, comentarios, con);

            listaConexion = bpProcesoconexionDao.listarPorElementoEntrada(elementoSalida as BpMaeprocesoelementoPk);

            foreach (BpProcesoconexion bpProcesoConexion in listaConexion)
            {
                BpTransaccionelemento transaccionElemento = new BpTransaccionelemento();
                transaccionElemento.IdTransaccion = idTransaccion;

                transaccionElemento.ActualIdProceso = elementoSalida.IdProceso;
                transaccionElemento.ActualIdElemento = elementoSalida.IdElemento;

                transaccionElemento.SiguienteIdProceso = bpProcesoConexion.SalidaIdProceso;
                transaccionElemento.SiguienteIdElemento = bpProcesoConexion.SalidaIdElemento;

                transaccionElemento.FlagRealizado = "N";
                transaccionElemento.CreacionFecha = DateTime.Now;
                transaccionElemento.CreacionUsuario = usuarioActual.UsuarioLogin;
                bpTransaccionelementoDao.registrar(transaccionElemento);
            }

            List<BpProcesorequerimiento> listaProcesoRequerimiento = null;
            listaProcesoRequerimiento = bpProcesorequerimientoDao.listarPorProceso(bpProceso as BpProcesoPk);
            foreach (BpProcesorequerimiento bpProcesoRequerimiento in listaProcesoRequerimiento)
            {
                BpTransaccionrequerimiento entity = new BpTransaccionrequerimiento();
                entity.IdTransaccion = idTransaccion;
                entity.IdRequerimiento = bpProcesoRequerimiento.IdRequerimiento;
                entity.FlagObligatorio = bpProcesoRequerimiento.FlagObligatorio;
                entity.FlagCumplido = "N";
                entity.CreacionFecha = DateTime.Now;
                entity.CreacionUsuario = usuarioActual.UsuarioLogin;
                bpTransaccionrequerimientoDao.registrar(entity);
            }


            //this.ejecutarEventos(usuarioActual, transaccion, con, BpMaeEvento.TIPO_EVENTO_EJECUCION, mensajeGenerico);

            return idTransaccion.Value;
        }


        private List<BpProcesoconexion> ejecutarValidacionRequerimiento(UsuarioActual usuarioActual,
                    BpTransaccion transaccion, List<BpProcesoconexion> lstEntrada)
        {
            for (int i = 0; i < lstEntrada.Count; i++)
            {
                lstEntrada[i].AuxRequerimientoHabilitar = true;
                MensajeResultado resultado = new MensajeResultado() { FlgValido = true };//TO-DO this.ejecutarEventos(usuarioActual, transaccion, lstEntrada[i], BpMaeEvento.TIPO_EVENTO_REQUERIMIENTO_CONEXION, null);
                lstEntrada[i].AuxDescripcion = lstEntrada[i].AuxDescripcion;

                if (!resultado.FlgValido)
                {
                    lstEntrada[i].AuxRequerimientoHabilitar = false;
                    String str = UString.obtenerValorCadenaSinNulo(lstEntrada[i].AuxDescripcion);
                    str = str + "\n" + resultado.getMensajesTexto();
                    lstEntrada[i].AuxDescripcion = str;
                }
            }

            return lstEntrada;
        }


    }
}

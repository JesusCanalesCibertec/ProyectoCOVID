using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.constante;
using net.royal.spring.proceso.dominio.dto;
using net.royal.spring.framework.constante;
using net.royal.spring.proceso.servicio.evento;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpProcesoconexionServicioImpl : GenericoServicioImpl, BpProcesoconexionServicio
    {

        private IServiceProvider servicioProveedor;
        private BpProcesoconexionDao bpProcesoconexionDao;
        private BpMaeprocesoDao bpMaeProcesoDao;
        private BpMaeprocesoelementoDao bpMaeprocesoelementoDao;
        private BpMaeprocesorolDao bpMaeProcesoRolDao;
        private BpStatemachineServicio bpStatemachineServicio;
        private BpTransaccionDao bpTransaccionDao;
        private BpTransaccionseguimientoDao bpTransaccionseguimientoDao;
        private SyReportearchivoDao syReportearchivoDao;
        private SyReporteServicio syReporteServicio;
        private BpMaeeventoDao bpMaeeventoDao;

        private IEnumerable<BpEjecutarServicio> bpEjecutarServicioS;
        private IEnumerable<BpValidarServicio> bpValidarServicioS;

        public BpProcesoconexionServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpProcesoconexionDao = servicioProveedor.GetService<BpProcesoconexionDao>();
            bpMaeProcesoDao = servicioProveedor.GetService<BpMaeprocesoDao>();
            bpMaeprocesoelementoDao = servicioProveedor.GetService<BpMaeprocesoelementoDao>();
            bpMaeProcesoRolDao = servicioProveedor.GetService<BpMaeprocesorolDao>();
            bpStatemachineServicio = servicioProveedor.GetService<BpStatemachineServicio>();
            bpTransaccionDao = servicioProveedor.GetService<BpTransaccionDao>();
            bpTransaccionseguimientoDao = servicioProveedor.GetService<BpTransaccionseguimientoDao>();
            syReportearchivoDao = servicioProveedor.GetService<SyReportearchivoDao>();
            syReporteServicio = servicioProveedor.GetService<SyReporteServicio>();
            bpMaeeventoDao = servicioProveedor.GetService<BpMaeeventoDao>();
            //


        }

        //CTransaccionAcciones.listarTransacciones()
        public DtoBpProcesoconexion listarTransacciones(Int32? idTransaccionPadre, int idTransaccion, String idProceso, UsuarioActual usuarioActual)
        {
            BpTransaccion bpTransaccion;
            List<BpProcesoconexion> listaEstados;
            List<BpProcesoconexion> listaEstadosCompletos;
            DtoBpProcesoconexion dto = new DtoBpProcesoconexion();

            dto.flgVerGuardar = false;

            if (UInteger.esCeroOrNulo(idTransaccion))
            {
                bpTransaccion = null;
                listaEstadosCompletos = bpStatemachineServicio
                        .listarAccionesDisponiblesPorUsuario(usuarioActual, idProceso);

                listaEstados = listaFiltrada(listaEstadosCompletos);

                if (listaEstadosCompletos.Count == 1 && listaEstadosCompletos[0].auxElementoEntrada.IdTipoElemento == "INIC")
                {
                    if (UBoolean.validarFlag(listaEstadosCompletos[0].auxElementoEntrada.FlagPuedeRegistrar))
                        dto.flgVerGuardar = true;
                }
            }
            else
            {
                bpTransaccion = bpTransaccionDao.obtenerPorId(new BpTransaccionPk(idTransaccion).obtenerArreglo());

                idProceso = bpTransaccion.IdProceso;

                listaEstadosCompletos = bpStatemachineServicio
                        .listarAccionesDisponiblesPorUsuario(usuarioActual, idProceso, bpTransaccion);

                listaEstados = listaFiltrada(listaEstadosCompletos);

                // permisos de los botones

                BpMaeprocesoelemento elemento = bpMaeprocesoelementoDao
                        .obtenerPorIdConfiguracion(new BpMaeprocesoelementoPk(bpTransaccion.ActualIdProceso,
                                bpTransaccion.ActualIdElemento), idTransaccionPadre, listaEstados);

                dto.flgVerGuardar = elemento.auxFlgPuedeActualizar;

                if (!elemento.auxFlgPuedeConexiones)
                    listaEstados.Clear();

                //if (listaEstadosCompletos.Count == 0)
                //    dto.flgVerGuardar = false;
            }

            dto.lista = listaEstados;

            return dto;
        }

        private List<BpProcesoconexion> listaFiltrada(List<BpProcesoconexion> lstEstadosCompletos)
        {
            List<BpProcesoconexion> lst = new List<BpProcesoconexion>();
            foreach (BpProcesoconexion e in lstEstadosCompletos)
            {
                if (!UBoolean.validarFlag(e.FlagOcultarUsuario))
                    lst.Add(e);
            }
            return lst;
        }

        public string obtenerLeyendaPorProceso(string idProceso, UsuarioActual usuarioActual)
        {
            String leyenda = "";
            BpMaeprocesoelemento elementoEntrada = null;

            BpMaeproceso bpMaeProceso = bpMaeProcesoDao.obtenerPorId(new BpMaeprocesoPk(idProceso).obtenerArreglo());

            List<BpMaeprocesoelemento> lstElementoInicial = bpMaeprocesoelementoDao.listarElementosIniciales(idProceso);

            if (lstElementoInicial.Count == 1)
            {
                elementoEntrada = lstElementoInicial[0];
            }
            else
            {
                elementoEntrada = new BpMaeprocesoelemento();
            }

            leyenda = bpMaeProceso.Nombre;

            if (!UString.estaVacio(elementoEntrada.IdRol))
            {
                BpMaeprocesorol rol = bpMaeProcesoRolDao.obtenerPorId(
                        new BpMaeprocesorolPk(elementoEntrada.IdProceso, elementoEntrada.IdRol).obtenerArreglo());
                leyenda = leyenda + " ==>  " + usuarioActual.UsuarioLogin + "(ROL:" + rol.Nombre + ")";
            }
            else
            {
                leyenda = leyenda + " ==>  " + usuarioActual.UsuarioLogin;
            }

            return leyenda;
        }

        public void registrarNotificacionCorreo(BpProcesoconexion bpProcesoconexion, UsuarioActual usuarioActual)
        {
            if (UBoolean.validarFlag(bpProcesoconexion.FlagGenerarNotificación))
            {
                generarNotificacion(usuarioActual, bpProcesoconexion, bpProcesoconexion.AuxTransaccionBean, null, bpProcesoconexion.IdExterno);
            }
            if (UBoolean.validarFlag(bpProcesoconexion.FlagGenerarCorreo))
            {
                generarCorreo(usuarioActual, bpProcesoconexion, bpProcesoconexion.AuxTransaccionBean, null, null, null);
            }
        }

        private void generarCorreo(UsuarioActual usuarioActual, BpProcesoconexion con, BpTransaccion transaccion,
            BpMaeprocesoelemento elementoSalida, String idExterno, List<ParametroPersistenciaGenerico> lstMetadata)
        {

            if (transaccion == null)
                return;
            if (elementoSalida == null)
            {
                elementoSalida = con.auxElementoSalida;
                if (elementoSalida == null)
                    return;
            }

            BpProcesoconexioncomunicacionServicio comunicacion = servicioProveedor.GetService<BpProcesoconexioncomunicacionServicio>();
            BpTransaccionServicio bpTransaccionServicio = servicioProveedor.GetService<BpTransaccionServicio>();
            SyCorreoServicio correoServicio = servicioProveedor.GetService<SyCorreoServicio>();
            BpMaeprocesorolusuarioServicio bpMaeProcesoRolUsuarioServicio = servicioProveedor.GetService<BpMaeprocesorolusuarioServicio>();

            List<DtoTransaccionUsuario> listaUsuarios;

            BpMaeprocesorolPk bpMaeProcesoRolPk = new BpMaeprocesorolPk();
            bpMaeProcesoRolPk.IdProceso = elementoSalida.IdProceso;
            bpMaeProcesoRolPk.IdRol = elementoSalida.IdRol;

            listaUsuarios = bpTransaccionServicio.listarTransaccionSeguimiento(usuarioActual,
                    transaccion.IdTransaccion.Value, transaccion.IdProceso);

            List<DtoTransaccionUsuario> lstUsuarios2 = comunicacion.listarUsuariosPorProcesoConexion(con as BpProcesoconexionPk, BpProcesoconexioncomunicacion.TIPO_COMUNICACION_CORREO);

            listaUsuarios.AddRange(lstUsuarios2);

            DtoCorreoInterface correoEnviar = new DtoCorreoInterface();

            String asunto1 = bpTransaccionServicio.obtenerContenidoCorreo(1, transaccion.IdTransaccion);

            correoEnviar.asunto = asunto1;
            correoEnviar.listaCorreos = new List<String>();

            foreach (DtoTransaccionUsuario dtoPersonaCorreo in listaUsuarios)
            {
                if (!UString.estaVacio(dtoPersonaCorreo.correoInterno))
                {
                    correoEnviar.listaCorreos.Add(dtoPersonaCorreo.correoInterno);
                }
                else
                {
                    if (!UString.estaVacio(dtoPersonaCorreo.correoExterno))
                    {
                        correoEnviar.listaCorreos.Add(dtoPersonaCorreo.correoExterno);
                    }
                }
            }


            var listaCorreos = new List<DtoCorreo>();

            foreach (var item in correoEnviar.listaCorreos)
            {
                listaCorreos.Add(new DtoCorreo(-1, null, item));
            }


            DtoReporteParametro reportePk = new DtoReporteParametro(con.ReporteIdAplicacion, con.ReporteId);
            reportePk.Version = con.IdVersion.ToString();

            if (lstMetadata == null)
            {
                lstMetadata = new List<ParametroPersistenciaGenerico>();
            }

            lstMetadata.Add(new ParametroPersistenciaGenerico(ConstanteCorreo.PARAMETRO_ASUNTO, framework.constante.ConstanteUtil.TipoDato.String, correoEnviar.asunto));

            var listaEmail = syReporteServicio.generarListaCorreo(reportePk, lstMetadata, listaCorreos);

            var configCorreo = correoServicio.obtenerConfiguracion();
            correoServicio.enviarCorreInmediatoAsincrono(configCorreo, listaEmail);
        }

        private void generarNotificacion(UsuarioActual usuarioActual, BpProcesoconexion con, BpTransaccion transaccion,
            BpMaeprocesoelemento elementoSalida, String idExterno)
        {
            if (transaccion == null)
                return;
            BpTransaccionServicio bpTransaccionServicio = servicioProveedor.GetService<BpTransaccionServicio>();
            PmNotificacionServicio pmNotificacionServicio = servicioProveedor.GetService<PmNotificacionServicio>();
            BpMaeprocesorolusuarioServicio bpMaeProcesoRolUsuarioServicio = servicioProveedor.GetService<BpMaeprocesorolusuarioServicio>();

            BpMaeproceso bpMaeProceso = bpMaeProcesoDao.obtenerPorId(new BpMaeprocesoPk(con.IdProceso).obtenerArreglo());

            if (elementoSalida == null)
            {
                elementoSalida = elementoSalida = bpMaeprocesoelementoDao
                        .obtenerPorId(new BpMaeprocesoelementoPk(con.SalidaIdProceso, con.SalidaIdElemento).obtenerArreglo());
            }

            DtoInterfazNotificacion dtoInterfazNotificacion = new DtoInterfazNotificacion();
            dtoInterfazNotificacion.procesoId = con.IdProceso;

            if (transaccion != null)
            {
                dtoInterfazNotificacion.procesoIdTransaccion = transaccion.IdTransaccion.Value;
                if (UString.estaVacio(transaccion.Descripcion))
                    dtoInterfazNotificacion.titulo = bpMaeProceso.NombreCorto + " : " + transaccion.IdTransaccion;
                else
                    dtoInterfazNotificacion.titulo = bpMaeProceso.NombreCorto + " : " + transaccion.IdTransaccion + " ( " + transaccion.Descripcion + " )";
            }
            else
            {
                dtoInterfazNotificacion.titulo = bpMaeProceso.Nombre + " : " + idExterno;
            }

            dtoInterfazNotificacion.procesoParametros = idExterno;

            List<DtoTransaccionUsuario> lst = bpTransaccionServicio.listarTransaccionSeguimiento(usuarioActual,
                    transaccion.IdTransaccion.Value, transaccion.IdProceso);

            BpMaeprocesorolPk bpMaeProcesoRolPk = new BpMaeprocesorolPk();
            bpMaeProcesoRolPk.IdProceso = elementoSalida.IdProceso;
            bpMaeProcesoRolPk.IdRol = elementoSalida.IdRol;
            dtoInterfazNotificacion.listaUsuarios = lst;

            pmNotificacionServicio.eliminarNotificacionesPorTransaccion(usuarioActual, transaccion.IdTransaccion);
            pmNotificacionServicio.generarNotificacionExterna(usuarioActual, dtoInterfazNotificacion);

        }

        public void validarBpStateMachineServicio(BpProcesoconexion bpProcesoconexion, UsuarioActual usuarioActual)
        {
            if (!UInteger.esCeroOrNulo(bpProcesoconexion.AuxTransaccion))
            {
                bpProcesoconexion.AuxTransaccionBean = bpTransaccionDao.obtenerPorId(new BpTransaccionPk() { IdTransaccion = bpProcesoconexion.AuxTransaccion }.obtenerArreglo());
            }

            validar(usuarioActual, bpProcesoconexion.AuxTransaccion, bpProcesoconexion.SalidaIdElemento);
            registrarSeguimiento(bpProcesoconexion, usuarioActual);
            registrarNotificacionCorreo(bpProcesoconexion, usuarioActual);
        }

        public BpTransaccionseguimiento registrarSeguimiento(BpProcesoconexion bpProcesoconexion, UsuarioActual usuarioActual)
        {

            Int32 idTransaccion = bpProcesoconexion.AuxTransaccion.HasValue ? bpProcesoconexion.AuxTransaccion.Value : 0;
            String comentarios = bpProcesoconexion.AuxComentario;

            BpMaeprocesoelementoPk actividadSalida = new BpMaeprocesoelementoPk() { IdElemento = bpProcesoconexion.SalidaIdElemento, IdProceso = bpProcesoconexion.SalidaIdProceso };

            BpMaeprocesoelemento elementoEntrada = null;
            BpMaeprocesoelemento elementoSalida = null;
            BpTransaccionseguimiento transaccionSeguimiento;
            BpTransaccion transaccion = bpTransaccionDao.obtenerPorId(new BpTransaccionPk(idTransaccion).obtenerArreglo());


            elementoEntrada = bpMaeprocesoelementoDao.obtenerPorId(
                    new BpMaeprocesoelementoPk(transaccion.ActualIdProceso, transaccion.ActualIdElemento).obtenerArreglo());
            if (actividadSalida != null)
                elementoSalida = bpMaeprocesoelementoDao.obtenerPorId(actividadSalida.obtenerArreglo());

            BpProcesoconexion con = bpProcesoconexionDao.obtener(transaccion.IdProceso, transaccion.IdVersion,
                    elementoEntrada as BpMaeprocesoelementoPk, elementoSalida as BpMaeprocesoelementoPk);
            if (con == null)
            {
                throw new UException("PROCESO CONEXION NO ENCONTRADO");
            }

            MensajeResultado resultado = this.ejecutarEventos(usuarioActual, transaccion, con, BpMaeevento.TIPO_EVENTO_VALIDACION);
            if (resultado.FlgValido == false)
            {
                throw new UException(resultado.Mensajes, true);
            }

            if (elementoSalida != null)
            {
                transaccion.ActualIdProceso = elementoSalida.IdProceso;
                transaccion.ActualIdElemento = elementoSalida.IdElemento;

                if (!UString.estaVacio(elementoSalida.IdEstado))
                    transaccion.IdEstado = elementoSalida.IdEstado;
                if (!UInteger.esCeroOrNulo(elementoSalida.PorcentajeAvance))
                {
                    if (elementoSalida.PorcentajeAvance != 0)
                        transaccion.PorcentajeAvance = elementoSalida.PorcentajeAvance;
                }
            }

            transaccionSeguimiento = bpTransaccionseguimientoDao.registrar(usuarioActual, transaccion, comentarios, con);

            transaccion.ActualIdPersona = usuarioActual.PersonaId;

            transaccion.ModificacionFecha = DateTime.Now;
            transaccion.ModificacionUsuario = usuarioActual.UsuarioLogin;

            bpTransaccionDao.actualizar(transaccion);

            //EJECUTAR EVENTO DE EJECUCION TO-DO
            this.ejecutarEventos(usuarioActual, transaccion, con, BpMaeevento.TIPO_EVENTO_EJECUCION);

            return transaccionSeguimiento;
        }


        public void validar(UsuarioActual usuarioActual, Nullable<Int32> idTransaccion, String idElemento)
        {
            List<MensajeUsuario> resultados = new List<MensajeUsuario>();
            BpMaeprocesoelemento elementoEntrada = null;
            BpMaeprocesoelemento elementoSalida = new BpMaeprocesoelemento();

            BpTransaccion transaccion = bpTransaccionDao.obtenerPorId(new BpTransaccionPk(idTransaccion).obtenerArreglo());
            if (transaccion == null)
            {
                resultados
                        .Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "No existe la transacción"));
                throw new UException(resultados);
            }

            elementoEntrada = bpMaeprocesoelementoDao.obtenerPorId(
                    new BpMaeprocesoelementoPk(transaccion.ActualIdProceso, transaccion.ActualIdElemento).obtenerArreglo());
            if (elementoEntrada == null)
            {
                resultados
                        .Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "No existe un elemento de entrada"));
                throw new UException(resultados);
            }

            elementoSalida.IdProceso = transaccion.IdProceso;
            elementoSalida.IdElemento = idElemento;

            BpProcesoconexion con = bpProcesoconexionDao.obtener(transaccion.IdProceso, transaccion.IdVersion,
                    (elementoEntrada as BpMaeprocesoelementoPk), (elementoSalida as BpMaeprocesoelementoPk));
            if (con == null)
            {
                resultados
                        .Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR,
                                "No existe una conexión activa" + "    ENTRADA::" + elementoEntrada.IdElemento
                                        + "   SALIDAD::" + elementoSalida.IdElemento + "   PROCESO::"
                                        + transaccion.IdProceso));
                throw new UException(resultados);
            }

            //REQUERIMIENTOS TO-DO
            /*if (UBoolean.validarFlag(con.getFlgValidarRequerimiento()))
            {
                List<DtoTransaccionRequerimiento> lst = bpTransaccionRequerimientoDao
                        .listaDocumentosPendientes(transaccion.getPk().getIdTransaccion());
                for (DtoTransaccionRequerimiento dtoTransaccionRequerimiento : lst)
                {
                    if (UBoolean.validarFlag(dtoTransaccionRequerimiento.getFlgObligatorio()))
                    {
                        if (!UBoolean.validarFlag(dtoTransaccionRequerimiento.getFlgCumplido()))
                        {
                            resultados.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR,
                                    "Requiere : " + dtoTransaccionRequerimiento.getNombre()));
                        }
                    }
                }
                if (resultados.Count > 0)
                    throw new UException(resultados);
            }*/


            ejecutarEventos(usuarioActual, transaccion, con, BpMaeevento.TIPO_EVENTO_VALIDACION);
        }

        private MensajeResultado ejecutarEventos(UsuarioActual usuarioActual, BpTransaccion transaccion, BpProcesoconexion bpProcesoConexion, String idTipoEvento)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            MensajeResultado resultado = new MensajeResultado();
            List<BpMaeevento> listaEventos = null;

            resultado.FlgValido = true;
            resultado.Mensajes = new List<MensajeUsuario>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_transaccion", ConstanteUtil.TipoDato.Integer, transaccion.IdTransaccion));
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, usuarioActual.UsuarioLogin));

            listaEventos = bpMaeeventoDao.listarEventosPorConexion(bpProcesoConexion as BpProcesoconexionPk, idTipoEvento);

            if (listaEventos == null)
                listaEventos = new List<BpMaeevento>();
            foreach (BpMaeevento item in listaEventos)
            {
                if (BpMaeevento.TIPO_OBJETO_BD_STORE_PROCEDURE == item.IdTipoobjeto)
                {
                    MensajeUsuario msg = bpMaeeventoDao.ejecutarEvento(item, parametros);
                    if (msg != null)
                    {
                        resultado.FlgValido = false;
                        resultado.Mensajes.Add(msg);
                    }
                }
                if (BpMaeevento.TIPO_OBJETO_JAVA_SPRING == item.IdTipoobjeto)
                {

                    if (item.IdTipoevento == BpMaeevento.TIPO_EVENTO_VALIDACION || item.IdTipoevento == BpMaeevento.TIPO_EVENTO_REQUERIMIENTO_CONEXION)
                    {

                        MensajeResultado msg1 = null;

                        if (item.IdTipoevento == BpMaeevento.TIPO_EVENTO_VALIDACION)
                        {
                            bpValidarServicioS = servicioProveedor.GetServices<BpValidarServicio>();
                            BpValidarServicio bpInterfaceServicioValidacion = bpValidarServicioS.FirstOrDefault(x => x.GetType().ToString() == item.ObjetoNetCore);
                            if (bpInterfaceServicioValidacion != null)
                            {
                                msg1 = bpInterfaceServicioValidacion.validar(usuarioActual, transaccion, bpProcesoConexion);
                            }
                        }
                        else
                        {
                            //BpRequerimientoServicio bpInterfaceServicioRequerimiento = (BpRequerimientoServicio)obj;
                            //msg1 = bpInterfaceServicioRequerimiento.cumpleRequerimiento(usuarioActual, transaccion, bpProcesoConexion, mensajeGenerico);
                        }

                        if (msg1 == null)
                            msg1 = new MensajeResultado();
                        if (msg1.FlgValido == false)
                        {
                            resultado.FlgValido = false;
                            resultado.Mensajes.AddRange(msg1.Mensajes);
                        }
                    }
                    if (item.IdTipoevento == BpMaeevento.TIPO_EVENTO_EJECUCION)
                    {
                        bpEjecutarServicioS = servicioProveedor.GetServices<BpEjecutarServicio>();
                        BpEjecutarServicio bpInterfaceServicioEjecucion = bpEjecutarServicioS.FirstOrDefault(x => x.GetType().ToString() == item.ObjetoNetCore);
                        if (bpInterfaceServicioEjecucion != null)
                        {
                            bpInterfaceServicioEjecucion.ejecutar(usuarioActual, transaccion, bpProcesoConexion);
                        }
                    }
                }
            }
            return resultado;
        }



        /*ERNESTO*/
        public BpProcesoconexion coreInsertar(UsuarioActual usuarioActual, BpProcesoconexion bean)
        {
            if (bean.EntradaIdProceso == bean.SalidaIdProceso && bean.EntradaIdElemento == bean.SalidaIdElemento)
            {
                throw new UException("Ambos datos de la entrada son iguales con los de la salida");
            }

            string cadena = bpProcesoconexionDao.obtenercadena(bean.IdProceso, bean.IdVersion, bean.Accion);

            if (cadena != null)
            {
                throw new UException("La acción ingresada ya se encuentra registrada");
            }

            bean.IdConexion = bpProcesoconexionDao.generarCodigo(bean.IdProceso);
            bean.IdUnico = String.Concat("CON", bean.IdProceso, bean.IdVersion, bean.IdConexion);
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            return bpProcesoconexionDao.registrar(bean);


        }

        public BpProcesoconexion coreActualizar(UsuarioActual usuarioActual, BpProcesoconexion bean)
        {
            
            if (bean.EntradaIdProceso == bean.SalidaIdProceso && bean.EntradaIdElemento == bean.SalidaIdElemento)
            {
                throw new UException("Ambos datos de la entrada son iguales con los de la salida");
            }

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.bpProcesoconexionDao.actualizar(bean);
            return bean;
        }


        public BpProcesoconexion obtenerPorId(BpProcesoconexionPk llave)
        {
            return bpProcesoconexionDao.obtenerPorId(llave.obtenerArreglo());
        }

        public List<DtoBpProcesoconexion> listado(string codigo)
        {
            return bpProcesoconexionDao.listado(codigo);
        }

        public void eliminar(string pIdProceso, Int32 pIdConexion, Int32 pIdVersion)
        {
            BpProcesoconexion objeto = new BpProcesoconexion();

            objeto.IdProceso = pIdProceso;
            objeto.IdConexion = pIdConexion;
            objeto.IdVersion = pIdVersion;

            bpProcesoconexionDao.eliminar(objeto);
        }
        /*ERNESTO*/

    }
}

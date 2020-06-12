using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System.Transactions;
using net.royal.spring.framework.core;

namespace net.royal.spring.minedu.servicio.impl
{
    public class MpContratacionServicioImpl : GenericoServicioImpl, MpContratacionServicio
    {
        private IServiceProvider servicioProveedor;
        private MpContratacionDao mpContratacionDao;
        private MpPersonaDao mpPersonaDao;
        private MpContratacionadendaentregableDao mpContratacionadendaentregableDao;

        public MpContratacionServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            mpContratacionDao = servicioProveedor.GetService<MpContratacionDao>();
            mpContratacionadendaentregableDao = servicioProveedor.GetService<MpContratacionadendaentregableDao>();
            mpPersonaDao = servicioProveedor.GetService<MpPersonaDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return mpContratacionDao.listarPaginacion(paginacion, filtro);
        }

        public MpContratacion registrar(UsuarioActual usuarioActual, MpContratacion bean)
        {
            string mensaje = "";
            try
            {
                MpContratacion existe = mpContratacionDao.ObtenerContratoxNOrden(bean.Numeroorden);
                if (existe != null && bean.IdModalidad == "OS")
                {
                    mensaje = "El número de orden ingresado se encuentra registrado";
                    throw new Exception();
                }

                MpPersona persona = mpPersonaDao.obtenerPorId(new MpPersonaPk() { IdPersona = bean.IdPersona }.obtenerArreglo());
                persona.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";
                mpPersonaDao.actualizar(persona);

                bean.IdContratacion = mpContratacionDao.generarCodigo();
                bean.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";

                bean = mpContratacionDao.registrar(usuarioActual, bean);

                return bean;
            }
            catch (Exception)
            {
                if (UString.estaVacio(mensaje)) mensaje = "Error en el proceso de registro";
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, mensaje));
                throw new UException(lista);
            }
        }

        public MpContratacion obtenerPorId(int pIdContratacion)
        {
            return mpContratacionDao.obtenerPorId(new MpContratacion() { IdContratacion = pIdContratacion }.obtenerArreglo());
        }

        public MpContratacion actualizar(UsuarioActual usuarioActual, MpContratacion bean)
        {
            try
            {
                MpPersona persona = mpPersonaDao.obtenerPorId(new MpPersonaPk() { IdPersona = bean.IdPersona }.obtenerArreglo());
                persona.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";
                mpPersonaDao.actualizar(persona);

                bean.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";

                bean = mpContratacionDao.actualizar(usuarioActual, bean);

                return bean;
            }
            catch (Exception)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de actualización"));
                throw new UException(lista);
            }
        }

        public MpContratacionPk cambiarestado(MpContratacionPk pk)
        {
            MpContratacion bean = mpContratacionDao.obtenerPorId(pk.obtenerArreglo());
            bean.Estado = bean.Estado == "A" ? "I" : "A";

            mpContratacionDao.actualizar(bean);
            return pk;
        }

        public MpContratacion registrarlista(UsuarioActual usuarioActual, MpContratacion bean)
        {
            try
            {
                MpContratacion contratacion = new MpContratacion();
                List<MpContratacionadendaentregable> lista = new List<MpContratacionadendaentregable>();

                lista = mpContratacionadendaentregableDao.listado(bean.IdContratacion);

                if (lista != null)
                {
                    mpContratacionadendaentregableDao.eliminarDetalle(bean.IdContratacion);
                }

                contratacion = mpContratacionDao.obtenerPorId(new MpContratacionPk() { IdContratacion = bean.IdContratacion }.obtenerArreglo());
                MpPersona persona = mpPersonaDao.obtenerPorId(new MpPersonaPk() { IdPersona = contratacion.IdPersona }.obtenerArreglo());

                contratacion.Numeroplazo = bean.listadetalle == null ? 0 : bean.listadetalle.Count;

                if (contratacion.IdModalidad != "OS")
                {
                    if (bean.listadetalle.Count > 0)
                    {
                        persona.Estado = DateTime.Now.Date > bean.listadetalle[bean.listadetalle.Count - 1].Fechafin ? "I" : "A";
                        contratacion.Estado = DateTime.Now.Date > bean.listadetalle[bean.listadetalle.Count - 1].Fechafin ? "I" : "A";
                    }
                    else
                    {
                        contratacion.Estado = DateTime.Now.Date > contratacion.Fechafin ? "I" : "A";
                        persona.Estado = DateTime.Now.Date > contratacion.Fechafin ? "I" : "A";
                    }
                }
                mpContratacionDao.actualizar(contratacion);
                mpPersonaDao.actualizar(persona);

                foreach (var item in bean.listadetalle)
                {
                    mpContratacionadendaentregableDao.registrar(item);
                }

                return bean;

            }
            catch (Exception)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de registro"));
                throw new UException(lista);
            }
        }

        public List<DtoContratacion> Contratosactivos()
        {
            return mpContratacionDao.Contratosactivos();
        }

        public List<DtoContratacion> ListarPersonaldisponible(DtoTabla filtro)
        {
            try
            {
                List<DtoContratacion> lista = new List<DtoContratacion>();
                foreach (var item in lista)
                {
                    DtoTabla auxDto = new DtoTabla();
                    auxDto.fechainicio1 = filtro.fechainicio1;
                    auxDto.fechafin1 = filtro.fechafin1;
                    auxDto.CodigoNumerico = filtro.CodigoNumerico;
                    auxDto.Secuencia = filtro.Secuencia;
                    auxDto.Id = item.IdContratacion;

                    List<DtoFechasdisponibles> auxlista = new List<DtoFechasdisponibles>();
                    auxlista = mpContratacionDao.obtenerFechasnodisponibles(auxDto);

                    if (auxlista != null)
                    {
                        foreach (var aux in auxlista)
                        {
                            item.diasocupados.Add(aux.fecha);
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de consulta"));
                throw new UException(lista);
            }
        }

        public List<DtoTabla> ListarPie()
        {
            return mpContratacionDao.ListarPie();
        }

        public List<DtoContratacion> ListarContratoPorPersona(int pIdPersona)
        {
            return mpContratacionDao.ListarContratoPorPersona(pIdPersona);
        }

        public ParametroPaginacionGenerico ListarPersonaldisponible1(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return mpContratacionDao.ListarPersonaldisponible1(paginacion, filtro);
        }
    }
}

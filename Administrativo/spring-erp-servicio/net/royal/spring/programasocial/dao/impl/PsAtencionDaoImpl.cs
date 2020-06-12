
using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.programasocial.dominio.dtos;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.dao.impl {
    public class PsAtencionDaoImpl : GenericoDaoImpl<PsAtencion>, PsAtencionDao {
        private IServiceProvider servicioProveedor;
        private PsAtencionDetalleDao psAtencionDetalleDao;
        private PsAtencionTratamientoDao psAtencionTratamientoDao;


        public PsAtencionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psatencion") {
            servicioProveedor = _servicioProveedor;
            psAtencionDetalleDao = servicioProveedor.GetService<PsAtencionDetalleDao>();
            psAtencionTratamientoDao = servicioProveedor.GetService<PsAtencionTratamientoDao>();
        }

        public PsAtencion obtenerPorId(PsAtencionPk id) {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsAtencion obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion) {
            return obtenerPorId(new PsAtencionPk(pIdInstitucion, pIdBeneficiario, pIdAtencion));
        }

        public int generarCodigo() {
            IQueryable<PsAtencion> query = this.getCriteria();

            List<PsAtencion> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0) {
                var = (int)(lst.Max(bean => bean.IdAtencion));
            }

            return var + 1;
        }

        private List<MensajeUsuario> validar(DtoAtencion bean) {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            List<Nullable<Int32>> lista = new List<Nullable<Int32>>();
            lista.Add(bean.IdDiagnostico01);
            lista.Add(bean.IdDiagnostico02);
            lista.Add(bean.IdDiagnostico03);
            lista.Add(bean.IdDiagnostico04);
            lista.Add(bean.IdDiagnostico05);

            foreach (var item in lista.GroupBy(x => x)) {
                if (item.Key != null) {
                    if (item.Count() > 1) {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Existen Diagnósticos Repetidos"));
                    }
                }
            }

            return lstRetorno;
        }

        public PsAtencion coreInsertar(UsuarioActual usuarioActual, DtoAtencion bean) {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            PsAtencion psAtencion = new PsAtencion();
            psAtencion.IdBeneficiario = bean.IdBeneficiario;
            psAtencion.IdInstitucion = bean.IdInstitucion;
            psAtencion.IdPeriodo = bean.idPeriodo;
            psAtencion.FechaAtencion = bean.FechaAtencion;
            psAtencion.IdAtencion = this.generarCodigo();
            psAtencion.IdTipoAtencion = bean.IdTipoAtencion;
            psAtencion.IdArea = bean.IdArea;
            psAtencion.Estado = "A";
            psAtencion.comentarios = bean.comentario;

            this.registrar(psAtencion);

            PsAtencionDetalle psAtencionDetalle = new PsAtencionDetalle();

            if (bean.IdDiagnostico01 != null) {
                psAtencionDetalle = new PsAtencionDetalle();
                psAtencionDetalle.IdAtencion = psAtencion.IdAtencion;
                psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico01;
                psAtencionDetalle.Posicion = 1;
                psAtencionDetalle.Estado = "A";
                psAtencionDetalleDao.registrar(psAtencionDetalle);

                if (!String.IsNullOrEmpty(bean.IdTratamiento01_1)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_1;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                    psAtencionTratamiento.PosicionTratamiento = 1;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento01_2)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_2;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                    psAtencionTratamiento.PosicionTratamiento = 2;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento01_3)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_3;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                    psAtencionTratamiento.PosicionTratamiento = 3;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }
            }

            if (bean.IdDiagnostico02 != null) {
                psAtencionDetalle = new PsAtencionDetalle();
                psAtencionDetalle.IdAtencion = psAtencion.IdAtencion;
                psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico02;

                psAtencionDetalle.Posicion = 2;
                psAtencionDetalle.Estado = "A";
                psAtencionDetalleDao.registrar(psAtencionDetalle);


                if (!String.IsNullOrEmpty(bean.IdTratamiento02_1)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_1;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                    psAtencionTratamiento.PosicionTratamiento = 1;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento02_2)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_2;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                    psAtencionTratamiento.PosicionTratamiento = 2;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento02_3)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_3;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                    psAtencionTratamiento.PosicionTratamiento = 3;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

            }

            if (bean.IdDiagnostico03 != null) {
                psAtencionDetalle = new PsAtencionDetalle();
                psAtencionDetalle.IdAtencion = psAtencion.IdAtencion;
                psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico03;

                psAtencionDetalle.Posicion = 3;
                psAtencionDetalle.Estado = "A";
                psAtencionDetalleDao.registrar(psAtencionDetalle);

                if (!String.IsNullOrEmpty(bean.IdTratamiento03_1)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_1;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                    psAtencionTratamiento.PosicionTratamiento = 1;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento03_2)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_2;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                    psAtencionTratamiento.PosicionTratamiento = 2;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento03_3)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_3;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                    psAtencionTratamiento.PosicionTratamiento = 3;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

            }

            if (bean.IdDiagnostico04 != null) {
                psAtencionDetalle = new PsAtencionDetalle();
                psAtencionDetalle.IdAtencion = psAtencion.IdAtencion;
                psAtencionDetalle.Posicion = 4;
                psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico04;

                psAtencionDetalle.Estado = "A";
                psAtencionDetalleDao.registrar(psAtencionDetalle);

                if (!String.IsNullOrEmpty(bean.IdTratamiento04_1)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_1;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                    psAtencionTratamiento.PosicionTratamiento = 1;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento04_2)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_2;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                    psAtencionTratamiento.PosicionTratamiento = 2;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento04_3)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_3;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                    psAtencionTratamiento.PosicionTratamiento = 3;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }
            }


            if (bean.IdDiagnostico05 != null) {
                psAtencionDetalle = new PsAtencionDetalle();
                psAtencionDetalle.IdAtencion = psAtencion.IdAtencion;
                psAtencionDetalle.Posicion = 5;
                psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico05;

                psAtencionDetalle.Estado = "A";
                psAtencionDetalleDao.registrar(psAtencionDetalle);

                if (!String.IsNullOrEmpty(bean.IdTratamiento05_1)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_1;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                    psAtencionTratamiento.PosicionTratamiento = 1;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento05_2)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_2;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                    psAtencionTratamiento.PosicionTratamiento = 2;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento05_3)) {
                    PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
                    psAtencionTratamiento.IdAtencion = psAtencion.IdAtencion;
                    psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_3;
                    psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                    psAtencionTratamiento.PosicionTratamiento = 3;
                    psAtencionTratamiento.Estado = "A";
                    psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                }
            }

            return psAtencion;
        }

        public PsAtencion coreActualizar(UsuarioActual usuarioActual, DtoAtencion bean) {
            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            PsAtencionDetalle psAtencionDetalle = new PsAtencionDetalle();
            PsAtencionTratamiento psAtencionTratamiento = new PsAtencionTratamiento();
            PsAtencion psAtencion = this.obtenerPorId(new PsAtencionPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdAtencion));
            psAtencion.FechaAtencion = bean.FechaAtencion;

            psAtencion.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            psAtencion.ModificacionFecha = DateTime.Now;
            psAtencion.ModificacionUsuario = usuarioActual.UsuarioLogin;
            psAtencion.comentarios = bean.comentario;
            this.actualizar(psAtencion);


            if (bean.IdDiagnostico01 != null) {
                psAtencionDetalle = psAtencionDetalleDao.obtenerPorId(
                new PsAtencionDetallePk(bean.IdAtencion, bean.IdDiagnostico01).obtenerArreglo());

                if (psAtencionDetalle == null) {
                    psAtencionDetalle = new PsAtencionDetalle();
                    psAtencionDetalle.IdAtencion = bean.IdAtencion;
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico01;

                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalle.Posicion = 1;
                    psAtencionDetalleDao.registrar(psAtencionDetalle);
                }
                else {
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico01;

                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.actualizar(psAtencionDetalle);
                }



                if (!String.IsNullOrEmpty(bean.IdTratamiento01_1)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                        new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico01, bean.IdTratamiento01_1).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.PosicionTratamiento = 1;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }


                if (!String.IsNullOrEmpty(bean.IdTratamiento01_2)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                    new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico01, bean.IdTratamiento01_2).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.PosicionTratamiento = 2;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento01_3)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                     new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico01, bean.IdTratamiento01_3).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                        psAtencionTratamiento.PosicionTratamiento = 3;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento01_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico01;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }
            }

            if (bean.IdDiagnostico02 != null) {

                psAtencionDetalle = psAtencionDetalle = psAtencionDetalleDao.obtenerPorId(
                new PsAtencionDetallePk(bean.IdAtencion, bean.IdDiagnostico02).obtenerArreglo());

                if (psAtencionDetalle == null) {
                    psAtencionDetalle = new PsAtencionDetalle();
                    psAtencionDetalle.IdAtencion = bean.IdAtencion;
                    psAtencionDetalle.Posicion = 2;
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico02;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.registrar(psAtencionDetalle);
                }
                else {
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico02;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.actualizar(psAtencionDetalle);
                }



                if (!String.IsNullOrEmpty(bean.IdTratamiento02_1)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                  new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico02, bean.IdTratamiento02_1).obtenerArreglo());
                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.PosicionTratamiento = 1;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento02_2)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                  new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico02, bean.IdTratamiento02_2).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                        psAtencionTratamiento.PosicionTratamiento = 2;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }



                if (!String.IsNullOrEmpty(bean.IdTratamiento02_3)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                 new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico02, bean.IdTratamiento02_3).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                        psAtencionTratamiento.PosicionTratamiento = 3;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento02_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico02;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }

            }

            if (bean.IdDiagnostico03 != null) {
                psAtencionDetalle = psAtencionDetalleDao.obtenerPorId(
                new PsAtencionDetallePk(bean.IdAtencion, bean.IdDiagnostico03).obtenerArreglo());


                if (psAtencionDetalle == null) {
                    psAtencionDetalle = new PsAtencionDetalle();
                    psAtencionDetalle.IdAtencion = bean.IdAtencion;
                    psAtencionDetalle.Posicion = 3;
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico03;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.registrar(psAtencionDetalle);
                }
                else {
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico03;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.actualizar(psAtencionDetalle);
                }



                if (!String.IsNullOrEmpty(bean.IdTratamiento03_1)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                   new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico03, bean.IdTratamiento03_1).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                        psAtencionTratamiento.PosicionTratamiento = 1;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }


                if (!String.IsNullOrEmpty(bean.IdTratamiento03_2)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico03, bean.IdTratamiento03_2).obtenerArreglo());
                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                        psAtencionTratamiento.PosicionTratamiento = 2;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }


                if (!String.IsNullOrEmpty(bean.IdTratamiento03_3)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                    new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico03, bean.IdTratamiento03_3).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                        psAtencionTratamiento.PosicionTratamiento = 3;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico03;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }
            }

            if (bean.IdDiagnostico04 != null) {
                psAtencionDetalle = psAtencionDetalleDao.obtenerPorId(
               new PsAtencionDetallePk(bean.IdAtencion, bean.IdDiagnostico04).obtenerArreglo());

                if (psAtencionDetalle == null) {
                    psAtencionDetalle = new PsAtencionDetalle();
                    psAtencionDetalle.IdAtencion = bean.IdAtencion;
                    psAtencionDetalle.Posicion = 4;
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico04;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.registrar(psAtencionDetalle);
                }
                else {
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico04;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.actualizar(psAtencionDetalle);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento04_1)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                   new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico04, bean.IdTratamiento04_1).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();
                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;

                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                        psAtencionTratamiento.PosicionTratamiento = 1;


                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }


                if (!String.IsNullOrEmpty(bean.IdTratamiento04_2)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico04, bean.IdTratamiento04_2).obtenerArreglo());
                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();

                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;

                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                        psAtencionTratamiento.PosicionTratamiento = 2;


                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }


                if (!String.IsNullOrEmpty(bean.IdTratamiento04_3)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                    new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico04, bean.IdTratamiento04_3).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();

                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;

                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento03_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                        psAtencionTratamiento.PosicionTratamiento = 3;


                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento04_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico04;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }
            }

            if (bean.IdDiagnostico05 != null) {
                psAtencionDetalle = psAtencionDetalleDao.obtenerPorId(
               new PsAtencionDetallePk(bean.IdAtencion, bean.IdDiagnostico05).obtenerArreglo());

                if (psAtencionDetalle == null) {
                    psAtencionDetalle = new PsAtencionDetalle();
                    psAtencionDetalle.IdAtencion = bean.IdAtencion;
                    psAtencionDetalle.Posicion = 5;
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico05;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.registrar(psAtencionDetalle);
                }
                else {
                    psAtencionDetalle.IdDiagnostico = bean.IdDiagnostico05;
                    psAtencionDetalle.Estado = "A";
                    psAtencionDetalleDao.actualizar(psAtencionDetalle);
                }

                if (!String.IsNullOrEmpty(bean.IdTratamiento05_1)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                   new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico05, bean.IdTratamiento05_1).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();

                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;

                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                        psAtencionTratamiento.PosicionTratamiento = 1;


                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_1;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }


                if (!String.IsNullOrEmpty(bean.IdTratamiento05_2)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico05, bean.IdTratamiento05_2).obtenerArreglo());
                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();

                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;

                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                        psAtencionTratamiento.PosicionTratamiento = 2;


                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_2;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }


                if (!String.IsNullOrEmpty(bean.IdTratamiento05_3)) {
                    psAtencionTratamiento = psAtencionTratamientoDao.obtenerPorId(
                                    new PsAtencionTratamientoPk(bean.IdAtencion, bean.IdDiagnostico05, bean.IdTratamiento05_3).obtenerArreglo());

                    if (psAtencionTratamiento == null) {
                        psAtencionTratamiento = new PsAtencionTratamiento();

                        psAtencionTratamiento.IdAtencion = bean.IdAtencion;

                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                        psAtencionTratamiento.PosicionTratamiento = 3;


                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.registrar(psAtencionTratamiento);
                    }
                    else {
                        psAtencionTratamiento.IdTratamiento = bean.IdTratamiento05_3;
                        psAtencionTratamiento.IdDiagnostico = bean.IdDiagnostico05;
                        psAtencionTratamiento.Estado = "A";
                        psAtencionTratamientoDao.actualizar(psAtencionTratamiento);
                    }
                }
            }


            return psAtencion;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion) {
            coreEliminar(new PsAtencionPk(pIdInstitucion, pIdBeneficiario, pIdAtencion));
        }

        public void coreEliminar(PsAtencionPk id) {
            PsAtencion bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsAtencion coreAnular(UsuarioActual usuarioActual, PsAtencionPk id) {
            PsAtencion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return null;
        }

        public PsAtencion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion) {
            return coreAnular(usuarioActual, new PsAtencionPk(pIdInstitucion, pIdBeneficiario, pIdAtencion));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroRas filtro) {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoAtencion> lstResultado = new List<DtoAtencion>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto)) {
                filtro.NombreCompleto = null;
            }
            if (String.IsNullOrEmpty(filtro.IdSexo)) {
                filtro.IdSexo = null;
            }
            if (String.IsNullOrEmpty(filtro.IdPrograma)) {
                filtro.IdPrograma = null;
            }
            if (String.IsNullOrEmpty(filtro.IdArea)) {
                filtro.IdArea = null;
            }
            if (String.IsNullOrEmpty(filtro.IdTipoAtencion)) {
                filtro.IdTipoAtencion = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_inicio", ConstanteUtil.TipoDato.DateTime, filtro.FechaAtencionInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_fin", ConstanteUtil.TipoDato.DateTime, filtro.FechaAtencionFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipoRas", ConstanteUtil.TipoDato.String, filtro.IdTipoAtencion));

            _reader = this.listarPorQuery("psantencion.listarPaginacion", parametros);

            while (_reader.Read()) {
                DtoAtencion bean = new DtoAtencion();

                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.NombreCompleto = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.FechaAtencion = _reader.GetDateTime(3);
                if (!_reader.IsDBNull(4))
                    bean.Documento = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.idPeriodo = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Residencia = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdArea = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.IdAtencion = _reader.GetInt32(8);

                if (!_reader.IsDBNull(9))
                    bean.IdDiagnostico01 = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.NombreDiagnostico01 = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.IdTratamiento01_1 = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdTratamiento01_2 = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.IdTratamiento01_3 = _reader.GetString(13);

                if (!_reader.IsDBNull(14))
                    bean.IdDiagnostico02 = _reader.GetInt32(14);
                if (!_reader.IsDBNull(15))
                    bean.NombreDiagnostico02 = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.IdTratamiento02_1 = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.IdTratamiento02_2 = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.IdTratamiento02_3 = _reader.GetString(18);

                if (!_reader.IsDBNull(19))
                    bean.IdDiagnostico03 = _reader.GetInt32(19);
                if (!_reader.IsDBNull(20))
                    bean.NombreDiagnostico03 = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.IdTratamiento03_1 = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.IdTratamiento03_2 = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.IdTratamiento03_3 = _reader.GetString(23);

                if (!_reader.IsDBNull(24))
                    bean.IdDiagnostico04 = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.NombreDiagnostico04 = _reader.GetString(25);
                if (!_reader.IsDBNull(26))
                    bean.IdTratamiento04_1 = _reader.GetString(26);
                if (!_reader.IsDBNull(27))
                    bean.IdTratamiento04_2 = _reader.GetString(27);
                if (!_reader.IsDBNull(28))
                    bean.IdTratamiento04_3 = _reader.GetString(28);

                if (!_reader.IsDBNull(29))
                    bean.IdDiagnostico05 = _reader.GetInt32(29);
                if (!_reader.IsDBNull(30))
                    bean.NombreDiagnostico05 = _reader.GetString(30);
                if (!_reader.IsDBNull(31))
                    bean.IdTratamiento05_1 = _reader.GetString(31);
                if (!_reader.IsDBNull(32))
                    bean.IdTratamiento05_2 = _reader.GetString(32);
                if (!_reader.IsDBNull(33))
                    bean.IdTratamiento05_3 = _reader.GetString(33);

                if (!_reader.IsDBNull(34))
                    bean.comentario = _reader.GetString(34);

                contador ++;

                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroRas filtro) {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoAtencion> lstResultado = new List<DtoAtencion>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto)) {
                filtro.NombreCompleto = null;
            }
            if (String.IsNullOrEmpty(filtro.IdSexo)) {
                filtro.IdSexo = null;
            }
            if (String.IsNullOrEmpty(filtro.IdPrograma)) {
                filtro.IdPrograma = null;
            }
            if (String.IsNullOrEmpty(filtro.IdArea)) {
                filtro.IdArea = null;
            }
            if (String.IsNullOrEmpty(filtro.IdTipoAtencion)) {
                filtro.IdTipoAtencion = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_inicio", ConstanteUtil.TipoDato.DateTime, filtro.FechaAtencionInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_fin", ConstanteUtil.TipoDato.DateTime, filtro.FechaAtencionFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipoRas", ConstanteUtil.TipoDato.String, filtro.IdTipoAtencion));
            parametros.Add(new ParametroPersistenciaGenerico("p_idDiagnostico", ConstanteUtil.TipoDato.Integer, filtro.IdDiagnostico));

            parametros.Add(new ParametroPersistenciaGenerico("p_orden", ConstanteUtil.TipoDato.Integer, filtro.orden));
            parametros.Add(new ParametroPersistenciaGenerico("p_sentido", ConstanteUtil.TipoDato.Integer, filtro.sentido));

            _reader = this.listarPorQuery("psantencion.listarPaginacionConsulta", parametros);

            while (_reader.Read()) {
                DtoAtencion bean = new DtoAtencion();

                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.NombreCompleto = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.FechaAtencion = _reader.GetDateTime(3);
                if (!_reader.IsDBNull(4))
                    bean.Documento = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.idPeriodo = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Residencia = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdArea = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.IdAtencion = _reader.GetInt32(8);

                if (!_reader.IsDBNull(9))
                    bean.IdDiagnostico01 = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.NombreDiagnostico01 = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.IdTratamiento01_1 = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdTratamiento01_2 = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.IdTratamiento01_3 = _reader.GetString(13);

                if (!_reader.IsDBNull(14))
                    bean.IdDiagnostico02 = _reader.GetInt32(14);
                if (!_reader.IsDBNull(15))
                    bean.NombreDiagnostico02 = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.IdTratamiento02_1 = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.IdTratamiento02_2 = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.IdTratamiento02_3 = _reader.GetString(18);

                if (!_reader.IsDBNull(19))
                    bean.IdDiagnostico03 = _reader.GetInt32(19);
                if (!_reader.IsDBNull(20))
                    bean.NombreDiagnostico03 = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.IdTratamiento03_1 = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.IdTratamiento03_2 = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.IdTratamiento03_3 = _reader.GetString(23);

                if (!_reader.IsDBNull(24))
                    bean.IdDiagnostico04 = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.NombreDiagnostico04 = _reader.GetString(25);
                if (!_reader.IsDBNull(26))
                    bean.IdTratamiento04_1 = _reader.GetString(26);
                if (!_reader.IsDBNull(27))
                    bean.IdTratamiento04_2 = _reader.GetString(27);
                if (!_reader.IsDBNull(28))
                    bean.IdTratamiento04_3 = _reader.GetString(28);

                if (!_reader.IsDBNull(29))
                    bean.IdDiagnostico04 = _reader.GetInt32(29);
                if (!_reader.IsDBNull(30))
                    bean.NombreDiagnostico04 = _reader.GetString(30);
                if (!_reader.IsDBNull(31))
                    bean.IdTratamiento04_1 = _reader.GetString(31);
                if (!_reader.IsDBNull(32))
                    bean.IdTratamiento04_2 = _reader.GetString(32);
                if (!_reader.IsDBNull(33))
                    bean.IdTratamiento04_3 = _reader.GetString(33);

                if (!_reader.IsDBNull(34))
                    bean.IdTratamiento01_1_nombre = _reader.GetString(34);
                if (!_reader.IsDBNull(35))
                    bean.IdTratamiento01_2_nombre = _reader.GetString(35);
                if (!_reader.IsDBNull(36))
                    bean.IdTratamiento01_3_nombre = _reader.GetString(36);

                if (!_reader.IsDBNull(37))
                    bean.IdTratamiento02_1_nombre = _reader.GetString(37);
                if (!_reader.IsDBNull(38))
                    bean.IdTratamiento02_2_nombre = _reader.GetString(38);
                if (!_reader.IsDBNull(39))
                    bean.IdTratamiento02_3_nombre = _reader.GetString(39);

                if (!_reader.IsDBNull(40))
                    bean.IdTratamiento03_1_nombre = _reader.GetString(40);
                if (!_reader.IsDBNull(41))
                    bean.IdTratamiento03_2_nombre = _reader.GetString(41);
                if (!_reader.IsDBNull(42))
                    bean.IdTratamiento03_3_nombre = _reader.GetString(42);

                if (!_reader.IsDBNull(43))
                    bean.IdTratamiento04_1_nombre = _reader.GetString(43);
                if (!_reader.IsDBNull(44))
                    bean.IdTratamiento04_2_nombre = _reader.GetString(44);
                if (!_reader.IsDBNull(45))
                    bean.IdTratamiento04_3_nombre = _reader.GetString(45);

                if (!_reader.IsDBNull(46))
                    bean.IdTratamiento05_1_nombre = _reader.GetString(46);
                if (!_reader.IsDBNull(47))
                    bean.IdTratamiento05_2_nombre = _reader.GetString(47);
                if (!_reader.IsDBNull(48))
                    bean.IdTratamiento05_3_nombre = _reader.GetString(48);
                if (!_reader.IsDBNull(49))
                    contador = _reader.GetInt32(49);
                if (!_reader.IsDBNull(50))
                    bean.comentario = _reader.GetString(50);

                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }
    }
}

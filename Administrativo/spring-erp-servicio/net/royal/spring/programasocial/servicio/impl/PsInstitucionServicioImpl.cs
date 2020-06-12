using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Text.RegularExpressions;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsInstitucionServicioImpl : GenericoServicioImpl, PsInstitucionServicio {

        private IServiceProvider servicioProveedor;
        private PsInstitucionDao psInstitucionDao;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;

        public PsInstitucionServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psInstitucionDao = servicioProveedor.GetService<PsInstitucionDao>();
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
        }

        public PsInstitucion coreInsertar(UsuarioActual usuarioActual, PsInstitucion bean) {
            string cadena, codigo;
            codigo = psInstitucionDao.obtenercodigo(bean.IdInstitucion);
            cadena = psInstitucionDao.obtenercadena(bean.Nombre);

            Regex valor = new Regex(@"^[a-zA-ZñÑ]{5}$");

            if (!valor.IsMatch(bean.IdInstitucion)) {
                throw new UException("El código debe contener 5 letras sin tildes");
            }
            if (codigo != null) {
                throw new UException("El código ingresado ya se encuentra registrado");
            }
            if (cadena != null) {
                throw new UException("El nombre ingresado ya se encuentra registrado");
            }
            else {
                if (UString.estaVacio(bean.Estado))
                    bean.Estado = ConstanteEstadoGenerico.ACTIVO;
                bean.IdInstitucion = bean.IdInstitucion.ToUpper();
                return psInstitucionDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }

        }

        public PsInstitucion coreActualizar(UsuarioActual usuarioActual, PsInstitucion bean) {

            return psInstitucionDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PsInstitucion coreAnular(UsuarioActual usuarioActual, PsInstitucionPk id) {
            return psInstitucionDao.coreAnular(usuarioActual, id);
        }

        public PsInstitucion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion) {
            return psInstitucionDao.coreAnular(usuarioActual, pIdInstitucion);
        }

        public void coreEliminar(PsInstitucionPk id) {
            psInstitucionDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion) {
            psInstitucionDao.coreEliminar(pIdInstitucion);
        }


        public PsInstitucion obtenerPorId(PsInstitucionPk id) {
            return psInstitucionDao.obtenerPorId(id);
        }

        public PsInstitucion obtenerPorId(string codigo) {
            return psInstitucionDao.obtenerPorId(codigo);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroInstitucion filtro) {
            return psInstitucionDao.listarPaginacion(paginacion, filtro);
        }

        public void eliminar(string codigo) {

            PsInstitucion capa = new PsInstitucion() { IdInstitucion = codigo };

            psInstitucionDao.eliminar(capa);

            psInstitucionDao.eliminarAreas(codigo);

        }

        public List<PsInstitucion> listarTodos() {
            return psInstitucionDao.listarTodos();
        }

        public string flgSeleccionarInstitucion(UsuarioActual usuarioActual) {
            return psInstitucionDao.flgSeleccionarInstitucion(usuarioActual);
        }

        public List<DtoTabla> listarPeriodos(string Institucion, String IdPrograma, string IdComponente, String IdUsuario, Nullable<Int32> IdBeneficiario) {
            return psInstitucionDao.listarPeriodos(Institucion, IdPrograma, IdComponente, IdUsuario, IdBeneficiario);
        }

        public List<PsInstitucion> listarTodosActivas() {
            return psInstitucionDao.listarTodosActivas();
        }
    }
}


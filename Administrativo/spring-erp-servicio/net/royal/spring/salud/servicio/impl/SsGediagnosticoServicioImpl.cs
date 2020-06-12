using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.salud.dominio;
using net.royal.spring.salud.dominio.dto;
using net.royal.spring.salud.dao;

namespace net.royal.spring.salud.servicio.impl {

    public class SsGediagnosticoServicioImpl : GenericoServicioImpl, SsGediagnosticoServicio {

        private IServiceProvider servicioProveedor;
        private SsGediagnosticoDao SsGediagnosticoDao;

        public SsGediagnosticoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            SsGediagnosticoDao = servicioProveedor.GetService<SsGediagnosticoDao>();
        }

        public SsGediagnostico coreInsertar(UsuarioActual usuarioActual, SsGediagnostico bean) {
            string cadena;
            SsGediagnostico objeto = new SsGediagnostico();

            objeto = SsGediagnosticoDao.obtenerNombrePorCodigo(bean.CodigoDiagnostic);
            cadena = SsGediagnosticoDao.obtenercadena(bean.Nombre);


            //Regex valor = new Regex(@"^[a-zA-ZñÑ]{4}$");

            /*if (!valor.IsMatch(bean.IdPrograma))
            {
                throw new UException("El código debe contener 4 letras sin tildes");
            }*/
            if (objeto != null) {
                throw new UException("El código ingresado ya se encuentra registrado");
            }
            if (cadena != null) {
                throw new UException("El nombre ingresado ya se encuentra registrado");
            }
            else {
                if (UInteger.esCeroOrNulo(bean.Estado))
                    bean.Estado = 2;
                return SsGediagnosticoDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }

        }

        public SsGediagnostico coreActualizar(UsuarioActual usuarioActual, SsGediagnostico bean) {
          

            return SsGediagnosticoDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }





        public void coreEliminar(SsGediagnosticoPk id) {
            SsGediagnosticoDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> codigo) {
            SsGediagnosticoDao.coreEliminar(codigo);
        }


        public SsGediagnostico obtenerPorId(SsGediagnosticoPk id) {
            return SsGediagnosticoDao.obtenerPorId(id);
        }

        public SsGediagnostico obtenerPorId(int codigo) {
            return SsGediagnosticoDao.obtenerPorId(codigo);
        }


        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroDiagnostico filtro) {
            return SsGediagnosticoDao.listarPaginacion(filtro.paginacion, filtro);
        }



        public void eliminar(Int32? codigo) {

            SsGediagnostico capa = new SsGediagnostico() { IdDiagnostico = codigo };

            SsGediagnosticoDao.eliminar(capa);

        }

        public SsGediagnostico obtenerNombrePorCodigo(string codigo) {
            return SsGediagnosticoDao.obtenerNombrePorCodigo(codigo);
        }

        public List<SsGediagnostico> listarActivosFlgCronicos() {
            return SsGediagnosticoDao.listarActivosFlgCronicos();
        }

        public SsGediagnostico coreAnular(UsuarioActual usuarioActual, SsGediagnosticoPk id)
        {
            return SsGediagnosticoDao.coreAnular(usuarioActual, id);
        }

        public SsGediagnostico coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdDiagnostico)
        {
            return SsGediagnosticoDao.coreAnular(usuarioActual, pIdDiagnostico);
        }
    }
}

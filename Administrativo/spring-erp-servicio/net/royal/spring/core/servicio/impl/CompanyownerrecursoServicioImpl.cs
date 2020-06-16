using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.framework.core;
using System.IO;
using net.royal.spring.sistema.dao;
using net.royal.spring.core.constante;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.core.servicio.impl
{

    public class CompanyownerrecursoServicioImpl : GenericoServicioImpl, CompanyownerrecursoServicio
    {

        private IServiceProvider servicioProveedor;
        private CompanyownerrecursoDao companyownerrecursoDao;
        private ParametrosmastDao parametrosmastDao;
        private CompanyownerDao companyownerDao;

        public CompanyownerrecursoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;

            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
            companyownerrecursoDao = servicioProveedor.GetService<CompanyownerrecursoDao>();
            companyownerDao = servicioProveedor.GetService<CompanyownerDao>();
        }

        public Companyownerrecurso actualizar(UsuarioActual usuarioActual, Companyownerrecurso bean)
        {
            if (!String.IsNullOrEmpty(bean.AuxString))
            {
                var inicio = bean.AuxString.IndexOf(',') + 1;
                var fin = bean.AuxString.Length;
                var archivo = bean.AuxString.Substring(inicio, fin - inicio);

                bean.Recurso = System.Convert.FromBase64String(archivo);
            }
            else
            {
                bean.Recurso = null;
            }
            companyownerrecursoDao.actualizar(bean);
            DtoRecursoParametro recurso = new DtoRecursoParametro(bean.Tiporecurso, bean.Companyowner, bean.Periodo);
            recurso.Archivo = bean.Recurso;
           // crearRecursoEnServidor(recurso);
            return bean;
        }

        public Companyownerrecurso registrar(UsuarioActual usuarioActual, Companyownerrecurso bean)
        {
            if (!String.IsNullOrEmpty(bean.AuxString))
            {
                var inicio = bean.AuxString.IndexOf(',') + 1;
                var fin = bean.AuxString.Length;
                var archivo = bean.AuxString.Substring(inicio, fin - inicio);

                bean.Recurso = System.Convert.FromBase64String(archivo);
            }
            companyownerrecursoDao.registrar(bean);
            DtoRecursoParametro recurso = new DtoRecursoParametro(bean.Tiporecurso, bean.Companyowner, bean.Periodo);

            recurso.Archivo = bean.Recurso;
            //crearRecursoEnServidor(recurso);
            return bean;
        }

        public DtoRecursoConfiguracionResultado obtenerRecursoEnServidor(DtoRecursoParametro recurso)
        {
            DtoRecursoConfiguracionResultado retorno = new DtoRecursoConfiguracionResultado();
            retorno.FlgOk = false;
            String nombreArhivoRecurso = null;
            String extension = "png";

            if (UString.estaVacio(recurso.Periodo))
                recurso.Periodo = "999999";
            if (UString.estaVacio(recurso.CompaniaSocio))
                recurso.CompaniaSocio = "999999";
            if (UString.estaVacio(recurso.TipoRecurso))
                throw new Exception("No especifico el tipo de recurso");

            String rutaReportes = parametrosmastDao.obtenerValorExplicacion(ConstanteCompanyownerRecurso.PARAMETRO_RUTA_RECURSOS_WEB, ConstanteCompanyownerRecurso.APLICACION_CODIGO);
            if (UString.estaVacio(rutaReportes))
                throw new Exception("No existe la configuracion en donde se encuentren los recursos fisicos");

            if (!Directory.Exists(rutaReportes))
                throw new Exception("No existe una ruta en donde se encuentren los recursos fisicos");

            /// si no existe la carpeta se crea
            if (!Directory.Exists(rutaReportes + recurso.TipoRecurso))
            {
                Directory.CreateDirectory(rutaReportes + recurso.TipoRecurso);
            }
            rutaReportes = rutaReportes + recurso.TipoRecurso + Path.DirectorySeparatorChar;

            /// armar nombre del recurso
            nombreArhivoRecurso = recurso.getNombreCompleto() + "." + extension;

            if (File.Exists(rutaReportes + nombreArhivoRecurso))
            {
                byte[] archivo = File.ReadAllBytes(rutaReportes + nombreArhivoRecurso);
                retorno.RutaCompleta = rutaReportes + nombreArhivoRecurso;
                retorno.FlgOk = true;
                retorno.Recurso = archivo;
                return retorno;
            }

            return retorno;
        }

        private DtoRecursoConfiguracionResultado crearRecursoEnServidor(DtoRecursoParametro recurso)
        {
            DtoRecursoConfiguracionResultado config = new DtoRecursoConfiguracionResultado();
            byte[] archivo = null;
            String rutaReportes = parametrosmastDao.obtenerValorExplicacion(ConstanteCompanyownerRecurso.PARAMETRO_RUTA_RECURSOS_WEB, ConstanteCompanyownerRecurso.APLICACION_CODIGO);
            String nombreReporte = null;
            String extension = "png";

            if (recurso.Archivo == null)
                throw new UException("Recurso no tiene ningun Archivo");
            if (UString.estaVacio(rutaReportes))
                throw new Exception("No existe una ruta en donde se encuentren los recursos fisicos");

            if (UString.estaVacio(recurso.Periodo))
                recurso.Periodo = "999999";
            if (UString.estaVacio(recurso.CompaniaSocio))
                recurso.CompaniaSocio = "999999";
            if (UString.estaVacio(recurso.TipoRecurso))
                throw new Exception("No especifico el tipo de recurso");

            nombreReporte = recurso.getNombreCompleto() + "." + extension;

            if (File.Exists(rutaReportes + nombreReporte))
            {
                File.Delete(rutaReportes + nombreReporte);
            }

            File.WriteAllBytes(rutaReportes + nombreReporte, recurso.Archivo);

            config.RutaCompleta = rutaReportes + nombreReporte;
            config.FlgOk = true;
            config.Recurso = archivo;

            return config;
        }

        public IList<Companyownerrecurso> listarPorRecurso(string tipoRecurso)
        {
            var lista = companyownerrecursoDao.listarPorRecurso(tipoRecurso);
            foreach (Companyownerrecurso recurso in lista)
            {
                recurso.AuxCompanyowner = companyownerDao.obtenerNombre(recurso.Companyowner);
                if (recurso.Recurso != null)
                {
                    recurso.AuxString = Convert.ToBase64String(recurso.Recurso);
                }

            }
            return lista;
        }

        public void eliminarPorTipoRecurso(string tipoRecurso)
        {
            companyownerrecursoDao.eliminarPorTipoRecurso(tipoRecurso);
        }

        public Companyownerrecurso obtenerPorId(CompanyownerrecursoPk pk)
        {
            Companyownerrecurso obj= companyownerrecursoDao.obtenerPorId(pk);

            if (obj.Recurso != null)
            {
                obj.AuxString = Convert.ToBase64String(obj.Recurso);
            }

            return obj;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.sistema.dao;
using net.royal.spring.core.constante;
using System.IO;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio.impl
{

    public class EmpleadomastServicioImpl : GenericoServicioImpl, EmpleadomastServicio
    {

        private IServiceProvider servicioProveedor;
        private EmpleadomastDao empleadomastDao;
        private ParametrosmastDao parametrosmastDao;
        private PersonamastDao personamastDao;

        public EmpleadomastServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            empleadomastDao = servicioProveedor.GetService<EmpleadomastDao>();
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
            personamastDao = servicioProveedor.GetService<PersonamastDao>();
        }

        public Empleadomast obtenerPorId(EmpleadomastPk empleadomastPk)
        {
            return empleadomastDao.obtenerPorId(empleadomastPk);
        }

        public Empleadomast registrar(Empleadomast empleadomast)
        {
            empleadomast = empleadomastDao.registrar(empleadomast);
            return empleadomast;
        }

        public Empleadomast actualizar(Empleadomast empleadomast)
        {
            empleadomastDao.actualizar(empleadomast);
            return empleadomast;
        }

        public void eliminar(EmpleadomastPk empleadomastPk)
        {
            Empleadomast empleadomast = empleadomastDao.obtenerPorId(empleadomastPk);
            empleadomastDao.eliminar(empleadomast);
        }
        public DtoEmpleadoBasico obtenerInformacionPorIdPersona(UsuarioActual usuario)
        {
            return empleadomastDao.obtenerInformacionPorIdPersona(usuario);
        }
        public DtoEmpleadoBasico obtenerInformacionPorIdPersonaA(UsuarioActual usuario)
        {
            return empleadomastDao.obtenerInformacionPorIdPersonaA(usuario);
        }
        public DtoEmpleadoBasico obtenerInformacionPorIdPersona(int? idPersona)
        {
            return empleadomastDao.obtenerInformacionPorIdPersona(idPersona);
        }

        public Empleadomast obtenerPorCodigoUsuario(string codigousuario)
        {
            return empleadomastDao.obtenerPorCodigoUsuario(codigousuario);
        }
        public string confirmarCargo(int? idEmpleado, int? codigoCargo)
        {
            Empleadomast emp = empleadomastDao.obtenerPorIdEmpleado(idEmpleado);
            if (emp == null)
                return null;

            if (UInteger.obtenerValorEnteroSinNulo(emp.Codigocargo).Equals(codigoCargo))
                return "S";

            return "N";
        }

        public ParametroPaginacionGenerico listarAniversarios(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro)
        {
            if (filtro.FechaInicio.HasValue)
                filtro.FechaInicio = filtro.FechaInicio.Value.AddYears(ConstanteEmpleado.ANIO_BISIESTO - filtro.FechaInicio.Value.Year);
            if (filtro.FechaFin.HasValue)
                filtro.FechaFin = filtro.FechaFin.Value.AddYears(ConstanteEmpleado.ANIO_BISIESTO - filtro.FechaFin.Value.Year);
            return empleadomastDao.listarAniversariosFiltroDividido(paginacion, filtro);
        }
        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            return empleadomastDao.listarBusqueda(paginacion, filtro);
        }
        public ParametroPaginacionGenerico listarCumpleanios(ParametroPaginacionGenerico paginacion, FiltroPaginacionCumpleanio filtro)
        {
            if (filtro.FechaInicio.HasValue)
                filtro.FechaInicio = filtro.FechaInicio.Value.AddYears(ConstanteEmpleado.ANIO_BISIESTO - filtro.FechaInicio.Value.Year);
            if (filtro.FechaFin.HasValue)
                filtro.FechaFin = filtro.FechaFin.Value.AddYears(ConstanteEmpleado.ANIO_BISIESTO - filtro.FechaFin.Value.Year);

            return empleadomastDao.listarCumpleanios(paginacion, filtro);
        }
        public ParametroPaginacionGenerico listarAniversariosConFoto(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro)
        {
            paginacion = empleadomastDao.listarAniversarios(paginacion, filtro);
            return paginacion;
        }
        //public DtoEmpleadoBasico obtenerInformacionPorIdPersona(UsuarioActual usuario)
        //{
        //    return empleadomastDao.obtenerInformacionPorIdPersona(usuario);
        //}

        public ParametroPaginacionGenerico listarCumpleaniosConFoto(ParametroPaginacionGenerico paginacion, FiltroPaginacionCumpleanio filtro)
        {
            paginacion = this.listarCumpleanios(paginacion, filtro);
            return paginacion;
        }

        private DominioArchivo fotoActualizarDisco(DtoConfiguracionRepositorioFoto conf, Int32? idEmpleado, String numeroDocumento, DominioArchivo archivoFoto)
        {
            String nombreRutaCompleta = null;

            if (!Directory.Exists(conf.RutaFoto))
            {
                archivoFoto.CodigoMensaje = DominioArchivo.MSG_CARPETA_NO_ENCONTRADA;
                return archivoFoto;
            }

            if (conf.FlgIdComoCodigoFoto.Contains("S"))
            {
                String rutaFoto = conf.RutaFoto;
                if (!UString.estaVacio(conf.RutaFoto))
                {
                    rutaFoto = rutaFoto.Trim();
                }
                nombreRutaCompleta = rutaFoto + Path.DirectorySeparatorChar + idEmpleado.ToString() + "."
                                    + conf.ExtensionFoto;
            }
            else
            {
                nombreRutaCompleta = conf.RutaFoto + Path.DirectorySeparatorChar
                        + UString.obtenerValorCadenaSinNulo(numeroDocumento).Trim() + "." + conf.ExtensionFoto;
            }

            if (File.Exists(nombreRutaCompleta))
            {
                //File.Delete(nombreRutaCompleta);
                archivoFoto.CodigoMensaje = DominioArchivo.MSG_ARCHIVO_ENCONTRADO;
            }

            File.WriteAllBytes(nombreRutaCompleta, archivoFoto.Archivo);
            archivoFoto.CodigoMensaje = DominioArchivo.MSG_ARCHIVO_ACTUALIZADO;

            return archivoFoto;
        }

        private void fotoActualizar(DtoConfiguracionRepositorioFoto conf, Int32? idEmpleado, String numeroDocumento, DominioArchivo archivoFoto)
        {
            DominioArchivo bean = null;
            if (conf.ModoGuardar.Equals(ConstanteEmpleado.FOTO_MODO_GUARDAR_DISCO))
                bean = fotoActualizarDisco(conf, idEmpleado, numeroDocumento, archivoFoto);
            else
                bean = new DominioArchivo();
            return;
        }

        public void fotoActualizar(int? idEmpleado, string numeroDocumento, DominioArchivo archivoFoto)
        {
            DtoConfiguracionRepositorioFoto conf = obtenerConfiguracionFotoEmpleado(null);
            fotoActualizar(conf, idEmpleado, numeroDocumento, archivoFoto);
        }

        public DominioArchivo obtenerFotoActual(int? idEmpleado)
        {
            DtoConfiguracionRepositorioFoto conf = obtenerConfiguracionFotoEmpleado(null);
            DominioArchivo beanFoto = new DominioArchivo();

            if (UString.estaVacio(conf.MesanjeConfiguracion))
            {
                Personamast persona = personamastDao.obtenerPorId(new PersonamastPk(idEmpleado.Value));
                if (persona == null)
                {
                    beanFoto.CodigoMensaje = DominioArchivo.MSG_CODIGO_BUSQUEDA_VACIO;
                    return beanFoto;
                }
                beanFoto = obtenerFotoActual(conf, idEmpleado.Value,
                        UString.obtenerValorCadenaSinNulo(persona.Documento).Trim());
            }
            else
            {
                beanFoto.CodigoMensaje = conf.MesanjeConfiguracion;
            }

            return beanFoto;
        }
        private DtoConfiguracionRepositorioFoto obtenerConfiguracionFotoEmpleado(String fotoModoRetorno)
        {
            DtoConfiguracionRepositorioFoto conf = new DtoConfiguracionRepositorioFoto();
            String modoGuardar = null;
            String rutaFoto = null;
            String extensionFoto = null;
            String flgIdComoCodigoFoto = null;

            modoGuardar = parametrosmastDao.obtenerValorCadena(ConstanteEmpleado.PARAMETRO_FOTO_MODO_GUARDAR,
                    ConstanteEmpleado.APLICACION_CODIGO);
            if (UString.estaVacio(modoGuardar))
                modoGuardar = ConstanteEmpleado.FOTO_MODO_GUARDAR_DISCO;

            rutaFoto = parametrosmastDao.obtenerValorExplicacion(ConstanteEmpleado.PARAMETRO_FOTO_EMPLEADO_RUTA_APROBADA,
                    ConstanteEmpleado.APLICACION_CODIGO);
            if (UString.estaVacio(rutaFoto))
                rutaFoto = "";

            extensionFoto = parametrosmastDao.obtenerValorCadena(ConstanteEmpleado.PARAMETRO_FOTO_EMPLEADO_EXTENSION,
                    ConstanteEmpleado.APLICACION_CODIGO);
            if (UString.estaVacio(extensionFoto))
                extensionFoto = "jpg";

            flgIdComoCodigoFoto = parametrosmastDao.obtenerValorCadena(
                    ConstanteEmpleado.PARAMETRO_FOTO_EMPLEADO_NOMBRE_ARCHIVO, ConstanteEmpleado.APLICACION_CODIGO);
            if (UString.estaVacio(flgIdComoCodigoFoto))
                flgIdComoCodigoFoto = "S";

            if (UString.estaVacio(fotoModoRetorno))
                fotoModoRetorno = ConstanteEmpleado.FOTO_RETORNO_DTO_STRING;

            conf.MesanjeConfiguracion = "";
            if (modoGuardar.Equals(ConstanteEmpleado.FOTO_MODO_GUARDAR_DISCO))
            {
                if (!Directory.Exists(rutaFoto))
                {
                    rutaFoto = parametrosmastDao.obtenerValorDescripcion(
                        ConstanteEmpleado.PARAMETRO_FOTO_EMPLEADO_RUTA_APROBADA, ConstanteEmpleado.APLICACION_CODIGO);
                    if (!Directory.Exists(rutaFoto))
                    {
                        conf.MesanjeConfiguracion = "CARPETA '" + rutaFoto + "' NO ENCONTRADA ";
                    }
                }
            }

            conf.ExtensionFoto = extensionFoto;
            conf.FlgIdComoCodigoFoto = flgIdComoCodigoFoto;
            conf.FotoModoRetorno = fotoModoRetorno;
            conf.ModoGuardar = modoGuardar;
            conf.RutaFoto = rutaFoto;

            return conf;
        }

        private DominioArchivo obtenerFotoActual(DtoConfiguracionRepositorioFoto conf, int idEmpleado,
            String numeroDocumento)
        {
            DominioArchivo bean = null;

            if (conf.ModoGuardar.Equals(ConstanteEmpleado.FOTO_MODO_GUARDAR_DISCO))
                bean = obtenerFotoActualDisco(conf, idEmpleado, numeroDocumento);
            else
                bean = new DominioArchivo();
            return bean;
        }

        public DtoEmpleado obtenerCompaniaActiva(int empleado)
        {
            return empleadomastDao.obtenerCompaniaActiva(empleado);
        }

        private DominioArchivo obtenerFotoActualDisco(DtoConfiguracionRepositorioFoto conf, int idEmpleado, String numeroDocumento)
        {
            DominioArchivo beanFoto = new DominioArchivo();
            String nombreRutaCompleta = null;
            String nombreFoto = null;

            if (!Directory.Exists(conf.RutaFoto))
            {
                beanFoto.CodigoMensaje = DominioArchivo.MSG_CARPETA_NO_ENCONTRADA;
                return beanFoto;
            }

            if (conf.FlgIdComoCodigoFoto.Contains("S"))
            {
                nombreFoto = idEmpleado.ToString() + "." + conf.ExtensionFoto;
                nombreRutaCompleta = conf.RutaFoto + Path.DirectorySeparatorChar + nombreFoto;
            }
            else
            {
                nombreFoto = UString.obtenerValorCadenaSinNulo(numeroDocumento).Trim() + "." + conf.ExtensionFoto;
                nombreRutaCompleta = conf.RutaFoto + Path.DirectorySeparatorChar
                        + nombreFoto;
            }

            if (!File.Exists(nombreRutaCompleta))
            {
                beanFoto.CodigoMensaje = DominioArchivo.MSG_ARCHIVO_NO_ENCONTRADO;
                return beanFoto;
            }

            beanFoto.CodigoMensaje = DominioArchivo.MSG_ARCHIVO_ENCONTRADO;
            beanFoto.NombreArchivo = nombreFoto;
            beanFoto.ExtensionArchivo = UFile.extraerExtension(nombreFoto);

            byte[] bytesrespaldo = UFile.obtenerArregloByte(nombreRutaCompleta);

            if (conf.FotoModoRetorno.Equals(ConstanteEmpleado.FOTO_RETORNO_DTO_BYTE))
                beanFoto.Archivo = bytesrespaldo;
            else
                beanFoto.ArchivoCadena = Convert.ToBase64String(bytesrespaldo);

            return beanFoto;
        }

        public ParametroPaginacionGenerico listarBusquedaUnidaOperativa(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado)
        {
            return empleadomastDao.listarBusquedaUnidaOperativa(paginacion, filtroPaginacionEmpleado);
        }

        public ParametroPaginacionGenerico listarBusquedaPuesto(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado)
        {
            return empleadomastDao.listarBusquedaPuesto(paginacion, filtroPaginacionEmpleado);
        }

        public List<DtoTabla> listarEmpresasActivasPorUsuario(string idUsuario)
        {
            return empleadomastDao.listarEmpresasActivasPorUsuario(idUsuario);
        }

        public string obtenerHorario(int persona, string compania, DateTime? inicio)
        {
            return empleadomastDao.obtenerHorario(persona, compania, inicio);
        }
        public string esFiscalizado(int persona)
        {
            // dario
            return empleadomastDao.esFiscalizado(persona);
        }

        public bool esJefe(int? personaId)
        {
            return empleadomastDao.esJefe(personaId);
        }

        public List<DtoTablaEntero> obtenerDiasPorMes(int nroMes)
        {
            List<DtoTablaEntero> listadoDias = new List<DtoTablaEntero>();
            Int32 cantidadDias = System.DateTime.DaysInMonth(DateTime.Today.Year, nroMes);

            for (int i = 1; i <= cantidadDias; i++)
            {
                DtoTablaEntero dia = new DtoTablaEntero();
                dia.codigo = i;
                listadoDias.Add(dia);
            }

            return listadoDias;
        }
    }
}

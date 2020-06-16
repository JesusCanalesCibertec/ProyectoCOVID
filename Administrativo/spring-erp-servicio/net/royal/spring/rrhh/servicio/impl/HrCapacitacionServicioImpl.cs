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
using net.royal.spring.rrhh.constante;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Linq;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dao;
using System.IO;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio.dto;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml.Style;
using System.Drawing;

namespace net.royal.spring.rrhh.servicio.impl
{

    public class HrCapacitacionServicioImpl : GenericoServicioImpl, HrCapacitacionServicio
    {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionDao hrCapacitacionDao;
        private HrEmpleadocapacitacionDao hrEmpleadocapacitacionDao;
        private HrEmpleadocaphorarioDao hrEmpleadocaphorarioDao;
        private PersonamastDao personamastDao;
        private HrCapacitacionFoliosDao hrCapacitacionFoliosDao;
        private ParametrosmastDao parametrosmastDao;
        private HrCapacitacionEmpleadoDao hrCapacitacionEmpleadoDao;
        private HrCapacitacionBeneficiarioDao hrCapacitacionBeneficiarioDao;
        private PsInstitucionDao psInstitucionDao;
        private PsEntidadDao psEntidadDao;
        private SyCorreoServicio syCorreoServicio;
        private SyReporteServicio syReporteServicio;
        private IHostingEnvironment _hostingEnvironment;
        private HrCursodescripcionDao hrCursodescripcionDao;
        private HrCentroestudiosDao hrCentroestudiosDao;
        public HrCapacitacionServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment)
        {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionDao = servicioProveedor.GetService<HrCapacitacionDao>();
            hrEmpleadocapacitacionDao = servicioProveedor.GetService<HrEmpleadocapacitacionDao>();
            hrEmpleadocaphorarioDao = servicioProveedor.GetService<HrEmpleadocaphorarioDao>();
            personamastDao = servicioProveedor.GetService<PersonamastDao>();

            hrCapacitacionEmpleadoDao = servicioProveedor.GetService<HrCapacitacionEmpleadoDao>();
            hrCapacitacionBeneficiarioDao = servicioProveedor.GetService<HrCapacitacionBeneficiarioDao>();
            hrCapacitacionFoliosDao = servicioProveedor.GetService<HrCapacitacionFoliosDao>();
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
            psInstitucionDao = servicioProveedor.GetService<PsInstitucionDao>();
            psEntidadDao = servicioProveedor.GetService<PsEntidadDao>();
            syCorreoServicio = servicioProveedor.GetService<SyCorreoServicio>();
            syReporteServicio = servicioProveedor.GetService<SyReporteServicio>();
            hrCursodescripcionDao = servicioProveedor.GetService<HrCursodescripcionDao>();
            hrCentroestudiosDao = servicioProveedor.GetService<HrCentroestudiosDao>();
            _hostingEnvironment = hostingEnvironment;
        }

        public HrCapacitacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacion bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrCapacitacionDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public HrCapacitacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacion bean)
        {
            return hrCapacitacionDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public HrCapacitacion coreAnular(UsuarioActual usuarioActual, HrCapacitacionPk id)
        {
            return hrCapacitacionDao.coreAnular(usuarioActual, id);
        }

        public HrCapacitacion coreAnular(UsuarioActual usuarioActual, String pCompanyowner, String pCapacitacion)
        {
            return hrCapacitacionDao.coreAnular(usuarioActual, pCompanyowner, pCapacitacion);
        }

        public void coreEliminar(HrCapacitacionPk id)
        {
            hrCapacitacionDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner, String pCapacitacion)
        {
            hrCapacitacionDao.coreEliminar(pCompanyowner, pCapacitacion);
        }


        public HrCapacitacion obtenerPorId(HrCapacitacionPk id)
        {
            return hrCapacitacionDao.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacion obtenerPorId(String pCompanyowner, String pCapacitacion)
        {
            return hrCapacitacionDao.obtenerPorId(pCompanyowner, pCapacitacion);
        }

        public ParametroPaginacionGenerico solicitudListar(ParametroPaginacionGenerico paginacion, FiltroPaginacionCapacitacion filtro)
        {
            return hrCapacitacionDao.solicitudListar(paginacion, filtro);
        }


        public HrCapacitacion solicitudRegistrar(UsuarioActual usuarioActual, HrCapacitacion hrCapacitacion)
        {

            String rutaServer = parametrosmastDao.obtenerValorExplicacion(HrCapacitacionFolios.PARAMETRO_RUTA_ADJUNTO, ConstanteCapacitacion.APLICACION_CODIGO);

            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            using (var dbTran = hrCapacitacionDao.transaccionIniciar())
            {
                try
                {
                    if (hrCapacitacion.Fechadesde == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La fecha Desde es Obligatoria"));
                    }

                    if (hrCapacitacion.Fechahasta == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La fecha Hasta es Obligatoria"));
                    }

                    if (hrCapacitacion.Fechadesde != null && hrCapacitacion.Fechahasta != null)
                    {
                        int comparacion = DateTime.Compare(hrCapacitacion.Fechadesde.Value, hrCapacitacion.Fechahasta.Value);

                        if (comparacion > 0)
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La fecha de Inicio debe ser menor a la fecha fin"));
                        }
                    }
                    /*
                    if (hrCapacitacion.Numerovacantes <= 0)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La Cantidad de vacantes es obligatorio"));
                    }
                    */
                    else
                    {
                        if (hrCapacitacion.listaEmpleados.Count <= 0)
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La lista de participantes debe ser menor o igual que la cantidad de vacantes"));
                        }
                    }

                    if (lstRetorno.Count > 0)
                    {
                        throw new UException(lstRetorno);
                    }

                    hrCapacitacion.Capacitacion = hrCapacitacionDao.generarCodigo("LIMA");

                    if (UString.estaVacio(hrCapacitacion.Companyowner))
                        hrCapacitacion.Companyowner = usuarioActual.CompaniaSocioCodigo;

                    hrCapacitacion.Fechasolicitud = DateTime.Now;
                    hrCapacitacion.Ultimousuario = usuarioActual.UsuarioLogin;
                    hrCapacitacion.Ultimafechamodif = DateTime.Today;
                    hrCapacitacion.Estado = ConstanteCapacitacion.ESTADO_APROBADO;

                    hrCapacitacionDao.registrar(hrCapacitacion);

                    foreach (HrEmpleadocapacitacion emp in hrCapacitacion.listaEmpleados)
                    {
                        emp.Companyowner = hrCapacitacion.Companyowner;
                        emp.Capacitacion = hrCapacitacion.Capacitacion;
                        emp.Ultimousuario = usuarioActual.UsuarioLogin;
                        emp.Ultimafechamodif = DateTime.Now;
                        hrEmpleadocapacitacionDao.registrar(emp);
                    }

                    foreach (HrEmpleadocaphorario horario in hrCapacitacion.listaHorarios)
                    {
                        horario.Empleado = 0;
                        horario.Capacitacion = hrCapacitacion.Capacitacion;
                        horario.Companyowner = hrCapacitacion.Companyowner;
                        horario.Ultimousuario = usuarioActual.UsuarioLogin;
                        horario.Ultimafechamodif = DateTime.Now;
                        hrEmpleadocaphorarioDao.registrar(horario);
                    }

                    String tmp = hrCapacitacion.Companyowner.Trim() + "-" + hrCapacitacion.Capacitacion.ToString();
                    if (!Directory.Exists(rutaServer + tmp))
                    {
                        Directory.CreateDirectory(rutaServer + tmp);
                    }
                    rutaServer = rutaServer + tmp + Path.DirectorySeparatorChar;


                    foreach (HrCapacitacionFolios folio in hrCapacitacion.listaFolios)
                    {
                        folio.Capacitacion = hrCapacitacion.Capacitacion;
                        folio.Companyowner = hrCapacitacion.Companyowner;
                        folio.Ultimousuario = usuarioActual.UsuarioLogin;
                        folio.Ultimafechamodif = DateTime.Now;
                        hrCapacitacionFoliosDao.registrar(folio);
                        //guardar el adjunto en la carpeta compartida
                        if ((UString.estaVacio(folio.Archivo) || UString.estaVacio(folio.auxData)))
                        {
                            continue;
                        }
                        String rutaDocumento = Path.GetFileName(folio.Archivo);
                        Int32 inicio = rutaDocumento.LastIndexOf(Path.DirectorySeparatorChar);
                        String nombreDocumento = rutaDocumento.Substring(inicio + 1);
                        rutaDocumento = rutaServer + nombreDocumento;
                        if (File.Exists(rutaDocumento))
                            File.Delete(rutaDocumento);
                        //quitar data:/
                        var i = folio.auxData.IndexOf(',') + 1;
                        var fin = folio.auxData.Length;
                        var bin = folio.auxData.Substring(i, fin - i);
                        folio.auxData = bin;
                        File.WriteAllBytes(rutaDocumento, System.Convert.FromBase64String(folio.auxData));
                    }

                    /*ernesto*/
                    foreach (HrCapacitacionEmpleado beanPsEmp in hrCapacitacion.listaPsEmpleados)
                    {
                        beanPsEmp.Companyowner = hrCapacitacion.Companyowner;
                        beanPsEmp.Capacitacion = hrCapacitacion.Capacitacion;
                        beanPsEmp.Ultimousuario = usuarioActual.UsuarioLogin;
                        beanPsEmp.Ultimafechamodif = DateTime.Now;
                        hrCapacitacionEmpleadoDao.registrar(beanPsEmp);
                    }

                    foreach (HrCapacitacionBeneficiario beanPsBen in hrCapacitacion.listaPsBeneficiarios)
                    {
                        beanPsBen.Companyowner = hrCapacitacion.Companyowner;
                        beanPsBen.Capacitacion = hrCapacitacion.Capacitacion;
                        beanPsBen.Ultimousuario = usuarioActual.UsuarioLogin;
                        beanPsBen.Ultimafechamodif = DateTime.Now;
                        hrCapacitacionBeneficiarioDao.registrar(beanPsBen);
                    }
                    /*ernesto*/

                    hrCapacitacionDao.transaccionFinalizar();
                    enviarCorreo(hrCapacitacion);
                    return hrCapacitacion;
                }
                catch (Exception ex)
                {
                    hrCapacitacionDao.transaccionCancelar();
                    throw ex;
                }
            }
        }
        public void enviarCorreo(HrCapacitacion hrCapacitacion)
        {
            //envio de correo


            EmailConfiguracion configCorreo = syCorreoServicio.obtenerConfiguracion();

            List<ParametroPersistenciaGenerico> lstMetadata = new List<ParametroPersistenciaGenerico>();
            List<DtoCorreo> listaCorreos = new List<DtoCorreo>();
            List<Email> listaEmail = new List<Email>();
            DtoReporteParametro reportePk = new DtoReporteParametro("HR", "CI");

            foreach (String item in hrCapacitacionDao.listarCorreos(hrCapacitacion))
            {
                listaCorreos.Add(new DtoCorreo(-1, null, item));
            }

            foreach (DtoTabla item in hrCapacitacionDao.listarParametros(hrCapacitacion))
            {
                lstMetadata.Add(new ParametroPersistenciaGenerico(item.Codigo, ConstanteUtil.TipoDato.String, item.Descripcion));
            }

            listaEmail = syReporteServicio.generarListaCorreo(reportePk, lstMetadata, listaCorreos);
            syCorreoServicio.enviarCorreInmediatoAsincrono(configCorreo, listaEmail);

            //fin envio de correos
        }

        public HrCapacitacion solicitudActualizar(UsuarioActual usuarioActual, HrCapacitacion hrCapacitacion)
        {

            String rutaServer = parametrosmastDao.obtenerValorExplicacion(HrCapacitacionFolios.PARAMETRO_RUTA_ADJUNTO, ConstanteCapacitacion.APLICACION_CODIGO);

            String tmp = hrCapacitacion.Companyowner.Trim() + "-" + hrCapacitacion.Capacitacion.ToString();
            if (!Directory.Exists(rutaServer + tmp))
            {
                Directory.CreateDirectory(rutaServer + tmp);
            }
            rutaServer = rutaServer + tmp + Path.DirectorySeparatorChar;

            DateTime ahora = DateTime.Today;
            hrCapacitacion.Ultimousuario = usuarioActual.UsuarioLogin;
            hrCapacitacion.Ultimafechamodif = ahora;

            hrCapacitacionDao.actualizar(hrCapacitacion);

            IQueryable<HrEmpleadocaphorario> query = hrEmpleadocaphorarioDao.getCriteria();
            query = query.Where(p => p.Capacitacion == hrCapacitacion.Capacitacion);
            query = query.Where(p => p.Companyowner == hrCapacitacion.Companyowner);
            List<HrEmpleadocaphorario> horarios = query.ToList();


            foreach (HrEmpleadocaphorario horario in horarios)
            {
                hrEmpleadocaphorarioDao.eliminar(horario);
            }

            IQueryable<HrEmpleadocapacitacion> queryempleadocapa = hrEmpleadocapacitacionDao.getCriteria();
            queryempleadocapa = queryempleadocapa.Where(p => p.Capacitacion == hrCapacitacion.Capacitacion.Trim());
            queryempleadocapa = queryempleadocapa.Where(p => p.Companyowner == hrCapacitacion.Companyowner.Trim());
            List<HrEmpleadocapacitacion> empleados = queryempleadocapa.ToList();


            IQueryable<HrCapacitacionBeneficiario> queryempleadocapa2 = hrCapacitacionBeneficiarioDao.getCriteria();
            queryempleadocapa2 = queryempleadocapa2.Where(p => p.Capacitacion == hrCapacitacion.Capacitacion.Trim());
            queryempleadocapa2 = queryempleadocapa2.Where(p => p.Companyowner == hrCapacitacion.Companyowner.Trim());
            List<HrCapacitacionBeneficiario> empleados2 = queryempleadocapa2.ToList();

            IQueryable<HrCapacitacionEmpleado> queryempleadocapa3 = hrCapacitacionEmpleadoDao.getCriteria();
            queryempleadocapa3 = queryempleadocapa3.Where(p => p.Capacitacion == hrCapacitacion.Capacitacion.Trim());
            queryempleadocapa3 = queryempleadocapa3.Where(p => p.Companyowner == hrCapacitacion.Companyowner.Trim());
            List<HrCapacitacionEmpleado> empleados3 = queryempleadocapa3.ToList();

            foreach (HrEmpleadocapacitacion empleado in empleados)
            {
                hrEmpleadocapacitacionDao.eliminar(empleado);
            }
            foreach (HrCapacitacionBeneficiario empleado in empleados2)
            {
                hrCapacitacionBeneficiarioDao.eliminar(empleado);

            }
            foreach (HrCapacitacionEmpleado empleado in empleados3)
            {
                hrCapacitacionEmpleadoDao.eliminar(empleado);
            }

            foreach (HrEmpleadocapacitacion emp in hrCapacitacion.listaEmpleados)
            {
                emp.Companyowner = hrCapacitacion.Companyowner;
                emp.Capacitacion = hrCapacitacion.Capacitacion;

                emp.Ultimousuario = usuarioActual.UsuarioLogin;
                emp.Ultimafechamodif = ahora;

                hrEmpleadocapacitacionDao.registrar(emp);
            }

            foreach (HrCapacitacionBeneficiario emp in hrCapacitacion.listaPsBeneficiarios)
            {
                emp.Companyowner = hrCapacitacion.Companyowner;
                emp.Capacitacion = hrCapacitacion.Capacitacion;

                emp.Ultimousuario = usuarioActual.UsuarioLogin;
                emp.Ultimafechamodif = ahora;

                hrCapacitacionBeneficiarioDao.registrar(emp);
            }

            foreach (HrCapacitacionEmpleado emp in hrCapacitacion.listaPsEmpleados)
            {
                emp.Companyowner = hrCapacitacion.Companyowner;
                emp.Capacitacion = hrCapacitacion.Capacitacion;

                emp.Ultimousuario = usuarioActual.UsuarioLogin;
                emp.Ultimafechamodif = ahora;

                hrCapacitacionEmpleadoDao.registrar(emp);
            }

            foreach (HrEmpleadocaphorario horario in hrCapacitacion.listaHorarios)
            {

                horario.Empleado = 0;
                horario.Capacitacion = hrCapacitacion.Capacitacion;
                horario.Companyowner = hrCapacitacion.Companyowner;

                horario.Ultimousuario = usuarioActual.UsuarioLogin;
                horario.Ultimafechamodif = ahora;

                hrEmpleadocaphorarioDao.registrar(horario);
            }

            foreach (HrCapacitacionFolios folio in hrCapacitacion.listaFolios)
            {
                if (!UString.estaVacio(folio.Capacitacion))
                {
                    continue;
                }
                folio.Capacitacion = hrCapacitacion.Capacitacion;
                folio.Companyowner = hrCapacitacion.Companyowner;
                folio.Ultimousuario = usuarioActual.UsuarioLogin;
                folio.Ultimafechamodif = DateTime.Now;
                hrCapacitacionFoliosDao.registrar(folio);
                //guardar el adjunto en la carpeta compartida
                if ((UString.estaVacio(folio.Archivo) || UString.estaVacio(folio.auxData)))
                {
                    continue;
                }
                String rutaDocumento = Path.GetFileName(folio.Archivo);
                Int32 inicio = rutaDocumento.LastIndexOf(Path.DirectorySeparatorChar);
                String nombreDocumento = rutaDocumento.Substring(inicio + 1);
                rutaDocumento = rutaServer + nombreDocumento;
                if (File.Exists(rutaDocumento))
                    File.Delete(rutaDocumento);
                //quitar data:/
                var i = folio.auxData.IndexOf(',') + 1;
                var fin = folio.auxData.Length;
                var bin = folio.auxData.Substring(i, fin - i);
                folio.auxData = bin;
                File.WriteAllBytes(rutaDocumento, System.Convert.FromBase64String(folio.auxData));
            }

            return hrCapacitacion;
        }

        public HrCapacitacion solicitudObtenerPorId(HrCapacitacionPk hrCapacitacionPk)
        {
            HrCapacitacion hrCapacitacion = this.obtenerPorId(hrCapacitacionPk);

            if (hrCapacitacion != null)
            {
                if (!UString.estaVacio(hrCapacitacion.IdInstitucion))
                {
                    var ins = psInstitucionDao.obtenerPorId(new PsInstitucionPk() { IdInstitucion = hrCapacitacion.IdInstitucion }.obtenerArreglo());
                    hrCapacitacion.direccion = ins.Direccion;
                }

                hrCapacitacion.listaEmpleados = hrEmpleadocapacitacionDao.listarPorCapacitacion(hrCapacitacionPk);
                hrCapacitacion.listaHorarios = hrEmpleadocaphorarioDao.listarPorCapacitacion(hrCapacitacionPk);
                hrCapacitacion.listaFolios = hrCapacitacionFoliosDao.listarPorCapacitacion(hrCapacitacionPk);


                //obtener ambos detalles

                hrCapacitacion.listaPsBeneficiarios = hrCapacitacionBeneficiarioDao.listarPorCapacitacion(hrCapacitacionPk);
                hrCapacitacion.listaPsEmpleados = hrCapacitacionEmpleadoDao.listarPorCapacitacion(hrCapacitacionPk);

                foreach (var item in hrCapacitacion.listaPsBeneficiarios)
                {
                    if (!UString.estaVacio(item.IdInstitucion))
                    {
                        var ins = psInstitucionDao.obtenerPorId(new PsInstitucionPk() { IdInstitucion = item.IdInstitucion }.obtenerArreglo());
                        item.auxInstitucion = ins == null ? "" : ins.Nombre;
                    }

                    if (!UInteger.esCeroOrNulo(item.IdBeneficiario))
                    {
                        var en = psEntidadDao.obtenerPorId(new PsEntidadPk() { IdEntidad = item.IdBeneficiario }.obtenerArreglo());
                        item.auxPersonaNombreCompleto = en == null ? "" : en.Nombrecompleto;
                        if (en.IdSexo != null)
                        {
                            item.sexo = en.IdSexo == "F" ? "Femenino" : en.IdSexo == "M" ? "Masculino" : "";

                        }
                        item.edad = en.Edad;
                    }

                }

                foreach (var item in hrCapacitacion.listaPsEmpleados)
                {
                    if (!UString.estaVacio(item.IdInstitucion))
                    {
                        var ins = psInstitucionDao.obtenerPorId(new PsInstitucionPk() { IdInstitucion = item.IdInstitucion }.obtenerArreglo());
                        item.auxInstitucion = ins == null ? "" : ins.Nombre;
                    }

                    if (!UInteger.esCeroOrNulo(item.IdEmpleado))
                    {
                        var en = psEntidadDao.obtenerPorId(new PsEntidadPk() { IdEntidad = item.IdEmpleado }.obtenerArreglo());
                        item.auxPersonaNombreCompleto = en == null ? "" : en.Nombrecompleto;
                        if (en.IdSexo != null)
                        {
                            item.sexo = en.IdSexo == "F" ? "Femenino" : en.IdSexo == "M" ? "Masculino" : "";

                        }

                        item.edad = en.Edad;
                    }
                }

                for (int i = 0; i < hrCapacitacion.listaEmpleados.Count; i++)
                {
                    HrEmpleadocapacitacion bean = hrCapacitacion.listaEmpleados[i];
                    hrCapacitacion.listaEmpleados[i].AuxPersonaNombreCompleto = personamastDao.obtenerNombrePorPk(new PersonamastPk() { Persona = bean.Empleado.Value });
                }

            }

            hrCapacitacion.Afe = hrCapacitacion.Afe == null ? null : hrCapacitacion.Afe.Trim();
            hrCapacitacion.Modalidad = hrCapacitacion.Modalidad == null ? null : hrCapacitacion.Modalidad.Trim();

            return hrCapacitacion;
        }

        public DtoTabla descargarAdjunto(HrCapacitacionFoliosPk pk)
        {
            var rutaBase = parametrosmastDao.obtenerValorExplicacion(HrCapacitacionFolios.PARAMETRO_RUTA_ADJUNTO, "HR", "999999");
            if (rutaBase != null)
            {
                rutaBase = rutaBase + pk.Companyowner + "-" + pk.Capacitacion;

                var folio = hrCapacitacionFoliosDao.obtenerPorId(pk.obtenerArreglo());
                if (folio != null)
                {
                    if (!UString.estaVacio(folio.Archivo))
                    {
                        rutaBase = rutaBase + Path.DirectorySeparatorChar + folio.Archivo;

                        var dto = new DtoTabla();
                        dto.Nombre = folio.Archivo;
                        var bytes = UFile.obtenerArregloByte(rutaBase);
                        if (bytes != null)
                        {
                            dto.base64 = Convert.ToBase64String(bytes);
                            return dto;
                        }
                    }
                }
            }
            return null;
        }

        public void eliminarFolio(HrCapacitacionFolios pk)
        {
            hrCapacitacionFoliosDao.eliminar(pk);
        }

        public List<DtoPrevencionSalud> listarPrevencionSaludCabecera(FiltroGraficos filtro)
        {
            return hrCapacitacionDao.listarPrevencionSaludCabecera(filtro);
        }

        public List<DtoPrevencionSalud> listarPrevencionSaludDetalle(FiltroGraficos filtro)
        {
            return hrCapacitacionDao.listarPrevencionSaludDetalle(filtro);
        }

        public string Exportar(HrCapacitacionPk pk)
        {
            HrCapacitacion bean = obtenerPorId(pk);
            HrCursodescripcion curso = new HrCursodescripcion();
            if (!UInteger.esCeroOrNulo(bean.Curso))
            {
                curso = hrCursodescripcionDao.obtenerPorId(new HrCursodescripcionPk() { Curso = bean.Curso }.obtenerArreglo());
            }
            HrCentroestudios centro = new HrCentroestudios();
            if (!UInteger.esCeroOrNulo(bean.Centrocapacitacion))
            {
                centro = hrCentroestudiosDao.obtenerPorId(new HrCentroestudiosPk() { Centro = bean.Centrocapacitacion }.obtenerArreglo());
            }
            List<DtoCapacitacionParticipante> listarFurh = hrCapacitacionDao.listarParticipantes(pk);

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Capacitación " + pk.Capacitacion + " " + UFechaHora.obtenerNombreScat() + ".xlsx";

            string URL = "../Archivos/Excel/" + sFileName;
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoja 1");
                //First add the headers

                worksheet.Cells[2, 1].Value = "CAPACITACION";
                worksheet.Cells[2, 2].Value = "TEMATICA";
                worksheet.Cells[2, 3].Value = "ASIGNADO A";
                worksheet.Cells[2, 4].Value = "TIPO CAPACITACION";
                worksheet.Cells[2, 5].Value = "CAPACITACION CERTIFICADA";
                worksheet.Cells[2, 6].Value = "FINANCIADO POR";
                worksheet.Cells[2, 7].Value = "CENTRO RESPONSABLE";
                worksheet.Cells[2, 8].Value = "RESPONSABLE";
                worksheet.Cells[2, 9].Value = "FECHA INICIO ";
                worksheet.Cells[2, 10].Value = "FECHA FIN";

                worksheet.Cells[3, 1].Value = bean.Capacitacion;
                worksheet.Cells[3, 2].Value = curso.Descripcion;
                worksheet.Cells[3, 3].Value = bean.asignado == "B" ? "Beneficiarios" : "Personal de Instituciones";
                worksheet.Cells[3, 4].Value = bean.Tipocurso == "I" ? "Interno" : "Externo";
                worksheet.Cells[3, 5].Value = bean.certificado == "S" ? "Si" : "No";
                worksheet.Cells[3, 6].Value = bean.financiadoPs == "F" ? "Fundación" : "Otro";
                worksheet.Cells[3, 7].Value = centro.Descripcion;
                worksheet.Cells[3, 8].Value = bean.Expositor;
                worksheet.Cells[3, 9].Value = bean.Fechadesde.HasValue ? bean.Fechadesde.Value.ToString("dd/MM/yyyy") : "";
                worksheet.Cells[3, 10].Value = bean.Fechahasta.HasValue ? bean.Fechahasta.Value.ToString("dd/MM/yyyy") : "";

                worksheet.Cells[6, 1].Value = "PARTICIPANTES";
                worksheet.Cells[6, 2].Value = "INSTITUCION";
                worksheet.Cells[6, 3].Value = "ASISTIO";
                worksheet.Cells[6, 4].Value = "RENDIMIENTO";
                worksheet.Cells[6, 5].Value = "PARTICIPACION";
                worksheet.Cells[6, 6].Value = "COMENTARIO";


                int fila = 7;

                if (listarFurh.Count > 0)
                {
                    foreach (DtoCapacitacionParticipante obj in listarFurh)
                    {
                        worksheet.Cells[fila, 1].Value = obj.participante;
                        worksheet.Cells[fila, 2].Value = obj.institucion;
                        worksheet.Cells[fila, 3].Value = obj.asistio;
                        worksheet.Cells[fila, 4].Value = obj.rendimiento;
                        worksheet.Cells[fila, 5].Value = obj.participacion;
                        worksheet.Cells[fila, 6].Value = obj.comentario;
                        fila++;
                    }
                }

                worksheet.Cells["A2:J3"].AutoFitColumns();

                worksheet.Cells["A6:F1000"].AutoFitColumns();

                using (var cells = worksheet.Cells[2, 1, 2, 10])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                using (var cells = worksheet.Cells[6, 1, 6, 6])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.

                return URL;
            }
        }
    }
}

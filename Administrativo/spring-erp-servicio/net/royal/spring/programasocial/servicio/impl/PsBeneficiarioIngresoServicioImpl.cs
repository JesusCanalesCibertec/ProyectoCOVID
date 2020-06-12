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
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsBeneficiarioIngresoServicioImpl : GenericoServicioImpl, PsBeneficiarioIngresoServicio {

        private IServiceProvider servicioProveedor;
        private PsBeneficiarioDao psBeneficiarioDao;
        private PsBeneficiarioIngresoDao psBeneficiarioIngresoDao;
        private PsBeneficiarioIngresoCausalDao psBeneficiarioIngresoCausalDao;
        private PsBeneficiarioIngresoDiagnosticoDao psBeneficiarioIngresoDiagnosticoDao;
        private SyCorreoServicio syCorreoServicio;
        private SyReporteServicio syReporteServicio;
        private PsEntidadDao psEntidadDao;

        public PsBeneficiarioIngresoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psBeneficiarioDao = servicioProveedor.GetService<PsBeneficiarioDao>();
            psBeneficiarioIngresoDao = servicioProveedor.GetService<PsBeneficiarioIngresoDao>();
            psBeneficiarioIngresoCausalDao = servicioProveedor.GetService<PsBeneficiarioIngresoCausalDao>();
            psBeneficiarioIngresoDiagnosticoDao = servicioProveedor.GetService<PsBeneficiarioIngresoDiagnosticoDao>();
            syCorreoServicio = servicioProveedor.GetService<SyCorreoServicio>();
            syReporteServicio = servicioProveedor.GetService<SyReporteServicio>();
            psEntidadDao = servicioProveedor.GetService<PsEntidadDao>();
        }

        public PsBeneficiarioIngreso coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean)
        {

            int ingreso = psBeneficiarioIngresoDao.ObtenerUltimoIngreso(bean.IdBeneficiario);

            if(ingreso == 0)
            {
                throw new UException("El beneficiario no cuenta con fecha de ingreso");
            }

            bean.IdIngreso = psBeneficiarioIngresoDao.generarCodigo(bean.IdBeneficiario);

            foreach (PsBeneficiarioIngresoCausal deta in bean.listaCausal)
            {
                deta.IdInstitucion = bean.IdInstitucion;
                deta.IdBeneficiario = bean.IdBeneficiario;
                deta.IdIngreso = bean.IdIngreso;
                psBeneficiarioIngresoCausalDao.coreInsertar(usuarioActual, deta);
            }
            if (UString.estaVacio(bean.Estado))
                bean.Estado = "A";
                bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
                bean.CreacionFecha = DateTime.Now;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;

            /*ACTUALIZANDO BENEFICIARIO*/
            PsBeneficiario beneficiario = new PsBeneficiario();

            beneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk(bean.IdInstitucion, bean.IdBeneficiario).obtenerArreglo());

            beneficiario.Estado = "ACT";
            beneficiario.IdArea = bean.IdArea;
            beneficiario.IdComponenteIngreso = bean.IdIngreso;
            beneficiario.IdComponenteCapacidad = null;
            beneficiario.IdComponenteNutricion = null;
            beneficiario.IdComponenteSalud = null;
            beneficiario.IdComponenteSocioAmbiental = null;

            psBeneficiarioDao.actualizar(beneficiario);
            /*ACTUALIZANDO BENEFICIARIO*/

            return psBeneficiarioIngresoDao.registrar(bean);
        }

        public PsBeneficiarioIngreso coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean)
        {
            PsBeneficiarioIngreso objeto = new PsBeneficiarioIngreso();

            int ingreso = psBeneficiarioIngresoDao.ObtenerUltimoIngreso(bean.IdBeneficiario);

            if (ingreso == 0)
            {
                throw new UException("El beneficiario no cuenta con datos de ingreso");
            }

            objeto = psBeneficiarioIngresoDao.obtenerPorId(new PsBeneficiarioIngresoPk(
               bean.IdInstitucion,
               bean.IdBeneficiario,
               ingreso
                ).obtenerArreglo());

            objeto.FechaEgreso = bean.FechaEgreso;
            objeto.IdMotivoEgreso = bean.IdMotivoEgreso;
            objeto.ComentariosEgreso = bean.ComentariosEgreso;

            foreach (PsBeneficiarioIngresoDiagnostico deta in bean.listaDiagnostico)
            {
                deta.IdInstitucion = objeto.IdInstitucion;
                deta.IdBeneficiario = objeto.IdBeneficiario;
                deta.IdIngreso = objeto.IdIngreso;
                psBeneficiarioIngresoDiagnosticoDao.coreInsertar(usuarioActual, deta);
            }

            /*ACTUALIZANDO BENEFICIARIO*/
            PsBeneficiario beneficiario = new PsBeneficiario();

            beneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk(bean.IdInstitucion,bean.IdBeneficiario).obtenerArreglo());

            if (bean.Estado == "FIN")
            {
                beneficiario.Estado = "EGR";
            }
            else
            {
                beneficiario.Estado = "TMP";

                //envio de correo

                PsEntidad entidad = psEntidadDao.obtenerPorId(new PsEntidadPk() { IdEntidad = bean.IdBeneficiario }.obtenerArreglo());
                


                EmailConfiguracion configCorreo = syCorreoServicio.obtenerConfiguracion();

                List<ParametroPersistenciaGenerico> lstMetadata = new List<ParametroPersistenciaGenerico>();
                List<DtoCorreo> listaCorreos = new List<DtoCorreo>();
                List<Email> listaEmail = new List<Email>();
                DtoReporteParametro reportePk = new DtoReporteParametro("PS", "RB2");



                foreach (String item in psBeneficiarioDao.listarCorreos(bean.IdInstitucion, "EGRCO"))
                {
                    listaCorreos.Add(new DtoCorreo(-1, null, item));
                }

                lstMetadata.Add(new ParametroPersistenciaGenerico("p_beneficiario", ConstanteUtil.TipoDato.String, entidad.Nombrecompleto));
                lstMetadata.Add(new ParametroPersistenciaGenerico("p_fechaegreso", ConstanteUtil.TipoDato.String, objeto.FechaEgreso));


                listaEmail = syReporteServicio.generarListaCorreo(reportePk, lstMetadata, listaCorreos);
                syCorreoServicio.enviarCorreInmediatoAsincrono(configCorreo, listaEmail);

                //fin envio de correos
            }


            psBeneficiarioDao.actualizar(beneficiario);
            /*ACTUALIZANDO BENEFICIARIO*/


            return psBeneficiarioIngresoDao.coreActualizar(usuarioActual,objeto,objeto.Estado);
        }

        public PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual, PsBeneficiarioIngresoPk id)
        {
            return psBeneficiarioIngresoDao.coreAnular(usuarioActual,id);
        }

        public PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual, String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso)
        {
            return psBeneficiarioIngresoDao.coreAnular(usuarioActual,  pIdInstitucion, pIdBeneficiario, pIdIngreso);
        }

        public void coreEliminar(PsBeneficiarioIngresoPk id)
        {
            psBeneficiarioIngresoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso)
        {
            psBeneficiarioIngresoDao.coreEliminar( pIdInstitucion, pIdBeneficiario, pIdIngreso);
        }  

        public PsBeneficiarioIngreso obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso)
        {
            return psBeneficiarioIngresoDao.obtenerPorId( pIdInstitucion, pIdBeneficiario, pIdIngreso);
        }

        public PsBeneficiarioIngreso obtenerUltimoIngreso(PsBeneficiarioIngresoPk id)
        {
            id.IdIngreso = psBeneficiarioIngresoDao.ObtenerUltimoIngreso(id.IdBeneficiario);
            return psBeneficiarioIngresoDao.obtenerPorId(id.obtenerArreglo());
        }

        public List<DtoBeneficiarioIngreso> listado(int pIdBeneficiario, string pIdInstitucion)
        {
            return psBeneficiarioIngresoDao.listado(pIdBeneficiario, pIdInstitucion);
        }
    }
}

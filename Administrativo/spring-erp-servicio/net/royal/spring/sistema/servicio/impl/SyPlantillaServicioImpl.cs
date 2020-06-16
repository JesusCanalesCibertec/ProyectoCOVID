using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.servicio.impl
{

    public class SyPlantillaServicioImpl : GenericoServicioImpl, SyPlantillaServicio {

        private IServiceProvider servicioProveedor;
        private SyPlantillaDao syPlantillaDao;

        public SyPlantillaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            syPlantillaDao = servicioProveedor.GetService<SyPlantillaDao>();
        }

        public void registrar(UsuarioActual usuarioActual, SyPlantilla bean)
        {
            bean.Plantilla = UByte.convertirByte(bean.AuxString);
            bean.Ultimafechamodif = new DateTime();
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            syPlantillaDao.actualizar(bean);
        }

        public void actualizar(UsuarioActual usuarioActual, SyPlantilla bean)
        {
            bean.Plantilla = UByte.convertirByte(bean.AuxString);
            bean.Ultimafechamodif = new DateTime();
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            syPlantillaDao.actualizar(bean);
        }

        public void eliminar(SyPlantillaPk syPlantillaPk)
        {
            SyPlantilla syPlantilla = syPlantillaDao.obtenerPorId(syPlantillaPk);
            syPlantillaDao.eliminar(syPlantilla);
        }
                
        public List<SyPlantilla> listarTodos()
        {
            return syPlantillaDao.listarTodos();
        }

        public SyPlantilla obtenerPorId(SyPlantillaPk syPlantillaPk)
        {
            return syPlantillaDao.obtenerPorId(syPlantillaPk);
        }

        public SyPlantilla obtenerPorIdString(SyPlantillaPk pkC)        {
            SyPlantilla plantilla = syPlantillaDao.obtenerPorId(pkC);
            if (plantilla.Plantilla == null)
            {
                return plantilla;
            }
            plantilla.AuxString = UByte.convertirString(plantilla.Plantilla);
            return plantilla;
        }

        public ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, SyPlantilla filtro)
        {
            return syPlantillaDao.solicitudListar(paginacion, filtro);
        }

        public List<Email> generarListaCorreo(SyPlantillaPk syPlantillaPk, List<ParametroPersistenciaGenerico> lstMetadata, List<string> listaCorreos)
        {
            SyPlantilla bean = null;
            List<Email> listaEmails = new List<Email>();
            String cuerpoCorreo = "";
            String asunto = "";
            
            bean = syPlantillaDao.obtenerPorId(syPlantillaPk.obtenerArreglo());

            if (bean == null)
            {
                asunto = "No existe plantilla";
                ParametroListaGenerico lista = new ParametroListaGenerico();
                lista.parametroAgregarLista(lstMetadata);
                cuerpoCorreo = cuerpoCorreo + lista.parametroObtenerString("p_proceso_nro") + "\n";
                cuerpoCorreo = cuerpoCorreo + lista.parametroObtenerString("p_solicitante_id") + "\n";
                cuerpoCorreo = cuerpoCorreo + lista.parametroObtenerString("p_solicitante_nombre") + "\n";
                cuerpoCorreo = cuerpoCorreo + lista.parametroObtenerString("p_fecha_solicitud") + "\n";
                cuerpoCorreo = cuerpoCorreo + lista.parametroObtenerString("p_compania_nombre") + "\n";
                cuerpoCorreo = cuerpoCorreo + lista.parametroObtenerString("p_estado_nombre") + "\n";
            }
            else
            {
                asunto = bean.Nombre;
                cuerpoCorreo = UByte.convertirString(bean.Plantilla);
                
                foreach (ParametroPersistenciaGenerico para in lstMetadata)
                {
                    //logger.debug("=====");
                    //logger.debug(para.getCampo());
                    //logger.debug(para.getValor());
                    if (para.Valor != null)
                        cuerpoCorreo = cuerpoCorreo.Replace(para.Campo, para.Valor.ToString());
                }
            }

            foreach (String email in listaCorreos)
            {
                Email e = new Email();
                e.Asunto = asunto;
                e.ListaCorreoDestino.Add(new EmailDestino(email));
                e.CuerpoCorreo = UByte.convertirByte(cuerpoCorreo);
                listaEmails.Add(e);
            }

            return listaEmails;
        }       
    }
}

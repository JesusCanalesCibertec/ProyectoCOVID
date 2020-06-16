using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System.Transactions;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.constante;

namespace net.royal.spring.minedu.servicio.impl
{
    public class MpPersonaServicioImpl : GenericoServicioImpl, MpPersonaServicio
    {
        private IServiceProvider servicioProveedor;
        private MpPersonaDao mpPersonaDao;
        private MpContratacionDao mpContratacionDao;
        private SeguridadperfilusuarioDao seguridadperfilusuarioDao;

        public MpPersonaServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            mpPersonaDao = servicioProveedor.GetService<MpPersonaDao>();
            mpContratacionDao = servicioProveedor.GetService<MpContratacionDao>();
            seguridadperfilusuarioDao = servicioProveedor.GetService<SeguridadperfilusuarioDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return mpPersonaDao.listarPaginacion(paginacion, filtro);
        }

        public MpPersona registrar(UsuarioActual usuarioActual, MpPersona bean)
        {
            string mensaje = "";
            try
            {
                MpPersona existePersona = mpPersonaDao.ObtenerPersonaxDNI(bean.Dni);
                if (existePersona != null)
                {
                    mensaje = "El DNI ingresado se encuentra registrado";
                    throw new Exception();
                }
                bean.IdPersona = mpPersonaDao.generarCodigo();
                bean.Apellido = UString.Mayusculas(bean.Apellido);
                bean.Nombre = UString.Mayusculas(bean.Nombre);
                bean.Usuario = UString.Mayusculas(bean.Usuario);
                bean.Compania = "00020000";
                bean.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";

                bean = mpPersonaDao.registrar(usuarioActual, bean);

                MpContratacion contrato = new MpContratacion();
                contrato.IdContratacion = mpContratacionDao.generarCodigo();
                contrato.IdPersona = bean.IdPersona;
                contrato.Fechainicio = bean.Fechainicio;
                contrato.Fechafin = bean.Fechafin;
                contrato.IdModalidad = bean.IdModalidad;
                contrato.Numeroorden = bean.Numeroorden;
                contrato.Cargo = bean.Cargo;
                contrato.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";

                MpContratacion existeContrato = mpContratacionDao.ObtenerContratoxNOrden(contrato.Numeroorden);
                if (existeContrato != null && contrato.IdModalidad == "OS")
                {
                    mensaje = "El número de orden ingresado se encuentra registrado";
                    throw new Exception();
                }

                mpContratacionDao.registrar(usuarioActual, contrato);

                return bean;

            }
            catch (Exception ex)
            {
                if (UString.estaVacio(mensaje)) mensaje = "Error en el proceso de registro";
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, mensaje));
                throw new UException(lista);
            }

        }

        public MpPersona obtenerPorId(int pIdPersona, int pIdContratacion)
        {
            try
            {
                MpPersona bean = new MpPersona();
                bean = mpPersonaDao.obtenerPorId(new MpPersona() { IdPersona = pIdPersona }.obtenerArreglo());

                if (pIdContratacion > 0)
                {
                    MpContratacion contrato = new MpContratacion();
                    contrato = mpContratacionDao.obtenerPorId(new MpContratacion() { IdContratacion = pIdContratacion }.obtenerArreglo());

                    bean.AuxIdContratacion = contrato.IdContratacion;
                    bean.Fechainicio = contrato.Fechainicio;
                    bean.Fechafin = contrato.Fechafin;
                    bean.IdModalidad = contrato.IdModalidad;
                    bean.Numeroorden = contrato.Numeroorden;
                    bean.Cargo = contrato.Cargo;
                }
                return bean;
            }
            catch (Exception ex)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de consulta"));
                throw new UException(lista);
            }
        }

        public MpPersona actualizar(UsuarioActual usuarioActual, MpPersona bean)
        {
            try
            {
                if (UString.estaVacio(bean.Correo)) bean.Correo = null;
                bean.Apellido = UString.Mayusculas(bean.Apellido);
                bean.Nombre = UString.Mayusculas(bean.Nombre);

                MpContratacion contrato = mpContratacionDao.obtenerPorId(new MpContratacionPk() { IdContratacion = bean.AuxIdContratacion }.obtenerArreglo());

                contrato.IdPersona = bean.IdPersona;
                contrato.Fechainicio = bean.Fechainicio;
                contrato.Fechafin = bean.Fechafin;
                contrato.IdModalidad = bean.IdModalidad;
                contrato.Numeroorden = bean.Numeroorden;
                contrato.Numeroplazo = bean.Numeroplazo;
                contrato.Cargo = bean.Cargo;
                contrato.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";

                mpContratacionDao.actualizar(usuarioActual, contrato);
                bean.Estado = DateTime.Now.Date > bean.Fechafin ? "I" : "A";

                return mpPersonaDao.actualizar(usuarioActual, bean); ;
            }
            catch (Exception ex)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de actualización"));
                throw new UException(lista);
            }
        }

        public MpContratacion cambiarestado(MpContratacion bean)
        {
            MpPersona persona = mpPersonaDao.obtenerPorId(new MpPersonaPk() { IdPersona = bean.IdPersona }.obtenerArreglo());
            persona.Estado = "I";
            mpPersonaDao.actualizar(persona);

            MpContratacion contratacion = mpContratacionDao.obtenerPorId(new MpContratacion() { IdContratacion = bean.IdContratacion }.obtenerArreglo());
            contratacion.Estado = "I";
            contratacion.Fechacese = bean.Fechacese;
            contratacion.Motivocese = bean.Motivocese;
            mpContratacionDao.actualizar(contratacion);

            return bean;
        }

        public List<MpPersona> ListarNombres()
        {
            return mpPersonaDao.ListarNombres();
        }

        public List<DtoListafechas> ListarPersonal(Nullable<DateTime> parametro)
        {
            return mpPersonaDao.ListarPersonal(parametro);
        }

        public List<DtoEventos> ListarEventos(int? pIdPersona)
        {
            string[] principal =
                new string[10] { "#117581", "#108e0e", "#2B4ED1", "#0a92a1", "#797979",
                    "#db4e4e", "#cc9200", "#7a28a3", "#2fa39d", "#ae5618" };
            string[] secundario =
                new string[10] { "#04a0b3", "#14b211", "#5d78de", "#0cb7c9", "#979797",
                    "#e88c8c", "#ffab00 ", "#b264d9  ", "#3fc8c1", "#e78e4f" };
            List<DtoEventos> lista = mpPersonaDao.ListarEventos(pIdPersona);

            foreach (var item in lista)
            {
                int residuo = item.id.Value % 10;
                item.backgroundColor = "" +
                    ";background: repeating-linear-gradient(45deg,  " + principal[residuo - 1] + ",  " +
                    principal[residuo - 1] + " 10px,  " +
                    secundario[residuo - 1] + " 10px,  " +
                    secundario[residuo - 1] + " 20px); ";
                item.borderColor = principal[residuo - 1];
            }
            return lista;
        }

        public ParametroPaginacionGenerico ListarPaginacionUsuario(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return seguridadperfilusuarioDao.ListarPaginacionUsuario(paginacion, filtro);
        }

        public Seguridadperfilusuario RegistrarUsuario(UsuarioActual usuarioActual, Seguridadperfilusuario bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Usuario = UString.Mayusculas(bean.Usuario);
            bean.Perfil = UString.Mayusculas(bean.Perfil);
            return seguridadperfilusuarioDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public Seguridadperfilusuario EliminarUsuario(Seguridadperfilusuario bean)
        {
            this.seguridadperfilusuarioDao.eliminar(bean);
            return bean;
        }

        public Seguridadperfilusuario ActualizarUsuario(UsuarioActual usuarioActual, Seguridadperfilusuario bean)
        {
            this.seguridadperfilusuarioDao.actualizar(bean);
            return bean;
        }
    }
}

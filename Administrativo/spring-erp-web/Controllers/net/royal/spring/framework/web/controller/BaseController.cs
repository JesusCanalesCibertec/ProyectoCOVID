using Angular.Controllers.net.royal.spring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.servicio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace net.royal.spring.framework.web.controller
{

    public class BaseController : Controller
    {

        private ParametrosmastServicio parametrosmastServicio;
        private SeguridadautorizacionesServicio _seguridadautorizacionesServicio;

        protected readonly LoginUser _datos;

        protected UsuarioActual _usuarioActualP;

        protected UsuarioActual _usuarioActual
        {
            get
            {
                if (_usuarioActualP == null)
                {
                    _usuarioActualP = getSessionData<UsuarioActual>(_datos.Usuario + "|" + _datos.Compania);
                }
                return _usuarioActualP;
            }
        }

        public BaseController(IHttpContextAccessor httpContextAccessor, IServiceProvider _servicioProveedor)
        {
            var usuarioLogin = httpContextAccessor.CurrentUser();
            _datos = usuarioLogin;
            this.parametrosmastServicio = _servicioProveedor.GetService<ParametrosmastServicio>();
            _seguridadautorizacionesServicio = _servicioProveedor.GetService<SeguridadautorizacionesServicio>();
        }


        public T getSessionData<T>(string key)
        {
            var d = HttpContext.Session.GetString(key);

            d = d == null ? null : d;

            if (d == null || d == "null")
            {
                return default(T);
            }

            T v;

            try
            {
                v = JsonConvert.DeserializeObject<T>(d);
                return v;
            }
            catch (Exception ex)
            {

            }

            return default(T);
        }

        public void setSessionData(string key, object value)
        {
            HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }


        private Boolean existeConcepto(List<Seguridadconcepto> lstConceptos, String concepto)
        {
            var d = parametrosmastServicio.obtenerPorId(new ParametrosmastPk("MODENT", "SY", "999999"));

            if (d != null)
            {
                return true;
            }

            foreach (Seguridadconcepto item in lstConceptos)
            {
                String c = item.Aplicacioncodigo.Trim() + item.Grupo.Trim() + item.Concepto.Trim();
                if (c.Equals(concepto))
                    return true;
            }
            return false;
        }

        public void inicializarMenu(string usuario)
        { 
            
            List<Seguridadgrupo> grupos = _seguridadautorizacionesServicio.listarPorAplicacionUsuario("MP", usuario);

            Menu menu = new Menu(new List<Item>());

            foreach (var item in grupos)
            {
                Item grupo = new Item();
                grupo.id = item.Grupo.Trim();
                grupo.label = item.Descripcion;

                grupo.icon =
                grupo.id == "GRUPO1" ? "fas fa-project-diagram" : 
                grupo.id == "GRUPO2" ? "fas fa-universal-access" : 
                grupo.id == "GRUPO3" ? "fas fa-chart-pie" : "fab fa-whmcs";
                //grupo.icon = "fa fa-fw fa-pencil-square-o";


                foreach (var item2 in item.conceptos)
                {
                    if (UBoolean.validarFlag(item2.Visibleflag))
                    {
                        Item concepto = new Item();
                        concepto.label = item2.Descripcion;
                        concepto.icon = "far fa-caret-square-right";
                        concepto.routerLink = item2.WebPage;
                        concepto.action = item2.WebAction;
                        grupo.items = grupo.items == null ? new List<Item>() : grupo.items;
                        grupo.items.Add(concepto);
                    }
                }

                menu.items.Add(grupo);
            }

           

            foreach (var i in grupos)
            {
                if (i.conceptos.Count==0)
                {
                    i.conceptos = null;
                }
                foreach (var item in i.conceptos)
                {
                    setSessionData("tiene" + (item.Aplicacioncodigo.Trim() + item.Grupo.Trim() + item.Concepto.Trim()), true);
                }
            }

            setSessionData("menu", menu);

        }


    }
}
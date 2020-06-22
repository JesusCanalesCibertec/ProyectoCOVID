using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.covid.dao;
using net.royal.spring.covid.dominio;
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid.servicio.impl
{
    public class TriajeServicioImpl : GenericoServicioImpl, TriajeServicio
    {
        private IServiceProvider servicioProveedor;
        private TriajeDao triajeDao;
        private CiudadanoDao ciudadanoDao;

        public TriajeServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            triajeDao = servicioProveedor.GetService<TriajeDao>();
            ciudadanoDao = servicioProveedor.GetService<CiudadanoDao>();
        }

        //public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroTriaje filtro)
        //{
        //    return triajeDao.listarPaginacion(paginacion, filtro);
        //}

        public Triaje registrar(UsuarioActual usuarioActual, Triaje bean)
        {
            try
            {
                bean.Estado = this.obtenerEstado(bean);

                Ciudadano aux = ciudadanoDao.obtenerPorId(new CiudadanoPk() { IdCiudadano = bean.IdCiudadano }.obtenerArreglo());
                aux.Estado = bean.Estado;

                ciudadanoDao.actualizar(aux);

                return triajeDao.registrar(usuarioActual, bean);
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public int obtenerEstado(Triaje bean){

            int? num_sintomas = this.contarSintoma(bean);
            int num_situaciones = this.contarSituacion(bean);
            int num_cronicos = this.contarCronico(bean);

            if (num_sintomas == null  && num_situaciones > 0 && num_cronicos > 0)
            {
                return 5;
            }
            else if (num_sintomas == null && num_situaciones > 0)
            {
                return 4;
            }
            else if (num_sintomas >= 2 && num_situaciones > 0)
            {
                return 3;
            }
            else if (num_sintomas < 2 || num_sintomas == null || num_situaciones > 0)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        public int? contarSintoma(Triaje bean)
        {
            int? numero = 0;

            if (bean.Disgus == "SI")
                numero = numero + 1;
            if (bean.Tos == "SI")
                numero = numero + 1;
            if (bean.Dolor == "SI")
                numero = numero + 1;
            if (bean.Nasal == "SI")
                numero = numero + 1;
            if (bean.Difi == "SI")
                numero = null;
            if (bean.Fiebre == "SI")
                numero = null;

            return numero;
        }

        public int contarCronico(Triaje bean)
        {
           int numero = 0;

            if (bean.Obesidad == "SI")
                numero = numero + 1;
            if (bean.Pulmonar == "SI")
                numero = numero + 1;
            if (bean.Asma == "SI")
                numero = numero + 1;
            if (bean.Diabetes == "SI")
                numero = numero + 1;
            if (bean.Hipertension == "SI")
                numero = numero + 1;
            if (bean.Cardio == "SI")
                numero = numero + 1;
            if (bean.Renal == "SI")
                numero = numero + 1;
            if (bean.Cardio == "SI")
                numero = numero + 1;
            return numero;
        }

        public int contarSituacion(Triaje bean)
        {
            int numero = 0;

            if (bean.Situacion1 == "SI")
                numero = numero + 1;
            if (bean.Situacion2 == "SI")
                numero = numero + 1;
            if (bean.Situacion3 == "SI")
                numero = numero + 1;
            return numero;
        }

        public Triaje obtenerPorId(int pIdTriaje)
        {
            return triajeDao.obtenerPorId(new Triaje() { IdTriaje = pIdTriaje}.obtenerArreglo());
        }

        public Triaje actualizar(UsuarioActual usuarioActual, Triaje bean)
        {
            //bean.Nombre = UString.Mayusculas(bean.Nombre);
            //bean.Apellido = UString.Mayusculas(bean.Apellido);
            //bean.Direccion = UString.Mayusculas(bean.Direccion);
            return triajeDao.actualizar(usuarioActual, bean);
        }

        public TriajePk cambiarestado(TriajePk pk)
        {
            return pk;
        }

        public List<Triaje> listado(DtoTabla filtro)
        {
            return triajeDao.listado(filtro);
        }
    }
}

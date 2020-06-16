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
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.proyecto.dominio.dto;
using Newtonsoft.Json;

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/programasocial/[controller]")]
    public class PsGraficosController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsSaludServicio psSaludServicio;
        private PsMarcologicoServicio psMarcologicoServicio;

        public PsGraficosController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psSaludServicio = servicioProveedor.GetService<PsSaludServicio>();
            psMarcologicoServicio = servicioProveedor.GetService<PsMarcologicoServicio>();
        }

        [HttpPost("[action]")]
        public List<DtoIndicadoresGraficos> listarGraficosListas([FromBody]FiltroGraficos filtro) {
            return psMarcologicoServicio.obtenerListaGraficos(_usuarioActual,filtro);
        }

        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> listarGraficoMultiple() {

            List<DtoGraficoMultiple> prueba = new List<DtoGraficoMultiple>();
            DtoGraficoMultiple dto = new DtoGraficoMultiple();
            dto.series = new List<Series>();
            dto.name = "Jose";

            Series serie = new Series();
            serie.name = "Terminadas";
            serie.value = 89;

            Series serie1 = new Series();
            serie1.name = "Vencidas";
            serie1.value = 50;

            Series serie2 = new Series();
            serie2.name = "Por Vencer";
            serie2.value = 10;


            dto.series.Add(serie);
            dto.series.Add(serie1);
            dto.series.Add(serie2);

            prueba.Add(dto);

            return prueba;

        }


        [HttpGet("[action]")]
        public List<DtoResponsable> listarGrafico(string codigo) {

            List<DtoResponsable> lista = new List<DtoResponsable>();

            //DtoResponsable obj = new DtoResponsable();
            //obj.Responsable = "Jose Torres";
            //obj.Tareas = 50;
            //obj.Estado = "Terminados";

            //lista.Add(obj);

            DtoResponsable obj1 = new DtoResponsable();
            obj1.Responsable = "Alejandro Navarro";
            obj1.Tareas = 70;
            obj1.Estado = "Vencidos";

            lista.Add(obj1);

            DtoResponsable obj2 = new DtoResponsable();
            obj2.Responsable = "Ernesto Guevara";
            obj2.Tareas = 70;
            obj2.Estado = "Vencidos";


            lista.Add(obj2);


            return lista;
        }


        [HttpGet("[action]")]
        public List<DtoProyecto> listarEvaluacion() {

            List<DtoProyecto> lista = new List<DtoProyecto>();

            DtoProyecto obj = new DtoProyecto();
            obj.NombreProyecto = "Ampliacion de Pabellon B";
            obj.MontoGastado = 999;
            obj.MontoGastado = 1000;
            obj.FechaFinPresupuestado = DateTime.Now;
            obj.FechaFinEstimado = DateTime.Now;

            lista.Add(obj);

            return lista;
        }

        [HttpGet("[action]")]
        public List<DtoIndicador> listarEvaluacionBar() {

            List<DtoIndicador> lista = new List<DtoIndicador>();

            DtoIndicador obj = new DtoIndicador();
            obj.nombre = "Proyecto de Ampliacion";
            obj.Comentario = "Si";
            obj.Porcentaje = 60;

            lista.Add(obj);

            DtoIndicador obj1 = new DtoIndicador();
            obj1.nombre = "Proyecto de Ampliacion";
            obj1.Comentario = "No";
            obj1.Porcentaje = 40;

            lista.Add(obj1);

            return lista;
        }

    }
}

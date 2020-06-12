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
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using net.royal.spring.rrhh.constante;

namespace net.royal.spring.framework.web.controller
{

    [Route("api/spring/[controller]")]
    public class ReportController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            this.Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("[action]")]
        public JsonResult imprimirEncuesta([FromBody]HrEncuestaPk pk)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("companyowner", pk.Companyowner);
            parameters.Add("periodo", pk.Periodo);
            parameters.Add("secuencia", pk.Secuencia);
            parameters.Add("p_logo", Directory.GetCurrentDirectory() + @"\report\" + "canevaro.jpg");

            return imprimir(ConstanteCapacitacion.REPORTE_ENCUESTA, parameters);
        }

        [HttpPost("[action]")]
        public JsonResult imprimirConsumo([FromBody]DtoConsumo filtro)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("ID_INSTITUCION", filtro.codInstitucion);
            parameters.Add("ID_CONSUMO", filtro.codConsumo);

            return imprimir(ConstanteCapacitacion.REPORTE_CONSUMO, parameters);
        }


        [HttpPost("[action]")]
        public JsonResult hrCapacitacionesParticipaciones([FromBody]HrCapacitacionPk pk)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("capacitacion", pk.Capacitacion);
            parameters.Add("company_owner", pk.Companyowner);
            parameters.Add("idCodigo", "REPCAP005");
            parameters.Add("idFechaFormato", DateTime.Now);
            parameters.Add("idVersion", "1");
            parameters.Add("p_logo", Directory.GetCurrentDirectory() + @"\report\" + "canevaro.jpg");

            return imprimir(ConstanteCapacitacion.REPORTE_RUTA_PARTICIPANTES, parameters);
        }

        [HttpPost("[action]")]
        public JsonResult imprimirResCabecera([FromBody]FiltroGraficos filtro)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (UString.estaVacio(filtro.nomTipoRas))
            {
                filtro.nomTipoRas = null;
            }
            if (UString.estaVacio(filtro.nomDiagnostico))
            {
                filtro.nomDiagnostico = null;
            }
            if (UString.estaVacio(filtro.nomInstitucion))
            {
                filtro.nomInstitucion = null;
            }
            if (filtro.Edad == 1)
            {
                parameters.Add("P_MENOR", 0);
                parameters.Add("P_MAYOR", 5);
            }
            else if (filtro.Edad == 2)
            {
                parameters.Add("P_MENOR", 5);
                parameters.Add("P_MAYOR", 100);
            }

            parameters.Add("ID_PERIODO", filtro.IdPeriodo);
            parameters.Add("ID_PROGRAMA", filtro.IdPrograma);
            parameters.Add("FECHA_INICIO", String.Format("{0:dd/MM/yyyy}", filtro.FechaInicio));
            parameters.Add("FECHA_FIN", String.Format("{0:dd/MM/yyyy}", filtro.FechaFin));
            parameters.Add("TIPO_RAS", filtro.tipoRAS);
            parameters.Add("DIAGNOSTICO", filtro.idDiagnostico);
            parameters.Add("INSTITUCION", filtro.IdInstitucion);
            parameters.Add("NOM_TIPO_RAS", filtro.nomTipoRas);
            parameters.Add("NOM_DIAGNOSTICO", filtro.nomDiagnostico);
            parameters.Add("NOM_INSTITUCION", filtro.nomInstitucion);
            parameters.Add("FILA", filtro.secuencia);





            if (filtro.tipo_reporte == null)
            {
                return imprimir("Indicadores\\" + filtro.Codigo + ".jasper", parameters);
            }
            else
            {
                return imprimir("Indicadores\\" + filtro.Codigo + "D.jasper", parameters);
            }

        }


        public JsonResult imprimir(String rutaArchivo, Dictionary<string, object> parameters)
        {
            var ruta = Directory.GetCurrentDirectory();
            ruta = ruta + @"\report\" + rutaArchivo;

            ruta = ruta.Replace("\\", "|");

            parameters.Add("P_REPORTE", ruta);

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var data = JsonConvert.SerializeObject(parameters);

            HttpContent httpContent = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(this.Configuration["Reporter:Url"], httpContent).Result;

            var dataObjects = new DtoReporte();
            if (response.IsSuccessStatusCode)
            {
                dataObjects = response.Content.ReadAsAsync<DtoReporte>().Result;
            }
            else
            {
                throw new UException(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }

            client.Dispose();

            var carpetaFinal = "Archivos" + "/" + "Temporales" + "/" + UFechaHora.obtenerNombreUnico() + ".pdf"; ;
            var carpeta = _hostingEnvironment.WebRootPath + UFile.getSeparador() + carpetaFinal;
            System.IO.File.WriteAllBytes(carpeta, dataObjects.datos);

            return Json(new { url = carpetaFinal });
        }
    }
    public class DtoReporte
    {
        public string excepcion { get; set; }

        public byte[] datos { get; set; }

    }
}

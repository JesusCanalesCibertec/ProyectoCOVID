
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using net.royal.spring.framework.web.dao;
using System;
using System.Text;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dao.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.dao.impl;
using net.royal.spring.core.servicio;
using net.royal.spring.core.servicio.impl;
using net.royal.spring.asistencia.dao.impl;
using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.asistencia.servicio.impl;
using net.royal.spring.contabilidad.dao;
using net.royal.spring.contabilidad.dao.impl;
using net.royal.spring.contabilidad.servicio;
using net.royal.spring.contabilidad.servicio.impl;
using net.royal.spring.planilla.dao;
using net.royal.spring.planilla.dao.impl;
using net.royal.spring.planilla.servicio;
using net.royal.spring.planilla.servicio.impl;
using net.royal.spring.proceso.dao.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio.impl;
using net.royal.spring.proceso.servicio;
using net.royal.spring.rrhh.dao.impl;
using net.royal.spring.rrhh.servicio.impl;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dao;
using net.royal.spring.sistema.servicio.impl;
using net.royal.spring.sistema.servicio;
using Angular.Controllers.net.royal.spring;
using Microsoft.Extensions.Logging;
using System.IO;
using net.royal.spring.programasocial.dao.impl;
using net.royal.spring.kpi.dao;
using net.royal.spring.kpi.dao.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.servicio.impl;
using net.royal.spring.kpi.servicio;
using net.royal.spring.kpi.servicio.impl;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.servicio.impl;
using net.royal.spring.proyecto.dao.impl;
using net.royal.spring.salud.dao;
using net.royal.spring.salud.dao.impl;
using net.royal.spring.salud.servicio;
using net.royal.spring.salud.servicio.impl;
using net.royal.spring.proyecto.servicio.evento;
using net.royal.spring.proceso.servicio.evento;
using net.royal.spring.dao;
using net.royal.spring.dao.impl;
using net.royal.spring.servicio.impl;
using net.royal.spring.servicio;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dao.impl;
using net.royal.spring.minedu.servicio;
using net.royal.spring.minedu.servicio.impl;
using net.royal.spring.covid.dao;
using net.royal.spring.covid.dao.impl;
using net.royal.spring.covid.servicio;
using net.royal.spring.covid.servicio.impl;


namespace Angular {
    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            ILoggerFactory _loggerFactory = null;
            if (_loggerFactory == null)
                _loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerFactory.AddFile("Logs/web-{Date}.txt");
            services.AddSingleton(_loggerFactory);
            services.AddLogging();

            services.AddMvc(options => {
                //options.Filters.Add(typeof(CustomExceptionFilterAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options => {
                options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "wwwroot";
            });


            services.AddAuthentication().AddJwtBearer(
                cfg => {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new TokenValidationParameters() {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };

                });


            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(600);
            });

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            /*DB inicio*/
            /*CONEXION A SQL SERVER*/
            services.AddDbContext<GenericoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerDB")));

            /*CONEXION A MYSQL SERVER*/
            services.AddDbContext<MySqlDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MySqlDB")));
            /*DB fin*/




            /*SERVICIOS*/

            /** COVID - INICIO **/

            /*Dao inicio*/
            services.AddTransient<CiudadanoDao, CiudadanoDaoImpl>();

            /*Dao fin*/

            /*servicio inicio*/
            services.AddTransient<CiudadanoServicio, CiudadanoServicioImpl>();

            /*servicio fin*/

            /** COVID - FIN **/

            /** MINEDU - INICIO **/

            /*Dao inicio*/
            services.AddTransient<MpPersonaDao, MpPersonaDaoImpl>();
            services.AddTransient<MpConocimientoDao, MpConocimientoDaoImpl>();
            services.AddTransient<MpPersonaconocimientoDao, MpPersonaconocimientoDaoImpl>();
            services.AddTransient<MpContratacionDao, MpContratacionDaoImpl>();
            services.AddTransient<MpPersonatituloDao, MpPersonatituloDaoImpl>();
            services.AddTransient<MpContratacionadendaentregableDao, MpContratacionadendaentregableDaoImpl>();
            services.AddTransient<MpProyectoDao, MpProyectoDaoImpl>();
            services.AddTransient<MpAreamineduDao, MpAreamineduDaoImpl>();
            services.AddTransient<MpProyectorecursoDao, MpProyectorecursoDaoImpl>();
            services.AddTransient<MpProyectorecursoperiodoDao, MpProyectorecursoperiodoDaoImpl>();
            services.AddTransient<MpSistemaDao, MpSistemaDaoImpl>();
            /*Dao fin*/

            /*servicio inicio*/
            services.AddTransient<MpPersonaServicio, MpPersonaServicioImpl>();
            services.AddTransient<MpConocimientoServicio, MpConocimientoServicioImpl>();
            services.AddTransient<MpPersonaconocimientoServicio, MpPersonaconocimientoServicioImpl>();
            services.AddTransient<MpContratacionServicio, MpContratacionServicioImpl>();
            services.AddTransient<MpPersonatituloServicio, MpPersonatituloServicioImpl>();
            services.AddTransient<MpContratacionadendaentregableServicio, MpContratacionadendaentregableServicioImpl>();
            services.AddTransient<MpProyectoServicio, MpProyectoServicioImpl>();
            services.AddTransient<MpAreamineduServicio, MpAreamineduServicioImpl>();
            services.AddTransient<MpProyectorecursoServicio, MpProyectorecursoServicioImpl>();
            services.AddTransient<MpSistemaServicio, MpSistemaServicioImpl>();
            /*servicio fin*/

            /** MINEDU - FIN **/

            /** ASISTENCIA - INICIO **/
            services.AddTransient<AsAccesosdiariosDao, AsAccesosdiariosDaoImpl>();
            services.AddTransient<AsAreaDao, AsAreaDaoImpl>();
            services.AddTransient<AsAsistenciadiariaDao, AsAsistenciadiariaDaoImpl>();
            services.AddTransient<AsAsistenciadiariamarcaDao, AsAsistenciadiariamarcaDaoImpl>();
            services.AddTransient<AsAutorizacionDao, AsAutorizacionDaoImpl>();
            services.AddTransient<AsAutorizacionDocAprobacionDao, AsAutorizacionDocAprobacionDaoImpl>();
            services.AddTransient<AsConceptoaccesoDao, AsConceptoaccesoDaoImpl>();
            services.AddTransient<AsConceptoaccesosistemaDao, AsConceptoaccesosistemaDaoImpl>();
            services.AddTransient<AsPeriodoDao, AsPeriodoDaoImpl>();
            services.AddTransient<AsTipohorarioDao, AsTipohorarioDaoImpl>();
            services.AddTransient<AsConceptoaccesoReglaDao, AsConceptoaccesoReglaDaoImpl>();
            services.AddTransient<RtIndicadorMetaDao, RtIndicadorMetaDaoImpl>();

            services.AddTransient<AsAccesosdiariosServicio, AsAccesosdiariosServicioImpl>();
            services.AddTransient<AsAreaServicio, AsAreaServicioImpl>();
            services.AddTransient<AsAsistenciadiariaServicio, AsAsistenciadiariaServicioImpl>();
            services.AddTransient<AsAsistenciadiariamarcaServicio, AsAsistenciadiariamarcaServicioImpl>();
            services.AddTransient<AsAutorizacionServicio, AsAutorizacionServicioImpl>();
            services.AddTransient<AsAutorizacionDocAprobacionServicio, AsAutorizacionDocAprobacionServicioImpl>();
            services.AddTransient<AsConceptoaccesoServicio, AsConceptoaccesoServicioImpl>();
            services.AddTransient<AsConceptoaccesosistemaServicio, AsConceptoaccesosistemaServicioImpl>();
            services.AddTransient<AsPeriodoServicio, AsPeriodoServicioImpl>();
            services.AddTransient<AsTipohorarioServicio, AsTipohorarioServicioImpl>();
            /** ASISTENCIA - FIN **/

            /** CONTABILIDAD - INICIO **/
            services.AddTransient<AcCostcentermstDao, AcCostcentermstDaoImpl>();
            services.AddTransient<AcSucursalDao, AcSucursalDaoImpl>();
            services.AddTransient<AcSucursalgrupoDao, AcSucursalgrupoDaoImpl>();

            services.AddTransient<AcCostcentermstServicio, AcCostcentermstServicioImpl>();
            services.AddTransient<AcSucursalServicio, AcSucursalServicioImpl>();
            services.AddTransient<AcSucursalgrupoServicio, AcSucursalgrupoServicioImpl>();
            /** CONTABILIDAD - FIN **/


            /** CORE - INICIO **/
            services.AddTransient<AccountmstDao, AccountmstDaoImpl>();
            services.AddTransient<AfemstDao, AfemstDaoImpl>();
            services.AddTransient<BancoDao, BancoDaoImpl>();
            services.AddTransient<CompaniamastDao, CompaniamastDaoImpl>();
            services.AddTransient<net.royal.spring.core.dao.CompanyownerDao, CompanyownerDaoImpl>();
            services.AddTransient<CompanyownerImagenesDao, CompanyownerImagenesDaoImpl>();
            services.AddTransient<DepartamentoDao, DepartamentoDaoImpl>();
            services.AddTransient<DepartmentmstDao, DepartmentmstDaoImpl>();
            services.AddTransient<EmpleadomastDao, EmpleadomastDaoImpl>();
            services.AddTransient<MaCtsDao, MaCtsDaoImpl>();
            services.AddTransient<MaMiscelaneosdetalleDao, MaMiscelaneosdetalleDaoImpl>();
            services.AddTransient<MaMiscelaneosheaderDao, MaMiscelaneosheaderDaoImpl>();
            services.AddTransient<MaUnidadnegocioDao, MaUnidadnegocioDaoImpl>();
            services.AddTransient<MonedamastDao, MonedamastDaoImpl>();
            services.AddTransient<OcupacionmastDao, OcupacionmastDaoImpl>();
            services.AddTransient<PaisDao, PaisDaoImpl>();
            services.AddTransient<PersonamastDao, PersonamastDaoImpl>();
            services.AddTransient<ProvinciaDao, ProvinciaDaoImpl>();
            services.AddTransient<TipocambiomastDao, TipocambiomastDaoImpl>();
            services.AddTransient<ZonapostalDao, ZonapostalDaoImpl>();
            services.AddTransient<CompanyownerrecursoDao, CompanyownerrecursoDaoImpl>();
            services.AddTransient<UnidadesmastDao, UnidadesmastDaoImpl>();
            services.AddTransient<HrCapacitacionBeneficiarioDao, HrCapacitacionBeneficiarioDaoImpl>();
            services.AddTransient<HrCapacitacionEmpleadoDao, HrCapacitacionEmpleadoDaoImpl>();

            services.AddTransient<AccountmstServicio, AccountmstServicioImpl>();
            services.AddTransient<AfemstServicio, AfemstServicioImpl>();
            services.AddTransient<BancoServicio, BancoServicioImpl>();
            services.AddTransient<CompaniamastServicio, CompaniamastServicioImpl>();
            services.AddTransient<CompanyownerServicio, CompanyownerServicioImpl>();
            services.AddTransient<CompanyownerImagenesServicio, CompanyownerImagenesServicioImpl>();
            services.AddTransient<DepartamentoServicio, DepartamentoServicioImpl>();
            services.AddTransient<DepartmentmstServicio, DepartmentmstServicioImpl>();
            services.AddTransient<EmpleadomastServicio, EmpleadomastServicioImpl>();
            services.AddTransient<MaCtsServicio, MaCtsServicioImpl>();
            services.AddTransient<MaMiscelaneosdetalleServicio, MaMiscelaneosdetalleServicioImpl>();
            services.AddTransient<MaMiscelaneosheaderServicio, MaMiscelaneosheaderServicioImpl>();
            services.AddTransient<MaUnidadnegocioServicio, MaUnidadnegocioServicioImpl>();
            services.AddTransient<MonedamastServicio, MonedamastServicioImpl>();
            services.AddTransient<OcupacionmastServicio, OcupacionmastServicioImpl>();
            services.AddTransient<PaisServicio, PaisServicioImpl>();
            services.AddTransient<PersonamastServicio, PersonamastServicioImpl>();
            services.AddTransient<ProvinciaServicio, ProvinciaServicioImpl>();
            services.AddTransient<TipocambiomastServicio, TipocambiomastServicioImpl>();
            services.AddTransient<ZonapostalServicio, ZonapostalServicioImpl>();
            services.AddTransient<CompanyownerrecursoServicio, CompanyownerrecursoServicioImpl>();
            services.AddTransient<UnidadesmastServicio, UnidadesmastServicioImpl>();
            services.AddTransient<HrCapacitacionBeneficiarioServicio, HrCapacitacionBeneficiarioServicioImpl>();
            services.AddTransient<HrCapacitacionEmpleadoServicio, HrCapacitacionEmpleadoServicioImpl>();
            services.AddTransient<RtIndicadorMetaServicio, RtIndicadorMetaServicioImpl>();
            /** CORE - FIN **/


            /** PLANILLA - INICIO **/
            services.AddTransient<PrTipoplanillaDao, PrTipoplanillaDaoImpl>();
            services.AddTransient<PrTipoprestamoDao, PrTipoprestamoDaoImpl>();
            services.AddTransient<PrTipoProcesoDao, PrTipoProcesoDaoImpl>();
            services.AddTransient<PrCalendarioferiadosDao, PrCalendarioferiadosDaoImpl>();
            services.AddTransient<PrTipoplanillaServicio, PrTipoplanillaServicioImpl>();
            services.AddTransient<PrTipoProcesoServicio, PrTipoProcesoServicioImpl>();
            services.AddTransient<PrTipoprestamoServicio, PrTipoprestamoServicioImpl>();
            services.AddTransient<PrCalendarioferiadosServicio, PrCalendarioferiadosServicioImpl>();


            /** PLANILLA - FIN **/

            /** PROCESO - INICIO **/
            services.AddTransient<BpAuditoriaDao, BpAuditoriaDaoImpl>();
            services.AddTransient<BpEnlaceDao, BpEnlaceDaoImpl>();
            services.AddTransient<BpMaeprocesoestadoDao, BpMaeprocesoestadoDaoImpl>();
            services.AddTransient<BpTransaccionseguimientoDao, BpTransaccionseguimientoDaoImpl>();
            services.AddTransient<BpProcesoconexionDao, BpProcesoconexionDaoImpl>();
            services.AddTransient<BpMaeprocesoelementoDao, BpMaeprocesoelementoDaoImpl>();
            services.AddTransient<BpMaeprocesoDao, BpMaeprocesoDaoImpl>();
            services.AddTransient<BpMaeprocesorolDao, BpMaeprocesorolDaoImpl>();
            services.AddTransient<BpProcesoDao, BpProcesoDaoImpl>();
            services.AddTransient<BpMaeprocesoelementoconfiguracionDao, BpMaeprocesoelementoconfiguracionDaoImpl>();
            services.AddTransient<BpMaeprocesorolusuarioDao, BpMaeprocesorolusuarioDaoImpl>();
            services.AddTransient<BpProcesoconexioncomunicacionDao, BpProcesoconexioncomunicacionDaoImpl>();
            services.AddTransient<BpTransaccionrequerimientoDao, BpTransaccionrequerimientoDaoImpl>();
            services.AddTransient<BpTransaccionelementoDao, BpTransaccionelementoDaoImpl>();
            services.AddTransient<BpProcesorequerimientoDao, BpProcesorequerimientoDaoImpl>();
            services.AddTransient<BpTransaccionDao, BpTransaccionDaoImpl>();
            services.AddTransient<BpMaeeventoDao, BpMaeeventoDaoImpl>();

            /** ERNESTO**/
            services.AddTransient<BpMaeprocesofuncionalidadDao, BpMaeprocesofuncionalidadDaoImpl>();
            services.AddTransient<BpTipoelementoDao, BpTipoelementoDaoImpl>();
            services.AddTransient<BpColorDao, BpColorDaoImpl>();
            services.AddTransient<BpMaerequerimientoDao, BpMaerequerimientoDaoImpl>();
            services.AddTransient<BpProcesoconexioneventoDao, BpProcesoconexioneventoDaoImpl>();
            /** ERNESTO **/

            services.AddTransient<BpEnlaceServicio, BpEnlaceServicioImpl>();
            services.AddTransient<BpAuditoriaServicio, BpAuditoriaServicioImpl>();
            services.AddTransient<BpMaeprocesoestadoServicio, BpMaeprocesoestadoServicioImpl>();
            services.AddTransient<BpTransaccionseguimientoServicio, BpTransaccionseguimientoServicioImpl>();
            services.AddTransient<BpProcesoconexionServicio, BpProcesoconexionServicioImpl>();
            services.AddTransient<BpMaeprocesoelementoServicio, BpMaeprocesoelementoServicioImpl>();
            services.AddTransient<BpMaeprocesoServicio, BpMaeprocesoServicioImpl>();
            services.AddTransient<BpMaeprocesorolServicio, BpMaeprocesorolServicioImpl>();
            services.AddTransient<BpStatemachineServicio, BpStatemachineServicioImpl>();
            services.AddTransient<BpProcesoServicio, BpProcesoServicioImpl>();
            services.AddTransient<BpMaeprocesoelementoconfiguracionServicio, BpMaeprocesoelementoconfiguracionServicioImpl>();
            services.AddTransient<BpMaeprocesorolusuarioServicio, BpMaeprocesorolusuarioServicioImpl>();
            services.AddTransient<BpProcesoconexioncomunicacionServicio, BpProcesoconexioncomunicacionServicioImpl>();
            services.AddTransient<BpTransaccionrequerimientoServicio, BpTransaccionrequerimientoServicioImpl>();
            services.AddTransient<BpTransaccionelementoServicio, BpTransaccionelementoServicioImpl>();
            services.AddTransient<BpProcesorequerimientoServicio, BpProcesorequerimientoServicioImpl>();
            services.AddTransient<BpTransaccionServicio, BpTransaccionServicioImpl>();

            /** ERNESTO**/
            services.AddTransient<BpMaeprocesofuncionalidadServicio, BpMaeprocesofuncionalidadServicioImpl>();
            services.AddTransient<BpTipoelementoServicio, BpTipoelementoServicioImpl>();
            services.AddTransient<BpColorServicio, BpColorServicioImpl>();
            services.AddTransient<BpMaerequerimientoServicio, BpMaerequerimientoServicioImpl>();
            services.AddTransient<BpProcesoconexioneventoServicio, BpProcesoconexioneventoServicioImpl>();

            /** ERNESTO **/


            /** PROCESO - FIN **/


            /** RRHH - INICIO **/
            services.AddTransient<HrDepartamentoDao, HrDepartamentoDaoImpl>();
            services.AddTransient<HrEmpleadoDao, HrEmpleadoDaoImpl>();
            services.AddTransient<HrTipocontratoDao, HrTipocontratoDaoImpl>();
            services.AddTransient<HrUnidadoperativaDao, HrUnidadoperativaDaoImpl>();

            services.AddTransient<HrEmpleadocapacitacionDao, HrEmpleadocapacitacionDaoImpl>();
            services.AddTransient<HrEmpleadocaphorarioDao, HrEmpleadocaphorarioDaoImpl>();


            services.AddTransient<HrEncuestaDao, HrEncuestaDaoImpl>();
            services.AddTransient<HrEncuestadetalleDao, HrEncuestadetalleDaoImpl>();
            services.AddTransient<HrEncuestaplantillaDao, HrEncuestaplantillaDaoImpl>();
            services.AddTransient<HrEncuestaplantilladetalleDao, HrEncuestaplantilladetalleDaoImpl>();
            services.AddTransient<HrEncuestapreguntaDao, HrEncuestapreguntaDaoImpl>();
            services.AddTransient<HrEncuestapreguntavaloresDao, HrEncuestapreguntavaloresDaoImpl>();
            services.AddTransient<HrEncuestasujetoDao, HrEncuestasujetoDaoImpl>();
            services.AddTransient<HrCursodescripcionDao, HrCursodescripcionDaoImpl>();


            services.AddTransient<HrDepartamentoServicio, HrDepartamentoServicioImpl>();
            services.AddTransient<HrEmpleadoServicio, HrEmpleadoServicioImpl>();
            services.AddTransient<HrTipocontratoServicio, HrTipocontratoServicioImpl>();
            services.AddTransient<HrUnidadoperativaServicio, HrUnidadoperativaServicioImpl>();

            services.AddTransient<HrEncuestadetalleServicio, HrEncuestadetalleServicioImpl>();
            services.AddTransient<HrEncuestaplantilladetalleServicio, HrEncuestaplantilladetalleServicioImpl>();
            services.AddTransient<HrEncuestaplantillaServicio, HrEncuestaplantillaServicioImpl>();
            services.AddTransient<HrEncuestapreguntaServicio, HrEncuestapreguntaServicioImpl>();
            services.AddTransient<HrEncuestapreguntavaloresServicio, HrEncuestapreguntavaloresServicioImpl>();
            services.AddTransient<HrEncuestaServicio, HrEncuestaServicioImpl>();
            services.AddTransient<HrEncuestasujetoServicio, HrEncuestasujetoServicioImpl>();
            services.AddTransient<HrCursodescripcionServicio, HrCursodescripcionServicioImpl>();

            services.AddTransient<HrEmpleadocapacitacionServicio, HrEmpleadocapacitacionServicioImpl>();
            services.AddTransient<HrEmpleadocaphorarioServicio, HrEmpleadocaphorarioServicioImpl>();
            services.AddTransient<HrGradoinstruccionServicio, HrGradoinstruccionServicioImpl>();

            services.AddTransient<HrGradoinstruccionDao, HrGradoinstruccionDaoImpl>();

            services.AddTransient<HrCapacitacionDao, HrCapacitacionDaoImpl>();
            services.AddTransient<HrCapacitacionempcalDao, HrCapacitacionempcalDaoImpl>();
            services.AddTransient<HrCapacitacionevalDao, HrCapacitacionevalDaoImpl>();
            services.AddTransient<HrCapacitacionevaluacionDao, HrCapacitacionevaluacionDaoImpl>();
            services.AddTransient<HrCapacitacionplanDao, HrCapacitacionplanDaoImpl>();
            services.AddTransient<HrCapacitacionplandetDao, HrCapacitacionplandetDaoImpl>();
            services.AddTransient<HrCursodescripcionDao, HrCursodescripcionDaoImpl>();
            services.AddTransient<HrCentroestudiosDao, HrCentroestudiosDaoImpl>();

            services.AddTransient<HrCapacitacionServicio, HrCapacitacionServicioImpl>();
            services.AddTransient<HrCapacitacionempcalServicio, HrCapacitacionempcalServicioImpl>();
            services.AddTransient<HrCapacitacionevalServicio, HrCapacitacionevalServicioImpl>();
            services.AddTransient<HrCapacitacionevaluacionServicio, HrCapacitacionevaluacionServicioImpl>();
            services.AddTransient<HrCapacitacionplanServicio, HrCapacitacionplanServicioImpl>();
            services.AddTransient<HrCapacitacionplandetServicio, HrCapacitacionplandetServicioImpl>();
            services.AddTransient<HrCursodescripcionServicio, HrCursodescripcionServicioImpl>();
            services.AddTransient<HrCentroestudiosServicio, HrCentroestudiosServicioImpl>();

            /** ERNESTO inicio**/
            services.AddTransient<HrPuestoempresaServicio, HrPuestoempresaServicioImpl>();
            services.AddTransient<PmPublicacionServicio, PmPublicacionServicioImpl>();


            services.AddTransient<HrPuestoempresaDao, HrPuestoempresaDaoImpl>();
            services.AddTransient<PmPublicacionDao, PmPublicacionDaoImpl>();
            /** ERNESTO fin**/

            /** RRHHH - FIN **/

            /** SISTEMA - INICIO **/
            services.AddTransient<AplicacionesmastDao, AplicacionesmastDaoImpl>();
            services.AddTransient<AprobacionRrhhDao, AprobacionRrhhDaoImpl>();
            services.AddTransient<ParametrosmastDao, ParametrosmastDaoImpl>();
            services.AddTransient<SeguridadautorizacioncompaniaDao, SeguridadautorizacioncompaniaDaoImpl>();
            services.AddTransient<SeguridadautorizacionesDao, SeguridadautorizacionesDaoImpl>();
            services.AddTransient<SeguridadautorizacionreporteDao, SeguridadautorizacionreporteDaoImpl>();
            services.AddTransient<SeguridadconceptoDao, SeguridadconceptoDaoImpl>();
            services.AddTransient<SeguridadgrupoDao, SeguridadgrupoDaoImpl>();
            services.AddTransient<SeguridadperfilusuarioDao, SeguridadperfilusuarioDaoImpl>();
            services.AddTransient<SyCorreoServicio, SyCorreoServicioImpl>();
            services.AddTransient<SyAprobacionnivelesDao, SyAprobacionnivelesDaoImpl>();
            services.AddTransient<SyAprobacionnivelesDetDao, SyAprobacionnivelesDetDaoImpl>();
            services.AddTransient<SyAprobacionprocesoDao, SyAprobacionprocesoDaoImpl>();
            services.AddTransient<SyAprobacionprocesoValoresDao, SyAprobacionprocesoValoresDaoImpl>();
            services.AddTransient<SyPlantillaDao, SyPlantillaDaoImpl>();
            services.AddTransient<SyReporteDao, SyReporteDaoImpl>();
            services.AddTransient<SyReportearchivoDao, SyReportearchivoDaoImpl>();
            services.AddTransient<SySeguridadautorizacionesDao, SySeguridadautorizacionesDaoImpl>();
            services.AddTransient<SyUnidadreplicacionDao, SyUnidadreplicacionDaoImpl>();
            services.AddTransient<SyUsuariopasswordlogDao, SyUsuariopasswordlogDaoImpl>();
            services.AddTransient<UsuarioDao, UsuarioDaoImpl>();
            services.AddTransient<UbicaciongeograficaDao, UbicaciongeograficaDaoImpl>();
            services.AddTransient<SyPreferencesDao, SyPreferencesDaoImpl>();

            services.AddTransient<AplicacionesmastServicio, AplicacionesmastServicioImpl>();
            services.AddTransient<AprobacionRrhhServicio, AprobacionRrhhServicioImpl>();
            services.AddTransient<ParametrosmastServicio, ParametrosmastServicioImpl>();
            services.AddTransient<SeguridadautorizacioncompaniaServicio, SeguridadautorizacioncompaniaServicioImpl>();
            services.AddTransient<SeguridadautorizacionesServicio, SeguridadautorizacionesServicioImpl>();
            services.AddTransient<SeguridadautorizacionreporteServicio, SeguridadautorizacionreporteServicioImpl>();
            services.AddTransient<SeguridadconceptoServicio, SeguridadconceptoServicioImpl>();
            services.AddTransient<SeguridadgrupoServicio, SeguridadgrupoServicioImpl>();
            services.AddTransient<SeguridadperfilusuarioServicio, SeguridadperfilusuarioServicioImpl>();
            services.AddTransient<SyAprobacionnivelesServicio, SyAprobacionnivelesServicioImpl>();
            services.AddTransient<SyAprobacionnivelesDetServicio, SyAprobacionnivelesDetServicioImpl>();
            services.AddTransient<SyAprobacionprocesoServicio, SyAprobacionprocesoServicioImpl>();
            services.AddTransient<SyAprobacionprocesoValoresServicio, SyAprobacionprocesoValoresServicioImpl>();
            services.AddTransient<SyPlantillaServicio, SyPlantillaServicioImpl>();
            services.AddTransient<SyReporteServicio, SyReporteServicioImpl>();
            services.AddTransient<SyReportearchivoServicio, SyReportearchivoServicioImpl>();
            services.AddTransient<SySeguridadautorizacionesServicio, SySeguridadautorizacionesServicioImpl>();
            services.AddTransient<SyUnidadreplicacionServicio, SyUnidadreplicacionServicioImpl>();
            services.AddTransient<SyUsuariopasswordlogServicio, SyUsuariopasswordlogServicioImpl>();
            services.AddTransient<UsuarioServicio, UsuarioServicioImpl>();
            services.AddTransient<UbicaciongeograficaServicio, UbicaciongeograficaServicioImpl>();
            services.AddTransient<SyPreferencesServicio, SyPreferencesServicioImpl>();
            /** SISTEMA - FIN **/

            /** PROGRAMA SOCIAL - INICIO **/
            services.AddTransient<PsBeneficiarioDao, PsBeneficiarioDaoImpl>();
            services.AddTransient<PsInstitucionAreaTrabajoDao, PsInstitucionAreaTrabajoDaoImpl>();
            services.AddTransient<PsComponenteDao, PsComponenteDaoImpl>();
            services.AddTransient<PsEmpleadoDao, PsEmpleadoDaoImpl>();
            services.AddTransient<PsEntidadDao, PsEntidadDaoImpl>();
            services.AddTransient<PsInstitucionDao, PsInstitucionDaoImpl>();
            services.AddTransient<PsProgramaDao, PsProgramaDaoImpl>();
            services.AddTransient<PsNutricionDao, PsNutricionDaoImpl>();
            services.AddTransient<PsAtencionDao, PsAtencionDaoImpl>();
            services.AddTransient<PsAtencionDetalleDao, PsAtencionDetalleDaoImpl>();
            services.AddTransient<PsSocioAmbientalDao, PsSocioAmbientalDaoImpl>();
            services.AddTransient<PsConsumoNutricionalDao, PsConsumoNutricionalDaoImpl>();
            services.AddTransient<PsConsumoPlantillaDao, PsConsumoPlantillaDaoImpl>();
            services.AddTransient<PsConsumoPlantillaDetalleDao, PsConsumoPlantillaDetalleDaoImpl>();
            services.AddTransient<PsConsumoDao, PsConsumoDaoImpl>();
            services.AddTransient<PsCapacidadDao, PsCapacidadDaoImpl>();
            services.AddTransient<PsCapacidadCursoDao, PsCapacidadCursoDaoImpl>();
            services.AddTransient<PsCapacidadTallerDao, PsCapacidadTallerDaoImpl>();
            services.AddTransient<PsSaludDao, PsSaludDaoImpl>();
            services.AddTransient<PsSaludBiomecanicaDao, PsSaludBiomecanicaDaoImpl>();
            services.AddTransient<PsSaludEstadoDao, PsSaludEstadoDaoImpl>();
            services.AddTransient<PsSaludAyudaDao, PsSaludAyudaDaoImpl>();

            services.AddTransient<PsSaludExamenDao, PsSaludExamenDaoImpl>();
            services.AddTransient<PsSaludSubcondicionDao, PsSaludSubcondicionDaoImpl>();
            services.AddTransient<PsSaludTerapiaDao, PsSaludTerapiaDaoImpl>();
            services.AddTransient<PsSaludDiscapacidadDao, PsSaludDiscapacidadDaoImpl>();
            services.AddTransient<PsSaludDiagnosticoDao, PsSaludDiagnosticoDaoImpl>();
            services.AddTransient<PsSaludTratamientoDao, PsSaludTratamientoDaoImpl>();
            services.AddTransient<PsItemDao, PsItemDaoImpl>();
            services.AddTransient<PsInstitucionAreaDao, PsInstitucionAreaDaoImpl>();
            services.AddTransient<PsInstitucionperiodoDao, PsInstitucionperiodoDaoImpl>();
            services.AddTransient<PsAtencionTratamientoDao, PsAtencionTratamientoDaoImpl>();
            services.AddTransient<PsBeneficiarioIngresoDao, PsBeneficiarioIngresoDaoImpl>();
            services.AddTransient<PsBeneficiarioIngresoCausalDao, PsBeneficiarioIngresoCausalDaoImpl>();
            services.AddTransient<PsEntidadDocumentoDao, PsEntidadDocumentoDaoImpl>();
            services.AddTransient<PsEntidadEquipamientoDao, PsEntidadEquipamientoDaoImpl>();
            services.AddTransient<PsEntidadParienteDao, PsEntidadParienteDaoImpl>();
            services.AddTransient<PsEntidadProgramaSocialDao, PsEntidadProgramaSocialDaoImpl>();
            services.AddTransient<PsEntidadSeguroSocialDao, PsEntidadSeguroSocialDaoImpl>();
            services.AddTransient<PsBeneficiarioIngresoDiagnosticoDao, PsBeneficiarioIngresoDiagnosticoDaoImpl>();
            services.AddTransient<PsMarcologicoDao, PsMarcologicoDaoImpl>();

            services.AddTransient<PsInstitucionAreaTrabajoServicio, PsInstitucionAreaTrabajoServicioImpl>();
            services.AddTransient<PsBeneficiarioServicio, PsBeneficiarioServicioImpl>();
            services.AddTransient<PsComponenteServicio, PsComponenteServicioImpl>();
            services.AddTransient<PsEmpleadoServicio, PsEmpleadoServicioImpl>();
            services.AddTransient<PsEntidadServicio, PsEntidadServicioImpl>();
            services.AddTransient<PsInstitucionServicio, PsInstitucionServicioImpl>();
            services.AddTransient<PsProgramaServicio, PsProgramaServicioImpl>();
            services.AddTransient<PsFurhServicio, PsFurhServicioImpl>();
            services.AddTransient<PsNutricionServicio, PsNutricionServicioImpl>();
            services.AddTransient<PsAtencionServicio, PsAtencionServicioImpl>();
            services.AddTransient<PsAtencionDetalleServicio, PsAtencionDetalleServicioImpl>();
            services.AddTransient<PsSocioAmbientalServicio, PsSocioAmbientalServicioImpl>();
            services.AddTransient<PsConsumoNutricionalServicio, PsConsumoNutricionalServicioImpl>();
            services.AddTransient<PsConsumoPlantillaServicio, PsConsumoPlantillaServicioImpl>();
            services.AddTransient<PsConsumoServicio, PsConsumoServicioImpl>();
            services.AddTransient<PsSaludServicio, PsSaludServicioImpl>();
            services.AddTransient<PsSaludBiomecanicaServicio, PsSaludBiomecanicaServicioImpl>();
            services.AddTransient<PsSaludEstadoServicio, PsSaludEstadoServicioImpl>();
            services.AddTransient<PsSaludExamenServicio, PsSaludExamenServicioImpl>();
            services.AddTransient<PsSaludSubcondicionServicio, PsSaludSubcondicionServicioImpl>();
            services.AddTransient<PsSaludTerapiaServicio, PsSaludTerapiaServicioImpl>();
            services.AddTransient<PsSaludDiscapacidadServicio, PsSaludDiscapacidadServicioImpl>();

            services.AddTransient<PsSaludTratamientoServicio, PsSaludTratamientoServicioImpl>();
            services.AddTransient<PsSaludDiagnosticoServicio, PsSaludDiagnosticoServicioImpl>();

            services.AddTransient<PsItemServicio, PsItemServicioImpl>();
            services.AddTransient<PsInstitucionAreaServicio, PsInstitucionAreaServicioImpl>();
            services.AddTransient<PsInstitucionperiodoServicio, PsInstitucionperiodoServicioImpl>();
            services.AddTransient<PsAtencionTratamientoServicio, PsAtencionTratamientoServicioImpl>();
            services.AddTransient<PsCapacidadServicio, PsCapacidadServicioImpl>();
            services.AddTransient<PsCapacidadCursoServicio, PsCapacidadCursoServicioImpl>();
            services.AddTransient<PsCapacidadTallerServicio, PsCapacidadTallerServicioImpl>();
            services.AddTransient<PsBeneficiarioIngresoServicio, PsBeneficiarioIngresoServicioImpl>();
            services.AddTransient<PsBeneficiarioIngresoCausalServicio, PsBeneficiarioIngresoCausalServicioImpl>();
            services.AddTransient<PsEntidadDocumentoServicio, PsEntidadDocumentoServicioImpl>();
            services.AddTransient<PsEntidadEquipamientoServicio, PsEntidadEquipamientoServicioImpl>();
            services.AddTransient<PsEntidadParienteServicio, PsEntidadParienteServicioImpl>();
            services.AddTransient<PsEntidadProgramaSocialServicio, PsEntidadProgramaSocialServicioImpl>();
            services.AddTransient<PsEntidadSeguroSocialServicio, PsEntidadSeguroSocialServicioImpl>();
            services.AddTransient<PsBeneficiarioIngresoDiagnosticoServicio, PsBeneficiarioIngresoDiagnosticoServicioImpl>();
            services.AddTransient<PsMarcologicoServicio, PsMarcologicoServicioImpl>();

            /** PROGRAMA SOCIAL - FIN **/

            /** INDICADOR - INICIO **/
            services.AddTransient<RtIndicadorDao, RtIndicadorDaoImpl>();

            services.AddTransient<RtIndicadorServicio, RtIndicadorServicioImpl>();
            /** INDICADOR - FIN **/

            /** SALUD - INICIO **/

            services.AddTransient<SsGediagnosticoDao, SsGediagnosticoDaoImpl>();
            services.AddTransient<SsCie10capituloDao, SsCie10capituloDaoImpl>();
            services.AddTransient<SsCie10grupoDao, SsCie10grupoDaoImpl>();
            services.AddTransient<SsCie10subgrupoDao, SsCie10subgrupoDaoImpl>();

            services.AddTransient<SsGediagnosticoServicio, SsGediagnosticoServicioImpl>();
            services.AddTransient<SsCie10capituloServicio, SsCie10capituloServicioImpl>();
            services.AddTransient<SsCie10grupoServicio, SsCie10grupoServicioImpl>();
            services.AddTransient<SsCie10subgrupoServicio, SsCie10subgrupoServicioImpl>();
            /** SALUD - FIN **/

            /** PROYECTO - INICIO **/
            services.AddTransient<PmTipoproyectoDao, PmTipoproyectoDaoImpl>();
            services.AddTransient<PmTipoproyectoServicio, PmTipoproyectoServicioImpl>();

            services.AddTransient<PmProyectoDao, PmProyectoDaoImpl>();
            services.AddTransient<PmProyectoServicio, PmProyectoServicioImpl>();

            services.AddTransient<PmNotificacionDao, PmNotificacionDaoImpl>();
            services.AddTransient<PmNotificacionServicio, PmNotificacionServicioImpl>();

            services.AddTransient<PmNotificacionpersonaDao, PmNotificacionpersonaDaoImpl>();
            services.AddTransient<PmNotificacionpersonaServicio, PmNotificacionpersonaServicioImpl>();

            services.AddTransient<PmProgramaDao, PmProgramaDaoImpl>();
            services.AddTransient<PmProgramaServicio, PmProgramaServicioImpl>();

            services.AddTransient<PmPortafolioDao, PmPortafolioDaoImpl>();
            services.AddTransient<PmPortafolioServicio, PmPortafolioServicioImpl>();

            services.AddTransient<PmTareaDao, PmTareaDaoImpl>();
            services.AddTransient<PmTareaServicio, PmTareaServicioImpl>();
            services.AddTransient<PmPlantillaTareasDao, PmPlantillaTareasDaoImpl>();
            services.AddTransient<PmPlantillaDao, PmPlantillaDaoImpl>();
            services.AddTransient<PmPlantillaTareasServicio, PmPlantillaTareasServicioImpl>();
            services.AddTransient<PmPlantillaServicio, PmPlantillaServicioImpl>();

            services.AddTransient<HrCapacitacionFoliosDao, HrCapacitacionFoliosDaoImpl>();
            /** PROYECTO - FIN **/


            /**EVENTOS**/
            services.AddTransient<BpEjecutarServicio, PmPlanAnularEjecutar>();
            services.AddTransient<BpEjecutarServicio, PmPlanAprobarEjecutar>();
            services.AddTransient<BpEjecutarServicio, PmTareaCompletadaEjecutar>();
            services.AddTransient<BpEjecutarServicio, PmTareaIperCompletadaEjecutar>();
            services.AddTransient<BpEjecutarServicio, PmTareaRegistrarReaprobarPlanEjecutar>();

            services.AddTransient<BpValidarServicio, PmPlanAnularValidar>();
            services.AddTransient<BpValidarServicio, PmPlanFinalizarValidar>();
            services.AddTransient<BpValidarServicio, PmPlanValidar>();
            services.AddTransient<BpValidarServicio, PmTareaRegistroFinalizarValidar>();
            /**EVENTOS**/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.StartupTimeout = new TimeSpan(0, 900, 0);
                spa.Options.SourcePath = "SpringNg";

                if (env.IsDevelopment()) {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}

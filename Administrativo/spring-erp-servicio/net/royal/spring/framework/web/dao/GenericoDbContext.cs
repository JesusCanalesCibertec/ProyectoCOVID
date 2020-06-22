using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using net.royal.spring.sistema.dominio;
using net.royal.spring.core.dominio;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.planilla.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.rrhh.dominio;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.salud.dominio;
using net.royal.spring.dominio;
using net.royal.spring.minedu.dominio;
using net.royal.spring.covid.dominio;

namespace net.royal.spring.framework.web.dao {
    public class GenericoDbContext : DbContext {


        /** COVID - INICIO **/

        public DbSet<Ciudadano> Ciudadano { get; set; }
        public DbSet<Triaje> Triaje { get; set; }

        /** COVID - FIN **/

        /** MINEDU - INICIO **/

        public DbSet<MpPersona> MpPersona { get; set; }
        public DbSet<MpPersonaconocimiento> MpPersonaconocimiento { get; set; }
        public DbSet<MpConocimiento> MpConocimiento { get; set; }
        public DbSet<MpContratacion> MpContratacion { get; set; }
        public DbSet<MpPersonatitulo> MpPersonatitulo { get; set; }
        public DbSet<MpContratacionadendaentregable> MpContratacionadendaentregable { get; set; }
        public DbSet<MpProyecto> MpProyecto { get; set; }
        public DbSet<MpProyectorecurso> MpProyectorecurso { get; set; }
        public DbSet<MpProyectorecursoperiodo> MpProyectorecursoperiodo { get; set; }
        public DbSet<MpAreaminedu> MpAreaminedu { get; set; }
        /** MINEDU - FIN **/


        /** ASISTENCIA - INICIO **/
        public DbSet<AsAccesosdiarios> AsAccesosdiarios { get; set; }
        public DbSet<AsArea> AsArea { get; set; }
        public DbSet<AsAsistenciadiaria> AsAsistenciadiaria { get; set; }
        public DbSet<AsAsistenciadiariamarca> AsAsistenciadiariamarca { get; set; }
        public DbSet<AsAutorizacion> AsAutorizacion { get; set; }
        public DbSet<AsAutorizacionDocAprobacion> AsAutorizacionDocAprobacion { get; set; }
        public DbSet<AsConceptoacceso> AsConceptoacceso { get; set; }
        public DbSet<AsConceptoaccesosistema> AsConceptoaccesosistema { get; set; }
        public DbSet<AsPeriodo> AsPeriodo { get; set; }
        public DbSet<AsTipohorario> AsTipohorario { get; set; }
        public DbSet<AsConceptoaccesoRegla> AsConceptoaccesoRegla { get; set; }
        /** ASISTENCIA - FIN **/

        /** CONTABILIDAD - INICIO **/
        public DbSet<AcCostcentermst> AcCostcentermst { get; set; }
        public DbSet<AcSucursal> AcSucursal { get; set; }
        public DbSet<AcSucursalgrupo> AcSucursalgrupo { get; set; }
        /** CONTABILIDAD - FIN **/


        /** CORE - INICIO **/
        public DbSet<Accountmst> Accountmst { get; set; }
        public DbSet<Afemst> Afemst { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Companiamast> Companiamast { get; set; }
        public DbSet<Companyowner> Companyowner { get; set; }
        public DbSet<CompanyownerImagenes> CompanyownerImagenes { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Departmentmst> Departmentmst { get; set; }
        public DbSet<Empleadomast> Empleadomast { get; set; }
        public DbSet<MaCts> MaCts { get; set; }
        public DbSet<MaMiscelaneosdetalle> MaMiscelaneosdetalle { get; set; }
        public DbSet<MaMiscelaneosheader> MaMiscelaneosheader { get; set; }
        public DbSet<MaUnidadnegocio> MaUnidadnegocio { get; set; }
        public DbSet<Monedamast> Monedamast { get; set; }
        public DbSet<Ocupacionmast> Ocupacionmast { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Personamast> Personamast { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Tipocambiomast> Tipocambiomast { get; set; }
        public DbSet<Zonapostal> Zonapostal { get; set; }
        public DbSet<Companyownerrecurso> Companyownerrecurso { get; set; }
        public DbSet<Unidadesmast> Unidadesmast { get; set; }
        /** CORE - FIN **/


        /** PLANILLA - INICIO **/
        public DbSet<PrTipoplanilla> PrTipoplanilla { get; set; }
        public DbSet<PrTipoProceso> PrTipoProceso { get; set; }
        public DbSet<PrTipoprestamo> PrTipoprestamo { get; set; }
        public DbSet<PrCalendarioferiados> PrCalendarioferiados { get; set; }

        /** PLANILLA - FIN **/

        /** PROCESO - INICIO **/
        public DbSet<BpAuditoria> BpAuditoria { get; set; }
        public DbSet<BpEnlace> BpEnlace { get; set; }
        public DbSet<BpMaeprocesoestado> BpMaeprocesoestado { get; set; }
        public DbSet<BpTransaccionseguimiento> BpTransaccionseguimiento { get; set; }
        public DbSet<BpProcesoconexion> BpProcesoconexion { get; set; }
        public DbSet<BpMaeprocesoelemento> BpMaeprocesoelemento { get; set; }
        public DbSet<BpMaeproceso> BpMaeproceso { get; set; }
        public DbSet<BpMaeprocesorol> BpMaeprocesorol { get; set; }
        public DbSet<BpTransaccion> BpTransaccion { get; set; }
        public DbSet<BpProceso> BpProceso { get; set; }
        public DbSet<BpMaeprocesoelementoconfiguracion> BpMaeprocesoelementoconfiguracion { get; set; }
        public DbSet<BpMaeprocesorolusuario> BpMaeprocesorolusuario { get; set; }
        public DbSet<BpProcesoconexioncomunicacion> BpProcesoconexioncomunicacion { get; set; }
        public DbSet<BpTransaccionrequerimiento> BpTransaccionrequerimiento { get; set; }
        public DbSet<BpTransaccionelemento> BpTransaccionelemento { get; set; }
        public DbSet<BpProcesorequerimiento> BpProcesorequerimiento { get; set; }

        /**ERNESTO**/
        public DbSet<BpMaeprocesofuncionalidad> BpMaeprocesofuncionalidad { get; set; }
        public DbSet<BpTipoelemento> BpTipoelemento { get; set; }
        public DbSet<BpColor> BpColor { get; set; }
        public DbSet<BpMaerequerimiento> BpMaerequerimiento { get; set; }

        public DbSet<BpProcesoconexionevento> BpProcesoconexionevento { get; set; }
        /**ERNESTO **/

        /** PROCESO - FIN **/


        /** RRHH - INICIO **/
        public DbSet<HrEncuesta> HrEncuesta { get; set; }
        public DbSet<HrEncuestadetalle> HrEncuestadetalle { get; set; }
        public DbSet<HrEncuestaplantilla> HrEncuestaplantilla { get; set; }
        public DbSet<HrEncuestaplantilladetalle> HrEncuestaplantilladetalle { get; set; }
        public DbSet<HrEncuestapregunta> HrEncuestapregunta { get; set; }
        public DbSet<HrEncuestapreguntavalores> HrEncuestapreguntavalores { get; set; }
        public DbSet<HrEncuestasujeto> HrEncuestasujeto { get; set; }

        public DbSet<HrCapacitacion> HrCapacitacion { get; set; }
        public DbSet<HrCapacitacionFolios> HrCapacitacionFolios { get; set; }
        public DbSet<HrCapacitacionempcal> HrCapacitacionempcal { get; set; }
        public DbSet<HrCapacitacioneval> HrCapacitacioneval { get; set; }
        public DbSet<HrCapacitacionevaluacion> HrCapacitacionevaluacion { get; set; }
        public DbSet<HrCapacitacionplan> HrCapacitacionplan { get; set; }
        public DbSet<HrCapacitacionplandet> HrCapacitacionplandet { get; set; }
        public DbSet<HrCursodescripcion> HrCursodescripcion { get; set; }
        public DbSet<HrCapacitacionBeneficiario> HrCapacitacionBeneficiario { get; set; }
        public DbSet<HrGradoinstruccion> HrGradoinstruccion { get; set; }
        public DbSet<HrCapacitacionEmpleado> HrCapacitacionEmpleado { get; set; }
        public DbSet<PsInstitucion> HrInstitucion { get; set; }

        /**ERNESTO inicio**/
        public DbSet<HrPuestoempresa> HrPuestoempresa { get; set; }
        public DbSet<PmPublicacion> PmPublicacion { get; set; }

        /**ERNESTO fin**/

        /** RRHH - FIN **/

        /** SISTEMA - INICIO **/
        public DbSet<Aplicacionesmast> Aplicacionesmast { get; set; }
        public DbSet<AprobacionRrhh> AprobacionRrhh { get; set; }
        public DbSet<Parametrosmast> Parametrosmast { get; set; }
        public DbSet<Seguridadautorizacioncompania> Seguridadautorizacioncompania { get; set; }
        public DbSet<Seguridadautorizaciones> Seguridadautorizaciones { get; set; }
        public DbSet<Seguridadautorizacionreporte> Seguridadautorizacionreporte { get; set; }
        public DbSet<Seguridadconcepto> Seguridadconcepto { get; set; }
        public DbSet<Seguridadgrupo> Seguridadgrupo { get; set; }
        public DbSet<Seguridadperfilusuario> Seguridadperfilusuario { get; set; }
        public DbSet<SyAprobacionniveles> SyAprobacionniveles { get; set; }
        public DbSet<SyAprobacionnivelesDet> SyAprobacionnivelesDet { get; set; }
        public DbSet<SyAprobacionproceso> SyAprobacionproceso { get; set; }
        public DbSet<SyAprobacionprocesoValores> SyAprobacionprocesoValores { get; set; }
        public DbSet<SyPlantilla> SyPlantilla { get; set; }
        public DbSet<SyReporte> SyReporte { get; set; }
        public DbSet<SySeguridadautorizaciones> SySeguridadautorizaciones { get; set; }
        public DbSet<SyUnidadreplicacion> SyUnidadreplicacion { get; set; }
        public DbSet<SyUsuariopasswordlog> SyUsuariopasswordlog { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<SyReporteArchivo> SyReportearchivo { get; set; }
        public DbSet<RtIndicadorMeta> RtIndicadorMeta { get; set; }
        public DbSet<SyPreferences> SyPreferences { get; set; }
        /** SISTEMA - FIN **/

        /** PROGRAMA SOCIAL - INICIO **/
        public DbSet<PsInstitucionAreaTrabajo> PsInstitucionAreaTrabajo { get; set; }
        public DbSet<PsBeneficiario> PsBeneficiario { get; set; }
        public DbSet<PsComponente> PsComponente { get; set; }
        public DbSet<PsEmpleado> PsEmpleado { get; set; }
        public DbSet<PsEntidad> PsEntidad { get; set; }
        public DbSet<PsSocioAmbiental> PsSocioAmbiental { get; set; }
        public DbSet<PsPrograma> PsPrograma { get; set; }
        public DbSet<PsPrograma> PsNutricion { get; set; }
        public DbSet<PsAtencion> PsAtencion { get; set; }
        public DbSet<PsAtencionDetalle> PsAtencionDetalle { get; set; }
        public DbSet<PsAtencionTratamiento> PsAtencionTratamiento { get; set; }

        /** ERNESTO - INICIO **/
        public DbSet<PsInstitucion> PsInstitucion { get; set; }
        public DbSet<PsConsumoNutricional> PsConsumoNutricional { get; set; }
        public DbSet<PsConsumoPlantilla> PsConsumoPlantilla { get; set; }
        public DbSet<PsConsumoPlantillaDetalle> PsConsumoPlantillaDetalle { get; set; }
        public DbSet<PsConsumo> PsConsumo { get; set; }
        public DbSet<PsItem> PsItem { get; set; }
        public DbSet<PsInstitucionArea> PsInstitucionArea { get; set; }
        public DbSet<PsInstitucionperiodo> PsInstitucionperiodo { get; set; }

        public DbSet<PsMarcologico> PsMarcologico { get; set; }

        /** ERNESTO - FIN **/
        public DbSet<PsCapacidad> PsCapacidad { get; set; }
        public DbSet<PsCapacidadCurso> PsCapacidadCurso { get; set; }
        public DbSet<PsCapacidadTaller> PsCapacidadTaller { get; set; }
        public DbSet<PsSalud> PsSalud { get; set; }
        public DbSet<PsSaludBiomecanica> PsSaludBiomecanica { get; set; }
        public DbSet<PsSaludEstado> PsSaludEstado { get; set; }
        public DbSet<PsSaludAyuda> PsSaludAyuda { get; set; }

        public DbSet<PsSaludExamen> PsSaludExamen { get; set; }
        public DbSet<PsSaludSubcondicion> PsSaludSubcondicion { get; set; }
        public DbSet<PsSaludTerapia> PsSaludTerapia { get; set; }
        public DbSet<PsSaludDiscapacidad> PsSaludDiscapacidad { get; set; }
        public DbSet<PsSaludTratamiento> PsSaludTratamiento { get; set; }
        public DbSet<PsSaludDiagnostico> PsSaludDiagnostico { get; set; }
        public DbSet<PsBeneficiarioIngreso> PsBeneficiarioIngreso { get; set; }
        public DbSet<PsBeneficiarioIngresoCausal> PsBeneficiarioIngresoCausal { get; set; }
        public DbSet<PsEntidadDocumento> PsEntidadDocumento { get; set; }
        public DbSet<PsEntidadEquipamiento> PsEntidadEquipamiento { get; set; }
        public DbSet<PsEntidadPariente> PsEntidadPariente { get; set; }
        public DbSet<PsEntidadProgramaSocial> PsEntidadProgramaSocial { get; set; }
        public DbSet<PsEntidadSeguroSocial> PsEntidadSeguroSocial { get; set; }
        public DbSet<PsBeneficiarioIngresoDiagnostico> PsBeneficiarioIngresoDiagnostico { get; set; }

        /** ERNESTO - FIN **/

        /** PROGRAMA SOCIAL - FIN **/

        /** INDICADOR - INICIO **/
        public DbSet<RtIndicador> RtIndicador { get; set; }
        /** INDICADOR - FIN **/

        /** PROYECTO - INICIO **/
        public DbSet<PmTipoproyecto> PmTipoproyecto { get; set; }
        public DbSet<PmNotificacion> PmNotificacion { get; set; }
        public DbSet<PmNotificacionpersona> PmNotificacionpersona { get; set; }
        public DbSet<PmPrograma> PmPrograma { get; set; }
        public DbSet<PmProyecto> PmProyecto { get; set; }
        public DbSet<PmPortafolio> PmPortafolio { get; set; }
        public DbSet<PmTarea> PmTarea { get; set; }

        public DbSet<PmPlantilla> PmPlantilla { get; set; }

        public DbSet<PmPlantillaTareas> PmPlantillaTareas { get; set; }


        public DbSet<Ubicaciongeografica> Ubicaciongeografica { get; set; }
        /** PROYECTO - FIN **/

        /** SALUD - INICIO **/
        public DbSet<SsGediagnostico> SsGediagnostico { get; set; }
        public DbSet<SsCie10capitulo> SsCie10capitulo { get; set; }
        public DbSet<SsCie10grupo> SsCie10grupo { get; set; }
        public DbSet<SsCie10subgrupo> SsCie10subgrupo { get; set; }
        /** SALUD - FIN **/

        public GenericoDbContext(DbContextOptions<GenericoDbContext> options)
        : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {


            /** COVID - INICIO **/
            modelBuilder.Entity<Ciudadano>().HasKey(c => new { c.IdCiudadano });
            modelBuilder.Entity<Triaje>().HasKey(c => new { c.IdTriaje });

            /** COVID - FIN **/


            /** MINEDU - INICIO **/
            modelBuilder.Entity<MpPersona>().HasKey(c => new { c.IdPersona });
            modelBuilder.Entity<MpConocimiento>().HasKey(c => new { c.IdConocimiento });
            modelBuilder.Entity<MpPersonaconocimiento>().HasKey(c => new { c.IdPersona , c.IdConocimiento});
            modelBuilder.Entity<MpContratacion>().HasKey(c => new { c.IdContratacion });
            modelBuilder.Entity<MpPersonatitulo>().HasKey(c => new { c.IdPersona, c.IdCarrera, c.IdCentro, c.IdNivel });
            modelBuilder.Entity<MpContratacionadendaentregable>().HasKey(c => new { c.IdContratacion, c.IdCodigo });
            modelBuilder.Entity<MpProyecto>().HasKey(c => new { c.IdProyecto });
            modelBuilder.Entity<MpProyectorecurso>().HasKey(c => new { c.IdProyecto, c.IdRecurso });
            modelBuilder.Entity<MpProyectorecursoperiodo>().HasKey(c => new { c.IdProyecto, c.IdPersona, c.IdPeriodo });
            modelBuilder.Entity<MpAreaminedu>().HasKey(c => new { c.Sedeid });
            /** MINEDU - FIN **/

            /** ASISTENCIA - INICIO **/
            modelBuilder.Entity<AsAccesosdiarios>()
            .HasKey(c => new { c.Carnetid, c.Fecha, c.Secuencia });
            modelBuilder.Entity<AsArea>()
            .HasKey(c => new { c.Area });
            modelBuilder.Entity<AsAsistenciadiaria>()
            .HasKey(c => new { c.Empleado, c.Fecha, c.Secuencia });
            modelBuilder.Entity<AsAsistenciadiariamarca>()
            .HasKey(c => new { c.Empleado, c.Fecha, c.Hora, c.Secuencia });

            modelBuilder.Entity<AsAutorizacion>(entity => {
                //entity.HasKey(c => new { c.Empleado, c.Fecha, c.Conceptoacceso, c.Desde });
                entity.HasKey(c => new { c.Numeroproceso });
                entity.Property(e => e.Numeroproceso)
                         .HasColumnName("NUMEROPROCESO")
                      .ValueGeneratedOnAdd();

                //.ValueGeneratedOnAdd().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
                /*entity.Property(e => e.Autogenerado)
                     .HasColumnName("AUTOGENERADO")
                     .ValueGeneratedOnAdd().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;*/
            });
            modelBuilder.Entity<AsAutorizacionDocAprobacion>()
            .HasKey(c => new { c.NumeroProceso, c.Secuencial });

            modelBuilder.Entity<AsConceptoacceso>()
            .HasKey(c => new { c.Conceptoacceso });
            modelBuilder.Entity<AsConceptoaccesosistema>()
            .HasKey(c => new { c.Conceptosistema });
            modelBuilder.Entity<AsPeriodo>()
            .HasKey(c => new { c.Secuencia });
            modelBuilder.Entity<AsTipohorario>()
            .HasKey(c => new { c.Tipohorario });
            modelBuilder.Entity<AsConceptoaccesoRegla>()
            .HasKey(c => new { c.Conceptoacceso, c.Regla });
            /** ASISTENCIA - FIN **/

            /** CONTABILIDAD - INICIO **/
            modelBuilder.Entity<AcCostcentermst>()
            .HasKey(c => new { c.Costcenter });

            modelBuilder.Entity<AcSucursal>()
            .HasKey(c => new { c.Sucursal });

            modelBuilder.Entity<AcSucursalgrupo>()
            .HasKey(c => new { c.Sucursalgrupo });
            /** CONTABILIDAD - FIN **/


            /** CORE - INICIO **/
            modelBuilder.Entity<Accountmst>()
            .HasKey(c => new { c.Account });

            modelBuilder.Entity<Afemst>()
            .HasKey(c => new { c.Afe });

            modelBuilder.Entity<Banco>()
            .HasKey(c => new { c.Banco });

            modelBuilder.Entity<Companiamast>()
            .HasKey(c => new { c.Companiacodigo });

            modelBuilder.Entity<Companyowner>()
            .HasKey(c => new { c.Companyowner });

            modelBuilder.Entity<CompanyownerImagenes>()
            .HasKey(c => new { c.Companyowner });

            modelBuilder.Entity<Departamento>()
            .HasKey(c => new { c.Departamento });

            modelBuilder.Entity<Departmentmst>()
            .HasKey(c => new { c.Department });

            modelBuilder.Entity<Empleadomast>()
            .HasKey(c => new { c.Companiasocio, c.Empleado });

            modelBuilder.Entity<MaCts>()
            .HasKey(c => new { c.Numerocts });

            modelBuilder.Entity<MaMiscelaneosdetalle>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Codigotabla, c.Compania, c.Codigoelemento });

            modelBuilder.Entity<MaMiscelaneosheader>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Codigotabla, c.Compania });

            modelBuilder.Entity<MaUnidadnegocio>()
            .HasKey(c => new { c.Unidadnegocio });

            modelBuilder.Entity<Monedamast>()
            .HasKey(c => new { c.Monedacodigo });

            modelBuilder.Entity<Ocupacionmast>()
            .HasKey(c => new { c.Ocupacion });

            modelBuilder.Entity<Pais>()
            .HasKey(c => new { c.IdPais });

            modelBuilder.Entity<Personamast>()
            .HasKey(c => new { c.Persona });

            modelBuilder.Entity<Provincia>()
            .HasKey(c => new { c.Departamento, c.Provincia });

            modelBuilder.Entity<Tipocambiomast>()
            .HasKey(c => new { c.Monedacodigo, c.Monedacambiocodigo, c.Fechacambio });

            modelBuilder.Entity<Zonapostal>()
            .HasKey(c => new { c.Departamento, c.Provincia, c.Codigopostal });

            modelBuilder.Entity<Companyownerrecurso>()
            .HasKey(c => new { c.Companyowner, c.Tiporecurso, c.Periodo });

            modelBuilder.Entity<Unidadesmast>()
            .HasKey(c => new { c.Unidadcodigo });

            modelBuilder.Entity<Ubicaciongeografica>()
            .HasKey(c => new { c.Ubigeo });


            /** CORE - FIN **/


            /** PLANILLA - INICIO **/
            modelBuilder.Entity<PrTipoplanilla>()
            .HasKey(c => new { c.Tipoplanilla });

            modelBuilder.Entity<PrTipoprestamo>()
            .HasKey(c => new { c.Tipoprestamo });

            modelBuilder.Entity<PrTipoProceso>()
            .HasKey(c => new { c.TipoProceso });

            modelBuilder.Entity<PrCalendarioferiados>()
            .HasKey(c => new { c.FechaMesDia });

            modelBuilder.Entity<RtIndicadorMeta>()
        .HasKey(c => new { c.IdIndicador, c.IdMeta });

            /** PLANILLA - FIN **/

            /** PROCESO - INICIO **/
            modelBuilder.Entity<BpAuditoria>()
            .HasKey(c => new { c.IdProceso, c.IdFuncionalidad, c.IdPersona, c.IdSecuencia });

            modelBuilder.Entity<BpEnlace>()
           .HasKey(c => new { c.IdEnlace, c.IdCorreo });

            modelBuilder.Entity<BpMaeprocesoestado>()
          .HasKey(c => new { c.IdProceso, c.IdEstado });

            modelBuilder.Entity<BpTransaccionseguimiento>()
         .HasKey(c => new { c.IdTransaccion, c.IdSeguimiento });

            modelBuilder.Entity<BpProcesoconexion>()
        .HasKey(c => new { c.IdProceso, c.IdVersion, c.IdConexion });

            modelBuilder.Entity<BpMaeprocesoelemento>()
       .HasKey(c => new { c.IdProceso, c.IdElemento });
            modelBuilder.Entity<BpMaeproceso>()
       .HasKey(c => new { c.IdProceso });
            modelBuilder.Entity<BpMaeprocesorol>()
       .HasKey(c => new { c.IdProceso, c.IdRol });

            modelBuilder.Entity<BpTransaccion>()
       .HasKey(c => new { c.IdTransaccion });

            modelBuilder.Entity<BpProceso>()
      .HasKey(c => new { c.IdProceso, c.IdVersion });

            modelBuilder.Entity<BpMaeprocesoelementoconfiguracion>()
      .HasKey(c => new { c.IdProceso, c.IdElemento, c.IdConfiguracion });
            modelBuilder.Entity<BpMaeprocesorolusuario>()
      .HasKey(c => new { c.IdProceso, c.IdRol, c.IdTipoSeguridad, c.IdConcepto });
            modelBuilder.Entity<BpProcesoconexioncomunicacion>()
       .HasKey(c => new { c.IdProceso, c.IdVersion, c.IdConexion, c.IdUsuario });
            modelBuilder.Entity<BpTransaccionrequerimiento>()
       .HasKey(c => new { c.IdTransaccion, c.IdRequerimiento });
            modelBuilder.Entity<BpTransaccionelemento>()
      .HasKey(c => new {
          c.IdTransaccion,
          c.ActualIdProceso,
          c.ActualIdElemento,
          c.SiguienteIdProceso,
          c.SiguienteIdElemento
      });

            modelBuilder.Entity<BpProcesorequerimiento>()
      .HasKey(c => new { c.IdProceso, c.IdVersion, c.IdRequerimiento });

            /**ERNESTO**/
            modelBuilder.Entity<BpMaeprocesofuncionalidad>()
       .HasKey(c => new { c.IdProceso, c.IdFuncionalidad });
            modelBuilder.Entity<BpTipoelemento>()
      .HasKey(c => new { c.IdClaseelemento, c.IdTipoelemento });
            modelBuilder.Entity<BpColor>()
      .HasKey(c => new { c.IdColor });
            modelBuilder.Entity<BpMaerequerimiento>()
     .HasKey(c => new { c.IdRequerimiento });
            modelBuilder.Entity<BpProcesoconexionevento>()
     .HasKey(c => new { c.IdProceso, c.IdVersion, c.IdConexion, c.IdEvento });
            /**ERNESTO**/

            /** PROCESO - FIN **/

            /** RRHH - INICIO **/
            modelBuilder.Entity<HrEncuesta>()
            .HasKey(c => new { c.Companyowner, c.Periodo, c.Secuencia });
            modelBuilder.Entity<HrEncuestadetalle>()
            .HasKey(c => new { c.Companyowner, c.Periodo, c.Secuencia, c.Pregunta });
            modelBuilder.Entity<HrEncuestaplantilla>()
            .HasKey(c => new { c.Plantilla });
            modelBuilder.Entity<HrEncuestaplantilladetalle>()
            .HasKey(c => new { c.Plantilla, c.Pregunta });
            modelBuilder.Entity<HrEncuestapregunta>()
            .HasKey(c => new { c.Pregunta });
            modelBuilder.Entity<HrEncuestapreguntavalores>()
            .HasKey(c => new { c.Pregunta, c.Valor });
            modelBuilder.Entity<HrEncuestasujeto>()
            .HasKey(c => new { c.Secuencia, c.Sujeto, c.Pregunta, c.Companyowner, c.Periodo });

            modelBuilder.Entity<HrCapacitacion>(
                entity => {
                    entity.HasKey(c => new { c.Companyowner, c.Capacitacion });
                    entity.Property(e => e.Numeroproceso)
                        .HasColumnName("NUMEROPROCESO")
                     .ValueGeneratedOnAdd().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
                });


            modelBuilder.Entity<PmPlantilla>(
              entity => {
                  entity.HasKey(c => new { c.Plantilla });

              });

            modelBuilder.Entity<PmPlantillaTareas>()
           .HasKey(c => new { c.Plantilla, c.Secuencia });
            modelBuilder.Entity<HrCapacitacionempcal>()
            .HasKey(c => new { c.Companyowner, c.Capacitacion, c.Empleado, c.Evaluacion });
            modelBuilder.Entity<HrCapacitacionFolios>()
            .HasKey(c => new { c.Companyowner, c.Capacitacion, c.Secuencia });
            modelBuilder.Entity<HrCapacitacioneval>()
            .HasKey(c => new { c.Companyowner, c.Capacitacion, c.Evaluacion });
            modelBuilder.Entity<HrCapacitacionevaluacion>()
            .HasKey(c => new { c.Capacitacion, c.Secuenciaempleado, c.Factor });
            modelBuilder.Entity<HrCapacitacionplan>()
            .HasKey(c => new { c.Companyowner, c.Anioplan });
            modelBuilder.Entity<HrCapacitacionplandet>()
            .HasKey(c => new { c.Companyowner, c.Anioplan, c.Capacitacion });
            modelBuilder.Entity<HrGradoinstruccion>()
                     .HasKey(c => new { c.Gradoinstruccion });
            modelBuilder.Entity<HrCapacitacionBeneficiario>()
                        .HasKey(c => new { c.Companyowner, c.Capacitacion, c.IdInstitucion, c.IdBeneficiario });
            modelBuilder.Entity<HrCapacitacionEmpleado>()
            .HasKey(c => new { c.Companyowner, c.Capacitacion, c.IdInstitucion, c.IdEmpleado });
            modelBuilder.Entity<HrCentroestudios>()
            .HasKey(c => new { c.Centro });
            modelBuilder.Entity<HrCursodescripcion>()
            .HasKey(c => new { c.Curso });
            modelBuilder.Entity<PsInstitucion>()
           .HasKey(c => new { c.IdInstitucion });
            modelBuilder.Entity<HrEmpleadoasistencias>()
            .HasKey(c => new { c.Companyowner, c.Capacitacion, c.Empleado, c.Secuencia });
            modelBuilder.Entity<HrEmpleadocapacitacion>()
            .HasKey(c => new { c.Companyowner, c.Capacitacion, c.Empleado });
            modelBuilder.Entity<HrEmpleadocaphorario>()
            .HasKey(c => new { c.Companyowner, c.Capacitacion, c.Empleado, c.Secuencia });


            /**ERNESTO**/
            modelBuilder.Entity<HrPuestoempresa>()
            .HasKey(c => new { c.CodigoPuesto });

            modelBuilder.Entity<PmPublicacion>()
            .HasKey(c => new { c.IdAplicacion, c.IdPubicacion });

            /**ERNESTO**/

            /** RRHH - FIN **/



            /** SISTEMA - INICIO **/
            modelBuilder.Entity<Aplicacionesmast>()
            .HasKey(c => new { c.Aplicacioncodigo });

            modelBuilder.Entity<AprobacionRrhh>()
            .HasKey(c => new { c.Aplicacion, c.Proceso, c.Procesointerno, c.Companiasocio, c.Codigoproceso, c.Numeroproceso, c.Nivel, c.Estadonivel, c.Usuarioaprobador });

            modelBuilder.Entity<Parametrosmast>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Parametroclave, c.Companiacodigo });

            modelBuilder.Entity<Seguridadautorizacioncompania>()
            .HasKey(c => new { c.Companyowner, c.Usuario });

            modelBuilder.Entity<Seguridadautorizaciones>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Grupo, c.Concepto, c.Usuario });

            modelBuilder.Entity<Seguridadautorizacionreporte>()
            .HasKey(c => new { c.Usuario, c.Aplicacioncodigo, c.Reportecodigo });

            modelBuilder.Entity<Seguridadconcepto>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Grupo, c.Concepto });

            modelBuilder.Entity<Seguridadgrupo>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Grupo });

            modelBuilder.Entity<Seguridadperfilusuario>()
            .HasKey(c => new { c.Perfil, c.Usuario });

            modelBuilder.Entity<SyAprobacionniveles>()
            .HasKey(c => new { c.Codigo, c.CompanyOwner });

            modelBuilder.Entity<SyAprobacionnivelesDet>()
            .HasKey(c => new { c.Codigo, c.Nivel, c.Usuario, c.CompanyOwner });

            modelBuilder.Entity<SyAprobacionproceso>()
            .HasKey(c => new { c.Aplicacion, c.Codigoproceso });

            modelBuilder.Entity<SyAprobacionprocesoValores>()
            .HasKey(c => new { c.Codigoproceso, c.Valor });

            modelBuilder.Entity<SyPlantilla>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Plantillacodigo });

            modelBuilder.Entity<SyReporte>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Reportecodigo });

            modelBuilder.Entity<SySeguridadautorizaciones>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Grupo, c.Concepto, c.Usuario });

            modelBuilder.Entity<SyUnidadreplicacion>()
            .HasKey(c => new { c.Unidadreplicacion });

            modelBuilder.Entity<SyUsuariopasswordlog>()
            .HasKey(c => new { c.Usuario, c.Secuencia });

            modelBuilder.Entity<Usuario>()
            .HasKey(c => new { c.Usuario });

            modelBuilder.Entity<SyReporteArchivo>()
            .HasKey(c => new { c.Aplicacioncodigo, c.Reportecodigo, c.Companiasocio, c.Periodo, c.Version });
            modelBuilder.Entity<SyPreferences>()
       .HasKey(c => new { c.Usuario, c.Preference });

            /** SISTEMA - FIN **/

            /** PROGRAMA SOCIAL - INICIO **/
            modelBuilder.Entity<PsBeneficiario>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario });
            modelBuilder.Entity<PsInstitucionAreaTrabajo>()
            .HasKey(c => new { c.IdInstitucion,c.IdArea });
            modelBuilder.Entity<PsComponente>()
            .HasKey(c => new { c.IdComponente });
            modelBuilder.Entity<PsEmpleado>()
            .HasKey(c => new { c.IdInstitucion, c.IdEmpleado });
            modelBuilder.Entity<PsEntidad>()
            .HasKey(c => new { c.IdEntidad });
            modelBuilder.Entity<PsInstitucion>()
            .HasKey(c => new { c.IdInstitucion });
            modelBuilder.Entity<PsPrograma>()
            .HasKey(c => new { c.IdPrograma });
            modelBuilder.Entity<PsNutricion>()
          .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdNutricion });
            modelBuilder.Entity<PsAtencion>()
        .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdAtencion });
            modelBuilder.Entity<PsAtencionDetalle>()
            .HasKey(c => new { c.IdAtencion, c.IdDiagnostico });
            modelBuilder.Entity<PsAtencionTratamiento>()
          .HasKey(c => new { c.IdAtencion, c.IdDiagnostico, c.IdTratamiento });

            modelBuilder.Entity<PsSocioAmbiental>()
       .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSocioAmbiental });
            modelBuilder.Entity<PsConsumoNutricional>()
.HasKey(c => new { c.IdInstitucion, c.IdConsumo, c.IdConsumoNutricional });
            modelBuilder.Entity<PsConsumoPlantilla>()
            .HasKey(c => new { c.IdInstitucion, c.Plantilla });
            modelBuilder.Entity<PsConsumoPlantillaDetalle>()
           .HasKey(c => new { c.IdInstitucion, c.Plantilla, c.IdItem });
            modelBuilder.Entity<PsConsumo>()
       .HasKey(c => new { c.IdInstitucion, c.IdConsumo });


            modelBuilder.Entity<PsItem>()
            .HasKey(c => new { c.IdItem });
            modelBuilder.Entity<PsInstitucionArea>()
            .HasKey(c => new { c.IdInstitucion, c.IdArea });
            modelBuilder.Entity<PsInstitucionperiodo>()
           .HasKey(c => new { c.IdInstitucion, c.IdAplicacion, c.IdGrupo, c.IdConcepto,c.IdPeriodo });


            modelBuilder.Entity<PsCapacidad>()
         .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdCapacidad });
            modelBuilder.Entity<PsCapacidadCurso>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdCapacidad, c.IdCurso });
            modelBuilder.Entity<PsCapacidadTaller>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdCapacidad, c.IdTaller });
            modelBuilder.Entity<PsSalud>()
        .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud });
            modelBuilder.Entity<PsSaludBiomecanica>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdTipoAyuda });
            modelBuilder.Entity<PsSaludEstado>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdEstado });
            modelBuilder.Entity<PsSaludAyuda>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdAyuda });
            modelBuilder.Entity<PsSaludDiscapacidad>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdDiscapacidad });
            modelBuilder.Entity<PsSaludExamen>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdExamen });
            modelBuilder.Entity<PsSaludSubcondicion>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdCondicion, c.IdSubcondicion });
            modelBuilder.Entity<PsSaludTerapia>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdTerapia });

            modelBuilder.Entity<PsSaludTratamiento>()
         .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdTratamiento });

            modelBuilder.Entity<PsSaludDiagnostico>()
         .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdSalud, c.IdDiagnostico });

            modelBuilder.Entity<PsBeneficiarioIngreso>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdIngreso });
            modelBuilder.Entity<PsBeneficiarioIngresoCausal>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdIngreso, c.IdCausal });
            modelBuilder.Entity<PsEntidadDocumento>()
            .HasKey(c => new { c.IdEntidad, c.IdTipoDocumento });
            modelBuilder.Entity<PsEntidadEquipamiento>()
            .HasKey(c => new { c.IdEntidad, c.IdEquipamiento });
            modelBuilder.Entity<PsEntidadPariente>()
            .HasKey(c => new { c.IdEntidad, c.IdPariente });
            modelBuilder.Entity<PsEntidadProgramaSocial>()
            .HasKey(c => new { c.IdEntidad, c.IdProgramaSocial });
            modelBuilder.Entity<PsEntidadSeguroSocial>()
            .HasKey(c => new { c.IdEntidad, c.IdSeguroSocial });
            modelBuilder.Entity<PsBeneficiarioIngresoDiagnostico>()
            .HasKey(c => new { c.IdInstitucion, c.IdBeneficiario, c.IdIngreso, c.IdDiagnostico });
            modelBuilder.Entity<PsMarcologico>()
           .HasKey(c => new { c.IdMarcologico });
            /** PROGRAMA SOCIAL - FIN **/

            /** KPI - INICIO **/
            modelBuilder.Entity<RtIndicador>()
            .HasKey(c => new { c.IdIndicador });
            /** KPI - FIN **/

            /** PROYECTO - INICIO **/
            modelBuilder.Entity<PmTipoproyecto>()
            .HasKey(c => new { c.IdTipoProyecto });
            modelBuilder.Entity<PmNotificacion>()
           .HasKey(c => new { c.IdNotificacion });
            modelBuilder.Entity<PmNotificacionpersona>()
           .HasKey(c => new { c.IdNotificacion, c.IdPersona });
            modelBuilder.Entity<PmPrograma>()
           .HasKey(c => new { c.IdPortafolio, c.IdPrograma });
            modelBuilder.Entity<PmProyecto>()
        .HasKey(c => new { c.IdPortafolio, c.IdPrograma, c.IdProyecto });
            modelBuilder.Entity<PmPortafolio>()
       .HasKey(c => new { c.IdPortafolio });
            modelBuilder.Entity<PmTarea>()
      .HasKey(c => new { c.IdProyecto, c.IdTarea });
            /** PROYECTO - FIN **/

            /** SALUD - INICIO **/
            modelBuilder.Entity<SsGediagnostico>()
           .HasKey(c => new { c.IdDiagnostico });
            modelBuilder.Entity<SsCie10capitulo>()
          .HasKey(c => new { c.IdCapitulo });
            modelBuilder.Entity<SsCie10grupo>()
          .HasKey(c => new { c.IdCapitulo, c.IdGrupo });
            modelBuilder.Entity<SsCie10subgrupo>()
          .HasKey(c => new { c.IdCapitulo, c.IdGrupo, c.IdSubgrupo });
            /** SALUD - FIN **/
        }
    }
}
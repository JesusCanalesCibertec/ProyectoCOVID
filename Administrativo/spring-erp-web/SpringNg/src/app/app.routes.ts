import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { DashboardDemoComponent } from './demo/view/dashboarddemo.component';
import { SampleDemoComponent } from './demo/view/sampledemo.component';
import { FormsDemoComponent } from './demo/view/formsdemo.component';
import { DataDemoComponent } from './demo/view/datademo.component';
import { PanelsDemoComponent } from './demo/view/panelsdemo.component';
import { OverlaysDemoComponent } from './demo/view/overlaysdemo.component';
import { MenusDemoComponent } from './demo/view/menusdemo.component';
import { MessagesDemoComponent } from './demo/view/messagesdemo.component';
import { MiscDemoComponent } from './demo/view/miscdemo.component';
import { EmptyDemoComponent } from './demo/view/emptydemo.component';
import { ChartsDemoComponent } from './demo/view/chartsdemo.component';
import { FileDemoComponent } from './demo/view/filedemo.component';
import { DocumentationComponent } from './demo/view/documentation.component';
import { LayoutComponent } from './base_module/components/layout.component';
import { PsFurhListadoComponent } from './erp_module/programasocial/psfurh/vista/psfurh-listado.component';
import { PsFurhMantenimientoComponent } from './erp_module/programasocial/psfurh/vista/psfurh-mantenimiento.component';
import { PsItemlistadoComponent } from './erp_module/programasocial/item/vista/item-listado.component';
import { PsInstitucionlistadoComponent } from './erp_module/programasocial/institucion/vista/institucion-listado.component';
import { PsInstitucionperiodolistadoComponent } from './erp_module/programasocial/institucion_periodo/vista/institucionperiodo-listado.component';
import { RasMantenimientoComponent } from './erp_module/programasocial/psatencionessalud/vista/ras-mantenimiento.component'; import { AuthGuard } from './base_module/guard/AuthGuard';
import { PsConsumolistadoComponent } from './erp_module/programasocial/consumo/vista/consumo-listado.component';
import { PsConsumomantenimientoComponent } from './erp_module/programasocial/consumo/vista/consumo-mantenimiento.component';
import { PsCapacidadesListadoComponent } from './erp_module/programasocial/capacidades/vista/pscapacidades-listado.listadocomponent';
import { PsBeneficiariolistadoComponent } from './erp_module/programasocial/beneficiario/vista/beneficiario-listado.component';
import { PsBeneficiariomantenimientoComponent } from './erp_module/programasocial/beneficiario/vista/beneficiario-mantenimiento.component';
import { GraficosComponent } from './erp_module/programasocial/graficos/vista/graficos.component';
import { SyReporteListadoComponent } from './erp_module/sistemas/syreporte/vista/syreporte-listado.component';
import { SyReporteComponent } from './erp_module/sistemas/syreporte/vista/syreporte-general.component';
import { LoginFundacionComponent } from './login_module/components/login-fundacion.component';
import { RasConsultaComponent } from './erp_module/programasocial/psatencionessalud/vista/ras-consulta.component';
import { PsCapacidadesConsultaComponent } from './erp_module/programasocial/capacidades/vista/pscapacidades-consulta.listadocomponent';
import { PsConsumoPlantillamantenimientoComponent } from './erp_module/programasocial/consumoplantilla/vista/consumo-plantilla-mantenimiento.component';
import { PsConsumoPlantillalistadoComponent } from './erp_module/programasocial/consumoplantilla/vista/consumo-plantilla-listado.component';
import { PsReportesComponent } from './erp_module/programasocial/reportes/vista/ps_reportes.component';
import { PersonaListadoComponent } from './erp_module/minedu/persona/vista/persona-listado.component';
import { ConocimientoListadoComponent } from './erp_module/minedu/conocimiento/vista/conocimiento-listado.component';
import { ContratacionListadoComponent } from './erp_module/minedu/contratacion/vista/contratacion-listado.component';
import { ProyectoListadoComponent } from './erp_module/minedu/proyecto/vista/proyecto-listado.component';
import { ProyectoMantenimientoComponent } from './erp_module/minedu/proyecto/vista/proyecto-mantenimiento.component';
import { ResumenlistadoComponent } from './erp_module/minedu/proyecto/vista/resumen-listado.component';
import { OcupacionlistadoComponent } from './erp_module/minedu/persona/vista/ocupacion-listado.componente';
import { UsuarioListadoComponent } from './erp_module/minedu/usuario/vista/usuario-listado.component';
import { CiudadanoListadoComponent } from './erp_module/covid/ciudadano/vista/ciudadano-listado.component';
import { TriajeListadoComponent } from './erp_module/covid/triaje/vista/triaje-listado.component';
export const routes: Routes = [
    //{ path: '', component: LoginComponent },
    //{ path: 'spring/institucion-login', component: LoginInstitucionComponent },
    { path: '', component: LoginFundacionComponent },
    //{ path: '', component: DashboardDemoComponent },
    {
        path: 'spring', component: LayoutComponent,
        runGuardsAndResolvers: 'paramsOrQueryParamsChange',
        children: [

            //minedu inicio
            { path: 'ciudadano-listado', component: CiudadanoListadoComponent },
            { path: 'triaje-listado', component: TriajeListadoComponent },
            { path: 'triaje-listado/:codigo', component: TriajeListadoComponent },

            //minedu final
            
            //minedu inicio
            { path: 'persona-listado', component: PersonaListadoComponent },
            { path: 'conocimiento-listado', component: ConocimientoListadoComponent },
            { path: 'contratacion-listado', component: ContratacionListadoComponent },
            { path: 'proyecto-listado', component: ProyectoListadoComponent },
            { path: 'proyecto-listado/:tipo', component: ProyectoListadoComponent },
            { path: 'proyecto-mantenimiento', component: ProyectoMantenimientoComponent },
            { path: 'proyecto-mantenimiento/:codigo/:accion', component: ProyectoMantenimientoComponent },
            { path: 'resumen-listado', component: ResumenlistadoComponent },
            { path: 'ocupacion-listado', component: OcupacionlistadoComponent },
            { path: 'usuario-listado', component: UsuarioListadoComponent },
            //minedu final


            //{ path: '', component: DashboardDemoComponent },
            { path: 'dashboard', component: DashboardDemoComponent },
            { path: 'sample', component: SampleDemoComponent },
            { path: 'forms', component: FormsDemoComponent },
            { path: 'data', component: DataDemoComponent },
            { path: 'panels', component: PanelsDemoComponent },
            { path: 'overlays', component: OverlaysDemoComponent },
            { path: 'menus', component: MenusDemoComponent },
            { path: 'messages', component: MessagesDemoComponent },
            { path: 'misc', component: MiscDemoComponent },
            { path: 'empty', component: EmptyDemoComponent },
            { path: 'charts', component: ChartsDemoComponent },
            { path: 'file', component: FileDemoComponent },
            { path: 'documentation', component: DocumentationComponent },
            { path: 'institucion-listado', component: PsInstitucionlistadoComponent },
            { path: 'beneficiario-listado', component: PsBeneficiariolistadoComponent },
            { path: 'beneficiario-mantenimiento/:TIPO/:INSTITUCION/:BENEFICIARIO/:EDITAR', component: PsBeneficiariomantenimientoComponent },

            // INICIO ERNESTO
            { path: 'item-listado', component: PsItemlistadoComponent },
            { path: 'institucionperiodo-listado', component: PsInstitucionperiodolistadoComponent },
            { path: 'consumo-listado', component: PsConsumolistadoComponent },
            { path: 'consumo-mantenimiento/:codigo', component: PsConsumomantenimientoComponent },
            { path: 'consumo-mantenimiento/:codInstitucion/:codConsumo/:ver', component: PsConsumomantenimientoComponent },
            { path: 'consumo-plantilla-listado', component: PsConsumoPlantillalistadoComponent },
            { path: 'consumo-plantilla-mantenimiento', component: PsConsumoPlantillamantenimientoComponent },
            { path: 'consumo-plantilla-mantenimiento/:codigo/:institucion/:ver', component: PsConsumoPlantillamantenimientoComponent },
            {
                path: 'syreporte-listado',
                component: SyReporteListadoComponent,
                canActivate: [AuthGuard],
            },
            {
                path: 'syreporte-mantenimiento',
                component: SyReporteComponent,
                canActivate: [AuthGuard],
            },
            {
                path: 'syreporte-mantenimiento/:aplicacionCodigo/:reporteCodigo',
                component: SyReporteComponent,
                canActivate: [AuthGuard],
            },
            // FIN ERNESTO

            // INICIO ALEJANDRO



            // FIN ALEJANDRO

            { path: 'psfurh-listado', component: PsFurhListadoComponent },
            { path: 'psfurh-mantenimiento/:idEntidad/:idInstitucion/:accion', component: PsFurhMantenimientoComponent },
            { path: 'psfurh-mantenimiento', component: PsFurhMantenimientoComponent },
            { path: 'ras-mantenimiento', component: RasMantenimientoComponent },
            { path: 'capacidades-mantenimiento', component: PsCapacidadesListadoComponent },
            { path: 'graficos', component: GraficosComponent },
            { path: 'ras-consulta', component: RasConsultaComponent },
            { path: 'capacidades-consulta', component: PsCapacidadesConsultaComponent },
            { path: 'reportes', component: PsReportesComponent },

        ]
    },

];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' });

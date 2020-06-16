import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BlockUIModule, InputTextModule, DropdownModule, CalendarModule, CheckboxModule, ConfirmDialogModule, TabViewModule, FieldsetModule, DataTableModule, RadioButtonModule, InputMaskModule, KeyFilterModule, RatingModule } from 'primeng/primeng';
import { TableModule } from 'primeng/table';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { HttpClientModule } from '@angular/common/http';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { PsFurhMantenimientoComponent } from './psfurh/vista/psfurh-mantenimiento.component';
import { PsFurhListadoComponent } from './psfurh/vista/psfurh-listado.component';
import { PsFurhService } from './psfurh/servicio/psfurh.service';
import { SharedModule } from '../shared/shared.module';
import { PsItemlistadoComponent } from './item/vista/item-listado.component';
import { PsItemServicio } from './item/service/psItemServicio';
import { PsInstitucionAreaServicio } from './institucion_area/servicio/PsInstitucionAreaServicio';
import { PsInstitucionAreaListadoComponent } from './institucion_area/vista/InstitucionArea-listado..component';
import { PsInstitucionlistadoComponent } from './institucion/vista/institucion-listado.component';
import { PsInstitucionServicio } from './institucion/service/PsInstitucionServicio';
import { PsInstitucionperiodoServicio } from './institucion_periodo/service/PsInstitucionperiodoServicio';
import { PsInstitucionperiodolistadoComponent } from './institucion_periodo/vista/institucionperiodo-listado.component';
import { RasService } from './psatencionessalud/servicio/ras.servicio';
import { RasMantenimientoComponent } from './psatencionessalud/vista/ras-mantenimiento.component';
import { TratamientoComponent } from './psatencionessalud/vista/ras-tratamiento.component';
import { PsConsumolistadoComponent } from './consumo/vista/consumo-listado.component';
import { PsConsumomantenimientoComponent } from './consumo/vista/consumo-mantenimiento.component';
import { PsConsumoService } from './consumo/service/PsConsumoService';
import { PsConsumoNutricionalService } from './consumonutricional/service/PsConsumoService';
import { PsCapacidadesListadoComponent } from './capacidades/vista/pscapacidades-listado.listadocomponent';
import { PsCapacidadesService } from './capacidades/servicio/pscapacidades.service';
import { BarthelComponent } from './capacidades/vista/pscapacidad-barthel.component';
import { KatzComponent } from './capacidades/vista/pscapacidad-katz.component';
import { PsCapacidadComentarioComponent } from './capacidades/vista/pscapacidad-comentario.component';
import { PsBeneficiariolistadoComponent } from './beneficiario/vista/beneficiario-listado.component';
import { PsBeneficiarioServicio } from './beneficiario/service/psBeneficiarioServicio';
import { PsBeneficiariomantenimientoComponent } from './beneficiario/vista/beneficiario-mantenimiento.component';
import { EdadAnioPipe } from 'src/app/base_module/util/pipes/edad2.pipe';
import { BeneficiarioCapacidadesComponent } from './beneficiario/vista/beneficiario-capacidades.component';
import { PsBeneficiarioIngresoService } from './beneficiario_ingreso/service/PsBeneficiarioingresoService';
import { PsBeneficiarioingresoComponent } from './beneficiario_ingreso/vista/beneficiario-ingreso-mantenimiento.component';
import { PsBeneficiarioegresoComponent } from './beneficiario_ingreso/vista/beneficiario-egreso-mantenimiento.component';
import { EdadCompletaPipe } from 'src/app/base_module/util/pipes/edad.pipe';
import { GraficosComponent } from './graficos/vista/graficos.component';
import { GraficosService } from './graficos/servicio/graficos.service';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { TreeTableModule } from 'primeng/treetable';
import { PsBeneficiarioIngresoListadoComponent } from './beneficiario_ingreso/vista/beneficiario-ingreso-listado.component';
import { PsCapacidadesConsultaComponent } from './capacidades/vista/pscapacidades-consulta.listadocomponent';
import { RasConsultaComponent } from './psatencionessalud/vista/ras-consulta.component';
import { PsConsumoPlantillamantenimientoComponent } from './consumoplantilla/vista/consumo-plantilla-mantenimiento.component';
import { PsConsumoPlantillalistadoComponent } from './consumoplantilla/vista/consumo-plantilla-listado.component';
import { ConsumoPlantillaSelectorComponent } from './consumoplantilla/vista/seelctor/consumo-plantilla-selector.component';
import { PsConsumoPlantillaService } from './consumoplantilla/service/PsConsumoPlantillaService';
import { PsInstitucionAreaTrabajoServicio } from './psintitucionareatrabajo/servicio/psinstitucionareatrabajo.service';
import { InstitucionPeriodoCopiarComponent } from './institucion_periodo/vista/institucionperiodo-copiar.component';
import { PsReportesComponent } from './reportes/vista/ps_reportes.component';
import { PsReporteServicio } from './reportes/servicio/ps_reporteservicio';

@NgModule({
    declarations: [
        PsFurhMantenimientoComponent,
        PsFurhListadoComponent,
        TratamientoComponent,
        PsCapacidadesListadoComponent,
        KatzComponent,
        BarthelComponent,
        PsCapacidadComentarioComponent,
        BeneficiarioCapacidadesComponent,

        // INICIO ERNESTO//
        PsItemlistadoComponent,
        PsInstitucionAreaListadoComponent,
        PsInstitucionlistadoComponent,
        PsInstitucionperiodolistadoComponent,
        PsConsumolistadoComponent,
        PsConsumomantenimientoComponent,
        PsBeneficiarioingresoComponent,
        PsBeneficiarioegresoComponent,
        PsBeneficiarioIngresoListadoComponent,
        PsConsumoPlantillalistadoComponent,
        PsConsumoPlantillamantenimientoComponent,
        ConsumoPlantillaSelectorComponent,
        // FIN ERNESTO //
        PsBeneficiariolistadoComponent,
        PsBeneficiariomantenimientoComponent,
        EdadAnioPipe,
        EdadCompletaPipe,
        GraficosComponent,
        PsCapacidadesConsultaComponent,
        RasConsultaComponent,
        InstitucionPeriodoCopiarComponent,
        PsReportesComponent,
        RasMantenimientoComponent
    ],
    imports: [
        RatingModule,
        SharedModule,
        NgxChartsModule,
        CommonModule,
        FormsModule,
        TableModule,
        PanelModule,
        BlockUIModule,
        InputTextModule,
        ButtonModule,
        DropdownModule,
        CalendarModule,
        CheckboxModule,
        ConfirmDialogModule,
        TabViewModule,
        FieldsetModule,
        DialogModule,
        DataTableModule,
        RadioButtonModule,
        InputMaskModule,
        HttpClientModule,
        KeyFilterModule,
        CurrencyMaskModule,
        TreeTableModule,
    ],
    exports: [
    ],
    providers: [
        PsFurhService,
        RasService,
        PsCapacidadesService,


        // INICIO ERNESTO //
        PsItemServicio,
        PsInstitucionAreaServicio,
        PsInstitucionServicio,
        PsInstitucionperiodoServicio,
        PsConsumoService,
        PsConsumoPlantillaService,
        PsConsumoNutricionalService,
        PsBeneficiarioIngresoService,
        // FIN ERNESTO //
        PsBeneficiarioServicio,
        GraficosService,
        PsInstitucionAreaTrabajoServicio,
        PsReporteServicio
    ],
})
export class ProgramaSocialModule { }

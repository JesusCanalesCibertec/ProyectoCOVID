import { NgModule } from '@angular/core';
import { CommonModule, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { SyAprobacionprocesoServicio } from './aprobaciones/servicio/SyAprobacionprocesoServicio';
import { AprobacionesListadoComponent } from './aprobaciones/vista/aprobaciones-listado.component';
import { AuthGuard } from '../../base_module/guard/AuthGuard';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { TableModule } from 'primeng/table';
import { PanelModule } from 'primeng/panel';
import { GrowlModule } from 'primeng/growl';
import { BlockUIModule } from 'primeng/blockui';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { FieldsetModule } from 'primeng/fieldset';
import { InputTextModule } from 'primeng/inputtext';
import { SpinnerModule } from 'primeng/spinner';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { DataTableModule, InputMaskModule, FileUploadModule } from 'primeng/primeng';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { TabViewModule } from 'primeng/tabview';
import { AprobacionNivelesMantenimientoComponent } from './aprobacionniveles/vista/aprobacionniveles-mantenimiento.component';
import { AprobacionNivelesComponent } from './aprobacionniveles/vista/aprobacionniveles-listado.component';
import { SyAprobacionnivelesServicio } from './aprobacionniveles/servicio/SyAprobacionnivelesServicio';
import { CorreoMantenimientoComponent } from './aprobacionniveles/vista/correo-mantenimiento.component';
import { BpTransaccionSeguimientoService } from './bptransaccionseguimiento/servicio/bptransaccionseguimiento.service';
import { CompaniaDetalleMantenimientoComponent } from './companiarecursos/vista/companiarecursos_detalle_mantenimiento.component';
import { CompaniaDetalleComponent } from './companiarecursos/vista/companiarecursos_detalle.component';
import { CompaniaRecursosGeneralComponent } from './companiarecursos/vista/companiarecursos_general.component';
import { CompaniaRecursosListadoComponent } from './companiarecursos/vista/companiarecursos_listado.component';
import { CompaniaRecursosMantenimientoComponent } from './companiarecursos/vista/companiarecursos_mantenimiento.component';
import { CompaniaRecursosService } from './companiarecursos/servicio/companiarecursos.service';
import { SyReporteDetalleMantenimientoComponent } from './syreporte/vista/syreporte-detalle-mantenimiento.component';
import { SyReporteArchivoDatoComponent } from './syreporte/vista/syreporte-archivo.component';
import { SyReporteComponent } from './syreporte/vista/syreporte-general.component';
import { SyReporteListadoComponent } from './syreporte/vista/syreporte-listado.component';
import { SyReporteService } from './syreporte/servicio/syreporte.service';
import { SyReporteArchivoService } from './syreportearchivo/servicio/sysreportearchivo.service';
import { SyReporteMantenimientoComponent } from './syreporte/vista/syreporte-mantenimiento.component';
import { AplicacionesmastServicio } from './aplicaciones/servicio/aplicacionesmast.service';
import { SyReporteDetalleComponent } from './syreporte/vista/syreporte-detalle.component';
import { HttpClientModule } from '@angular/common/http';
import { AngularEditorModule } from '@kolkov/angular-editor';

@NgModule({
    declarations: [

        AprobacionesListadoComponent,
        AprobacionNivelesComponent,
        AprobacionNivelesMantenimientoComponent,
        CorreoMantenimientoComponent,

        CompaniaDetalleMantenimientoComponent,
        CompaniaDetalleComponent,
        CompaniaRecursosGeneralComponent,
        CompaniaRecursosListadoComponent,
        CompaniaRecursosMantenimientoComponent,

        SyReporteDetalleMantenimientoComponent,
        SyReporteArchivoDatoComponent,
        SyReporteMantenimientoComponent,
        SyReporteComponent,
        SyReporteListadoComponent,
        SyReporteDetalleComponent

    ],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        TableModule, PanelModule, GrowlModule, BlockUIModule,
        InputTextModule, ButtonModule, DropdownModule,
        SpinnerModule, CalendarModule, CheckboxModule,
        ConfirmDialogModule, TabViewModule, FieldsetModule,
        DialogModule,
        DataTableModule,
        RadioButtonModule,
        InputMaskModule, FileUploadModule,
        HttpClientModule,
        AngularEditorModule

    ],
    exports: [],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        AuthGuard,
        SyAprobacionnivelesServicio,
        SyAprobacionprocesoServicio,
        BpTransaccionSeguimientoService,
        CompaniaRecursosService,
        SyReporteService,
        SyReporteArchivoService,
        AplicacionesmastServicio
    ],
})
export class SistemasModule { }

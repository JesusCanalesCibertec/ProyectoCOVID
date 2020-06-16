import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaMiscelaneosdetalleServicio } from './miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { EmpleadomastServicio } from './selectores/empleado/servicio/EmpleadomastServicio';
import { CompanyownerServicio } from './companyowner/servicio/CompanyownerServicio';
import { AprobacionPreguntasComponent } from './util/AprobacionComponent';

import { DialogModule } from 'primeng/dialog';
import { InputTextareaModule, DropdownModule, InputMaskModule, Button } from 'primeng/primeng';
import { FormsModule } from '@angular/forms';
import { PanelModule } from 'primeng/panel';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { GrowlModule } from 'primeng/growl';
import { InputTextModule } from 'primeng/inputtext';
import { BlockUIModule } from 'primeng/blockui';
import { SpinnerModule } from 'primeng/spinner';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { TabViewModule } from 'primeng/tabview';
import { FieldsetModule } from 'primeng/fieldset';
import { DataTableModule } from 'primeng/primeng';
import { PersonamastService } from './persona/servicio/personamast.service';
import { EmpleadomastSelectorComponent } from './selectores/empleado/vista/empleadomastselector.component';
import { AcSucursalService } from './sucursal/servicio/acsucursal.service';
import { MaUnidadNegocioService } from './unidadnegocio/servicio/maunidadnegocio.service';
import { AcCostCenterSelectorComponent } from './selectores/centroCosto/vista/accostcenter-selector/accostcentermstselector.component';
import { DepartmentMstService } from './selectores/departamentmst/servicio/departmentmst.service';
import { BancoServicio } from './banco/servicio/BancoServicio';
import { DepartamentoServicio } from './departamento/servicio/DepartamentoServicio';
import { ProvinciaServicio } from './provincia/servicio/ProvinciaServicio';
import { ZonapostalServicio } from './zonapostal/servicio/ZonapostalServicio';
import { UbicacionGeograficaSelectorComponent } from './selectores/ubicaciongeografica/ubicaciongeografica-selector.component';
import { HrDepartamentoSelectorComponent } from './departamento/vista/hrdepartamentoselector.component';
import { HrDepartamentoService } from './departamento/servicio/hrdepartamento.service';
import { EmpleadomastMultipleSelectorComponent } from './selectores/empleado/vista/empleadomastmultipleselector.component';
import { MyCurrencyPipe } from '../../base_module/util/pipes/MyCurrencyPipe';
import { LabelFromItem } from '../../base_module/util/pipes/LabelFromItem.pipe';
import { PopUpMantenimientoMiscelaneoComponent } from './miscelaneos/vista/miscelaneo-mantenimiento-popup.component';
import { EdadCadenaPipe } from 'src/app/base_module/util/pipes/edadCadena.pipe';
import { PuestoempresaSelectorComponent } from './selectores/puestoempresa/vista/Puestoempresaselector.component';
import { PuestoempresaServicio } from './selectores/puestoempresa/servicio/PuestoempresaServicio';
import { UsuarioServicio } from './usuario/servicio/UsuarioServicio';
import { UsuarioSelectorComponent } from './selectores/usuario/vista/usuario-selector.component';
import { PsItemSelectorComponent } from './selectores/item/item-selector.component';
import { PsBeneficiarioMultipleSelectorComponent } from './selectores/psbeneficiario/vista/psbeneficiariomultipleselector.component';
import { AfeSelectorComponent } from './afe/vista/afeselector.component';
import { AfemstService } from './afe/servicio/afemst.service';
import { PsEmpleadoMultipleSelectorComponent } from '../programasocial/psempleado/vista/psempleadomultipleselector.component';
import { PsEmpleadoServicio } from '../programasocial/psempleado/servicio/PsEmpleadoServicio';
import { AreamineduServicio } from './area/servicio/areaminedu.service';
import { AreamineduSelectorComponent } from './area/vista/areaminedu-selector.component';
import { PaisServicio } from '../covid/pais/servicio/PaisServicio';

@NgModule({
    declarations: [
        LabelFromItem,
        MyCurrencyPipe,
        AprobacionPreguntasComponent,
        EmpleadomastSelectorComponent,
        EmpleadomastMultipleSelectorComponent,
        AcCostCenterSelectorComponent,
        HrDepartamentoSelectorComponent,
        UbicacionGeograficaSelectorComponent,
        PuestoempresaSelectorComponent,
        UsuarioSelectorComponent,
        PsItemSelectorComponent,
        PsBeneficiarioMultipleSelectorComponent,
        /*Ernesto */
        PopUpMantenimientoMiscelaneoComponent,
        EdadCadenaPipe,
        PsEmpleadoMultipleSelectorComponent,
        AfeSelectorComponent,
        AreamineduSelectorComponent

    ],
    imports: [
        CommonModule,
        FormsModule,

        DialogModule,
        InputTextareaModule,
        PanelModule,
        RadioButtonModule,
        DropdownModule, InputMaskModule, ButtonModule,
        TableModule, GrowlModule, BlockUIModule,
        InputTextModule, SpinnerModule, CalendarModule, CheckboxModule,
        ConfirmDialogModule, TabViewModule, FieldsetModule, DataTableModule,

    ],

    exports: [AprobacionPreguntasComponent, EmpleadomastSelectorComponent,
        UbicacionGeograficaSelectorComponent,
        HrDepartamentoSelectorComponent, EmpleadomastMultipleSelectorComponent, MyCurrencyPipe, LabelFromItem,
        /*Ernesto */

        PuestoempresaSelectorComponent,
        UsuarioSelectorComponent,
        PsItemSelectorComponent,
        PsBeneficiarioMultipleSelectorComponent,
        PsEmpleadoMultipleSelectorComponent,
        /*Ernesto */
        PopUpMantenimientoMiscelaneoComponent,
        EdadCadenaPipe,
        AfeSelectorComponent,
        PsEmpleadoMultipleSelectorComponent,

        AreamineduSelectorComponent
    ],

    providers: [
        MaMiscelaneosdetalleServicio, EmpleadomastServicio, CompanyownerServicio,
        ConfirmationService, PersonamastService, AcSucursalService, MaUnidadNegocioService,
        DepartmentMstService, HrDepartamentoService,
        BancoServicio, PaisServicio, DepartamentoServicio, ProvinciaServicio, ZonapostalServicio,

        /*Ernesto */
        PuestoempresaServicio,
        UsuarioServicio,
        AfemstService,
        PsEmpleadoServicio,
        AreamineduServicio,
        PaisServicio

    ],
})
export class SharedModule { }

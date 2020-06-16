
import { DataTableModule, InputTextModule, DialogModule, ButtonModule, CalendarModule, DropdownModule, CheckboxModule, ConfirmDialogModule, FieldsetModule, RadioButtonModule, PanelModule, InputMaskModule, KeyFilterModule, BlockUIModule, SpinnerModule, AccordionModule, TooltipModule, ToggleButtonModule, PickListModule, SelectButtonModule, MultiSelectModule, SidebarModule, OverlayPanelModule, ScheduleModule, InputSwitchModule, SliderModule } from "primeng/primeng";
import {TabViewModule} from 'primeng/tabview';
import { PersonaListadoComponent } from "./persona/vista/persona-listado.component";

import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { CommonModule } from "@angular/common";
import { PersonaMantenimientoComponent } from "./persona/vista/persona-mantenimiento.component";
import { ConocimientoListadoComponent } from "./conocimiento/vista/conocimiento-listado.component";
import { ConocimientoMantenimientoComponent } from "./conocimiento/vista/conocimiento-mantenimiento.component";
import { ConocimientoService } from "./conocimiento/servicio/conocimiento.service";
import { PersonaconocimientoListadoComponent } from "./personaconocimiento/vista/personaconocimiento-listado.component";
import { PersonaService } from "./persona/servicio/persona.service";
import { PersonaconocimientoServicio } from "./personaconocimiento/servicio/personaconocimiento.service";
import { SharedModule } from "../shared/shared.module";
import { NgxChartsModule } from "@swimlane/ngx-charts";
import { TableModule } from "primeng/table";
import { CurrencyMaskModule } from "ng2-currency-mask";
import { ContratacionListadoComponent } from "./contratacion/vista/contratacion-listado.component";
import { ContratacionService } from "./contratacion/servicio/contratacion.service";
import { ContratacionMantenimientoComponent } from "./contratacion/vista/contratacion-mantenimiento.component";
import { PersonatituloListadoComponent } from "./personatitulo/vista/personatitulo-listado.component";
import { PersonatituloServicio } from "./personatitulo/servicio/personatitulo.service";
import { ContratacionentregableListadoComponent } from "./contratacionadendaentregable/vista/contratacionentregable-listado.component";
import { ContratacionadendaentregableServicio } from "./contratacionadendaentregable/servicio/contratacionadendaentregable.service";
import { ContratacionadendaListadoComponent } from "./contratacionadendaentregable/vista/contratacionadenda-listado.component";
import { ProyectoListadoComponent } from "./proyecto/vista/proyecto-listado.component";
import { ProyectoMantenimientoComponent } from "./proyecto/vista/proyecto-mantenimiento.component";
import { ProyectoService } from "./proyecto/servicio/Proyecto.service";
import { ProyectoDatosgeneralesComponent } from "./proyecto/vista/proyecto-tab1.component";
import { ProyectoRecursosComponent } from "./proyecto/vista/proyecto-tab2.component";
import { ProyectoSeguimientoComponent } from "./proyecto/vista/proyecto-tab3.component";
import { ProyectoRiesgosComponent } from "./proyecto/vista/proyecto-tab4.component";
import { SelectorEquipoComponent } from "./proyecto/vista/selector-equipo.component";
import { ResumenlistadoComponent } from "./proyecto/vista/resumen-listado.component";
import { ContratacionNuevoComponent } from "./persona/vista/contratacion-nuevo.component";
import { OcupacionlistadoComponent } from "./persona/vista/ocupacion-listado.componente";
import { UsuarioListadoComponent } from "./usuario/vista/usuario-listado.component";

@NgModule({
    declarations:[
    PersonaListadoComponent,
    PersonaMantenimientoComponent,
    PersonaconocimientoListadoComponent,
    ConocimientoListadoComponent,
    ConocimientoMantenimientoComponent,
    ContratacionListadoComponent,
    ContratacionMantenimientoComponent,
    PersonatituloListadoComponent,
    ContratacionentregableListadoComponent,
    ContratacionadendaListadoComponent,
    ProyectoListadoComponent,
    ProyectoMantenimientoComponent,
    ProyectoDatosgeneralesComponent,
    ProyectoRecursosComponent,
    ProyectoSeguimientoComponent,
    ProyectoRiesgosComponent,
    SelectorEquipoComponent,
    ResumenlistadoComponent,
    ContratacionNuevoComponent,
    OcupacionlistadoComponent,
    UsuarioListadoComponent
    ],
    imports:[
        SharedModule,
        NgxChartsModule,
        CommonModule,
        FormsModule,
        TableModule,
        PanelModule,
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
        BlockUIModule,
        SpinnerModule,
        AccordionModule,
        TooltipModule,
        ToggleButtonModule,
        PickListModule,
        SelectButtonModule,
        MultiSelectModule,
        SidebarModule,
        OverlayPanelModule,
        ScheduleModule,
        InputSwitchModule,
        SliderModule                                    
    ],
    exports:[
    ],
    providers:[
    PersonaService,
    PersonaconocimientoServicio,
    ConocimientoService,
    ContratacionService,
    PersonatituloServicio,
    ContratacionadendaentregableServicio,
    ProyectoService
    ]
})

export class MineduModule{}

import { DataTableModule, InputTextModule, DialogModule, ButtonModule, CalendarModule, DropdownModule, CheckboxModule, ConfirmDialogModule, FieldsetModule, RadioButtonModule, PanelModule, InputMaskModule, KeyFilterModule, BlockUIModule, SpinnerModule, AccordionModule, TooltipModule, ToggleButtonModule, PickListModule, SelectButtonModule, MultiSelectModule, SidebarModule, OverlayPanelModule, ScheduleModule, InputSwitchModule, SliderModule } from "primeng/primeng";
import {TabViewModule} from 'primeng/tabview';
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { CommonModule } from "@angular/common";
import { SharedModule } from "../shared/shared.module";
import { NgxChartsModule } from "@swimlane/ngx-charts";
import { TableModule } from "primeng/table";
import { CurrencyMaskModule } from "ng2-currency-mask";
import { CiudadanoListadoComponent } from "./ciudadano/vista/ciudadano-listado.component";
import { CiudadanoService } from "./ciudadano/servicio/Ciudadano.service";
import { PaisServicio } from "./pais/servicio/PaisServicio";
import { TriajeListadoComponent } from "./triaje/vista/triaje-listado.component";
import { TriajeService } from "./triaje/servicio/Triaje.service";

@NgModule({
    declarations:[
        CiudadanoListadoComponent,
        TriajeListadoComponent,
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
        SliderModule,
        RadioButtonModule                                 
    ],
    exports:[
    ],
    providers:[
        CiudadanoService,
        PaisServicio,
        TriajeService
    ]
})

export class CovidModule{}

import { NgModule } from '@angular/core';
import { CommonModule, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { AppMenuComponent, AppSubMenuComponent } from './components/app.menu.component';
import { AppTopBarComponent } from './components/app.topbar.component';
import { AppFooterComponent } from './components/app.footer.component';
import { AppRightPanelComponent } from './components/app.rightpanel.component';

import { TableModule } from 'primeng/table';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
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
import { DialogModule } from 'primeng/dialog';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ScheduleModule } from 'primeng/schedule';
import { ChartModule } from 'primeng/chart';

import { MegaMenuModule } from 'primeng/megamenu';
import { FormsModule } from '@angular/forms';
import { MenuModule } from 'primeng/menu';
import { MenubarModule } from 'primeng/menubar';
import { ContextMenuModule } from 'primeng/contextmenu';
import { PanelMenuModule } from 'primeng/panelmenu';
import { SlideMenuModule } from 'primeng/slidemenu';
import { TabMenuModule } from 'primeng/tabmenu';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { ContraseniaMantenientoComponent } from './components/contrasenia-mantenimiento/contrasenia-mantenimiento.component';
import { EmpleadomastServicio } from '../erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { ParametrosmastServicio } from '../erp_module/sistemas/parametrosmast/servicio/ParametrosmastServicio';
import { DashboardDemoComponent } from '../demo/view/dashboarddemo.component';
import { LayoutComponent } from './components/layout.component';
import { SampleDemoComponent } from '../demo/view/sampledemo.component';
import { FormsDemoComponent } from '../demo/view/formsdemo.component';
import { DataDemoComponent } from '../demo/view/datademo.component';
import { PanelsDemoComponent } from '../demo/view/panelsdemo.component';
import { OverlaysDemoComponent } from '../demo/view/overlaysdemo.component';
import { MenusDemoComponent } from '../demo/view/menusdemo.component';
import { MessagesDemoComponent } from '../demo/view/messagesdemo.component';
import { MiscDemoComponent } from '../demo/view/miscdemo.component';
import { EmptyDemoComponent } from '../demo/view/emptydemo.component';
import { ChartsDemoComponent } from '../demo/view/chartsdemo.component';
import { FileDemoComponent } from '../demo/view/filedemo.component';
import { DocumentationComponent } from '../demo/view/documentation.component';
import { FileUploadModule, OverlayPanelModule, LightboxModule, ToolbarModule, InputMaskModule, DataTableModule } from 'primeng/primeng';
import { MessageModule } from 'primeng/message';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { StepsModule } from 'primeng/steps';
import { AccordionModule } from 'primeng/accordion';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CardModule } from 'primeng/card';
import { CarouselModule } from 'primeng/carousel';
import { ChipsModule } from 'primeng/chips';
import { CodeHighlighterModule } from 'primeng/codehighlighter';
import { ColorPickerModule } from 'primeng/colorpicker';
import { DataViewModule } from 'primeng/dataview';
import { EditorModule } from 'primeng/editor';
import { GalleriaModule } from 'primeng/galleria';
import { InplaceModule } from 'primeng/inplace';
import { InputSwitchModule } from 'primeng/inputswitch';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ListboxModule } from 'primeng/listbox';
import { MessagesModule } from 'primeng/messages';
import { MultiSelectModule } from 'primeng/multiselect';
import { OrderListModule } from 'primeng/orderlist';
import { OrganizationChartModule } from 'primeng/organizationchart';
import { PaginatorModule } from 'primeng/paginator';
import { PasswordModule } from 'primeng/password';
import { PickListModule } from 'primeng/picklist';
import { ProgressBarModule } from 'primeng/progressbar';
import { RatingModule } from 'primeng/rating';
import { SelectButtonModule } from 'primeng/selectbutton';
import { SliderModule } from 'primeng/slider';
import { SplitButtonModule } from 'primeng/splitbutton';
import { TerminalModule } from 'primeng/terminal';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { TooltipModule } from 'primeng/tooltip';
import { TreeModule } from 'primeng/tree';
import { TreeTableModule } from 'primeng/treetable';
import { AppMegamenuComponent } from './components/app.megamenu.component';
import { CarService } from '../demo/service/carservice';
import { EventService } from '../demo/service/eventservice';
import { CountryService } from '../demo/service/countryservice';
import { NodeService } from '../demo/service/nodeservice';
import { SharedModule } from '../erp_module/shared/shared.module';
import { NgxEditorModule } from 'ngx-editor';
import { AppBreadcrumbComponent } from './components/app.breadcrumb.component';
import { AppProfileComponent } from './components/app.profile.component';

@NgModule({

    imports: [

        /**/
        AccordionModule,
        AutoCompleteModule,
        BreadcrumbModule,
        ButtonModule,
        CalendarModule,
        CardModule,
        CarouselModule,
        ChartModule,
        CheckboxModule,
        ChipsModule,
        CodeHighlighterModule,
        ConfirmDialogModule,
        ColorPickerModule,
        ContextMenuModule,
        DataViewModule,
        DialogModule,
        DropdownModule,
        EditorModule,
        FieldsetModule,
        FileUploadModule,
        GalleriaModule,
        GrowlModule,
        InplaceModule,
        InputMaskModule,
        InputSwitchModule,
        InputTextModule,
        InputTextareaModule,
        LightboxModule,
        ListboxModule,
        MegaMenuModule,
        MenuModule,
        MenubarModule,
        MessageModule,
        MessagesModule,
        MultiSelectModule,
        OrderListModule,
        OrganizationChartModule,
        OverlayPanelModule,
        PaginatorModule,
        PanelModule,
        PanelMenuModule,
        PasswordModule,
        PickListModule,
        ProgressBarModule,
        RadioButtonModule,
        RatingModule,
        ScheduleModule,
        ScrollPanelModule,
        SelectButtonModule,
        SlideMenuModule,
        SliderModule,
        SpinnerModule,
        SplitButtonModule,
        StepsModule,
        TableModule,
        TabMenuModule,
        TabViewModule,
        TerminalModule,
        TieredMenuModule,
        ToggleButtonModule,
        ToolbarModule,
        TooltipModule,
        TreeModule,
        TreeTableModule,
        /* */
        SharedModule,
        CommonModule,
        ScrollPanelModule,
        FormsModule,
        TableModule, PanelModule, GrowlModule, BlockUIModule,
        InputTextModule, ButtonModule, DropdownModule,
        SpinnerModule, CalendarModule, CheckboxModule,
        ConfirmDialogModule, TabViewModule, FieldsetModule,
        DialogModule, RadioButtonModule, ScheduleModule, ChartModule,
        DataTableModule,
        NgxEditorModule,

        MegaMenuModule, MenuModule, MenubarModule, ContextMenuModule,
        PanelMenuModule, SlideMenuModule, TabMenuModule, TieredMenuModule,
    ],

    declarations: [
        DocumentationComponent,
        FileDemoComponent,
        ChartsDemoComponent,
        EmptyDemoComponent,
        MiscDemoComponent,
        MessagesDemoComponent,
        MenusDemoComponent,
        OverlaysDemoComponent,
        PanelsDemoComponent,
        DataDemoComponent,
        FormsDemoComponent,
        SampleDemoComponent,
        LayoutComponent,
        AppMegamenuComponent,
        AppRightPanelComponent,
        AppMenuComponent,
        AppSubMenuComponent,
        AppTopBarComponent,
        AppFooterComponent,
        DashboardDemoComponent,
        ContraseniaMantenientoComponent,
        AppBreadcrumbComponent,
        AppProfileComponent
    ],

    providers: [
        NodeService,
        CountryService,
        CarService,
        EventService,
        ConfirmationService,
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        EmpleadomastServicio,
        ParametrosmastServicio,
    ]

})
export class BaseModule { }

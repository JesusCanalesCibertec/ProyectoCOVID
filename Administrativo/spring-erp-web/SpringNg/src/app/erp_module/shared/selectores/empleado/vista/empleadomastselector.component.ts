import { BaseComponent } from '../../../../../base_module/components/basecomponent';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { FiltroPaginacionEmpleado } from '../dominio/filtropaginacionempleado';
import { DtoEmpleadoBasico } from '../dominio/DtoEmpleadoBasico';
import { SelectItem, DataTable, LazyLoadEvent } from 'primeng/primeng';
import { EmpleadomastServicio } from '../servicio/EmpleadomastServicio';
import { CompanyownerServicio } from '../../../companyowner/servicio/CompanyownerServicio';
import { AcSucursalService } from '../../../sucursal/servicio/acsucursal.service';
import { MaUnidadNegocioService } from '../../../unidadnegocio/servicio/maunidadnegocio.service';
import { NoAuthorizationInterceptor } from '../../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { ISelectorComponent } from '../../../../../base_module/components/selector.component';

@Component({
    selector: 'app-empleadomast-selector',
    templateUrl: './empleadomastselector.component.html'
})
export class EmpleadomastSelectorComponent extends BaseComponent implements OnInit, ISelectorComponent {

    @ViewChild(DataTable) dataTableComponent: DataTable;

    tag: string = '';
    verSelector: boolean = false;
    estado: string = '';
    filtro: FiltroPaginacionEmpleado;
    registroSeleccionado: DtoEmpleadoBasico;
    verListado: boolean = false;

    lstCompania: SelectItem[] = [];
    lstSucursal: SelectItem[] = [];
    lstUnidad: SelectItem[] = [];
    lstEstado: SelectItem[] = [];
    verSelector2: boolean = false;
    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();

    constructor(
        private empleadomastService: EmpleadomastServicio,
        private companyownerService: CompanyownerServicio,
        private acSucursalService: AcSucursalService,
        private maUnidadNegocioService: MaUnidadNegocioService,
        messageService: MessageService) {
        super(messageService);
    }

    ngOnInit() {
        this.filtro = new FiltroPaginacionEmpleado();
        this.registroSeleccionado = new DtoEmpleadoBasico();
        this.verSelector = false;
    }

    inicializarSelector(tag: string) {
        this.tag = tag;
        this.iniciarComponente();
    }

    iniciarComponente() {
        this.filtro = new FiltroPaginacionEmpleado();
        this.filtro.empleadoEstado = this.estado;
        this.cargarCombos(
            () => this.llenarFiltros()
        );

        this.verListado = false;
    }

    llenarFiltros() {
        this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            empleado => {
                this.filtro.idSucursal = empleado.sucursalId;
                this.filtro.idCompaniaSocio = empleado.companiaId;
                this.filtro.empleadoJefe = empleado.personaId;
                this.dataTableComponent.reset();
            }
        );
    }

    cargarCombos(callBack?: () => void): void {
        this.verSelector = true;
        this.lstSucursal = [];
        this.lstSucursal.push({ label: ' -- Todos -- ', value: '' });
        const p1 = this.acSucursalService.listarTodos().then(respuesta => {
            respuesta.forEach(obj => this.lstSucursal.push({ label: obj.descripcionlocal, value: obj.sucursal.trim() }));
        });

        this.lstUnidad = [];
        this.lstUnidad.push({ label: ' -- Todos -- ', value: '' });
        const p3 = this.maUnidadNegocioService.listarTodos().then(respuesta => {
            respuesta.forEach(obj => this.lstUnidad.push({ label: obj.descripcionlocal, value: obj.unidadnegocio.trim() }));
            this.filtro.idUnidadNegocioAsignada = '';
        });

        this.lstCompania = [];
        this.lstCompania.push({ label: ' -- Todos -- ', value: '' });
        const p4 = this.companyownerService.listarCompaniasPorSeguridad()
            .then(td => {
                td.forEach(obj => this.lstCompania.push({ label: obj.descripcion, value: obj.codigo }));
            });

        Promise.all([p1, p3, p4]).then(
            r => {
                this.verSelector = true;
                // this.filtro.empleadoEstado = '0';
                this.dataTableComponent.reset();
                if (callBack) { callBack(); }
            }
        );
    }

    cargarSucursal(): any {
        this.lstSucursal = [];
        this.lstSucursal.push({ label: ' -- Todos -- ', value: '' });
        this.acSucursalService.listarTodos().then(respuesta => {
            respuesta.forEach(obj => this.lstSucursal.push({ label: obj.descripcionlocal, value: obj.sucursal.trim() }));
        });
    }

    cargarUnidad() {
        this.lstUnidad = [];
        this.lstUnidad.push({ label: ' -- Todos -- ', value: '' });
        this.maUnidadNegocioService.listarTodos().then(respuesta => {
            respuesta.forEach(obj => this.lstUnidad.push({ label: obj.descripcionlocal, value: obj.unidadnegocio.trim() }));
            this.filtro.idUnidadNegocioAsignada = '';
        });
    }

    cargarCompania() {
        this.lstCompania = [];
        this.lstCompania.push({ label: ' -- Todos -- ', value: '' });
        this.companyownerService.listarCompaniasPorSeguridad()
            .then(td => {
                td.forEach(obj => this.lstCompania.push({ label: obj.descripcion, value: obj.codigo }));
            });
    }

    onRowSelect(event: any) {
        const reg: any = new Object();
        reg.data = event.data;
        reg.tag = this.tag;
        this.cargarSeleccionEvent.emit(reg);
        this.salir();
    }

    salir() {
        this.verSelector = false;
        this.verSelector2 = false;
    }

    buscar(datatable?: any) {
        this.verListado = true;
        datatable.reset();
    }

    cargarListadoEvent(event: LazyLoadEvent) {
        if (!this.verSelector) {
            return;
        }
        if (!this.verListado) {
            this.verSelector2 = this.verSelector;
            return;
        }

        this.block.emit();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.empleadomastService.listarPaginacionEmpleadoSelector(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
                // this.verSelector2 = this.verSelector;
            });
    }

}



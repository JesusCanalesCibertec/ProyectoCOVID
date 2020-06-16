import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { FiltroPsEmpleado } from '../dominio/FiltroPsEmpleado';
import { DtoPsEmpleado } from '../dominio/DtoPsEmpleado';
import { SelectItem, DataTable, LazyLoadEvent } from 'primeng/primeng';

import { MessageService } from 'primeng/components/common/messageservice';
import { PsEmpleadoServicio } from '../servicio/PsEmpleadoServicio';
import { PsInstitucionServicio } from 'src/app/erp_module/programasocial/institucion/service/PsInstitucionServicio';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import { CompanyownerServicio } from 'src/app/erp_module/shared/companyowner/servicio/CompanyownerServicio';
import { AcSucursalService } from 'src/app/erp_module/shared/sucursal/servicio/acsucursal.service';

@Component({
    selector: 'app-psempleadomultiple-selector',
    templateUrl: './psempleadomultipleselector.component.html'
})
export class PsEmpleadoMultipleSelectorComponent extends BaseComponent implements OnInit {

    @ViewChild(DataTable) dataTableComponent: DataTable;

    tag: string = '';
    verSelector: boolean = false;
    max: number = 100000;

    filtro: FiltroPsEmpleado;
    registroSeleccionado: DtoPsEmpleado;
    verListado: boolean = false;
    lstArea: SelectItem[] = [];
    lstCompania: SelectItem[] = [];
    lstSucursal: SelectItem[] = [];
    lstUnidad: SelectItem[] = [];
    lstEstado: SelectItem[] = [];
    lstInstituciones: SelectItem[] = [];
    lstSexo: SelectItem[] = [];
    verSelector2: boolean = false;

    seleccionados: DtoPsEmpleado[] = [];

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();

    constructor(

        private PsEmpleadoService: PsEmpleadoServicio,
        private acSucursalService: AcSucursalService,
        private psInstitucionServicio: PsInstitucionServicio,
        messageService: MessageService) {
        super(messageService);
    }

    ngOnInit() {
        this.lstSexo.push({ label: " -- Todos --", value: null });
        this.lstSexo.push({ label: "Masculino", value: 'M' });
        this.lstSexo.push({ label: "Femenino", value: 'F' });

        this.cargarSucursal();
        this.filtro = new FiltroPsEmpleado();
        this.registroSeleccionado = new DtoPsEmpleado();
        this.verSelector = false;
    }

    inicializarSelector(tag: string) {
        this.iniciarComponente(tag, [], 1000000);
    }

    iniciarComponente(tag: string, listaPrevia: DtoPsEmpleado[], max: number) {
        this.block.emit();
        listaPrevia = listaPrevia == undefined || listaPrevia == null ? [] : listaPrevia.constructor === Array ? listaPrevia : [];

        this.tag = tag;
        this.seleccionados = listaPrevia;
        this.max = max == undefined || max == null ? 0 : max;
        this.cargarInstituciones();

        this.filtro = new FiltroPsEmpleado();

        //this.cargarCombos(() => this.llenarFiltros());
        this.unBlock.emit();
        this.verListado = false;
        this.verSelector = true
    }

    eliminar(dto: DtoPsEmpleado) {

        var listaNoElminar = [];

        for (var i = 0; i < this.seleccionados.length; i++) {

            if (this.seleccionados[i].idEmpleado == dto.idEmpleado && this.seleccionados[i].idEmpleado == dto.idEmpleado) {

            }
            else {

                listaNoElminar.push(this.seleccionados[i]);
            }
        }

        this.seleccionados = listaNoElminar;
    }
    /*
    llenarFiltros() {
        this.PsEmpleadoService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            empleado => {
                
                this.filtro.idSucursal = empleado.sucursalId;
                this.filtro.idCompaniaSocio = empleado.companiaId;
                this.filtro.empleadoJefe = empleado.personaId;
                this.unBlock.emit();
                //this.dataTableComponent.reset();
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
                this.dataTableComponent.reset();
                if (callBack) { callBack(); }
            }
        );
    }
    */

    cargarSucursal(): any {

        this.lstArea = [];

        this.lstArea.push({ label: ' -- Todos -- ', value: '' });
        
    }

    cargarInstituciones() {
        this.lstInstituciones = [];
        this.lstInstituciones.push({ label: '--Todos--', value: null });
        return this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                td.forEach(obj => this.lstInstituciones.push({ label: obj.nombre, value: obj.idInstitucion }));
                return 1;
            });
    }

    /*
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

    */

    onRowSelect(event: any) {
        /*
        if (this.seleccionados.length >= this.max) {
            this.mostrarMensajeAdvertencia("El máximo número de empleados es " + this.max);
            return;
        }*/
        const flag = event.flag == undefined ? false : event.flag;

        const reg = event.data;

        var duplicado = this.seleccionados.find(s => s.idEmpleado == reg.idEmpleado);

        if (duplicado == undefined) {
            let lst = [...this.seleccionados];
            lst.push(reg);
            this.seleccionados = lst;
        }
        else if (!flag) {
            this.mostrarMensajeAdvertencia("El empleado ya ha sido ingresado a la lista");
        }
    }

    aceptar() {
        const reg: any = new Object();
        reg.tag = this.tag;
        reg.data = this.seleccionados;
        this.cargarSeleccionEvent.emit(reg);
        this.salir();
    }

    salir() {
        this.verSelector = false;
        this.unBlock.emit();
    }

    buscar(datatable?: any) {
        this.verListado = true;
        datatable.reset();
    }

    cargarListadoEvent(event: LazyLoadEvent) {
        if (!this.verSelector) {
            return;
        }

        this.block.emit();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.PsEmpleadoService.listarPaginacionEmpleadoSelector(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
                // this.verSelector2 = this.verSelector;
            });
    }

    todos() {
        this.block.emit();

        var filtro2 = new FiltroPsEmpleado();
        filtro2.area = this.filtro.area;
        filtro2.codEmpleado = this.filtro.codEmpleado;
        filtro2.codInstitucion = this.filtro.codInstitucion;
        filtro2.edad = this.filtro.edad;
        filtro2.nomEmpleado = this.filtro.nomEmpleado;

        filtro2.paginacion.listaResultado = [];
        filtro2.paginacion.registroInicio = 0;
        filtro2.paginacion.cantidadRegistrosDevolver = 10000000;
        this.PsEmpleadoService.listarPaginacionEmpleadoSelector(filtro2)
            .then(pg => {

                pg.listaResultado.forEach(
                    rr => {
                        this.onRowSelect({ data: rr, flag: true });
                    }
                );

                this.unBlock.emit();
            });
    }

}









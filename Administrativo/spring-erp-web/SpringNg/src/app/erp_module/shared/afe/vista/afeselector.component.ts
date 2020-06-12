import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { SelectItem, LazyLoadEvent } from 'primeng/primeng';
import { DtoTabla } from '../../dominio/dto/DtoTabla';
import { Afemst } from '../dominio/afemst';
import { AfemstService } from '../servicio/afemst.service';

@Component({
    selector: 'afe-selector',
    templateUrl: './afeselector.component.html'
})
export class AfeSelectorComponent implements OnInit {

    tag: string = '';
    verSelector: Boolean = false;
    lstEstados: SelectItem[] = [];
    filtro: DtoTabla = new DtoTabla();
    registrosPorPagina: number = 7;
    registroSeleccionado: Afemst = new Afemst();

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();

    constructor(private afemstService: AfemstService) {
    }

    ngOnInit() {

    }
    cargarEstados() {
        this.lstEstados = [];
        this.lstEstados.push({ label: '--Todos--', value: '' });
        this.lstEstados.push({ label: 'Activo', value: 'A' });
        this.lstEstados.push({ label: 'Inactivo', value: 'I' });
    }
    inicializarSelector(tag: string) {
        this.tag = tag;
        this.iniciarComponente();
    }
    iniciarComponente() {
        this.filtro = new DtoTabla();
        this.block.emit();
        this.cargarEstados();
        this.filtro.estado = '';
        this.listarDefecto();
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
    }

    buscar(datatable?: any) {
        datatable.reset();
    }

    cargarCostCenter(event: LazyLoadEvent) {
        if (!this.verSelector) {
            return;
        }
        this.block.emit();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.afemstService.listarPaginacion(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
            });
    }

    listarDefecto() {

        this.filtro.paginacion.registroInicio = 0;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.registrosPorPagina;
        this.afemstService.listarPaginacion(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
                this.verSelector = true;
            });
    }

    preBuscar(event?: any, tb?: any) {
        if (event.keyCode === 13) {
            this.buscar(tb);
        }
    }

}

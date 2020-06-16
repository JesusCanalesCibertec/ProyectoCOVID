import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { SelectItem, LazyLoadEvent } from 'primeng/primeng';
import { AcCostcentermstService } from '../../servicio/accostcentermst.service';
import { FiltroPaginacionAcCostcentermst } from '../../dominio/filtropaginacionaccostcentermst';
import { AcCostcentermst } from '../../dominio/accostcentermst';
import { ISelectorComponent } from '../../../../../../base_module/components/selector.component';

@Component({
    selector: 'app-cost-center-selector',
    templateUrl: './accostcentermstselector.component.html'
})
export class AcCostCenterSelectorComponent implements OnInit, ISelectorComponent {

    tag: string = '';
    verSelector: Boolean = false;
    lstEstados: SelectItem[] = [];
    filtro: FiltroPaginacionAcCostcentermst = new FiltroPaginacionAcCostcentermst();
    registrosPorPagina: number = 7;
    registroSeleccionado: AcCostcentermst = new AcCostcentermst();

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();

    constructor(private acCostcentermstService: AcCostcentermstService) {
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
        this.filtro = new FiltroPaginacionAcCostcentermst();
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
        this.filtro.estado = 'A';
        this.acCostcentermstService.listarConPaginacion(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
            });
    }

    listarDefecto() {

        this.filtro.paginacion.registroInicio = 0;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.registrosPorPagina;
        this.filtro.estado = 'A';
        this.acCostcentermstService.listarConPaginacion(this.filtro)
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

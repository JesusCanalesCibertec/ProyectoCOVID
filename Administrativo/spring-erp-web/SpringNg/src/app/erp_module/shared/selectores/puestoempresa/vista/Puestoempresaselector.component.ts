import { Component, OnInit, EventEmitter, Output, ViewChild } from '@angular/core';
import { SelectItem, LazyLoadEvent, DataTable } from 'primeng/primeng';
import { Puestoempresa } from '../dominio/Puestoempresa';
import { PuestoempresaServicio } from '../servicio/PuestoempresaServicio';
import { FiltroPuestoempresa } from '../dominio/FiltroPuestoempresa';





@Component({
    selector: 'app-puestoempresa-selector',
    templateUrl: './Puestoempresaselector.component.html'
})

export class PuestoempresaSelectorComponent
    implements OnInit {



    @ViewChild(DataTable) dataTableComponent: DataTable;



    verSelector: Boolean = false;

    anio: string;

    mes: string;

    tag: string = '';

    lstEstados: SelectItem[] = [];

    filtro: FiltroPuestoempresa = new FiltroPuestoempresa();

    registroSeleccionado: Puestoempresa = new Puestoempresa();

    registrosPorPagina: number = 7;



    @Output() block = new EventEmitter();

    @Output() unBlock = new EventEmitter();

    @Output() cargarSeleccionEvent = new EventEmitter();



    constructor(private PuestoempresaService: PuestoempresaServicio) {
        
    }



    ngOnInit() {

    }



    iniciarComponente() {

        this.filtro =
            new FiltroPuestoempresa();

        this.dataTableComponent.reset();

        this.block.emit();

        this.listarDefecto();

    

    }



    listarDefecto() {

        this.filtro.paginacion.registroInicio =
            0;

        this.filtro.paginacion.cantidadRegistrosDevolver =
            this.registrosPorPagina;

        this.PuestoempresaService.listarPaginacion(this.filtro)

            .then(pg => {

                this.verSelector =
                    true;

                this.filtro.paginacion = pg;

                this.unBlock.emit();

            });

    }



    onRowSelect(event: any) {

        const reg: any =
            new Object();

        reg.data = event.data;

        reg.tag = this.tag;

        this.cargarSeleccionEvent.emit(reg);

    }



    salir() {

        this.verSelector =
            false;

    }


    buscar(datatable?: any) {

        datatable.reset();

    }

    cargar(event: LazyLoadEvent) {

        if (!this.verSelector) {

            return;

        }

        this.block.emit();

        this.filtro.paginacion.listaResultado = [];

        this.filtro.paginacion.registroInicio = event.first;

        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

        this.PuestoempresaService.listarPaginacion(this.filtro)

            .then(pg => {

                this.filtro.paginacion = pg;

                this.unBlock.emit();

            });

    }

    defaultBuscar(event?: any) {
        if (event.keyCode === 13) {
            this.buscar(this.dataTableComponent)
        }
    }




}




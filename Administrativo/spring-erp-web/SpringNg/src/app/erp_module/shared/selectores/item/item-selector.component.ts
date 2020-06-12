import { Component, OnInit, EventEmitter, Output, ViewChild } from '@angular/core';
import { SelectItem, LazyLoadEvent, DataTable } from 'primeng/primeng';
import { FiltroItem } from 'src/app/erp_module/programasocial/item/dominio/filtroitem';
import { DtoItem } from 'src/app/erp_module/programasocial/item/dominio/dtoItem';
import { PsItemServicio } from 'src/app/erp_module/programasocial/item/service/psItemServicio';


@Component({
    selector: 'app-item-selector',
    templateUrl: './item-selector.component.html'
})

export class PsItemSelectorComponent  implements OnInit {

    @ViewChild(DataTable) dataTableComponent: DataTable;



    verSelector: Boolean = false;

    anio: string;

    mes: string;

    tag: string = '';

    lstEstados: SelectItem[] = [];

    filtro: FiltroItem = new FiltroItem();

    registroSeleccionado: DtoItem = new DtoItem();

    registrosPorPagina: number = 7;



    @Output() block = new EventEmitter();

    @Output() unBlock = new EventEmitter();

    @Output() cargarSeleccionEvent = new EventEmitter();



    constructor(private PsItemService: PsItemServicio) {

    }



    ngOnInit() {

    }

    cargarEstados() {

        this.lstEstados = [];

        this.lstEstados.push({
            label:
                '--Todos--', value: ''
        });

        this.lstEstados.push({
            label:
                'Activo', value: 'A'
        });

        this.lstEstados.push({
            label:
                'Inactivo', value: 'I'
        });

    }

    iniciarComponente() {

        this.filtro =
            new FiltroItem();

        this.dataTableComponent.reset();

        this.filtro.estado = 'A';

        this.block.emit();

        this.listarDefecto();

        this.cargarEstados();

    }



    listarDefecto() {

        this.filtro.paginacion.registroInicio =
            0;

        this.filtro.paginacion.cantidadRegistrosDevolver =
            this.registrosPorPagina;

        this.PsItemService.listarPaginacion(this.filtro)

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



    guardar() {

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

        this.PsItemService.listarPaginacion(this.filtro)

            .then(pg => {

                this.filtro.paginacion = pg;

                this.unBlock.emit();

            });

    }



}




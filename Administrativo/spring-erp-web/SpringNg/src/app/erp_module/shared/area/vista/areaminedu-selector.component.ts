import { Component, OnInit, EventEmitter, Output, ViewChild } from '@angular/core';
import { SelectItem, LazyLoadEvent, DataTable } from 'primeng/primeng';
import { DtoTabla } from '../../dominio/dto/DtoTabla';
import { dtoAreaminedu } from '../dominio/dtoAreaminedu';
import { AreamineduServicio } from '../servicio/areaminedu.service';




@Component({
    selector: 'app-areaminedu-selector',
    templateUrl: './areaminedu-selector.component.html'
})

export class AreamineduSelectorComponent
    implements OnInit {



    @ViewChild(DataTable) dt: DataTable;



    verSelector: Boolean = false;

    anio: string;

    mes: string;

    tag: number = 0;

    lstEstados: SelectItem[] = [];

    filtro: DtoTabla = new DtoTabla();

    registroSeleccionado: dtoAreaminedu = new dtoAreaminedu();

    registrosPorPagina: number = 7;



    @Output() bloquear = new EventEmitter();

    @Output() desbloquear = new EventEmitter();

    @Output() cargarSeleccionEvent = new EventEmitter();



    constructor(private areamineduService: AreamineduServicio) {

    }



    ngOnInit() {

    }

 
    iniciarComponente(codigo: number) {

        this.tag = codigo;

        this.filtro =
            new DtoTabla();

        this.dt.reset();

        this.bloquear.emit();

        this.listarDefecto();


    }



    listarDefecto() {

        this.filtro.paginacion.registroInicio =
            0;

        this.filtro.paginacion.cantidadRegistrosDevolver =
            this.registrosPorPagina;

        this.areamineduService.listarPaginacion(this.filtro)

            .then(pg => {

                this.verSelector =
                    true;

                this.filtro.paginacion = pg;

                this.desbloquear.emit();

            });

    }

    defaultBuscar(event?: any) {
        if (event.keyCode === 13) {
            this.buscar(this.dt);
        }
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

        this.bloquear.emit();

        this.filtro.paginacion.listaResultado = [];

        this.filtro.paginacion.registroInicio = event.first;

        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

        this.areamineduService.listarPaginacion(this.filtro)

            .then(pg => {

                this.filtro.paginacion = pg;

                this.desbloquear.emit();

            });

    }



}




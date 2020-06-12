import { Component, OnInit, EventEmitter, Output, ViewChild } from '@angular/core';
import { SelectItem, LazyLoadEvent, DataTable } from 'primeng/primeng';
import { Usuario } from '../../../usuario/dominio/Usuario';
import { UsuarioServicio } from '../../../usuario/servicio/UsuarioServicio';
import { DtoTabla } from '../../../dominio/dto/DtoTabla';
import { FiltroUsuario } from '../../../usuario/dominio/FiltroUsuario';






@Component({
    selector: 'app-usuario-selector',
    templateUrl: './usuario-selector.component.html'
})

export class UsuarioSelectorComponent
    implements OnInit {



    @ViewChild(DataTable) dataTableComponent: DataTable;



    verSelector: Boolean = false;

    anio: string;

    mes: string;

    tag: string = '';

    lstEstados: SelectItem[] = [];

    filtro: FiltroUsuario = new FiltroUsuario();

    registroSeleccionado: Usuario = new Usuario();

    registrosPorPagina: number = 7;



    @Output() block = new EventEmitter();

    @Output() unBlock = new EventEmitter();

    @Output() cargarSeleccionEvent = new EventEmitter();



    constructor(private UsuarioService: UsuarioServicio) {
        
    }



    ngOnInit() {

    }



    iniciarComponente() {

        this.filtro =
            new FiltroUsuario();

        this.dataTableComponent.reset();

        this.block.emit();

        this.listarDefecto();

    

    }



    listarDefecto() {

        this.filtro.paginacion.registroInicio =
            0;

        this.filtro.paginacion.cantidadRegistrosDevolver =
            this.registrosPorPagina;

        this.UsuarioService.listarPaginacion(this.filtro)

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

        this.UsuarioService.listarPaginacion(this.filtro)

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




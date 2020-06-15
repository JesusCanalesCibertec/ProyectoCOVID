import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { LazyLoadEvent, SelectItem } from 'primeng/primeng';
import { DtoTabla } from '../../dominio/dto/DtoTabla';
import { DtoUbigeo } from './dominio/dto/DtoUbigeo';
import { DepartamentoServicio } from '../../departamento/servicio/DepartamentoServicio';
import { ProvinciaServicio } from '../../provincia/servicio/ProvinciaServicio';
import { PaisServicio } from 'src/app/erp_module/covid/pais/servicio/PaisServicio';

@Component({
    selector: 'app-selector-ubicaciongeografica',
    templateUrl: './ubicaciongeografica-selector.component.html'
})
export class UbicacionGeograficaSelectorComponent implements OnInit {
    verSelector: Boolean = false;
    filtro: DtoTabla = new DtoTabla();
    registrosPorPagina: number = 7;
    registroSeleccionado: DtoUbigeo = new DtoUbigeo();
    tag: string;
    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();
    paises: SelectItem[] = [];
    deps: SelectItem[] = [];
    provs: SelectItem[] = [];

    constructor(
        private departamentoServicio: DepartamentoServicio,
        private provinciaServicio: ProvinciaServicio,
        private paisServicio: PaisServicio
    ) {
    }

    ngOnInit() {
        this.paises = [];
        this.filtro.valor1 = null;
        this.paises.push({ label: '-Seleccione-', value: null });
        this.paisServicio.listarTodos().then(respuesta => {
            respuesta.forEach(dep => {
               // this.paises.push({ label: dep.descripcioncorta, value: dep.pais.trim() });

            });
            this.filtro.valor1 = "001";
            this.prim = true;
            this.cargarDepartamentosPorPais();
        });
    }

    prim: boolean = false;

    cargarDepartamentosPorPais() {
        this.deps = [];
        this.filtro.valor2 = null;
        this.deps.push({ label: '-- Seleccione --', value: null });
        this.departamentoServicio.listarActivosPorPais(this.filtro.valor1).then(respuesta => {
            respuesta.forEach(dep => {
                this.deps.push({ label: dep.descripcioncorta, value: dep.departamento.trim() });
            });
            if (this.prim) {
                this.filtro.valor2 = "15";
                this.prim = false;
            }
            this.cargarProvinciasPorDepartamento();
        });
    }

    cargarProvinciasPorDepartamento() {
        this.provs = [];
        this.provs.push({ label: '-- Seleccione --', value: null });
        this.provinciaServicio.listarActivosPorDepartamento(this.filtro.valor2).then(respuesta => {
            respuesta.forEach(prov => this.provs.push({ label: prov.descripcioncorta, value: prov.provincia.trim() }));
            this.filtro.valor3 = null;
        });
    }

    iniciarComponente() {
        this.filtro = new DtoTabla();
        this.filtro.valor1 = "001";
        this.filtro.valor2 = "15";
        this.block.emit();
        this.filtro.estado = '';
        this.listarDefecto();
    }

    onRowSelect(event: any) {
        var data = { tag: this.tag, data: event.data };
        this.cargarSeleccionEvent.emit(data);
        this.salir();
    }

    salir() {
        this.verSelector = false;
    }

    buscar(datatable?: any) {
        datatable.reset();
    }

    cargarPuesto(event: LazyLoadEvent) {
        if (!this.verSelector) {
            return;
        }
        this.block.emit();
        this.filtro.descripcion = this.tag;
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.filtro.estado = 'A';
        this.paisServicio.listarUbigeoPorFiltro(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
            });
    }

    listarDefecto() {
        this.filtro.descripcion = this.tag;
        this.filtro.paginacion.registroInicio = 0;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.registrosPorPagina;
        this.filtro.estado = 'A';
        this.paisServicio.listarUbigeoPorFiltro(this.filtro)
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

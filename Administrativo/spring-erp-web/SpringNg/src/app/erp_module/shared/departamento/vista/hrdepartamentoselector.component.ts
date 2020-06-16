import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { SelectItem, LazyLoadEvent } from 'primeng/primeng';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { DtoFiltro } from '../../dominio/dto/dtofiltro';
import { HrDepartamento } from '../dominio/hrdepartamento';
import { DtoTabla } from '../../dominio/dto/DtoTabla';
import { SyAprobacionNivelesDet } from '../../../sistemas/aprobacionniveles/dominio/syaprobacionnivelesdet';
import { HrDepartamentoService } from '../servicio/hrdepartamento.service';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-hr-departamento-selector',
    templateUrl: './hrdepartamentoselector.component.html'
})
export class HrDepartamentoSelectorComponent extends BaseComponent implements OnInit {

    verSelector: Boolean = false;
    lstEstados: SelectItem[] = [];
    filtro: DtoFiltro = new DtoFiltro();
    registrosPorPagina: number = 7;
    lstJefaturas: HrDepartamento[] = [];
    lstSeleccionada: HrDepartamento[] = [];

    det: SyAprobacionNivelesDet = new SyAprobacionNivelesDet();
    @Output() cargarDataEvent = new EventEmitter();

    constructor(private hrDepartamentoService: HrDepartamentoService, messageService: MessageService) {
        super(messageService);
    }

    ngOnInit() {

    }

    cargarEstados() {
        this.lstEstados = [];
        this.lstEstados.push({ label: '--Todos--', value: '' });
        this.lstEstados.push({ label: 'Activo', value: 'A' });
        this.lstEstados.push({ label: 'Inactivo', value: 'I' });
        this.filtro.estado = 'A';
    }
    iniciarComponente() {
        this.filtro = new DtoFiltro();
        this.lstJefaturas = [];

        this.cargarDatos();

        this.bloquearPagina();
        this.cargarEstados();
        this.buscar();
    }

    cargarDatos() {
        this.lstSeleccionada = [];
        const ls = [...this.lstSeleccionada];

        for (let i = 0; i < this.det.listaAreas.length; i++) {
            const dd = new HrDepartamento();
            dd.departamento = this.det.listaAreas[i].departamento;
            dd.descripcion = this.det.listaAreas[i].descripcion;
            ls.push(dd);
            this.lstSeleccionada = ls;
        }
    }

    salir() {

        this.verSelector = false;
    }

    buscar() {
        this.hrDepartamentoService.listar(this.filtro).then(
            res => {
                this.lstJefaturas = res;
                this.desbloquearPagina();
                this.verSelector = true;
            }
        );
    }
    onRowSelect(event: any) {
        const d = event.data;

        if (!this.validaRepetido(d)) {
            this.messageService.clear();
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Jefatura ya seleccionada' });
            return;
        }

        const ls = [...this.lstSeleccionada];
        const dd = new HrDepartamento();
        dd.departamento = d.departamento;
        dd.descripcion = d.descripcion;
        ls.push(dd);
        this.lstSeleccionada = ls;


    }
    validaRepetido(d: any): boolean {
        let valida = true;
        this.lstSeleccionada.forEach(trg => {
            if (trg.departamento === d.departamento.trim()) {
                valida = false;
            }
        });
        return valida;
    }

    quitarSeleccionado(el: HrDepartamento) {
        const index = this.lstSeleccionada.indexOf(el);
        this.lstSeleccionada = this.lstSeleccionada.filter((val, i) => i !== index);
    }

    aceptar() {
        let areas = '';
        this.lstSeleccionada.forEach(reg => {
            areas += reg.departamento + ',';
        });

        this.det.listaAreas = this.lstSeleccionada;
        this.det.area = areas.substring(0, areas.length - 1);

        this.cargarDataEvent.emit(this.det);
    }

}

import { BaseComponent } from './../../../../base_module/components/basecomponent';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DtoPsInstitucionperiodo } from '../dominio/dtoPsInstitucionperiodo';
import { SelectItem } from 'primeng/primeng';
import { PsInstitucionperiodoServicio } from '../service/PsInstitucionperiodoServicio';
import { PsInstitucionperiodo } from '../dominio/PsInstitucionperiodo';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-institucionperiodocopiar',
    templateUrl: './institucionperiodo-copiar.component.html'
})
export class InstitucionPeriodoCopiarComponent extends BaseComponent implements OnInit {
    constructor(
        private psInstitucionperiodoServicio: PsInstitucionperiodoServicio,
        private psInstitucionServicio: PsInstitucionServicio,
        messageService: MessageService,
    ) {
        super(messageService);
    }

    instituciones: SelectItem[] = [];
    aux: string;

    verSelector: Boolean = false;
    dtoPsInstitucionperiodo: DtoPsInstitucionperiodo = new DtoPsInstitucionperiodo();
    listaPeriodos: SelectItem[] = [];
    @Output() volverABuscar = new EventEmitter();

    verVentana(bean: PsInstitucionperiodo) {
        this.limpiar();
        this.dtoPsInstitucionperiodo.codInstitucion = bean.idInstitucion;


        this.verSelector = true;
    }

    ngOnInit(): void {
        this.dtoPsInstitucionperiodo.codPeriodo = '';
        this.cargarPeriodo();
        this.cargarInstituciones();
    }

    cargarInstituciones() {
        this.instituciones = [];
        this.instituciones.push({ label: '--Seleccione--', value: null });
        this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                td.forEach(obj => this.instituciones.push({ label: obj.nombre, value: obj.idInstitucion }));
                this.desbloquearPagina();
            });
    }

    cargarPeriodo() {
        this.listaPeriodos = [];
        this.listaPeriodos.push({ label: '--Seleccione--', value: '' });
        this.psInstitucionperiodoServicio.listarPeriodoSimple(
            this.dtoPsInstitucionperiodo.codPeriodo,
        ).then(respuesta => {
            respuesta.forEach(obj => this.listaPeriodos.push({ label: obj.nombre, value: obj.codigo }));
        });
    }

    validar() {
        this.messageService.clear();

        let valida = true;

        if (this.dtoPsInstitucionperiodo.codPeriodoCopiar === undefined ||
            this.dtoPsInstitucionperiodo.codPeriodoCopiar === null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Periodo A Copiar es obligatorio' });
            valida = false;
        }

        if (this.dtoPsInstitucionperiodo.codPeriodo === undefined ||
            this.dtoPsInstitucionperiodo.codPeriodo === null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Periodo es obligatorio' });
            valida = false;
        }

        if (this.dtoPsInstitucionperiodo.fechainicio == null || this.dtoPsInstitucionperiodo.fechainicio === undefined) {
            if (this.dtoPsInstitucionperiodo.fechafin == null || this.dtoPsInstitucionperiodo.fechafin === undefined) {
                if (this.dtoPsInstitucionperiodo.fechainicio >= this.dtoPsInstitucionperiodo.fechafin) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de Inicio debe ser menor a la fecha Fin' });
                    valida = false;
                }
            }
        }

        if (this.dtoPsInstitucionperiodo.fechainicioregistro == null || this.dtoPsInstitucionperiodo.fechainicioregistro === undefined) {
            if (this.dtoPsInstitucionperiodo.fechafinregistro == null || this.dtoPsInstitucionperiodo.fechafinregistro === undefined) {
                if (this.dtoPsInstitucionperiodo.fechainicioregistro >= this.dtoPsInstitucionperiodo.fechafinregistro) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de Inicio Registro debe ser menor a la fecha Fin Registro' });
                    valida = false;
                }
            }
        }

        return valida;

    }

    aceptar() {

        if (!this.validar()) {
            this.desbloquearPagina();
            return;
        }

        this.psInstitucionperiodoServicio.copiarPeriodo(this.dtoPsInstitucionperiodo).then(
            resp => {
                if (resp != null) {
                    
                    const reg: any = new Object()          
                    reg.data = this.dtoPsInstitucionperiodo;
                    this.volverABuscar.emit(reg);
                    this.mostrarMensajeExito("El periodo se registró con éxito");
                }


            }
        );
    }

    salir() {

        this.verSelector =
            false;

    }

    limpiar(){
        this.dtoPsInstitucionperiodo = new DtoPsInstitucionperiodo;
    }


}

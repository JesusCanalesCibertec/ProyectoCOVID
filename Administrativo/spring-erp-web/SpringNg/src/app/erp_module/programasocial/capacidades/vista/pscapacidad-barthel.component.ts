import { SelectItem } from 'primeng/primeng';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { DtoCapacidad } from '../dominio/dtocapacidad';
import { PsCapacidadesService } from '../servicio/pscapacidades.service';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-lista-barthel',
    templateUrl: './pscapacidad-barthel.component.html'
})
export class BarthelComponent extends BaseComponent implements OnInit {
    constructor(
        messageService: MessageService,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        private psCapacidadesService: PsCapacidadesService,
    ) { super(messageService); }

    dtoBarthel: DtoCapacidad = new DtoCapacidad();
    listaComida: SelectItem[] = [];
    listaLavarse: SelectItem[] = [];
    listaVestido: SelectItem[] = [];
    listaArreglarse: SelectItem[] = [];
    listaDeposicion: SelectItem[] = [];
    listaMiccion: SelectItem[] = [];
    listaUsoRetrete: SelectItem[] = [];
    listaTrasladarse: SelectItem[] = [];
    listaDeambular: SelectItem[] = [];
    listaEscaleras: SelectItem[] = [];
    cambios: Boolean = false;
    verSelector: Boolean = false;
    @Output() insertaBarthel = new EventEmitter();

    ngOnInit(): void {
        this.cargarListaComida();
        this.cargarListaLavarse();
        this.cargarListaVestido();
        this.cargarListaArreglarse();
        this.cargarListaDeposicion();
        this.cargarListaMiccion();
        this.cargarListaUsoRetrete();
        this.cargarListaTrasladarse();
        this.cargarListaDeambular();
        this.cargarListaEscaleras();
    }

    onChangeCambios() {
        this.cambios = true;
    }


    verVentana(rowData) {
        this.cambios = false;
        this.dtoBarthel = new DtoCapacidad();
        this.dtoBarthel = rowData;
        this.verSelector = true;
    }

    cargarListaComida() {
        this.listaComida.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL1)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaComida.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaLavarse() {
        this.listaLavarse.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL2)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaLavarse.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaVestido() {
        this.listaVestido.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL3)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaVestido.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaArreglarse() {
        this.listaArreglarse.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL4)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaArreglarse.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaDeposicion() {
        this.listaDeposicion.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL5)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaDeposicion.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaMiccion() {
        this.listaMiccion.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL6)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaMiccion.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaUsoRetrete() {
        this.listaUsoRetrete.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL7)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaUsoRetrete.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaTrasladarse() {
        this.listaTrasladarse.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL8)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaTrasladarse.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaDeambular() {
        this.listaDeambular.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL9)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaDeambular.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaEscaleras() {
        this.listaEscaleras.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_BARTHEL10)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaEscaleras.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    validar(): boolean {
        this.messageService.clear();
        let value = false;
        if (this.dtoBarthel.barthel1 === null || this.dtoBarthel.barthel1 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Comida' });
            value = true;
        }
        if (this.dtoBarthel.barthel2 === null || this.dtoBarthel.barthel2 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Lavarse(baño)' });
            value = true;
        }
        if (this.dtoBarthel.barthel3 === null || this.dtoBarthel.barthel3 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Vestido' });
            value = true;
        }
        if (this.dtoBarthel.barthel4 === null || this.dtoBarthel.barthel4 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Arreglarse' });
            value = true;
        }
        if (this.dtoBarthel.barthel5 === null || this.dtoBarthel.barthel5 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Deposición' });
            value = true;
        }
        if (this.dtoBarthel.barthel6 === null || this.dtoBarthel.barthel6 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Micción' });
            value = true;
        }
        if (this.dtoBarthel.barthel7 === null || this.dtoBarthel.barthel7 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Uso de Retrete' });
            value = true;
        }
        if (this.dtoBarthel.barthel8 === null || this.dtoBarthel.barthel8 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Trasladarse' });
            value = true;
        }
        if (this.dtoBarthel.barthel9 === null || this.dtoBarthel.barthel9 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Deambulación' });
            value = true;
        }
        if (this.dtoBarthel.barthel10 === null || this.dtoBarthel.barthel10 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Escaleras' });
            value = true;
        }

        return value;
    }

    aceptar() {
        if (this.validar()) {
            return;
        }

        this.messageService.clear();

        if (this.cambios) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Ha realizado cambios, por favor presione calcular antes de guardar' });
            return;
        }
        if (this.dtoBarthel.porcentajeGradoBarthel === null || this.dtoBarthel.porcentajeGradoBarthel === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No existe Puntuación, por favor seleccione dependencias y presione el botón calcular' });
            return;
        }


        this.insertaBarthel.emit(this.dtoBarthel);
        this.verSelector = false;
    }

    calcular() {
        this.cambios = false;
        this.psCapacidadesService.calcularBarthel(this.dtoBarthel).then(
            resp => {
                this.dtoBarthel.porcentajeGradoBarthel = resp.porcentajeGradoBarthel;
                this.dtoBarthel.gradoDependenciaBarthel = resp.gradoDependenciaBarthel;
            }
        );
    }


}

import { SelectItem } from 'primeng/primeng';
import { Component, OnInit, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { DtoCapacidad } from '../dominio/dtocapacidad';
import { PsCapacidadesService } from '../servicio/pscapacidades.service';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-lista-katz',
    templateUrl: './pscapacidad-katz.component.html'
})
export class KatzComponent extends BaseComponent implements OnInit {
    constructor(
        messageService: MessageService,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        private psCapacidadesService: PsCapacidadesService,
    ) {
        super(messageService);
    }

    verSelector: Boolean = false;
    dtoKatz: DtoCapacidad = new DtoCapacidad();
    valorInicial: string;
    listaBanio: SelectItem[] = [];
    listaVestido: SelectItem[] = [];
    listaUsoWC: SelectItem[] = [];
    listaMovilidad: SelectItem[] = [];
    listaContinencia: SelectItem[] = [];
    listaAlimentacion: SelectItem[] = [];
    cambios: Boolean = false;

    @Output() insertaKatz = new EventEmitter();
    ngOnInit(): void {
        this.cargarListaBanio();
        this.cargarListaVestido();
        this.cargarListaUsoWC();
        this.cargarListaMovilidad();
        this.cargarListaContinencia();
        this.cargarListaAlimentacio();
    }

    verVentana(rowData) {
        this.cambios = false;
        this.dtoKatz = new DtoCapacidad();
        this.dtoKatz = rowData;
        this.valorInicial = rowData.gradoDependenciaKatz;
        this.verSelector = true;
        console.log(this.dtoKatz);
    }

    onChangeCambios() {
        this.cambios = true;
    }


    validar(): boolean {
        this.messageService.clear();
        let value = false;
        if (this.dtoKatz.katz1 === null || this.dtoKatz.katz1 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Baño' });
            value = true;
        }
        if (this.dtoKatz.katz2 === null || this.dtoKatz.katz2 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Vestido' });
            value = true;
        }
        if (this.dtoKatz.katz3 === null || this.dtoKatz.katz3 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Uso del WC' });
            value = true;
        }
        if (this.dtoKatz.katz4 === null || this.dtoKatz.katz4 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Movilidad' });
            value = true;
        }
        if (this.dtoKatz.katz5 === null || this.dtoKatz.katz5 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Continencia' });
            value = true;
        }
        if (this.dtoKatz.katz6 === null || this.dtoKatz.katz6 === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe seleccionar un valor para el campo Alimentación' });
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
        if (this.dtoKatz.porcentajeGradoKatz === null || this.dtoKatz.porcentajeGradoKatz === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No existe Puntuación, por favor seleccione dependencias y presione el botón calcular' });
            return;
        }

        this.insertaKatz.emit(this.dtoKatz);
        this.verSelector = false;
    }

    calcular() {
        this.cambios = false;
        this.psCapacidadesService.calcularKatz(this.dtoKatz).then(
            resp => {
                this.dtoKatz.porcentajeGradoKatz = resp.porcentajeGradoKatz;
                this.dtoKatz.gradoDependenciaKatz = resp.gradoDependenciaKatz;
            }
        );
    }


    cargarListaBanio() {
        this.listaBanio.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_KATZ1)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaBanio.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaVestido() {
        this.listaVestido.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_KATZ2)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaVestido.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaUsoWC() {
        this.listaUsoWC.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_KATZ3)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaUsoWC.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaMovilidad() {
        this.listaMovilidad.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_KATZ4)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaMovilidad.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaContinencia() {
        this.listaContinencia.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_KATZ5)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaContinencia.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }

    cargarListaAlimentacio() {
        this.listaAlimentacion.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_KATZ6)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaAlimentacion.push({ label: obj.descripcionlocal, value: parseInt(obj.codigoelemento) }));
            });
    }
}

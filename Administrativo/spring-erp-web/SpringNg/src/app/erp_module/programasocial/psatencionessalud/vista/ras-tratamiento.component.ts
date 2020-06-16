import { DtoAtencion } from './../dominio/dtoatencion';
import { SelectItem } from 'primeng/primeng';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DtoTratamientos } from '../dominio/dtotratamiento';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsConstante } from '../../psconstantes.ts/psconstantes';

@Component({
    selector: 'app-tratamiento',
    templateUrl: './ras-tratamiento.component.html'
})
export class TratamientoComponent implements OnInit {
    constructor(
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
    ) { }

    dtoTratamientos: DtoTratamientos = new DtoTratamientos();
    listaTratamientos: SelectItem[] = [];
    posicion: number;

    verSelector: Boolean = false;
    @Output() insertaTratamientos = new EventEmitter();

    ngOnInit(): void {

    }

    verVentana(rowData, secuencia: number) {
        this.dtoTratamientos = new DtoTratamientos();
        this.dtoTratamientos.secuencia = secuencia;
        this.cargarListaTratamientos(secuencia, rowData);
    }

    obtenerDatosMiscelaneos(secuencia: number, rowData) {
        if (secuencia === 1) {
            this.dtoTratamientos.tratamiento1 = rowData.idTratamiento01_1;
            this.dtoTratamientos.tratamiento2 = rowData.idTratamiento01_2;
            this.dtoTratamientos.tratamiento3 = rowData.idTratamiento01_3;
        }

        if (secuencia === 2) {
            this.dtoTratamientos.tratamiento1 = rowData.idTratamiento02_1;
            this.dtoTratamientos.tratamiento2 = rowData.idTratamiento02_2;
            this.dtoTratamientos.tratamiento3 = rowData.idTratamiento02_3;
        }

        if (secuencia === 3) {
            this.dtoTratamientos.tratamiento1 = rowData.idTratamiento03_1;
            this.dtoTratamientos.tratamiento2 = rowData.idTratamiento03_2;
            this.dtoTratamientos.tratamiento3 = rowData.idTratamiento03_3;
        }

        if (secuencia === 4) {
            this.dtoTratamientos.tratamiento1 = rowData.idTratamiento04_1;
            this.dtoTratamientos.tratamiento2 = rowData.idTratamiento04_2;
            this.dtoTratamientos.tratamiento3 = rowData.idTratamiento04_3;
        }

        if (secuencia === 5) {
            this.dtoTratamientos.tratamiento1 = rowData.idTratamiento05_1;
            this.dtoTratamientos.tratamiento2 = rowData.idTratamiento05_2;
            this.dtoTratamientos.tratamiento3 = rowData.idTratamiento05_3;
        }

    }

    cargarListaTratamientos(secuencia: number, rowData: DtoAtencion) {
        this.dtoTratamientos.tratamiento1 = null;
        this.listaTratamientos = [];
        this.listaTratamientos.push({ label: '--Seleccione--', value: '' });
        this.maMiscelaneosdetalleServicio.
            listarActivosBean(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_TRATAMIENTOS)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaTratamientos.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                this.obtenerDatosMiscelaneos(secuencia, rowData);
                this.verSelector = true;
            });
    }

    aceptar() {
        this.insertaTratamientos.emit(this.dtoTratamientos);
        this.verSelector = false;
    }
}

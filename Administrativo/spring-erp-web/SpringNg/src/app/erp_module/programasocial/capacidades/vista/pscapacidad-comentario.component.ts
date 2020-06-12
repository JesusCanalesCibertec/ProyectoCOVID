import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DtoCapacidad } from '../dominio/dtocapacidad';

@Component({
    selector: 'app-capacidadcomentario',
    templateUrl: './pscapacidad-comentario.component.html'
})
export class PsCapacidadComentarioComponent implements OnInit {
    constructor() { }
    dtoCapacidad: DtoCapacidad;
    comentario: string;
    tipoComentario: string;
    verBotonAplicar: Boolean = false;

    verSelector: Boolean = false;
    @Output() insertaComentario = new EventEmitter();


    ngOnInit(): void {
        this.dtoCapacidad = new DtoCapacidad();
    }

    verVentana(rowData, tipo: string, accion: string) {
        if (accion === 'nuevo') {
            this.verBotonAplicar = true;
        } else {
            this.verBotonAplicar = false;
        }

        if (tipo === 'Capacitacion') {
            this.comentario = rowData.comentarioCapacidad;
        }
        if (tipo === 'Rendimiento') {
            this.comentario = rowData.comentarioRendimiento;
        }
        if (tipo === 'Retraso') {
            this.comentario = rowData.comentarioRetraso;
        }
        if (tipo === 'Taller') {
            this.comentario = rowData.comentarioTalleres;
        }

        this.tipoComentario = tipo;
        this.dtoCapacidad = rowData;
        this.verSelector = true;
    }

    aceptar() {
        if (this.tipoComentario === 'Capacitacion') {
            this.dtoCapacidad.comentarioCapacidad = this.comentario;
        }
        if (this.tipoComentario === 'Rendimiento') {
            this.dtoCapacidad.comentarioRendimiento = this.comentario;
        }
        if (this.tipoComentario === 'Retraso') {
            this.dtoCapacidad.comentarioRetraso = this.comentario;
        }
        if (this.tipoComentario === 'Taller') {
            this.dtoCapacidad.comentarioTalleres = this.comentario;
        }

        this.insertaComentario.emit(this.dtoCapacidad);
        this.verSelector = false;
    }


}

import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';

export class FiltroItem {

    constructor() {
        this.cantidad = 10;
        this.paginacion = new ParametroPaginacionGenerico();
        this.paginacion.cantidadRegistrosDevolver = this.cantidad;
    }
    paginacion: ParametroPaginacionGenerico;

    codigo: string;
    nombre: string;
    estado: string;

    cantidad: number;

}

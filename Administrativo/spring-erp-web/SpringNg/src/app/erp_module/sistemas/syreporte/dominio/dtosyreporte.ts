import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';

export class DtoSyReporte {

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    aplicacionCodigo: string;
    aplicacionDescripcion: string;
    reporteCodigo: string;
    nombre: string;
    tipoReporte: string;
    estado: string;
}

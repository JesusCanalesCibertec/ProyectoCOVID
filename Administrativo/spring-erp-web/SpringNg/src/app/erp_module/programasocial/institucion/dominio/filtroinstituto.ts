import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';

export class FiltroInstitucion {

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    codigo: string;
    nombre: string;
    estado: string;

}

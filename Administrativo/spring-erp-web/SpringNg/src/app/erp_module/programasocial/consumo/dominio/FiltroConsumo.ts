import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';

export class FiltroConsumo {

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    codigoInstitucion: string;
    nombreInstitucion: string;
    codigoPeriodo: string;
    fechainicial: Date;
    fechafinal: Date;
    estado: string;

}

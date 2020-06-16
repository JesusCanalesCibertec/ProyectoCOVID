import { ParametroPaginacionGenerico } from '../../../dominio/ParametroPaginacionGenerico';

export class FiltroPaginacionAcCostcentermst {
    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;
    codigo: string;
    nombre: string;
    estado: string;
}

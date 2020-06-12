import { ParametroPaginacionGenerico } from '../../../dominio/ParametroPaginacionGenerico';
export class FiltroPaginacionCumpleanio {
    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    idPersona: number;
    idCentroCostos: string;
    fechaInicio: Date;
    fechaFin: Date;
    compania: string;
    dia: number;
    mes: number;
}

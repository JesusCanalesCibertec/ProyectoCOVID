import { ParametroPaginacionGenerico } from '../../../dominio/ParametroPaginacionGenerico';
export class FiltroPaginacionAniversario {
    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    idPersona: number;
    idCentroCostos: string;
    fechaInicio: Date;
    fechaFin: Date;

    mes: number;
    dia: number;
    anios: number;
    signo: string;

    compania: string;

}
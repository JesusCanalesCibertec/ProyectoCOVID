import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';

export class FiltroBeneficiario {

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    codigo: number;
    nombre: string;
    dni: string;
    institucion: string;
    programa: string;
    estado: string;
    desdeNac: Date;
    hastaNac: Date;
    desdeReg: Date;
    hastaReg: Date;
    sexo: string;
    edad: number;
    area: string;

    orden: number;
    sentido: number;
}

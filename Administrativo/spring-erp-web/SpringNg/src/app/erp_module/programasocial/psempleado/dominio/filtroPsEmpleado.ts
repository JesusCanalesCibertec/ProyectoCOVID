import { ParametroPaginacionGenerico } from "src/app/erp_module/shared/dominio/ParametroPaginacionGenerico";

export class FiltroPsEmpleado {
    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    codInstitucion: string;
    codEmpleado: number;
    nomEmpleado: string;
    numDocumento: string;
    edad: number;
    area: string;
    sexo: string;

}

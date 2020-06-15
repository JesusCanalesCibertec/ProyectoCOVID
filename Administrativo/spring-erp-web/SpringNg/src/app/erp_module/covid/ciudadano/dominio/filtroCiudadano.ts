import { ParametroPaginacionGenerico } from "src/app/erp_module/shared/dominio/ParametroPaginacionGenerico";

export class FiltroCiudadano {
    documento: string;
    nombre: string;
    pais: string;
    departamento: string;
    provincia: Date;
    distrito: string;
    direccion: string;
    estado: number;

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;
}


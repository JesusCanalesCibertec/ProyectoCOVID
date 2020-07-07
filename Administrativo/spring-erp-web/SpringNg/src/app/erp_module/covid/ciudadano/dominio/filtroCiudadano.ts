import { ParametroPaginacionGenerico } from "src/app/erp_module/shared/dominio/ParametroPaginacionGenerico";

export class FiltroCiudadano {
    documento: string;
    nombre: string;
    pais: string;
    departamento: string;
    provincia: string;
    distrito: string;
    direccion: string;
    estado: number;
    tipodocumento: number;

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;
}


import { ParametroPaginacionGenerico } from "../../../dominio/ParametroPaginacionGenerico";


export class FiltroPuestoempresa {

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    codigo: number;
    nombre: string;

    
}
